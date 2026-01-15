/// <summary>
/// Game.cs
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
	internal class Game
	{
		public string gameDescription;
		public List<Move> gameMoves;

		public Game(string description = "")
		{
			gameMoves = new List<Move>();
			gameDescription = description;
		}

		// Returns the index of the move added
		public int AddMove(string move, int totalMaterialOnBoard)
		{
			gameMoves.Add(new Move(move, totalMaterialOnBoard));
			return gameMoves.Count - 1;
		}

		public string GetMoveBoard(int move)
		{
			string ret = "";
			if (move < gameMoves.Count)
			{
				ret = gameMoves[move].ToString();
			}
			return ret;
		}

		public void UpdateGame()  // This could be put in Move.cs....
		{
			for (int i = 0; i < gameMoves.Count; i++)
			{
				if (i == 0) // skip the first move, nothing to see there
				{
					gameMoves[i].Reset();
					gameMoves[i].SetPiece('X');
				}
				else
				{
					// first, go ahead and store the end of the move from the PGN. Note the + at the end means check

					//In Portable Game Notation (PGN), kingside castling is indicated by the sequence O-O, and queenside 
					//castling is indicated by the sequence O-O-O. These are capital Os, not zeroes, which differs from 
					//the FIDE standard for notation. 
					string pgn = gameMoves[i].GetPgn();
					if (pgn == "O-O-O" || pgn == "O-O-O+")
					{
						gameMoves[i].SetMoveType(SpecialMoveType.QUEENSIDECASTLE);
						gameMoves[i].SetCheck(pgn.EndsWith('+'));
						if (gameMoves[i].IsWhite())                  // white queen side castle
						{
							gameMoves[i].SetPiece('K');
							gameMoves[i].SetLocation("e1", true);
							gameMoves[i].SetLocation("c1", false);
						}
						else
						{                                               // black queen side castle
							gameMoves[i].SetPiece('k');
							gameMoves[i].SetLocation("e8", true);
							gameMoves[i].SetLocation("c8", false);
						}
					}
					else if (pgn == "O-O" || pgn == "O-O+")
					{
						gameMoves[i].SetMoveType(SpecialMoveType.KINGSIDECASTLE);
						gameMoves[i].SetCheck(pgn.EndsWith('+'));
						if (gameMoves[i].IsWhite())                  // white king side castle
						{
							gameMoves[i].SetPiece('K');
							gameMoves[i].SetLocation("e1", true);
							gameMoves[i].SetLocation("g1", false);
						}
						else
						{                                               // black king side castle
							gameMoves[i].SetPiece('k');
							gameMoves[i].SetLocation("e8", true);
							gameMoves[i].SetLocation("g8", false);
						}
					}

					// Handle the non-special moves
					else
					{

						//Pawn promotions are notated by appending = to the destination square, followed by the piece the 
						//pawn is promoted to. For example: e8=Q . If the move is a checking move, + is also appended; if 
						//the move is a checkmating move, # is appended instead
						//
						//If you're moving a pawn to a square which promotes it to another piece, put an '=' after the 
						//destination square followed by the designation letter of the piece you wish to promote to. 
						//For example: a8=Q, axb8=R, etc. 
						//
						if (pgn.Contains('='))
						{
							gameMoves[i].SetMoveType(SpecialMoveType.PROMOTION);
							gameMoves[i].SetCheck(pgn.EndsWith('+') || pgn.EndsWith('#'));
							int eqs = pgn.IndexOf("=");
							gameMoves[i].SetLocation(pgn.Substring(eqs - 2, 2), false);

						}
						else if (pgn.EndsWith('+'))
						{
							gameMoves[i].SetCheck(true);
							gameMoves[i].SetLocation(pgn.Substring(pgn.Length - 3, 2), false);
						}
						else
						{
							gameMoves[i].SetCheck(false);
							gameMoves[i].SetLocation(pgn.Substring(pgn.Length - 2, 2), false);
						}

						//////////Console.WriteLine(gameMoves[i].pgnMove + "->" + gameMoves[i].indexToMove(gameMoves[i].endLocationLetterIndex, gameMoves[i].endLocationNumberIndex));
						gameMoves[i].UpdatePiece(); // just for sanity!

						// next, figure out the locations of the differences
						// find and store all of the differences (for en passant)
						int diffCount = gameMoves[i].UpdateStartLocation();

						// Now check for en passant...

						// I think this is where to handle en passant (move 11 of JackOfAllHobbies)
						// first, check if the Piece was a P or p, then look for the column followed by x (like ex or cx)
						// ahh find the two differences and check the location in the previous move
						// aaaaahhh just check the column from the start location!
						if (gameMoves[i].GetPiece() == 'p' || gameMoves[i].GetPiece() == 'P')
						{
							if (diffCount > 2) // ha! found it!!! Now just figure out the correct start location. Look for that piece in the previous board at the three locations
							{
								gameMoves[i].SetMoveType(SpecialMoveType.ENPASSANT);
								for (int j = 0; j < diffCount; j++)
								{
									if (gameMoves[i - 1].GetPiece(gameMoves[i].differenceLocationLetters[j], gameMoves[i].differenceLocationNumbers[j]) == gameMoves[i].GetPiece())
									{
										gameMoves[i].SetLocation(gameMoves[i].differenceLocationLetters[j], gameMoves[i].differenceLocationNumbers[j], true);
									}
								}

							}
						}
					}
				}
			}
		}

	}


}
