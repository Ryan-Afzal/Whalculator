using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class InvalidEquationException : Exception {

		public InvalidEquationException(int errorCode) {
			ErrorCode = errorCode;
		}

		public int ErrorCode { get; }

	}
}
