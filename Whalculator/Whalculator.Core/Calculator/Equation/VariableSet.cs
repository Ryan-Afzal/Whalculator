using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class VariableSet : IVariableSet {

		private struct VariableInfo {
			public string Name { get; internal set; }
			public bool IsConstant { get; internal set; }
			public ISolvable Value { get; internal set; }
		}

		private readonly IDictionary<string, VariableInfo> variables;

		public ISolvable this[string name] {
			get {
				return this.variables[name].Value.Clone();
			}
		}

		public VariableSet() {
			this.variables = new Dictionary<string, VariableInfo>();

			this.AddConstant("pi", new Literal(Math.PI));
			this.AddConstant("e", new Literal(Math.E));
			//this.AddConstant("i", );
		}

		private void AddConstant(string name, ISolvable value) {
			this.variables[name] = new VariableInfo() {
				Name = name,
				IsConstant = true,
				Value = value
			};
		}

		public bool SetVariable(string name, ISolvable value) {
			if (this.IsVariable(name) && this.IsConstant(name)) {
				return false;
			}

			this.variables[name] = new VariableInfo() {
				Name = name,
				IsConstant = false,
				Value = value.Clone()
			};
			return true;
		}

		public bool IsVariable(string name) {
			return this.variables.ContainsKey(name);
		}

		public bool IsConstant(string name) {
			return this.variables[name].IsConstant;
		}
	}
}
