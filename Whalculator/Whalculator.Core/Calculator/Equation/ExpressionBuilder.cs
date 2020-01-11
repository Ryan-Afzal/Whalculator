using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {

	/// <summary>
	/// Represents arguments passed during the generation of an <c>ISolvable</c>-based node graph
	/// </summary>
	public struct GenerationArgs {
		public IOperatorSet OperatorSet { get; set; }
		public IBuiltinFunctionOperationSet BuiltinFunctionOperationSet { get; set; }
		public char[,] BracketPairs { get; set; }
	}

	/// <summary>
	/// Contains methods to generate and simplify node graph expressions.
	/// </summary>
	public static class ExpressionBuilder {

		/// <summary>
		/// A simplified stack used by the generator methods
		/// </summary>
		private class ExpressionBuilderStack {

			private class ExpressionBuilderStackNode {

				public ExpressionBuilderStackNode(char c) {
					Value = c;
				}

				public char Value { get; }
				public ExpressionBuilderStackNode? Prev { get; set; }
			}

			private ExpressionBuilderStackNode? top;

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
				if (this.top is null) {
					throw new NotImplementedException();
				}

				char value = this.top.Value;
				this.top = this.top.Prev;
				Count--;
				return value;
			}

			public char Peek() {
				if (this.top is null) {
					throw new InvalidOperationException();
				}

				return this.top.Value;
			}

			public char PeekPrev() {
				if (this.top?.Prev is null) {
					throw new InvalidOperationException();
				}

				return this.top.Prev.Value;
			}

			public int Count { get; private set; }

			public bool IsEmpty {
				get {
					return Count == 0;
				}
			}

		}
		
		/// <summary>
		/// Gets the <c>ISolvable</c>-based node graph for the specified input text
		/// </summary>
		/// <param name="text"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static ISolvable GetSolvable(string text, GenerationArgs args) {
			text = text.Replace(" ", "");

			return ParseText(text, args).Simplify(new Simplifier[] {
				Simplifiers.SimplifyLevelOperators,
				Simplifiers.SimplifyTransformNegatives
			});
		}
		
		/// <summary>
		/// General-case recursive parse function for generating <c>ISolvable</c>-based nodes from text
		/// </summary>
		/// <param name="text"></param>
		/// <param name="args"></param>
		/// <returns></returns>
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

				if (IsOpenBracket(c, args)) {
					stack.Push(c);
					isNumeric = false;
					isAlphanumeric = false;
				} else if (IsCloseBracket(c, args)) {
					isNumeric = false;
					isAlphanumeric = false;
					if (!stack.IsEmpty && IsBracketPair(stack.Peek(), c, args)) {
						stack.Pop();
					} else {
						throw new MalformedEquationException(ErrorCode.MismatchedParentheses);
					}
				} else if (stack.IsEmpty) {
					if (isAlphanumeric && !(char.IsDigit(c) || c == '.')) {
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
							ISolvable[] _args = SeparateArgumentsBySeparator(text[(i + 1)..^1], ',', args);

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

		/// <summary>
		/// Separates arguments by a separator, with regards to brackets
		/// </summary>
		/// <param name="input"></param>
		/// <param name="separator"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private static ISolvable[] SeparateArgumentsBySeparator(string input, char separator, GenerationArgs args) {
			if (input.Equals("")) {
				return new ISolvable[] { };
			}

			LinkedList<string> list = new LinkedList<string>();
			ExpressionBuilderStack stack = new ExpressionBuilderStack();
			
			int last = 0;
			for (int k = 0; k < input.Length; k++) {
				char c = input[k];

				if (IsOpenBracket(c, args)) {
					stack.Push(c);
				} else if (IsCloseBracket(c, args)) {
					if (!stack.IsEmpty && IsBracketPair(stack.Peek(), c, args)) {
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

		/// <summary>
		/// Invokes a <c>Simplifier</c> on the specified <c>ISolvable</c> and all its child nodes
		/// </summary>
		/// <param name="solvable"></param>
		/// <param name="simplifier"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Continuously applies the specified simplifiers to the specified <c>ISolvable</c> until the node graph does not change
		/// </summary>
		/// <param name="solvable"></param>
		/// <param name="simplifiers"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Returns true if the input character is an open bracket
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private static bool IsOpenBracket(char c, GenerationArgs args) {
			for (int i = 0; i < args.BracketPairs.Length / 2; i++) {
				if (c == args.BracketPairs[i, 0]) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Returns true if the input character is a close bracket
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private static bool IsCloseBracket(char c, GenerationArgs args) {
			for (int i = 0; i < args.BracketPairs.Length / 2; i++) {
				if (c == args.BracketPairs[i, 1]) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Returns true if the specified input characters are an open and close bracket pair, respectively.
		/// </summary>
		/// <param name="c1"></param>
		/// <param name="c2"></param>
		/// <returns></returns>
		private static bool IsBracketPair(char c1, char c2, GenerationArgs args) {
			for (int i = 0; i < args.BracketPairs.Length / 2; i++) {
				if (c1 == args.BracketPairs[i, 0] && c2 == args.BracketPairs[i, 1]) {
					return true;
				}
			}

			return false;
		}
	}
}
