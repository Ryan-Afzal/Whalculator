using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface ISimplifierHook {
		/// <summary>
		/// Marks the node graph as modified, meaning that it will be simplified again.
		/// </summary>
		public void Modified();
	}
}
