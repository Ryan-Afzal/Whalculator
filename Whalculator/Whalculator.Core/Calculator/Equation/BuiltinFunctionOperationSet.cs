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
			this.AddOperation(BuiltinFunctionOperations.AbsOperation);
			this.AddOperation(BuiltinFunctionOperations.CeilOperation);
			this.AddOperation(BuiltinFunctionOperations.FloorOperation);

			this.AddOperation(BuiltinFunctionOperations.SineOperation);
		}

		private void AddOperation(BuiltinFunctionOperation value) {
			this.operations[value.Name] = value;
		}

		public bool IsBuiltinFunctionOperation(string name) {
			return this.operations.ContainsKey(name);
		}
	}
}
