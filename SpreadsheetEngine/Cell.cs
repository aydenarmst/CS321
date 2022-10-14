// <copyright file="Cell.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// main cell public abstract class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Protected Cell Text.
        /// </summary>
        protected string cellText;

        /// <summary>
        /// Protected Cell Value. Its says to make it private but im going off of the rubrics guidelines.
        /// </summary>
        protected string cellValue;

        /// <summary>
        /// Gets rowIndex.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// Gets column index.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">given row.</param>
        /// <param name="column">given column.</param>
        protected Cell(int row, int column)
        {
            this.rowIndex = row;
            this.columnIndex = column;
            this.cellText = string.Empty;
            this.cellValue = string.Empty;
        }

        /// <summary>
        /// Property Changed to notify property change.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets cellText Constructors.
        /// </summary>
        public string CellTextAccessor
        {
            get
            {
                return this.cellText;
            }

            set
            {
                if (value != this.CellTextAccessor)
                {
                    this.cellText = value;
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets with a conditonal for the cell value.
        /// </summary>
        public string CellValueAccessor
        {
            get
            {
                if (this.cellText.Length > 0 && this.cellText[0].Equals('='))
                {
                    return this.cellValue;
                }
                else
                {
                    return this.cellText;
                }
            }
        }

        /// <summary>
        /// Gets getter for row index.
        /// </summary>
        public int GetRowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets getter for column index.
        /// </summary>
        public int GetColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Sets the cell value, in which only is accessable through the spreadsheet assembly.
        /// </summary>
        /// <param name="value">cell value.</param>
        internal void SetCellValue(string value)
        {
            this.cellValue = value;
            this.NotifyPropertyChanged("Value");
        }

        /// <summary>
        /// Method to notify.
        /// </summary>
        /// <param name="name">string inputted.</param>
        protected void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}