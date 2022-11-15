// <copyright file="ConstantNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Constant Node.
    /// </summary>
    public class ConstantNode : ExpressionTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">Constant node is created, when we need to evalaute then we return the value.</param>
        public ConstantNode(double value)
        {
            this.ConstValue = value;
        }

        private double ConstValue { get; set; }

        /// <summary>
        /// Evaluate the constant Node.
        /// </summary>
        /// <returns>The constant value of the current node.</returns>
        public override double Evaluate()
        {
            return this.ConstValue;
        }
    }
}
