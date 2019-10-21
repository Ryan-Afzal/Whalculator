﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class OperatorSet : IOperatorSet {

		private readonly IDictionary<char, Operation> dictionary;

		public OperatorSet() {
			this.dictionary = new Dictionary<char, Operation>();
			this.AddOperation(new Operation(Operations.AddExactValueOperation, Operations.AddDoubleValueOperation, 0, '+'));
		}

		private void AddOperation(Operation operation) {
			this.dictionary.Add(operation.Name, operation);
		}

		public bool IsOperator(char c) {
			return this.dictionary.ContainsKey(c);
		}

		public Operation GetOperation(char c) {
			return this.dictionary[c];
		}

	}
}
