// <copyright file="Form1.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Main entry for UI of the spreadsheet.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.engine = new SpreadsheetEngine.Spreadsheet(50, 26);
        }

        private void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear();

            for (char column = 'A'; column <= 'Z'; column++)
            {
                this.dataGridView1.Columns.Add("Column", column.ToString());
            }

            this.dataGridView1.Rows.Add(50);

            for (int row = 1; row <= 50; row++)
            {
                this.dataGridView1.Rows[row - 1].HeaderCell.Value = row.ToString();
            }

            this.dataGridView1.RowHeadersWidth = 70;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            this.SubscribePropertyChange();
            this.InitializeDataGrid();
        }

        private void SubscribePropertyChange()
        {
            this.engine.CellPropertyChanged += this.Spreadsheet_PropertyChange!;
        }

        private void Spreadsheet_PropertyChange(object sender, PropertyChangedEventArgs e)
        {
            Cell? target = sender as Cell;
            this.dataGridView1.Rows[target!.GetRowIndex].Cells[target.GetColumnIndex].Value = target.CellValueAccessor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.engine.Demo1();
        }
    }
}