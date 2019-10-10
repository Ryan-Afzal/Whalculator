using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class ExpressionBuilder {

		private struct GenerationArgs {
			
		}

		public static ISolvable GetSolvableFromText(string text) {
			throw new NotImplementedException();
		}

		private static ISolvable GetSolvableFromText(string text, GenerationArgs args) {
			if (text.Length == 0) {
				return new Literal(0.0);
			}

			char op = (char)0;
			int index = -1;

			int pDepth = 0;//The depth of parenthetical nesting that the function is going into
			int oDepth = 0;//The depth of operator nesting that the function is going into
			int fDepth = 0;//The depth of functional nesting that the function is going into

			for (int i = 0; i < text.Length; i++) {
				char c = text[i];

				if (c == '(') {
					if (fDepth >= pDepth && (i <= 0 
						|| args.Operators.IsOperator(text[i - 1]) 
						|| text[i - 1] == '(' 
						|| text[i - 1] == ')')) {
						fDepth++;
					}

					pDepth++;
				} else if (c == ')') {
					pDepth--;

					if (fDepth > pDepth) {
						fDepth--;
					}
				} else if (fDepth >= pDepth && args.Operators.IsOperator(c)) {
					if (index == -1 || (pDepth < oDepth || (pDepth == oDepth && args.Operators.GetOperator(op).Order >=
						args.Operators.GetOperator(c).Order))) {

						oDepth = pDepth;
						op = c;
						index = i;
					}
				}

				if (pDepth < 0) {
					Debug.WriteLine(text);
					throw new InvalidEquationException(0);
				}
			}

			throw new NotImplementedException();
		}

		public static bool IsOperator(this char c) {
			throw new NotImplementedException();
		}
	}
}
