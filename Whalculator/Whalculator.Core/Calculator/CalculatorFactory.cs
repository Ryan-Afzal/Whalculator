using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator {
	public static class CalculatorFactory {

		public static BaseCalculator GetDefaultCalculator() {
			return new BaseCalculator();
		}

	}
}
