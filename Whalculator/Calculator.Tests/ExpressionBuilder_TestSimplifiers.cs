using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestSimplifiers {

		[TestMethod]
		public void TestBuildTransformNegatives1() {
			var output = TestManager.GetSolvableFromText("1-2") as Operator;
			if (output.Operation.Name == '+') {
				if (output.operands[0] is Literal && output.operands[1] is Operator _o) {
					if (_o.operands[0] is Literal && _o.operands[0] is Literal) {
						return;
					}
				}
			}

			Assert.Fail();
		}

		[TestMethod]
		public void TestBuildTransformNegatives2() {
			var output = TestManager.GetSolvableFromText("-2") as Operator;
			if (output.Operation.Name == '*' && output.operands[0] is Literal && output.operands[1] is Literal) {
				return;
			} else {
				Assert.Fail();
			}
		}

		[TestMethod]
		public void TestBuildLevelOperators1() {
			var output = TestManager.GetSolvableFromText("1+2+3") as Operator;
			foreach (var x in output.operands) {
				if (x is Operator) {
					Assert.Fail();
				}
			}
		}

		[TestMethod]
		public void TestBuildCollectLikeTerms1() {
			var output = TestManager.GetSolvableFromText("(x^2)*(x^3)");
			Assert.AreEqual("x^(2+3)", output.GetEquationString());
		}
	}
}
