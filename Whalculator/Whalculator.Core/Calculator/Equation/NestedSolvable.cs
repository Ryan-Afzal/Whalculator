using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Misc;

namespace Whalculator.Core.Calculator.Equation {
	public abstract class NestedSolvable : ISolvable {

		public readonly SortedArray<ISolvable> operands;

		protected NestedSolvable(ISolvable[] operands) {
			this.operands = new SortedArray<ISolvable>(operands);
		}

		public abstract ISolvable Clone();
		public abstract double GetDoubleValue(ExpressionEvaluationArgs args);
		public abstract ISolvable GetExactValue(ExpressionEvaluationArgs args);
		public abstract string GetEquationString();

		protected internal ISolvable[] CloneOperands() {
			ISolvable[] output = new ISolvable[this.operands.Length];

			for (int i = 0; i < output.Length; i++) {
				output[i] = this.operands[i];
			}

			return output;
		}

	}
}
