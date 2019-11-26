using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public interface ISolvable : IComparable<ISolvable>, IEquatable<ISolvable> {

		/// <summary>
		/// Gets the exact simplified value of the ISolvable
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public ISolvable GetExactValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets the decimal value of this ISolvable.
		/// Note that this function assumes that the ISolvable cannot be simplified further.
		/// <remarks>
		/// As such, it should not call GetExactValue
		/// </remarks>
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public IResult GetResultValue(ExpressionEvaluationArgs args);

		/// <summary>
		/// Gets a functional, recursive clone of this <code>ISolvable</code> 
		/// </summary>
		/// <returns>Returns a functional copy of this ISolvable</returns>
		public ISolvable Clone();

		/// <summary>
		/// Gets a string such that if put back into the EquationBuilder, it would reproduce this ISolvable.
		/// </summary>
		/// <returns></returns>
		public string GetEquationString();

		bool IEquatable<ISolvable>.Equals(ISolvable other) {
			return this.GetEquationString().Equals(other.GetEquationString());
		}

		int IComparable<ISolvable>.CompareTo(ISolvable other) {
			return Compare(this, other);
		}

		public static int Compare(ISolvable solvable1, ISolvable solvable2) {
			if (solvable1 is Literal l1) {
				if (solvable2 is Literal l2) {
					if (l1.Value > l2.Value) {
						return 1;
					} else if (l1.Value < l2.Value) {
						return -1;
					} else {
						return 0;
					}
				} else {
					return -1;
				}
			} else if (solvable1 is Variable v1) {
				if (solvable2 is Literal) {
					return 1;
				} else if (solvable2 is Variable v2) {
					return v1.VariableName.CompareTo(v2.VariableName);
				} else {
					return -1;
				}
			} else if (solvable1 is Operator o1) {
				if (solvable2 is Literal) {
					return 1;
				} else if (solvable2 is Variable) {
					return 1;
				} else if (solvable2 is Operator o2) {
					return o1.Operation.Order - o2.Operation.Order;
				} else {
					return -1;
				}
			} else if (solvable1 is BuiltinFunction b1) {
				if (solvable2 is Literal) {
					return 1;
				} else if (solvable2 is Variable) {
					return 1;
				} else if (solvable2 is Operator) {
					return 1;
				} else if (solvable2 is BuiltinFunction b2) {
					return b1.Operation.Name.CompareTo(b2.Operation.Name);
				} else {
					return -1;
				}
			} else if (solvable1 is Function f1) {
				if (solvable2 is Literal) {
					return 1;
				} else if (solvable2 is Variable) {
					return 1;
				} else if (solvable2 is Operator) {
					return 1;
				} else if (solvable2 is BuiltinFunction) {
					return 1;
				} else if (solvable2 is Function f2) {
					return f1.GetEquationString().CompareTo(f2.GetEquationString());
				} else {
					return -1;
				}
			} else {
				return solvable1.GetEquationString().CompareTo(solvable2.GetEquationString());
			}
		}


	}

}
