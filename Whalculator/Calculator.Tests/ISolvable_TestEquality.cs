using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whalculator.Core.Calculator;
using Whalculator.Core.Calculator.Equation;

namespace Whalculator.Tests {
	[TestClass]
	public class ISolvable_TestEquality {

		[TestMethod]
		public void TestSolvableEqualityLiteral1() {
			ISolvable s1 = new Literal(1);
			ISolvable s2 = new Literal(1);
			Assert.IsTrue(s1.Equals(s2));
		}

		[TestMethod]
		public void TestSolvableEqualityLiteral2() {
			ISolvable s1 = new Literal(25.250);
			ISolvable s2 = new Literal(25.250);
			Assert.IsTrue(s1.Equals(s2));
		}

		[TestMethod]
		public void TestSolvableEqualityVariable1() {
			ISolvable s1 = new Variable("x");
			ISolvable s2 = new Variable("x");
			Assert.IsTrue(s1.Equals(s2));
		}

		[TestMethod]
		public void TestSolvableEqualityOperator1() {
			ISolvable s1 = new Operator(Operations.AddOperation, new Literal(1), new Variable("x"));
			ISolvable s2 = new Operator(Operations.MultiplyOperation, new Operator(Operations.AddOperation, new Literal(1), new Variable("x")));
			s1.Equals(s2);
			Assert.IsTrue(s1.Equals(s2));
		}

	}
}
