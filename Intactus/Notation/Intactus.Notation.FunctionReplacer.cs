
//
//  Intactus.Notation.FunctionReplacer.cs
//
//  Created by Pouya Kary on 2015/4/24
//  Copyright (c) 2015 Pouya Kary. All rights reserved.
//


using System;
using System.Text.RegularExpressions;

namespace Kary.Intactus
{
	public partial class Notation
	{
		/// <summary>
		/// Standardizes the input for the engine
		/// </summary>
		/// <returns>The replacer.</returns>
		/// <param name="text">Text.</param>
		private static string FunctionReplacer (string text) {

			text = text.Replace ('(', '[').Replace (')', ']').Replace ('*', '×');
			text = ' ' + text.TrimEnd ().TrimStart () + ' ';

			string[] functions = { "sqrt", "abs", "floor"};
			string[] intactus_notation = { "√", "⦗", "⎣"};

			for (int i = 0; i < functions.Length; i++) {

				foreach (var match in Regex.Matches (text, functions[i] + " *\\[")) {

					text = text.Replace (match.ToString (), intactus_notation [i]);

				}

			}

			return text;
		}
	}
}

