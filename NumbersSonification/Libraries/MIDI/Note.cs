
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

/// <summary>
/// Note.cs
/// 
/// Encapsulate the various parts of a note.
/// Based initially on: https://gist.github.com/deejaygraham/2250ce39438e2aeef558
/// Utilizes: https://github.com/naudio/NAudio
/// 
/// Date created: 6/28/2024 (mcdermscott@gmail.com)
///  -Reinterpret SimpleMidiFileWrite 
/// Date modified: 9/14/2024 (mcdermscott@gmail.com)
///  -Split into separate files...
///  
/// </summary>

namespace MIDI
{
    internal class Note
    {
        protected char _noteName; // lowercase letter like 'c' or 'g' or 'r' for rest
        protected char _modifier; // either f or b for Flat, F or B for double flat, s or # for sharp, S or x for double sharp, or n or 0 for natural
        protected int _octave;
        protected double _duration; // 1.0 for whole note, 0.25 for quarter, etc...
        protected int _velocity;   // (softest) 0...255 (loudest)
        protected string _patch;

        // Time stuff (not part of a MIDI file)... Stored in ms
        private long _startTime;
        private long _endTime;

        public Note()
        {
            _noteName = 'c';
            _modifier = 'n';
            _octave = 4;
            _duration = 0.25;
            _velocity = 120;
            _patch = "steel";

            _startTime = 0;
            _endTime = 0;
        }

        public bool Set(char noteName, char modifier, int octave, double duration, int velocity = 120, string patch = "")
        {
            // First fix the note name, modifier, etc...
            noteName = Char.ToLower(noteName);
            if ((noteName < 'a' || noteName > 'g') && noteName != 'r') return false;
            if (modifier != 'f' && modifier != 'b' && modifier != 'F' && modifier != 'B' &&
                modifier != 's' && modifier != '#' && modifier != 'S' && modifier != 'x' &&
                modifier != 'n' && modifier != 0) return false;
            if (octave < 0 || octave > 8) return false;
            //if (patch == "") patch = "steel";

            _noteName = noteName;
            _modifier = modifier;
            _octave = octave;
            _duration = duration;
            _velocity = velocity;
            _patch = patch;

            return true;
        }


        public override string ToString()
        {
            string str = "";
            str += char.ToUpper(_noteName) + "" + _modifier + "" + _octave;
            str += " from " + _startTime + "ms to " + _endTime + "ms";
            return str;
        }

        public int CalculateMidiValue()
        {
            int value = 0;
            int pitchOffset = -9999;
            int pitchModifier = -9999;
            if (_noteName == 'c') pitchOffset = 0;
            if (_noteName == 'd') pitchOffset = 2;
            if (_noteName == 'e') pitchOffset = 4;
            if (_noteName == 'f') pitchOffset = 5;
            if (_noteName == 'g') pitchOffset = 7;
            if (_noteName == 'a') pitchOffset = 9;
            if (_noteName == 'b') pitchOffset = 11;
            if (_modifier == 'f' || _modifier == 'b') pitchModifier = -1;
            if (_modifier == 'F' || _modifier == 'B') pitchModifier = -2;
            if (_modifier == 's' || _modifier == '#') pitchModifier = 1;
            if (_modifier == 'S' || _modifier == 'x') pitchModifier = 2;
            if (_modifier == 'n' || _modifier == 0) pitchModifier = 0;
            if (pitchOffset == -9999 || pitchModifier == -9999) return -9999;

            value = (pitchOffset + pitchModifier) + (12 * _octave);

            return value;
        }

        public string GetPatch()
        {
            return _patch;
        }

        public void SetPatch(string patch)
        {
            _patch = patch;
        }
        public int GetVelocity()
        {
            return _velocity;
        }
        public double GetDuration()
        {
            return _duration;
        }
        public void SetTime(long start, long end)
        {
            _startTime = start;
            _endTime = end;
        }

        // be careful using this. there is a brief rest between notes!
        public bool IsBetween(double ms)
        {
            return ((long)ms >= _startTime && (long)ms <= _endTime);
        }

        public bool IsEndBefore(double ms)
        {
            return ((long)ms <= _endTime);
        }
        public bool IsStartBefore(double ms)
        {
            return ((long)ms <= _startTime);
        }

        public bool IsStartAfter(double ms)
        {
            return ((long)ms >= _startTime);
        }
        public bool IsEndAfter(double ms)
        {
            return ((long)ms >= _endTime);
        }
    }


}
