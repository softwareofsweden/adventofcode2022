using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day01 : Puzzle
    {
        /// <summary>
        /// Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var max = 0;
            var current = 0;

            foreach (var s in data)
            {
                if (s == "")
                {
                    if (current > max)
                        max = current;
                    current = 0;
                    continue;
                }
                current += int.Parse(s);
            }

            return max.ToString();
        }

        /// <summary>
        /// Find the top three Elves carrying the most Calories. How many Calories are those 
        /// Elves carrying in total?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var current = 0;
            var list = new List<int>();

            foreach (var s in data)
            {
                if (s == "")
                {
                    list.Add(current);
                    current = 0;
                    continue;
                }
                current += int.Parse(s);
            }

            list.Sort();
            list.Reverse();

            return list.Take(3).Sum().ToString();
        }
    }
}
