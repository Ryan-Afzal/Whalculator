using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public abstract class Operator : ISolvable {

		protected readonly ISolvable[] operands;

		public Operator(params ISolvable[] operands) {
			this.operands = operands;
		}

		protected ISolvable[] CloneOperands() {
			ISolvable[] output = new ISolvable[this.operands.Length];

			for (int i = 0; i < output.Length; i++) {
				output[i] = this.operands[i];
			}

			return output;
		}

		public abstract ISolvable Clone();
		public abstract double GetDoubleValue(ExpressionEvaluationArgs args);
		public abstract ISolvable GetExactValue(ExpressionEvaluationArgs args);
		public abstract ISolvable Add(ISolvable other);
		public abstract ISolvable Divide(ISolvable other);
		public abstract ISolvable Exponate(ISolvable other);
		public abstract ISolvable Multiply(ISolvable other);
	}
}
