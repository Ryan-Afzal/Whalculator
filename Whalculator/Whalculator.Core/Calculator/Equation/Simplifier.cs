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

		/// <summary>
		/// Turns division into negative exponation
		/// </summary>
		/// <param name="solvable"></param>
		/// <returns></returns>
		public static ISolvable SimplifyRationalExpressions(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '/') {
					return new Operator(
						Operations.MultiplyOperation,
						o.operands[0],
						new Operator(
							Operations.ExponateOperation,
							o.operands[1],
							new Literal(-1)
						)
					);
				}
			}

			return solvable;
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

		public static ISolvable SimplifySortTerms(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '+') {
					Array.Sort(o.operands);

					return solvable;
				} else if (o.Operation.Name == '*') {
					Array.Sort(o.operands);

					return solvable;
				}
			}

			return solvable;
		}

		public static ISolvable SimplifyCollectLikeTerms(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '+') {
					
				} else if (o.Operation.Name == '*') {
					int c = o.operands.Length - 1;

					for (int i = c; i > 0; i--) {
						if (!(o.operands[i] is Operator exp && exp.Operation.Name == '^')) {
							o.operands[i] = new Operator(Operations.ExponateOperation, o.operands[i], new Literal(1));
						}

						var curr = o.operands[i] as Operator;

						if (o.operands[i - 1] is Operator prevOperator) {
							if (prevOperator.Operation.Name == '^') {
								if (prevOperator.operands[0].Equals(curr.operands[0])) {
									o.operands[i - 1] = new Operator(Operations.ExponateOperation, prevOperator.operands[0], new Operator(Operations.AddOperation, prevOperator.operands[1], curr.operands[1]));
									o.operands[i] = null;
									c--;
								}
							}
						}
					}

					ISolvable[] output = new ISolvable[c + 1];
					int k = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (!(o.operands[i] is null)) {
							output[k] = o.operands[i];
							k++;
						}
					}

					return new Operator(Operations.MultiplyOperation, output);
				}
			}

			return solvable;
		}
	}

}
