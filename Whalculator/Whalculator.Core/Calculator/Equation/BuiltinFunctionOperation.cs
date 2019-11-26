using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public delegate ISolvable BuiltinFunctionExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	public delegate IResult BuiltinFunctionResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	public struct BuiltinFunctionOperation {
		
		internal BuiltinFunctionOperation(string name, int numargs, BuiltinFunctionExactValueOperation exactValueOperation, BuiltinFunctionResultValueOperation resultValueOperation) {
			Name = name;
			NumArgs = numargs;
			ExactValueOperation = exactValueOperation;
			ResultValueOperation = resultValueOperation;
		}

		public string Name { get; private set; }
		public int NumArgs { get; private set; }
		public BuiltinFunctionExactValueOperation ExactValueOperation { get; private set; }
		public BuiltinFunctionResultValueOperation ResultValueOperation { get; private set; }

	}
}
