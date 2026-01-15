/// <summary>
/// GameList.cs
/// 
/// Clump a whole bunch of games together for storage and manipulation. These classes are entirely independent of imported libraries.
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
	internal class GameList
	{
		private List<Game> games;

		public GameList()
		{
			games = new List<Game>();
		}

		public void Clean()
		{
			foreach (Game game in games)
			{
				game.gameMoves.Clear();
			}
			games.Clear();
		}

		public int Add(string description)
		{
			games.Add(new Game(description));
			return games.Count - 1;
		}

		public int AddMove(int gameNumber, string move, int totalMaterialOnBoard)
		{
			if (gameNumber < games.Count)
			{
				return games[gameNumber].AddMove(move, totalMaterialOnBoard);
			}
			return -1;
		}

		public void SetMovePGN(int gameNumber, int moveNumber, string pgnMove)
		{
			if (gameNumber < games.Count)
			{
				if (moveNumber < games[gameNumber].gameMoves.Count)
				{
					char color = 'X';
					Move? prev = null;
					if ((moveNumber - 1) % 2 == 0) { color = 'W'; }
					else { color = 'b'; }
					if (moveNumber > 0) { prev = games[gameNumber].gameMoves[moveNumber - 1]; }
					games[gameNumber].gameMoves[moveNumber].SetMove(pgnMove, moveNumber, color, prev);

				}
			}
		}
		public Move? GetMove(int gameNumber, int moveNumber)
		{
			if (gameNumber < games.Count && moveNumber < games[gameNumber].gameMoves.Count)
			{
				return games[gameNumber].gameMoves[moveNumber];
			}
			return null;
		}

		public string GetMoveBoard(int gameNumber, int moveNumber)
		{
			string ret = "";
			if (gameNumber < games.Count)
			{
				if (moveNumber < games[gameNumber].gameMoves.Count)
				{
					ret = games[gameNumber].GetMoveBoard(moveNumber);
				}
			}
			return ret;
		}

		public char GetMovePiece(int gameNumber, int moveNumber, int letter, int number)
		{
			char ret = '-';
			if (gameNumber < games.Count)
			{
				if (moveNumber < games[gameNumber].gameMoves.Count)
				{
					ret = games[gameNumber].gameMoves[moveNumber].GetPiece(letter, number);
                }
			}
			return ret;
		}

		public string GetMovePGN(int gameNumber, int moveNumber)
		{
			string ret = "";
			if (gameNumber < games.Count)
			{
				if (moveNumber < games[gameNumber].gameMoves.Count)
				{
					ret = games[gameNumber].gameMoves[moveNumber].GetPgn();
				}
			}
			return ret;
		}

		public int GetMoveCount(int gameNumber)
		{
			int ret = 0;
			if (gameNumber != -1 && gameNumber < games.Count)
			{
				ret = games[gameNumber].gameMoves.Count;
			}
			return ret;
		}

		// Only call this when everything has been loaded into the games
		public void UpdateGames()
		{
			foreach (Game game in games)
			{
				game.UpdateGame();
			}

		}

		public int GetMoveDistance(int gameNumber, int moveNumber)
		{
			int ret = 0;
			if (gameNumber < games.Count)
			{
				if (moveNumber < games[gameNumber].gameMoves.Count)
				{
					ret = games[gameNumber].gameMoves[moveNumber].GetVerticalDistance();
				}
			}
			return ret;
		}

		// I should probably take this out... Too much access...
		public Game? GetGame(int gameNumber)
		{
			Game? ret = null;
			if (gameNumber != -1 && gameNumber < games.Count)
			{
				ret = games[gameNumber];
			}
			return ret;
		}

	}
}
