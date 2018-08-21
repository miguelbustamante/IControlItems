using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IControlItems.Interfaces;
using IControlItems.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace IControlItems.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemContext _context = null;

        public ItemRepository(IOptions<Settings> settings)
        {
            _context = new ItemContext(settings);
        }

        public async Task AddItem(Item item)
        {
            try
            {
                await _context.Items.InsertOneAsync(item);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            try {
                return await _context.Items.Find(_ => true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Item> GetItem(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Items.Find(note => note.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoteItem(string id)
        {
            try
            {
                DeleteResult actionResult =
                    await _context.Items.DeleteOneAsync(
                            Builders<Item>.Filter.Eq("Id", id));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateItem(string id, Item item)
        {
            try
            {
                ReplaceOneResult actionResult =
                    await _context.Items.ReplaceOneAsync(n => n.Id.Equals(id)
                        , item
                        , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
