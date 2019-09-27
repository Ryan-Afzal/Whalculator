using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Literal : ISolvable {

		private readonly double value;

		public Literal(double value) {
			this.value = value;
		}

		public ISolvable Clone() {
			return new Literal(this.value);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return this.value;
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public override bool Equals(object obj) {
			if (obj is Literal l) {
				return this.value == l.value;
			} else {
				return false;
			}
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}
