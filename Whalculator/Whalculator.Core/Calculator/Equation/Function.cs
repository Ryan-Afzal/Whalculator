using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public struct FunctionArgumentArgs {
		public Dictionary<string, int> ArgNames { get; internal set; }
		public ISolvable[] Args { get; internal set; }
	}

	public sealed class Function : NestedSolvable {

		private readonly FunctionInfo info;

		public Function(FunctionInfo info, ISolvable[] args) : base(args) {
			this.info = info;
		}

		public override ISolvable Clone() {
			return new Function(this.info, this.CloneOperands());
		}

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			ISolvable[] _args = this.CloneOperands();

			args.Args = new FunctionArgumentArgs() {
				ArgNames = this.info.ArgNames,
				Args = _args
			};

			return this.info.Function.GetExactValue(args);
		}

		public override double GetDoubleValue(ExpressionEvaluationArgs args) {
			ISolvable[] _args = this.CloneOperands();

			args.Args = new FunctionArgumentArgs() {
				ArgNames = this.info.ArgNames,
				Args = _args
			};

			return this.GetExactValue(args).GetDoubleValue(args);
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();
			builder.Append(this.info.Name);
			builder.Append('(');

			if (this.operands.Length > 0) {
				builder.Append(this.operands[0].GetEquationString());

				for (int i = 1; i < this.operands.Length; i++) {
					builder.Append(",");
					builder.Append(this.operands[i].GetEquationString());
				}
			}

			builder.Append(')');
			return builder.ToString();
		}
	}

}
