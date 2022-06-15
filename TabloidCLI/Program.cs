
﻿using TabloidCLI.UserInterfaceManagers;
using System;
using System.Collections.Generic;


namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
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
            Console.WriteLine("Please provide the number of color to set your background color.");
            Console.ReadLine()
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
