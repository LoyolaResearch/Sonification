using ilf.pgn.Data;
using PgnToFenCore;
using PgnToFenCore.Conversion;


//https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-8.0
//https://learn.microsoft.com/en-us/previous-versions/windows/desktop/wmp/embedding-the-windows-media-player-control-in-a-c--solution
//https://learn.microsoft.com/en-us/previous-versions/windows/desktop/wmp/axwmplib-axwindowsmediaplayer-playstate--vb-and-c
//https://www.bytehide.com/blog/timer-csharp

//https://greenchess.net/info.php?item=downloads

using System.Timers;


//https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-8.0
namespace ChessSonification
{
    public partial class MainForm : Form
    {
        //private static System.Timers.Timer? aTimer;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        string pgnFilename = "Kasparov.pgn";
        string outputFilename = "testmid.mid";
        int currentGameNumber;
        int currentMoveNumber;
        PictureBox[,] pieces;
        ChessSong mySong;

        protected Graphics? myGraphics;

        GameList games;
        Database? gameDb;

        public MainForm()
        {
            InitializeComponent();
            games = new GameList();
            mySong = new ChessSong();
            gameDb = null;
            currentGameNumber = -1;
            currentMoveNumber = -1;
            player.URL = outputFilename;
            player.Ctlcontrols.stop();

            int xloc = imgBoard.Location.X;
            int yloc = imgBoard.Location.Y;
            int wd = imgBoard.Width / 8;
            int ht = imgBoard.Height / 8;
            pieces = new PictureBox[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    pieces[x, y] = new PictureBox();
                    pieces[x, y].Parent = imgBoard;
                    pieces[x, y].Location = new Point(xloc + (x * wd), yloc + imgBoard.Height - ht - (y * ht));
                    pieces[x, y].Visible = false;
                    pieces[x, y].SizeMode = PictureBoxSizeMode.StretchImage;
                    pieces[x, y].Size = new Size(wd, ht);
                    this.Controls.Add(pieces[x, y]); imgBoard.SendToBack();
                    pieces[x, y].BringToFront();
                }
            }

            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 50 ms.
            myTimer.Interval = 50;
            //myTimer.Start();
            //myTimer.Stop();

        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (currentGameNumber < 0) return;
                if (currentMoveNumber < 0) return;
                int moveNum = mySong.GetCurrentMoveNumber();
                if (moveNum < games.GetMoveCount(currentGameNumber))
                    lstMoves.SelectedIndex = moveNum;

            }
        }
        //myTimer.Stop();

        // Displays a message box asking whether to continue running the timer.
        //if (MessageBox.Show("Continue running?", "Count is: " + alarmCounter,
        //   MessageBoxButtons.YesNo) == DialogResult.Yes)
        //{
        // Restarts the timer and increments the counter.
        //    alarmCounter += 1;
        //    myTimer.Enabled = true;
        //}
        //else
        //{
        // Stops the timer.
        //    exitFlag = true;
        //}


        /*
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(100);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (currentGameNumber < 0) return;
                if (currentMoveNumber < 0) return;
                int moveNum = mySong.GetCurrentMoveNumber();
                if (moveNum < games.GetMoveCount(currentGameNumber))
                    lstMoves.SelectedIndex = moveNum;
                //Console.WriteLine("move" + mySong.GetCurrentMoveNumber());

                    //Console.WriteLine("Currently playing at {0:HH:mm:ss.fff}", e.SignalTime);
            }
            else
            {
                Console.WriteLine("Stopped playing at {0:HH:mm:ss.fff}", DateTime.Now);
                aTimer.Stop();
                //aTimer.Dispose();
            }
        }
        */

        private void btnLoadPGN_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Browse For Chess Files";
            openFileDialog1.DefaultExt = "pgn";
            openFileDialog1.Filter = "Chess files (*.pgn)|*.pgn|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            pgnFilename = openFileDialog1.FileName;

            if (pgnFilename == "") return;

            games.Clean();

            // read the fen stuff...
            PgnToFenConverter converter = new PgnToFenConverter();
            AdvancedSaveFensToListStrategy conversionStrategy = new AdvancedSaveFensToListStrategy();
            gameDb = converter.Convert(conversionStrategy, pgnFilename);

            foreach (var game in gameDb.Games)
            {
                string gameDetails = game.WhitePlayer + " vs " + game.BlackPlayer + " in " + game.Site + " (" + game.Year + ")";
                Console.WriteLine(gameDetails);
                games.Add(gameDetails);
                lstGames.Items.Add(gameDetails);
            }

            // The fen data is tricky in that all moves for all games are combined... Annoying,
            // but when the game number hits 1, it's the next game! But it hits one twice, so be careful!
            bool foundFirstFirst = false;
            int gameNum = -1;
            int moveNum = -1;
            string movePGN = "";
            char[] delimiterChars = { '.', ' ' };
            foreach (FenData fen in conversionStrategy.Positions)
            {
                moveNum = -1;
                if (fen.MoveNumber == 1)
                {
                    if (!foundFirstFirst) { gameNum++; foundFirstFirst = true; }
                    else { foundFirstFirst = false; }
                }
                //Console.WriteLine(gameNum + " - " + fen.Fen.ToString() + " - " + fen.TotalMaterialOnBoard);
                moveNum = games.AddMove(gameNum, fen.Fen.ToString(), fen.TotalMaterialOnBoard);
                if (moveNum > 0) // skip the first move because it is just the initial board
                {
                    movePGN = gameDb.Games[gameNum].MoveText[(moveNum - 1) / 2].ToString();
                    string[] parts = movePGN.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                    if (moveNum % 2 == 1) { movePGN = parts[1]; }
                    else { movePGN = parts[2]; }
                    games.SetMovePGN(gameNum, moveNum, movePGN);
                }
            }
            games.UpdateGames();
        }
        private void btnGenerateMIDI_Click(object sender, EventArgs e)
        {

            player.URL = "";
            //if (currentGameNumber != -1)
            {
                mySong.Clear();
                /*

                    Game? g = games.GetGame(currentGameNumber); // can be null... maybe check?
                    Move? move = null;
                    bool isMono = chkIsMono.Checked;
                    bool includeWhite = chkIncludeWhite.Checked;
                    bool includeBlack = chkIncludeBlack.Checked;
                    int numMoves = games.GetMoveCount(currentGameNumber);
                    int distance = 0;
                    char ch = 'C';
                    int whiteBlack = 0;
                    int trackNumber = 0;
                    bool isWhite = true;
                    for (int i = 1; i < numMoves; i++)
                    {
                        move = games.GetMove(currentGameNumber, i);
                        distance = games.GetMoveDistance(currentGameNumber, i);
                        //isWhite = move.
                        ch = (char)('C' + distance);
                        if (ch > 'G')
                            ch = (char)('A' + (ch - 'H'));
                        Console.WriteLine("Note: voice: " + whiteBlack + ", " + ch + "4" + " (distance of " + distance + ")");
                        if ((whiteBlack == 0 && includeWhite) && !mySong.AddNote(whiteBlack, ch, 'n', 4, 0.25))
                        {
                            Console.WriteLine("ERROR! " + i + "->" + distance);
                        }
                        if (!isMono && whiteBlack == 0) whiteBlack = 1;
                        else whiteBlack = 0;
                    }
                    mySong.SaveToFile(outputFilename);


                */
                mySong.AddNote(0, 'D', 'n', 4, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'B', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'E', 'n', 4, 0.25);
                mySong.AddNote(0, 'B', 'n', 4, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'D', 'n', 4, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 5, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'F', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'F', 'n', 4, 0.25); mySong.AddNote(1, 'E', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'E', 'n', 4, 0.25);
                mySong.AddNote(0, 'F', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 5, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'E', 'n', 4, 0.25); mySong.AddNote(1, 'E', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 5, 0.25); mySong.AddNote(1, 'B', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'E', 'n', 4, 0.25);
                mySong.AddNote(0, 'B', 'n', 4, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'B', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'E', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'F', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'F', 'n', 4, 0.25); mySong.AddNote(1, 'G', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 5, 0.25); mySong.AddNote(1, 'B', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'C', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 5, 0.25); mySong.AddNote(1, 'C', 'n', 5, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'B', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 5, 0.25); mySong.AddNote(1, 'B', 'n', 4, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'C', 'n', 4, 0.25);
                mySong.AddNote(0, 'B', 'n', 4, 0.25); mySong.AddNote(1, 'F', 'n', 4, 0.25);
                mySong.AddNote(0, 'G', 'n', 4, 0.25); mySong.AddNote(1, 'B', 'n', 4, 0.25);
                mySong.AddNote(0, 'F', 'n', 4, 0.25); mySong.AddNote(1, 'B', 'n', 4, 0.25);
                mySong.AddNote(0, 'E', 'n', 4, 0.25); mySong.AddNote(1, 'C', 'n', 5, 0.25);
                mySong.AddNote(0, 'B', 'n', 4, 0.25); mySong.AddNote(1, 'C', 'n', 5, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'C', 'n', 5, 0.25);
                mySong.AddNote(0, 'C', 'n', 4, 0.25); mySong.AddNote(1, 'C', 'n', 5, 0.25);
                mySong.SaveToFile(outputFilename);
                lblMessages.Text = "time: " + mySong.GetDuration() + "ms";
            }

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            mySong.StartPlayback();
            //player.URL = outputFilename;
            player.Ctlcontrols.play();
            myTimer.Start();
            //SetTimer();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();

        }
        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstMoves.Items.Clear();
            currentGameNumber = lstGames.SelectedIndex;
            currentMoveNumber = 0;

            if (currentGameNumber != -1)
            {
                int numMoves = games.GetMoveCount(currentGameNumber);
                for (int i = 0; i < numMoves; i++)
                {
                    lstMoves.Items.Add(i + ". " + games.GetMovePGN(currentGameNumber, i));
                }
                lstMoves.SelectedIndex = 0;
            }
            DisplayBoard();
        }

        private void btnNextMove_Click(object sender, EventArgs e)
        {
            currentMoveNumber++;
            if (currentMoveNumber < lstMoves.Items.Count) { lstMoves.SelectedIndex = currentMoveNumber; }
        }
        private void btnPrevMove_Click(object sender, EventArgs e)
        {
            currentMoveNumber--;
            if (currentMoveNumber >= 0) { lstMoves.SelectedIndex = currentMoveNumber; }
        }
        private void lstMoves_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentMoveNumber = lstMoves.SelectedIndex;
            DisplayBoard();
        }

        private void DisplayBoard()
        {
            char tileColor = 'W';
            char pieceColor = 'W';
            char piece = '-';
            string imageName;

            if (currentMoveNumber != 0)
            {
                txtMoveNumber.Text = "" + currentMoveNumber;
                if ((currentMoveNumber - 1) % 2 == 0) pieceColor = 'W';
                else pieceColor = 'b';
                txtMoveNumber.Text += pieceColor;
            }
            else
            {
                txtMoveNumber.Text = "";
            }

            if (currentMoveNumber != -1)
            {
                lblBoardSnapshot.Text = games.GetMoveBoard(currentGameNumber, currentMoveNumber);
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {

                        piece = games.GetMovePiece(currentGameNumber, currentMoveNumber, x, y);
                        if (piece == 'X' || piece == '_')
                        {
                            pieces[x, y].Visible = false;
                        }
                        else
                        {
                            tileColor = 'W';
                            if ((((x + y)) % 2) == 0) tileColor = 'B';
                            if (char.IsUpper(piece)) pieceColor = 'W';
                            else pieceColor = 'b';
                            imageName = "" + pieceColor + "" + piece + "_" + tileColor;
                            pieces[x, y].Image = (Image?)ChessSonification.Properties.Resources.ResourceManager.GetObject(imageName);
                            //pieces[x, y].Image = ChessSonification.Properties.Resources.WK_B;
                            pieces[x, y].Visible = true;
                        }

                    }
                }
            }
        }
    }

}










