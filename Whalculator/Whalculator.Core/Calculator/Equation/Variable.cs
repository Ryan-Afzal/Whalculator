using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class Variable : ISolvable {

		public Variable(string variableName) {
			VariableName = variableName;
		}

		public string VariableName { get; }

		public ISolvable Clone() {
			return new Variable(VariableName);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return args.VariableSet[VariableName].GetDoubleValue(args);
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public string GetEquationString() {
			return VariableName;
		}

		public int CompareTo(ISolvable solvable) {
			if (solvable is Literal) {
				return -1;
			} else if (solvable is Variable v) {
				return VariableName.CompareTo(v.VariableName);
			} else {
				return 1;
			}
		}

		public override bool Equals(object obj) {
			if (obj is Variable v && v.VariableName.Equals(VariableName)) {
				return true;
			} else {
				return false;
			}
		}
	}
}
