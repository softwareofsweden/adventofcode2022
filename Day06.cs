using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day06 : Puzzle
    {
        /// <summary>
        /// How many characters need to be processed before the first start-of-packet marker is detected?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            for (int i = 0; i < data[0].Length - 3; i++)
                if (data[0].Substring(i, 4).Distinct().Count() == 4)
                    return (i + 4).ToString();
            return "";
        }

        /// <summary>
        /// How many characters need to be processed before the first start-of-packet marker is detected?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            for (int i = 0; i < data[0].Length - 13; i++)
                if (data[0].Substring(i, 14).Distinct().Count() == 14)
                    return (i + 14).ToString();
            return "";
        }
    }
}
