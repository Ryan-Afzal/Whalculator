using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class InvalidEquationException : Exception {

		public InvalidEquationException(ErrorCode errorCode) : this(errorCode, "?") {

		}

		public InvalidEquationException(ErrorCode errorCode, string data) {
			ErrorCode = errorCode;
			Data = data;
		}

		public ErrorCode ErrorCode { get; }
		public new string Data { get; }

		public string GetMessageFromErrorCode() {
			return ErrorCode switch
			{
				ErrorCode.NonexistentVariable => $"Nonexistent Variable {Data[0]}",
				ErrorCode.NonexistentFunction => $"Nonexistent Function {Data[0]}",
				ErrorCode.MismatchedParentheses => "Mismatched Parentheses",
				ErrorCode.RecursiveFunction => "Recursive Function",
				ErrorCode.InvalidNumArguments => "Invalid Function Argument Number",
				ErrorCode.InvalidArguments => "Invalid Function Arguments",
				ErrorCode.MultivariableDifferentiation => $"Cannot Take Implicit Derivative of Multi-Variable Function {Data[0]}",
				ErrorCode.MismatchedArgumentType => "Mismatched Argument Type",
				_ => "Unknown Equation Error",
			};
		}

	}
}
