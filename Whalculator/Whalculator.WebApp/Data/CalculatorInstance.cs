using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whalculator.Core.Calculator;

namespace Whalculator.WebApp.Data {
	public class CalculatorInstance {

		public CalculatorInstance() {
			Calculator = new Calculator();
		}

		public Calculator Calculator { get; }

	}
}
