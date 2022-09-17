// <copyright file="DistinctIntegers.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights Reserved.
// </copyright>

namespace HW2
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///   Contains all methods for performing the # of distinct integers in a list.
    /// </summary>
    public class DistinctIntegers
    {
        /// <summary>
        /// private readonly list.
        /// </summary>
        private readonly List<int> readList;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctIntegers"/> class.
        /// </summary>
        public DistinctIntegers()
        {
            this.readList = new List<int>();
            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                // populates a random non-negative integer within the range of 20,000
                this.readList.Add(random.Next(20000));
            }
        }

        /// <summary>
        ///   getter of the list.
        /// </summary>
        /// <returns>
        ///   returns the list.
        /// </returns>
        public List<int> GetList()
        {
            return this.readList;
        }

        /// <summary>
        ///  distinct integer via boolean hashtable.
        /// </summary>
        /// <returns>
        ///  integer which is the # of distinct elements in the list.
        /// </returns>
        public int HashSetDistinct()
        {
            int numDistinct = 0;
            bool[] hash = new bool[20000];

            foreach (int ele in this.readList)
            {
                hash[ele] = true;
            }

            for (int i = 0; i < hash.Count(); i++)
            {
                if (hash[i])
                {
                    numDistinct++;
                }
            }

            return numDistinct;
        }

        /// <summary>
        ///  method of 0(1) space which iterates through the list and checks if the current element is the only occurance.
        /// </summary>
        /// <returns>
        /// integer which is the # of distinct elements in the list.
        /// </returns>
        public int TraverseEachDistinct()
        {
            // set to 1 to account for the last element that was checked but never updated the counter
            int numDistinct = 1;
            int last = this.readList.Count();

            // main (leading) traverse of the list, ele is the value we compare for the second traverse
            for (int ele = 1; ele < last; ele++)
            {
                bool isDistinct = true;

                // traverses the other direction.
                for (int checkEle = ele - 1; checkEle >= 0; checkEle--)
                {
                    if (this.readList[checkEle] == this.readList[ele])
                    {
                        isDistinct = false;
                    }
                }

                if (isDistinct)
                {
                    numDistinct++;
                }
            }

            return numDistinct;
        }

        /// <summary>
        ///  method to show if there are any duplicate integers around the index of the numbers.
        /// </summary>
        /// <returns>
        /// integer which is the # of distinct elements in the list.
        /// </returns>
        public int SortDistinct()
        {
            // again setting the number of distinct to 1 to account for last element
            int numDistinct = 1;
            this.readList.Sort();
            int last = this.readList.Count();

            // since the list is sorted we can traverse the list once and check for non-distinct values
            for (int ele = 1; ele < last; ele++)
            {
                if (this.readList[ele] != this.readList[ele - 1])
                {
                    numDistinct++;
                }
            }

            return numDistinct;
        }
    }
}
