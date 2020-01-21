using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator {

		protected internal Calculator() {
			Settings = this.GetDefaultSettings();

			OperatorSet = new OperatorSet();
			BuiltinFunctionOperationSet = new BuiltinFunctionOperationSet();
			Variables = new VariableSet();
			Functions = new FunctionSet();
		}

		public CalculatorSettings Settings { get; }

		public IOperatorSet OperatorSet { get; }
		public IBuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; }

		public IVariableSet Variables { get; }
		public IFunctionSet Functions { get; }

		protected virtual CalculatorSettings GetDefaultSettings() {
			return new CalculatorSettings() { IsDegrees = true };
		}

		[Obsolete]
		public ISolvable GetSolvableFromText(string input) {
			return ExpressionBuilder.GetSolvable(input, new GenerationArgs() {
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

		[Obsolete]
		public IResult GetResultValue(string input) {
			return this.GetSolvableFromText(input)
				.GetResultValue(this.GetArgs());
		}

		public async Task<IResult> GetResultValueAsync(string input) {
			return (await this.GetSolvableFromTextAsync(input))
				.GetResultValue(this.GetArgs());
		}

		[Obsolete]
		public string GetExactValue(string input) {
			return this.GetSolvableFromText(input)
				.GetExactValue(this.GetArgs())
				.GetEquationString();
		}

		public async Task<string> GetExactValueAsync(string input) {
			return (await this.GetSolvableFromTextAsync(input))
				.GetExactValue(this.GetArgs())
				.GetEquationString();
		}

		[Obsolete]
		public bool SetVariable(string head, string body) {
			return Variables.SetVariable(head, this.GetResultValue(body));
		}

		public async Task<bool> SetVariableAsync(string head, string body) {
			return Variables.SetVariable(head, await this.GetResultValueAsync(body));
		}

		[Obsolete]
		public bool SetFunction(string head, string body) {
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
				Function = this.GetSolvableFromText(body)
			};

			return Functions.SetFunction(info.Name, info);
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

		private ExpressionEvaluationArgs GetArgs() {
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
