using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day14 : Puzzle
    {
        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private class Line
        {
            public List<Point> Points { get; set; }

            public Line()
            {
                Points = new List<Point>();
            }

            public Point Min()
            {
                var minX = Points[0].X;
                var minY = Points[0].Y;
                for (int i = 1; i < Points.Count; i++)
                {
                    if (Points[i].X < minX)
                        minX = Points[i].X;
                    if (Points[i].Y < minY)
                        minY = Points[i].Y;
                }
                return new Point(minX, minY);
            }

            public Point Max()
            {
                var maxX = Points[0].X;
                var maxY = Points[0].Y;
                for (int i = 1; i < Points.Count; i++)
                {
                    if (Points[i].X > maxX)
                        maxX = Points[i].X;
                    if (Points[i].Y > maxY)
                        maxY = Points[i].Y;
                }
                return new Point(maxX, maxY);
            }

            public Point[] GetAllPoints()
            {
                var points = new List<Point>();
                for (int i = 1; i < Points.Count; i++)
                {
                    var p1 = Points[i - 1];
                    var p2 = Points[i];
                    var startX = Math.Min(p1.X, p2.X);
                    var endX = Math.Max(p1.X, p2.X);
                    var startY = Math.Min(p1.Y, p2.Y);
                    var endY = Math.Max(p1.Y, p2.Y);
                    if (startX == endX)
                        for (int y = startY; y < endY + 1; y++)
                            points.Add(new Point(startX, y));
                    else
                        for (int x = startX; x < endX + 1; x++)
                            points.Add(new Point(x, startY));
                }
                return points.ToArray();
            }
        }

        private class SandSimulator
        {
            private int w;
            private readonly int h;
            private int[,] map;
            private readonly Point sandStart;

            public bool Completed;
            public int SandCount;
            private Point sand;

            public bool hasFloor;

            public SandSimulator(string[] data, bool hasFloor = false)
            {
                this.hasFloor = hasFloor;
                var lines = new List<Line>();
                foreach (var lineRow in data)
                {
                    var line = new Line();
                    var pointData = lineRow.Split(" -> ");
                    foreach (var point in pointData)
                    {
                        var coords = point.Split(',');
                        line.Points.Add(new Point(int.Parse(coords[0]), int.Parse(coords[1])));
                    }
                    lines.Add(line);
                }

                var minX = lines[0].Min().X;
                var minY = lines[0].Min().Y;
                var maxX = lines[0].Max().X;
                var maxY = lines[0].Max().Y;
                for (int i = 1; i < lines.Count; i++)
                {
                    var min = lines[i].Min();
                    var max = lines[i].Max();
                    if (min.X < minX)
                        minX = min.X;
                    if (min.Y < minY)
                        minY = min.Y;
                    if (max.X > maxX)
                        maxX = max.X;
                    if (max.Y > maxY)
                        maxY = max.Y;
                }

                if (hasFloor)
                    maxY += 2;

                w = maxX - minX + 1;
                h = maxY + 1;

                map = new int[w, h];

                foreach (var line in lines)
                    foreach (var point in line.GetAllPoints())
                        map[point.X - minX, point.Y] = 1;

                if (hasFloor)
                    for (int i = 0; i < w; i++)
                        map[i, h - 1] = 1;

                Completed = false;
                SandCount = 0;
                sandStart = new Point(500 - minX, 0);
                sand = new Point(sandStart.X, sandStart.Y);
            }

            private int GetTile(int x, int y)
            {
                if (hasFloor)
                {
                    if (x < 0)
                    {
                        // Expand map left
                        int[,] newMap = new int[w + 1, h];
                        // Copy old to new
                        for (int yy = 0; yy < h; yy++)
                            for (int xx = 0; xx < w; xx++)
                                newMap[xx + 1, yy] = map[xx, yy];
                        newMap[0, h - 1] = 1; // Add floor
                        map = newMap;
                        w++;
                        // Offset other stuff
                        x++;
                        sand.X++;
                        sandStart.X++;
                    }
                    else if (x > w - 1)
                    {
                        // Expand map right
                        int[,] newMap = new int[w + 1, h];
                        // Copy old to new
                        for (int yy = 0; yy < h; yy++)
                            for (int xx = 0; xx < w; xx++)
                                newMap[xx, yy] = map[xx, yy];
                        newMap[w, h - 1] = 1; // Add floor
                        map = newMap;
                        w++;
                    }
                }
                if (x < 0 || x > w - 1 || y < 0 || y > h - 1)
                    return -1;
                return map[x, y];
            }

            public void Simulate()
            {
                if (Completed)
                    return;

                // Can move down?
                var tileBelow = GetTile(sand.X, sand.Y + 1);
                if (tileBelow == -1)
                {
                    // Simulation done!
                    Completed = true;
                    return;
                }
                if (tileBelow == 0)
                {
                    sand.Y++;
                    return;
                }

                // Can move left?
                var tileLeft = GetTile(sand.X - 1, sand.Y + 1);
                if (tileLeft == -1)
                {
                    // Simulation done!
                    Completed = true;
                    return;
                }
                if (tileLeft == 0)
                {
                    sand.X--;
                    sand.Y++;
                    return;
                }

                // Can move right?
                var tileRight = GetTile(sand.X + 1, sand.Y + 1);
                if (tileRight == -1)
                {
                    // Simulation done!
                    Completed = true;
                    return;
                }
                if (tileRight == 0)
                {
                    sand.X++;
                    sand.Y++;
                    return;
                }

                // Cannot move, so it landed
                map[sand.X, sand.Y] = 2;
                SandCount++;

                // Check if source of the sand is blocked
                if (sand.X == sandStart.X && sand.Y == sandStart.Y)
                {
                    Completed = true;
                    return;
                }

                // Spawn new
                sand.X = sandStart.X;
                sand.Y = sandStart.Y;
            }

            public void Draw()
            {
                Console.Write("\n");
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        if (x == sand.X && y == sand.Y)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("o");
                        }
                        else if (x == sandStart.X && y == sandStart.Y)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("+");
                        }
                        else if (map[x, y] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("#");
                        }
                        else if (map[x, y] == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("O");
                        }
                        else
                            Console.Write(" ");
                    }
                    Console.Write("\n");
                }
            }
        }

        /// <summary>
        /// Using your scan, simulate the falling sand. How many units of sand 
        /// come to rest before sand starts flowing into the abyss below?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var sim = new SandSimulator(data);
            while (!sim.Completed)
            {
                //Console.Clear();
                sim.Simulate();
                //sim.Draw();
                //Console.WriteLine("\n");
                //Console.WriteLine(sim.SandCount);
                //Console.ReadKey();
            }
            return sim.SandCount.ToString();
        }

        /// <summary>
        /// Using your scan, simulate the falling sand until the source of the 
        /// sand becomes blocked. How many units of sand come to rest?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var sim = new SandSimulator(data, true);
            while (!sim.Completed)
            {
                //Console.Clear();
                sim.Simulate();
                //sim.Draw();
                //Console.WriteLine("\n");
                //Console.WriteLine(sim.SandCount);
                //Console.ReadKey();
            }
            return sim.SandCount.ToString();
        }
    }
}
