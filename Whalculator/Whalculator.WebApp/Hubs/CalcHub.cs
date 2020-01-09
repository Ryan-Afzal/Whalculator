using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation;
using Whalculator.WebApp.Interfaces;

namespace Whalculator.WebApp.Hubs {
	public class CalcHub : Hub {

		private readonly ICalculatorService calculatorService;

		public CalcHub(ICalculatorService calculatorService) {
			this.calculatorService = calculatorService;
		}

		public async Task ProcessInput(string input) {
			string result;
			input = input.Replace(" ", "");

			int idx = input.IndexOf("d/dx");
			if (idx == 0) {
				string body = input.Substring(4);
				result = this.calculatorService.GetSolvable(body).GetDerivative("x").GetEquationString();
			} else {
				int i = input.IndexOf('=');
				if (i == -1) {
					result = this.calculatorService.GetResult(input).GetEquationString();
				} else {
					string head = input.Substring(0, i);
					string body = input.Substring(i + 1);

					int hi = head.IndexOf('(');
					if (hi == -1) {
						this.calculatorService.SetVariable(head, body);
					} else {
						this.calculatorService.SetFunction(head, body);
					}

					result = "Set " + head + "!";
				}
			}

			await Clients.Caller.SendAsync("ReceiveResult", result);
		}

	}
}
