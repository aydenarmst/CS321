// <copyright file="Form1.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights Reserved.
// </copyright>

namespace HW2
{
    using System;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// class for the form.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// loads the form and formats the strings for data.
        /// </summary>
        /// <param name="sender"> control that the action is for.</param>
        /// <param name="e">event.</param>
        private void LoadForm(object sender, EventArgs e)
        {
            DistinctIntegers distinctIntegers = new DistinctIntegers();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("1. HashSet method: {0} unique numbers", distinctIntegers.HashSetDistinct()).AppendLine();
            stringBuilder.Append("This method loops through the list twice giving us O(n * 2) linear time complexity").AppendLine();
            stringBuilder.AppendFormat("2. O(1) storage method: {0} unique numbers", distinctIntegers.TraverseEachDistinct()).AppendLine();
            stringBuilder.AppendFormat("3. Sorted method: {0} unique numbers", distinctIntegers.SortDistinct()).AppendLine();

            this.TextBox.Text = stringBuilder.ToString();
        }
    }
}