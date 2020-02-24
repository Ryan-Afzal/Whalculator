using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whalculator.Core.Misc;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Base class for <c>ISolvable</c> nodes that have an arbitrary number of children represented as operands. 
	/// </summary>
	public abstract class NestedSolvable : ISolvable {

		public readonly ISolvable[] operands;

		protected NestedSolvable(ISolvable[] operands) {
			this.operands = operands;
		}

		public abstract ISolvable Clone();
		public abstract Task<IResult> GetResultValueAsync(ExpressionEvaluationArgs args);
		public abstract string GetEquationString();

		protected internal ISolvable[] CloneOperands() {
			ISolvable[] output = new ISolvable[this.operands.Length];

			for (int i = 0; i < output.Length; i++) {
				output[i] = this.operands[i];
			}

			return output;
		}

		protected internal async Task<ISolvable[]> EvaluateOperandsResult(ExpressionEvaluationArgs args) {
			IResult[] output = new IResult[this.operands.Length];

			for (int i = 0; i < output.Length; i++) {
				output[i] = await this.operands[i].GetResultValueAsync(args);
			}

			return output;
		}

	}
}
