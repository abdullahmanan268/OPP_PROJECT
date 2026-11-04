using System.Collections.Generic;
using LostAndFoundCSharp.Models;
using LostAndFoundCSharp.Repositories;

namespace LostAndFoundCSharp.Services
{
    
    public class ItemService
    {
        private ItemRepository repo = new ItemRepository();
        private HistoryService historyService;
        private List<Item> items;

        public ItemService(HistoryService historyService)
        {
            this.historyService = historyService;
            items = repo.Load();
        }
        //chnage
        public List<Item> GetAll()
        {
            

           
            return items; 
        }

        public bool IsFull()
        {
            return items.Count >= 100;
        }

        
        public bool IsValidNumber(int number)
        {
            return number >= 1 && number <= items.Count;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            historyService.Add("Added " + item.Status + " item: " + item.Name);
            repo.Save(items);
        }

        //chnage
        public List<int> Search( bool useStatus, string status,
                                bool useRange, int from, int to)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < items.Count; i++)
            {
                bool match = false;
                int number = i + 1;

               
              

                
               if (useStatus)
                {
                    if (items[i].Status.ToLower() == status.ToLower())
                    {
                        match = true;
                    }
               }

                
                if (useRange)
                {
                    if (number >= from && number <= to)
                    {
                        match = true;
                    }
                }

                if (match)
                {
                    result.Add(number);
                }
            }
            return result;
        }

        public void UpdateItem(int number, string name, string description, string location, string date)
        {
            Item item = items[number - 1];
            item.Name = name;
            item.Description = description;
            item.Location = location;
            item.Date = date;

            historyService.Add("Updated item: " + name);
            repo.Save(items);
        }

        public void DeleteItem(int number)
        {
            historyService.Add("Deleted item: " + items[number - 1].Name);
            items.RemoveAt(number - 1);
            repo.Save(items);
        }

        public void ClaimItem(int number)
        {
            items[number - 1].Status = "claimed";
            historyService.Add("Claimed item: " + items[number - 1].Name);
            repo.Save(items);
        }

       
        public void SortByName()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                for (int j = 0; j < items.Count - 1 - i; j++)
                {
                    if (string.CompareOrdinal(items[j].Name, items[j + 1].Name) > 0)
                    {
                        Item temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
            historyService.Add("Sorted items by name");
            repo.Save(items);
        }

        
        public void SortByDate()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                for (int j = 0; j < items.Count - 1 - i; j++)
                {
                    if (string.CompareOrdinal(items[j].Date, items[j + 1].Date) > 0)
                    {
                        Item temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
            historyService.Add("Sorted items by date");
            repo.Save(items);
        }
    }
}
