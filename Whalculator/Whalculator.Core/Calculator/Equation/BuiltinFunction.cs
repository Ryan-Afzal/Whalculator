using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class BuiltinFunction : NestedSolvable {

		private readonly BuiltinFunctionOperation operation;

		public BuiltinFunction(BuiltinFunctionOperation operation, params ISolvable[] operands) : base(operands) {
			this.operation = operation;

			if (operands.Length != this.operation.NumArgs) {
				throw new InvalidEquationException(ErrorCode.InvalidNumArguments);
			}
		}

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.operation.ExactValueOperation.Invoke(this.operands, args);
		}

		public override double GetDoubleValue(ExpressionEvaluationArgs args) {
			return this.operation.DoubleValueOperation.Invoke(this.operands, args);
		}

		public override ISolvable Clone() {
			return new BuiltinFunction(
				this.operation, 
				this.CloneOperands());
		}
	}
}
