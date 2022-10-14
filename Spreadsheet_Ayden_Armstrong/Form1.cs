// <copyright file="Form1.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace Spreadsheet_Ayden_Armstrong
{
    using System.ComponentModel;

    /// <summary>
    /// Main entry for UI of the spreadsheet.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
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
        }
    }
}