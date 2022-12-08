using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day05 : Puzzle
    {
        /// <summary>
        /// After the rearrangement procedure completes, what crate ends up on top of each stack?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var stacksStart = Array.IndexOf(data, "") - 2;
            var numberOfStacks = int.Parse(data[stacksStart + 1].Split(' ').Where(x => x != "").Last());

            Stack<char>[] stacks = new Stack<char>[numberOfStacks];
            for (int i = 0; i < numberOfStacks; i++)
                stacks[i] = new Stack<char>();

            for (int i = stacksStart; i >= 0; i--)
                for (int j = 0; j < numberOfStacks; j++)
                    if (data[i][(j * 4) + 1] != ' ')
                        stacks[j].Push(data[i][(j * 4) + 1]);

            for (int i = stacksStart + 3; i < data.Length; i++)
            {
                var instructions = data[i].Split(' ');
                var numberToMove = int.Parse(instructions[1]);
                var fromStack = int.Parse(instructions[3]) - 1;
                var toStack = int.Parse(instructions[5]) - 1;
                for (int j = 0; j < numberToMove; j++)
                    stacks[toStack].Push(stacks[fromStack].Pop());
            }

            var result = "";
            for (int i = 0; i < numberOfStacks; i++)
                result += stacks[i].Pop();

            return result;
        }

        /// <summary>
        /// After the rearrangement procedure completes, what crate ends up on top of each stack?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var stacksStart = Array.IndexOf(data, "") - 2;
            var numberOfStacks = int.Parse(data[stacksStart + 1].Split(' ').Where(x => x != "").Last());

            Stack<char>[] stacks = new Stack<char>[numberOfStacks];
            for (int i = 0; i < numberOfStacks; i++)
                stacks[i] = new Stack<char>();

            for (int i = stacksStart; i >= 0; i--)
                for (int j = 0; j < numberOfStacks; j++)
                    if (data[i][(j * 4) + 1] != ' ')
                        stacks[j].Push(data[i][(j * 4) + 1]);

            for (int i = stacksStart + 3; i < data.Length; i++)
            {
                var instructions = data[i].Split(' ');
                var numberToMove = int.Parse(instructions[1]);
                var fromStack = int.Parse(instructions[3]) - 1;
                var toStack = int.Parse(instructions[5]) - 1;

                var stackToMove = new Stack<char>();
                for (int j = 0; j < numberToMove; j++)
                    stackToMove.Push(stacks[fromStack].Pop());

                foreach (var item in stackToMove)
                    stacks[toStack].Push(item);
            }

            var result = "";
            for (int i = 0; i < numberOfStacks; i++)
                result += stacks[i].Pop();

            return result;
        }
    }
}
