/// <summary>
/// Move.cs
/// 
/// Encapsulate the various parts of a single chess move. These classes are entirely independent of imported libraries.
/// Utilizes: https://github.com/craylton/PgnToFen
/// Utilizes: https://github.com/iigorr/pgn.net/
/// Reference: http://www.saremba.de/chessgml/standards/pgn/pgn-complete.htm
/// 
/// 
/// Date created: 6/28/2024 (mcdermscott@gmail.com)
///  -Reinterpret SimpleMidiFileWrite 
/// Date modified: 9/14/2024 (mcdermscott@gmail.com)
///  -Split into separate files...
///  
/// </summary>

namespace ChessSonification
{
	public enum SpecialMoveType
	{
		NORMAL,
		KINGSIDECASTLE,
		QUEENSIDECASTLE,
		PROMOTION,
		ENPASSANT,
	}
	
	internal class Move
	{
		// PGN and FEN stuff...
		public string _pgnMove; // Set in SetMovePGN
		private int _moveNumber; // Set in SetMovePGN
		private string _fen; // this is the long string version of the move. split these into eight lines of chars per
		private char[,] _board; // c# is stupid in that it forces row, column indexing. Store it flipped and adjust. Stupid!!!
								//public fixed char board2[8][8];
		private int _materialCounts; // this is a nice little thing we get from the fen

		// Derived stuff.... set these by calling Games.UpdateGames
		private char _color; // W or b
		private char _piece;
		// these are the indexes of the piece on the board grid, starting from 0
		private int _startLocationLetterIndex;  // if x is 0=>a, 1=>b, ...
		private int _startLocationNumberIndex;  // for y, 0=>1, 1=>2, ...
		private int _endLocationLetterIndex;    // if x is 0=>a, 1=>b, ...
		private int _endLocationNumberIndex;    // for y, 0=>1, 1=>2, ...
		private SpecialMoveType _moveType;
		private bool _isCheck;
		private Move? _previousMove; // Set in SetMovePGN

		// The are public and updated by GetDifferenceCount
		public int[] differenceLocationLetters = { -1, -1, -1, -1, -1, -1, -1, -1 };
		public int[] differenceLocationNumbers = { -1, -1, -1, -1, -1, -1, -1, -1 };

