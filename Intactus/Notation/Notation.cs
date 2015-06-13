//
//  MyClass.cs
//
//  Created by Pouya Kary on 2015/4/19
//  Copyright (c) 2015 Pouya Kary. All rights reserved.
//


namespace Kary.Intactus
{
    public partial class Notation
    {
        public static string Generate(string text)
        {
			//
			// SOME CHANGES BEFORE WE EVEN START
			//

			text = text.Replace ( "->" , "→" );

			//
			// DONE, WE'RE READY TO GO
			//

			return Notation.MasterWrapper ( Notation.FunctionReplacer ( text ) );
        }
    }
}

