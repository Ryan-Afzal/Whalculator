using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public interface ISolvable {

		/// <summary>
		/// Gets the exact simplified value of the ISolvable
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		ISolvable GetExactValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets the decimal value of this ISolvable.
		/// Note that this function assumes that the ISolvable cannot be simplified further.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		double GetDoubleValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets a functional, recursive clone of this <code>ISolvable</code> 
		/// </summary>
		/// <returns>Returns a functional copy of this ISolvable</returns>
		ISolvable Clone();

	}

}
