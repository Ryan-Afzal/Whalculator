using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Operations {

		public static ISolvable AddExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(operands[0].GetDoubleValue(args) + operands[1].GetDoubleValue(args));
		}

		public static double AddDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return operands[0].GetDoubleValue(args) + operands[1].GetDoubleValue(args);
		}

	}
}
