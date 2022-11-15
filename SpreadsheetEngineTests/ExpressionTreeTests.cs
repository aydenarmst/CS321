// <copyright file="ExpressionTreeTests.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace Spreadsheet_Ayden_Armstrong
{
    using System.Configuration;
    using System.Globalization;
    using NUnit.Framework;
    using SpreadsheetEngine;

    /// <summary>
    /// Tests for the expression tree.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTests
    {
        [Test]

        // Constant tests.
        [TestCase("7+11", ExpectedResult = 18.0)]
        [TestCase("100/0", ExpectedResult = double.PositiveInfinity)]
        [TestCase("((((9*10)-(4+5))))", ExpectedResult = 81)]
        [TestCase("10+(24/8)", ExpectedResult =13)]
        [TestCase("1-(24+4*3)", ExpectedResult = -35)]

        // Variable Tests.
        [TestCase("A1+B9", ExpectedResult = 130)]
        [TestCase("C1+90/(2)", ExpectedResult = 105)]
        [TestCase("(C1/B9)", ExpectedResult = 0.5)]
        [TestCase("D1", ExpectedResult = 0.0)]
        [TestCase("C1/E1", ExpectedResult = double.PositiveInfinity)]
        /// <summary>
        /// Tests for the expression tree HW6.
        /// </summary>
        /// <param name="expression">string expression</param>
        /// <returns>The evaluated value</returns>
        public double Test1(string expression)
        {
            ExpressionTree tree = new ExpressionTree(expression);
            tree.SetVariable("A1", 10);
            tree.SetVariable("B9", 120);
            tree.SetVariable("C1", 60);
            return tree.Evaluate();
        }
    }
}
