using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	public static class Operations {

		public static readonly Operation AddOperation = new Operation(AddResultValueOperation, 0, '+', true);
		public static readonly Operation SubtractOperation = new Operation(SubtractResultValueOperation, 0, '-', false);
		public static readonly Operation MultiplyOperation = new Operation(MultiplyResultValueOperation, 1, '*', true);
		public static readonly Operation DivideOperation = new Operation(DivideResultValueOperation, 2, '/', false);
		public static readonly Operation ModuloOperation = new Operation(ModuloResultValueOperation, 2, '%', false);
		public static readonly Operation ExponateOperation = new Operation(ExponateResultValueOperation, 3, '^', false);
		

		public static async Task<IResult> AddResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 0.0;

			foreach (var o in operands) {
				output += ((Literal)await o.GetResultValueAsync(args)).Value;
			}

			return new Literal(output);
		}

		public static async Task<IResult> SubtractResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 0.0;

			foreach (var o in operands) {
				output -= ((Literal)await o.GetResultValueAsync(args)).Value;
			}

			return new Literal(output);
		}

		public static async Task<IResult> MultiplyResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			double output = 1.0;

			foreach (var o in operands) {
				output *= ((Literal)await o.GetResultValueAsync(args)).Value;
			}

			return new Literal(output);
		}

		public static async Task<IResult> DivideResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(((Literal)await operands[0].GetResultValueAsync(args)).Value / ((Literal)await operands[1].GetResultValueAsync(args)).Value);
		}

		public static async Task<IResult> ExponateResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)await operands[0].GetResultValueAsync(args)).Value, ((Literal)await operands[1].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ModuloResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(((Literal)await operands[0].GetResultValueAsync(args)).Value % ((Literal)await operands[1].GetResultValueAsync(args)).Value);
		}
	}
}
