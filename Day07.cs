using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day07 : Puzzle
    {
        /// <summary>
        /// Find all of the directories with a total size of at most 100000. 
        /// What is the sum of the total sizes of those directories?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var wd = "";
            Dictionary<string, int> dir = new Dictionary<string, int>();
            dir.Add("", 0);
            foreach (var s in data)
            {
                if (s == "$ cd /")
                    wd = "";
                else if (s == "$ cd ..")
                    wd = String.Join("\\", wd.Split("\\").SkipLast(1));
                else if (s.StartsWith("$ cd "))
                {
                    wd += "\\" + s.Substring(5);
                    dir.Add(wd, 0);
                }
                else if (s != "$ ls" && !s.StartsWith("dir "))
                    dir[wd] += int.Parse(s.Split(" ").First());
            }

            var totalSize = 0;
            foreach (var d in dir)
            {
                var size = dir.Where(x => x.Key.StartsWith(d.Key)).Sum(x => x.Value);
                if (size <= 100000)
                    totalSize += size;
            }

            return totalSize.ToString();
        }

        /// <summary>
        /// Find the smallest directory that, if deleted, would free up enough space 
        /// on the filesystem to run the update. What is the total size of that directory?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var wd = "";
            Dictionary<string, int> dir = new Dictionary<string, int>();
            dir.Add("", 0);
            foreach (var s in data)
            {
                if (s == "$ cd /")
                    wd = "";
                else if (s == "$ cd ..")
                    wd = String.Join("\\", wd.Split("\\").SkipLast(1));
                else if (s.StartsWith("$ cd "))
                {
                    wd += "\\" + s.Substring(5);
                    dir.Add(wd, 0);
                }
                else if (s != "$ ls" && !s.StartsWith("dir "))
                    dir[wd] += int.Parse(s.Split(" ").First());
            }

            // The total disk space available to the filesystem is 70000000.
            // To run the update, you need unused space of at least 30000000.
            // You need to find a directory you can delete that will free up
            // enough space to run the update.

            var freeSpace = 70000000 - dir.Sum(x => x.Value);
            var missingSpace = 30000000 - freeSpace;
            var smallest = 0;
            foreach (var d in dir)
            {
                var size = dir.Where(x => x.Key.StartsWith(d.Key)).Sum(x => x.Value);
                if (size >= missingSpace)
                    if (smallest == 0 || size < smallest)
                        smallest = size;
            }

            return smallest.ToString();
        }
    }
}
