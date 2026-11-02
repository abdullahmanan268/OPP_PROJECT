using System;
using System.Collections.Generic;
using LostAndFoundCSharp.Services;

namespace LostAndFoundCSharp.UI
{
    public class HistoryUI
    {
        private HistoryService historyService;
        private AuthService auth;

        public HistoryUI(HistoryService historyService, AuthService auth)
        {
            this.historyService = historyService;
            this.auth = auth;
        }

        public void ViewHistory()
        {
            if (auth.IsAdmin == false)
            {
                Console.WriteLine("ACCESS DENIED! Only Admin can view history.");
                ConsoleHelper.Pause();
                return;
            }

            Console.WriteLine("=== ACTIVITY HISTORY ===");
            Console.WriteLine();

            List<string> history = historyService.GetAll();

            if (history.Count == 0)
            {
                Console.WriteLine("No history available.");
            }
            else
            {
                for (int i = 0; i < history.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + history[i]);
                }
            }
            ConsoleHelper.Pause();
        }

        public void ClearHistory()
        {
            Console.WriteLine("=== CLEAR HISTORY ===");
            Console.WriteLine();

            historyService.Clear();
            Console.WriteLine("History cleared successfully!");
            ConsoleHelper.Pause();
        }
    }
}
