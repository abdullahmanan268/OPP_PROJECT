using System.Collections.Generic;
using System.IO;
using LostAndFoundCSharp.Models;

namespace LostAndFoundCSharp.Repositories
{
    
    public class ItemRepository
    {
        private string path = DataPath.GetFile("items.txt");

        public List<Item> Load()
        {
            List<Item> items = new List<Item>();

            if (!File.Exists(path))
            {
                return items;
            }

            StreamReader reader = new StreamReader(path);
            string firstLine = reader.ReadLine();

            
            if (firstLine == null)
            {
                reader.Close();
                return items;
            }

            int count = int.Parse(firstLine.Trim());

            for (int i = 0; i < count; i++)
            {
                Item item = new Item();
                item.Name = reader.ReadLine();
                item.Description = reader.ReadLine();
                item.Location = reader.ReadLine();
                item.Date = reader.ReadLine();
                item.Status = reader.ReadLine();

                
                if (item.Status == null)
                {
                    break;
                }

                items.Add(item);
            }

            reader.Close();
            return items;
        }

        public void Save(List<Item> items)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(items.Count);

            for (int i = 0; i < items.Count; i++)
            {
                writer.WriteLine(items[i].Name);
                writer.WriteLine(items[i].Description);
                writer.WriteLine(items[i].Location);
                writer.WriteLine(items[i].Date);
                writer.WriteLine(items[i].Status);
            }

            writer.Close();
        }
    }
}
