using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {

	public abstract class Simplifier {

		internal Simplifier? Next { get; set; }

		public abstract ISolvable Invoke(ISolvable solvable);

	}

}