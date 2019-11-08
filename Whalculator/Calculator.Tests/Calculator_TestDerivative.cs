using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class Calculator_TestDerivative {

		[TestMethod]
		public void TestDifferentiateLinear1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("2*x");
			Assert.AreEqual("2", eqn.GetDerivative("x").GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateLinear2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("2*x+100");
			Assert.AreEqual("2", eqn.GetDerivative("x").GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateQuadratic1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^2");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2*x", derivative.GetEquationString());
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

		[TestMethod]
		public void TestDifferentiateQuadratic2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^2+2x+1");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2*x+2", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(2, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(4, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(6, derivative.GetDoubleValue(args));
		}

		[TestMethod]
		public void TestDifferentiateCubic1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^3");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("3*x^2", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(0, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(3, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(12, derivative.GetDoubleValue(args));
		}

		[TestMethod]
		public void TestDifferentiateCubic2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^3+3*x^2+3*x+1");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("3+3*x^2+6*x", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(3, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(12, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(27, derivative.GetDoubleValue(args));
		}

		[TestMethod]
		public void TestDifferentiateChainPoly1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("(x+1)^2");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2*(x+1)", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(0, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(3, derivative.GetDoubleValue(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(12, derivative.GetDoubleValue(args));
		}

	}
}
