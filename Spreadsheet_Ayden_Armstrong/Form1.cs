// <copyright file="Form1.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

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

            // New event.
            this.engine.PropertyChanged += this.SingleCellPropertyChanged;
        }

        private void SingleCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? newCell = sender as Cell;
            if (newCell != null)
            {
                if (e.PropertyName == "Changed cell.")
                {
                    this.dataGridView1.Rows[newCell.GetRowIndex].Cells[newCell.GetColumnIndex].Value = newCell.CellValueAccessor;
                }
            }
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
            this.InitializeDataGrid();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string alert = string.Format("You are editing {0}, {1}", e.ColumnIndex, e.RowIndex);
            this.Text = alert;

            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.engine.Sheet[e.RowIndex, e.ColumnIndex].CellTextAccessor;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string alert = string.Format("Finished editing {0}, {1}", e.ColumnIndex, e.RowIndex);
            this.Text = alert;

            string textTemp;

            if (this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                textTemp = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                this.engine.Sheet[e.RowIndex, e.ColumnIndex].CellTextAccessor = textTemp;

                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.engine.Sheet[e.RowIndex, e.ColumnIndex].CellValueAccessor;
            }
            else
            {
                this.engine.Sheet[e.RowIndex, e.ColumnIndex].CellTextAccessor = string.Empty;
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
            }
        }
    }
}