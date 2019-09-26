using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public interface ISolvable {

		/// <summary>
		/// Gets the exact simplified value of the <code>ISolvable</code>
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		ISolvable GetExactValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets the decimal value of this <code>ISolvable</code>.
		/// Note that this function assumes that the <code>ISolvable</code> cannot be simplified further.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		double GetDoubleValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets a functional, recursive clone of this <code>ISolvable</code> 
		/// </summary>
		/// <returns>Returns a functional copy of this ISolvable</returns>
		ISolvable Clone();

		ISolvable Add(ISolvable other);
		ISolvable Multiply(ISolvable other);
		ISolvable Divide(ISolvable other);
		ISolvable Exponate(ISolvable other);

	}

}
