using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class Evaluation_TestOperators {

		[TestMethod]
		public void TestEvaluateOperatorLiteral01() {
			var output = TestManager.GetSolvableFromText("1+1");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral02() {
			var output = TestManager.GetSolvableFromText("2+38");
			Assert.AreEqual(40, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral03() {
			var output = TestManager.GetSolvableFromText("38+2+438");
			Assert.AreEqual(478, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral04() {
			var output = TestManager.GetSolvableFromText("5-3");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral05() {
			var output = TestManager.GetSolvableFromText("1-2+3");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral06() {
			var output = TestManager.GetSolvableFromText("1+3-2");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral07() {
			var output = TestManager.GetSolvableFromText("1*0");
			Assert.AreEqual(0, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral08() {
			var output = TestManager.GetSolvableFromText("1*2");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral09() {
			var output = TestManager.GetSolvableFromText("2*3");
			Assert.AreEqual(6, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral10() {
			var output = TestManager.GetSolvableFromText("10*5*4");
			Assert.AreEqual(200, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral11() {
			var output = TestManager.GetSolvableFromText("78*37583");
			Assert.AreEqual(2931474, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral12() {
			var output = TestManager.GetSolvableFromText("10/1");
			Assert.AreEqual(10, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral13() {
			var output = TestManager.GetSolvableFromText("10/0.5");
			Assert.AreEqual(20, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral14() {
			var output = TestManager.GetSolvableFromText("27/3");
			Assert.AreEqual(9, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral15() {
			var output = TestManager.GetSolvableFromText("1^2");
			Assert.AreEqual(1, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral16() {
			var output = TestManager.GetSolvableFromText("2^3");
			Assert.AreEqual(8, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorLiteral17() {
			var output = TestManager.GetSolvableFromText("4^(0.5)");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorMixed01() {
			var output = TestManager.GetSolvableFromText("1+4*2");
			Assert.AreEqual(9, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateOperatorMixed02() {
			var output = TestManager.GetSolvableFromText("(1+4)*2");
			Assert.AreEqual(10, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}
		
		[TestMethod]
		public void TestEvaluateOperatorMixed03() {
			var output = TestManager.GetSolvableFromText("4^(1/2)");
			Assert.AreEqual(2, output.GetResultValue(new ExpressionEvaluationArgs() { }));
		}

	}
}
