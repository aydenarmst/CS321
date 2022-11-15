// <copyright file="AdditionOperatorNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Additon operator.
    /// </summary>
    public class AdditionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets addition Operator.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Gets additon Precedence.
        /// </summary>
        public static ushort Precedence => 7;

        /// <summary>
        /// Gets left associative.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Override to evaluate the additon operator.
        /// </summary>
        /// <returns>sum of the left and right.</returns>
        public override double Evaluate()
        {
            if (this.Left != null && this.Right != null)
            {
                return this.Left.Evaluate() + this.Right.Evaluate();
            }
            else
            {
                return 0;
            }
        }
    }
}
