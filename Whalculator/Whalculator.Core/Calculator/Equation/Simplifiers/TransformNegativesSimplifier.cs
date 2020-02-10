using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class TransformNegativesSimplifier : Simplifier {
		protected override async Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				n = await InvokeOnChildrenAsync(n);

				if (n is Operator o && o.Operation.Name == '-') {
					if (o.operands[0] is Literal l && l.Value == 0) {
						return (new Operator(Operations.MultiplyOperation, new Literal(-1), o.operands[1]), true);
					} else {
						return (new Operator(Operations.AddOperation, o.operands[0], new Operator(Operations.MultiplyOperation, new Literal(-1), o.operands[1])), true);
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
