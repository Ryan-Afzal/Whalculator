using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
    public interface ICalculator {
		string GetExactValue(string input);
		IResult GetResultValue(string input);
		bool SetVariable(string head, string body);
		bool SetFunction(string head, string body);
    }
}
