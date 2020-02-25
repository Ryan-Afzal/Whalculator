using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class ExactValuesSimplifier : Simplifier {
		protected async override Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o) {
					if (o.Operation.Name == Operations.AddOperation.Name) {
						int c = o.operands.Length;

						/*
						 * Add Literals
						 */
						for (int i = 0; i < o.operands.Length - 1; i++) {// Go from 0 to the penultimate operand
							if (o.operands[i] is Literal l) {// If the loop never finds a literal, it's OK: if the last operand is a literal, it couldn't be simplified here anyway.
								for (int k = i + 1; k < o.operands.Length; k++) {
									if (o.operands[i] is Literal _l) {
										o.operands[i] = new Literal(l.Value + _l.Value);
										o.operands[k] = null!;
										c--;
									}
								}

								break;
							}

							if (!(o.operands[i] is Operator mult && mult.Operation.Name == Operations.MultiplyOperation.Name)) {
								o.operands[i] = new Operator(Operations.MultiplyOperation, o.operands[i], new Literal(1));
							}

							var curr = (Operator)o.operands[i];

							if (o.operands[i + 1] is Operator nextOperator) {
								if (nextOperator.Operation.Name == Operations.ExponateOperation.Name && nextOperator.operands[0].Equals(curr.operands[0])) {
									o.operands[i + 1] = new Operator(Operations.ExponateOperation, 
										nextOperator.operands[0], 
										new Operator(Operations.AddOperation, 
											nextOperator.operands[1], 
											curr.operands[1]
											)
										);
									o.operands[i] = null!;
									c--;
								}
							}
						}

						/*
						 * Remove nulls and return
						 */
						ISolvable[] output = new ISolvable[c];
						int _i = 0;
						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is object) {
								output[_i] = o.operands[i];
								_i++;
							}
						}

						return (new Operator(Operations.AddOperation, output), true);
					} else if (o.Operation.Name == Operations.MultiplyOperation.Name) {
						throw new NotImplementedException();
					} else if (o.Operation.Name == Operations.DivideOperation.Name) {
						throw new NotImplementedException();
					} else if (o.Operation.Name == Operations.ExponateOperation.Name) {
						throw new NotImplementedException();
					} else if (o.Operation.Name == Operations.DivideOperation.Name) {
						throw new NotImplementedException();
					} else {
						for (int i = 0; i < o.operands.Length; i++) {
							(o.operands[i], _) = await InvokeAsync(o.operands[i]);
						}

						return (o, true);
					}
				} else if (n is BuiltinFunction b) {
					if (b.Operation.Name == BuiltinFunctionOperations.LnOperation.Name) {
						throw new NotImplementedException();
					} else if (false) {
						throw new NotImplementedException();
					} else {
						for (int i = 0; i < b.operands.Length; i++) {
							(b.operands[i], _) = await InvokeAsync(b.operands[i]);
						}

						return (b, true);
					}
				} else {
					for (int i = 0; i < n.operands.Length; i++) {
						(n.operands[i], _) = await InvokeAsync(n.operands[i]);
					}

					return (n, true);
				}
			} else {
				return (solvable, true);
			}
		}
	}
}