		/////////////////////////////////////////
		/// <summary>
		/// Move Constructor!
		/// </summary>
		/// <param name="fen"></param>
		/// <param name="materialCounts"></param>
		public Move(string fen = "", int materialCounts = 0)
		{
			Reset();
			_pgnMove = "initial";
			_moveNumber = 0;
			_color = '-';
			_previousMove = null;
			_board = new char[8, 8];
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					_board[i, j] = 'X';
				}
			}
			_fen = fen;
			_materialCounts = materialCounts;
			_isCheck = false;
			_moveType = SpecialMoveType.NORMAL;
			this.ParseFen();
		}

		public void SetMove(string pgn, int moveNumber, char color, Move? previousMove)
		{
			_pgnMove = pgn;
			_moveNumber = moveNumber;
			SetColor(color);
			_previousMove = previousMove;
		}

		/////////////////////////////////////////
		/// <summary>
		/// Called by the constructor to read the fen and store it as something useful
		/// </summary>
		public void ParseFen()
		{
			// parse the string!
			if (_fen != "")
			{
				char[] delimiterChars = { '/', ' ' };
				string[] lines = _fen.Split(delimiterChars);
				int lineIndex;
				int lineLength;
				for (int i = 0; i < 8; i++)
				{
					lineIndex = 0;
					lineLength = lines[i].Length;
					for (int j = 0; j < lineLength; j++)
					{
						char c = lines[i][j];
						if (char.IsDigit(c))
						{   // if it is a number, repeat an empty space that many times (4P3)
							int rep = c - '0';
							for (int k = 0; k < rep; k++)
							{
								_board[7 - i, lineIndex++] = '_';
							}
						}
						else
						{// c# is stupid in that it forces row, column indexing. Store it flipped and adjust. Stupid!!!
							_board[7 - i, lineIndex++] = c;
						}
					}
				}
			}
		}

		public void Reset()
		{
			_piece = '-';
			_startLocationLetterIndex = 0;
			_startLocationNumberIndex = 0;
			_endLocationLetterIndex = 0;
			_endLocationNumberIndex = 0;
		}

		/////////////////////////////////////////
		/// <summary>
		/// Print the current board to a string
		/// </summary>
		public override string ToString()
		{
			string ret = "   a b c d e f g h\n";
			for (int i = 0; i < 8; i++)
			{
				ret += ((char)('8' - i)) + ": ";
				for (int j = 0; j < 8; j++)
				{
					ret += _board[7 - i, j];
					ret += ' ';
				}
				ret += '\n';
			}
			ret += '\n';
			ret += _moveNumber + ". " + _pgnMove;
			ret += "(" + _piece + ", " + StartLocation() + "=>" + EndLocation() + ")";
			return ret;
		}

		/////////////////////////////////////////
		/// <summary>
		/// Convert from numbers to the board grid
		/// if x is 0=>a, 1=>b, ...
		/// for y, 0=>1, 1=>2, ... 
		/// </summary>
		/// <param name="letter"></param>
		/// <param name="number"></param>
		public string IndexToMove(int letter, int number)
		{
			string ret = "";
			ret += (char)('a' + letter);
			ret += (char)('0' + (number + 1));
			return ret;
		}
		public string StartLocation()
		{
			return IndexToMove(_startLocationLetterIndex, _startLocationNumberIndex);
		}
		public string EndLocation()
		{
			return IndexToMove(_endLocationLetterIndex, _endLocationNumberIndex);
		}

		/// <summary>
		/// Returns the row number from 1 at the bottom to 8 at the top
		/// </summary>
		public int StartRow()
		{
			return _startLocationLetterIndex + 1;
        }
		public int EndRow()
        {
            return _endLocationLetterIndex + 1;
        }


        /////////////////////////////////////////
        /// <summary>
        /// Given a pgn style string, set the start location
        /// if x is 0=>a, 1=>b, ...
        /// for y, 0=>1, 1=>2, ... 
        /// <param name="location"></param>
        /// <param name="isStartLocation"></param>
        public void SetLocation(string location, bool isStartLocation)
		{
			int x = 0, y = 0;
			x = char.ToLower(location[0]) - 'a';
			y = (location[1] - '0') - 1;
			if (x >= 0 && x <= 8 && y >= 0 && y <= 8)
			{
				if (isStartLocation)
				{
					_startLocationLetterIndex = x;
					_startLocationNumberIndex = y;
				}
				else
				{
					_endLocationLetterIndex = x;
					_endLocationNumberIndex = y;
					UpdatePiece();
				}
			}
		}
		public void SetLocation(int letterIndex, int numberIndex, bool isStartLocation)
		{
			if (letterIndex >= 0 && letterIndex <= 8 && numberIndex >= 0 && numberIndex <= 8)
			{
				if (isStartLocation)
				{
					_startLocationLetterIndex = letterIndex; // (char)('a' + letterIndex);
					_startLocationLetterIndex = numberIndex; // (char)('0' + (numberIndex + 1));
				}
				else
				{
					_endLocationLetterIndex = letterIndex; // (char)('a' + letterIndex);
					_endLocationLetterIndex = numberIndex; // (char)('0' + (numberIndex + 1));
					UpdatePiece();
				}
			}
		}

		// Call UpdatePiece whenever endLocation changes
		public void UpdatePiece()
		{
			if (_endLocationLetterIndex < 8 && _endLocationNumberIndex < 8)
				_piece = GetPiece(_endLocationLetterIndex, _endLocationNumberIndex);
		}

		public int UpdateStartLocation()
		{
			int diffCount = 0;
			for (int i = 0; i < 8; i++)
			{ // reset
				differenceLocationLetters[i] = -1;
				differenceLocationNumbers[i] = -1;
			}

			for (int l = 0; l < 8; l++)
			{
				for (int n = 0; n < 8; n++)
				{
					if (_previousMove is not null &&  _board[n, l] != _previousMove._board[n, l]) // yes, I know it's backwards! Stupid C#
					{
						differenceLocationLetters[diffCount] = l;
						differenceLocationNumbers[diffCount] = n;
						diffCount++;
					}
				}
			}

			// finally, check for which is not the end of move and store the other as the start location
			if (differenceLocationLetters[0] == _endLocationLetterIndex && differenceLocationNumbers[0] == _endLocationNumberIndex)
			{
				_startLocationLetterIndex = differenceLocationLetters[1];
				_startLocationNumberIndex = differenceLocationNumbers[1];
			}
			else
			{
				_startLocationLetterIndex = differenceLocationLetters[0];
				_startLocationNumberIndex = differenceLocationNumbers[0];
			}
			return diffCount;
		}



		/////////////////////////////////////////
		/// Basic accessors!

		public char GetPiece() { return _piece; }
		public char GetPiece(int letter, int number)
		{ // C# is stupid in that it forces row, column indexing. Store it flipped and adjust. Stupid!!!
			char c = 'X';
			c = _board[number, letter];
			return c;
		}
		public void SetPiece(char piece) { _piece = piece; }
		public string GetPgn() { return _pgnMove; }
		public void SetCheck(bool check) { _isCheck = check; }
		public bool IsCheck() { return _isCheck; }
		public void SetMoveType(SpecialMoveType type) { _moveType = type; }
		public SpecialMoveType GetMoveType() { return _moveType; }
		public char GetColor() { return _color; }
		public bool IsWhite() { return (_color == 'W'); }// or b // Check if it is White's turn or black
		public void SetColor(char color) { if (color == 'b' || color == 'W') _color = color; else _color = 'X'; }

		/////////////////////////////////////////
		// Calculations!

		public int GetVerticalDistance()
		{
			return Math.Abs(_endLocationNumberIndex - _startLocationNumberIndex);
		}
	}
}
