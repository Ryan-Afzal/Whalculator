using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation.Expression {
    public interface IExpression : ICloneable {
        IExpression GetExactValue(ExpressionEvaluationArgs args);
        double GetValue(ExpressionEvaluationArgs args);
    }
}
