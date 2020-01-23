using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class ImplicitDifferentiationSymbol : IResult {

		public ImplicitDifferentiationSymbol(string num, string den) {
			Numerator = num;
			Denominator = den;
		}

		public string Numerator { get; }
		public string Denominator { get; }

		public ISolvable Clone() {
			return new ImplicitDifferentiationSymbol(Numerator, Denominator);
		}

		public string GetEquationString() {
			return $"d{Numerator}/d{Denominator}";
		}
	}
}
