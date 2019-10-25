using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class MalformedEquationException : Exception {

		public MalformedEquationException(ErrorCode errorCode) {
			ErrorCode = errorCode;
		}

		public ErrorCode ErrorCode { get; }

	}
}
