using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {

	public abstract class Simplifier {

		internal Simplifier? Next { get; set; }

		protected abstract Task<(ISolvable, bool)> InvokeAsync(ISolvable solvable);

		internal async Task<ISolvable> InvokeBaseAsync(ISolvable solvable) {
			var invokeResult = await InvokeAsync(solvable);

			if (Next is object && invokeResult.Item2) {
				return await Next.InvokeBaseAsync(invokeResult.Item1);
			} else {
				return await Task.FromResult(invokeResult.Item1);
			}
		}

		protected async Task<NestedSolvable> InvokeOnChildrenAsync(NestedSolvable solvable) {
			for (int i = 0; i < solvable.operands.Length; i++) {
				solvable.operands[i] = await InvokeBaseAsync(solvable.operands[i]);
			}

			return solvable;
		}

	}

}