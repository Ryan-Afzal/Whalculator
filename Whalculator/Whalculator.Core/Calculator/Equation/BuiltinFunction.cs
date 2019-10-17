using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class BuiltinFunction : ISolvable {

		private readonly BuiltinFunctionOperation operation;

		private readonly ISolvable[] operands;

		public BuiltinFunction(BuiltinFunctionOperation operation, params ISolvable[] operands) {
			this.operation = operation;

			if (operands.Length != this.operation.NumArgs) {
				throw new ArgumentException("Function " + this.operation.Name + " requires " + this.operation.NumArgs + " arguments.");
			}

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
			return this.operation.ExactValueOperation.Invoke(this.operands);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return this.operation.DoubleValueOperation.Invoke(this.operands);
		}

		public ISolvable Clone() {
			return new BuiltinFunction(
				this.operation, 
				this.CloneOperands());
		}
	}
}
