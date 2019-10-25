using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilderTests {

		[TestMethod]
		public void TestBuildLiteral() {
			var output = ExpressionBuilder.GetSolvableFromText("42.420");
			Assert.AreEqual("42.42", output.GetEquationString());
		}

		[TestMethod]
		public void TestBuildOperatorBasic() {
			var output = ExpressionBuilder.GetSolvableFromText("1+1");
			Assert.AreEqual("1+1", output.GetEquationString());
		}

		//[TestMethod]
		//public void TestOOP() {
		//	throw new NotImplementedException();
		//}
	}
}
