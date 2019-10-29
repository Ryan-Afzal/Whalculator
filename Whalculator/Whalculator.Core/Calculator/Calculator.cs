using System;
using System.Collections.Generic;
using System.Text;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Core.Calculator {
	public class Calculator : ICalculator {

		protected internal Calculator() {
			Settings = this.GetSettings();

			OperatorSet = new OperatorSet();
			BuiltinFunctionOperationSet = new BuiltinFunctionOperationSet();
			Variables = new VariableSet();
			Functions = new FunctionSet();
		}

		public ICalculatorSettings Settings { get; }

		public IOperatorSet OperatorSet { get; }
		public IBuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; }

		public IVariableSet Variables { get; }
		public IFunctionSet Functions { get; }

		protected virtual ICalculatorSettings GetSettings() {
			throw new NotImplementedException();
		}

		public double GetDoubleValue(string input) {
			throw new NotImplementedException();
		}

		public string GetExactValue(string input) {
			throw new NotImplementedException();
		}
	}
}
