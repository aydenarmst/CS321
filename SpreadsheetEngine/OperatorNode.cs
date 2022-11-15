// <copyright file="OperatorNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    /// <summary>
    /// Adding the left and the right subtree, relying on abstraction.
    /// </summary>
    public abstract class OperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Defining what associativity might be, covers both cases.
        /// </summary>
        public enum OperatorAssociativity
        {
            /// <summary>
            /// Right associativity.
            /// </summary>
            Right,

            /// <summary>
            /// Left associativity.
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets or sets left child.
        /// </summary>
        public ExpressionTreeNode? Left { get; set; }

        /// <summary>
        /// Gets or sets right child.
        /// </summary>
        public ExpressionTreeNode? Right { get; set; }
    }
}
