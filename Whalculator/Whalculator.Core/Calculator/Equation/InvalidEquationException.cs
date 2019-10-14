using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class InvalidEquationException : Exception {

		public InvalidEquationException(ErrorCode errorCode) {
			ErrorCode = errorCode;
		}

		public ErrorCode ErrorCode { get; }

	}
}
