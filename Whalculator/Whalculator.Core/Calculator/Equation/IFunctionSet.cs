using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IFunctionSet {
		bool ContainsFunction(string name);
		bool SetFunction(string name, FunctionInfo value);
		FunctionInfo GetFunction(string name);
	}
}
