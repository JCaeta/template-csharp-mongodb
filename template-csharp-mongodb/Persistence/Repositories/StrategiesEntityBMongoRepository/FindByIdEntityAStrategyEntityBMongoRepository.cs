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
using template_csharp_mongodb.Persistence.Repositories.StrategiesEntityBMongoRepository;

namespace template_csharp_mongodb.Persistence.Repositories.StrategiesEntityBMongoRepository
{
    public class FindByIdEntityAStrategyEntityBMongoRepository<EntityB> : IStrategyEntityBMongoRepository<EntityB>
    where EntityB: template_csharp_mongodb.Entities.EntityB, new()
    {
        public IMongoCollection<MongoEntityB> Collection { get; set; }
        public IMongoDatabase Database { get; set; }
        IClientSessionHandle SessionHandle { get; set; }
        CancellationToken CancellationToken { get; set; }
        public ObjectId IdEntityA { get; set; }
        IClientSessionHandle IStrategyEntityBMongoRepository<EntityB>.SessionHandle { get; set ; }
        CancellationToken IStrategyEntityBMongoRepository<EntityB>.CancellationToken { get; set; }

        public List<EntityB> find(EntityB item)
        {
            // The item is not used here
            /*
             * The item is not used here
             * The EntityA id it has to be set by properties
             */

            FilterDefinition<MongoEntityB> filter = new BsonDocument("idEntityA", this.IdEntityA);
            List<MongoEntityB> result = this.Collection.Find(filter).ToList();
            List<EntityB> entitiesB = new List<EntityB>();
            foreach(MongoEntityB mongoEntityB in result)
            {
                EntityB entityB = new EntityB();
                entityB.Id = mongoEntityB.Id.ToString();
                entityB.Name = mongoEntityB.Name;
                entitiesB.Add(entityB);
            }
            return entitiesB;
        }
    }
}
