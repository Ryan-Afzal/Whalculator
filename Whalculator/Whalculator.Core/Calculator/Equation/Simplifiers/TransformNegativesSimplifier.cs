using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class TransformNegativesSimplifier : Simplifier {
		public override ISolvable Invoke(ISolvable solvable, ISimplifierHook hook) {
			if (solvable is NestedSolvable n) {
				if (n is Operator o && o.Operation.Name == '-') {
					if (o.operands[0] is Literal l && l.Value == 0) {
						hook.Modified();
						return new Operator(Operations.MultiplyOperation, new Literal(-1), o.operands[1]);
					} else {
						hook.Modified();
						return new Operator(Operations.AddOperation, o.operands[0], new Operator(Operations.MultiplyOperation, new Literal(-1), o.operands[1]));
					}
				} else {
					return solvable;
				}
			} else {
				return solvable;
			}
		}
	}
}
