/// <summary>
/// Song.cs
/// 
/// Encapsulate the various parts of a Song. This is where the MIDI stuff happens!
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

namespace MIDI
{
    internal class Song
    {
        const int TicksPerQuarterNote = 60; // Ticks per beat (used with BMP)

        // Song/MIDI stuff...
        protected List<Track> _tracks;
        protected string? _midiFileName;
        protected int _midiFileType;
        protected int _beatsPerMinute;
        protected long _absoluteLength; //the absolute time (in delta ticks) over the delta ticks per quarter note times
        protected bool _processed;
        protected int _numTracks;

        // Timing stuff...
        protected DateTime? _startTime;
        protected DateTime? _currentTime;

        // Calculated stuff...
        protected int _longestTrackNumber;
        protected long _longestTrackLength;

        public Song()
        {
            _tracks = new List<Track>();
            _midiFileName = "";
            _midiFileType = 0;
            _beatsPerMinute = 100;
            _absoluteLength = 0;
            _processed = false;

            _startTime = null;
            _currentTime = null;

            _longestTrackNumber = 0;
            _longestTrackLength = 0;
        }

        public void SetSong(string midiFileName, int numTracks, int numMoves, string pgnFile)
        {
            _midiFileName = midiFileName;
            _numTracks = numTracks;
            _startTime = null;
            _currentTime = null;
        }

        public void Clear()
        {
            int ct = _tracks.Count;
            for (int i = 0; i  < ct; i++)
                _tracks[i].Clear();
            _tracks.Clear();
            _tracks = new List<Track>();
        }

        public bool AddNote(int track, char noteName, char modifier, int octave, double duration, int velocity = 120, string patch = "")
        {
            // First check if we need to add more tracks
            if (_tracks.Count <= track)
            {
                int numTracks = _tracks.Count;
                for (int i = numTracks; i < track + 1; i++)
                    _tracks.Add(new Track());
            }

            Note nt = new Note();
            if (nt.Set(noteName, modifier, octave, duration, velocity, patch))
            {
                _tracks[track].AddNote(nt);
                return true;
            }
            Console.WriteLine("Error adding note! (" + noteName + "" + octave + ")");
            return false;
        }

        // Nora to do
        // convert string to note (like A4, Bf2)
        // allow for upper or lowercase and naturals specified or not. Basically, use this to call the previous function
        public bool AddNote(string note, int track = 0, double duration = 4, int velocity = 120, string patch = "")
        {
            char noteName = 'A';
            char modifier = 'n';
            int octave = 4;

            return AddNote(track, noteName, modifier, octave, duration, velocity, patch);
        }

        public void SaveToFile(string fileName)
        {
            Patch patch = new Patch();
            long absoluteTime = 0;
            int currentChannelNumber = 1;
            int currentTrackNumber = 0;
            long trackTime = 0;
            _absoluteLength = 0;

            int numTracks = _tracks.Count;
            if (numTracks <= 0) return;
            string? currentPatch = _tracks[0].GetPatch(0); // get the patch of the first note
            int currentPatchNumber = patch.GetValue(currentPatch ?? "");

            // Setup the header stuff...
            MidiEventCollection collection = new MidiEventCollection(_midiFileType, TicksPerQuarterNote);
            collection.AddEvent(new TextEvent("Note Stream", MetaEventType.TextEvent, absoluteTime), currentTrackNumber);
            absoluteTime++;
            collection.AddEvent(new TempoEvent(CalculateMicrosecondsPerQuarterNote(_beatsPerMinute), absoluteTime), currentTrackNumber);
            collection.AddEvent(new PatchChangeEvent(0, currentChannelNumber, currentPatchNumber), currentTrackNumber);

            // Now let the tracks do the work!
            _longestTrackLength = 0;
            _longestTrackNumber = 0;
            //foreach (Track track in _tracks)
            for (int t = 0; t < numTracks; t++)
            {
                Track track = _tracks[t];
                trackTime = track.ParseTrack(collection, absoluteTime, currentChannelNumber, TicksPerQuarterNote, currentTrackNumber);
                if (trackTime > _absoluteLength)
                {
                    _absoluteLength = trackTime;
                    _longestTrackNumber = t;
                    _longestTrackLength = CalculateMillisecondsFromTicks(_absoluteLength, _beatsPerMinute);
                }
            }
            //_absoluteTime--; // there is an extra tick for the header stuff
            collection.PrepareForExport();
            MidiFile.Export(fileName, collection);
            _processed = true;
        }
        protected static int CalculateMicrosecondsPerQuarterNote(int bpm)
        {
            return TicksPerQuarterNote * 1000 * 1000 / bpm;
        }

        public static long CalculateMillisecondsFromTicks(long ticks, int bpm)
        {
            // TICKS * (1BEAT / 100 TICKS) * (1 min / 120 BEAT) * (60 sec / 1 min) * (1000 ms / 1 sec)
            return (60000 * ticks / (TicksPerQuarterNote * bpm));
        }
        /// <summary>
        /// Return the length in ms
        /// </summary>
        public long GetDuration()
        {
            long length = 0;
            if (_processed)
            {
                // TICKS * (1BEAT / 100 TICKS) * (1 min / 120 BEAT) * (60 sec / 1 min) * (1000 ms / 1 sec)
                length = 60000 * _absoluteLength / (TicksPerQuarterNote * _beatsPerMinute);
            }
            return length;
        }

        public void StartPlayback()
        {
            _numTracks = _tracks.Count;
            _startTime = DateTime.Now;
        }
        public int GetTrackCount()
        {
            return _tracks.Count;
        }

        public int GetNoteCount(int trackNum = 0)
        {
            if (trackNum > _tracks.Count - 1) return 0;
            return _tracks[trackNum].GetNoteCount();
        }

        public Note GetNote(int trackNum = 0, int noteNum = 0)
        {
            if (trackNum > _tracks.Count - 1) return null;
            if (noteNum > _tracks[trackNum].GetNoteCount()) return null;
            return _tracks[trackNum].GetNote(noteNum);
        }

        public Note GetCurrentNote(ref int noteNum, int trackNum = 0)
        {
            if (trackNum > _tracks.Count - 1) return null;
            TimeSpan timeDiff = DateTime.Now - (DateTime)_startTime;
            double ms = timeDiff.TotalMilliseconds;
            noteNum = _tracks[trackNum].GetNoteNumberAtMS(ms);
            return _tracks[trackNum].GetNote(noteNum);
        }

    }

}
