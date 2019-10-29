using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator {
    public static class CalculatorFactory {

        public static ICalculator GetDefaultCalculator() {
			return new Calculator();
        }

    }
}
