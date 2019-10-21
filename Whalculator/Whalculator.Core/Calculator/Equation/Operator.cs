using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class Operator : NestedSolvable {

		private readonly Operation operation;

		public Operator(Operation operation, params ISolvable[] operands) : base(operands) {
			this.operation = operation;
		}

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.operation.ExactValueOperation.Invoke(this.operands, args);
		}

		public override double GetDoubleValue(ExpressionEvaluationArgs args) {
			return this.operation.DoubleValueOperation.Invoke(this.operands, args);
		}

		public override ISolvable Clone() {
			return new Operator(this.operation, this.CloneOperands());
		}
	}
}
