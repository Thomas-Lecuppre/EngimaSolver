using EnigmaSolverCore;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSolverTest
{
    [TestClass]
    public class FourDigitsProblems
    {
        [TestMethod]
        public void FirstEnigma()
        {
            List<string> numbers =
            [
                "9205",
                "3684",
                "9170",
                "4903",
                "7465"
            ];

            List<LineRule> laneRules =
            [
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 1 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 },
                new(){ CountOfCorrect = 1, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 1, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 }
            ];

            string solution = "8123";

            Enigma enigma = new(numbers, laneRules);

            List<string> results = Solver.BrutSolve(enigma);
            // Debug results
            foreach (var item in results)
            {
                Debug.WriteLine(item);
            }
            Assert.IsTrue(results.Contains(solution));
            results.Should().HaveCount(1);
        }
    }
}
