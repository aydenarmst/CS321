// <copyright file="GenerateCell.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{

    /// <summary>
    /// GenerateCell inherits from the Cell class.
    /// </summary>
    public class GenerateCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCell"/> class.
        /// </summary>
        /// <param name="rows">inputted rows.</param>
        /// <param name="columns">inputted columns.</param>
        public GenerateCell(int rows, int columns)
            : base(rows, columns)
        {
        }
    }
}
