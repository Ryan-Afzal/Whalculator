using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public struct FunctionInfo {
		public ISolvable Function { get; internal set; }
		public string Name { get; internal set; }
		public string Head { get; internal set; }
		public Dictionary<string, int> ArgNames { get; internal set; }
	}

	public sealed class FunctionSet : IFunctionSet {

		private readonly Dictionary<string, FunctionInfo> functions;

		public FunctionSet() {
			this.functions = new Dictionary<string, FunctionInfo>();
		}

		public bool ContainsFunction(string name) {
			return this.functions.ContainsKey(name);
		}

		public bool SetFunction(string name, FunctionInfo value) {
			if (ContainsFunction(name)) {
				this.functions[name] = value;
			} else {
				this.functions.Add(name, value);
			}

			return true;
		}

		public FunctionInfo GetFunction(string name) {
			return functions[name];
		}

	}

}
