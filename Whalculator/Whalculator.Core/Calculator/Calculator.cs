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

		private static ISolvable GetDerivative(ISolvable input, DerivativeArgs args) {
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
					ISolvable[] _args = new ISolvable[o.operands.Length];

					for (int i = 0; i < _args.Length; i++) {
						_args[i] = GetDerivative(o.operands[i], args);
					}

					return new Operator(o.Operation, _args);
				} else if (o.Operation.Name == '*') {
					ISolvable[] _args = new ISolvable[o.operands.Length];

					for (int i = 0; i < _args.Length; i++) {
						ISolvable[] __args = new ISolvable[_args.Length];
						
						for (int j = 0; j < i; j++) {
							__args[j] = o.operands[j].Clone();
						}

						__args[i] = GetDerivative(o.operands[i], args);

						for (int j = i + 1; j < _args.Length; j++) {
							__args[j] = o.operands[j].Clone();
						}

						_args[i] = new Operator(Operations.MultiplyOperation, __args);
					}

					return new Operator(Operations.AddOperation, _args);
				} else if (o.Operation.Name == '/') {
					ISolvable n = new Operator(Operations.AddOperation, 
						new Operator(Operations.MultiplyOperation,
							o.operands[1].Clone(),
							GetDerivative(o.operands[0], args)
						),
						new Operator(Operations.MultiplyOperation,
							new Literal(-1),
							GetDerivative(o.operands[1], args),
							o.operands[0].Clone()
						));
					ISolvable d = new Operator(Operations.ExponateOperation, o.operands[1].Clone(), new Literal(2));

					return new Operator(Operations.DivideOperation, n, d);
				} else if (o.Operation.Name == '^') {
					ISolvable o1 = o.Clone();
					ISolvable o2 = GetDerivative(
						new Operator(Operations.MultiplyOperation, 
							o.operands[1].Clone(),
							new BuiltinFunction(BuiltinFunctionOperations.LnOperation, o.operands[0].Clone())
						),
						args
					);

					return new Operator(Operations.MultiplyOperation, o1, o2);
				} else {
					throw new NotImplementedException();
				}
			} else if (input is BuiltinFunction f) {
				if (f.Operation.Name.Equals("ln")) {
					return new Operator(Operations.DivideOperation, new Literal(1), f.operands[0].Clone());
				} else if (f.Operation.Name.Equals("log")) {
					return new Operator(Operations.DivideOperation,
						new Literal(1),
						new Operator(Operations.MultiplyOperation,
							new BuiltinFunction(BuiltinFunctionOperations.LnOperation,
								f.operands[1].Clone()
							),
							f.operands[0].Clone()));
				} else {
					throw new NotImplementedException();
				}
			} else {
				throw new NotImplementedException();
			}
		}
	}
}
