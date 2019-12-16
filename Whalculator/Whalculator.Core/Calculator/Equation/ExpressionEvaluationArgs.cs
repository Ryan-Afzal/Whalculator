using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Arguments passed during expression evaluation.
	/// </summary>
	public struct ExpressionEvaluationArgs {
		public IVariableSet VariableSet { get; set; }
		public IFunctionSet FunctionSet { get; set; }
		public FunctionArgumentArgs Args { get; set; }
		public bool IsDegrees { get; set; }
	}
}
