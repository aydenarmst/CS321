// <copyright file="SpreadsheetEngineTests.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace Spreadsheet_Ayden_Armstrong
{
    using System;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using Spreadsheet_Ayden_Armstrong;
    using SpreadsheetEngine;

    /// <summary>
    /// Testing of the engine.
    /// </summary>
    [TestFixture]
    public class SpreadsheetEngineTests
    {
        /// <summary>
        /// Tests Cell creations.
        /// </summary>
        [Test]
        public void TestCellClass()
        {
            GenerateCell c = new (20, 30);
            Assert.Multiple(() =>
            {
                Assert.That(c.CellTextAccessor, Is.EqualTo(string.Empty));
                Assert.That(c.GetRowIndex, Is.EqualTo(20));
                Assert.That(c.GetColumnIndex, Is.EqualTo(30));
            });
            GenerateCell c2 = new (1000, 900);
            Assert.Multiple(() =>
            {
                Assert.That(c2.CellTextAccessor, Is.EqualTo(string.Empty));
                Assert.That(c2.GetRowIndex, Is.EqualTo(1000));
                Assert.That(c2.GetColumnIndex, Is.EqualTo(900));
            });
        }

        /// <summary>
        /// Tests SPreadSheet class.
        /// </summary>
        [Test]
        public void TestSpreadsheetClass()
        {
            Spreadsheet sp = new Spreadsheet(12, 24);
            GenerateCell testCell = sp.GetCell(2, 12) !;
            Assert.Multiple(() =>
            {
                Assert.That(testCell.CellTextAccessor, Is.EqualTo(string.Empty));
                Assert.That(testCell.GetRowIndex, Is.EqualTo(2));
                Assert.That(testCell.GetColumnIndex, Is.EqualTo(12));
            });
        }
    }
}