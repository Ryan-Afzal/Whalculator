using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation.Expression {
    public interface ISolvable : ICloneable {
        ISolvable GetExactValue(ExpressionEvaluationArgs args);
        double GetValue(ExpressionEvaluationArgs args);
    }
}
