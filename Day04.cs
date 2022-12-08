using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day04 : Puzzle
    {
        /// <summary>
        /// In how many assignment pairs does one range fully contain the other?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var count = 0;
            foreach (var pair in data)
            {
                var parts = pair.Split(',');
                var range1 = parts[0].Split('-');
                var range1start = int.Parse(range1[0]);
                var range1end = int.Parse(range1[1]);
                var range2 = parts[1].Split('-');
                var range2start = int.Parse(range2[0]);
                var range2end = int.Parse(range2[1]);
                if ((range1start >= range2start && range1end <= range2end) ||
                    (range2start >= range1start && range2end <= range1end))
                    count++;
            }
            return count.ToString();
        }

        /// <summary>
        /// In how many assignment pairs do the ranges overlap?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var count = 0;
            foreach (var pair in data)
            {
                var parts = pair.Split(',');
                var range1 = parts[0].Split('-');
                var range1start = int.Parse(range1[0]);
                var range1end = int.Parse(range1[1]);
                var range2 = parts[1].Split('-');
                var range2start = int.Parse(range2[0]);
                var range2end = int.Parse(range2[1]);
                if ((range1start >= range2start && range1start <= range2end) ||
                    (range1end >= range2start && range1end <= range2end) ||
                    (range2start >= range1start && range2start <= range1end) ||
                    (range2end >= range1start && range2end <= range1end))
                    count++;
            }
            return count.ToString();
        }
    }
}
