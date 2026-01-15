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
        private int _trackToUse;
        private int _noteCountForTrack;
        private double _notesPerMove;

        public ChessSong()
        {
            _pgnFile = "";
            _numMoves = -1;
            _game = null;
            _trackToUse = 0;
            _noteCountForTrack = 0;
            _notesPerMove = 1.0;
        }

        public new void StartPlayback()
        {
            if (_tracks.Count == 0 || _trackToUse > _tracks.Count)
                return;
            _noteCountForTrack = _tracks[_trackToUse].GetNoteCount();
            base.StartPlayback();
            //CalculateMicrosecondsPerQuarterNote(_beatsPerMinute);
        }

        public int GetCurrentMoveNumber()
        {
            if (_trackToUse > _tracks.Count)
                return 0;
            TimeSpan timeDiff = DateTime.Now - (DateTime)_startTime;
            double ms = timeDiff.TotalMilliseconds;
            return ((int)(_tracks[_trackToUse].GetNoteNumberAtMS(ms) / _notesPerMove) * _tracks.Count);
        }
        /// <summary>
        /// Set the number of notes per chess move for the specified track to use
        /// </summary>
        public void SetNotesPerMove(double notesPerMove)
        {
            _notesPerMove = notesPerMove;
        }
    }

}
