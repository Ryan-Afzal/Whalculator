using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Whalculator.Core.Calculator;
using Whalculator.WebApp_V2.Models;

namespace Whalculator.WebApp_V2.API {
	[Route("api/[controller]")]
	[ApiController]
	public class CalculatorController : ControllerBase {

		[HttpPost]
		public async Task<ActionResult<CalculatorResponseModel>> Post(CalculatorInputModel model) {
			try {
				var calc = new Calculator();
				return new CalculatorResponseModel() {
					Response = await calc.ProcessInputAsync(model.Input)
				};
			} catch (Exception) {
				return new CalculatorResponseModel() {
					Response = "UNKNOWN ERROR"
				};
			}
		}

		[Obsolete]
		public async Task<ActionResult<string>> Post(IEnumerable<string> inputStrings) {
			var calc = new Calculator();
			var enumerator = inputStrings.GetEnumerator();

			string? output;

			do {
				output = await calc.ProcessInputAsync(enumerator.Current);
				enumerator.MoveNext();
			} while (enumerator.Current is string);

			if (output is null) {
				return BadRequest();
			} else {
				return output;
			}
		}

	}
}
