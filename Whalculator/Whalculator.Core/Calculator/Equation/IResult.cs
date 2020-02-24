using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Special type of <c>ISolvable</c> that is a basic mathematical representation, such as a <c>Literal</c>, <c>List</c>, or <c>Vector</c>.
	/// </summary>
	public interface IResult : ISolvable {

		Task<IResult> ISolvable.GetResultValueAsync(ExpressionEvaluationArgs args) {
			return Task.FromResult((IResult)this.Clone());
		}

	}
}
