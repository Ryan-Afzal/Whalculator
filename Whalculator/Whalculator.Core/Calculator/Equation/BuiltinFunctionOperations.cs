using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class BuiltinFunctionOperations {

		public static BuiltinFunctionOperation SineOperation = new BuiltinFunctionOperation("sin", 1, SineExactValueOperation, SineDoubleValueOperation);

		public static ISolvable SineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(operands[0].GetDoubleValue(args)));
		}

		public static double SineDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Sin(operands[0].GetDoubleValue(args));
		}

	}
}
