using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public delegate ISolvable Simplifier(ISolvable solvable);

	public static class Simplifiers {

		public static ISolvable SimplifyTransformNegatives(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '-') {
					if (o.operands[0] is Literal l) {
						if (l.GetDoubleValue(new ExpressionEvaluationArgs()) == 0) {
							return new Operator(Operations.MultiplyOperation, new Literal(-1), o.operands[1]);
						}
					}

					return new Operator(Operations.AddOperation, o.operands[0], new Operator(Operations.MultiplyOperation, new Literal(-1), o.operands[1]));
				}
			}

			return solvable;
		}

		public static ISolvable SimplifyLevelOperators(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '+' || o.Operation.Name == '*') {
					int len = o.operands.Length;

					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Operator _o && _o.Operation.Name == o.Operation.Name) {
							len += _o.operands.Length - 1;
						}
					}

					if (len == o.operands.Length) {
						return solvable;
					} else {
						ISolvable[] arr = new ISolvable[len];
						int c = 0;

						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is Operator _o && _o.Operation.Name == o.Operation.Name) {
								foreach (var s in _o.operands) {
									arr[c] = s;
									c++;
								}
							} else {
								arr[c] = o.operands[i];
								c++;
							}
						}

						return new Operator(o.Operation, arr);
					}
				} else {
					return solvable;
				}
			} else {
				return solvable;
			}
		}

		public static ISolvable SimplifyRationalExpressions(ISolvable solvable) {
			throw new NotImplementedException();
		}

		public static ISolvable SimplifyZeros(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '*') {
					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Literal l) {
							if (l.Value == 0) {
								return new Literal(0);
							}
						}
					}
				} else if (o.Operation.Name == '+') {
					int z = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Literal l) {
							if (l.Value == 0) {
								z++;
							}
						}
					}

					if (z != 0) {
						ISolvable[] args = new ISolvable[o.operands.Length - z];
						int k = 0;

						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is Literal l) {
								if (l.Value == 0) {
									continue;
								}
							}

							args[k] = o.operands[i];
							k++;
						}

						return new Operator(o.Operation, args);
					}
				}
			}

			return solvable;
		}

	}
	
}
