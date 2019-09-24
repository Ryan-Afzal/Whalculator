using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public interface ISolvable {
		/// <summary>
		/// Gets an exact value answer, expressed as an ISolvable
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		ISolvable GetExactValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets the decimal value of the answer. This function assumes that the structure cannot be simplified further.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		double GetDoubleValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets an ISolvable basically equal to this one. 
		/// </summary>
		/// <returns>Returns a functional copy of this ISolvable</returns>
		ISolvable Clone();

		ISolvable Add(ISolvable other);
		ISolvable Multiply(ISolvable other);
		ISolvable Divide(ISolvable other);
		ISolvable Exponate(ISolvable other);
	}
}
