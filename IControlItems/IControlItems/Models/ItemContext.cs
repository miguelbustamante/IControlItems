
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IControlItems.Models
{
    public class ItemContext : DbContext
    {
        private readonly IMongoDatabase _database = null;

        public ItemContext(IOptions<Settings> settings) {

            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }


        public IMongoCollection<Item> Items
        {
            get
            {
                return _database.GetCollection<Item>("Item");
            }
        }
    }
}
