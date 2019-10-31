using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestVariable {

		[TestMethod]
		public void TestBuildVariableSimple1() {
			var output = TestManager.GetSolvableFromText("x");
			Assert.AreEqual("x", output.GetEquationString());
		}

		[TestMethod]
		public void TestBuildVariableSimple2() {
			var output = TestManager.GetSolvableFromText("name");
			Assert.AreEqual("name", output.GetEquationString());
		}

	}
}
