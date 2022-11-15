// <copyright file="SubtractionOperatorNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Subtraction Operator.
    /// </summary>
    public class SubtractionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets Subtraction operator.
        /// </summary>
        public static char Operator = '-';

        /// <summary>
        /// Gets precedence of subtraction.
        /// </summary>
        public static ushort Precedence => 6;

        /// <summary>
        /// Gets subtraction is Left associative.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Evaluates the subtraction.
        /// </summary>
        /// <returns>Value of the two subtracted.</returns>
        public override double Evaluate()
        {
            if (this.Left != null && this.Right != null)
            {
                return this.Left.Evaluate() - this.Right.Evaluate();
            }
            else
            {
                return 0;
            }
        }
    }
}
