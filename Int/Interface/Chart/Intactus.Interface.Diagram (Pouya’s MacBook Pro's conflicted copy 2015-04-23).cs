
//
//  Intactus.Interface.Diagram.cs
//
//  Created by Pouya Kary on 2015/4/23
//  Copyright (c) 2015 Pouya Kary. All rights reserved.
//


using System;
using Kary.Leopard;
using Kary.Leopard.Text;

namespace Kary.Intactus
{
	public partial class Interface
	{
		public static void PrintDiagrams (string[] inputs, int io_counter)
		{
			//
			// CREATING THE MAIN VIEW
			//

			int start_x = Terminal.X;
			int start_y = Terminal.Y;


			//
			// GENERATING THE SUTABLE NUMBERS
			//

			double[] members = new double[inputs.Length];

			for (int index_counter = 0; index_counter < inputs.Length; index_counter++) {

				members [index_counter] = Calculate (inputs [index_counter]);

			}


			//
			// GENERATING THE MAX SIZE
			// 

			max_size = members [0];

			foreach (var member in members) {
				if (member > max_size) {
					max_size = member;
				}
			}


			//
			// GENERATING THE DIAGRAM
			//

			string diagram = CreateDiagram (members);



			//
			// GENERATING STATICS
			//

			string statics = "\n";

			int i = 1;

			foreach (var mem in members) {

				if (mem == double.MaxValue) {

					statics += " " + Numerics.Roman (i).PadLeft (5) + ": Infinity\n";

				} else {

					statics += " " + Numerics.Roman (i).PadLeft (5) + ": " + mem + '\n';

				}

				i++;
				
			}
				
			diagram = Utilities.Concatenate (diagram, statics);



			//
			// PRINTING THE OUTPUT
			//
		
			string[] lines = diagram.Split ('\n');
			string prompt = "Out[" + Numerics.Roman (io_counter) + "]= ";

			for (int index_counter = 0; index_counter < lines.Length; index_counter++) {

				if ((double)index_counter == Math.Floor ((double)lines.Length / 2)) {

					Terminal.Magenta();
					Terminal.Print ("Out[" + Numerics.Roman (io_counter) + "]= ");
					Terminal.Reset ();
					Terminal.PrintLn (lines[index_counter]);

				} else {

					Terminal.PrintLn (
						Utilities.Repeat (" ", prompt.Length) +
						lines[index_counter]
					);
				}
			}
		}


		private static double max_size = 0;
		private static int height_size = 15;


		private static string CreateDiagram (double[] inputs) {
		
			//
			// FIRST PART, THE L OF THE DIAGRAM
			//

			string result = Utilities.Repeat ("│  \n", height_size) + "└──\n" + Utilities.Repeat (" ", 3);


			//
			// NOW ADDING EACH PART
			//

			for (int index_counter = 0; index_counter < inputs.Length; index_counter++) {

				result = Utilities.Concatenate (result, DiagramPart (inputs[index_counter], index_counter + 1));

			}
				
			return result;
		}




		public static string DiagramPart (double size, int index_number) {

			string result = "";

			int size_in_lines = (int)(Math.Floor ((size / max_size) * 14));

			if (size_in_lines < 1) {

				//
				// THE UPPER EMPTY
				// 

				for (int i = 0; i < height_size; i++) {

					result += Utilities.Repeat (" ", 5) + '\n';

				}

				result += "─────\n " + index_number + "   ";



			} else {

				//
				// THE UPPER EMPTY
				// 

				for (int i = 0; i < height_size - size_in_lines; i++) {

					result += Utilities.Repeat (" ", 5) + '\n';

				}


				//
				// THE TOP OF THE THING
				//

				result += "┌─┐  \n";


				//
				// NOW THE REST OF THE BOX
				//

				for (int i = 0; i < size_in_lines - 1; i++) {

					result += "│ │  \n";

				}


				//
				// AND FINALLY THE BOTTOM
				// 

				result += "┴─┴──\n " + index_number + "   ";


			}
		

			return result;
		}



		/// <summary>
		/// Calculates a string reault of Calculat
		/// </summary>
		/// <param name="inputs">Inputs.</param>
		private static double Calculate (string input) {

			switch (input) {

			case "False":
			case "True":
			case "Operation Failure":
				return 0;

			case "Infinity":
				return double.MaxValue;

			default:
				return double.Parse (input);
			}
		}
	}
}

