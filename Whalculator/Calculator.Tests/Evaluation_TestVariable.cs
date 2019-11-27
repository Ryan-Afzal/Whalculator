using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class Evaluation_TestVariable {

		[TestMethod]
		public void TestEvaluateVariableSimple1() {
			var output = TestManager.GetSolvableFromText("x");
			var set = new VariableSet();
			set.SetVariable("x", new Literal(25));
			Assert.AreEqual(
				new Literal(25),
				output.GetResultValue(
					new ExpressionEvaluationArgs() {
						VariableSet = set
					}
				)
			);
		}

		[TestMethod]
		public void TestEvaluateVariableSimple2() {
			var output = TestManager.GetSolvableFromText("x");
			var set = new VariableSet();
			set.SetVariable("x", new Literal(25.00001));
			Assert.AreEqual(
				new Literal(25.00001),
				output.GetResultValue(
					new ExpressionEvaluationArgs() {
						VariableSet = set
					}
				)
			);
		}

	}
}
