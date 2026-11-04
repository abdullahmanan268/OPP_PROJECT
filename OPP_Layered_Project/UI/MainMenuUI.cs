using System;
using LostAndFoundCSharp.Services;

namespace LostAndFoundCSharp.UI
{
    
    public class MainMenuUI
    {
        private AuthService auth;
        private LoginUI loginUI;
        private ItemUI itemUI;
        private HistoryUI historyUI;

        public MainMenuUI()
        {
            auth = new AuthService();
            HistoryService historyService = new HistoryService();
            ItemService itemService = new ItemService(historyService);

            loginUI = new LoginUI(auth);
            itemUI = new ItemUI(itemService, auth);
            historyUI = new HistoryUI(historyService, auth);
        }

        public void Start()
        {
            if (loginUI.Show() == false)
            {
                return;
            }

            while (true)
            {
                DisplayMenu();
                Console.WriteLine();
                int choice = ConsoleHelper.ReadNumber("Enter your choice: ");

                Console.Clear();

                if (ProcessChoice(choice) == false)
                {
                    break;
                }
            }
        }

        private void DisplayMenu()
        {
            ConsoleHelper.PrintHeader();

            if (auth.IsAdmin)
            {
                Console.WriteLine("Logged in as: ADMIN");
            }
            else
            {
                Console.WriteLine("Logged in as: USER");
            }
            Console.WriteLine();
            Console.WriteLine("1.  Add Lost Item");
            Console.WriteLine("2.  Add Found Item");
            Console.WriteLine("3.   paginated view of all Items");
            Console.WriteLine("4.  Search Items");
            Console.WriteLine("5.  Update Item");

            if (auth.IsAdmin)
            {
                Console.WriteLine("6.  Delete Item");
            }

            Console.WriteLine("7.  Claim Item");

            if (auth.IsAdmin)
            {
                Console.WriteLine("8.  View History");
                Console.WriteLine("9.  Sort Items by Name (A-Z)");
                Console.WriteLine("10. Sort Items by Date");
            }

            Console.WriteLine("11. View Lost Items Only");
            Console.WriteLine("12. View Found Items Only");
            Console.WriteLine("13. View Claimed Items Only");

            if (auth.IsAdmin)
            {
                Console.WriteLine("14. Clear History (Admin Only)");
                Console.WriteLine("15. Exit");
            }
            else
            {
                Console.WriteLine("14. Exit");
            }
        }

        
        private bool ProcessChoice(int choice)
        {
            if (choice == 1)
            {
                itemUI.AddLostItem();
            }
            else if (choice == 2)
            {
                itemUI.AddFoundItem();
            }
            else if (choice == 3)
            {
                itemUI.ViewAllItems();
            }
            else if (choice == 4)
            {
                itemUI.SearchItems();
            }
            else if (choice == 5)
            {
                itemUI.UpdateItem();
            }
            else if (choice == 6)
            {
                itemUI.DeleteItem();
            }
            else if (choice == 7)
            {
                itemUI.ClaimItem();
            }
            else if (choice == 8)
            {
                historyUI.ViewHistory();
            }
            else if (choice == 9)
            {
                itemUI.SortItemsByName();
            }
            else if (choice == 10)
            {
                itemUI.SortItemsByDate();
            }
            else if (choice == 11)
            {
                itemUI.ViewItemsByStatus("lost", "LOST ITEMS", "No lost items.");
            }
            else if (choice == 12)
            {
                itemUI.ViewItemsByStatus("found", "FOUND ITEMS", "No found items.");
            }
            else if (choice == 13)
            {
                itemUI.ViewItemsByStatus("claimed", "CLAIMED ITEMS", "No claimed items.");
            }
            else if (choice == 14)
            {
                if (auth.IsAdmin)
                {
                    historyUI.ClearHistory();
                }
                else
                {
                    Console.WriteLine("Exiting the system. Goodbye!");
                    return false;
                }
            }
            else if (choice == 15)
            {
                if (auth.IsAdmin)
                {
                    Console.WriteLine("Exiting the system. Goodbye!");
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please try again.");
                    ConsoleHelper.Pause();
                }
            }
            else
            {
                Console.WriteLine("Invalid choice! Please enter a valid option.");
                ConsoleHelper.Pause();
            }
            return true;
        }
    }
}
