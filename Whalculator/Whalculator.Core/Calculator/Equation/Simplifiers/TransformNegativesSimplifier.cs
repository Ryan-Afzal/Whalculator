using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class TransformNegativesSimplifier : Simplifier {
		public override async Task<ISolvable> InvokeAsync(ISolvable solvable) {
			if (solvable is Operator o && o.Operation.Name == '-') {
				if (o.operands[0] is Literal l && l.Value == 0) {
					return new Operator(Operations.MultiplyOperation, new Literal(-1), await InvokeNext(o.operands[1]));
				} else {
					return new Operator(Operations.AddOperation, await InvokeNext(o.operands[0]), new Operator(Operations.MultiplyOperation, new Literal(-1), await InvokeNext(o.operands[1])));
				}
			} else {
				return await InvokeNext(solvable);
			}
		}
	}
}
