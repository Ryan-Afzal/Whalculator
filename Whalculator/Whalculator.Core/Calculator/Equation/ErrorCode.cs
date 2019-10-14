using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public enum ErrorCode {
		MISMATCHED_PARENTHESES,
		NONEXISTENT_VARIABLE,
		NONEXISTENT_FUNCTION,
		RECURSIVE_FUNCTION
	}
}
