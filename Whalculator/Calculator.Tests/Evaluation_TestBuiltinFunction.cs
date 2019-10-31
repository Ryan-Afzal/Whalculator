using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class Evaluation_TestBuiltinFunction {

		[TestMethod]
		public void TestEvaluateBuiltinFunctionAbs1() {
			var output = TestManager.GetSolvableFromText("abs(25)");
			Assert.AreEqual(25, output.GetDoubleValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateBuiltinFunctionAbs2() {
			var output = TestManager.GetSolvableFromText("abs(-25)");
			Assert.AreEqual(25, output.GetDoubleValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateBuiltinFunctionCeil1() {
			var output = TestManager.GetSolvableFromText("ceil(25.99999)");
			Assert.AreEqual(26, output.GetDoubleValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateBuiltinFunctionCeil2() {
			var output = TestManager.GetSolvableFromText("ceil(25.00001)");
			Assert.AreEqual(26, output.GetDoubleValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateBuiltinFunctionFloor1() {
			var output = TestManager.GetSolvableFromText("floor(25.99999)");
			Assert.AreEqual(25, output.GetDoubleValue(new ExpressionEvaluationArgs() { }));
		}

		[TestMethod]
		public void TestEvaluateBuiltinFunctionFloor2() {
			var output = TestManager.GetSolvableFromText("floor(25.00001)");
			Assert.AreEqual(25, output.GetDoubleValue(new ExpressionEvaluationArgs() { }));
		}

	}
}
