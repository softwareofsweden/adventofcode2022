using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day12 : Puzzle
    {
        protected int CountStepsMethod1(int startX, int startY, int endX, int endY, int[,] map, int w, int h, int maxC)
        {
            var track = new int[w, h];
            track[startX, startY] = 1;
            var c = 1;
            while (c < maxC)
            {
                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                        if (track[x, y] == c)
                        {
                            var e = map[x, y] + 2;
                            if (x > 0 && map[x - 1, y] < e && track[x - 1, y] == 0)
                                track[x - 1, y] = c+1;
                            if (x < w - 1 && map[x + 1, y] < e && track[x + 1, y] == 0)
                                track[x + 1, y] = c+1;
                            if (y > 0 && map[x, y - 1] < e && track[x, y - 1] == 0)
                                track[x, y - 1] = c+1;
                            if (y < h - 1 && map[x, y + 1] < e && track[x, y + 1] == 0)
                                track[x, y + 1] = c+1;
                        }
                if (track[endX, endY] != 0)
                    return c;
                c++;
            }
            return -1;
        }

        protected int CountStepsMethod2(int startX, int startY, int endX, int endY, int[,] map, int w, int h)
        {
            var visited = new SortedSet<Tuple<int, int>>();
            var walkers = new SortedSet<Tuple<int, int>>();
            walkers.Add(Tuple.Create(startX, startY));
            visited.Add(Tuple.Create(startX, startY));
            var steps = 0;
            while (walkers.Count > 0)
            {
                var newWalkers = new List<Tuple<int, int>>();
                foreach (var walker in walkers)
                {
                    if (walker.Item1 == endX && walker.Item2 == endY)
                    {
                        return steps;
                    }
                    var wx = walker.Item1;
                    var wy = walker.Item2;
                    var e = map[wx, wy] + 2;
                    if (wx > 0 && map[wx - 1, wy] < e && !visited.Contains(Tuple.Create(wx - 1, wy)))
                    {
                        newWalkers.Add(Tuple.Create(wx - 1, wy));
                        visited.Add(Tuple.Create(wx - 1, wy));
                    }
                    if (wx < w - 1 && map[wx + 1, wy] < e && !visited.Contains(Tuple.Create(wx + 1, wy)))
                    {
                        newWalkers.Add(Tuple.Create(wx + 1, wy));
                        visited.Add(Tuple.Create(wx + 1, wy));
                    }
                    if (wy > 0 && map[wx, wy - 1] < e && !visited.Contains(Tuple.Create(wx, wy - 1)))
                    {
                        newWalkers.Add(Tuple.Create(wx, wy - 1));
                        visited.Add(Tuple.Create(wx, wy - 1));
                    }
                    if (wy < h - 1 && map[wx, wy + 1] < e && !visited.Contains(Tuple.Create(wx, wy + 1)))
                    {
                        newWalkers.Add(Tuple.Create(wx, wy + 1));
                        visited.Add(Tuple.Create(wx, wy + 1));
                    }
                }
                walkers.Clear();
                foreach (var newWalker in newWalkers)
                    walkers.Add(newWalker);
                steps++;
            }
            return -1;
        }


        protected int CountStepsMethod3(int startX, int startY, int endX, int endY, int[,] map, int w, int h)
        {
            var walkers = new HashSet<Tuple<int, int>>();
            walkers.Add(Tuple.Create(startX, startY));
            var track = new int[w, h];
            track[startX, startY] = 1;
            var steps = 0;
            while (walkers.Count > 0)
            {
                var newWalkers = new List<Tuple<int, int>>();
                foreach (var walker in walkers)
                {
                    if (walker.Item1 == endX && walker.Item2 == endY)
                    {
                        return steps;
                    }
                    var wx = walker.Item1;
                    var wy = walker.Item2;
                    var e = map[wx, wy] + 2;
                    if (wx > 0 && map[wx - 1, wy] < e && track[wx - 1, wy] == 0)
                    {
                        newWalkers.Add(Tuple.Create(wx - 1, wy));
                        track[wx - 1, wy] = 1;
                    }
                    if (wx < w - 1 && map[wx + 1, wy] < e && track[wx + 1, wy] == 0)
                    {
                        newWalkers.Add(Tuple.Create(wx + 1, wy));
                        track[wx + 1, wy] = 1;
                    }
                    if (wy > 0 && map[wx, wy - 1] < e && track[wx, wy - 1] == 0)
                    {
                        newWalkers.Add(Tuple.Create(wx, wy - 1));
                        track[wx, wy - 1] = 1;
                    }
                    if (wy < h - 1 && map[wx, wy + 1] < e && track[wx, wy + 1] == 0)
                    {
                        newWalkers.Add(Tuple.Create(wx, wy + 1));
                        track[wx, wy + 1] = 1;
                    }
                }
                walkers.Clear();
                foreach (var newWalker in newWalkers)
                    walkers.Add(newWalker);
                steps++;

                //Console.Clear();
                //for (int y = 0; y < h; y++)
                //{
                //    for (int x = 0; x < w; x++)
                //    {
                //        if (walkers.Contains(Tuple.Create(x, y)))
                //        {
                //            Console.ForegroundColor = ConsoleColor.Red;
                //            Console.Write("*");
                //        }
                //        else if (track[x, y ] == 1)
                //        {
                //            Console.ForegroundColor = ConsoleColor.DarkGray;
                //            Console.Write((char)(map[x, y] - 1 + (int)'a'));
                //        }
                //        else
                //        {
                //            Console.ForegroundColor = ConsoleColor.DarkYellow;
                //            if (map[x, y] > 2)
                //                Console.ForegroundColor = ConsoleColor.Yellow;
                //            if (map[x, y] > 5)
                //                Console.ForegroundColor = ConsoleColor.Green;
                //            if (map[x, y] > 10)
                //                Console.ForegroundColor = ConsoleColor.DarkGreen;
                //            if (map[x, y] > 14)
                //                Console.ForegroundColor = ConsoleColor.DarkRed;
                //            if (map[x, y] > 19)
                //                Console.ForegroundColor = ConsoleColor.Cyan;
                //            if (map[x, y] > 23)
                //                Console.ForegroundColor = ConsoleColor.White;
                //            Console.Write(((char)(map[x, y] - 1 + (int)'a')).ToString().ToUpper());
                //        }
                //    }
                //    Console.Write("\n");
                //}
                //Console.ReadKey();

            }
            return -1;
        }

        /// <summary>
        /// What is the fewest steps required to move from your current 
        /// position to the location that should get the best signal?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var w = data[0].Length;
            var h = data.Length;
            var map = new int[w, h];
            var startX = 0;
            var startY = 0;
            var endX = 0;
            var endY = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    map[x, y] = (int)data[y][x];
                    if (data[y][x] == 'S')
                    {
                        map[x, y] = (int)'a';
                        startX = x;
                        startY = y;
                    }
                    else if (data[y][x] == 'E')
                    {
                        map[x, y] = (int)'z';
                        endX = x;
                        endY = y;
                    }
                    map[x, y] = map[x, y] - (int)'a' + 1;
                }
            }

            // return CountStepsMethod1(startX, startY, endX, endY, map, w, h, w * h).ToString();
            // return CountStepsMethod2(startX, startY, endX, endY, map, w, h).ToString();
            return CountStepsMethod3(startX, startY, endX, endY, map, w, h).ToString();
        }

        /// <summary>
        /// What is the fewest steps required to move starting from any square 
        /// with elevation a to the location that should get the best signal?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var w = data[0].Length;
            var h = data.Length;
            var map = new int[w, h];
            var endX = 0;
            var endY = 0;
            var startLocations = new List<Tuple<int, int>>(); 
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    map[x, y] = (int)data[y][x];
                    if (data[y][x] == 'S' || data[y][x] == 'a')
                    {
                        map[x, y] = (int)'a';
                        startLocations.Add(Tuple.Create(x, y));
                    }
                    else if (data[y][x] == 'E')
                    {
                        map[x, y] = (int)'z';
                        endX = x;
                        endY = y;
                    }
                    map[x, y] = map[x, y] - (int)'a' + 1;
                }

            var shortest = w * h;
            foreach (var startLocation in startLocations)
            {
                var startX = startLocation.Item1;
                var startY = startLocation.Item2;
                // var steps = CountStepsMethod1(startX, startY, endX, endY, map, w, h, shortest);
                // var steps = CountStepsMethod2(startX, startY, endX, endY, map, w, h);
                var steps = CountStepsMethod3(startX, startY, endX, endY, map, w, h);
                if (steps != -1 && steps < shortest)
                    shortest = steps;
            }

            return shortest.ToString();
        }
    }
}
