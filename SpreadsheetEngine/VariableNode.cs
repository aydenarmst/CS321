// <copyright file="VariableNode.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Variables node.
    /// </summary>
    public class VariableNode : ExpressionTreeNode
    {
        private readonly string vName;

        private Dictionary<string, double> vDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">Name of the variable node.</param>
        /// <param name="variables">Reference of the dictionary of variables that will be changed by the user when setting values of variable nodes. </param>
        public VariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.vName = name;
            this.vDict = variables;
            this.vDict[this.vName] = 0;
        }

        /// <summary>
        /// Returns the value of the node using the dictionary of variables. if not found the value will be 0.
        /// </summary>
        /// <returns>0.0 or the value assigned to the variable in the variables. </returns>
        public override double Evaluate()
        {
            double value = 0.0;
            if (this.vDict.ContainsKey(this.vName))
            {
                value = this.vDict[this.vName];
            }

            return value;
        }
    }
}
