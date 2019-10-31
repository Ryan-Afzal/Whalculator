using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestLiteral {

		[TestMethod]
		public void TestBuildLiteralSimple1() {
			var output = TestManager.GetSolvableFromText("0");
			Assert.AreEqual("0", output.GetEquationString());
		}

		[TestMethod]
		public void TestBuildLiteralSimple2() {
			var output = TestManager.GetSolvableFromText("250");
			Assert.AreEqual("250", output.GetEquationString());
		}

		[TestMethod]
		public void TestBuildLiteralDecimal1() {
			var output = TestManager.GetSolvableFromText("42.420");
			Assert.AreEqual("42.42", output.GetEquationString());
		}

		[TestMethod]
		public void TestBuildLiteralDecimal2() {
			var output = TestManager.GetSolvableFromText("12.000001");
			Assert.AreEqual("12.000001", output.GetEquationString());
		}

	}
}
