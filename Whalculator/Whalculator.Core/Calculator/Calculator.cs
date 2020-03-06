using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator {

		private delegate Task<string> Command(params string[] args);

		private readonly BaseCalculator baseCalculator;

		private readonly Dictionary<string, Command> commands;

		public Calculator() {
			this.baseCalculator = CalculatorFactory.GetDefaultCalculator();
			this.commands = new Dictionary<string, Command>() {
				{ "d/d", DerivativeCommand }
			};
		}

		public IViewableBuiltinFunctionOperationSet BuiltinFunctions {
			get {
				return this.baseCalculator.BuiltinFunctionOperationSet;
			}
		}

		public IViewableFunctionSet Functions {
			get {
				return this.baseCalculator.Functions;
			}
		}

		public IViewableOperatorSet Operators {
			get {
				return this.baseCalculator.OperatorSet;
			}
		}

		public IViewableVariableSet Variables {
			get {
				return this.baseCalculator.Variables;
			}
		}

		public async Task<string> ProcessInputAsync(string input) {
			string result;
			input = input.Trim();

			int index = input.IndexOf(' ');

			if (index != -1) {
				string cmdString = input.Substring(0, index);
				string restString = input
					//.Substring(index + 1)
					.Trim();

				foreach (var pair in this.commands) {
					if (pair.Key == cmdString) {
						var list = new LinkedList<string>();

						int last = -1;
						for (int k = 0; k < restString.Length; k++) {
							char c = restString[k];

							if (c == ' ') {
								if (restString[k + 1] == '-') {
									if (last != -1) {
										list.AddLast(restString[(last)..k].Replace("-", "").Trim());
									}

									last = k;
								} else {
									list.AddLast(restString[(last)..k].Replace("-", "").Trim());
									list.AddLast(restString.Substring(k + 1));
									break;
								}
							}
						}

						return await pair.Value.Invoke(list.ToArray());
					}
				}
			}

			input = input.Replace(" ", "");

			int i = input.IndexOf('=');
			if (i == -1) {
				var res = await this.baseCalculator.GetExactValueAsync(input);

				if (res is IResult) {
					result = res.GetEquationString();
				} else {
					result = $"{res.GetEquationString()}\n{(await res.GetResultValueAsync(this.baseCalculator.GetArgs())).GetEquationString()}";
				}
			} else {
				string head = input.Substring(0, i);
				string body = input.Substring(i + 1);

				int hi = head.IndexOf('(');
				if (hi == -1) {
					if (await this.baseCalculator.SetVariableAsync(head, body)) {
						result = $"{head} = {Variables.GetVariable(head)}";
					} else {
						throw new InvalidOperationException();
					}
				} else {
					if (await this.baseCalculator.SetFunctionAsync(head, body)) {
						result = $"{head} = {body}";
					} else {
						throw new InvalidOperationException();
					}
				}
			}

			return result;
		}

		private async Task<string> DerivativeCommand(string[] args) {
			return (await (await this.baseCalculator.GetSolvableFromTextAsync(args[1])).GetDerivativeAsync(args[0], true)).GetEquationString();
		}

	}
}
