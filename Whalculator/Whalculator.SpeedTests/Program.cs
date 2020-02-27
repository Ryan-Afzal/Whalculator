using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation.Simplifiers;

namespace Whalculator.SpeedTests {
	public class Program {
		public static async Task Main(string[] args) {
			var calc = new Calculator();
			var baseCalc = new BaseCalculator();
			_ = baseCalc.GetResultValueAsync("1").Result;
			Console.WriteLine("Starting Speed Tests...");

			Console.WriteLine("Starting Node Graph Tests...");
			TestExpression(baseCalc, "1");
			TestExpression(baseCalc, "1+1");
			TestExpression(baseCalc, "1000000*23");
			TestExpression(baseCalc, "e");
			TestExpression(baseCalc, "e^10");
			TestExpression(baseCalc, "ln(e)");
			TestExpression(baseCalc, "ln(e + 100)");
			TestExpression(baseCalc, "(2+(100^2*40+25)*100+10/2-354)^2");
			Console.WriteLine("Ended Node Graph Tests.");
			
			Console.WriteLine("Starting Input Tests...");
			TestExpression(calc, "1");
			TestExpression(calc, "1+1");
			TestExpression(calc, "d/d -x x^2 + x*y + y^2");
			Console.WriteLine("Ended Input Tests.");

			Console.WriteLine("Starting Exact Value Tests...");
			await TestExactValue(baseCalc, "1 + 5");
			await TestExactValue(baseCalc, "1 + 5 + 3");
			await TestExactValue(baseCalc, "1 + 5 + 3 + (-1)");
			await TestExactValue(baseCalc, "1 + 5 + 3 + (-1)");
			await TestExactValue(baseCalc, "1 + 5 + 3 + ln(10)");
			Console.WriteLine("Ended Exact Value Tests.");
		}

		private static void TestExpression(BaseCalculator calc, string exp) {
			Console.WriteLine($"Expression: {exp}");
   
			var timer = new Stopwatch();
			timer.Start();
			var result = calc.GetResultValueAsync(exp).Result;
			timer.Stop();

			Console.WriteLine($"\tResult: {result.GetEquationString()}");
			Console.WriteLine($"\tElapsed Ticks: {timer.ElapsedTicks}");
			Console.WriteLine($"\tElapsed Time: {timer.ElapsedMilliseconds}");
		}

		private static void TestExpression(Calculator calc, string exp) {
			Console.WriteLine($"Input: {exp}");

			var timer = new Stopwatch();
			timer.Start();
			var result = calc.ProcessInputAsync(exp).Result;
			timer.Stop();

			Console.WriteLine($"\tResult: {result}");
			Console.WriteLine($"\tElapsed Ticks: {timer.ElapsedTicks}");
			Console.WriteLine($"\tElapsed Time: {timer.ElapsedMilliseconds}");
		}

		private static async Task TestExactValue(BaseCalculator calc, string exp) {
			Console.WriteLine($"Input: {exp}");
			var result = await (await calc.GetSolvableFromTextAsync(exp))
				.GetSimplifier()
				.AddLevelOperatorSimplifier()
				.AddRationalExpressionsSimplifier()
				.AddRemoveZerosOnesSimplifier()
				.AddCollectLikeTermsSimplifier()
				.AddSimplifier(new ExactValuesSimplifier())
				.SimplifyAsync();
			Console.WriteLine($"\tResult: {result.GetEquationString()}");
		}
	}
}
