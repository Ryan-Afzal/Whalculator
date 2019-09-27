using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class Operator : ISolvable {

		private readonly ExactValueOperation exactValueOperation;
		private readonly DoubleValueOperation doubleValueOperation;

		private readonly ISolvable[] operands;

		public Operator(ExactValueOperation exactValueOperation, DoubleValueOperation doubleValueOperation, params ISolvable[] operands) {
			this.exactValueOperation = exactValueOperation;
			this.doubleValueOperation = doubleValueOperation;
			this.operands = operands;
		}

		private ISolvable[] CloneOperands() {
			ISolvable[] output = new ISolvable[this.operands.Length];

			for (int i = 0; i < output.Length; i++) {
				output[i] = this.operands[i];
			}

			return output;
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.exactValueOperation.Invoke(this.operands);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return this.doubleValueOperation.Invoke(this.operands);
		}

		public ISolvable Clone() {
			return new Operator(this.exactValueOperation, this.doubleValueOperation, this.CloneOperands());
		}
	}
}
