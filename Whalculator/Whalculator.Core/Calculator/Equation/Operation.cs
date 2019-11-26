using System;
using System.Collections;
using System.Collections.Generic;

namespace Whalculator.Core.Calculator.Equation {

	public delegate ISolvable ExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	public delegate double DoubleValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	public struct Operation {

		internal Operation(ExactValueOperation exactValueOperation, DoubleValueOperation doubleValueOperation, int order, char name, bool sortedOperands) {
			ExactValueOperation = exactValueOperation;
			DoubleValueOperation = doubleValueOperation;
			Order = order;
			Name = name;
			SortedOperands = sortedOperands;
		}

		public ExactValueOperation ExactValueOperation { get; }
		public DoubleValueOperation DoubleValueOperation { get; }

		public int Order { get; }

		public char Name { get; }

		public bool SortedOperands { get; }

	}

}