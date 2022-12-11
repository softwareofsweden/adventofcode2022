using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day11 : Puzzle
    {
        private class Monkey
        {
            private List<long> items;
            private string operation;
            private int divisibleTest;
            private int throwToIfTrue;
            private int throwToIfFalse;
            public long InspectionCount;

            public Monkey(List<long> items, string operation, int divisibleTest, int throwToIfTrue, int throwToIfFalse)
            {
                this.items = items;
                this.operation = operation;
                this.divisibleTest = divisibleTest;
                this.throwToIfTrue = throwToIfTrue;
                this.throwToIfFalse = throwToIfFalse;
                this.InspectionCount = 0;
            }

            public bool HaveItems()
            {
                return this.items.Count > 0;
            }

            private long AdjustWorryLevel(long item)
            {
                bool isAddition = operation.Contains("+");
                string p2 = operation.Split(isAddition ? '+' : '*').Last().Trim();
                long p2val = p2 == "old" ? item : long.Parse(p2);
                return isAddition ? item + p2val : item * p2val;
            }

            public Tuple<int, long> Inspect(bool decreaseWorryLevel = true)
            {
                InspectionCount++;
                var item = items.Last();
                items.RemoveAt(items.Count - 1);
                item = AdjustWorryLevel(item);
                // divided by three and rounded down to the nearest integer
                if (decreaseWorryLevel)
                    item = (long)Math.Floor((float)item / 3f);
                var throwTo = item % divisibleTest == 0 ? throwToIfTrue : throwToIfFalse;
                return Tuple.Create(throwTo, item);
            }

            public void TakeItem(long item)
            {
                items.Add(item);
            }

        }

        private List<Monkey> monkeys = new List<Monkey>();

        /// <summary>
        /// What is the level of monkey business after 20 rounds of stuff-slinging simian shenanigans?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            // Create Monkeys
            foreach (var chunk in data.Chunk(7))
            {
                var items = Array.ConvertAll(chunk[1].Substring(chunk[1].IndexOf(':') + 1).Trim().Split(", "), x => long.Parse(x)).ToList();
                var operation = chunk[2].Substring(chunk[2].IndexOf('=') + 1).Trim();
                var divisibleTest = int.Parse(chunk[3].Substring(chunk[3].IndexOf("by ") + 3).Trim());
                var throwToIfTrue = int.Parse(chunk[4].Substring(chunk[4].IndexOf("monkey ") + 7).Trim());
                var throwToIfFalse = int.Parse(chunk[5].Substring(chunk[5].IndexOf("monkey ") + 7).Trim());
                monkeys.Add(new Monkey(items, operation, divisibleTest, throwToIfTrue, throwToIfFalse));
            }

            const int rounds = 20;
            for (int round = 0; round < rounds; round++)
                foreach (var monkey in monkeys)
                    while (monkey.HaveItems())
                    {
                        var inspect = monkey.Inspect();
                        monkeys[inspect.Item1].TakeItem(inspect.Item2);
                    }

            var top2 = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).ToArray();
            var monkeyBusiness = top2[0].InspectionCount * top2[1].InspectionCount;

            return monkeyBusiness.ToString();
        }

        /// <summary>
        /// What is the level of monkey business after 10000 rounds?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            long divisibleTestsProduct = 1;

            // Create Monkeys
            foreach (var chunk in data.Chunk(7))
            {
                var items = Array.ConvertAll(chunk[1].Substring(chunk[1].IndexOf(':') + 1).Trim().Split(", "), x => long.Parse(x)).ToList();
                var operation = chunk[2].Substring(chunk[2].IndexOf('=') + 1).Trim();
                var divisibleTest = int.Parse(chunk[3].Substring(chunk[3].IndexOf("by ") + 3).Trim());
                var throwToIfTrue = int.Parse(chunk[4].Substring(chunk[4].IndexOf("monkey ") + 7).Trim());
                var throwToIfFalse = int.Parse(chunk[5].Substring(chunk[5].IndexOf("monkey ") + 7).Trim());
                monkeys.Add(new Monkey(items, operation, divisibleTest, throwToIfTrue, throwToIfFalse));
                divisibleTestsProduct *= divisibleTest;
            }

            const int rounds = 10000;
            for (int round = 0; round < rounds; round++)
                foreach (var monkey in monkeys)
                    while (monkey.HaveItems())
                    {
                        var inspect = monkey.Inspect(false);
                        var item = inspect.Item2;
                        item -= ((item - divisibleTestsProduct) / divisibleTestsProduct) * divisibleTestsProduct;
                        monkeys[inspect.Item1].TakeItem(item);
                    }

            var top2 = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).ToArray();
            var monkeyBusiness = top2[0].InspectionCount * top2[1].InspectionCount;

            return monkeyBusiness.ToString();
        }
    }
}
