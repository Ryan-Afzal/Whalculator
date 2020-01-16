﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Represents a literal value, such as <c>1</c> or <c>23.225</c>
	/// </summary>
	public class Literal : IResult {

		public Literal(double value) {
			Value = value;
		}

		public double Value { get; }

		public ISolvable Clone() {
			return new Literal(Value);
		}

		public IResult GetResultValue(ExpressionEvaluationArgs args) {
			return (IResult)this.Clone();
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public string GetEquationString() {
			return Value.ToString();
		}

		public override int GetHashCode() {
			return HashCode.Combine(Value);
		}
	}
}
