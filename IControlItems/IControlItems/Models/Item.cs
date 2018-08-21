using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IControlItems.Models
{
    public class Item
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Cantidad { get; set; }

        public string Tipo { get; set; }
    }
}
