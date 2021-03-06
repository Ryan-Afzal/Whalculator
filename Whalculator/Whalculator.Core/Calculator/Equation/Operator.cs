﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	/// <summary>
	/// Represents an operator with an arbitrary number of operands
	/// </summary>
	public sealed class Operator : NestedSolvable {

		public Operator(Operation operation, params ISolvable[] operands) : base(SortOperands(operation.SortedOperands, operands)) {
			Operation = operation;
		}

		public Operation Operation { get; }

		public override async Task<IResult> GetResultValueAsync(ExpressionEvaluationArgs args) {
			return await Operation.ResultValueOperation.Invoke(this.operands, args);
		}

		public override ISolvable Clone() {
			return new Operator(Operation, this.CloneOperands());
		}

		public override string GetEquationString() {
			if (this.operands.Length == 0) {
				return "";
			}
			StringBuilder builder = new StringBuilder();

			builder.Append(this.GetStringFromOperand(operands[0]));
			
			for (int i = 1; i < operands.Length; i++) {
				builder.Append(Operation.Name);
				builder.Append(this.GetStringFromOperand(operands[i]));
			}

			return builder.ToString();
		}

		private string GetStringFromOperand(ISolvable x) {
			if (x is Operator o) {
				if (o.Operation.Order <= Operation.Order) {
					return $"({o.GetEquationString()})";
				}
			}

			return x.GetEquationString();
		}

		private static ISolvable[] SortOperands(bool sort, ISolvable[] operands) {
			if (operands.Length is 0) {
				throw new ArgumentException("Cannot make an operator with zero operands.", nameof(operands));
			}

			if (sort) {
				Array.Sort(operands);
			}

			return operands;
		}

	}
}
