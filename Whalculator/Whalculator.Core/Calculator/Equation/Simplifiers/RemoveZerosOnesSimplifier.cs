using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class RemoveZerosOnesSimplifier : Simplifier {
		protected override async Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				n = await InvokeOnChildrenAsync(n);

				if (n is Operator o) {
					if (o.Operation.Name == '*') {
						int z = 0;
						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is Literal l) {
								if (l.Value == 0) {
									return (new Literal(0), false);
								} else if (l.Value == 1) {
									o.operands[i] = null!;
									z++;
								}
							}
						}

						if (z != 0) {
							int length = o.operands.Length - z;

							if (length == 0) {
								return (new Literal(1), false);
							} else if (length == 1) {
								for (int i = 0; i < o.operands.Length; i++) {
									if (!(o.operands[i] is null)) {
										return (o.operands[i], true);
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

							return (new Operator(o.Operation, args), true);
						}

						return (solvable, true);
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
								return (new Literal(0), false);
							} else if (length == 1) {
								for (int i = 0; i < o.operands.Length; i++) {
									if (!(o.operands[i] is null)) {
										return (o.operands[i], true);
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

							return (new Operator(o.Operation, args), true);
						}

						return (solvable, true);
					} else if (o.Operation.Name == '^') {
						if (o.operands[0] is Literal l) {
							if (l.Value == 0) {
								return (new Literal(0), false);
							} else if (l.Value == 1) {
								return (new Literal(1), false);
							}
						}

						if (o.operands[1] is Literal _l) {
							if (_l.Value == 0) {
								return (new Literal(1), false);
							} else if (_l.Value == 1) {
								return (o.operands[0], true);
							}
						}

						return (solvable, true);
					} else {
						return (solvable, true);
					}
				} else {
					return (solvable, true);
				}
			} else {
				return (solvable, true);
			}
		}
	}
}
