using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IControlItems.Models;

namespace IControlItems.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetItem(string id);

        Task AddItem(Item item);

        Task<bool> RemoteItem(string id);

        Task<bool> UpdateItem(string id, Item item);

    }
}
