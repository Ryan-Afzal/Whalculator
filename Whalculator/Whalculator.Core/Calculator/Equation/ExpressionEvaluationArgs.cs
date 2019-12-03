using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public struct ExpressionEvaluationArgs {
		public IVariableSet VariableSet { get; set; }
		public IFunctionSet FunctionSet { get; set; }
		public FunctionArgumentArgs Args { get; set; }
	}
}
