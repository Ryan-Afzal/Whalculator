using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public struct FunctionArgumentArgs {
		public Dictionary<string, int> ArgNames { get; internal set; }
		public ISolvable[] Args { get; internal set; }
	}

	public sealed class Function : NestedSolvable {

		public Function(FunctionInfo info, ISolvable[] args) : base(args) {
			Info = info;
		}

		public FunctionInfo Info { get; }

		public override ISolvable Clone() {
			return new Function(Info, this.CloneOperands());
		}

		public override ISolvable GetExactValue(ExpressionEvaluationArgs args) {
			ISolvable[] _args = this.EvaluateOperands(args);

			args.Args = new FunctionArgumentArgs() {
				ArgNames = Info.ArgNames,
				Args = _args
			};

			return Info.Function.GetExactValue(args);
		}

		public override double GetDoubleValue(ExpressionEvaluationArgs args) {
			ISolvable[] _args = this.EvaluateOperands(args);

			args.Args = new FunctionArgumentArgs() {
				ArgNames = Info.ArgNames,
				Args = _args
			};

			return Info.Function.GetExactValue(args).GetDoubleValue(args);
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();
			builder.Append(Info.Name);
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

		private ISolvable[] EvaluateOperands(ExpressionEvaluationArgs args) {
			ISolvable[] output = new ISolvable[this.operands.Length];

			for (int i = 0; i < output.Length; i++) {
				output[i] = this.operands[i].GetExactValue(args);
			}

			return output;
		}
	}

}
