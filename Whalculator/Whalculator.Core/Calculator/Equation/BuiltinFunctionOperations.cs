using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	public static class BuiltinFunctionOperations {

		public static BuiltinFunctionOperation AbsOperation = new BuiltinFunctionOperation("abs", 1, AbsExactValueOperation, AbsResultValueOperation);
		public static BuiltinFunctionOperation CeilOperation = new BuiltinFunctionOperation("ceil", 1, CeilExactValueOperation, CeilResultValueOperation);
		public static BuiltinFunctionOperation FloorOperation = new BuiltinFunctionOperation("floor", 1, FloorExactValueOperation, FloorResultValueOperation);
		public static BuiltinFunctionOperation RootOperation = new BuiltinFunctionOperation("root", 2, RootExactValueOperation, RootResultValueOperation);
		public static BuiltinFunctionOperation SqrtOperation = new BuiltinFunctionOperation("sqrt", 1, SqrtExactValueOperation, SqrtResultValueOperation);
		public static BuiltinFunctionOperation LogOperation = new BuiltinFunctionOperation("log", 2, LogExactValueOperation, LogResultValueOperation);
		public static BuiltinFunctionOperation LnOperation = new BuiltinFunctionOperation("ln", 1, LnExactValueOperation, LnResultValueOperation);

		public static BuiltinFunctionOperation ToDegreesOperation = new BuiltinFunctionOperation("deg", 1, ToDegreesExactValueOperation, ToDegreesResultValueOperation);
		public static BuiltinFunctionOperation ToRadiansOperation = new BuiltinFunctionOperation("rad", 1, ToRadiansExactValueOperation, ToRadiansResultValueOperation);

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

		public static BuiltinFunctionOperation SortOperation = new BuiltinFunctionOperation("sort", 1, SortExactValueOperation, SortResultValueOperation);
		public static BuiltinFunctionOperation ModeOperation = new BuiltinFunctionOperation("mode", 1, ModeExactValueOperation, ModeResultValueOperation);
		public static BuiltinFunctionOperation MedianOperation = new BuiltinFunctionOperation("med", 1, MedianExactValueOperation, MedianResultValueOperation);
		public static BuiltinFunctionOperation MeanOperation = new BuiltinFunctionOperation("mean", 1, MeanExactValueOperation, MeanResultValueOperation);
		public static BuiltinFunctionOperation StandardDeviationOperation = new BuiltinFunctionOperation("stddev", 1, StandardDeviationExactValueOperation, StandardDeviationResultValueOperation);

		public static BuiltinFunctionOperation VectorFromMagnitudeAndDirectionOperation = new BuiltinFunctionOperation("vector", 2, VectorFromMagnitudeAndDirectionExactValueOperation, VectorFromMagnitudeAndDirectionResultValueOperation);
		public static BuiltinFunctionOperation MagnitudeFromVectorOperation = new BuiltinFunctionOperation("mag", 1, MagnitudeFromVectorExactValueOperation, MagnitudeFromVectorResultValueOperation);

		// Regular functions

		public static async Task<ISolvable> AbsExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Abs(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> AbsResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Abs(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> CeilExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Ceiling(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CeilResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Ceiling(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> FloorExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Floor(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> FloorResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Floor(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> RootExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)await operands[0].GetResultValueAsync(args)).Value, 1 / ((Literal)await operands[1].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> RootResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Pow(((Literal)await operands[0].GetResultValueAsync(args)).Value, 1 / ((Literal)await operands[1].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> SqrtExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sqrt(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> SqrtResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sqrt(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> LogExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)await operands[1].GetResultValueAsync(args)).Value) / Math.Log(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> LogResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)await operands[1].GetResultValueAsync(args)).Value) / Math.Log(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> LnExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> LnResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Log(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		// Angle functions

		public static async Task<ISolvable> ToDegreesExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Operator(Operations.DivideOperation,
				 new Operator(Operations.MultiplyOperation,
					 new Literal(180),
					 operands[0].Clone()
					 ),
				 new Variable("pi")
				 );
		}

		public static async Task<IResult> ToDegreesResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await ToDegreesExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

		public static async Task<ISolvable> ToRadiansExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Operator(Operations.DivideOperation,
				new Operator(Operations.MultiplyOperation,
					new Variable("pi"),
					operands[0].Clone()
					),
				new Literal(180)
				);
		}

		public static async Task<IResult> ToRadiansResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await ToRadiansExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

		// Trig functions

		public static async Task<ISolvable> SineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> SineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> CosineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CosineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> TangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Tan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> TangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Tan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> SecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> SecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> CosecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Sin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CosecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Sin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> CotangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Tan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> CotangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(1 / Math.Tan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> ArcsineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Asin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArcsineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Asin(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> ArccosineExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArccosineResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Cos(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> ArctangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArctangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> ArcsecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Acos(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArcsecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Acos(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> ArccosecantExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArccosecantResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Sin(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<ISolvable> ArccotangentExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		public static async Task<IResult> ArccotangentResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return new Literal(Math.Atan(1 / ((Literal)await operands[0].GetResultValueAsync(args)).Value));
		}

		//List Functions

		public static async Task<ISolvable> SortExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			ISolvable[] elements = await ((List)await operands[0].GetExactValueAsync(args)).EvaluateOperandsExact(args);
			Array.Sort(elements);
			return new List(elements);
		}

		public static async Task<IResult> SortResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			ISolvable[] elements = await ((List)operands[0]).EvaluateOperandsResult(args);
			Array.Sort(elements);
			return new List(elements);
		}

		public static async Task<ISolvable> ModeExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			ISolvable[] elements = await ((List)await operands[0].GetExactValueAsync(args)).EvaluateOperandsExact(args);
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

		public static async Task<ISolvable> MedianExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			ISolvable[] elements = await ((List)await operands[0].GetExactValueAsync(args)).EvaluateOperandsExact(args);
			Array.Sort(elements);
			return elements[elements.Length / 2];
		}

		public static async Task<IResult> MedianResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await MedianExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

		public static async Task<ISolvable> MeanExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			List node = (List)await operands[0].GetExactValueAsync(args);
			return await new Operator(Operations.DivideOperation,
					new Operator(Operations.AddOperation, await node.EvaluateOperandsExact(args)),
					new Literal(node.operands.Length)
				).GetExactValueAsync(args);
		}

		public static async Task<IResult> MeanResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await MeanExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

		public static async Task<ISolvable> StandardDeviationExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			List node = (List)await operands[0].GetExactValueAsync(args);
			
			if (node.operands.Length < 2) {
				return new Literal(0);
			}

			ISolvable minusMean = new Operator(Operations.MultiplyOperation, 
				new Literal(-1), 
				await MeanExactValueOperation(new ISolvable[] { node }, args)
			);

			ISolvable[] sum = await node.EvaluateOperandsExact(args);

			for (int i = 0; i < sum.Length; i++) {
				sum[i] = new Operator(Operations.ExponateOperation,
					new Operator(Operations.AddOperation, sum[i].Clone(), minusMean.Clone()),
					new Literal(2)
				);
			}

			return await new BuiltinFunction(SqrtOperation, new Operator(Operations.DivideOperation,
					new Operator(Operations.AddOperation, sum),
					new Literal(node.operands.Length - 1)
				)).GetExactValueAsync(args);
		}

		public static async Task<IResult> StandardDeviationResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await StandardDeviationExactValueOperation(operands, args)).GetResultValueAsync(args);
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
			return await output.GetExactValueAsync(args);
		}

		public static async Task<IResult> MagnitudeFromVectorResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args) {
			return await (await MagnitudeFromVectorExactValueOperation(operands, args)).GetResultValueAsync(args);
		}

	}
}
