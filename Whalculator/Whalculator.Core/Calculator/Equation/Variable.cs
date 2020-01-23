using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	
	/// <summary>
	/// Represents a reference to a variable, such as <c>x</c>, or a constant such as <c>e</c>
	/// </summary>
	public sealed class Variable : ISolvable {

		public Variable(string variableName) {
			VariableName = variableName;
		}

		public string VariableName { get; }

		public ISolvable Clone() {
			return new Variable(VariableName);
		}

		public async Task<IResult> GetResultValueAsync(ExpressionEvaluationArgs args) {
			if (args.Args.ArgNames.ContainsKey(VariableName)) {
				var arg = args.Args.Args[args.Args.ArgNames[VariableName]];
				//args.Args = new FunctionArgumentArgs() { ArgNames = new Dictionary<string, int>() };
				return await arg.GetResultValueAsync(args);
			} else if (args.VariableSet.IsVariable(VariableName)) {
				return await args.VariableSet[VariableName].GetResultValueAsync(args);
			} else {
				throw new InvalidEquationException(ErrorCode.NonexistentVariable);
			}
		}

		public async Task<ISolvable> GetExactValueAsync(ExpressionEvaluationArgs args) {
			if (args.Args.ArgNames.ContainsKey(VariableName)) {
				var arg = args.Args.Args[args.Args.ArgNames[VariableName]];
				//args.Args = new FunctionArgumentArgs() { ArgNames = new Dictionary<string, int>() };
				return await arg.GetExactValueAsync(args);
			} else if (args.VariableSet.IsVariable(VariableName)) {
				return await args.VariableSet[VariableName].GetExactValueAsync(args);
			} else {
				throw new InvalidEquationException(ErrorCode.NonexistentVariable);
			}
		}

		public string GetEquationString() {
			return VariableName;
		}

		public override bool Equals(object? obj) {
			if (obj is Variable v && v.VariableName.Equals(VariableName)) {
				return true;
			} else {
				return false;
			}
		}

		public override int GetHashCode() {
			return HashCode.Combine(VariableName);
		}
	}
}
