using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.WebApp.Interfaces {
	public interface ICalculatorService {
		public IResult GetResult(string input);
		public ISolvable GetSolvable(string input);
		public void SetVariable(string head, string body);
		public void SetFunction(string head, string body);
	}
}
