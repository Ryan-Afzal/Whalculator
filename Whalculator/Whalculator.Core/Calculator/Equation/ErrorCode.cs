using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public enum ErrorCode {
		MismatchedParentheses,
		NonexistentVariable,
		NonexistentFunction,
		RecursiveFunction,
		InvalidNumArguments,
		InvalidArguments,
		MultivariableDifferentiation,
		MismatchedArgumentType,
		ConstantRedefinition,
		BuiltinFunctionRedefinition
	}
}
