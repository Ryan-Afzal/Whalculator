using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class RationalExpressionsSimplifier : Simplifier {
		public override ISolvable Invoke(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o && o.Operation.Name == '/') {
					return new Operator(
						Operations.MultiplyOperation, 
						o.operands[0],
						new Operator(
							Operations.ExponateOperation,
							o.operands[1],
							new Literal(-1)
							)
						);
				} else {
					return solvable;
				}
			} else {
				return solvable;
			}
		}
	}
}
