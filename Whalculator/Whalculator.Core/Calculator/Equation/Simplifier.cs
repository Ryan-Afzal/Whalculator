﻿using System;
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

					if (len == 1) {
						return o.operands[0];
					}

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

		public static ISolvable SimplifyRemoveZeros(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '*') {
					int z = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Literal l) {
							if (l.Value == 0) {
								return new Literal(0);
							} else if (l.Value == 1) {
								o.operands[i] = null;
								z++;
							}
						}
					}

					if (z != 0) {
						int length = o.operands.Length - z;

						if (length == 0) {
							return new Literal(1);
						} else if (length == 1) {
							for (int i = 0; i < o.operands.Length; i++) {
								if (!(o.operands[i] is null)) {
									return o.operands[i];
								}
							}
						}

						ISolvable[] args = new ISolvable[length];
						int k = 0;

						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is null) {
								continue;
							}

							args[k] = o.operands[i];
							k++;
						}

						return new Operator(o.Operation, args);
					}

					return solvable;
				} else if (o.Operation.Name == '+') {
					int z = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Literal l) {
							if (l.Value == 0) {
								o.operands[i] = null;
								z++;
							}
						}
					}

					if (z != 0) {
						int length = o.operands.Length - z;

						if (length == 0) {
							return new Literal(0);
						} else if (length == 1) {
							for (int i = 0; i < o.operands.Length; i++) {
								if (!(o.operands[i] is null)) {
									return o.operands[i];
								}
							}
						}

						ISolvable[] args = new ISolvable[length];
						int k = 0;

						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is null) {
								continue;
							}

							args[k] = o.operands[i];
							k++;
						}

						return new Operator(o.Operation, args);
					}
				} else if (o.Operation.Name == '^') {
					if (o.operands[0] is Literal l) {
						if (l.Value == 0) {
							return new Literal(0);
						} else if (l.Value == 1) {
							return new Literal(1);
						}
					}

					if (o.operands[1] is Literal _l) {
						if (_l.Value == 0) {
							return new Literal(1);
						} else if (_l.Value == 1) {
							return o.operands[0];
						}
					}
				}
			}

			return solvable;
		}

		public static ISolvable SimplifySortTerms(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '+') {
					Array.Sort(o.operands, ISolvable.Compare);

					return solvable;
				} else if (o.Operation.Name == '*') {
					Array.Sort(o.operands, ISolvable.Compare);

					return solvable;
				}
			}

			return solvable;
		}

		public static ISolvable SimplifyCollectLikeTerms(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '+') {
					int c = o.operands.Length - 1;

					for (int i = c; i > 0; i--) {
						if (o.operands[i] is Literal l) {
							if (o.operands[i - 1] is Literal _l) {
								o.operands[i - 1] = new Literal(l.Value + _l.Value);
								o.operands[i] = null;
								c--;
								continue;
							}
						}

						//if (!(o.operands[i] is Operator mult && mult.Operation.Name == '*')) {
						//	o.operands[i] = new Operator(Operations.MultiplyOperation, o.operands[i], new Literal(1));
						//}

						//var curr = o.operands[i] as Operator;

						//if (o.operands[i - 1] is Operator prevOperator) {
						//	if (prevOperator.Operation.Name == '^') {
						//		if (prevOperator.operands[0].Equals(curr.operands[0])) {
						//			o.operands[i - 1] = new Operator(Operations.ExponateOperation, prevOperator.operands[0], new Operator(Operations.AddOperation, prevOperator.operands[1], curr.operands[1]));
						//			o.operands[i] = null;
						//			c--;
						//			continue;
						//		}
						//	}
						//}
					}

					ISolvable[] output = new ISolvable[c + 1];
					int k = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (!(o.operands[i] is null)) {
							output[k] = o.operands[i];
							k++;
						}
					}

					return new Operator(Operations.AddOperation, output);
				} else if (o.Operation.Name == '*') {
					int c = o.operands.Length - 1;

					for (int i = c; i > 0; i--) {
						if (o.operands[i] is Literal l) {
							if (o.operands[i - 1] is Literal _l) {
								o.operands[i - 1] = new Literal(l.Value * _l.Value);
								o.operands[i] = null;
								c--;
								continue;
							}
						}

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
									continue;
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