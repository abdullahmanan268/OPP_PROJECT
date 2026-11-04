using System;
using System.Collections.Generic;
using LostAndFoundCSharp.Models;
using LostAndFoundCSharp.Services;

namespace LostAndFoundCSharp.UI
{
    // All screens related to items
    public class ItemUI
    {
        private ItemService itemService;
        private AuthService auth;

        public ItemUI(ItemService itemService, AuthService auth)
        {
            this.itemService = itemService;
            this.auth = auth;
        }

        public void AddLostItem()
        {
            AddItem("=== ADD LOST ITEM ===", "lost", "Enter Location Lost: ", "Lost item added successfully!");
        }

        public void AddFoundItem()
        {
            AddItem("=== ADD FOUND ITEM ===", "found", "Enter Location Found: ", "Found item added successfully!");
        }

        // Used by both lost and found (only status and messages are different)
        private void AddItem(string title, string status, string locationMessage, string successMessage)
        {
            Console.WriteLine(title);
            Console.WriteLine();

            if (itemService.IsFull())
            {
                Console.WriteLine("Storage is full!");
            }
            else
            {
                Item item = new Item();
                item.Name = ConsoleHelper.ReadText("Enter Item Name: ");
                item.Description = ConsoleHelper.ReadText("Enter Description: ");
                item.Location = ConsoleHelper.ReadText(locationMessage);
                item.Date = ConsoleHelper.ReadText("Enter Date (dd/mm/yyyy): ");
                item.Status = status;

                itemService.AddItem(item);

                Console.WriteLine();
                Console.WriteLine(successMessage);
            }
            ConsoleHelper.Pause();
        }

        public void ViewAllItems()
        {
            Console.WriteLine("===  PAGINATED VIEW ALL ITEMS ===");
            Console.WriteLine();

            List<Item> items = itemService.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items in the system.");
            }
            else
            {
                Console.WriteLine("No. | Name | Description | Location | Date | Status");
                Console.WriteLine("----------------------------------------------------");

                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + items[i]);
                }
            }
            ConsoleHelper.Pause();
        }
