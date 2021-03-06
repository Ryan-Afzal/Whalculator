﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			Assert.AreEqual("2", await eqn.GetDerivative("x").GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateQuadratic1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^2");
			var derivative = await eqn.GetDerivative("x");
			Assert.AreEqual("2*x", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(new Literal(0), await derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(new Literal(2), await derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(new Literal(4), await derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateQuadratic2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^2+2*x+1");
			var derivative = await eqn.GetDerivative("x");
			Assert.AreEqual("2+2*x", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(2, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(4, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(6, derivative.GetResultValueAsync(args));
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
			Assert.AreEqual(0, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(3, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(12, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateCubic2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^3+3*x^2+3*x+1");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("3+6*x+3*x^2", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(3, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(12, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(27, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateChainPoly1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("(x+1)^2");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2*(1+x)", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0));
			Assert.AreEqual(2, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(4, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(6, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateSqrt1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("x^(1/2)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2^-1*x^(-1/2)", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(1.0 / 2, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(4));
			Assert.AreEqual(1.0 / 4, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(9));
			Assert.AreEqual(1.0 / 6, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateChainSqrt1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("(2*x)^(1/2)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("(2*x)^(-1/2)", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(0.5));
			Assert.AreEqual(1.0, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(1.0 / 2, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(4.5));
			Assert.AreEqual(1.0 / 6, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateLn1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("ln(x)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("1/x", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(1.0, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(1.0 / 2, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(3));
			Assert.AreEqual(1.0 / 3, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateLn2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("2*ln(x+5)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2/(5+x)", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(1.0 / 3, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(2.0 / 7, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(3));
			Assert.AreEqual(1.0 / 4, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateLn3() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("ln(3*x+5)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("3/(5+3*x)", derivative.GetEquationString());
			var args = new ExpressionEvaluationArgs() {
				VariableSet = calc.Variables
			};
			calc.Variables.SetVariable("x", new Literal(1));
			Assert.AreEqual(1.0 / 3, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(2));
			Assert.AreEqual(2.0 / 7, derivative.GetResultValueAsync(args));
			calc.Variables.SetVariable("x", new Literal(3));
			Assert.AreEqual(1.0 / 4, derivative.GetResultValueAsync(args));
		}

		[TestMethod]
		public void TestDifferentiateSin1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("sin(x)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("cos(x)", derivative.GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateSin2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("sin(2*x)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("2*cos(2*x)", derivative.GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateCos1() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("cos(x)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("-1*sin(x)", derivative.GetEquationString());
		}

		[TestMethod]
		public void TestDifferentiateCos2() {
			var calc = CalculatorFactory.GetDefaultCalculator();
			var eqn = calc.GetSolvableFromText("cos(2*x)");
			var derivative = eqn.GetDerivative("x");
			Assert.AreEqual("-2*sin(2*x)", derivative.GetEquationString());
		}

	}
}
