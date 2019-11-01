using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class BuiltinFunction : NestedSolvable {

		public BuiltinFunction(BuiltinFunctionOperation operation, params ISolvable[] operands) : base(operands) {
			Operation = operation;

			if (operands.Length != Operation.NumArgs) {
				throw new InvalidEquationException(ErrorCode.InvalidNumArguments);
			}
		}

		public BuiltinFunctionOperation Operation { get; }

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return Operation.ExactValueOperation.Invoke(this.operands, args);
		}

		public override double GetDoubleValue(ExpressionEvaluationArgs args) {
			return Operation.DoubleValueOperation.Invoke(this.operands, args);
		}

		public override ISolvable Clone() {
			return new BuiltinFunction(
				Operation, 
				this.CloneOperands());
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();
			builder.Append(Operation.Name);
			builder.Append('(');

			if (this.operands.Length > 0) {
				builder.Append(this.operands[0].GetEquationString());

				for (int i = 1; i < this.operands.Length; i++) {
					builder.Append(",");
					builder.Append(this.operands[i].GetEquationString());
				}
			}

			builder.Append(')');
			return builder.ToString();
		}
	}
}
