using System;

namespace LostAndFoundCSharp.UI
{
    
    public static class ConsoleHelper
    {
        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static string ReadText(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        
        public static int ReadNumber(string message)
        {
            Console.Write(message);
            int number;
            if (int.TryParse(Console.ReadLine(), out number))
            {
                return number;
            }
            return 0;
        }

        public static void PrintHeader()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("    LOST AND FOUND MANAGEMENT SYSTEM");
            Console.WriteLine("============================================");
            Console.WriteLine();
        }
    }
}
