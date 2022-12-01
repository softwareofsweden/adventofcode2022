using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal abstract class Puzzle
    {
        protected string[] data;

        public Puzzle()
        {
            var dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            var file = this.GetType().Name + ".txt";
            data = System.IO.File.ReadAllLines(System.IO.Path.Combine(dir, "Input", file));
        }

        protected abstract string SolveInternal1();

        protected abstract string SolveInternal2();

        public string Solve1()
        {
            var start = DateTime.Now;
            var result = SolveInternal1();
            var duration = DateTime.Now - start;
            return String.Format("{0} P1 Time: {1} Result: {2}", this.GetType().Name, duration, result);
        }

        public string Solve2()
        {
            var start = DateTime.Now;
            var result = SolveInternal2();
            var duration = DateTime.Now - start;
            return String.Format("{0} P2 Time: {1} Result: {2}", this.GetType().Name, duration, result);
        }
    }
}
