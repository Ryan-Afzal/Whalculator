using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class BuiltinFunctionOperations {

		public static BuiltinFunctionOperation AbsOperation = new BuiltinFunctionOperation("abs", 1, AbsExactValueOperation, AbsResultValueOperation);
		public static BuiltinFunctionOperation CeilOperation = new BuiltinFunctionOperation("ceil", 1, CeilExactValueOperation, CeilResultValueOperation);
		public static BuiltinFunctionOperation FloorOperation = new BuiltinFunctionOperation("floor", 1, FloorExactValueOperation, FloorResultValueOperation);
		public static BuiltinFunctionOperation RootOperation = new BuiltinFunctionOperation("root", 2, RootExactValueOperation, RootResultValueOperation);
		public static BuiltinFunctionOperation SqrtOperation = new BuiltinFunctionOperation("sqrt", 1, SqrtExactValueOperation, SqrtResultValueOperation);
		public static BuiltinFunctionOperation LogOperation = new BuiltinFunctionOperation("log", 2, LogExactValueOperation, LogResultValueOperation);
		public static BuiltinFunctionOperation LnOperation = new BuiltinFunctionOperation("ln", 1, LnExactValueOperation, LnResultValueOperation);

		public static BuiltinFunctionOperation SineOperation = new BuiltinFunctionOperation("sin", 1, SineExactValueOperation, SineResultValueOperation);
		public static BuiltinFunctionOperation CosineOperation = new BuiltinFunctionOperation("cos", 1, CosineExactValueOperation, CosineResultValueOperation);
		public static BuiltinFunctionOperation TangentOperation = new BuiltinFunctionOperation("tan", 1, TangentExactValueOperation, TangentResultValueOperation);
		public static BuiltinFunctionOperation SecantOperation = new BuiltinFunctionOperation("sec", 1, SecantExactValueOperation, SecantResultValueOperation);
		public static BuiltinFunctionOperation CosecantOperation = new BuiltinFunctionOperation("csc", 1, CosecantExactValueOperation, CosecantResultValueOperation);
		public static BuiltinFunctionOperation CotangentOperation = new BuiltinFunctionOperation("cot", 1, CotangentExactValueOperation, CotangentResultValueOperation);
		public static BuiltinFunctionOperation ArcsineOperation = new BuiltinFunctionOperation("arcsin", 1, ArcsineExactValueOperation, ArcsineResultValueOperation);
		public static BuiltinFunctionOperation ArccosineOperation = new BuiltinFunctionOperation("arccos", 1, ArccosineExactValueOperation, ArccosineResultValueOperation);
		public static BuiltinFunctionOperation ArctangentOperation = new BuiltinFunctionOperation("arctan", 1, ArctangentExactValueOperation, ArctangentResultValueOperation);
		public static BuiltinFunctionOperation ArcsecantOperation = new BuiltinFunctionOperation("arcsec", 1, ArcsecantExactValueOperation, ArcsecantResultValueOperation);
		public static BuiltinFunctionOperation ArccosecantOperation = new BuiltinFunctionOperation("arccsc", 1, ArccosecantExactValueOperation, ArccosecantResultValueOperation);
		public static BuiltinFunctionOperation ArccotangentOperation = new BuiltinFunctionOperation("arccot", 1, ArccotangentExactValueOperation, ArccotangentResultValueOperation);

		public static BuiltinFunctionOperation VectorFromMagnitudeAndDirectionOperation = new BuiltinFunctionOperation("vector", 2, VectorFromMagnitudeAndDirectionExactValueOperation, VectorFromMagnitudeAndDirectionResultValueOperation);
		public static BuiltinFunctionOperation MagnitudeFromVectorOperation = new BuiltinFunctionOperation("mag", 1, MagnitudeFromVectorExactValueOperation, MagnitudeFromVectorResultValueOperation);
		
		// Regular functions

		public static ISolvable AbsExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Abs(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult AbsResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Abs(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable CeilExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Ceiling(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult CeilResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Ceiling(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable FloorExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Floor(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult FloorResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Floor(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable RootExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)operands[0].GetResultValue(args)).Value, 1 / ((Literal)operands[1].GetResultValue(args)).Value));
		}

		public static IResult RootResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)operands[0].GetResultValue(args)).Value, 1 / ((Literal)operands[1].GetResultValue(args)).Value));
		}

		public static ISolvable SqrtExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sqrt(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult SqrtResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sqrt(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable LogExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)operands[1].GetResultValue(args)).Value) / Math.Log(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult LogResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)operands[1].GetResultValue(args)).Value) / Math.Log(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable LnExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult LnResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)operands[0].GetResultValue(args)).Value));
		}

		// Trig functions

		public static ISolvable SineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult SineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable CosineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult CosineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable TangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Tan(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult TangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Tan(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable SecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Cos(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult SecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Cos(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable CosecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Sin(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult CosecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Sin(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable CotangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Tan(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult CotangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Tan(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable ArcsineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Asin(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult ArcsineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Asin(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable ArccosineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult ArccosineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable ArctangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult ArctangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable ArcsecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Acos(1 / ((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult ArcsecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Acos(1 / ((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable ArccosecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(1 / ((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult ArccosecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(1 / ((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static ISolvable ArccotangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(1 / ((Literal)operands[0].GetResultValue(args)).Value));
		}

		public static IResult ArccotangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(1 / ((Literal)operands[0].GetResultValue(args)).Value));
		}

		//List Functions



		//Vector Functions

		public static ISolvable VectorFromMagnitudeAndDirectionExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			var mag = operands[0];
			var dir = operands[1];

			return new Vector(
				new Operator(Operations.MultiplyOperation,
					mag,
					new BuiltinFunction(CosineOperation, dir)
				),
				new Operator(Operations.MultiplyOperation,
					mag,
					new BuiltinFunction(SineOperation, dir)
				)
			);
		}

		public static IResult VectorFromMagnitudeAndDirectionResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return VectorFromMagnitudeAndDirectionExactValueOperation(operands, args).GetResultValue(args);
		}

		public static ISolvable MagnitudeFromVectorExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			Vector v = (operands[0].GetResultValue(args)) as Vector;

			ISolvable[] terms = new ISolvable[v.Dimensions];

			for (int i = 0; i < terms.Length; i++) {
				terms[i] = new Operator(Operations.ExponateOperation, v.Components[i].Clone(), new Literal(2));
			}

			var output = new BuiltinFunction(SqrtOperation,	new Operator(Operations.AddOperation, terms));
			return output.GetExactValue(args);
		}

		public static IResult MagnitudeFromVectorResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return MagnitudeFromVectorExactValueOperation(operands, args).GetResultValue(args);
		}

	}
}
