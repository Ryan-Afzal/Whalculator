using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IBuiltinFunctionOperationSet {
		bool IsBuiltinFunctionOperation(string name);
		BuiltinFunctionOperation this[string name] { get; }
	}
}
