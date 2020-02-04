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
				s = await this.first.InvokeAsync(s);
			}

			return s;
		}

	}
}
