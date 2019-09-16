using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation.Expression;

namespace Whalculator.Core.Calculator.Equation {
    public class Equation : ICloneable {

        private readonly IExpression leftExpression;
        private readonly IExpression rightExpression;

        public Equation(IExpression left, IExpression right) {
            this.leftExpression = left;
            this.rightExpression = right;
        }

        public ICloneable Clone() {
            return new Equation(
                (IExpression)this.leftExpression.Clone(),
                (IExpression)this.rightExpression.Clone());
        }
    }
}
