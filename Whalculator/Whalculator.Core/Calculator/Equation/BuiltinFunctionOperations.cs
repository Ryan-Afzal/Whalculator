using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class BuiltinFunctionOperations {

		public static BuiltinFunctionOperation AbsOperation = new BuiltinFunctionOperation("abs", 1, AbsExactValueOperation, AbsDoubleValueOperation);
		public static BuiltinFunctionOperation CeilOperation = new BuiltinFunctionOperation("ceil", 1, CeilExactValueOperation, CeilDoubleValueOperation);
		public static BuiltinFunctionOperation FloorOperation = new BuiltinFunctionOperation("floor", 1, FloorExactValueOperation, FloorDoubleValueOperation);
		public static BuiltinFunctionOperation RootOperation = new BuiltinFunctionOperation("root", 2, RootExactValueOperation, RootDoubleValueOperation);
		public static BuiltinFunctionOperation SqrtOperation = new BuiltinFunctionOperation("sqrt", 1, SqrtExactValueOperation, SqrtDoubleValueOperation);
		public static BuiltinFunctionOperation LogOperation = new BuiltinFunctionOperation("log", 2, LogExactValueOperation, LogDoubleValueOperation);
		public static BuiltinFunctionOperation LnOperation = new BuiltinFunctionOperation("ln", 1, LnExactValueOperation, LnDoubleValueOperation);


		public static BuiltinFunctionOperation SineOperation = new BuiltinFunctionOperation("sin", 1, SineExactValueOperation, SineDoubleValueOperation);

		public static ISolvable AbsExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Abs(operands[0].GetDoubleValue(args)));
		}

		public static double AbsDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Abs(operands[0].GetDoubleValue(args));
		}

		public static ISolvable CeilExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Ceiling(operands[0].GetDoubleValue(args)));
		}

		public static double CeilDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Ceiling(operands[0].GetDoubleValue(args));
		}

		public static ISolvable FloorExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Floor(operands[0].GetDoubleValue(args)));
		}

		public static double FloorDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Floor(operands[0].GetDoubleValue(args));
		}

		public static ISolvable RootExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(operands[0].GetDoubleValue(args), 1 / operands[1].GetDoubleValue(args)));
		}

		public static double RootDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Pow(operands[0].GetDoubleValue(args), 1 / operands[1].GetDoubleValue(args));
		}

		public static ISolvable SqrtExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sqrt(operands[0].GetDoubleValue(args)));
		}

		public static double SqrtDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Sqrt(operands[0].GetDoubleValue(args));
		}

		public static ISolvable LogExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(operands[1].GetDoubleValue(args)) / Math.Log(operands[0].GetDoubleValue(args)));
		}

		public static double LogDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Log(operands[1].GetDoubleValue(args)) / Math.Log(operands[0].GetDoubleValue(args));
		}

		public static ISolvable LnExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(operands[0].GetDoubleValue(args)));
		}

		public static double LnDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Log(operands[0].GetDoubleValue(args));
		}

		public static ISolvable SineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(operands[0].GetDoubleValue(args)));
		}

		public static double SineDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Sin(operands[0].GetDoubleValue(args));
		}

	}
}
