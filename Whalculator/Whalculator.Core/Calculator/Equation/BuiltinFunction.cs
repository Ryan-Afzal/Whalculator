using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class BuiltinFunction : ISolvable {

		private readonly string name;
		private readonly int numargs;

		private readonly BuiltinFunctionExactValueOperation exactValueOperation;
		private readonly BuiltinFunctionDoubleValueOperation doubleValueOperation;

		private readonly ISolvable[] operands;

		public BuiltinFunction(string name, int numargs, BuiltinFunctionExactValueOperation exactValueOperation, BuiltinFunctionDoubleValueOperation doubleValueOperation, params ISolvable[] operands) {
			this.name = name;
			this.numargs = numargs;

			if (operands.Length != this.numargs) {
				throw new ArgumentException("Function " + this.name + " requires " + this.numargs + " arguments.");
			}

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
			return new BuiltinFunction(
				this.name, 
				this.numargs, 
				this.exactValueOperation, 
				this.doubleValueOperation, 
				this.CloneOperands());
		}
	}
}
