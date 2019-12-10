using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Special type of <c>ISolvable</c> that represents a 
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