//change search thing (question 2)
        public void SearchItems()
        {
            Console.WriteLine("=== SEARCH ITEMS ===");
            Console.WriteLine("You can search by 1,and 2 or all 2 options. Each option shows its own results.");
            Console.WriteLine();

            
            bool useStatus = AskYesNo("Search by Status? (y/n): ");
            bool useRange = AskYesNo("Search by Range? (y/n): ");

            if ( useStatus == false && useRange == false)
            {
                Console.WriteLine();
                Console.WriteLine("Please select at least one search option!");
                ConsoleHelper.Pause();
                return;
            }

            string name = "";
            string status = "";
            int from = 0;
            int to = 0;
            //change
           
            if (useStatus)
            {
                status = ConsoleHelper.ReadText("Enter status (lost / found / claimed): ");
            }
            if (useRange)
            {
                from = ConsoleHelper.ReadNumber("Enter range start number: ");
                to = ConsoleHelper.ReadNumber("Enter range end number: ");

                if (from < 1 || to < from)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid range!");
                    ConsoleHelper.Pause();
                    return;
                }
            }
            //change
            List<int> results = itemService.Search( useStatus, status, useRange, from, to);
            List<Item> items = itemService.GetAll();
           Console.WriteLine("FRONT VIEW");

            Console.WriteLine();
            if (results.Count == 0)
            {
                Console.WriteLine("No record found!");
            }
            else
            {
                Console.WriteLine("Search Results:");
                Console.WriteLine("----------------------------------------------------");
                for (int i = 0; i < results.Count; i++)
                {
                    int number = results[i];
                    Console.WriteLine(number + ". " + items[number - 1]);
                }
            }
            ConsoleHelper.Pause();
        }

        // Asks a yes/no question, returns true for y or Y
        private bool AskYesNo(string message)
        {
            string answer = ConsoleHelper.ReadText(message);
            return answer.Trim().ToLower() == "y";
        }

        public void UpdateItem()
        {
            Console.WriteLine("=== UPDATE ITEM ===");
            Console.WriteLine();

            if (itemService.GetAll().Count == 0)
            {
                Console.WriteLine("No items to update.");
            }
            else
            {
                ShowNameAndStatus();

                Console.WriteLine();
                int number = ConsoleHelper.ReadNumber("Enter item number to update: ");

                if (itemService.IsValidNumber(number) == false)
                {
                    Console.WriteLine("Invalid item number!");
                }
                else
                {
                    string name = ConsoleHelper.ReadText("Enter new Name: ");
                    string description = ConsoleHelper.ReadText("Enter new Description: ");
                    string location = ConsoleHelper.ReadText("Enter new Location: ");
                    string date = ConsoleHelper.ReadText("Enter new Date (dd/mm/yyyy): ");

                    itemService.UpdateItem(number, name, description, location, date);

                    Console.WriteLine();
                    Console.WriteLine("Item updated successfully!");
                }
            }
            ConsoleHelper.Pause();
        }

        public void DeleteItem()
        {
            if (auth.IsAdmin == false)
            {
                Console.WriteLine("ACCESS DENIED! Only Admin can delete items.");
                ConsoleHelper.Pause();
                return;
            }

            Console.WriteLine("=== DELETE ITEM ===");
            Console.WriteLine();

            if (itemService.GetAll().Count == 0)
            {
                Console.WriteLine("No items to delete.");
            }
            else
            {
                ShowNameAndStatus();

                Console.WriteLine();
                int number = ConsoleHelper.ReadNumber("Enter item number to delete: ");

                if (itemService.IsValidNumber(number) == false)
                {
                    Console.WriteLine("Invalid item number!");
                }
                else
                {
                    itemService.DeleteItem(number);

                    Console.WriteLine();
                    Console.WriteLine("Item deleted successfully!");
                }
            }
            ConsoleHelper.Pause();
        }

        public void ClaimItem()
        {
            Console.WriteLine("=== CLAIM ITEM ===");
            Console.WriteLine();

            List<Item> items = itemService.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items available.");
            }
            else
            {
                bool found = false;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Status == "found")
                    {
                        Console.WriteLine((i + 1) + ". " + items[i].Name + " - Found at: " + items[i].Location);
                        found = true;
                    }
                }

                if (found == false)
                {
                    Console.WriteLine("No found items available to claim.");
                }
                else
                {
                    Console.WriteLine();
                    int number = ConsoleHelper.ReadNumber("Enter item number to claim: ");

                    if (itemService.IsValidNumber(number) == false)
                    {
                        Console.WriteLine("Invalid item number!");
                    }
                    else if (items[number - 1].Status != "found")
                    {
                        Console.WriteLine("This item is not available for claiming!");
                    }
                    else
                    {
                        itemService.ClaimItem(number);

                        Console.WriteLine();
                        Console.WriteLine("Item claimed successfully!");
                    }
                }
            }
            ConsoleHelper.Pause();
        }

        public void SortItemsByName()
        {
            if (auth.IsAdmin == false)
            {
                Console.WriteLine("ACCESS DENIED! Only Admin can sort items.");
                ConsoleHelper.Pause();
                return;
            }

            Console.WriteLine("=== SORT ITEMS BY NAME (A-Z) ===");
            Console.WriteLine();

            if (itemService.GetAll().Count == 0)
            {
                Console.WriteLine("No items to sort.");
            }
            else
            {
                itemService.SortByName();

                Console.WriteLine("Items sorted by name successfully!");
                Console.WriteLine();
                ShowNameAndStatus();
            }
            ConsoleHelper.Pause();
        }

        public void SortItemsByDate()
        {
            if (auth.IsAdmin == false)
            {
                Console.WriteLine("ACCESS DENIED! Only Admin can sort items.");
                ConsoleHelper.Pause();
                return;
            }

            Console.WriteLine("=== SORT ITEMS BY DATE ===");
            Console.WriteLine();

            if (itemService.GetAll().Count == 0)
            {
                Console.WriteLine("No items to sort.");
            }
            else
            {
                itemService.SortByDate();

                Console.WriteLine("Items sorted by date successfully!");
                Console.WriteLine();

                List<Item> items = itemService.GetAll();
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + items[i].Name + " | " + items[i].Date + " | " + items[i].Status);
                }
            }
            ConsoleHelper.Pause();
        }

        // Used for lost / found / claimed lists
        public void ViewItemsByStatus(string status, string title, string noMessage)
        {
            Console.WriteLine("=== " + title + " ===");
            Console.WriteLine();

            List<Item> items = itemService.GetAll();
            bool found = false;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Status == status)
                {
                    Console.WriteLine((i + 1) + ". " + items[i].Name + " | " + items[i].Description
                        + " | " + items[i].Location + " | " + items[i].Date);
                    found = true;
                }
            }

            if (found == false)
            {
                Console.WriteLine(noMessage);
            }
            ConsoleHelper.Pause();
        }

        // Prints "1. Name (status)" for all items
        private void ShowNameAndStatus()
        {
            List<Item> items = itemService.GetAll();
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + items[i].Name + " (" + items[i].Status + ")");
            }
        }
    }
}
