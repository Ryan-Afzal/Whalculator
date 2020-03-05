using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation;
using Whalculator.Core.Calculator.Equation.Simplifiers;

namespace Whalculator.Core.Calculator {
	public class BaseCalculator {

		public BaseCalculator() {
			Settings = this.GetDefaultSettings();

			OperatorSet = new OperatorSet();
			BuiltinFunctionOperationSet = new BuiltinFunctionOperationSet();
			Variables = new VariableSet();
			Functions = new FunctionSet();
		}

		public CalculatorSettings Settings { get; }

		public OperatorSet OperatorSet { get; }
		public BuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; }

		public VariableSet Variables { get; }
		public FunctionSet Functions { get; }

		protected virtual CalculatorSettings GetDefaultSettings() {
			return new CalculatorSettings() { IsDegrees = true };
		}

		public async Task<ISolvable> GetSolvableFromTextAsync(string input) {
			return await ExpressionBuilder.GetSolvableAsync(input, new GenerationArgs() {
				OperatorSet = OperatorSet,
				BuiltinFunctionOperationSet = BuiltinFunctionOperationSet,
				BracketPairs = new char[,] {
					{ '(', ')' },
					{ '{', '}' },
					{ '<', '>' },
					{ '[', ']' }
				}
			});
		}

		public async Task<IResult> GetResultValueAsync(string input) {
			return await (await this.GetSolvableFromTextAsync(input))
				.GetResultValueAsync(this.GetArgs());
		}

		public async Task<ISolvable> GetExactValueAsync(string input) {
			return await (await this.GetSolvableFromTextAsync(input))
				.GetSimplifier()
					.AddLevelOperatorSimplifier()
					.AddRationalExpressionsSimplifier()
					.AddRemoveZerosOnesSimplifier()
					.AddExactValuesSimplifier()
				.SimplifyAsync();
		}

		public async Task<bool> SetVariableAsync(string head, string body) {
			return Variables.SetVariable(head, await this.GetResultValueAsync(body));
		}

		public async Task<bool> SetFunctionAsync(string head, string body) {
			int hi = head.IndexOf('(');
			string name = head.Substring(0, hi);

			if (name.Equals("\'")) {
				throw new ArgumentException("Cannot use a keyword as a function name");
			} else if (name.Equals("$")) {
				throw new ArgumentException("Cannot use a keyword as a function name");
			}

			var argnames = new Dictionary<string, int>();
			string[] fnArgs = head.Substring(hi + 1, head.Length - hi - 2).Split(',');

			for (int k = 0; k < fnArgs.Length; k++) {
				argnames[fnArgs[k]] = k;
			}

			FunctionInfo info = new FunctionInfo() {
				Name = name,
				Head = head,
				ArgNames = argnames,
				Function = await this.GetSolvableFromTextAsync(body)
			};

			return Functions.SetFunction(info.Name, info);
		}

		internal ExpressionEvaluationArgs GetArgs() {
			return new ExpressionEvaluationArgs() {
				VariableSet = Variables,
				FunctionSet = Functions,
				Args = new FunctionArgumentArgs() {
					ArgNames = new Dictionary<string, int>(),
					Args = new ISolvable[0]
				},
				IsDegrees = Settings.IsDegrees
			};
		}
	}
}
