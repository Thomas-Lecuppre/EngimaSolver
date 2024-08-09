using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnigmaSolverCore
{
    public static class Solver
    {
        public static List<string> BrutSolve(Enigma enigma)
        {
            // Initiate the enigma.
            _enigma = enigma;
            _lanes.Clear();
            CreateDigitValidationList();

            foreach (var pair in enigma.ProblemLane)
            {
                _lanes.Add(new Lane(pair.Key, pair.Value.CountOfCorrect, pair.Value.CountOfInvalidPlacement));
            }

            List<string> solution = [];
            FindIncorrect();


            for(int i = 0; i < Math.Pow(10, enigma.NumbersLenght); i++)
            {
                ResetLanes();
                string number = i.ToString();
                while (number.Length < enigma.NumbersLenght)
                {
                    number = "0" + number;
                }
                if (CheckIncorrect(number)) continue;

                CheckNumber(number);

                if (IsValidNumber())
                {
                    solution.Add(number);
                }
            }

            return solution;
        }

        #region Private variables

        private static List<Lane> _lanes = [];

        private static Enigma? _enigma;

        private static List<DigitValidation> digitValidations = [];

        #endregion


        #region Set and Get basic methods

        private static Task<List<string>> SolveThemAsync(int lower, int higher)
        {
            List<string> _solutions = [];
            for(int i = lower; i < higher; i++)
            {
                ResetLanes();
                string number = i.ToString();
                while (number.Length < _enigma.NumbersLenght)
                {
                    number = "0" + number;
                }
                if (CheckIncorrect(number)) continue;

                CheckNumber(number);

                if (IsValidNumber())
                {
                    _solutions.Add(number);
                }
            }
            return Task.FromResult(_solutions);
        }

        private static bool CheckIncorrect(string number)
        {
            if (_enigma == null) throw new NullReferenceException("The enigma is not set.");

            // For each digit in the number we check if it is in the incorrect list.
            for (int i = 0; i < number.Length; i++)
            {
                if (!int.TryParse(number[i].ToString(), out int digit)) throw new ArgumentException("The number should only contain digits.");

                // If the digit is in the incorrect list, we return false.
                // Because it cannot be the solution in at this index and so the whole number is incorrect.
                if (!digitValidations[i].CouldBe(digit)) return false;
            }
            return true;
        }

        private static void CheckNumber(string number)
        {
            if (number.Length != _enigma.NumbersLenght) throw new ArgumentException("The number should have the same length as the enigma.");

            // for each digit in the number we check if the associated lane to calculate the 
            for (int i = 0; i < number.Length; i++)
            {
                string dig = number[i].ToString();
                if (!int.TryParse(dig, out int digit)) throw new ArgumentException("The number should only contain digits.");
                CheckLanes(digit, i);
            }
        }

        private static void CheckLanes(int digit, int position)
        {
            foreach (var lane in _lanes)
            {
                lane.CheckDigit(digit, position);
            }
        }

        private static bool IsValidNumber()
        {
            foreach (var lane in _lanes)
            {
                if (!lane.IsCorrect()) return false;
            }
            return true;
        }

        private static void ResetLanes()
        {
            foreach (var lane in _lanes)
            {
                lane.Reset();
            }
        }

        /// <summary>
        /// Create the digit validation for each digit in the number.
        /// </summary>
        /// <param name="num">A number to </param>
        private static void CreateDigitValidationList()
        {
            if (_enigma == null) throw new NullReferenceException("The enigma is not set.");
            for (int i = 0; i < _enigma.NumbersLenght; i++)
            {
                digitValidations.Add(new DigitValidation(i));
            }
        }

        #endregion

        #region Defining DigitValidation


        /// <summary>
        /// Find all the numbers with not correct and no invalid placement and set all the digit in the number as incorrect in all the digit validation.
        /// </summary>
        /// <exception cref="NullReferenceException">Returned if Enigma isn't set.</exception>
        /// <exception cref="ArgumentException">Returned if the number isn't an integer.</exception>
        private static void FindIncorrect()
        {
            if (_enigma == null) throw new NullReferenceException("The enigma is not set.");

            // Get the list of key/value pairs where the rules contain 0 correct and 0 valid.
            Dictionary<string, LineRule> pairs = _enigma.ProblemLane
                .Where(x => x.Value.CountOfCorrect == 0 && x.Value.CountOfInvalidPlacement == 0)
                .ToDictionary(x => x.Key, x => x.Value);

            // For each pair we add the incorrect digit in each digit validation.
            foreach (var pair in pairs)
            {
                // The incorrect digit is the one that is in the number.
                for (int i = 0; i < pair.Key.Length; i++)
                {
                    // Check if the digit is a digit.
                    if (!int.TryParse(pair.Key[i].ToString(), out int digit)) throw new ArgumentException("The number should only contain digits.");
                    foreach (var digitValidation in digitValidations)
                    {
                        digitValidation.AddIncorrect(digit);
                    }
                }
            }
        }

        #endregion

        public class DigitValidation
        {
            public DigitValidation(int position)
            {
                PositionInSolution = position;
            }

            public int PositionInSolution { get; set; }
            public List<int> Correct { get; private set; } = [];
            public List<int> Incorrect { get; private set; } = [];
            public int Solution { get; private set; } = -1;

            /// <summary>
            /// Set the solution of the digit.
            /// If <see cref="solution"/> is in the <see cref="Incorrect"/> list, the solution will not be set."/>
            /// </summary>
            /// <param name="solution">The digit to set as solution</param>
            public void SetSolution(int solution)
            {
                //Check if the solution is a digit.
                if (solution < 0 || solution > 9) throw new ArgumentOutOfRangeException(nameof(solution) + "is not a valid digit.");
                if (Incorrect.Contains(solution)) return;
                Solution = solution;
            }

            /// <summary>
            /// Check if the digit could be the solution by looking if it is in the <see cref="Incorrect"/> list.
            /// </summary>
            /// <param name="digit">The digit to check.</param>
            /// <returns><c>True</c> if the digit can be the solution.</returns>
            public bool CouldBe(int digit)
            {
                //Check if the solution is a digit.
                if (digit < 0 || digit > 9) throw new ArgumentOutOfRangeException(nameof(digit) + "is not a valid digit.");
                return Incorrect.Contains(digit);
            }

            public void AddCorrect(int digit)
            {
                //Check if the solution is a digit.
                if (digit < 0 || digit > 9) throw new ArgumentOutOfRangeException(nameof(digit) + "is not a valid digit.");
                if (Incorrect.Contains(digit)) Incorrect.Remove(digit);
                Correct.Add(digit);
            }

            public void AddIncorrect(int digit)
            {
                //Check if the solution is a digit.
                if (digit < 0 || digit > 9) throw new ArgumentOutOfRangeException(nameof(digit) + "is not a valid digit.");
                if (Correct.Contains(digit)) Correct.Remove(digit);
                Incorrect.Add(digit);
            }
        }


        public class Lane
        {
            public Lane(string number, int correct, int invalid)
            {
                // Check if the solution is a digit.
                if (!int.TryParse(number, out int num)) throw new ArgumentException("The number should only contain digits.");
                _number = number;
                _correct = correct;
                _invalid = invalid;
            }

            private string _number { get; init; }
            private int _correct { get; init; }
            private int _invalid { get; init; }

            public int correctCount { get; private set; }
            public int correctInvalid { get; private set; }

            public void CheckDigit(int digit, int position)
            {
                if (digit < 0 || digit > 9) throw new ArgumentOutOfRangeException(nameof(digit) + "is not a valid digit.");
                if (position < 0 || position > _number.ToString().Length) throw new ArgumentOutOfRangeException(nameof(position) + "is not a valid position.");

                // get the digit at the position in the number.
                int num = int.Parse(_number[position].ToString());
                
                if (num == digit)
                {
                    correctCount++;
                }
                else if (_number.Contains(digit.ToString()))
                {
                    correctInvalid++;
                }
            }

            public void Reset()
            {
                correctCount = 0;
                correctInvalid = 0;
            }

            public bool IsCorrect()
            {
                return correctCount == _correct && correctInvalid == _invalid;
            }
        }
    }
}
