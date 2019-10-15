using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IVariableSet {
		bool SetVariable(string name, ISolvable value);
		bool IsVariable(string name);
		bool IsConstant(string name);
		ISolvable this[string name] { get; }
	}
}
