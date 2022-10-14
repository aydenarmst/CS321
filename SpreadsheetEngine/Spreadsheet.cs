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
                    this.sheet[i, j].PropertyChanged += new PropertyChangedEventHandler(PropertyChangeHandler!);
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
        private int RowCount { get; set; }

        /// <summary>
        /// Gets column Count.
        /// </summary>
        private int ColumnCount { get; set; }

        /// <summary>
        /// Gets the cell from given index's.
        /// </summary>
        /// <param name="row">row input.</param>
        /// <param name="column">column input.</param>
        /// <returns>cell according to those inputs.</returns>
        public GenerateCell? GetCell(int row, int column)
        {
            if (row >= 0 && row < this.RowCount && column >= 0 && column < this.ColumnCount)
            {
                return (GenerateCell)this.sheet[row, column];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method for the demo to illistrate how changing cells in the worksheet object trigger a proper UI update.
        /// </summary>
        public void Demo1()
        {
            var rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                this.sheet[rand.Next(49), rand.Next(25)].CellTextAccessor = "Hello World!";
            }

            for (int i = 0; i < 50; i++)
            {
                this.sheet[i, 1].CellTextAccessor = "This is cell B" + (i + 1) ;
                this.sheet[i, 0].CellTextAccessor = "=B" + i;
            }
        }

        /// <summary>
        /// Method to notify.
        /// </summary>
        /// <param name="sender">object sender.</param>
        /// <param name="name">string inputted.</param>
        private void NotifyPropertyChanged(object sender, string name)
        {
            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Method for Evaluating if which variable the change falls under and computed on conditonals.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">event.</param>
        private void PropertyChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            GenerateCell cell = (GenerateCell)sender;

            if (e.PropertyName == "Text")
            {
                this.EvaluateCell(cell!);
                this.NotifyPropertyChanged(cell!, "Value");
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
        private void EvaluateCell(GenerateCell target)
        {
            string cellText = target.CellTextAccessor;

            if (cellText.Length == 0)
            {
                target.SetCellValue(string.Empty);
            }

            if (cellText.StartsWith('='))
            {
                this.ComputeCell(target);
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
        private void ComputeCell(GenerateCell target)
        {
            string exp = target.CellTextAccessor[1..];
            char column = exp[0];
            int columnNumber = column - 'A';
            var row = int.Parse(exp[1..]);
            GenerateCell get = this.GetCell(row, columnNumber) !;
            if (get != null)
            {
                target.SetCellValue(get.CellValueAccessor);
            }
        }
    }
}
