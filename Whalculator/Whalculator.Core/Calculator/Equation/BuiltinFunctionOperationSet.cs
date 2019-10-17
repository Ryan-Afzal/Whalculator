using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class BuiltinFunctionOperationSet : IBuiltinFunctionOperationSet {

		private readonly IDictionary<string, BuiltinFunctionOperation> operations;

		public BuiltinFunctionOperation this[string name] {
			get {
				return this.operations[name];
			}
		}

		public BuiltinFunctionOperationSet() {
			this.operations = new Dictionary<string, BuiltinFunctionOperation>();
		}

		private bool SetVariable(string name, BuiltinFunctionOperation value) {
			this.operations[name] = value;

			return true;
		}

		public bool IsBuiltinFunctionOperation(string name) {
			return this.operations.ContainsKey(name);
		}
	}
}
