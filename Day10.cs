using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day10 : Puzzle
    {
        /// <summary>
        /// Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 
        /// 220th cycles. What is the sum of these six signal strengths?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            int x = 1;
            int cycle = 0;
            int total = 0;
            foreach (var opcode in data)
            {
                var steps = opcode.StartsWith("addx") ? 2 : 1;
                for (int i = 0; i < steps; i++)
                {
                    cycle++;
                    if ((cycle + 20) % 40 == 0)
                        total += x * cycle;
                }
                if (steps == 2)
                    x += int.Parse(opcode[5..]);
            }
            return total.ToString();
        }

        /// <summary>
        /// Render the image given by your program. What eight capital letters 
        /// appear on your CRT?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            int x = 1;
            int screenX = 0;
            foreach (var opcode in data)
            {
                var cycles = opcode.StartsWith("addx") ? 2 : 1;
                for (int i = 0; i < cycles; i++)
                {
                    Console.Write((x == screenX % 40 || x - 1 == screenX % 40 || x + 1 == screenX % 40) ? "#" : " ");
                    screenX++;
                    if (screenX % 40 == 0)
                        Console.Write("\n");
                }
                if (cycles == 2)
                    x += int.Parse(opcode[5..]);
            }
            return "PLEFULPB";
        }
    }
}
