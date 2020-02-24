using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class ExactValuesSimplifier : Simplifier {
		protected async override Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o) {
					throw new NotImplementedException();
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
