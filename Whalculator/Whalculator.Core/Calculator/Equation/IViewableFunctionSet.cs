using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IViewableFunctionSet {
		public bool ContainsFunction(string name);
		public FunctionInfo GetFunction(string name);
		public IEnumerable<FunctionInfo> GetAllFunctions();
	}
}
