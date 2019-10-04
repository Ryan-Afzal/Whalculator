using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public delegate ISolvable BuiltinFunctionExactValueOperation(ISolvable[] operands);

	public delegate double BuiltinFunctionDoubleValueOperation(ISolvable[] operands);

	public struct BuiltinFunctionOperation {
		
		private BuiltinFunctionOperation(string name, int numargs, BuiltinFunctionExactValueOperation exactValueOperation, BuiltinFunctionDoubleValueOperation doubleValueOperation) {
			Name = name;
			NumArgs = numargs;
			ExactValueOperation = exactValueOperation;
			DoubleValueOperation = doubleValueOperation;
		}

		public string Name { get; private set; }
		public int NumArgs { get; private set; }
		public BuiltinFunctionExactValueOperation ExactValueOperation { get; private set; }
		public BuiltinFunctionDoubleValueOperation DoubleValueOperation { get; private set; }

	}
}
