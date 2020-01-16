using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator {
	public static class CalculatorFactory {

		public static Calculator GetDefaultCalculator() {
			return new Calculator();
		}

	}
}
