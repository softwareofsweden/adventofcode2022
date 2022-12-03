using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day03 : Puzzle
    {
        /// <summary>
        /// Find the item type that appears in both compartments of each rucksack. 
        /// What is the sum of the priorities of those item types?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var sum = 0;
            foreach (var rucksack in data)
            {
                var compartment1 = rucksack.Substring(0, rucksack.Length / 2);
                var compartment2 = rucksack.Substring(rucksack.Length / 2);
                foreach (var item in compartment1.Distinct())
                {
                    if (compartment2.Contains(item))
                    {
                        var priority = (int)item - (item > 'Z' ? 96 : 38);
                        sum += priority;
                    }
                }
            }
            return sum.ToString();
        }

        /// <summary>
        /// Find the item type that corresponds to the badges of each three-Elf group. 
        /// What is the sum of the priorities of those item types?
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var sum = 0;
            foreach (var group in data.Chunk(3))
            {
                var item = String.Concat(group[0].Intersect(group[1]).Intersect(group[2]))[0];
                var priority = (int)item - (item > 'Z' ? 96 : 38);
                sum += priority;
            }
            return sum.ToString();
        }
    }
}
