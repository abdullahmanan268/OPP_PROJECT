using System.Collections.Generic;
using LostAndFoundCSharp.Repositories;

namespace LostAndFoundCSharp.Services
{
    
    public class HistoryService
    {
        private HistoryRepository repo = new HistoryRepository();
        private List<string> history;

        public HistoryService()
        {
            history = repo.Load();
        }

        public List<string> GetAll()
        {
            return history;
        }

        public void Add(string text)
        {
            if (history.Count >= 200)
            {
                history.RemoveAt(0);   // remove oldest
            }
            history.Add(text);
            repo.Save(history);
        }

        public void Clear()
        {
            history.Clear();
            repo.Save(history);
        }
    }
}
