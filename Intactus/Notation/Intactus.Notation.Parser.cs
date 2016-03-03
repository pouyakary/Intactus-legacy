
//
//  Kary.Intactus.Wrapper.Parser.cs
//
//  Created by Pouya Kary on 2015/4/17
//  Copyright (c) 2015-2016 Pouya Kary. All rights reserved.
//


using Kary.Text;

namespace Kary.Intactus
{
    /* ───────────────────────────────────────────────────────────────── *
	 * :::::::::: I N T A C T U S   E N G I N E   P A R S E R :::::::::: *
	 * ───────────────────────────────────────────────────────────────── */

    public partial class Notation
    {
        /// <summary>
		/// This struct is designed to hold a string and also
		/// the place holder "i" as the last character of the
		/// string that we're reading
		/// </summary>
		private struct Formula
        {
            public int i;
            public string code;

            public Formula ( string input )
            {
                this.i = 0;
                this.code = input;
            }
        }



        private static string MasterWrapper ( string input )
        {
            //
            // FORMULA INIT / ALLOC
            //

			var formula = new Formula ( input );
			return FormulaWrapper ( ref formula , '[' );
        }



        private static string FormulaWrapper(ref Formula code, char open_command)
        {
			return LevelTwoWrapper ( ref code , open_command );
        }



        private static string DivideDiveString ( string text )
        {
            //
            // SPACES
            //

			string p1 = "", p2 = "", result = "";
			int divider_place_holder = 0;
			var lines = text.Split ( '\n' );


            //
            // FINDING THE PLACE OF /
            //

            string line_to_read = lines [ lines.Length / 2 ];
			
            for ( int i = 0 ; i < line_to_read.Length ; i++ ) {
				
                if ( line_to_read [ i ] == '/') {
					
                    divider_place_holder = i;
                    i = line_to_read.Length;

                }
            }


            //
            // FILLING THE P1
            //

            foreach ( var line in lines ) {
				
                for ( int i = 0 ; i < divider_place_holder ; i++ ) {
					
                    p1 += line [ i ];

                }
				
                p1 += '\n';
            }
			
			p1 = Utilities.RemoveFromEnd ( p1 , "\n" );


            //
            // FILLING THE P2
            //

            foreach ( var line in lines ) {
				
                for ( int i = divider_place_holder + 1 ; i < line.Length ; i++ ) {
					
					p2 += line [ i ];

                }
				
                p2 += '\n';
            }
			
			p2 = Utilities.RemoveFromEnd ( p2 , "\n" );


            //
            // TRIM
            //

			p1 = LineTrim ( p1 );
			p2 = LineTrim ( p2 );


            //
            // GENERATING THE DIVIDE
            //

			result = DivideMaker ( p1 , p2 );
			return Utilities.Perpend ( result , " " );
        }


        /// <summary>
        /// Removed the empty lines.
        /// </summary>
        /// <param name="text">Text.</param>
        private static string LineTrim ( string text ) {
			
			text = text.Replace ( Utilities.Repeat ( " " , Utilities.LongestLine ( text ) ) + '\n' , "" );
			text = text.Replace ( '\n' + Utilities.Repeat ( " " , Utilities.LongestLine ( text ) ) , "" );
            return text;

        }



        private static string LevelTwoWrapper ( ref Formula code , char open_command ) {
			
			string result = LevelOneWrapper ( ref code , open_command );

            if ( result.Split('/').Length == 2 ) {
				
				result = DivideDiveString ( result );

            }

            if ( open_command == '√' ) {
				
				return RadicalMaker ( result );

            } else if ( open_command == '⦗' ) {
				
				return BoxMaker ( result , Box.Abs );

            } else if ( open_command == '⎣' ) {
				
				return BoxMaker ( result , Box.Floor );

            } else {
				
				return BoxMaker ( result , Box.Brackets );

            }
        }


        private static string LevelOneWrapper(ref Formula code, char open_command)
        {
            string result = "";


            //
            // INIT / ALLOC OF CLOSE COMMAND
            //

            char close_command = ']';


            //
            // CLOSE COMMAND FINDING BODY
            //

            while ( code.i < code.code.Length ) {
				
				var index = code.code [ code.i ];

                if ( index == close_command ) {
					
                    return result;

                } else if ( index == '[' || index == '√' || index == '⦗' | index == '⎣' ) {
					
                    code.i++;
					result = Utilities.Concatenate ( result , FormulaWrapper ( ref code , index ) );

                } else {
					
					result = Utilities.Concatenate ( result , index.ToString () );

                }

                code.i++;
            }


            //
            // DONE
            //

            return result;
        }
    }
}