using System;
using System.Collections.Generic;
using System.Linq;

namespace FekrBekrGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            string[] colorList = {"red", "blue", "green", "yellow", "orange", "purple", "black", "white"};

            Console.WriteLine("=== Welcome to Fekr Bekr Game ===");
            Console.WriteLine("1. Start Game | 0. Exit");
            var choice = Console.ReadLine();
            if (choice == "0") return;
            if (choice == "1")
            {
                var targetColors = GetRandomColors(colorList);
                

                Console.WriteLine("\nGuide: Enter colors with spaces (e.g., 1 2 3 4)");
                Console.WriteLine("Numbers: 1.red, 2.blue, 3.green, 4.yellow, 5.orange, 6.purple, 7.black, 8.white");

                // Max 10 attempts
                for (var attempt = 1; attempt <= 10; attempt++)
                {
                    Console.WriteLine($"\n--- Attempt {attempt} of 10 ---");
                    Console.Write("Your guess: ");
                    var input = Console.ReadLine();

                    if (input == "0") break; // Exit game

                    var inputParts = input.Split(' ');
                    if (inputParts.Length != 4)
                    {
                        Console.WriteLine("Please enter exactly 4 numbers!");
                        attempt--; // Don't count this attempt
                        continue;
                    }

                    var userGuesses = new List<string>();
                    try
                    {
                        foreach (var part in inputParts)
                        {
                            var colorIndex = int.Parse(part) - 1;
                            if (colorIndex < 0 || colorIndex >= colorList.Length)
                                throw new IndexOutOfRangeException("Color index out of range.");
                            userGuesses.Add(colorList[colorIndex]);
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format. Please enter numbers only.");
                        attempt--;
                        continue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine($"Invalid number. Please choose between 1 and {colorList.Length}.");
                        attempt--;
                        continue;
                    }
                    catch (Exception ex) // Catch any other unexpected errors
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        attempt--;
                        continue;
                    }

                    // Check win condition
                    var win = true;
                    for (var i = 0; i < 4; i++)
                    {
                        if (userGuesses[i] == targetColors[i])
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write($" {userGuesses[i]} ");
                        }
                        else if (targetColors.Contains(userGuesses[i]))
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($" {userGuesses[i]} ");
                            win = false;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write($" {userGuesses[i]} ");
                            win = false;
                        }

                        Console.ResetColor();
                        Console.Write(" ");
                    }

                    if (win)
                    {
                        Console.WriteLine("\nCongratulations! You won!");
                        return; // End game on win
                    }
                }

                // If loop finishes without winning
                Console.WriteLine("\nOut of attempts. The target colors were: " + string.Join(", ", targetColors));
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 1 or 0.");
            }
        }

        public static List<string> GetRandomColors(string[] colors)
        {
            var random = new Random();
            var selectedIndices = new HashSet<int>();
            while (selectedIndices.Count < 4)
                selectedIndices.Add(random.Next(0, colors.Length));
            return selectedIndices.Select(i => colors[i]).ToList();
        }
    }
}