using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Literal : IResult {

		public Literal(double value) {
			Value = value;
		}

		public double Value { get; }

		public ISolvable Clone() {
			return new Literal(Value);
		}

		public IResult GetResultValue(ExpressionEvaluationArgs args) {
			return this.Clone() as IResult;
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public string GetEquationString() {
			return Value.ToString();
		}

		public override int GetHashCode() {
			return HashCode.Combine(Value);
		}
	}
}
