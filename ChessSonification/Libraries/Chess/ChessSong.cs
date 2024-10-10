/// <summary>
/// ChessSong.cs
/// 
/// Extend the MIDI song to handle PGN stuff...
/// 
/// Date created: 9/24/2024 (mcdermscott@gmail.com).
///  
/// </summary>



namespace ChessSonification
{
    internal class ChessSong : MIDI.Song
    {

        // PGN stuff...
        private string? _pgnFile;
        private int _numMoves;
        private Game? _game;

        public ChessSong()
        {
            _pgnFile = "";
            _numMoves = -1;
            _game = null;
        }

        public void StartPlayback()
        {
            _numTracks = _tracks.Count;
            CalculateMicrosecondsPerQuarterNote(_beatsPerMinute);
            _startTime = DateTime.Now;
        }

        public int GetCurrentMoveNumber()
        {
            _currentTime = DateTime.Now;
            //if (_)
            return _numMoves;
        }
    }

}
