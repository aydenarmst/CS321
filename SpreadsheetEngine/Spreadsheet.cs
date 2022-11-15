// <copyright file="Spreadsheet.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Class for the spreadsheet.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">inputted rows.</param>
        /// <param name="columns">inputted columns.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.Sheet = new Cell[rows, columns];
            this.RowCount = rows;
            this.ColumnCount = columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.Sheet[i, j] = new GenerateCell(i, j);
                    this.Sheet[i, j].PropertyChanged += new PropertyChangedEventHandler(this.CellPropertyChange!);
                }
            }
        }

        /// <summary>
        /// Event which subscribes to a single event that lets the user know when any property for a cell has changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets instance of a sheet.
        /// </summary>
        public Cell[,] Sheet { get; set; }

        /// <summary>
        /// Gets or sets row Count.
        /// </summary>
        private int RowCount { get; set; }

        /// <summary>
        /// Gets or sets column Count.
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
                return (GenerateCell)this.Sheet[row, column];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GEt cell value at the index of the column name.
        /// </summary>
        /// <param name="columnName">for ex A1.</param>
        /// <returns>value at that index.</returns>
        public string GetCellValue(string columnName)
        {
            int columnIndex = CellIndexFromColumn(columnName[0]);

            if (int.TryParse(columnName[1..], out int rowIndex))
            {
                rowIndex--;
                return this.Sheet[rowIndex, columnIndex].CellValueAccessor;
            }
            else
            {
                return "Unable to get column";
            }
        }

        /// <summary>
        /// Cell changed event.
        /// </summary>
        /// <param name="cellEvent">event.</param>
        protected void CellChangedEvent(Cell cellEvent)
        {
            this.PropertyChanged?.Invoke(cellEvent, new PropertyChangedEventArgs("Changed cell."));
        }

        /// <summary>
        /// Creates the property changed and raises an event for the cell.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">event argument.</param>
        /// <exception cref="Exception">expection when it is not of type cell.</exception>
        protected void CellPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            switch (e.PropertyName)
            {
                case "Text":
                    // Validates all variables thatc it starts with '=' and is not null.
                    this.GetVariables(cell);

                    // Converting expression to post fix.
                    string[]? line = NewExpressionTree(cell);

                    // Iterating over each substring in expression, checking if it is a variable or not.
                    this.ExpressionSubstring(cell, line);
                    break;
                default:
                    break;
            }

            // Calling event.
            this.CellChangedEvent(cell);
        }

        /// <summary>
        /// Index of the cell from the column name.
        /// </summary>
        /// <param name="columnName">for ex A1.</param>
        /// <returns>index of that column.</returns>
        private static int CellIndexFromColumn(char columnName)
        {
            // ASCII Value to the index of the column for the name, removes whitespace.
            return (int)char.ToUpper(columnName) - 65;
        }

        /// <summary>
        /// Creates a new expression tree.
        /// </summary>
        /// <param name="cell">input.</param>
        /// <returns>line of postfixed expression, aswell as updating to the shunting yard algorithm.</returns>
        private static string[] NewExpressionTree(Cell cell)
        {
            // Deals with single digit constant nodes.
            return cell.CellTextAccessor.Length switch
            {
                // Case if the value is a single digit, we do not parse the substring.
                1 => SingleDigitConstant(cell),

                // Otherwise we parse.
                _ => RegularExpression(cell),
            };
        }


        /// <summary>
        /// Represents parsing a regular expression i.e everything that is not a single digit.
        /// </summary>
        /// <param name="cell">input cell.</param>
        /// <returns>parsed exp.</returns>
        private static string[] RegularExpression(Cell cell)
        {
            string[] tokenizedLine2 = Regex.Split(cell.CellTextAccessor[1..], @"([*()\^\/]|(?<!E)[\+\-])");
            string[] line2 = ExpressionTree.ConvertToPostFix(tokenizedLine2);
            Array.Reverse(line2);

            // Removing whitespace.
            line2 = line2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            cell.CellExpression = new ExpressionTree(cell.CellTextAccessor[1..]);
            return line2;
        }

        /// <summary>
        /// If it is a single digit, i.e already parsed then we still create a new expresison tree but without the substring.
        /// </summary>
        /// <param name="cell">input cell.</param>
        /// <returns>parsed expression.</returns>
        private static string[] SingleDigitConstant(Cell cell)
        {
            string[] tokenizedLine = Regex.Split(cell.CellTextAccessor, @"([*()\^\/]|(?<!E)[\+\-])");
            string[] line = ExpressionTree.ConvertToPostFix(tokenizedLine);
            Array.Reverse(line);

            // Removing whitespace
            line = line.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            cell.CellExpression = new ExpressionTree(cell.CellTextAccessor);
            return line;
        }

        /// <summary>
        /// Evaluate the expression substring.
        /// </summary>
        /// <param name="cell">cell.</param>
        /// <param name="line">postfixed.</param>
        private void ExpressionSubstring(Cell cell, string[] line)
        {
            foreach (string variable in line)
            {
                if (cell.CellExpression != null)
                {
                    switch (cell.CellExpression.InDictionary(variable))
                    {
                        // If it is a variable.
                        case true:
                            this.VariableEvaluation(cell, variable);
                            break;

                        // Then it is a constant.
                        case false:
                            this.ConstantEvaluation(cell);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// If the cell expression substring is a variable, then we retrieve the cell and set the variable, then evaluate.
        /// </summary>
        /// <param name="cell">input cell.</param>
        /// <param name="variable">Variable name.</param>
        private void VariableEvaluation(Cell cell, string variable)
        {
            // Getting the variable cell.
            var cellTemp = this.RetrieveCell(variable);
            string cellValueString = this.GetCellValue(variable);
            if (double.TryParse(cellValueString, out double value))
            {
                cell.CellExpression.SetVariable(variable, value);
                this.Sheet[cell.GetRowIndex, cell.GetColumnIndex].CellValueAccessor = cell.CellExpression.Evaluate().ToString();
            }

            // Adding Changing the property of the cell.
            if (cellTemp != null)
            {
                cellTemp.PropertyChanged += cell.CellPropertyChange;
            }
        }

        /// <summary>
        /// Gets all the variables in the cell.
        /// </summary>
        /// <param name="cell">input cell.</param>
        private void GetVariables(Cell cell)
        {
            if (cell != null && cell.CellTextAccessor.StartsWith('=') && cell.CellExpression != null)
            {
                // Gets all the variables from the sheet.
                List<string> listVariables = cell.CellExpression.GetVariables();

                foreach (string variable in listVariables)
                {
                    var cellTemp = this.RetrieveCell(variable);
                    if (cellTemp != null)
                    {
                        cellTemp.PropertyChanged -= cell.CellPropertyChange;
                    }
                }
            }
            else
            {
                cell.CellExpression = null;
            }
        }

        private Cell? RetrieveCell(string name)
        {
            if (name.Length == 0)
            {
                return null;
            }

            if (int.TryParse(name.AsSpan(1), out int row))
            {
                int column = CellIndexFromColumn(name[0]);
                return this.GetCell(row - 1, column);
            }

            // index out of bounds or other error.
            return null;
        }

        /// <summary>
        /// Represents evaluating if the value is a constant, so we do not need to retrieve any cell.
        /// </summary>
        /// <param name="cell">input cell.</param>
        private void ConstantEvaluation(Cell cell)
        {
            this.Sheet[cell.GetRowIndex, cell.GetColumnIndex].CellValueAccessor = cell.CellExpression.Evaluate().ToString();
        }
    }
}
