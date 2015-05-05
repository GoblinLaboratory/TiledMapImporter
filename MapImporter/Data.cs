﻿using System;

namespace MapImporter
{
    /// <summary>
    /// Stores the data for each layer. Each layer contains
    /// a list of gids and those are stored as a matrix.
    /// </summary>
    /// <see cref="TileLayer"/>
    public class Data
    {
        /// <summary>
        /// Holds the global ids for each tile in the layer.
        /// </summary>
        public int[,] TileData { set; get; }

        /// <summary>
        /// Instantiates the new multidimensional array.
        /// </summary>
        /// <param name="i">The number of tiles in the i direction.</param>
        /// <param name="j">The number of tiles in the j direction.</param>
        public Data(int i, int j)
        {
            TileData = new int[i, j];
        }

        /// <summary>
        /// Returns the gid of the tile at the specified indices.
        /// </summary>
        /// <param name="i">The index in the i direction.</param>
        /// <param name="j">The index in the j direction.</param>
        /// <returns>The global id at the given indices.</returns>
        public int GetTileData(int i, int j)
        {
            try
            {
                return TileData[i, j];
            }
            catch
            {
                throw new IndexOutOfRangeException("\nIndex out of Range in TileData.");
            }
        }

        /// <summary>
        /// Changes the global id of the given indices to the new value.
        /// Here if you need it, but don't try rewriting your whole map using it.
        /// </summary>
        /// <param name="newVal">The new global id to be stored at this position.</param>
        /// <param name="i">The index in the i direction.</param>
        /// <param name="j">The index in the j direction.</param>
        public void SetTileDataAt(int newVal, int i, int j)
        {
            try
            {
                TileData[i, j] = newVal;
            }
            catch
            {
                throw new IndexOutOfRangeException("\nIndex out of Range in TileData.");
            }
        }

        /// <summary>
        /// Prints the entire matrix to the console. Used for debugging.
        /// </summary>
        public void PrintData()
        {
            for (int j = 0; j <= TileData.GetUpperBound(1); j++)
            {
                for (int i = 0; i <= TileData.GetUpperBound(0); i++)
                {
                    Console.Write(TileData[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
