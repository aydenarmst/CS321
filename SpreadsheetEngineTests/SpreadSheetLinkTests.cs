// <copyright file="SpreadSheetLinkTests.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngineTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SpreadsheetEngine;

    /// <summary>
    /// Class for the spreadsheet link tests.
    /// </summary>
    internal class SpreadSheetLinkTests
    {
        private const string V = "22";

        /// <summary>
        /// Setup for the spreadsheet.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Testing the spreadsheet variables.
        /// </summary>
        [Test]
        public void Test1()
        {
            Spreadsheet tester = new (26, 50);

            tester.GetCell(0, 0);
            Assert.That(tester.GetCell(0, 0), Is.EqualTo(tester.Sheet[0, 0]));

            // Testing assertions for the variables that have not been assigned a value.
            Assert.Throws<Exception>(() => tester.GetCellValue("A1"));
            Assert.Throws<Exception>(() => tester.GetCellValue("B1"));
        }
    }
}
