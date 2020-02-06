using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public class LevelOperatorsSimplifier : Simplifier {
		public override async Task<ISolvable> InvokeAsync(ISolvable solvable) {
			if (solvable is NestedSolvable n) {
				n = await InvokeOnChildrenAsync(n);

				if (n is Operator o
				&& (o.Operation.Name == '+'
				|| o.Operation.Name == '*')) {
					int len = o.operands.Length;

					if (len == 1) {
						return await InvokeNext(o.operands[0]);
					}

					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Operator _o && _o.Operation.Name == o.Operation.Name) {
							len += _o.operands.Length - 1;
						}
					}

					ISolvable[] arr = new ISolvable[len];
					int c = 0;
					for (int i = 0; i < o.operands.Length; i++) {
						if (o.operands[i] is Operator _o && _o.Operation.Name == o.Operation.Name) {
							foreach (var s in _o.operands) {
								arr[c] = s;
								c++;
							}
						} else {
							arr[c] = o.operands[i];
							c++;
						}
					}

					return await InvokeNext(new Operator(o.Operation, arr));
				} else {
					return await InvokeNext(solvable);
				}
			} else {
				return await InvokeNext(solvable);
			}
		}
	}
}
