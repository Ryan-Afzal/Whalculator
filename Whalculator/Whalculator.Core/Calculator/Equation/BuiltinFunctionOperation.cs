using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {

	public delegate Task<IResult> BuiltinFunctionResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	/// <summary>
	/// The operation component of a builtin function. It is designed not to change.
	/// </summary>
	public struct BuiltinFunctionOperation {
		
		internal BuiltinFunctionOperation(string name, int numargs, BuiltinFunctionResultValueOperation resultValueOperation) {
			Name = name;
			NumArgs = numargs;
			ResultValueOperation = resultValueOperation;
		}

		public string Name { get; private set; }
		public int NumArgs { get; private set; }
		public BuiltinFunctionResultValueOperation ResultValueOperation { get; private set; }

	}
}
