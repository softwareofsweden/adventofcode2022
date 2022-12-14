using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day13 : Puzzle
    {
        private static string[] GetItems(string s)
        {
            var items = new List<string>();
            if (s.StartsWith('['))
                s = s.Substring(1, s.Length - 2);
            var currentItem = "";
            var open = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                    open++;
                else if (s[i] == ']')
                    open--;
                if (open == 0 && s[i] == ',')
                {
                    items.Add(currentItem);
                    currentItem = "";
                }
                else
                    currentItem += s[i];
            }
            if (currentItem != "")
                items.Add(currentItem);
            return items.ToArray();
        }

        private static bool IsNumeric(string s)
        {
            return !s.Contains('[');
        }

        private int Sort(string[] left, string[] right)
        {
            var max = Math.Max(left.Length, right.Length);
            for (int i = 0; i < max; i++)
            {
                if (left.Length <= i)
                    return -1;
                if (right.Length <= i)
                    return 1;
                var lv = left[i];
                var rv = right[i];
                if (IsNumeric(lv) && IsNumeric(rv))
                {
                    var l = int.Parse(lv);
                    var r = int.Parse(rv);
                    if (l < r)
                        return -1;
                    if (l > r)
                        return 1;
                    continue;
                }
                if (IsNumeric(lv))
                    lv = '[' + lv.ToString() + ']';
                if (IsNumeric(rv))
                    rv = '[' + rv.ToString() + ']';
                var check = Sort(GetItems(lv), GetItems(rv));
                if (check == 0)
                    continue;
                return check;
            }
            return 0;
        }

        /// <summary>
        /// Determine which pairs of packets are already in the right order.
        /// What is the sum of the indices of those pairs?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var total = 0;
            var pairIndex = 1;
            foreach (var packetPair in data.Chunk(3))
            {
                if (Sort(GetItems(packetPair[0]), GetItems(packetPair[1])) < 0)
                    total += pairIndex;
                pairIndex++;
            }
            return total.ToString();
        }

        /// <summary>
        /// Organize all of the packets into the correct order. What is the decoder 
        /// key for the distress signal?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var packets = new List<string>
            {
                "[[2]]",
                "[[6]]"
            };
            foreach (var packetPair in data.Chunk(3))
            {
                packets.Add(packetPair[0]);
                packets.Add(packetPair[1]);
            }
            packets.Sort(delegate (string p1, string p2)
            {
                return Sort(GetItems(p1), GetItems(p2));
            });
            return ((packets.IndexOf("[[2]]") + 1) * (packets.IndexOf("[[6]]") + 1)).ToString();
        }
    }
}
