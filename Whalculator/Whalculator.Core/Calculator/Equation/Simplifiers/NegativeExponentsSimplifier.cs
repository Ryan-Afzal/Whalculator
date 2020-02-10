using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class NegativeExponentsSimplifier : Simplifier {
		protected override async Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				n = await InvokeOnChildrenAsync(n);

				if (n is Operator mult) {
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

						int _n = 0;
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
								_n++;
							}
						}

						return (new Operator(
							Operations.DivideOperation,
							numerator.Length == 1 ? numerator[0] : new Operator(Operations.MultiplyOperation, numerator),
							denominator.Length == 1 ? denominator[0] : new Operator(Operations.MultiplyOperation, denominator)
						), true);
					} else {
						return (n, true);
					}
				} else {
					return (n, true);
				}
			} else {
				return (solvable, true);
			}
		}
	}
}
