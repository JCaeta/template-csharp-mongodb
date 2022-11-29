using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template_csharp_mongodb.Entities;

namespace template_csharp_mongodb.Persistence.Entities
{
    public class MongoEntityB
    {

        public MongoEntityB(EntityB item)
        {
            if (item.Id != null)
            {
                this.Id = new ObjectId(item.Id);
            }
            this.Name = item.Name;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("idEntityA")]
        public ObjectId IdEntityA { get; set; }
    }
}
