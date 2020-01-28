using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Arguments passed during expression evaluation.
	/// </summary>
	public struct ExpressionEvaluationArgs {
		public VariableSet VariableSet { get; set; }
		public FunctionSet FunctionSet { get; set; }
		public FunctionArgumentArgs Args { get; set; }
		public bool IsDegrees { get; set; }
	}
}
