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
			if (args.Args.ArgNames.ContainsKey(VariableName)) {
				return args.Args.Args[args.Args.ArgNames[VariableName]].GetDoubleValue(args);
			} else if (args.VariableSet.IsVariable(VariableName)) {
				return args.VariableSet[VariableName].GetDoubleValue(args);
			} else {
				throw new InvalidEquationException(ErrorCode.NonexistentVariable);
			}
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public string GetEquationString() {
			return VariableName;
		}

		public override bool Equals(object obj) {
			if (obj is Variable v && v.VariableName.Equals(VariableName)) {
				return true;
			} else {
				return false;
			}
		}

		public override int GetHashCode() {
			return HashCode.Combine(VariableName);
		}
	}
}
