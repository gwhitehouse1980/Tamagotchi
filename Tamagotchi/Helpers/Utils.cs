using System;

namespace Tamagotchi.Helpers
{
    public class Utils
    {
        public static void WriteAt(string s, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}