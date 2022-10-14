// <copyright file="Cell.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace Spreadsheet_Ayden_Armstrong
{
    using System.ComponentModel;

    /// <summary>
    /// main cell public abstract class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">given row.</param>
        /// <param name="column">given column.</param>
        protected Cell(int row, int column)
        {
            this.RowIndex = row;
            this.ColumnIndex = column;
            this.cellText = string.Empty;
            this.cellValue = string.Empty;
        }


        /// <summary>
        /// Property Changed to notify property change.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets rowIndex.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Gets column index.
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Protected Cell Text.
        /// </summary>
        protected string cellText;

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
                if (this.cellText != this.CellTextAccessor)
                {
                    this.cellText = value;
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Protected Cell Value.
        /// </summary>
        protected string cellValue;


        /// <summary>
        /// Gets with a conditonal for the cell value.
        /// </summary>
        public string CellValueAccessor
        {
            get
            {
                if (this.cellValue[0].Equals('='))
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