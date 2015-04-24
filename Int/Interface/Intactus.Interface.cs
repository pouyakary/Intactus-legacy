
//
//  Kary.Intactus.Interface.cs
//
//  Created by Pouya Kary on 2015/4/17
//  Copyright (c) 2015 Pouya Kary. All rights reserved.
//


using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Kary.Leopard;
using Kary.Leopard.Text;
using Kary.Intactus;
using Kary.Calculat;

namespace Int
{
	public partial class Interface
	{
		public static string Input (int io_counter) {

			string prompt = " In[" + Numerics.Roman (io_counter) + "]= ";

			Terminal.Cyan ();
			Terminal.Print ("\n" + prompt);

			Terminal.Reset ();
			var result = Terminal.TextBox (Terminal.Width - prompt.Length - 1, "", true);

			Terminal.Y--;
			Terminal.CleanLine ();

			InputRewriter (result, io_counter);

			result = result.Replace ("[", "(").Replace ("]", ")".Replace (" ",""));

			return result.ToLower ();
		}




		public static void InputRewriter (string rewriter, int io_counter) {

			string space_name = "";
			bool load = false;


			//
			// REMOVING SYSTEM COMMANDS FROM THE
			// START OF REWRITER
			//

			if (rewriter.Split ('?').Length == 2) {

				var parts = rewriter.Split ('?');
				space_name = parts [0];
				rewriter = parts [1];

			} else if (rewriter.StartsWith ("load ")) {

				load = true;
				rewriter = Utilities.RemoveFromStart (rewriter, "load ");
			}




			//
			// GENERATING THE REWRITER IN
			// THE DESIERED FORMAT
			//
		
			if (rewriter.Replace (" vs ","") != rewriter) {

				//
				// IN CASE OF VERSUS 
				//

				var rewriter_parts = rewriter.Replace ("vs", "&").Split ('&');

				string new_rewriter = "";

				for (int i = 0; i < rewriter_parts.Length - 1; i++) {

					new_rewriter = Utilities.Concatenate (new_rewriter, Notation.Generate (rewriter_parts [i]));
					new_rewriter = Utilities.Concatenate (new_rewriter, " vs ");

				}

				rewriter = Utilities.Concatenate (new_rewriter, Notation.Generate (rewriter_parts [rewriter_parts.Length - 1]));


			} else {

				//
				// IN CASE OF THE SIMPLE REWRITER
				//

				rewriter = Notation.Generate (rewriter);

			}


	

			//
			// SYSTEM COMMANDS WRAPPER
			//

			if (space_name != "") {

				rewriter = Utilities.Concatenate (space_name + " → ", rewriter);

			} else if (load) {

				rewriter = Utilities.Concatenate ("HistoryLoader ← ", rewriter);

			}


			var rewriter_lines = rewriter.Split ('\n');

			string prompt = "\n In[" + Numerics.Roman (io_counter) + "]= ";

			int max_possible_size = Terminal.Width - prompt.Length - 3 ;
			bool summarize = Utilities.LongestLine (rewriter) > max_possible_size;

			for (int i = 0; i < rewriter_lines.Length; i++) {

				if ((double)i == Math.Floor((double)rewriter_lines.Length  / 2)) {

					Terminal.Y--;

					Terminal.Cyan ();
					Terminal.Print (prompt);
					Terminal.Reset ();

					if (summarize) {

						Terminal.Print (rewriter_lines[i].Substring (0, max_possible_size - 1));
						Terminal.Cyan ();
						Terminal.PrintLn (" •••");
						Terminal.Reset ();

					} else {

						Terminal.PrintLn (rewriter_lines [i]);
	
					}
						
				} else {

					if (summarize) {

						Terminal.PrintLn (Utilities.Repeat (" ", prompt.Length - 1) + rewriter_lines [i].Substring (0, max_possible_size - 4));

					} else {

						Terminal.PrintLn (Utilities.Repeat (" ", prompt.Length - 1) + rewriter_lines [i]);
					}
				}
			}
		}



		/// <summary>
		/// Prints output in a single line form
		/// </summary>
		/// <param name="output">Output.</param>
		/// <param name="io_counter">Io_counter.</param>
		public static void PrintOut (string output, int io_counter) {

			Terminal.Magenta();
			Terminal.Print ("Out[" + Numerics.Roman (io_counter) + "]= ");
			Terminal.Reset ();
			Terminal.PrintLn (output);
			Terminal.NewLine ();

		}
	}
}