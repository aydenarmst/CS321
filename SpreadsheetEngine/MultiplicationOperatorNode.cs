// <copyright file="MultiplicationOperatorNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Muliply Operator.
    /// </summary>
    public class MultiplicationOperatorNode : OperatorNode
    {
        /// <summary>
        /// Multiply operator.
        /// </summary>
        public static char Operator = '*';

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public static ushort Precedence => 6;

        /// <summary>
        /// Gets left associative property.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Multiplies the left and right.
        /// </summary>
        /// <returns>Result of the multiplication.</returns>
        public override double Evaluate()
        {
            if (this.Left != null && this.Right != null)
            {
                return this.Left.Evaluate() * this.Right.Evaluate();
            }
            else
            {
                return 0;
            }
        }
    }
}
