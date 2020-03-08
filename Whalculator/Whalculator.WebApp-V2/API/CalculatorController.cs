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

				string? output = null;

				for (int i = 0; i < model.Input.Length; i++) {
					output = await calc.ProcessInputAsync(model.Input[i]);
				}

				if (output is null) {
					return BadRequest();
				} else {
					return new CalculatorResponseModel() {
						Response = output
					};
				}
			} catch (Exception) {
				return new CalculatorResponseModel() {
					Response = "UNKNOWN ERROR"
				};
			}
		}

	}
}
