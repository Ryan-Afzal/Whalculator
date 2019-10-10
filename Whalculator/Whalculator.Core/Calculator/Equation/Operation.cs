using System;
using System.Collections;
using System.Collections.Generic;

namespace Whalculator.Core.Calculator.Equation {

	public delegate ISolvable ExactValueOperation(ISolvable[] operands);

	public delegate double DoubleValueOperation(ISolvable[] operands);

	public struct Operation {


		private Operation(ExactValueOperation exactValueOperation, DoubleValueOperation doubleValueOperation, int order, char name) {
			ExactValueOperation = exactValueOperation;
			DoubleValueOperation = doubleValueOperation;
			Order = order;
			Name = name;
		}

		public ExactValueOperation ExactValueOperation { get; }
		public DoubleValueOperation DoubleValueOperation { get; }

		public int Order { get; }

		public char Name { get; }

	}

}