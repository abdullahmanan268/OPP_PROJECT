using System;
using LostAndFoundCSharp.Services;

namespace LostAndFoundCSharp.UI
{
    public class LoginUI
    {
        private AuthService auth;

        public LoginUI(AuthService auth)
        {
            this.auth = auth;
        }

       
        public bool Show()
        {
            ConsoleHelper.PrintHeader();
            Console.WriteLine("Select User Type:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. User");
            int userType = ConsoleHelper.ReadNumber("Enter choice: ");

            if (userType == 1)
            {
                int attempts = 0;
                while (attempts < 3)
                {
                    Console.WriteLine();
                    string password = ConsoleHelper.ReadText("Enter Password: ");

                    if (auth.CheckPassword(password))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Login Successful!");
                        ConsoleHelper.Pause();
                        return true;
                    }

                    attempts++;
                    Console.WriteLine("Wrong Password! Attempts left: " + (3 - attempts));
                }

                Console.WriteLine("Too many failed attempts. Program will exit.");
                return false;
            }

            ConsoleHelper.Pause();
            return true;
        }
    }
}
