using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Eye_Killer
{
    class Program
    {

        static void Main()
        {
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine("Welcome to Eye Killer!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter - Play Game");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Esc - Exit");
                Console.ResetColor();

                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case (char)ConsoleKey.Enter:
                        Console.Clear();
                        // Turn on GamePlay
                        Game game = new Game();
                        game.Gameplay();
                        break;

                    case (char)ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("Goodbey");
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nInvalid choice.\nPlease press 'enter' to play the game or 'esc' to exit.");
                        return;
                }
            }
        }

        class Game
        {
            private int lives;
            private int correctShots;
            private int shotsToWin;
            private Random random;
            private HashSet<int> generatedNumbers;
            private bool isPaused = false;

            public Game()
            {
                lives = 3;
                correctShots = 0;
                shotsToWin = 5;
                random = new Random();
                generatedNumbers = new HashSet<int>();
            }

            public void Gameplay()
            {
                Console.WriteLine("Welcome to Eye Killer!");
                SkipCutScene("In this game, your eyes can shoot lasers. Press 'z' to shoot at even numbers and 'x' for odd numbers.");
                SkipCutScene($"You have {lives} lives, and you need to successfully shoot lasers {shotsToWin} times to win.");

                while (lives > 0 && correctShots < shotsToWin)
                {
                    int randomNumber = GenerateRandomNumber();
                    DisplayGameHub(randomNumber, 'Z', 'X');
                    // Read a single key from the user
                    ConsoleKeyInfo key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            break;

                        case ConsoleKey.Z:
                            if (randomNumber % 2 == 0)
                            {
                                DisplayAction($"Nice shot: {ConsoleKey.Z}");
                                correctShots++;

                                if (correctShots == shotsToWin)
                                {
                                    DisplayGameWon();
                                    break;
                                }
                            }
                            else
                            {
                                DisplayAction("Incorrect shot!");
                                lives--;

                                if (lives == 0)
                                {
                                    DisplayGameOver();
                                    break;
                                }
                            }
                            break;

                        case ConsoleKey.X:
                            if (randomNumber % 2 != 0)
                            {
                                DisplayAction($"Nice shot: {ConsoleKey.X}");
                                correctShots++;

                                if (correctShots == shotsToWin)
                                {
                                    DisplayGameWon();
                                    break;
                                }
                            }
                            else
                            {
                                DisplayAction("Incorrect shot!");
                                lives--;

                                if (lives == 0)
                                {
                                    DisplayGameOver();
                                    break;
                                }
                            }
                            break;


                        default:
                            Console.Clear();
                            Console.WriteLine("\nInvalid choice.\nPlease press 'z' or 'x' to shoot or 'esc' to exit.");
                            break;



                    }


                }
            }
            private void DisplayGameHub(int number, char buttonZ, char buttonX)
            {
                Console.Clear(); // Clear the console before displaying the new information
                Console.WriteLine($"Number: {number}");
                Console.WriteLine($"Score: {correctShots}/{shotsToWin}");
                Console.WriteLine($"Hearts: {lives}♥");
                Console.WriteLine($"Press '{buttonZ}' to shoot at even numbers.");
                Console.WriteLine($"Press '{buttonX}' to shoot at odd numbers.");
                Console.WriteLine("Press 'Esc' at any time to exit the game.");
            }

            private void DisplayAction(string action)
            {
                Console.Clear();
                Console.WriteLine(action);
                Console.ReadKey(); // Wait for user input before displaying the next number
            }

            private int GenerateRandomNumber()
            {
                int randomNumber;

                do
                {
                    randomNumber = random.Next(11); // Generate a random number between 0 and 10
                } while (!generatedNumbers.Add(randomNumber));

                return randomNumber;
            }

            private void DisplayGameOver()
            {
                Console.Clear();
                Console.WriteLine("Game Over! You're out of lives.");
                Console.ReadKey();
                Console.Clear();
            }

            private void DisplayGameWon()
            {
                Console.Clear();
                Console.WriteLine("Congratulations!\n You've successfully shot lasers five times!");
                Console.ReadKey();
                Console.Clear();
            }

            private void SkipCutScene(string text)
            {
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true); // Consume the key press
                        Console.Clear();
                        Console.WriteLine(text);
                        break; // Exit the loop after the first key press
                    }
                }
            }
        }
    }
}