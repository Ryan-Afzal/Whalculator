using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public struct FunctionInfo {
		public ISolvable Function { get; set; }
		public string Name { get; set; }
		public string Head { get; set; }
		public Dictionary<string, int> ArgNames { get; set; }
	}
}
