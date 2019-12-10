using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class Operations {

		public static readonly Operation AddOperation = new Operation(AddExactValueOperation, AddResultValueOperation, 0, '+', true);
		public static readonly Operation SubtractOperation = new Operation(SubtractExactValueOperation, SubtractResultValueOperation, 0, '-', false);
		public static readonly Operation DivideOperation = new Operation(DivideExactValueOperation, DivideResultValueOperation, 1, '/', false);
		public static readonly Operation ModuloOperation = new Operation(ModuloExactValueOperation, ModuloResultValueOperation, 1, '%', false);
		public static readonly Operation MultiplyOperation = new Operation(MultiplyExactValueOperation, MultiplyResultValueOperation, 2, '*', true);
		public static readonly Operation ExponateOperation = new Operation(ExponateExactValueOperation, ExponateResultValueOperation, 3, '^', false);
		
		public static ISolvable AddExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 0.0;

			foreach (var o in operands) {
				output += ((Literal)o.GetResultValue(args)).Value;
			}

			return new Literal(output);
		}

		public static IResult AddResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			//double output = 0.0;

			//foreach (var o in operands) {
			//	output += ((Literal)o.GetResultValue(args)).Value;
			//}

			//return new Literal(output);
			return AddExactValueOperation(operands, args).GetResultValue(args);
		}

		public static ISolvable SubtractExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 0.0;

			foreach (var o in operands) {
				output -= ((Literal)o.GetResultValue(args)).Value;
			}

			return new Literal(output);
		}

		public static IResult SubtractResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 0.0;

			foreach (var o in operands) {
				output -= ((Literal)o.GetResultValue(args)).Value;
			}

			return new Literal(output);
		}

		public static ISolvable MultiplyExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 1.0;

			foreach (var o in operands) {
				output *= ((Literal)o.GetResultValue(args)).Value;
			}

			return new Literal(output);
		}

		public static IResult MultiplyResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 1.0;

			foreach (var o in operands) {
				output *= ((Literal)o.GetResultValue(args)).Value;
			}

			return new Literal(output);
		}

		public static ISolvable DivideExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(((Literal)operands[0].GetResultValue(args)).Value / ((Literal)operands[1].GetResultValue(args)).Value);
		}

		public static IResult DivideResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(((Literal)operands[0].GetResultValue(args)).Value / ((Literal)operands[1].GetResultValue(args)).Value);
		}

		public static ISolvable ExponateExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)operands[0].GetResultValue(args)).Value, ((Literal)operands[1].GetResultValue(args)).Value));
		}

		public static IResult ExponateResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)operands[0].GetResultValue(args)).Value, ((Literal)operands[1].GetResultValue(args)).Value));
		}

		public static ISolvable ModuloExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(((Literal)operands[0].GetResultValue(args)).Value % ((Literal)operands[1].GetResultValue(args)).Value);
		}

		public static IResult ModuloResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(((Literal)operands[0].GetResultValue(args)).Value % ((Literal)operands[1].GetResultValue(args)).Value);
		}
	}
}
