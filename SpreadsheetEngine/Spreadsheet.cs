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
        /// Event which subscribes to a single event that lets the user know when any property for a cell has changed.
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged;

        /// <summary>
        /// Gets row Count.
        /// </summary>
        private int RowCount { get;  }

        /// <summary>
        /// Gets column Count.
        /// </summary>
        private int ColumnCount { get; }

        /// <summary>
        /// Gets the cell from given index's.
        /// </summary>
        /// <param name="row">row input.</param>
        /// <param name="column">column input.</param>
        /// <returns>cell according to those inputs.</returns>
        public Cell? GetCell(int row, int column)
        {
            if (row >= 0 && row < this.RowCount && column >= 0 && row < this.ColumnCount)
            {
                return this.sheet[row, column];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to notify.
        /// </summary>
        /// <param name="name">string inputted.</param>
        /// <param name="sender">object sender.</param>
        private void NotifyPropertyChanged(object sender, string name)
        {
            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }

        private void PropertyChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            GenerateCell cell = (GenerateCell)sender;

            if (e.PropertyName == "Text")
            {
                this.Compute(cell);
                this.NotifyPropertyChanged(cell, "Value");
            }

            if (e.PropertyName == "Value")
            {
                this.NotifyPropertyChanged(cell, "Value");
            }
        }


        /// <summary>
        /// Evaluates to see if the string is empty or if it starts with =.
        /// </summary>
        /// <param name="target">cell target.</param>
        private void Evaluate(Cell target)
        {
            string cellText = target.CellTextAccessor;
            string cellValue = target.CellValueAccessor;

            if (cellText == string.Empty)
            {
                target.SetCellValue(string.Empty);
            }

            if (cellText.StartsWith('='))
            {
                this.Compute(target);
            }
            else
            {
                target.SetCellValue(target.CellValueAccessor);
            }
        }

        /// <summary>
        /// Compute everything after the = in the cell if given constraints.
        /// </summary>
        /// <param name="target">cell target.</param>
        private void Compute(Cell target)
        {
            string exp = target.CellTextAccessor[1..];
            char column = exp[0];
            int columnNumber = column - 'A';

            try
            {
                int row = Convert.ToInt32(exp[1..]) - 1;
                Cell get = this.GetCell(row, columnNumber) !;
                target.SetCellValue(get.CellValueAccessor);
            }
            catch
            {
                throw;
            }
        }
    }
}
