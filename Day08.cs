using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day08 : Puzzle
    {
        /// <summary>
        /// Consider your map; how many trees are visible from outside the grid?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            const int size = 99;
            int[,] treeMap = new int[size, size];
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    treeMap[x, y] = int.Parse(data[y][x].ToString());
            var count = 0;
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    var h = treeMap[x, y];
                    var vis = true;
                    for (int i = 0; i < y; i++)
                        if (treeMap[x, i] >= h)
                        {
                            vis = false;
                            break;
                        }
                    if (vis)
                    {
                        count++;
                        continue;
                    }
                    vis = true;
                    for (int i = size - 1; i > y; i--)
                        if (treeMap[x, i] >= h)
                        {
                            vis = false;
                            break;
                        }
                    if (vis)
                    {
                        count++;
                        continue;
                    }
                    vis = true;
                    for (int i = 0; i < x; i++)
                        if (treeMap[i, y] >= h)
                        {
                            vis = false;
                            break;
                        }
                    if (vis)
                    {
                        count++;
                        continue;
                    }
                    vis = true;
                    for (int i = size - 1; i > x; i--)
                        if (treeMap[i, y] >= h)
                        {
                            vis = false;
                            break;
                        }
                    if (vis)
                        count++;
                }
            return count.ToString();
        }

        /// <summary>
        /// Consider each tree on your map. What is the highest scenic 
        /// score possible for any tree?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            const int size = 99;
            int[,] treeMap = new int[size, size];
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    treeMap[x, y] = int.Parse(data[y][x].ToString());
            var maxScore = 0;
            for (int y = 1; y < size - 1; y++)
                for (int x = 1; x < size - 1; x++)
                {
                    var h = treeMap[x, y];
                    var upScore = 0;
                    for (int i = y - 1; i >= 0; i--)
                    {
                        upScore++;
                        if (treeMap[x, i] >= h)
                            break;
                    }
                    var downScore = 0;
                    for (int i = y + 1; i < size; i++)
                    {
                        downScore++;
                        if (treeMap[x, i] >= h)
                            break;
                    }
                    var leftScore = 0;
                    for (int i = x - 1; i >= 0; i--)
                    {
                        leftScore++;
                        if (treeMap[i, y] >= h)
                            break;
                    }
                    var rightScore = 0;
                    for (int i = x + 1; i < size; i++)
                    {
                        rightScore++;
                        if (treeMap[i, y] >= h)
                            break;
                    }
                    var score = upScore * downScore * leftScore * rightScore;
                    if (score > maxScore)
                        maxScore = score;
                }
            return maxScore.ToString();
        }
    }
}
