﻿using ilf.pgn;
using ilf.pgn.Data;
using PgnToFenCore.Conversion;
using System;

namespace PgnToFenCore
{
    public class PgnToFenConverter
    {
        public Database Convert(IConversionStrategy strategy, string pgnFilename)  // scott 6/2024
        {
            var pgnFileReader = new PgnReader();
            Database gameDb = pgnFileReader.ReadFromFile(pgnFilename);

            Console.WriteLine(gameDb.Games.Count + " games found");

            foreach (var game in gameDb.Games)
            {
                strategy.ConvertAllFens(game);
            }
            return gameDb; // scott 6/2024
        }
    }
}
