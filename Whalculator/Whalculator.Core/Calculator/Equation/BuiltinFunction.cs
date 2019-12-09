using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Represents a builtin function, such as <c>sin(x)</c> or <c>ln(x)</c>. These functions are not redefineable.
	/// </summary>
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

		public override IResult GetResultValue(ExpressionEvaluationArgs args) {
			return Operation.ResultValueOperation.Invoke(this.operands, args);
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
