using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class NegativeExponentsSimplifier : Simplifier {
		public override ISolvable Invoke(ISolvable solvable, ISimplifierHook hook) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o) {
					if (o.Operation.Name == '^') {
						if (o.operands[1] is Literal l && l.Value < 0) {
							hook.Modified();
							return new Operator(Operations.DivideOperation,
								new Literal(1),
								new Operator(Operations.ExponateOperation,
									o.operands[0],
									new Literal(Math.Abs(l.Value))
									)
								);
						} else {
							return o;
						}
					} else if (o.Operation.Name == '*') {
						int numNegative = 0;

						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is Operator _o
								&& _o.Operation.Name == '^'
								&& _o.operands[1] is Literal l
								&& l.Value < 0) {
								numNegative++;
							}
						}

						if (numNegative != 0) {
							hook.Modified();
							ISolvable[] numerator = new ISolvable[o.operands.Length - numNegative];
							ISolvable[] denominator = new ISolvable[numNegative];

							int _n = 0;
							int d = 0;

							for (int i = 0; i < o.operands.Length; i++) {
								if (o.operands[i] is Operator _o
								&& _o.Operation.Name == '^'
								&& _o.operands[1] is Literal l
								&& l.Value < 0) {
									_o.operands[1] = new Literal(l.Value * -1);
									denominator[d] = o.operands[i];
									d++;
								} else {
									numerator[d] = o.operands[i];
									_n++;
								}
							}

							return new Operator(
								Operations.DivideOperation,
								numerator.Length == 1 ? numerator[0] : new Operator(Operations.MultiplyOperation, numerator),
								denominator.Length == 1 ? denominator[0] : new Operator(Operations.MultiplyOperation, denominator)
							);
						} else {
							return n;
						}
					} else {
						return o;
					}
				} else {
					return n;
				}
			} else {
				return solvable;
			}
		}
	}
}
