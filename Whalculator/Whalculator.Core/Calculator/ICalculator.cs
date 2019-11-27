using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
    public interface ICalculator {
		ISolvable GetSolvableFromText(string input);
		string GetExactValue(string input);
		IResult GetResultValue(string input);
    }
}
