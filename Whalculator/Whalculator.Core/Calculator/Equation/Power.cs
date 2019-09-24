using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Power : ISolvable {

		private readonly ISolvable pBase;
		private readonly ISolvable exponent;

		public Power(ISolvable pBase, ISolvable exponent) {
			this.pBase = pBase;
			this.exponent = exponent;
		}

		public ISolvable Add(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Clone() {
			return new Power(this.pBase.Clone(), this.exponent.Clone());
		}

		public ISolvable Divide(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Exponate(ISolvable other) {
			throw new NotImplementedException();
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return Math.Pow(this.pBase.GetDoubleValue(args), this.exponent.GetDoubleValue(args));
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			throw new NotImplementedException();
		}

		public ISolvable Multiply(ISolvable other) {
			throw new NotImplementedException();
		}
	}
}
