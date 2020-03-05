using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class CollectLikeTermsSimplifier : Simplifier {
		public override ISolvable Invoke(ISolvable solvable, ISimplifierHook hook) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o) {
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
						}

						if (c < o.operands.Length - 1) {
							hook.Modified();
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

						int e = 0;

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
								e++;
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

						if (c < o.operands.Length - 1) {
							hook.Modified();
						}

						ISolvable[] output = new ISolvable[c + 1];
						int k = 0;
						for (int i = 0; i < o.operands.Length; i++) {
							if (!(o.operands[i] is null)) {
								if (o.operands[i] is Operator exp && exp.Operation.Name == '^' && exp.operands[1] is Literal l && l.Value == 1) {
									e--;
									output[k] = exp.operands[0];
									k++;
								} else {
									output[k] = o.operands[i];
									k++;
								}
							}
						}

						if (e > 0) {
							hook.Modified();
						}

						return new Operator(Operations.MultiplyOperation, output);
					} else {
						return n;
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
