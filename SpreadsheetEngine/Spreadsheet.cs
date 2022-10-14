// <copyright file="Spreadsheet.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Class for the spreadsheet.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Instance of a sheet.
        /// </summary>
        private readonly Cell[,] sheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">inputted rows.</param>
        /// <param name="columns">inputted columns.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.sheet = new Cell[rows, columns];
            this.RowCount = rows;
            this.ColumnCount = columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.sheet[i, j] = new GenerateCell(i, j);
                }
            }
        }

        /// <summary>
        /// Gets or sets row Count.
        /// </summary>
        private int RowCount { get; set; }

        /// <summary>
        /// Gets or sets column Count.
        /// </summary>
        private int ColumnCount { get; set; }

        /// <summary>
        /// Gets the cell from given index's. Assuming the index's inputted arent starting from 0.
        /// </summary>
        /// <param name="row">row input.</param>
        /// <param name="column">column input.</param>
        /// <returns>cell according to those inputs.</returns>
        public Cell? GetCell(int row, int column)
        {
            if (this.sheet[row - 1, column - 1] == null)
            {
                return null;
            }
            else
            {
                return this.sheet[row - 1, column - 1];
            }
        }

        /// <summary>
        /// Event which subscribes to a single event that lets the user know when any property for a cell has changed.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;
    }
}
