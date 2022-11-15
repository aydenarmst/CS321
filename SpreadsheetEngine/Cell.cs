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
        /// Gets rowIndex.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// Gets column index.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// Protected Cell Text.
        /// </summary>
        private string cellText;

        /// <summary>
        /// Protected Cell Value. Its says to make it private but im going off of the rubrics guidelines.
        /// </summary>
        private string cellValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">given row.</param>
        /// <param name="column">given column.</param>
        public Cell(int row, int column)
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
        /// Gets or sets expression for a cell.
        /// </summary>
        public ExpressionTree? CellExpression { get; set; }

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
                else
                {
                    this.cellText = string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets with a conditonal for the cell value.
        /// </summary>
        public string CellValueAccessor
        {
            get
            {
                // Using exception to verify if the cell has cellText which corresponds to a value, if not than catch exception due to index out of bounds when calling cellText[0]
                try
                {
                    if (this.cellText != null && this.cellText[0].Equals('='))
                    {
                        return this.cellValue;
                    }
                    else
                    {
                        return this.cellText;
                    }
                }
                catch
                {
                    throw new Exception("This cell does not have a value assigned.");
                }
            }

            // sets the cell value
            set
            {
                this.cellValue = value;
                this.NotifyPropertyChanged("Value");
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
        /// If the cells text is changed, invoke the event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">event arg.</param>
        internal void CellPropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
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