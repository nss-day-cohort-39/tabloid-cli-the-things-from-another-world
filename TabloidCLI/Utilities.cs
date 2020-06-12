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

            // Get the list of available colors 
            ConsoleColor[] consoleColors
                = (ConsoleColor[])ConsoleColor
                      .GetValues(typeof(ConsoleColor));

            ConsoleColor color = consoleColors[colorIndex];


            // Set the Background color
            Console.BackgroundColor = color;

            switch (colorIndex)
            {
                case 3:
                case 6:
                case 7:
                case 10:
                case 11:
                case 14:
                case 15:
                    Console.ForegroundColor = ConsoleColor.Black;
                    return;
                case 0:
                case 1:
                case 2:
                case 4:
                case 5:
                case 8:
                case 9:
                case 12:
                case 13:
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
            }

            // Display current Background color 
            Console.WriteLine("Changed Background Color: {0}",
                              Console.BackgroundColor);
        }

    }
}

