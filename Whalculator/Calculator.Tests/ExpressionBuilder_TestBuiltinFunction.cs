using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestBuiltinFunction {

		[TestMethod]
		public void TestBuildBuiltinFunctionSine1() {
			var output = TestManager.GetSolvableFromText("sin(0)");
			Assert.AreEqual("sin(0)", output.GetEquationString());
			if (!(output is BuiltinFunction)) {
				Assert.Fail();
			}
		}

		[TestMethod]
		public void TestBuildBuiltinFunctionSine2() {
			var output = TestManager.GetSolvableFromText("sin(x + 10)");
			Assert.AreEqual("sin(x+10)", output.GetEquationString());
			if (!(output is BuiltinFunction)) {
				Assert.Fail();
			}
		}

	}
}
