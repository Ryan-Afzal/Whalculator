using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	internal class DefaultSimplificationProvider : ISimplificationProvider {

		private readonly ISolvable solvable;
		private readonly LinkedList<ISimplifier> simplifiers;

		internal DefaultSimplificationProvider(ISolvable solvable) {
			this.solvable = solvable.Clone();
			this.simplifiers = new LinkedList<ISimplifier>();
		}

		public bool AddSimplifier(ISimplifier simplifier) {
			this.simplifiers.AddLast(simplifier);
			return true;
		}

		public async Task<ISolvable> SimplifyAsync() {
			ISolvable s = this.solvable.Clone();
			string str = "";

			while (!str.Equals(s.GetEquationString())) {
				str = s.GetEquationString();

				LinkedListNode<ISimplifier>? node = this.simplifiers.First;
				while (node is LinkedListNode<ISimplifier>) {
					bool pass;
					(s, pass) = await node.Value.Simplify(s);

					if (pass) {
						node = node.Next;
					} else {
						node = null;
					}
				}
			}

			return s;
		}

	}
}
