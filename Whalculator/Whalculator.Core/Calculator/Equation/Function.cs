using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {

	/// <summary>
	/// Arguments used in evaluating functions during expression evaluation.
	/// </summary>
	public struct FunctionArgumentArgs {
		public Dictionary<string, int> ArgNames { get; internal set; }
		public ISolvable[] Args { get; internal set; }
	}

	/// <summary>
	/// Represents a reference to a function, such as <c>f(x)</c>, or <c>def(ax, by)</c>
	/// </summary>
	public sealed class Function : NestedSolvable {

		public Function(string name, ISolvable[] args) : this(name, 0, args) {
			
		}

		public Function(string name, int deg, ISolvable[] args) : base(args) {
			Name = name;
			DifferentiationDegree = deg;
		}

		public string Name { get; }
		public int DifferentiationDegree { get; }

		public override ISolvable Clone() {
			return new Function(Name, DifferentiationDegree, this.CloneOperands());
		}

		public override async Task<IResult> GetResultValueAsync(ExpressionEvaluationArgs args) {
			ISolvable[] _args = await this.EvaluateOperandsResult(args);
			var info = args.FunctionSet.GetFunction(Name);

			ISolvable solvable = info.Function.Clone();

			if (DifferentiationDegree > 0) {
				var x = info.ArgNames.Keys.GetEnumerator();
				x.MoveNext();
				string argName = x.Current;

				for (int i = 0; i < DifferentiationDegree; i++) {
					solvable = await solvable.GetDerivativeAsync(argName, false);
				}
			}

			int k = -1;
			for (int i = 0; i < _args.Length; i++) {
				if (_args[i] is List) {
					if (k == -1) {
						k = i;
					} else {
						throw new ArgumentException("Cannot operate on two lists");
					}
				}
			}

			if (k == -1) {
				args.Args = new FunctionArgumentArgs() {
					ArgNames = info.ArgNames,
					Args = _args
				};

				return await solvable.GetResultValueAsync(args);
			} else {
				List l = (List)_args[k];
				ISolvable[] output = new ISolvable[l.operands.Length];

				for (int i = 0; i < output.Length; i++) {
					ISolvable[] __args = new ISolvable[_args.Length];
					Array.Copy(_args, __args, __args.Length);
					__args[k] = l.operands[i];

					args.Args = new FunctionArgumentArgs() {
						ArgNames = info.ArgNames,
						Args = __args
					};

					output[i] = await solvable.GetResultValueAsync(args);
				}

				return new List(output);
			}
		}

		public override string GetEquationString() {
			StringBuilder builder = new StringBuilder();
			builder.Append(Name);

			for (int i = 0; i < DifferentiationDegree; i++) {
				builder.Append('\'');
			}

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
