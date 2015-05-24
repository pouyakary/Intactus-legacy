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
            return Notation.MasterWrapper(Notation.FunctionReplacer(text));
        }
    }
}

