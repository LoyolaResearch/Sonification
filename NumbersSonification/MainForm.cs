



using MIDI;
using System.Timers;


//https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-8.0
namespace NumbersSonification
{
    public partial class MainForm : Form
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        string outputFilename = "testmid.mid";
        string inputFileName = "";
        MIDI.Song mySong;

        string numberString;

        protected Graphics? myGraphics;



        public MainForm()
        {
            InitializeComponent();
            mySong = new MIDI.Song();
            numberString = string.Empty;

            player.URL = outputFilename;
            player.Ctlcontrols.stop();

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 10;

        }
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (player != null && player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                int noteNum = 0;
                Note currentNote = mySong.GetCurrentNote(ref noteNum);
                int numNotes = mySong.GetNoteCount();
                if (currentNote != null)
                {
                    //lblMessages.Text = "Note at " + (noteNum + 1) + " of " + numNotes + ": \'" + numberString[noteNum] + "\' - " + currentNote + " ms";
                    lblMessages.Text = "Note at " + (noteNum + 1) + " of " + numNotes + ": (" + currentNote + ")";
                }
            }
        }


        private void btnLoadTxtFile_Click(object sender, EventArgs e)
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
            string? tmpString;
            numberString = "";
            lblFeedback.Text = "";
            // now read the number from the file
            // https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
            try
            {
                StreamReader sr = new StreamReader(inputFileName);
                tmpString = sr.ReadLine();
                while (tmpString != null)
                {
                    lblFeedback.Text += tmpString + "\n";
                    numberString += tmpString + "\n";
                    tmpString = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception er) { Console.WriteLine("Exception: " + er.Message); }

        }
        private void btnGenerateMIDI_Click(object sender, EventArgs e)
        {
            if (numberString == null || numberString == "") return;
            mySong.Clear();

            player.URL = "";
            int length = numberString.Length;


            if (radioSimple.Checked)
            {
                char ch = 'C';
                int num = 0;
                int track = 0;
                double duration = 0.25;
                int currentNote = 0;
                int numTracks = tbNumTracks.Value;
                for (int i = 0; i < length; i++)
                {
                    if (chkNotePerLine.Checked)
                    {
                        String s = "";
                        Int64 num2;
                        while (numberString[i] != '\n')
                        {
                            s += numberString[i];
                            i++;
                        }
                        s += '\0';
                        if (Int64.TryParse(s, out num2))
                        {
                            num = (int)(num2 % 8);
                            lblMessages.Text = "num: " + num;
                        }
                    }
                    else
                    {
                        if (chkUseDuration.Checked && (i + 1) < length) // make sure there is still a note after this to use
                        {
                            num = numberString[i] - '0';
                            if (num < 0 || num > 9) num = 0;
                            if (num < 1) duration = 0.125;
                            else if (num < 4) duration = 0.250;
                            else if (num < 8) duration = 0.500;
                            else duration = 1.000;
                            i++;
                        }
                        num = numberString[i] - '0';
                    }
                    if (num < 0 || num > 9) num = 0;
                    ch = (char)('C' + num);
                    if (ch > 'G')
                        ch = (char)('A' + (ch - 'H'));
                    track = currentNote % numTracks;
                    currentNote++;
                    mySong.AddNote(track, ch, 'n', 4, duration);
                }
            }
            //else if (radTwoNote.Checked)

            mySong.SaveToFile(outputFilename);
            lblMessages.Text = "time: " + mySong.GetDuration() + "ms";
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            mySong.StartPlayback();
            //player.URL = outputFilename;
            player.Ctlcontrols.play();
            myTimer.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
        }

        private void tbNumTracks_Scroll(object sender, EventArgs e)
        {
            lblNumTracks.Text = "Number of Tracks (" + tbNumTracks.Value + "):";
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