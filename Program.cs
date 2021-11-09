using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    class RockPaperScissors
    {
        
        public enum Gesture
        {
            Rock = 0,
            Paper = 1,
            Scissors = 2
        }
        private Array gestureValues = Enum.GetValues(typeof(Gesture));
        private Gesture player1Gesture;
        private Gesture player2Gesture;

        #region AI Variables
        private int rocks = 0;
        private int papers = 0;
        private int scissors = 0;
        private Gesture lastGesture;
        private const int maxSameInRow = 2;
        #endregion

        public RockPaperScissors()
        {
            ColorfulWrite("Rock, Paper, Scissors!\n", ConsoleColor.White, ConsoleColor.Blue);
            ColorfulWrite("Please, enter the number of games:", ConsoleColor.White, ConsoleColor.Green);
            Console.Write(" ");
            int numberOfGames = Convert.ToInt32(Console.ReadLine());
            ColorfulWrite("Play with another player (Yes/No)? Else - AI:", ConsoleColor.White, ConsoleColor.Green);
            Console.Write(" ");
            string IsTwoPlayersInput = Console.ReadLine();
            bool IsTwoPlayers;
            if (IsTwoPlayersInput == "Yes" || IsTwoPlayersInput == "yes")
            {
                IsTwoPlayers = true;
            }
            else if (IsTwoPlayersInput == "No" || IsTwoPlayersInput == "no")
            {
                IsTwoPlayers = false;
            }
            else
            {
                ColorfulWrite("Invalid input, considered as No", ConsoleColor.White, ConsoleColor.Green);
                IsTwoPlayers = false;
            }
            Play(numberOfGames, IsTwoPlayers);   
        }
        private void Play(int numberOfGames, bool IsTwoPlayers)
        {
            List<string>  winners = new List<string>();
            string winner;
            for (int i = numberOfGames; i > 0; i--)
            {
                Console.Write("\n\n");
                if (IsTwoPlayers == false)
                {
                    ColorfulWrite("Player 1:", ConsoleColor.Black, ConsoleColor.White);
                    Console.Write(" ");
                    player1Gesture = GetPlayerGesture(Console.ReadLine());
                    player2Gesture = GetAIGesture();
                    CountPlayerGestureIn(player1Gesture);
                    ColorfulWrite($"Player 2 (AI):", ConsoleColor.Black, ConsoleColor.White);
                    Console.Write($" {player2Gesture}\n");
                }
                else
                {
                    ColorfulWrite("Player 1:", ConsoleColor.Black, ConsoleColor.White);
                    Console.Write(" ");
                    player1Gesture = GetPlayerGesture(Console.ReadLine());
                    ColorfulWrite("Player 2:", ConsoleColor.Black, ConsoleColor.White);
                    Console.Write(" ");
                    player2Gesture = GetPlayerGesture(Console.ReadLine());
                }
                winner = GetWinner(player1Gesture, player2Gesture);
                winners.Add(winner);

                ColorfulWrite("Winner:", ConsoleColor.Black, ConsoleColor.White);
                Console.Write($" {winner}\n");
                ColorfulWrite($"Games Left: {i - 1}", ConsoleColor.White, ConsoleColor.Red);
            }
            string mostOccuringCase = winners.GroupBy(el => el).OrderByDescending(group => group.Count()).Select(grp => grp.Key).First();
            int mostOccuringCaseCount = winners.Count(i => i == mostOccuringCase);
            ColorfulWrite($"Most occuring winning case: {mostOccuringCase}, {mostOccuringCaseCount} occuriences.\n", ConsoleColor.White, ConsoleColor.Green);
            ColorfulWrite("Thanks For Playing!", ConsoleColor.White, ConsoleColor.Blue);
        }
        private Gesture GetPlayerGesture(string input)
        {
            Gesture result;
            if (input == "Rock" || input == "rock")
            {
                result = RockPaperScissors.Gesture.Rock;
            }
            else if (input == "Paper" || input == "paper")
            {
                result = RockPaperScissors.Gesture.Paper;
            }
            else if (input == "Scissors" || input == "scissors")
            {
                result = RockPaperScissors.Gesture.Scissors;
            }
            else
            {
                throw new ArgumentException("Your gesture is not valid, only gestures supported: Rock, rock, Paper, paper, Scissors, scissors"); 
            }
            return result;
        }
        private Gesture GetAIGesture()
        {
            Random rnd = new Random();
            if (rocks >= maxSameInRow)
            {
                return Gesture.Paper;
            }
            else if (papers >= maxSameInRow)
            {
                return Gesture.Scissors;
            }
            else if (scissors >= maxSameInRow)
            {
                return Gesture.Rock;
            }
            else
            {
                return (Gesture)gestureValues.GetValue(rnd.Next(0, 3));
            } 
        }
        private void CountPlayerGestureIn(Gesture playerGesture)
        {
            if (playerGesture == Gesture.Rock)
            {
                rocks++;
                papers = scissors = 0;
            }
            if (playerGesture == Gesture.Paper)
            {
                papers++;
                rocks = scissors = 0;
            }
            if (playerGesture == Gesture.Scissors)
            {
                scissors++;
                rocks = papers = 0;
            }
        }
        private string GetWinner(Gesture p1, Gesture p2)
        {
            if (p1 == Gesture.Paper && p2 == Gesture.Rock)
            {
                return "Player 1";
            }
            else if (p1 == Gesture.Scissors && p2 == Gesture.Paper)
            {
                return "Player 1";
            }
            else if (p1 == Gesture.Rock && p2 == Gesture.Scissors)
            {
                return "Player 1";
            }
            else if (p1 == p2)
            {
                return "Draw";
            }
            else
            {
                return "Player 2";
            }
        }
        private void ColorfulWrite(string str, ConsoleColor foregroundColor) // Graphics 
        {
            Console.ForegroundColor = foregroundColor;
            Console.Write(str);
            Console.ResetColor();
        } 
        private void ColorfulWrite(string str, ConsoleColor foregroundColor, ConsoleColor backgroundColor) // Graphics 
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(str);
            Console.ResetColor();
        } 
    }
    class Program
    {
        static void Main(string[] args)
        {
            RockPaperScissors rps = new RockPaperScissors();
        }
    }
}
