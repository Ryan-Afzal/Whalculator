using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class Operator : NestedSolvable {

		public Operator(Operation operation, params ISolvable[] operands) : base(operands) {
			Operation = operation;
		}

		public Operation Operation { get; }

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return Operation.ExactValueOperation.Invoke(this.operands, args);
		}

		public override double GetDoubleValue(ExpressionEvaluationArgs args) {
			return Operation.DoubleValueOperation.Invoke(this.operands, args);
		}

		public override ISolvable Clone() {
			return new Operator(Operation, this.CloneOperands());
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();

			builder.Append(operands[0].GetEquationString());
			
			for (int i = 1; i < operands.Length; i++) {
				builder.Append(Operation.Name);
				builder.Append(operands[i].GetEquationString());
			}

			return builder.ToString();
		}
	}
}
