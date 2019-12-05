using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	public struct GenerationArgs {
		public IOperatorSet OperatorSet { get; set; }
		public IBuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; set; }
		public IFunctionSet Functions { get; set; }
	}

	public static class ExpressionBuilder {
		
		private class ExpressionBuilderStack {

			private class ExpressionBuilderStackNode {

				public ExpressionBuilderStackNode(char c) {
					Value = c;
				}

				public char Value { get; }
				public ExpressionBuilderStackNode Prev { get; set; }
			}

			private ExpressionBuilderStackNode top;

			public ExpressionBuilderStack() {
				Count = 0;
				top = null;
			}

			public void Push(char c) {
				var node = new ExpressionBuilderStackNode(c) { Prev = this.top };
				this.top = node;
				Count++;
			}

			public char Pop() {
				char value = this.top.Value;
				this.top = this.top.Prev;
				Count--;
				return value;
			}

			public char Peek() {
				return this.top.Value;
			}

			public char PeekPrev() {
				return this.top.Prev.Value;
			}

			public int Count { get; private set; }

			public bool IsEmpty {
				get {
					return Count == 0;
				}
			}

		}
		
		public static ISolvable GetSolvable(string text, GenerationArgs args) {
			text = text.Replace(" ", "");

			return ParseText(text, args).Simplify(new Simplifier[] {
				Simplifiers.SimplifyLevelOperators,
				Simplifiers.SimplifyTransformNegatives
			});
		}

		[Obsolete]
		private static ISolvable GetSolvableFromText(string text, GenerationArgs args) {
			if (text.Length == 0) {
				return new Literal(0);
			}

			char op = default;//Last operation
			int index = -1;//Index of last operator

			int pDepth = 0;//The depth of parenthetical nesting that the function is going into
			int oDepth = 0;//The depth of operator nesting that the function is going into
			int fDepth = 0;//The depth of functional nesting that the function is going into

			for (int i = 0; i < text.Length; i++) {
				char c = text[i];//Get the current character

				if (c == '(' || c == '{' || c == '<') {//Open Parentheses
					if (fDepth >= pDepth && (i <= 0
						|| args.OperatorSet.IsOperator(text[i - 1])
						|| text[i - 1] == '('
						|| text[i - 1] == ')')) {//In function
						fDepth++;
					}

					pDepth++;
				} else if (c == ')' || c == '}' || c == '>') {//Close Parentheses
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
					throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
				}
			}

			if (pDepth != 0) {//Mismatched parentheses - Too many open-parens
				throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
			}

			if (index == -1) {
				if (text[0] == '(') {
					return GetSolvableFromText(text[1..^1], args);
				} else if (text[0] == '{') {
					return new List(SeparateArgumentsBySeparator(text[1..^1], ',', args));
				} else if (text[0] == '<') {
					return new Vector(SeparateArgumentsBySeparator(text[1..^1], ',', args));
				} else {
					bool isVar = false;
					for (int i = 0; i < text.Length; i++) {
						if (!(char.IsDigit(text[i]) || text[i] == '.')) {
							isVar = true;
						}

						if (text[i] == '(') {
							//Function
							string name = text.Substring(0, i);
							ISolvable[] _args = SeparateArgumentsBySeparator(text[(i + 1)..(text.LastIndexOf(')'))], ',', args);

							if (args.BuiltinFunctionOperationSet.IsBuiltinFunctionOperation(name)) {//Built-in 'special' function
								return new BuiltinFunction(args.BuiltinFunctionOperationSet[name], _args);
							} else {//User-defined function
								if (name[^1] == '\'') {//f' notation for derivatives
									int fnIndex = 2;
									for (; fnIndex < name.Length; fnIndex++) {
										if (name[^fnIndex] != '\'') {
											break;
										}
									}

									string fnName = name[0..^fnIndex];

									if (args.Functions.ContainsFunction(fnName)) {
										FunctionInfo info = args.Functions.GetFunction(name);

										if (info.ArgNames.Count > 1) {
											throw new InvalidEquationException(ErrorCode.MultivariableDifferentiation);
										}

										info.Function = info.Function.Clone();

										string ind = info.ArgNames.Keys.GetEnumerator().Current;
										for (int d = 0; d < fnIndex; d++) {
											info.Function = info.Function.GetDerivative(ind);
										}

										throw new NotImplementedException();
									}
								} else {
									if (args.Functions.ContainsFunction(name)) {
										FunctionInfo info = args.Functions.GetFunction(name);
										info.Function = info.Function.Clone();
										return new Function(info.Name, _args);
									}
								}

								throw new InvalidEquationException(ErrorCode.NonexistentFunction);
							}
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

		private static ISolvable ParseText(string text, GenerationArgs args) {
			if (text.Length == 0) {
				return new Literal(0);
			}

			char op = default;//Last operation
			int index = -1;//Index of last operator

			bool isNumeric = true;
			bool isAlphanumeric = true;

			ExpressionBuilderStack stack = new ExpressionBuilderStack();//Bracket stack

			for (int i = 0; i < text.Length; i++) {
				char c = text[i];

				if (c.IsOpenBracket()) {
					stack.Push(c);
					isNumeric = false;
					isAlphanumeric = false;
				} else if (c.IsCloseBracket()) {
					isNumeric = false;
					isAlphanumeric = false;
					if (IsBracketPair(stack.Peek(), c)) {
						stack.Pop();
					} else {
						throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
					}
				} else if (stack.IsEmpty) {
					if (isAlphanumeric && !char.IsDigit(c)) {
						isNumeric = false;
						if (!char.IsLetter(c)) {
							isAlphanumeric = false;
						}
					}
					
					if (args.OperatorSet.IsOperator(c)) {
						isNumeric = false;
						isAlphanumeric = false;
						if (index == -1 || args.OperatorSet.GetOperation(op).Order >= args.OperatorSet.GetOperation(c).Order) {
							op = c;
							index = i;
						}
					}
				}
			}

			if (!stack.IsEmpty) {
				throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
			}

			if (index == -1) {
				if (isAlphanumeric) {// Variable/Literal
					if (isNumeric) {// Literal
						return new Literal(double.Parse(text));
					} else {// Variable
						return new Variable(text);
					}
				} else if (text[0] == '(') {// Entire expression is enclosed in parentheses
					return ParseText(text[1..^1], args);
				} else if (text[0] == '{') {// List
					return new List(SeparateArgumentsBySeparator(text[1..^1], ',', args));
				} else if (text[0] == '<') {// Vector
					return new Vector(SeparateArgumentsBySeparator(text[1..^1], ',', args));
				} else if (text[0] == '[') {// Matrix (Unimplemented)
					throw new NotImplementedException();
				} else {// Function
					int d = 0;
					for (int i = 0; i < text.Length; i++) {
						char c = text[i];

						if (c == '(') {
							string name = text[0..(i - d)];
							ISolvable[] _args = SeparateArgumentsBySeparator(text[i..^0], ',', args);

							if (args.BuiltinFunctionOperationSet.IsBuiltinFunctionOperation(name)) {// Builtin Function
								return new BuiltinFunction(args.BuiltinFunctionOperationSet[name], _args);
							} else {// Function
								return new Function(name, d, _args);
							}
						} else if (c == '\'') {
							d++;
						}
					}

					throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
				}
			} else {
				return new Operator(args.OperatorSet.GetOperation(op),
					ParseText(text[0..index], args),
					ParseText(text[(index + 1)..^0], args));
			}
		}

		private static ISolvable[] SeparateArgumentsBySeparator(string input, char separator, GenerationArgs args) {
			if (input.Equals("")) {
				return new ISolvable[] { };
			}

			LinkedList<string> list = new LinkedList<string>();
			ExpressionBuilderStack stack = new ExpressionBuilderStack();
			
			int last = 0;
			for (int k = 0; k < input.Length; k++) {
				char c = input[k];

				if (c.IsOpenBracket()) {
					stack.Push(c);
				} else if (c.IsCloseBracket()) {
					if (IsBracketPair(stack.Peek(), c)) {
						stack.Pop();
					} else {
						throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
					}
				} else if (stack.IsEmpty) {
					if (c == separator) {
						list.AddLast(input[last..k]);
						last = k + 1;
					}
				}
			}

			list.AddLast(input[last..^0]);

			ISolvable[] parts = new ISolvable[list.Count];
			int i = 0;
			foreach (string s in list) {
				parts[i] = ParseText(s, args);
				i++;
			}

			return parts;
		}

		private static ISolvable Simplify(ISolvable solvable, Simplifier simplifier) {
			if (solvable is NestedSolvable nested) {
				for (int i = 0; i < nested.operands.Length; i++) {
					nested.operands[i] = Simplify(nested.operands[i], simplifier);
				}

				return simplifier.Invoke(nested);
			} else {
				return simplifier.Invoke(solvable);
			}
		}

		public static ISolvable Simplify(this ISolvable solvable, IEnumerable<Simplifier> simplifiers) {
			ISolvable s = solvable;
			string str = "";

			while (!str.Equals(s.GetEquationString())) {
				str = s.GetEquationString();

				foreach (var sim in simplifiers) {
					s = Simplify(s, sim);
				}
			}

			return s;
		}

		private static bool IsOpenBracket(this char c) {
			return c == '('
				|| c == '{'
				|| c == '<'
				|| c == '[';
		}

		private static bool IsCloseBracket(this char c) {
			return c == ')'
				|| c == '}'
				|| c == '>'
				|| c == ']';
		}

		private static bool IsBracketPair(char c1, char c2) {
			return c1 switch
			{
				'(' => c2 == ')',
				'{' => c2 == '}',
				'<' => c2 == '>',
				'[' => c2 == ']',
				_ => false,
			};
		}
	}
}
