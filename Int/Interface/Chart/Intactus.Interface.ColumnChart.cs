
//
//  Intactus.Interface.ColumnChart.cs
//
//  Created by Pouya Kary on 2015/4/23
//  Copyright (c) 2015-2016 Pouya Kary. All rights reserved.
//


using System;
using Kary;
using Kary.Text;
using Kary.Intactus;

namespace Int
{
    public partial class Interface
    {
        public static void PrintColumnChart(string[] inputs, int io_counter)
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

            for (int index_counter = 0; index_counter < inputs.Length; index_counter++)
            {
                members[index_counter] = Calculate(inputs[index_counter]);
            }


            //
            // GENERATING THE CHART
            //

            string column_chart = Charts.ColumnChart(members, 15);


            //
            // GENERATING STATICS
            //

            string statics = "\n";
            int i = 1;

            foreach (var mem in members)
            {
                if (mem == double.MaxValue)
                {
                    statics += " " + Numerics.Roman(i).PadLeft(5) + "│ Infinity\n";
                }
                else
                {
                    statics += " " + Numerics.Roman(i).PadLeft(5) + "│ " + mem + '\n';
                }

                i++;
            }


            //
            // CONCATENATING THE STATICS TO THE CHART
            //

            column_chart = Utilities.Concatenate(column_chart, statics);


            //
            // PRINTING THE OUTPUT
            //

            string[] lines = column_chart.Split('\n');
            string prompt = "Out[" + Numerics.Roman(io_counter) + "]= ";

            for (int index_counter = 0; index_counter < lines.Length; index_counter++)
            {
                if ((double)index_counter == Math.Floor((double)lines.Length / 2))
                {
                    Terminal.Magenta();
                    Terminal.Print("Out[" + Numerics.Roman(io_counter) + "]= ");
                    Terminal.Reset();
                    Terminal.PrintLn(lines[index_counter]);
                }
                else
                {
                    Terminal.PrintLn(
                        Utilities.Repeat(" ", prompt.Length) +
                        lines[index_counter]
                    );
                }
            }
        }



        /// <summary>
        /// Represents a string reault of Calculat
        /// </summary>
        /// <param name="inputs">Inputs.</param>
        private static double Calculate(string input)
        {
            switch (input)
            {
                case "False":
                case "True":
                case "Operation Failure":
                    return 0;

                case "Infinity":
                    return double.MaxValue;

                default:
                    return double.Parse(input);
            }
        }
    }
}