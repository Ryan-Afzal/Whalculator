using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class Term : ISolvable {

		private readonly ISolvable[] coefficients;

		public Term(params ISolvable[] coefficients) {
			this.coefficients = coefficients;
		}

		public ISolvable Clone() {
			var newArray = new ISolvable[this.coefficients.Length];
			for (int i = 0; i < newArray.Length; i++) {
				newArray[i] = this.coefficients[i].Clone();
			}

			return new Term(newArray);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			if (this.coefficients.Length == 0) {
				return 0.0;
			} else {
				double output = 1.0;

				foreach (ISolvable solvable in this.coefficients) {
					output *= solvable.GetDoubleValue(args);
				}

				return output;
			}
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			if (this.coefficients.Length == 0) {
				return new Literal(0.0);
			} else {
				ISolvable solvable = this.coefficients[0];

				for (int i = 0; i < this.coefficients.Length; i++) {
					solvable = solvable.Multiply(this.coefficients[i]);
				}

				return solvable;
			}
		}

		public ISolvable Add(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Multiply(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Divide(ISolvable other) {
			throw new NotImplementedException();
		}

		public ISolvable Exponate(ISolvable other) {
			throw new NotImplementedException();
		}
	}
}
