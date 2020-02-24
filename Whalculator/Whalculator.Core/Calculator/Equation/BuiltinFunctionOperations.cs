using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	public static class BuiltinFunctionOperations {

		public static BuiltinFunctionOperation AbsOperation = new BuiltinFunctionOperation("abs", 1, AbsResultValueOperation);
		public static BuiltinFunctionOperation CeilOperation = new BuiltinFunctionOperation("ceil", 1, CeilResultValueOperation);
		public static BuiltinFunctionOperation FloorOperation = new BuiltinFunctionOperation("floor", 1, FloorResultValueOperation);
		public static BuiltinFunctionOperation RootOperation = new BuiltinFunctionOperation("root", 2, RootResultValueOperation);
		public static BuiltinFunctionOperation SqrtOperation = new BuiltinFunctionOperation("sqrt", 1, SqrtResultValueOperation);
		public static BuiltinFunctionOperation LogOperation = new BuiltinFunctionOperation("log", 2, LogResultValueOperation);
		public static BuiltinFunctionOperation LnOperation = new BuiltinFunctionOperation("ln", 1, LnResultValueOperation);

		public static BuiltinFunctionOperation ToDegreesOperation = new BuiltinFunctionOperation("deg", 1, ToDegreesResultValueOperation);
		public static BuiltinFunctionOperation ToRadiansOperation = new BuiltinFunctionOperation("rad", 1, ToRadiansResultValueOperation);

		public static BuiltinFunctionOperation SineOperation = new BuiltinFunctionOperation("sin", 1, SineResultValueOperation);
		public static BuiltinFunctionOperation CosineOperation = new BuiltinFunctionOperation("cos", 1, CosineResultValueOperation);
		public static BuiltinFunctionOperation TangentOperation = new BuiltinFunctionOperation("tan", 1, TangentResultValueOperation);
		public static BuiltinFunctionOperation SecantOperation = new BuiltinFunctionOperation("sec", 1, SecantResultValueOperation);
		public static BuiltinFunctionOperation CosecantOperation = new BuiltinFunctionOperation("csc", 1, CosecantResultValueOperation);
		public static BuiltinFunctionOperation CotangentOperation = new BuiltinFunctionOperation("cot", 1, CotangentResultValueOperation);
		public static BuiltinFunctionOperation ArcsineOperation = new BuiltinFunctionOperation("arcsin", 1, ArcsineResultValueOperation);
		public static BuiltinFunctionOperation ArccosineOperation = new BuiltinFunctionOperation("arccos", 1, ArccosineResultValueOperation);
		public static BuiltinFunctionOperation ArctangentOperation = new BuiltinFunctionOperation("arctan", 1, ArctangentResultValueOperation);
		public static BuiltinFunctionOperation ArcsecantOperation = new BuiltinFunctionOperation("arcsec", 1, ArcsecantResultValueOperation);
		public static BuiltinFunctionOperation ArccosecantOperation = new BuiltinFunctionOperation("arccsc", 1, ArccosecantResultValueOperation);
		public static BuiltinFunctionOperation ArccotangentOperation = new BuiltinFunctionOperation("arccot", 1, ArccotangentResultValueOperation);

		public static BuiltinFunctionOperation SortOperation = new BuiltinFunctionOperation("sort", 1, SortResultValueOperation);
		public static BuiltinFunctionOperation ModeOperation = new BuiltinFunctionOperation("mode", 1, ModeResultValueOperation);
		public static BuiltinFunctionOperation MedianOperation = new BuiltinFunctionOperation("med", 1, MedianResultValueOperation);
		public static BuiltinFunctionOperation MeanOperation = new BuiltinFunctionOperation("mean", 1, MeanResultValueOperation);
		public static BuiltinFunctionOperation StandardDeviationOperation = new BuiltinFunctionOperation("stddev", 1, StandardDeviationResultValueOperation);

		public static BuiltinFunctionOperation VectorFromMagnitudeAndDirectionOperation = new BuiltinFunctionOperation("vector", 2, VectorFromMagnitudeAndDirectionResultValueOperation);
		public static BuiltinFunctionOperation MagnitudeFromVectorOperation = new BuiltinFunctionOperation("mag", 1, MagnitudeFromVectorResultValueOperation);

		// Regular functions

		public static async Task<IResult> AbsResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Abs(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CeilResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Ceiling(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> FloorResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Floor(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> RootResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)await operands[0].GetResultValueAsync(args)).Value, 1 / ((Literal)await operands[1].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> SqrtResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sqrt(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> LogResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)await operands[1].GetResultValueAsync(args)).Value) / Math.Log(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> LnResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		// Angle functions

		public static async Task<IResult> ToDegreesResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await new Operator(Operations.DivideOperation,
				 new Operator(Operations.MultiplyOperation,
					 new Literal(180),
					 operands[0].Clone()
					 ),
				 new Variable("pi")
				 ).GetResultValueAsync(args);
		}

		public static async Task<IResult> ToRadiansResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await new Operator(Operations.DivideOperation,
				new Operator(Operations.MultiplyOperation,
					new Variable("pi"),
					operands[0].Clone()
					),
				new Literal(180)
				).GetResultValueAsync(args);
		}

		// Trig functions

		public static async Task<IResult> SineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CosineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> TangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Tan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> SecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CosecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Sin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CotangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Tan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArcsineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Asin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArccosineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArctangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArcsecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Acos(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArccosecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArccotangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		//List Functions

		public static async Task<IResult> SortResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			ISolvable[] elements = await ((List)operands[0]).EvaluateOperandsResult(args);
			Array.Sort(elements);
			return new List(elements);
		}

		public static async Task<ISolvable> ModeExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			var elements = await ((List)operands[0].Clone()).EvaluateOperandsResult(args);
			Dictionary<string, (ISolvable, int)> dict = new Dictionary<string, (ISolvable, int)>();
			
			for (int i = 0; i < elements.Length; i++) {
				string str = elements[i].GetEquationString();
				if (dict.ContainsKey(str)) {
					(ISolvable a, int b) = dict[str];
					dict[str] = (a, b + 1);
				} else {
					dict.Add(str, (elements[i], 1));
				}
			}

			ISolvable? output = null;
			int count = 0;
			foreach (var value in dict.Values) {
				if (value.Item2 > count) {
					output = value.Item1;
					count = value.Item2;
				}
			}

			return output!;
		}

		public static async Task<IResult> ModeResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await ModeExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

		public static async Task<IResult> MedianResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			ISolvable[] elements = await ((List)operands[0].Clone()).EvaluateOperandsResult(args);
			Array.Sort(elements);
			return await elements[elements.Length / 2].GetResultValueAsync(args);
		}

		public static async Task<IResult> MeanResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			List node = (List)operands[0].Clone();
			return await new Operator(Operations.DivideOperation,
					new Operator(Operations.AddOperation, await node.EvaluateOperandsResult(args)),
					new Literal(node.operands.Length)
				).GetResultValueAsync(args);
		}

		public static async Task<IResult> StandardDeviationResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			List node = (List)operands[0].Clone();
			
			if (node.operands.Length < 2) {
				return new Literal(0);
			}

			ISolvable minusMean = new Operator(Operations.MultiplyOperation, 
				new Literal(-1), 
				await MeanResultValueOperation(new ISolvable[] { node }, args)
			);

			ISolvable[] sum = await node.EvaluateOperandsResult(args);

			for (int i = 0; i < sum.Length; i++) {
				sum[i] = new Operator(Operations.ExponateOperation,
					new Operator(Operations.AddOperation, sum[i].Clone(), minusMean.Clone()),
					new Literal(2)
				);
			}

			return await new BuiltinFunction(SqrtOperation, new Operator(Operations.DivideOperation,
					new Operator(Operations.AddOperation, sum),
					new Literal(node.operands.Length - 1)
				)).GetResultValueAsync(args);
		}

		//Vector Functions

		public static async Task<ISolvable> VectorFromMagnitudeAndDirectionExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
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

		public static async Task<IResult> VectorFromMagnitudeAndDirectionResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await VectorFromMagnitudeAndDirectionExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

		public static async Task<ISolvable> MagnitudeFromVectorExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			Vector v = (Vector)await operands[0].GetResultValueAsync(args);

			ISolvable[] terms = new ISolvable[v.Dimensions];

			for (int i = 0; i < terms.Length; i++) {
				terms[i] = new Operator(Operations.ExponateOperation, v.operands[i].Clone(), new Literal(2));
			}

			var output = new BuiltinFunction(SqrtOperation,	new Operator(Operations.AddOperation, terms));
			return output;
		}

		public static async Task<IResult> MagnitudeFromVectorResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await MagnitudeFromVectorExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

	}
}
