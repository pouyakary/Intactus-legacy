
//
//  Intactus.Charts.ColumnCharts.cs
//
//  Created by Pouya Kary on 2015/4/24
//  Copyright (c) 2015 Pouya Kary. All rights reserved.
//


using System;
using Kary.Text;

namespace Kary.Intactus
{
    public partial class Charts
    {
        private static double max_size = 0;
        private static int height_size = 15;


        /// <summary>
        /// Generates a ColumnCharts
        /// </summary>
        /// <returns>The chart.</returns>
        /// <param name="inputs">Inputs.</param>
        public static string ColumnChart ( double [] inputs , int height ) {
			
            height_size = height;
			return CreateColumnChart ( inputs );

        }



        /// <summary>
        /// Generates the mian chart
        /// </summary>
        /// <returns>The diagram.</returns>
        /// <param name="inputs">Inputs.</param>
        private static string CreateColumnChart ( double [] inputs ) {
			
            //
            // GENERATING THE MAX SIZE
            //

			max_size = inputs [ 0 ];

            foreach ( var member in inputs ) {
				
                if ( member > max_size ) {
					
                    max_size = member;

                }
            }


            //
            // FIRST PART, THE L OF THE CHART
            //

			string result = Utilities.Repeat ( "│  \n" , height_size ) + "└──\n" + Utilities.Repeat ( " " , 3 );


            //
            // NOW ADDING EACH PART
            //

            for ( int index_counter = 0 ; index_counter < inputs.Length ; index_counter++ ) {
				
				result = Utilities.Concatenate ( result , ColumnMaker ( inputs [ index_counter ] , index_counter + 1 ) );

            }

            return result;
        }



        /// <summary>
        /// Draws each one the chart strockes
        /// </summary>
        /// <returns>The part.</returns>
        /// <param name="size">Size.</param>
        /// <param name="index_number">Index_number.</param>
        public static string ColumnMaker ( double size , int index_number )
        {
            string result = "";
			int size_in_lines = ( int ) ( Math.Floor ( ( size / max_size ) * ( height_size - 1 ) ) );

            if ( size_in_lines < 1 ) {
				
                //
                // THE UPPER EMPTY
                // 

                for ( int i = 0 ; i < height_size ; i++ ) {
					
					result += Utilities.Repeat ( " " , 5 ) + '\n';

                }

				result += "─────\n " + index_number + "   ";

            } else {
				
                //
                // THE UPPER EMPTY
                // 

                for ( int i = 0 ; i < height_size - size_in_lines ; i++ ) {
					
					result += Utilities.Repeat ( " " , 5 ) + '\n';

                }


                //
                // THE TOP OF THE THING
                //

				result += "┌─┐  \n";


                //
                // NOW THE REST OF THE BOX
                //

                for ( int i = 0 ; i < size_in_lines - 1 ; i++ ) {
					
                    result += "│ │  \n";

                }


                //
                // AND FINALLY THE BOTTOM
                // 

				result += "┴─┴──\n "
				+ index_number
				+ Utilities.Repeat ( " " , 4 - index_number.ToString ().Length );
				
            }

            return result;
        }
    }
}