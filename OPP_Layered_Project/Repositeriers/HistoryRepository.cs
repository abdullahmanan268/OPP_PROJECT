using System.Collections.Generic;
using System.IO;

namespace LostAndFoundCSharp.Repositories
{
    
    public class HistoryRepository
    {
        private string path = DataPath.GetFile("history.txt");

        public List<string> Load()
        {
            List<string> history = new List<string>();

            if (!File.Exists(path))
            {
                return history;
            }

            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            while (line != null)
            {
                history.Add(line);
                line = reader.ReadLine();
            }

            reader.Close();
            return history;
        }

        public void Save(List<string> history)
        {
            StreamWriter writer = new StreamWriter(path);

            for (int i = 0; i < history.Count; i++)
            {
                writer.WriteLine(history[i]);
            }

            writer.Close();
        }
    }
}
