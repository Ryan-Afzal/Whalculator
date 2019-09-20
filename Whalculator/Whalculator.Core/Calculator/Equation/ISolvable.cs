using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public interface ISolvable {
		ISolvable GetExactValue(ExpressionEvaluationArgs args);
		double GetDoubleValue(ExpressionEvaluationArgs args);
		ISolvable Clone();
	}
}
