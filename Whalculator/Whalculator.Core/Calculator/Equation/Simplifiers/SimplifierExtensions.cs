using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation.Simplifiers {
	public static class SimplifierExtensions {

		public static ISimplificationProvider AddTransformNegativesSimplifier(this ISimplificationProvider provider) {
			return provider.AddSimplifier(new TransformNegativesSimplifier());
		}

		public static ISimplificationProvider AddLevelOperatorSimplifier(this ISimplificationProvider provider) {
			return provider.AddSimplifier(new LevelOperatorsSimplifier());
		}

	}
}
