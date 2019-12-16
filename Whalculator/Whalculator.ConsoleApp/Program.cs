using System;
using System.Collections.Generic;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;
using Whalculator.Core.Misc;

namespace Whalculator.ConsoleApp {
	public class Program {
		public static void Main(string[] args) {
			Console.WriteLine("Starting Whalculator...");
			
			Console.Write("\tLoading Calculator...");
			var calc = CalculatorFactory.GetDefaultCalculator();
			Console.WriteLine("Done");

			Console.WriteLine("Loading Complete.");

			Console.WriteLine("Welcome to Whalculator! Type \'--quit\' to quit");

			string input = Console.ReadLine();
			while (!input.Equals("--quit")) {
				try {
					input = input.Replace(" ", "");
					
					int idx = input.IndexOf("d/dx");
					if (idx == 0) {
						string body = input.Substring(4);
						Console.WriteLine(calc.GetSolvableFromText(body).GetDerivative("x").GetEquationString());
					} else {
						int i = input.IndexOf('=');
						if (i == -1) {
							Console.WriteLine(calc.GetExactValue(input));
						} else {
							string head = input.Substring(0, i);
							string body = input.Substring(i + 1);
							
							int hi = head.IndexOf('(');
							if (hi == -1) {
								calc.SetVariable(head, body);
							} else {
								calc.SetFunction(head, body);
							}
						}
					}
				} catch (InvalidEquationException ie) {
					Console.WriteLine("ERROR: " + ie.ErrorCode.ToString());
				} catch (Exception e) {
					Console.WriteLine("ERROR: " + e.ToString());
				} finally {
					input = Console.ReadLine();
				}
			}
		}
	}
}
