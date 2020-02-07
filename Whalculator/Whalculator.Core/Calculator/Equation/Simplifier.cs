using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {

	public abstract class Simplifier {
		
		internal Simplifier? Next { get; set; }

		protected abstract Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable);

		internal async Task<ISolvable> InvokeBaseAsync(ISolvable solvable) {
			var invokeResult = await InvokeAsync(solvable);

			if (Next is object && invokeResult.Item2) {
				return await Next.InvokeBaseAsync(invokeResult.Item1);
			} else {
				return await Task.FromResult(invokeResult.Item1);
			}
		}

		protected async Task<NestedSolvable> InvokeOnChildrenAsync(NestedSolvable solvable) {
			for (int i = 0; i < solvable.operands.Length; i++) {
				(solvable.operands[i], _) = await InvokeAsync(solvable.operands[i]);
			}

			return solvable;
		}

	}

	public static class OldSimplifiers {

		/// <summary>
		/// Removes zeros and ones in addition, multiplication, and exponation
		/// </summary>
		/// <param name="solvable"></param>
		/// <returns></returns>
		public static ISolvable SimplifyRemoveZerosOnes(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '*') {
					int z = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Literal l) {
							if (l.Value == 0) {
								return new Literal(0);
							} else if (l.Value == 1) {
								o.operands[i] = null!;
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
								o.operands[i] = null!;
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

		/// <summary>
		/// Collects like terms in addition and multiplication
		/// </summary>
		/// <param name="solvable"></param>
		/// <returns></returns>
		public static ISolvable SimplifyCollectLikeTerms(ISolvable solvable) {
			if (solvable is Operator o) {
				if (o.Operation.Name == '+') {
					int c = o.operands.Length - 1;

					for (int i = c; i > 0; i--) {
						if (o.operands[i] is Literal l) {
							if (o.operands[i - 1] is Literal _l) {
								o.operands[i - 1] = new Literal(l.Value + _l.Value);
								o.operands[i] = null!;
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
								o.operands[i] = null!;
								c--;
								continue;
							}
						}

						if (!(o.operands[i] is Operator exp && exp.Operation.Name == '^')) {
							o.operands[i] = new Operator(Operations.ExponateOperation, o.operands[i], new Literal(1));
						}

						var curr = (Operator)o.operands[i];

						if (o.operands[i - 1] is Operator prevOperator) {
							if (prevOperator.Operation.Name == '^') {
								if (prevOperator.operands[0].Equals(curr.operands[0])) {
									o.operands[i - 1] = new Operator(Operations.ExponateOperation, prevOperator.operands[0], new Operator(Operations.AddOperation, prevOperator.operands[1], curr.operands[1]));
									o.operands[i] = null!;
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

		/// <summary>
		/// Turns negative exponation into rational expressions
		/// </summary>
		/// <param name="solvable"></param>
		/// <returns></returns>
		public static ISolvable SimplifyNegativeExponents(ISolvable solvable) {
			if (solvable is Operator mult) {
				if (mult.Operation.Name == '*') {
					int numNegative = 0;

					for (int i = 0; i < mult.operands.Length; i++) {
						if (mult.operands[i] is Operator o
							&& o.Operation.Name == '^'
							&& o.operands[1] is Literal l
							&& l.Value < 0) {
							numNegative++;
						}
					}

					if (numNegative != 0) {
						ISolvable[] numerator = new ISolvable[mult.operands.Length - numNegative];
						ISolvable[] denominator = new ISolvable[numNegative];

						int n = 0;
						int d = 0;

						for (int i = 0; i < mult.operands.Length; i++) {
							if (mult.operands[i] is Operator o
							&& o.Operation.Name == '^'
							&& o.operands[1] is Literal l
							&& l.Value < 0) {
								o.operands[1] = new Literal(l.Value * -1);
								denominator[d] = mult.operands[i];
								d++;
							} else {
								numerator[d] = mult.operands[i];
								n++;
							}
						}

						return new Operator(
							Operations.DivideOperation, 
							numerator.Length == 1 ? numerator[0] : new Operator(Operations.MultiplyOperation, numerator), 
							denominator.Length == 1 ? denominator[0] : new Operator(Operations.MultiplyOperation, denominator)
						);
					}
				}
			}

			return solvable;
		}
	}

}
