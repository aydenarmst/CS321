// <copyright file="Program.cs" company="Ayden Armstrong [011672241]">
// Copyright (c) Ayden Armstrong [011672241]. All rights reserved.
// </copyright>
namespace ExpressionTreeConsole
{
    using SpreadsheetEngine;

    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry.
        /// </summary>
        /// <param name="args">string argument.</param>
        public static void Main()
        {
            int route = 0;
            string defaultExp = "A1+B1+C1";
            ExpressionTree centralTree = new (defaultExp);

            while (route != 4)
            {
                route = ChoiceMenu(defaultExp);
                if (route == 1)
                {
                    Console.WriteLine("Enter a new expression: ");
                    string? ln = Console.ReadLine();
                    if (ln == null)
                    {
                        break;
                    }

                    defaultExp = ln;
                    centralTree = new ExpressionTree(defaultExp);
                }
                else if (route == 2)
                {
                    Console.WriteLine("Enter a variable name: ");
                    string? name = Console.ReadLine();
                    if (name == null)
                    {
                        break;
                    }

                    Console.WriteLine("Enter a variable value: ");
                    string? value = Console.ReadLine();
                    if (value == null)
                    {
                        break;
                    }

                    centralTree.SetVariable(name, double.Parse(value));
                }
                else if (route == 3)
                {
                    Console.WriteLine("Evaluated tree = " + centralTree.Evaluate().ToString());
                }
                else
                {
                    Console.WriteLine("End");
                }
            }
        }

        private static int ChoiceMenu(string exp)
        {
            Console.WriteLine("Menu (current expression='" + exp + "')");
            Console.WriteLine("1 = Enter a new expression");
            Console.WriteLine("2 = Set a variable value");
            Console.WriteLine("3 = Evaluate tree");
            Console.WriteLine("4 = Quit");
            Console.Write("\r\nSelect and option: ");

            return Console.ReadLine() switch
            {
                "1" => 1,
                "2" => 2,
                "3" => 3,
                "4" => 4,
                _ => 4,
            };
        }
    }
}