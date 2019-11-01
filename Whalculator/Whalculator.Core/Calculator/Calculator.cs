using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator : ICalculator {

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
			throw new NotImplementedException();
		}

		public ISolvable GetSolvableFromText(string input) {
			return ExpressionBuilder.GetSolvable(input, new GenerationArgs() {
				OperatorSet = OperatorSet,
				BuiltinFunctionOperationSet = BuiltinFunctionOperationSet,
				Functions = Functions
			});
		}

		public double GetDoubleValue(string input) {
			return this.GetSolvableFromText(input)
				.GetDoubleValue(new ExpressionEvaluationArgs() { VariableSet = Variables });
		}

		public string GetExactValue(string input) {
			return this.GetSolvableFromText(input)
				.GetExactValue(new ExpressionEvaluationArgs() { VariableSet = Variables })
				.GetEquationString();
		}

		public struct DerivativeArgs {
			public string IndependentVariable { get; set; }
		}

		public static ISolvable GetDerivative(ISolvable input, string ind) {
			return GetDerivative(input, new DerivativeArgs() { IndependentVariable = ind });
		}

		public static ISolvable GetDerivative(ISolvable input, DerivativeArgs args) {
			if (input is Literal) {
				return new Literal(0);
			} else if (input is Variable v) {
				if (v.VariableName.Equals(args.IndependentVariable)) {
					return new Literal(1);
				} else {
					return new Literal(0);
				}
			} else if (input is Operator o) {
				if (o.Operation.Name == '+') {
					throw new NotImplementedException();
				} else if (o.Operation.Name == '*') {
					throw new NotImplementedException();
				} else if (o.Operation.Name == '/') {
					throw new NotImplementedException();
				} else if (o.Operation.Name == '^') {
					throw new NotImplementedException();
				} else {
					throw new NotImplementedException();
				}
			} else if (input is BuiltinFunction f) {
				throw new NotImplementedException();
			} else {
				throw new NotImplementedException();
			}
		}
	}
}
