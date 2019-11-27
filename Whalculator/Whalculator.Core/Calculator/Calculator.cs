using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator : ICalculator {

		private class CalculatorSettings : ICalculatorSettings {
			public bool IsDegrees { get; set; }
			public bool SigFigs { get; set; }
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
			return new CalculatorSettings() { IsDegrees = true, SigFigs = false };
		}

		public ISolvable GetSolvableFromText(string input) {
			return ExpressionBuilder.GetSolvable(input, new GenerationArgs() {
				OperatorSet = OperatorSet,
				BuiltinFunctionOperationSet = BuiltinFunctionOperationSet,
				Functions = Functions
			});
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

		private ExpressionEvaluationArgs GetArgs() {
			return new ExpressionEvaluationArgs() {
				VariableSet = Variables,
				Args = new FunctionArgumentArgs() {
					ArgNames = new Dictionary<string, int>(),
					Args = new ISolvable[0]

				}
			};
		}
	}
}
