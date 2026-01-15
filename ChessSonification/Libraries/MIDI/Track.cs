/// <summary>
/// Note.cs
/// 
/// Encapsulate the various parts of a Track. This is also where the MIDI stuff happens
/// Based initially on: https://gist.github.com/deejaygraham/2250ce39438e2aeef558
/// Utilizes: https://github.com/naudio/NAudio
/// 
/// Date created: 6/28/2024 (mcdermscott@gmail.com)
///  -Reinterpret SimpleMidiFileWrite 
/// Date modified: 9/14/2024 (mcdermscott@gmail.com)
///  -Split into separate files...
///  
/// </summary>


using NAudio.Midi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MIDI
{
    internal class Track
    {
        private List<Note> _notes;
        private string _currentPatch;
        private Patch _patch;
        private int _beatsPerMinute;

        public Track()
        {
            _notes = new List<Note>();
            _currentPatch = "steel";
            _patch = new Patch();
            _beatsPerMinute = 100;
        }

        public void Clear()
        {
            _notes.Clear();
            _notes = new List<Note>();
        }

        public void AddNote(Note nt)
        {
            string nPatch = nt.GetPatch();
            // Need to check if the patch has changed
            // first, set the default for the first note or any other in the track if necessary
            if (nPatch == "")
            {
                if (_patch.IsValid(_currentPatch))
                    nt.SetPatch(_currentPatch);
            }
            // otherwise update the current path with the new one
            else
            {
                _currentPatch = nPatch;
            }
            _notes.Add(nt);
        }

        public string? GetPatch(int noteNum)
        {
            if (noteNum > _notes.Count) return null;
            return _notes[noteNum].GetPatch();
        }

        public long ParseTrack(MidiEventCollection collection, long absoluteTime, int ChannelNumber, int TicksPerQuarterNote, int TrackNumber)
        {
            int midiValue;
            int duration; // number of ticks
            int velocity;
            int spaceBetweenNotes;
            string nextPatch = "";
            string currentPatch = _notes[0].GetPatch();
            int patchNumber;
            foreach (Note note in _notes)
            {
                nextPatch = note.GetPatch();
                if (nextPatch != currentPatch)
                {
                    currentPatch = nextPatch;
                    patchNumber = _patch.GetValue(nextPatch);
                    collection.AddEvent(new PatchChangeEvent(absoluteTime, ChannelNumber, patchNumber), TrackNumber);
                }

                spaceBetweenNotes = (int)(TicksPerQuarterNote * 0.05);
                midiValue = note.CalculateMidiValue();
                duration = (int)(TicksPerQuarterNote * 4 * note.GetDuration()) - spaceBetweenNotes;
                velocity = note.GetVelocity();
                collection.AddEvent(new NoteOnEvent(absoluteTime, ChannelNumber, midiValue, velocity, duration), TrackNumber);
                collection.AddEvent(new NoteEvent(absoluteTime + duration, ChannelNumber, MidiCommandCode.NoteOff, midiValue, 0), TrackNumber);
                note.SetTime(Song.CalculateMillisecondsFromTicks(absoluteTime, _beatsPerMinute), Song.CalculateMillisecondsFromTicks(absoluteTime + duration, _beatsPerMinute));
                Console.WriteLine(note.ToString());
                absoluteTime += duration + spaceBetweenNotes;
            }
            return absoluteTime;
            //the absolute time (in delta ticks) over the delta ticks per quarter note times
            //the number of milliseconds per quarter note = time in milliseconds
        }

        public void SetBMP(int bpm)
        {
            _beatsPerMinute = bpm;
        }

        public int GetNoteCount()
        {
            return _notes.Count;
        }
        public int GetNoteNumberAtMS(double ms)
        {
            int noteNum = 0;
            int count = _notes.Count - 1;
            for (noteNum = 0; noteNum < count; noteNum++)
            {   // now check each note if it is in the time
                if (_notes[noteNum].IsStartAfter(ms) && _notes[noteNum + 1].IsStartBefore(ms))
                {
                    return noteNum;
                }
            }
            return noteNum;
        }
        public Note GetNote(int noteNum = 0)
        {
            if (noteNum > _notes.Count) return null;
            return _notes[noteNum];
        }
    }

}
