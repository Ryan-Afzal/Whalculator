using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilder_TestOperators {

		[TestMethod]
		public void TestBuildOperatorAddition1() {
			var output = ExpressionBuilder.GetSolvableFromText("1+1");
			Assert.AreEqual("1+1", output.GetEquationString());
		}

		[TestMethod]
		public void TestBuildOperatorAddition2() {
			var output = ExpressionBuilder.GetSolvableFromText("1+2+3");
			Assert.AreEqual("1+2+3", output.GetEquationString());
		}

		//[TestMethod]
		public void TestBuildOperatorMultiplication1() {
			throw new NotImplementedException();
		}

		//[TestMethod]
		public void TestBuildOperatorMultiplication2() {
			throw new NotImplementedException();
		}

	}
}
