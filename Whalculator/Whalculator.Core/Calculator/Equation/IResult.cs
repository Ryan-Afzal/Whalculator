using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Special type of <c>ISolvable</c> that is a basic mathematical representation, such as a <c>Literal</c>, <c>List</c>, or <c>Vector</c>.
	/// </summary>
	public interface IResult : ISolvable {
		
		ISolvable ISolvable.GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		IResult ISolvable.GetResultValue(ExpressionEvaluationArgs args) {
			return this.Clone() as IResult;
		}

	}
}
