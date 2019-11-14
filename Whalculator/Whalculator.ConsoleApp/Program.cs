using System;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;

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
							Console.WriteLine(calc.GetDoubleValue(input));
						} else {
							string head = input.Substring(0, i);
							string body = input.Substring(i + 1);

							int hi = head.IndexOf('(');
							if (hi == -1) {
								calc.Variables.SetVariable(head, new Literal(calc.GetDoubleValue(body)));
							} else {
								throw new NotImplementedException();
							}
						}
					}
				} catch (InvalidEquationException ie) {
					Console.WriteLine("ERROR: " + nameof(ie.ErrorCode));
				} catch (Exception) {
					Console.WriteLine("ERROR: An unknown error occured.");
				} finally {
					input = Console.ReadLine();
				}
			}
		}
	}
}
