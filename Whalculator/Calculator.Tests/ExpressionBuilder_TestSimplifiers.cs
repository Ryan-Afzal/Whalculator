using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestSimplifiers {

		[TestMethod]
		public void TestBuildLevelOperators() {
			var output = ExpressionBuilder.GetSolvableFromText("1+2+3") as Operator;
			foreach (var x in output.operands) {
				if (x is Operator) {
					Assert.Fail();
				}
			}
		}

		//[TestMethod]
		public void TestBuildTransformNegatives() {
			throw new NotImplementedException();
		}

	}
}
