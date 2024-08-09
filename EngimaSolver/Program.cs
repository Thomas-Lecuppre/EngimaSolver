using EnigmaSolverCore;

namespace EngimaSolver
{
    internal class Program
    {
        static void Main(string[] args)
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

            DateTime start = DateTime.Now;

            List<string> results = Solver.BrutSolve(enigma);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Time elapsed: {end - start}");
            Console.WriteLine("Results: ");
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            
        }
    }
}
