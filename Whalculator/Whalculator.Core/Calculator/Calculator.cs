using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator {

		private delegate Task<string> Command(string input);

		private readonly BaseCalculator baseCalculator;

		private readonly Dictionary<string, Command> commands;

		public Calculator() {
			this.baseCalculator = CalculatorFactory.GetDefaultCalculator();
			this.commands = new Dictionary<string, Command>() {
				{ "d/dx", DerivativeCommand }
			};
		}

		public async Task<string> ProcessInputAsync(string input) {
			string result;
			input = input.Trim();

			int index = input.IndexOf(' ');

			if (index != -1) {
				string cmdString = input.Substring(0, index);
				string restString = input.Substring(index + 1);

				foreach (var pair in this.commands) {
					if (pair.Key == cmdString) {
						return await pair.Value.Invoke(restString);
					}
				}
			}

			input = input.Replace(" ", "");

			int i = input.IndexOf('=');
			if (i == -1) {
				result = (await this.baseCalculator.GetResultValueAsync(input)).GetEquationString();
			} else {
				string head = input.Substring(0, i);
				string body = input.Substring(i + 1);

				int hi = head.IndexOf('(');
				if (hi == -1) {
					if (await this.baseCalculator.SetVariableAsync(head, body)) {
						result = $"{head} = {null}";
					} else {
						result = "ERROR";
					}
				} else {
					if (await this.baseCalculator.SetFunctionAsync(head, body)) {
						result = $"{head} = {body}";
					} else {
						result = "ERROR";
					}
				}
			}

			return result;
		}

		private async Task<string> DerivativeCommand(string input) {
			return (await (await this.baseCalculator.GetSolvableFromTextAsync(input)).GetDerivativeAsync("x")).GetEquationString();
		}

	}
}
