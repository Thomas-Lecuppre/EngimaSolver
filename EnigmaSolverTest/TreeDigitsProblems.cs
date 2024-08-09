using EnigmaSolverCore;
using FluentAssertions;
using System.Diagnostics;

namespace EnigmaSolverTest
{
    [TestClass]
    public class TreeDigitsProblems
    {
        [TestMethod]
        public void FirstEnigma()
        {
            List<string> numbers =
            [
                "302",
                "314",
                "273",
                "690",
                "607"
            ];

            List<LineRule> laneRules =
            [
                new(){ CountOfCorrect = 1, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 1 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 1 }
            ];

            string solution = "742";

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


        [TestMethod]
        public void SecondEnigma()
        {
            List<string> numbers =
            [
                "247",
                "315",
                "358",
                "256",
                "682"
            ];

            List<LineRule> laneRules =
            [
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 1 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 1 },
                new(){ CountOfCorrect = 1, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 }
            ];

            string solution = "876";

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


        [TestMethod]
        public void ThirdEnigma()
        {
            List<string> numbers =
            [
                "629",
                "034",
                "325",
                "781",
                "509"
            ];

            List<LineRule> laneRules =
            [
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 1, CountOfInvalidPlacement = 1 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 }
            ];

            string solution = "295";

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


        [TestMethod]
        public void FourthEnigma()
        {
            List<string> numbers =
            [
                "913",
                "280",
                "047",
                "426",
                "587"
            ];

            List<LineRule> laneRules =
            [
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 1, CountOfInvalidPlacement = 1 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 0 },
                new(){ CountOfCorrect = 0, CountOfInvalidPlacement = 2 }
            ];

            string solution = "870";

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