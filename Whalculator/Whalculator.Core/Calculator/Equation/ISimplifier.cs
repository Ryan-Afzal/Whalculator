﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Calculator.Equation {
	public interface ISimplifier {
		ISolvable Simplify(ISolvable solvable);
	}
}