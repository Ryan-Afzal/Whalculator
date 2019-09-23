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
			throw new NotImplementedException();
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			throw new NotImplementedException();
		}
	}
}
