//
//  Kary.Intactus.Wrapper.Shapes.cs
//
//  Created by Pouya Kary on 2015/4/17
//  Copyright (c) 2015-2016 Pouya Kary. All rights reserved.
//


using System;
using Kary.Text;

namespace Kary.Intactus
{
    /* ───────────────────────────────────────────────────────────────── *
	 * :::::::::: I N T A C T U S   E N G I N E   S H A P E S :::::::::: *
	 * ───────────────────────────────────────────────────────────────── */

    public partial class Notation
    {
        private enum Box {
			
            Brackets,
            Abs,
            Floor

        }



        /// <summary>
        /// Creates a string box around a fixed size string
        /// </summary>
        /// <returns>The maker.</returns>
        /// <param name="input">Input.</param>
        /// <param name="box_option">Box_option.</param>
        private static string BoxMaker ( string input , Box box_option )
        {
			char topleft, topright, bottomleft, bottomright, left, right;

            switch ( box_option ) {
				
                case Box.Abs:
                    topleft = '⎜';
                    topright = '⎟';
                    bottomleft = '⎜';
                    bottomright = '⎟';
                    left = '⎜';
                    right = '⎟';
                    break;

                case Box.Floor:
                    topleft = '⎜';
                    topright = '⎟';
                    bottomleft = '⎣';
                    bottomright = '⎦';
                    left = '⎜';
                    right = '⎟';
                    break;

                default:
                    topleft = '┌';
                    topright = '┐';
                    bottomleft = '└';
                    bottomright = '┘';
					left = '│';
					right = '│';
                    break;
            }


            //
            // THE SPECIAL CASE OF [ ] AND SO...
            //

            if ( input.Split ( '\n' ).Length == 1 && input.Replace ( "[" , "" ) == input
                && input.Replace ( "⎪" , "" ) == input && input.Replace ( '⎣' , ' ' ) == input ) {

                switch ( box_option )
                {
					case Box.Brackets:
						return '[' + input + ']';

                    case Box.Abs:
                        return '⎪' + input + '⎪';

                    case Box.Floor:
                        return '⎣' + input + '⎦';

                    default:
                        return input;
                }
            }


            //
            // THE ORIGINAL WAY
            //

            var result = "";
            var max_line_size = 0;
			
            foreach ( var line in input.Split ( '\n' ) ) {
				
                if ( line.Length > max_line_size ) {
					
					max_line_size = line.Length;

                }
            }


            //
            // MAKING THE BOX
            //

			result += topleft + Utilities.Repeat ( " " , max_line_size ) + topright + '\n';

            foreach ( var line in input.Split ( '\n' ) ) {
				
				result += left + line.PadRight ( max_line_size ) + right + '\n';

            }

			result += bottomleft + Utilities.Repeat ( " " , max_line_size ) + bottomright;


            //
            // DONE
            //

            return result;
        }



        /// <summary>
        /// Creates a Radical
        /// </summary>
        /// <returns>The maker.</returns>
        /// <param name="text">Text.</param>
        private static string RadicalMaker ( string text )
        {
            string result = "";
			var lines = text.Split ( '\n' );


            //
            // MAKES THE BODY OF THE SQRT. YOU THE LINES AND SO
            //

            for ( int i = 0 ; i < lines.Length ; i++ ) {
				
                if ( i == lines.Length - 1 ) {
					
					result += "╲╱" + Utilities.Repeat ( " " , lines.Length - 1 )
					+ lines [ i ] + '\n';

                } else {
					
					result += Utilities.Repeat ( " " , lines.Length - i )
					+ '╱' + Utilities.Repeat ( " " , i )
					+ lines [ i ] + '\n';
                }
            }


            //
            // UPPERLINE OF THE RADICAL
            //

			result = Utilities.Repeat ( " " , lines.Length + 1 )
			+ Utilities.Repeat ( "_" , Utilities.LongestLine ( text ) )
			+ '\n' + result;


            //
            // IT MAKES THE PALACE IN BOX OT MAKE SOME KIND OF
            // BOTTOM EMPTY LINE UDNDER THE SQRT. MAGIC IT IS
            // 

            result += " ";


            //
            // PLACES THE WHOLE THING IN A BOX SO IT SOMEHOW
            // ADDS SPACE SO IT CAN BE ABLE TO CONCATENATE IN
            // MULTI LINE WAY AND THEN IT RETURNS IT
            //

			return Utilities.PlaceAtBox ( Utilities.LongestLine ( result ) , countLines ( result ) , result );
        }



        private static int countLines ( string text )
        {
            int result = 1;
			
            foreach ( var letter in text ) {
				
                if ( letter == '\n' ) {
					
                    result++;

                }
            }
			
            return result;
        }



        /// <summary>
        /// Creates a divide
        /// </summary>
        /// <returns>The maker.</returns>
        /// <param name="numerator">Numerator.</param>
        /// <param name="denumerator">Denumerator.</param>
        private static string DivideMaker ( string numerator , string denumerator ) {
			
            string result = "";


            //
            // SIZES
            //

			int numerator_width = Utilities.LongestLine ( numerator );
			int denumerator_width = Utilities.LongestLine ( denumerator );


            //
            // UP AND DOWN MAKERS 
            //

			result += Utilities.PlaceAtCenter ( Math.Max ( numerator_width , denumerator_width ) , numerator );

			result += '\n' + Utilities.Repeat ( "─" , Math.Max ( numerator_width , denumerator_width ) ) + " \n";

			result += Utilities.PlaceAtCenter ( Math.Max ( numerator_width , denumerator_width ) , denumerator );


            //
            // RESULT
            //

            return result;
        }
    }
}