using System;
using System.Collections;
using System.Collections.Generic;

namespace Whalculator.Core.Calculator.Equation {

	public delegate ISolvable ExactValueOperation(ISolvable[] operands);

	public delegate double DoubleValueOperation(ISolvable[] operands);

	public struct Operation {

		public static readonly Operation AddOperation = new Operation();

		private Operation(ExactValueOperation exactValueOperation, DoubleValueOperation doubleValueOperation) {
			ExactValueOperation = exactValueOperation;
			DoubleValueOperation = doubleValueOperation;
		}

		public ExactValueOperation ExactValueOperation { get; private set; }
		public DoubleValueOperation DoubleValueOperation { get; private set; }

	}

}