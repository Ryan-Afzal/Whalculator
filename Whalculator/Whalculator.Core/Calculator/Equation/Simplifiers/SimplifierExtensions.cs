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

		public static ISimplificationProvider AddRationalExpressionsSimplifier(this ISimplificationProvider provider) {
			return provider.AddSimplifier(new RationalExpressionsSimplifier());
		}

		public static ISimplificationProvider AddRemoveZerosOnesSimplifier(this ISimplificationProvider provider) {
			return provider.AddSimplifier(new RemoveZerosOnesSimplifier());
		}

		public static ISimplificationProvider AddCollectLikeTermsSimplifier(this ISimplificationProvider provider) {
			return provider.AddSimplifier(new CollectLikeTermsSimplifier());
		}

		public static ISimplificationProvider AddNegativeExponentsSimplifier(this ISimplificationProvider provider) {
			return provider.AddSimplifier(new NegativeExponentsSimplifier());
		}

	}
}
