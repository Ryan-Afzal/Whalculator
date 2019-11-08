using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public class Literal : ISolvable {

		public Literal(double value) {
			Value = value;
		}

		public double Value { get; }

		public ISolvable Clone() {
			return new Literal(Value);
		}

		public double GetDoubleValue(ExpressionEvaluationArgs args) {
			return Value;
		}

		public ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			return this.Clone();
		}

		public string GetEquationString() {
			return $"{Value}";
		}

		public override bool Equals(object obj) {
			if (obj is Literal l) {
				return Value == l.Value;
			} else {
				return false;
			}
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(ISolvable solvable) {
			if (solvable is Literal l) {
				if (Value < l.Value) {
					return -1;
				} else if (Value == l.Value) {
					return 0;
				} else {
					return 1;
				}
			}

			return -1;
		}
	}
}
