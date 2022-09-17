// <copyright file="DistinctIntegersTests.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights Reserved.
// </copyright>

namespace HW2
{
    using NUnit.Framework;

    /// <summary>
    /// test program for the DistinctIntegers methods.
    /// all naming of the test methods was used by microsoft docs.
    /// (best practices) which uses the name of the method, the scenario in which its being tested and the expected behhavior of output.
    /// Link of documentation https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices?source=recommendations.
    /// </summary>
    public class DistinctIntegersTests
    {
        /// <summary>
        /// Tests if the method can create the list.
        /// </summary>
        [Test]
        public void DistinctIntegers_100000Elements_ReturnSameNumber()
        {
            // Arrange
            DistinctIntegers listTest = new ();

            // Assert
            Assert.That(listTest.GetList(), Has.Count.EqualTo(10000));
        }

        /// <summary>
        /// Testing that the hash set is working correctly testing against the LINQ .Distinct().
        /// </summary>
        [Test]
        public void HashSetDistinct_DistinctList_ReturnSameCount()
        {
            // Arrange
            DistinctIntegers listTest = new ();

            // Act
            var list = listTest.GetList();
            var actual = list.Distinct().ToList();

            // Assert
            Assert.That(listTest.HashSetDistinct(), Is.EqualTo(actual.Count));
        }

        /// <summary>
        /// Testing that the double traverse is working correctly against the LINQ .Distinct().
        /// </summary>
        [Test]
        public void TraverseEachDistinct_DistinctList_ReturnSameCount()
        {
            // Arrange
            DistinctIntegers listTest = new ();

            // Act
            var list = listTest.GetList();
            var actual = list.Distinct().ToList();

            // Assert
            Assert.That(listTest.TraverseEachDistinct(), Is.EqualTo(actual.Count));
        }

        /// <summary>
        /// Testing that the sort distince is working correctly against the LINQ .Distinct().
        /// </summary>
        [Test]
        public void SortDistinct_DistinctList_ReturnSameCount()
        {
            // Arrange
            DistinctIntegers listTest = new ();

            // Act
            var list = listTest.GetList();
            var actual = list.Distinct().ToList();

            // Assert
            Assert.That(listTest.SortDistinct(), Is.EqualTo(actual.Count));
        }
    }
}