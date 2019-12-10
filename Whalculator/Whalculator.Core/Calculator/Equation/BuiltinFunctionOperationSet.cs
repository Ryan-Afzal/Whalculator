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
			this.AddOperation(BuiltinFunctionOperations.RootOperation);
			this.AddOperation(BuiltinFunctionOperations.SqrtOperation);
			this.AddOperation(BuiltinFunctionOperations.LogOperation);
			this.AddOperation(BuiltinFunctionOperations.LnOperation);

			this.AddOperation(BuiltinFunctionOperations.SineOperation);
			this.AddOperation(BuiltinFunctionOperations.CosineOperation);
			this.AddOperation(BuiltinFunctionOperations.TangentOperation);
			this.AddOperation(BuiltinFunctionOperations.SecantOperation);
			this.AddOperation(BuiltinFunctionOperations.CosecantOperation);
			this.AddOperation(BuiltinFunctionOperations.CotangentOperation);
			this.AddOperation(BuiltinFunctionOperations.ArcsineOperation);
			this.AddOperation(BuiltinFunctionOperations.ArccosineOperation);
			this.AddOperation(BuiltinFunctionOperations.ArctangentOperation);
			this.AddOperation(BuiltinFunctionOperations.ArcsecantOperation);
			this.AddOperation(BuiltinFunctionOperations.ArccosecantOperation);
			this.AddOperation(BuiltinFunctionOperations.ArccotangentOperation);

			this.AddOperation(BuiltinFunctionOperations.VectorFromMagnitudeAndDirectionOperation);
			this.AddOperation(BuiltinFunctionOperations.MagnitudeFromVectorOperation);
		}

		private void AddOperation(BuiltinFunctionOperation value) {
			this.operations[value.Name] = value;
		}

		public bool IsBuiltinFunctionOperation(string name) {
			return this.operations.ContainsKey(name);
		}
	}
}
