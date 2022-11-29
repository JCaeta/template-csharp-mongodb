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
    public class FindByNameStrategyEntityAMongoRepository<EntityA> : IStrategyEntityAMongoRepository<EntityA>
    where EntityA : template_csharp_mongodb.Entities.EntityA, new()
    {

        public IMongoDatabase Database { get; set; }
        public IMongoCollection<MongoEntityA> Collection { get; set; }

        public IClientSessionHandle SessionHandle { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public List<EntityA> find(EntityA item)
        {
            // Find entityA
            FilterDefinition<MongoEntityA> filter = new BsonDocument("name", item.Name);
            List<MongoEntityA> result = this.Collection.Find(filter).ToList();

            // Find entitiesB of the entityA
            EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(this.SessionHandle, this.CancellationToken, this.Database);
            FindByIdEntityAStrategyEntityBMongoRepository<EntityB> strategy = new FindByIdEntityAStrategyEntityBMongoRepository<EntityB>();
            entityBMongoRepository.setFindStrategy(strategy);

            List<EntityA> listEntitiesA = new List<EntityA>();
            foreach (MongoEntityA mongoEntityA in result)
            {
                entityBMongoRepository.setIdEntityAStrategy(mongoEntityA.Id);
                List<EntityB> listEntitiesB = entityBMongoRepository.find(new EntityB());
                EntityA entityA = new EntityA();
                entityA.Id = mongoEntityA.Id.ToString();
                entityA.Name = mongoEntityA.Name;
                entityA.EntitiesB = listEntitiesB;
                listEntitiesA.Add(entityA);
            }
            return listEntitiesA;
        }
    }
}
