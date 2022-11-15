// <copyright file="ExpressionTreeNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    /// <summary>
    /// Expression tree superinterface.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        /// <summary>
        /// MEthod to evalaute the parameter.
        /// </summary>
        /// <returns>.</returns>
        public abstract double Evaluate();
    }
}
