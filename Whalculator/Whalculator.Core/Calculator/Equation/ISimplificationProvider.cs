using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whalculator.Core.Calculator.Equation {
	public interface ISimplificationProvider {
		public Task<ISolvable> SimplifyAsync();
		public ISimplificationProvider AddSimplifier(Simplifier simplifier);
	}
}
