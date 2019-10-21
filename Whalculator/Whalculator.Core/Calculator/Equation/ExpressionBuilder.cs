﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public static class ExpressionBuilder {

		private struct GenerationArgs {
			public IOperatorSet OperatorSet { get; set; }
			public IBuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; set; }
		}

		public static ISolvable GetSolvableFromText(string text) {
			GenerationArgs args = new GenerationArgs() {
				OperatorSet = new OperatorSet(),
				BuiltinFunctionOperationSet = new BuiltinFunctionOperationSet()
			};

			return GetSolvableFromText(text, args);
		}

		private static ISolvable GetSolvableFromText(string text, GenerationArgs args) {
			if (text.Length == 0) {
				return new Literal(0.0);
			}

			char op = (char)0;//Last operation
			int index = -1;//Index of last operator

			int pDepth = 0;//The depth of parenthetical nesting that the function is going into
			int oDepth = 0;//The depth of operator nesting that the function is going into
			int fDepth = 0;//The depth of functional nesting that the function is going into

			for (int i = 0; i < text.Length; i++) {
				char c = text[i];//Get the current character

				if (c == '(') {//Open Parentheses
					if (fDepth >= pDepth && (i <= 0 
						|| args.OperatorSet.IsOperator(text[i - 1]) 
						|| text[i - 1] == '(' 
						|| text[i - 1] == ')')) {//In function
						fDepth++;
					}

					pDepth++;
				} else if (c == ')') {//Close Parentheses
					pDepth--;

					if (fDepth > pDepth) {//In function
						fDepth--;
					}
				} else if (fDepth >= pDepth && args.OperatorSet.IsOperator(c)) {//Operator
					if (index == -1 || (pDepth < oDepth || (pDepth == oDepth && args.OperatorSet.GetOperation(op).Order >=
						args.OperatorSet.GetOperation(c).Order))) {//Get the operator

						oDepth = pDepth;
						op = c;
						index = i;
					}
				}

				if (pDepth < 0) {//Mismatched Parentheses - Too many close-parens
					throw new InvalidEquationException(ErrorCode.MismatchedParentheses);
				}
			}

			if (pDepth != 0) {//Mismatched parentheses - Too many parentheses
				throw new InvalidEquationException(ErrorCode.MismatchedParentheses);
			}

			if (index == -1) {
				if (text[0] == '(') {
					return GetSolvableFromText(text[1..^2], args);
				} else {
					bool isVar = false;
					for (int i = 0; i < text.Length; i++) {
						if (!(char.IsDigit(text[i]) || text[i] == '.')) {
							isVar = true;
						}

						if (text[i] == '(') {
							//Function
							string name = text.Substring(0, i);
							ISolvable[] _args = SeparateFunctionArguments(text[(i + 1)..(text.LastIndexOf(')'))], args);

							if (args.BuiltinFunctionOperationSet.IsBuiltinFunctionOperation(name)) {//Built-in 'special' function
								return new BuiltinFunction(args.BuiltinFunctionOperationSet[name], _args);
							}/* else {//User-defined function
								FunctionInfo info = args.Functions.GetFunction(name);
								info.Function = info.Function.Clone();
								return new Function(info, _args);
							}*/
						}
					}

					if (isVar) {//Variable
						return new Variable(text);
					} else {//Literal value
						return new Literal(double.Parse(text));
					}
				}
			} else if (oDepth != 0) {
				return GetSolvableFromText(text[1..^1], args);
			} else {
				return new Operator(args.OperatorSet.GetOperation(op),
					GetSolvableFromText(text.Substring(0, index), args),
					GetSolvableFromText(text.Substring(index + 1), args));
			}
		}

		private static ISolvable[] SeparateFunctionArguments(string input, GenerationArgs args) {
			if (input.Equals("")) {
				return new ISolvable[] { };
			}

			LinkedList<string> list = new LinkedList<string>();
			char[] arr = input.ToCharArray();

			int pDepth = 0;
			int last = 0;
			for (int k = 0; k < arr.Length; k++) {
				if (arr[k] == '(') {
					pDepth++;
				} else if (arr[k] == ')') {
					pDepth--;
				} else {
					if (arr[k] == ',' && pDepth == 0) {
						list.AddLast(input[last..k]);
						last = k + 1;
					}
				}
			}

			list.AddLast(input.Substring(last));

			ISolvable[] parts = new ISolvable[list.Count];
			int i = 0;
			foreach (string s in list) {
				parts[i] = GetSolvableFromText(s, args);
				i++;
			}

			return parts;
		}

	}
}
