using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;
using Whalculator.WebApp.Interfaces;

namespace Whalculator.WebApp.Services {
	public class CalculatorService : ICalculatorService {

		private readonly Calculator calculator;

		public CalculatorService() {
			this.calculator = CalculatorFactory.GetDefaultCalculator();
		}

		public IResult GetResult(string input) {
			return this.calculator.GetResultValue(input);
		}

		public ISolvable GetSolvable(string input) {
			return this.calculator.GetSolvableFromText(input);
		}

		public void SetVariable(string head, string body) {
			this.calculator.SetVariable(head, body);
		}

		public void SetFunction(string head, string body) {
			this.calculator.SetFunction(head, body);
		}
	}
}
