using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IViewableBuiltinFunctionOperationSet {
		public BuiltinFunctionOperation this[string name] { get; }
		public bool IsBuiltinFunctionOperation(string name);
		public IEnumerable<BuiltinFunctionOperation> GetAllBuiltinFunctionOperations();
	}
}
