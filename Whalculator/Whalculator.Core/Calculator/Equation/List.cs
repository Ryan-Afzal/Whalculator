using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Represents an ordered list of elements
	/// </summary>
	public class List : NestedSolvable, IResult {

		public List(params ISolvable[] elements) : base(elements) {

		}

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public override IResult GetResultValue(ExpressionEvaluationArgs args) {
			return this.Clone() as IResult;
		}

		public override ISolvable Clone() {
			return new List(this.CloneOperands());
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();

			builder.Append('{');

			if (operands.Length > 0) {
				builder.Append(this.operands[0].GetEquationString());

				for (int i = 1; i < this.operands.Length; i++) {
					builder.Append(",");
					builder.Append(this.operands[i].GetEquationString());
				}
			}

			builder.Append('}');

			return builder.ToString();
		}
	}
}
