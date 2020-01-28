using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface IViewableOperatorSet {
		public bool IsOperator(char c);
		public Operation GetOperation(char c);
	}
}
