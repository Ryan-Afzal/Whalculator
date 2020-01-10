using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whalculator.WebApp.Data {
	public class CalculatorService {

		public Task<CalculatorInstance> GetInstanceAsync() {
			return Task.FromResult(new CalculatorInstance());
		}

	}
}
