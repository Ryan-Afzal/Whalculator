using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class Operations {

		public static readonly Operation AddOperation = new Operation(AddExactValueOperation, AddDoubleValueOperation, 0, '+');
		public static readonly Operation SubtractOperation = new Operation(SubtractExactValueOperation, SubtractDoubleValueOperation, 0, '-');
		public static readonly Operation DivideOperation = new Operation(DivideExactValueOperation, DivideDoubleValueOperation, 1, '/');
		public static readonly Operation ModuloOperation = new Operation(ModuloExactValueOperation, ModuloDoubleValueOperation, 1, '%');
		public static readonly Operation MultiplyOperation = new Operation(MultiplyExactValueOperation, MultiplyDoubleValueOperation, 2, '*');
		public static readonly Operation ExponateOperation = new Operation(ExponateExactValueOperation, ExponateDoubleValueOperation, 3, '^');
		public static ISolvable AddExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double sum = 0.0;

			foreach (var o in operands) {
				sum += o.GetDoubleValue(args);
			}

			return new Literal(sum);
		}

		public static double AddDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double sum = 0.0;

			foreach (var o in operands) {
				sum += o.GetDoubleValue(args);
			}

			return sum;
		}

		public static ISolvable SubtractExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double sum = 0.0;

			foreach (var o in operands) {
				sum -= o.GetDoubleValue(args);
			}

			return new Literal(sum);
		}

		public static double SubtractDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double sum = 0.0;

			foreach (var o in operands) {
				sum -= o.GetDoubleValue(args);
			}

			return sum;
		}

		public static ISolvable MultiplyExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double sum = 1.0;

			foreach (var o in operands) {
				sum *= o.GetDoubleValue(args);
			}

			return new Literal(sum);
		}

		public static double MultiplyDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double sum = 1.0;

			foreach (var o in operands) {
				sum *= o.GetDoubleValue(args);
			}

			return sum;
		}

		public static ISolvable DivideExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(operands[0].GetDoubleValue(args) / operands[1].GetDoubleValue(args));
		}

		public static double DivideDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return operands[0].GetDoubleValue(args) / operands[1].GetDoubleValue(args);
		}

		public static ISolvable ExponateExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(operands[0].GetDoubleValue(args), operands[1].GetDoubleValue(args)));
		}

		public static double ExponateDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return Math.Pow(operands[0].GetDoubleValue(args), operands[1].GetDoubleValue(args));
		}

		public static ISolvable ModuloExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(operands[0].GetDoubleValue(args) % operands[1].GetDoubleValue(args));
		}

		public static double ModuloDoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return operands[0].GetDoubleValue(args) % operands[1].GetDoubleValue(args);
		}
	}
}
