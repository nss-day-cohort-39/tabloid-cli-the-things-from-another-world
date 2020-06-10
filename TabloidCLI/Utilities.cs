using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI
{
    public class Utilities
    {
        public static void ListColor()
        {
            // Get the list of available colors 
            // that can be changed 
            ConsoleColor[] consoleColors
                = (ConsoleColor[])ConsoleColor
                      .GetValues(typeof(ConsoleColor));

            // Display the list 
            // of available console colors 
            Console.WriteLine("List of available "
                              + "Console Colors:");
            foreach (var color in consoleColors)
                Console.WriteLine($"{Array.IndexOf(consoleColors, color)} - {color}");

        }
        
        public static void ChangeColor(int colorIndex)
        {

            // Display current Background color 
            Console.WriteLine("Default Background Color: {0}",
                              Console.BackgroundColor);

            // Set the Background color to blue 
            Console.BackgroundColor
                = ConsoleColor.Blue;

            // Display current Background color 
            Console.WriteLine("Changed Background Color: {0}",
                              Console.BackgroundColor);
        }

    }
}

