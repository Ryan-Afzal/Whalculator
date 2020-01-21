using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.TestApp {
	public class Program {
		public static async Task Main(string[] args) {
			var calc = CalculatorFactory.GetDefaultCalculator();

			Task<ISolvable> task1 = TestDerivativeAsync("2*x^2 + sin(x)", calc);
			Task<ISolvable> task2 = calc.GetSolvableFromTextAsync("2*x^2");

			var allTasks = new List<Task<ISolvable>> { task1, task2 };

			while (allTasks.Any()) {
				Task<ISolvable> finished = await Task.WhenAny(allTasks);
				Console.WriteLine((await finished).GetEquationString());
				allTasks.Remove(finished);
			}
		}

		private static async Task<ISolvable> TestDerivativeAsync(string text, Calculator calc) {
			ISolvable solvable = await calc.GetSolvableFromTextAsync(text);
			return await solvable.GetDerivativeAsync("x");
		}
	}
}
