using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IViewableVariableSet {
		public ISolvable this[string name] { get; }
		public bool IsVariable(string name);
		public IEnumerable<(string, IResult)> GetAllVariables();
	}
}
