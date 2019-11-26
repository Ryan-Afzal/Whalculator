﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Whalculator.Core.Calculator.Equation {

	public delegate ISolvable ExactValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	public delegate IResult ResultValueOperation(ISolvable[] operands, ExpressionEvaluationArgs args);

	public struct Operation {

		internal Operation(ExactValueOperation exactValueOperation, ResultValueOperation resultValueOperation, int order, char name, bool sortedOperands) {
			ExactValueOperation = exactValueOperation;
			ResultValueOperation = resultValueOperation;
			Order = order;
			Name = name;
			SortedOperands = sortedOperands;
		}

		public ExactValueOperation ExactValueOperation { get; }
		public ResultValueOperation ResultValueOperation { get; }

		public int Order { get; }

		public char Name { get; }

		public bool SortedOperands { get; }

	}

}