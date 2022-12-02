using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day02 : Puzzle
    {
        /// <summary>
        /// What would your total score be if everything goes exactly according to your strategy guide?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal1()
        {
            var totalScore = 0;

            foreach (var s in data)
            {
                var opponent = s[0] == 'A' ? "rock" : s[0] == 'B' ? "paper" : "scissors";
                var me = s[2] == 'X' ? "rock" : s[2] == 'Y' ? "paper" : "scissors";

                switch (me)
                {
                    case "rock":
                        if (opponent == "rock") totalScore += 3;
                        if (opponent == "scissors") totalScore += 6;
                        totalScore += 1;
                        break;
                    case "paper":
                        if (opponent == "paper") totalScore += 3;
                        if (opponent == "rock") totalScore += 6;
                        totalScore += 2;
                        break;
                    case "scissors":
                        if (opponent == "scissors") totalScore += 3;
                        if (opponent == "paper") totalScore += 6;
                        totalScore += 3;
                        break;
                }
            }

            return totalScore.ToString();
        }

        /// <summary>
        /// What would your total score be if everything goes exactly according to your strategy guide?
        /// </summary>
        /// <returns></returns>
        protected override string SolveInternal2()
        {
            var totalScore = 0;

            foreach (var s in data)
            {
                var opponent = s[0] == 'A' ? "rock" : s[0] == 'B' ? "paper" : "scissors";
                var myResult = s[2] == 'X' ? "lose" : s[2] == 'Y' ? "draw" : "win";

                switch (opponent)
                {
                    case "rock":
                        if (myResult == "lose")
                            totalScore += 3;
                        else if (myResult == "win")
                            totalScore += 8;
                        else
                            totalScore += 4;
                        break;
                    case "paper":
                        if (myResult == "lose")
                            totalScore += 1;
                        else if (myResult == "win")
                            totalScore += 9;
                        else
                            totalScore += 5;
                        break;
                    case "scissors":
                        if (myResult == "lose")
                            totalScore += 2;
                        else if (myResult == "win")
                            totalScore += 7;
                        else
                            totalScore += 6;
                        break;
                }
            }

            return totalScore.ToString();
        }
    }
}
