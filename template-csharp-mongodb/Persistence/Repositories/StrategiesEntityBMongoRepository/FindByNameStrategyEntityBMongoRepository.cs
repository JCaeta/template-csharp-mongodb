using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Persistence.Entities;

namespace template_csharp_mongodb.Persistence.Repositories.StrategiesEntityBMongoRepository
{
    public class FindByNameStrategyEntityBMongoRepository<EntityB> : IStrategyEntityBMongoRepository<EntityB>
    where EntityB: template_csharp_mongodb.Entities.EntityB, new()
    {
        public IMongoCollection<MongoEntityB> Collection { get; set; }
        public IMongoDatabase Database { get; set; }
        public IClientSessionHandle SessionHandle { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public ObjectId IdEntityA { get; set; }
        public List<EntityB> find(EntityB entityB)
        {
            List<EntityB> listEntitiesB = new List<EntityB>();
            FilterDefinition<MongoEntityB> filter = new BsonDocument("name", entityB.Name);

            // Search
            var result = this.Collection.Find(filter).ToList();
            foreach (var r in result)
            {
                EntityB entB = new EntityB();
                entB.Id = r.Id.ToString();
                entB.Name = r.Name;
                listEntitiesB.Add(entB);
            }
            return listEntitiesB;
        }
    }
}
