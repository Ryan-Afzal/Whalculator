using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Vector : ISolvable {

		public Vector(params ISolvable[] components) {
			Components = components;
		}

		public ISolvable[] Components { get; }

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			throw new InvalidEquationException(ErrorCode.MismatchedArgumentType);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			throw new InvalidEquationException(ErrorCode.MismatchedArgumentType);
		}

		public ISolvable Clone() {
			ISolvable[] components = new ISolvable[Components.Length];

			for (int i = 0; i < components.Length; i++) {
				components[i] = Components[i];
			}

			return new Set(components);
		}

		public string GetEquationString() {
			StringBuilder builder = new StringBuilder();

			builder.Append('<');

			if (Components.Length > 0) {
				builder.Append(Components[0].GetEquationString());

				for (int i = 1; i < Components.Length; i++) {
					builder.Append(",");
					builder.Append(Components[i].GetEquationString());
				}
			}

			builder.Append('>');

			return builder.ToString();
		}

	}
}
