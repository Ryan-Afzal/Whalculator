using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ExpressionBuilderTests {

		[TestMethod]
		public static void TestBuildLiteral() {
			var output = ExpressionBuilder.GetSolvableFromText("42.420");
			Assert.Equals(42.420, output.GetDoubleValue(new ExpressionEvaluationArgs()));
		}

		[TestMethod]
		public static void TestBuildOperatorBasic() {
			var output = ExpressionBuilder.GetSolvableFromText("1+1");
			Assert.Equals(2, output.GetDoubleValue(new ExpressionEvaluationArgs()));
		}
	}
}
