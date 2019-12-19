using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whalculator.Core.Calculator;

namespace Whalculator.WebApp.Data {
	public class CalculatorService {
		public async Task<CalculatorInstance> GetCalculatorInstanceAsync() {
			CalculatorInstance instance = new CalculatorInstance {
				Calculator = CalculatorFactory.GetDefaultCalculator()
			};

			return await Task.FromResult(instance);
		}
	}
}
