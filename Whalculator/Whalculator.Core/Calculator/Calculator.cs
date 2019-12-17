using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator : ICalculator {

		private class CalculatorSettings : ICalculatorSettings {
			public bool IsDegrees { get; set; }
		}

		protected internal Calculator() {
			Settings = this.GetSettings();

			OperatorSet = new OperatorSet();
			BuiltinFunctionOperationSet = new BuiltinFunctionOperationSet();
			Variables = new VariableSet();
			Functions = new FunctionSet();
		}

		public ICalculatorSettings Settings { get; }

		public IOperatorSet OperatorSet { get; }
		public IBuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; }

		public IVariableSet Variables { get; }
		public IFunctionSet Functions { get; }

		protected virtual ICalculatorSettings GetSettings() {
			return new CalculatorSettings() { IsDegrees = true };
		}

		public IResult GetResultValue(string input) {
			return this.GetSolvableFromText(input)
				.GetResultValue(this.GetArgs());
		}

		public string GetExactValue(string input) {
			return this.GetSolvableFromText(input)
				.GetExactValue(this.GetArgs())
				.GetEquationString();
		}

		public bool SetVariable(string head, string body) {
			return Variables.SetVariable(head, this.GetResultValue(body));
		}

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
