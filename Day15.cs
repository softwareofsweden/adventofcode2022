using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day15 : Puzzle
    {
        private class Sensor
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int BeaconX { get; set; }
            public int BeaconY { get; set; }
            public int DistanceToBeacon { get; set; }

            public Sensor(int x, int y, int beaconX, int beaconY)
            {
                X = x;
                Y = y;
                BeaconX = beaconX;
                BeaconY = beaconY;
                DistanceToBeacon = Math.Abs(X - BeaconX) + Math.Abs(Y - BeaconY);
            }

            public int DistanceTo(int x, int y)
            {
                return Math.Abs(X - x) + Math.Abs(Y - y);
            }

            public int[] GetXReachAtY(int y)
            {
                var res = new List<int>();
                var yOff = Math.Abs(Y - y);
                var xStart = X - DistanceToBeacon - yOff;
                var xEnd = X + DistanceToBeacon - yOff;
                for (int i = xStart; i < xEnd + 1; i++)
                {
                    var d = Math.Abs(X - i) + Math.Abs(Y - y);
                    if (d <= DistanceToBeacon)
                        res.Add(i);
                }
                return res.ToArray();
            }

            public void GetOutside(HashSet<Tuple<int, int>> r)
            {
                var xOff = - (DistanceToBeacon + 1);
                var yOff = 0;
                var yDir = 1;
                while (xOff <= DistanceToBeacon + 1)
                {
                    var ax = X + xOff;
                    var ay1 = Y - yOff;
                    var ay2 = Y + yOff;
                    if (ax >= 0 && ax <= 4000000)
                    {
                        if (ay1 >= 0 && ay1 <= 4000000)
                            r.Add(Tuple.Create(ax, ay1));
                        if (ay2 >= 0 && ay2 <= 4000000)
                            r.Add(Tuple.Create(ax, ay2));
                    }
                    if (yDir == 1)
                        yOff++;
                    else
                        yOff--;
                    if (xOff == 0)
                        yDir = 0;
                    xOff++;
                }
            }

        }

        /// <summary>
        /// Consult the report from the sensors you just deployed. In the row 
        /// where y=2000000, how many positions cannot contain a beacon?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var checkRow = 2000000;

            var sensors = new List<Sensor>();
            var beaconsX = new HashSet<int>();
            foreach (var item in data)
            {
                var s = item.Replace("Sensor at x=", "").Replace(", y=", ",").Replace(": closest beacon is at x=", ",").Split(',');
                sensors.Add(new Sensor(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
                if (int.Parse(s[3]) == checkRow)
                    beaconsX.Add(int.Parse(s[2]));
            }

            var occupied = new HashSet<int>();
            foreach (var sensor in sensors)
                foreach (var item in sensor.GetXReachAtY(checkRow))
                    occupied.Add(item);

            occupied.RemoveWhere(x => beaconsX.Contains(x));

            return occupied.Count.ToString();
        }

        /// <summary>
        /// Find the only possible position for the distress beacon. 
        /// What is its tuning frequency?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var sensors = new List<Sensor>();
            foreach (var item in data)
            {
                var s = item.Replace("Sensor at x=", "").Replace(", y=", ",").Replace(": closest beacon is at x=", ",").Split(',');
                sensors.Add(new Sensor(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
            }

            var outsidePositions = new HashSet<Tuple<int, int>>();
            foreach (var sensor in sensors)
                sensor.GetOutside(outsidePositions);

            foreach (var outside in outsidePositions)
            {
                var reachable = false;
                foreach (var sensor in sensors)
                    if (sensor.DistanceTo(outside.Item1, outside.Item2) <= sensor.DistanceToBeacon)
                    {
                        reachable = true;
                        break;
                    }
                if (!reachable)
                {
                    BigInteger tf = outside.Item1;
                    tf = tf * 4000000;
                    tf = tf + outside.Item2;
                    return tf.ToString();
                }
            }
            return "";
        }
    }
}