/*
             // typical args :
             //      guitar.mid E3 A3 D4 G4 B4 E5
             // or
             //      ukulele.mid G4 C4 E4 A4
             PitchParser pitchParser = new PitchParser();
             string[] args = { "E3", "A3", "D4", "G4", "B4", "E5" };
             IEnumerable<Pitch> midiValues = pitchParser.Parse(args);// "E3", "A3", "D4", "G4", "B4", "E5");

             if (midiValues.Any())
             {
                 var exporter = new MidiExporter();
                 exporter.SaveToFile(outputFilename, midiValues);
             }
 */

/*
var game = gameDb.Games[currentGameNumber];
var moves = new List<MoveTextEntry>();
moves = game.MoveText.ToList();
int count = moves.Count;
foreach (MoveTextEntry move in moves)
    lstMoves.Items.Add(move.ToString());
currentMoveNumber = -1;
btnNextMove_Click(sender, e);
//lblGameMoves.Text += move.ToString() + " - " + move.GetType() + "\n";
//lblGameMoves.Text = gameDb.Games[index].ToString();
*/




// 1 -> C
// 2 -> D
// 3 -> E
// 4 -> F
// 5 -> G
// 6 -> A
// 7 -> B
// 8 -> C

/*
 2, 4
 7, 5
1, 3
7, 6
2, 4
8, 4
4, 5
4, 3
1, 3
6, 5
1, 4
8, 6
3, 3
8, 7
1, 3
7, 6
1, 5
7, 5
3, 5
6, 5
4, 5
8, 7
1, 1
8, 8
1, 7
8, 7
1, 1
7, 6
5, 7
6, 7
3, 8
7, 8
1, 8












// Still need to make more interesting notes...
Song mySong = new Song();
mySong.AddNote(0, 'D', 'n', 4, 0.25);
mySong.AddNote(0, 'F', 'n', 4, 0.25);

mySong.AddNote(0, 'B', 'n', 4, 0.25);
mySong.AddNote(0, 'G', 'n', 4, 0.25);

mySong.AddNote(0, 'C', 'n', 4, 0.25);
mySong.AddNote(0, 'E', 'n', 4, 0.25);

mySong.AddNote(0, 'B', 'n', 4, 0.25);
mySong.AddNote(0, 'A', 'n', 4, 0.25);

mySong.AddNote(0, 'C', 'n', 4, 0.25);
mySong.AddNote(0, 'd', 'n', 4, 0.25);

mySong.AddNote(0, 'C', 'n', 4, 0.25);
mySong.AddNote(0, 'd', 'n', 4, 0.25);

mySong.AddNote(0, 'C', 'n', 4, 0.25);
mySong.AddNote(0, 'd', 'n', 4, 0.25);

mySong.AddNote(0, 'C', 'n', 4, 0.25);
mySong.AddNote(0, 'd', 'n', 4, 0.25);

mySong.AddNote(0, 'C', 'n', 4, 0.25);
mySong.AddNote(0, 'd', 'n', 4, 0.25);

 */

/*
mySong.AddNote(0, 'G', 'n', 4, 0.50, 120, "jazz");
mySong.AddNote(0, 'B', 'b', 4, 0.25);
mySong.AddNote(0, 'C', 'n', 5, 0.50);
mySong.AddNote(0, 'G', 'n', 4, 0.125);
mySong.AddNote(0, 'B', 'b', 4, 0.125);
mySong.AddNote(0, 'C', 'n', 5, 0.125);

mySong.AddNote(1, 'C', 'n', 3, 0.25);
mySong.AddNote(1, 'G', 'b', 3, 0.25);
mySong.AddNote(1, 'E', 'n', 3, 0.25, 120, "bass");
mySong.AddNote(1, 'C', 'n', 3, 0.25);
mySong.AddNote(1, 'g', 'b', 3, 0.25);
mySong.AddNote(1, 'C', 'n', 2, 0.25);
*/