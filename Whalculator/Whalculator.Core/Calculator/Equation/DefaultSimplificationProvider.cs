using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	internal class DefaultSimplificationProvider : ISimplificationProvider {

		private readonly ISolvable solvable;
		private Simplifier? first;
		private Simplifier? last;

		internal DefaultSimplificationProvider(ISolvable solvable) {
			this.solvable = solvable.Clone();
			this.first = null;
			this.last = null;
		}

		public ISimplificationProvider AddSimplifier(Simplifier simplifier) {
			if (this.first is null) {
				this.first = simplifier;
				this.last = this.first;
			} else {
				this.last!.Next = simplifier;
				this.last = simplifier;
			}
			
			return this;
		}

		public async Task<ISolvable> SimplifyAsync() {
			ISolvable s = this.solvable.Clone();

			if (this.first is null) {
				return s;
			}

			string str = "";

			while (!str.Equals(s.GetEquationString())) {
				str = s.GetEquationString();
				Simplifier? node = this.first;
				while (node is object) {
					s = await Simplify(node, s);
					node = node.Next;
				}
			}

			return s;
		}

		private async Task<ISolvable> Simplify(Simplifier simp, ISolvable node) {
			if (node is NestedSolvable n) {
				for (int i = 0; i < n.operands.Length; i++) {
					n.operands[i] = await Task.Run(() => simp.Invoke(n.operands[i]));
				}
			}

			return await Task.Run(() => simp.Invoke(node));
		}

	}
}
