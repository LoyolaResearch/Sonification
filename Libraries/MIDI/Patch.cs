/// <summary>
/// Patch.cs
/// 
/// Encapsulate a patch, or voice (maybe rename it to voice?).
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

    internal class Patch
    {
        private Dictionary<string, int> _patchMap;
        public Patch()
        {
            _patchMap = new Dictionary<string, int>();

            _patchMap.Add("nylon", 25);
            _patchMap.Add("steel", 26);
            _patchMap.Add("jazz", 27);
            _patchMap.Add("clean", 28);
            _patchMap.Add("muted", 29);
            _patchMap.Add("distortion", 31);
            _patchMap.Add("bass", 33);
            _patchMap.Add("violin", 41);
            _patchMap.Add("viola", 42);
            _patchMap.Add("cello", 43);
            _patchMap.Add("sitar", 105);
            _patchMap.Add("banjo", 106);
            _patchMap.Add("fiddle", 111);
        }

        public int GetValue(string patchName)
        {
            int value = 25;
            try { value = _patchMap[patchName.ToLower()]; }
            catch (KeyNotFoundException) { value = 25; }
            return value;
        }

        public bool IsValid(string patchName)
        {
            int value = -9999;
            try { value = _patchMap[patchName.ToLower()]; }
            catch (KeyNotFoundException) { return false; }
            return true;
        }
    }
}
