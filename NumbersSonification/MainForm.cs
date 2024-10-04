



using System.Timers;


//https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-8.0
namespace NumbersSonification
{
    public partial class MainForm : Form
    {
        private static System.Timers.Timer? aTimer;

        string outputFilename = "testmid.mid";
        string inputFileName = "";


        protected Graphics? myGraphics;



        public MainForm()
        {
            InitializeComponent();
            player.URL = outputFilename;
            player.Ctlcontrols.stop();
        }

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
                Console.WriteLine("Currently playing at {0:HH:mm:ss.fff}", e.SignalTime);
            }
            else
            {
                Console.WriteLine("Stopped playing at {0:HH:mm:ss.fff}", DateTime.Now);
                aTimer.Stop();
                //aTimer.Dispose();
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Browse For Text Files";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            inputFileName = openFileDialog1.FileName;

            if (inputFileName == "") return;


        }
        private void btnGenerateMIDI_Click(object sender, EventArgs e)
        {

            player.URL = "";
            {
                MIDI.Song mySong = new MIDI.Song();

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
            player.URL = outputFilename;
            player.Ctlcontrols.play();
            SetTimer();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();

        }

        private void btnLoadTxtFile_Click(object sender, EventArgs e)
        {

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