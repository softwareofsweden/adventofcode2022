using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day09 : Puzzle
    {
        /// <summary>
        /// How many positions does the tail of the rope visit at least once?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            int hx = 0;
            int hy = 0;
            int lastHx;
            int lastHy;
            int tx = 0;
            int ty = 0;
            var visited = new HashSet<Tuple<int, int>>();
            visited.Add(Tuple.Create(0, 0));
            for (int i = 0; i < data.Length; i++)
            {
                var parts = data[i].Split(' ');
                var dir = parts[0];
                var steps = int.Parse(parts[1]);
                var dx = dir == "L" ? -1 : dir == "R" ? 1 : 0;
                var dy = dir == "U" ? -1 : dir == "D" ? 1 : 0;
                for (int j = 0; j < steps; j++)
                {
                    lastHx = hx;
                    lastHy = hy;
                    hx += dx;
                    hy += dy;
                    if (Math.Abs(hx - tx) > 1 || Math.Abs(hy - ty) > 1)
                    {
                        tx = lastHx;
                        ty = lastHy;
                        visited.Add(Tuple.Create(tx, ty));
                    }
                }
            }
            return visited.Count.ToString();
        }

        /// <summary>
        /// Simulate your complete series of motions on a larger rope with ten knots.
        /// How many positions does the tail of the rope visit at least once?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            int[] tx = new int[10];
            int[] ty = new int[10];
            var visited = new HashSet<Tuple<int, int>>();
            visited.Add(Tuple.Create(0, 0));
            for (int i = 0; i < data.Length; i++)
            {
                var parts = data[i].Split(' ');
                var dir = parts[0];
                var steps = int.Parse(parts[1]);
                var dx = dir == "L" ? -1 : dir == "R" ? 1 : 0;
                var dy = dir == "U" ? -1 : dir == "D" ? 1 : 0;
                for (int j = 0; j < steps; j++)
                {
                    tx[0] += dx;
                    ty[0] += dy;
                    for (int k = 1; k < 10; k++)
                    {
                        if (Math.Abs(tx[k - 1] - tx[k]) > 1 || Math.Abs(ty[k - 1] - ty[k]) > 1)
                        {
                            if (tx[k - 1] - tx[k] > 0)
                                tx[k]++;
                            else if (tx[k - 1] - tx[k] < 0)
                                tx[k]--;
                            if (ty[k - 1] - ty[k] > 0)
                                ty[k]++;
                            else if (ty[k - 1] - ty[k] < 0)
                                ty[k]--;
                            if (k == 9)
                                visited.Add(Tuple.Create(tx[9], ty[9]));
                        }
                    }
                }
            }
            return visited.Count.ToString();
        }
    }
}
