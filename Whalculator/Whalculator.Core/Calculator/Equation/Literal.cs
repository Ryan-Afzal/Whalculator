using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Literal : ISolvable {

		private readonly double value;

		public Literal(double value) {
			this.value = value;
		}

		public ISolvable Add(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Clone() {
			return new Literal(this.value);
		}

		public ISolvable Divide(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Exponate(ISolvable other) {
			throw new NotImplementedException();
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			throw new NotImplementedException();
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			throw new NotImplementedException();
		}

		public ISolvable Multiply(ISolvable other) {
			throw new NotImplementedException();
		}
	}
}
