using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoektQuiz.Models;

namespace BoektQuiz.Services
{
    public class MockDataStore : IDataStore<Vraag>
    {
        readonly List<Vraag> items;

        public MockDataStore()
        {
            items = new List<Vraag>()
            {
                new Vraag { Id = 0, Text = "Vraag 1", Answer="" },
                new Vraag { Id = 1, Text = "Vraag 2", Answer="" },
                new Vraag { Id = 2, Text = "Vraag 3", Answer="" },
                new Vraag { Id = 3, Text = "Vraag 4", Answer="" },
                new Vraag { Id = 4, Text = "Vraag 5", Answer="" },
                new Vraag { Id = 5, Text = "Vraag 6", Answer="" },
                new Vraag { Id = 6, Text = "Vraag 7", Answer="" },
                new Vraag { Id = 7, Text = "Vraag 8", Answer="" },
                new Vraag { Id = 8, Text = "Vraag 9", Answer="" },
                new Vraag { Id = 9, Text = "Vraag 10", Answer="" },
            };
        }

        public async Task<bool> AddItemAsync(Vraag item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Vraag item)
        {
            var oldItem = items.Where((Vraag arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Vraag arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Vraag> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Vraag>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}