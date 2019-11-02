using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class Calculator_TestDerivative {

		[TestMethod]
		public void TestDifferentiateLinear() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("2*x");
			Assert.AreEqual("2*1", eqn.GetDerivative("x").GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateQuadratic() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^2");
			var derivative = eqn.GetDerivative("x");
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(0, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(2, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(4, derivative.GetDoubleValue(args));
		}

	}
}
