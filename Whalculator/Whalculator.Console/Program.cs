using System;
using Whalculator.Core.Calculator;

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
				if (input.Contains('=')) {
					throw new NotImplementedException();
				} else {
					Console.WriteLine(calc.GetDoubleValue(input));
				}

				input = Console.ReadLine();
			}
		}
	}
}
