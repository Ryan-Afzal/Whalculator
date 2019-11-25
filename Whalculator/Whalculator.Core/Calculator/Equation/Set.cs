using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Set : ISolvable {

		public Set(params ISolvable[] elements) {
			Elements = elements;
		}

		public ISolvable[] Elements { get; }

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			throw new InvalidEquationException(ErrorCode.MismatchedArgumentType);
		}

		public ISolvable Clone() {
			ISolvable[] elements = new ISolvable[Elements.Length];

			for (int i = 0; i < elements.Length; i++) {
				elements[i] = Elements[i];
			}

			return new Set(elements);
		}

		public string GetEquationString() {
			StringBuilder builder = new StringBuilder();

			builder.Append('{');

			if (Elements.Length > 0) {
				builder.Append(Elements[0].GetEquationString());

				for (int i = 1; i < Elements.Length; i++) {
					builder.Append(",");
					builder.Append(Elements[i].GetEquationString());
				}
			}

			builder.Append('}');

			return builder.ToString();
		}
	}
}
