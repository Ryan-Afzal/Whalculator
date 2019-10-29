using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator {
    public interface ICalculator {
		string GetExactValue(string input);
		double GetDoubleValue(string input);
    }
}
