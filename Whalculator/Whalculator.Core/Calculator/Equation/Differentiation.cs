using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation.Simplifiers;

namespace Whalculator.Core.Calculator.Equation {
	public static class Differentiation {

		private struct DerivativeArgs {

			public DerivativeArgs(string ind, bool imp) {
				IndependentVariable = ind;
				Implicit = imp;
			}

			public string IndependentVariable { get; }
			public bool Implicit { get; }
		}

		public static async Task<ISolvable> GetDerivativeAsync(this ISolvable input, string ind, bool imp) {
			ISolvable s = GetDerivative(input, new DerivativeArgs(ind, imp));

			s = await s.SimplifyDerivative1Async();
			s = await s.SimplifyDerivative2Async();
			s = await s.SimplifyDerivative3Async();

			return s;
		}

		/// <summary>
		/// Gets the derivative of the input expression
		/// </summary>
		/// <param name="input"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private static ISolvable GetDerivative(ISolvable input, DerivativeArgs args) {
			if (input is Literal) {
				return new Literal(0);
			} else if (input is Variable v) {
				if (v.VariableName.Equals(args.IndependentVariable)) {
					return new Literal(1);
				} else {
					if (args.Implicit) {
						return new ImplicitDifferentiationSymbol(v.VariableName, args.IndependentVariable);
					} else {
						return new Literal(0);
					}
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
				ISolvable output;

				if (f.Operation.Name.Equals("ln")) {// 1/x
					output = new Operator(Operations.DivideOperation, new Literal(1), f.operands[0].Clone());
				} else if (f.Operation.Name.Equals("log")) {// 1/ln(a)x
					output = new Operator(Operations.DivideOperation,
						new Literal(1),
						new Operator(Operations.MultiplyOperation,
							new BuiltinFunction(BuiltinFunctionOperations.LnOperation,
								f.operands[1].Clone()
							),
							f.operands[0].Clone()));
				} else if (f.Operation.Name.Equals("sqrt")) {
					output = new Operator(Operations.DivideOperation, 
						new Literal(1), 
						new Operator(Operations.MultiplyOperation, 
							new Literal(2),
							new BuiltinFunction(BuiltinFunctionOperations.SqrtOperation, 
								f.operands[0].Clone()
								)
							)
						);
				} else if (f.Operation.Name.Equals("root")) {
					throw new NotImplementedException();
				} else if (f.Operation.Name.Equals("sin")) {// cos(x)
					output = new BuiltinFunction(BuiltinFunctionOperations.CosineOperation, f.CloneOperands());
				} else if (f.Operation.Name.Equals("cos")) {// -sin(x)
					output = new Operator(Operations.MultiplyOperation,
						new Literal(-1),
						new BuiltinFunction(BuiltinFunctionOperations.SineOperation, f.CloneOperands()));
				} else if (f.Operation.Name.Equals("tan")) {// sec(x)^2
					output = new Operator(Operations.ExponateOperation,
						new BuiltinFunction(BuiltinFunctionOperations.SecantOperation, f.CloneOperands()),
						new Literal(2));
				} else if (f.Operation.Name.Equals("sec")) {// sec(x)tan(x)
					output = new Operator(Operations.MultiplyOperation,
						new BuiltinFunction(BuiltinFunctionOperations.SecantOperation, f.CloneOperands()),
						new BuiltinFunction(BuiltinFunctionOperations.TangentOperation, f.CloneOperands())
						);
				} else if (f.Operation.Name.Equals("csc")) {// -csc(x)cot(x)
					output = new Operator(Operations.MultiplyOperation,
						new Literal(-1),
						new BuiltinFunction(BuiltinFunctionOperations.SecantOperation, f.CloneOperands()),
						new BuiltinFunction(BuiltinFunctionOperations.TangentOperation, f.CloneOperands())
						);
				} else if (f.Operation.Name.Equals("cot")) {// -csc(x)^2
					output = new Operator(Operations.MultiplyOperation,
						new Literal(-1),
						new Operator(Operations.ExponateOperation,
							new BuiltinFunction(BuiltinFunctionOperations.SecantOperation, f.CloneOperands()),
							new Literal(2)
						)
					);
				} else if (f.Operation.Name.Equals("arcsin")) {
					throw new NotImplementedException();
				} else if (f.Operation.Name.Equals("arccos")) {
					throw new NotImplementedException();
				} else if (f.Operation.Name.Equals("arctan")) {
					throw new NotImplementedException();
				} else if (f.Operation.Name.Equals("arcsec")) {
					throw new NotImplementedException();
				} else if (f.Operation.Name.Equals("arccsc")) {
					throw new NotImplementedException();
				} else if (f.Operation.Name.Equals("arccot")) {
					throw new NotImplementedException();
				} else {
					throw new NotImplementedException();
				}

				return new Operator(Operations.MultiplyOperation, output, GetDerivative(f.operands[0], args));
			} else if (input is Function _f) {
				throw new NotImplementedException();
			} else {
				throw new NotImplementedException();
			}
		}

		private static async Task<ISolvable> SimplifyDerivative1Async(this ISolvable input) {
			return await input
				.GetSimplifier()
					.AddLevelOperatorSimplifier()
					.AddRemoveZerosOnesSimplifier()
					.AddRationalExpressionsSimplifier()
				.SimplifyAsync();
		}

		private static async Task<ISolvable> SimplifyDerivative2Async(this ISolvable input) {
			//return await input.SimplifyAsync(new Simplifier[] {
			//	Simplifiers.SimplifyRemoveZerosOnes,
			//	Simplifiers.SimplifyLevelOperators,
			//	Simplifiers.SimplifyRationalExpressions,
			//	Simplifiers.SimplifyCollectLikeTerms
			//});
			throw new NotImplementedException();
		}

		private static async Task<ISolvable> SimplifyDerivative3Async(this ISolvable input) {
			return await input
				.GetSimplifier()
					.AddRemoveZerosOnesSimplifier()
					.AddLevelOperatorSimplifier()
				.SimplifyAsync();
		}
	}
}
