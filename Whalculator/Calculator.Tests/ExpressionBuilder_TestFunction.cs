using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestFunction {

		[TestMethod]
		public void TestBuildFunctionF1() {
			TestManager.Functions.SetFunction("f", new FunctionInfo() { Name = "f", Head = "f(x)", ArgNames = new System.Collections.Generic.Dictionary<string, int>() { { "x", 0 } }, Function = new Variable("x") });
			var output = TestManager.GetSolvableFromText("f(0)");
			Assert.AreEqual("f(0)", output.GetEquationString());
			if (!(output is Function)) {     
				Assert.Fail();
			}
		}

		[TestMethod]
		public void TestBuildFunctionF2() {
			TestManager.Functions.SetFunction("f", new FunctionInfo() { Name = "f", Head = "f(x)", ArgNames = new System.Collections.Generic.Dictionary<string, int>() { { "x", 0 } }, Function = new Variable("x") });
			var output = TestManager.GetSolvableFromText("f(x+10)");
			Assert.AreEqual("f(x+10)", output.GetEquationString());
			if (!(output is Function)) {
				Assert.Fail();
			}
		}

	}
}
