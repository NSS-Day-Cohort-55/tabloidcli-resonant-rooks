
﻿using TabloidCLI.UserInterfaceManagers;
using System;
using System.Collections.Generic;


namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundSelect();
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
        static void BackgroundSelect()
        {
            List<string> backgroundColors = new List<string>
                {
                    "Black", "Dark Green", "Dark Yelow", "Dark Gray", "Cyan", "Magenta"
                };
            int index = 1;
            Console.WriteLine("Hello! Welcome to Tabloid!");
            Console.WriteLine("--------------------------");
            foreach (string color in backgroundColors)
            {
                Console.WriteLine($"{index}) {color}");
                index++;
            }
            Console.WriteLine(" ");
            Console.WriteLine("Please provide the number of color to set your background color.");
            string selectedColor = Console.ReadLine();
            switch (selectedColor)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    break;
                case "5":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                case "6":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    break;
            }
        }
    }
}
