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

namespace template_csharp_mongodb.Persistence.Repositories.StrategiesEntityAMongoRepository
{
    public class FindByEntityBNameStrategyEntityAMongoRepository<EntityA>: IStrategyEntityAMongoRepository<EntityA>
    where EntityA: template_csharp_mongodb.Entities.EntityA, new()
    {
        public IMongoDatabase Database { get ; set ; }
        public IMongoCollection<MongoEntityA> Collection { get; set; }

        public MongoClient Client { get; set; }
        public IClientSessionHandle SessionHandle { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public List<EntityA> find(EntityA entityA)
        {
            // Set strategy to find the entitiesB of entitiesA
            EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(this.Database);
            FindByIdEntityAStrategyEntityBMongoRepository<EntityB> strategy = new FindByIdEntityAStrategyEntityBMongoRepository<EntityB>();
            entityBMongoRepository.setFindStrategy(strategy);

            // Find entitiesA id
            List<ObjectId> idEntitiesA = entityBMongoRepository.findIdEntityAByNameEntityB(entityA.EntitiesB[0].Name);

            // Build complete EntityA objects
            List<EntityA> entitiesA = new List<EntityA>();

            foreach (ObjectId id in idEntitiesA)
            {
                entityBMongoRepository.setIdEntityAStrategy(id);

                List<EntityB> entitiesB = entityBMongoRepository.find(new EntityB());

                FilterDefinition<MongoEntityA> filter = new BsonDocument("_id", id);
                MongoEntityA result = this.Collection.Find(filter).FirstOrDefault();

                EntityA entA = new EntityA();
                entA.Id = result.Id.ToString();
                entA.Name = result.Name;
                entA.EntitiesB = entitiesB;
                entitiesA.Add(entA);
            }

            return entitiesA;
        }
    }
}
