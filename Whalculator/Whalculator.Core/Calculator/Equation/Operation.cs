using System;
using System.Collections;
using System.Collections.Generic;

namespace Whalculator.Core.Calculator.Equation {

	public delegate ISolvable ExactValueOperation(ISolvable[] operands);

	public delegate double DoubleValueOperation(ISolvable[] operands);

}