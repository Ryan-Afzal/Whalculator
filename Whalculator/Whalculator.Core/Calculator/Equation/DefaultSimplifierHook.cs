using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public sealed class DefaultSimplifierHook : ISimplifierHook {

		public bool IsModified { get; internal set; }

		public void Modified() {
			IsModified = true;
		}

	}
}
