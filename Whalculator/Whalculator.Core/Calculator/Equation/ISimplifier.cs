using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	public interface ISimplifier {
		/// <summary>
		/// Simplifies the provided <see cref="ISolvable"/>.
		/// It returns the simplified solvable along with whether to pass the object to the next <see cref="ISimplifier"/> in the sequence.
		/// </summary>
		/// <param name="solvable"></param>
		/// <returns></returns>
		public Task<(ISolvable, bool)> Simplify(ISolvable solvable);
	}
}
