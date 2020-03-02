using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class RemoveZerosOnesSimplifier : Simplifier {
		public override ISolvable Invoke(ISolvable solvable, ISimplifierHook hook) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o) {
					if (o.Operation.Name == '*') {
						int z = 0;
						for (int i = 0; i < o.operands.Length; i++) {
							if (o.operands[i] is Literal l) {
								if (l.Value == 0) {
									hook.Modified();
									return new Literal(0);
								} else if (l.Value == 1) {
									o.operands[i] = null!;
									z++;
								}
							}
						}

						if (z != 0) {
							hook.Modified();
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
							hook.Modified();
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

						return solvable;
					} else if (o.Operation.Name == '^') {
						if (o.operands[0] is Literal l) {
							if (l.Value == 0) {
								hook.Modified();
								return new Literal(0);
							} else if (l.Value == 1) {
								hook.Modified();
								return new Literal(1);
							}
						}

						if (o.operands[1] is Literal _l) {
							if (_l.Value == 0) {
								hook.Modified();
								return new Literal(1);
							} else if (_l.Value == 1) {
								hook.Modified();
								return o.operands[0];
							}
						}

						return n;
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
