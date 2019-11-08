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

			builder.Append(this.GetStringFromOperand(operands[0]));
			
			for (int i = 1; i < operands.Length; i++) {
				builder.Append(Operation.Name);
				builder.Append(this.GetStringFromOperand(operands[i]));
			}

			return builder.ToString();
		}

		private string GetStringFromOperand(ISolvable x) {
			if (x is Operator o) {
				if (o.Operation.Order < Operation.Order) {
					return $"({o.GetEquationString()})";
				}
			}

			return x.GetEquationString();
		}

		public int CompareTo(ISolvable other) {
			if (other is Literal) {
				return 1;
			} else if (other is Variable) {
				return 1;
			} else {
				return this.GetEquationString().CompareTo(other.GetEquationString());
			}
		}
	}
}
