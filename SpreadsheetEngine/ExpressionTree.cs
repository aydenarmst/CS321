// <copyright file="ExpressionTree.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Expression Tree class.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Root node of the tree.
        /// </summary>
        private ExpressionTreeNode? root;

        /// <summary>
        /// Dictionary containing variable values that can be set and altered.
        /// </summary>
        private Dictionary<string, double>? vNodes = new ();

        /// <summary>
        /// Expression.
        /// </summary>
        private string[] expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expre">input expression.</param>
        public ExpressionTree(string expre)
        {
            // Tokenizing and converting to postfix.
            // https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.split?view=net-7.0
            string[] token = Regex.Split(expre, @"([*()\^\/]|(?<!E)[\+\-])");
            string[] postfixed = ConvertToPostFix(token);
            Array.Reverse(postfixed);

            this.expression = postfixed;
            this.vNodes = new Dictionary<string, double>();

            Stack<ExpressionTreeNode> stack = new ();

            OperatorNodeFactory opFactory = new ();

            for (int i = 0; i < postfixed.Length; i++)
            {
                string element = postfixed[i];

                if (element == string.Empty)
                {
                    continue;
                }
                else if (element == "/" || element == "*" || element == "+" || element == "-")
                {
                    // Getting the operands.
                    ExpressionTreeNode temp1 = stack.Pop();
                    ExpressionTreeNode temp2 = stack.Pop();

                    // Generating operator node with left and right children.
                    OperatorNode opNode = opFactory.CreateOperatorNode(char.Parse(element));
                    opNode.Right = temp1;
                    opNode.Left = temp2;

                    // New head.
                    stack.Push(opNode);
                }

                // if it is a constant.
                else if (double.TryParse(postfixed[i], out double value))
                {
                    try
                    {
                        ExpressionTreeNode constNode = new ConstantNode(value);
                        stack.Push(constNode);
                    }
                    catch (Exception w)
                    {
                        Console.WriteLine(w.Message);
                    }
                }

                // assuming is has to be a variable.
                else
                {
                    try
                    {
                        this.vNodes.Add(postfixed[i], 0);

                        // Passing reference to the vNodes.
                        ExpressionTreeNode variableNode = new VariableNode(postfixed[i], ref this.vNodes);
                        stack.Push(variableNode);
                    }
                    catch (Exception w)
                    {
                        Console.WriteLine(w.Message);
                    }
                }
            }

            this.root = stack.Peek();
        }

        /// <summary>
        /// From infix to postfix aka shunting yard algorithm.
        /// </summary>
        /// <param name="exp">string expression.</param>
        /// <returns>infixed expression.</returns>
        public static string[] ConvertToPostFix(string[] exp)
        {
            // Stack we will return.
            Stack<string> postFix = new ();

            // Empty stack to hold operators.
            Stack<string> stack = new ();

            // Parsing the expression for operators, constants, or parenthesis.
            for (int i = 0; i < exp.Length; ++i)
            {
                switch (exp[i])
                {
                    case "*":
                    case "+":
                    case "-":
                    case "/":
                        OperatorNodeExpression(exp, postFix, stack, i);
                        break;
                    case "(":
                        stack.Push(exp[i]);
                        break;
                    case ")":
                        DisgardParenthesis(postFix, stack);
                        break;
                    default:
                        postFix.Push(exp[i]);
                        break;
                }
            }

            while (stack.Count > 0)
            {
                postFix.Push(stack.Pop());
            }

            return postFix.ToArray();
        }

        /// <summary>
        /// Sets a new variable in the dictionary.
        /// </summary>
        /// <param name="name">names of the var.</param>
        /// <param name="value">value associated.</param>
        public void SetVariable(string name, double value)
        {
            // Will throw exception for a key already in the dict.
            try
            {
                this.vNodes.Add(name, value);
            }
            catch
            {
                this.vNodes![name] = value;
            }
        }

        /// <summary>
        /// Public Evaluate.
        /// </summary>
        /// <returns>.</returns>
        public double Evaluate()
        {
            if (this.root != null)
            {
                return this.root.Evaluate();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a list of all the variables we currently have in the dict.
        /// </summary>
        /// <returns>a new list with the keys.</returns>
        public List<string> GetVariables()
        {
            if (this.vNodes != null)
            {
                return new List<string>(this.vNodes.Keys);
            }
            else
            {
                throw new ArgumentException("There is no varaibles in the dict.");
            }
        }

        /// <summary>
        /// Checks if the name is already in the dictionary.
        /// </summary>
        /// <param name="name">name of variable.</param>
        /// <returns>bool of if found or not.</returns>
        public bool InDictionary(string name)
        {
            bool inDict = this.vNodes.ContainsKey(name);
            return inDict switch
            {
                true => true,
                _ => false,
            };
        }

        /// <summary>
        /// Gets the precedence of the current operators.
        /// </summary>
        /// <param name="operatorCheck">Char of operator.</param>
        /// <returns>precedence.</returns>
        internal static int Precedence(string operatorCheck)
        {
            return operatorCheck switch
            {
                "+" or "-" => 1,
                "*" or "/" => 2,
                _ => -1,
            };
        }

        /// <summary>
        /// Disgards the right parenthesis.
        /// </summary>
        /// <param name="postFix">opStack.</param>
        /// <param name="stack">constant Stack.</param>
        /// <param name="err">error message.</param>
        /// <param name="accepted">accepted message.</param>
        private static void DisgardParenthesis(Stack<string> postFix, Stack<string> stack)
        {
            while (stack.Count > 0 && stack.Peek() != "(")
            {
                postFix.Push(stack.Pop());
            }

            if (stack.Count > 0 && stack.Peek() != "(")
            {
                throw new Exception("Error");
            }
            else
            {
                stack.Pop();
            }
        }

        /// <summary>
        /// Print the operand to post fix.
        /// </summary>
        /// <param name="exp">expression.</param>
        /// <param name="postFix">opStack.</param>
        /// <param name="stack">constant stack.</param>
        /// <param name="i">element index.</param>
        private static void OperatorNodeExpression(string[] exp, Stack<string> postFix, Stack<string> stack, int i)
        {
            while (stack.Count > 0 && Precedence(exp[i]) <= Precedence(stack.Peek()))
            {
                postFix.Push(stack.Pop());
            }

            stack.Push(exp[i]);
        }
    }
}
