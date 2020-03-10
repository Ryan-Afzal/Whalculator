using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class VariableSet : IViewableVariableSet {

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
			this.AddConstant("π", new Literal(Math.PI));

			this.AddConstant("tau", new Literal(Math.PI * 2));
			this.AddConstant("τ", new Literal(Math.PI * 2));

			this.AddConstant("e", new Literal(Math.E));
		}

		private void AddConstant(string name, ISolvable value) {
			this.variables[name] = new VariableInfo() {
				Name = name,
				IsConstant = true,
				Value = value
			};
		}

		public bool SetVariable(string name, ISolvable value) {
			if (double.TryParse(name, out double testVar)) {
				return false;
			}

			if (this.IsVariable(name) && this.IsConstant(name)) {
				return false;
			}

			if (char.IsDigit(name[0])) {
				return false;
			}

			for (int i = 0; i < name.Length; i++) {
				if (!char.IsLetterOrDigit(name[i]) && name[i] != '_') {
					return false;
				}
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
			if (!this.IsVariable(name)) {
				return false;
			}

			return this.variables[name].IsConstant;
		}

		public IEnumerable<(string, IResult)> GetAllVariables() {
			foreach (var item in this.variables.Values) {
				yield return (item.Name, (IResult)item.Value.Clone());
			}
		}

		public string GetVariable(string name) {
			return this[name].GetEquationString();
		}
	}
}
