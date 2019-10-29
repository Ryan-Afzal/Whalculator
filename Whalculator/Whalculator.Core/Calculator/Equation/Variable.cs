using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class Variable : ISolvable {

		private readonly string variableName;

		public Variable(string variableName) {
			this.variableName = variableName;
		}

		public ISolvable Clone() {
			return new Variable(this.variableName);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return args.VariableSet[this.variableName].GetDoubleValue(args);
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public string GetEquationString() {
			return this.variableName;
		}
	}
}
