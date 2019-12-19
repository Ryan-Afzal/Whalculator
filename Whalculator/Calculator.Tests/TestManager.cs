using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	public static class TestManager {

		public static readonly IOperatorSet Operators = new OperatorSet();
		public static readonly IVariableSet Variables = new VariableSet();
		public static readonly IBuiltinFunctionOperationSet BuiltinFunctionOperations = new BuiltinFunctionOperationSet();
		public static readonly IFunctionSet Functions = new FunctionSet();

		public static ISolvable GetSolvableFromText(string text) {
			return ExpressionBuilder.GetSolvable(text, new GenerationArgs() {
				OperatorSet = Operators,
				BuiltinFunctionOperationSet = BuiltinFunctionOperations
			});
		}

	}
}
