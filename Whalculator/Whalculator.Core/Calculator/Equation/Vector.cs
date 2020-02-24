using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Represents a vector, such as <c>&lt;1,0&gt;</c> or <c>&lt;10.35,2.5,9.17&gt;</c>
	/// </summary>
	public class Vector : NestedSolvable, IResult {

		public Vector(params ISolvable[] components) : base(components) {

		}

		public int Dimensions {
			get {
				return this.operands.Length;
			}
		}

		public override async Task<IResult> GetResultValueAsync(ExpressionEvaluationArgs args) {
			return new Vector(await this.EvaluateOperandsResult(args));
		}

		public override ISolvable Clone() {
			return new Vector(this.CloneOperands());
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();

			builder.Append('<');

			if (operands.Length > 0) {
				builder.Append(this.operands[0].GetEquationString());

				for (int i = 1; i < this.operands.Length; i++) {
					builder.Append(",");
					builder.Append(this.operands[i].GetEquationString());
				}
			}

			builder.Append('>');

			return builder.ToString();
		}

	}
}
