using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class Evaluation_TestLiteral {

		[TestMethod]
		public void TestEvaluateLiteralSimple1() {
			var output = TestManager.GetSolvableFromText("0");
			Assert.AreEqual(0.0, output.GetResultValueAsync(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateLiteralSimple2() {
			var output = TestManager.GetSolvableFromText("25");
			Assert.AreEqual(25.0, output.GetResultValueAsync(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateLiteralDecimal1() {
			var output = TestManager.GetSolvableFromText("25.1");
			Assert.AreEqual(25.1, output.GetResultValueAsync(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateLiteralDecimal2() {
			var output = TestManager.GetSolvableFromText("420.000000001");
			Assert.AreEqual(420.000000001, output.GetResultValueAsync(new ExpressionEvaluationArgs() { }));
		}

	}
}
