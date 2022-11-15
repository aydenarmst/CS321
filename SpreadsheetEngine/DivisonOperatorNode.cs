// <copyright file="DivisonOperatorNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Divison Operator.
    /// </summary>
    public class DivisonOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets divison Operator.
        /// </summary>
        public static char Operator => '/';

        /// <summary>
        /// Gets operator Precedence.
        /// </summary>
        public static ushort Precedence => 6;

        /// <summary>
        /// Gets left associative.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Evaluates the left and right.
        /// </summary>
        /// <returns>Returns the divison value.</returns>
        public override double Evaluate()
        {
            if (this.Left != null && this.Right != null)
            {
                return this.Left.Evaluate() / this.Right.Evaluate();
            }
            else
            {
                return 0;
            }
        }
    }
}
