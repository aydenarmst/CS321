// <copyright file="OperatorNodeFactory.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Creates the operator node.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Creating an operator node with the current expressions supported.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>new node of given operator.</returns>
        /// <exception cref="NotSupportedException">exception thrown.</exception>
        internal OperatorNode CreateOperatorNode(char op)
        {
            // but which one?
            return op switch
            {
                '+' => new AdditionOperatorNode(),
                '-' => new SubtractionOperatorNode(),
                '*' => new MultiplicationOperatorNode(),
                '/' => new DivisonOperatorNode(),

                // if it is not any of the operators that we support, throw an exception:
                _ => throw new NotSupportedException(
                                            "Operator " + op + " not supported."),
            };
            throw new NotSupportedException();
        }
    }
}
