namespace EnigmaSolverCore
{
    /// <summary>
    /// Represent a set of numbers that should be solved with a bunch of rules.
    /// Each lane should have a set of rules that should be applied to solve the enigma.
    /// </summary>
    public class Enigma
    {
        // Make sure this class always need a list of strings to be created.
        public Enigma(List<string> numbers, List<LineRule> laneRules)
        {
            CanBeSolved(numbers, laneRules);

            for(int i = 0; i < numbers.Count; i++)
            {
                ProblemLane.Add(numbers[i], laneRules[i]);
            }

            NumbersLenght = numbers.First().Length;
        }

        public int NumbersLenght { get; private set; }
        public Dictionary<string, LineRule> ProblemLane { get; private set; } = [];

        private bool CanBeSolved(List<string> numbers, List<LineRule> laneRules)
        {
            if(numbers.Count != laneRules.Count)
                throw new ArgumentNullException("Numbers list and rules list should have the same amount of items");

            // Check if all numbers have the same length.
            if(numbers.Any(x => x.Length != numbers.First().Length))
                throw new ArgumentException("All numbers should have the same length.");

            // Check if there is at least one rule that can be applied.
            if(laneRules.All(x => x.CountOfCorrect == 0 && x.CountOfInvalidPlacement == 0))
                throw new ArgumentException("At least one rule should be applied.");

            return true;
        }
    }


    public class LineRule
    {
        public int CountOfCorrect { get; set; }
        public int CountOfInvalidPlacement { get; set; }
    }
}
