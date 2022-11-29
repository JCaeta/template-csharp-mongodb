using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Persistence.Repositories.StrategiesEntityAMongoRepository;
using template_csharp_mongodb.Persistence.Entities;

namespace template_csharp_mongodb.Persistence.Repositories
{
    public class EntityAMongoRepository<EntityA> : IRepository<EntityA>
    where EntityA : template_csharp_mongodb.Entities.EntityA
    {
        protected IMongoCollection<MongoEntityA> collection;
        protected IMongoDatabase database;
        protected MongoClient client;
        protected IClientSessionHandle sessionHandle;
        protected CancellationToken cancellationToken;
        protected IStrategyEntityAMongoRepository<EntityA> findStrategy;

        public EntityAMongoRepository(IClientSessionHandle sessionHandle, CancellationToken cancellationToken, IMongoDatabase database)
        {
            this.sessionHandle = sessionHandle;
            this.cancellationToken = cancellationToken;
            this.database = database;
            this.collection = this.database.GetCollection<MongoEntityA>("entitiesA");
        }

        public EntityAMongoRepository(IMongoDatabase database)
        {
            /**
             * This constructor is for only read operations
             */
            this.database = database;
            this.collection = this.database.GetCollection<MongoEntityA>("entitiesA");
        }

        public EntityA create(EntityA item)
        {
            // Insert EntityA
            MongoEntityA mongoEntityA = new MongoEntityA(item);
            this.collection.InsertOne(this.sessionHandle, mongoEntityA, cancellationToken: this.cancellationToken);
            item.Id = mongoEntityA.Id.ToString();

            // Insert EntitiesB
            EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(this.sessionHandle, this.cancellationToken, this.database);
            foreach(EntityB entityB in item.EntitiesB)
            {
                entityBMongoRepository.create(entityB, mongoEntityA.Id);
            }

            return item;
        }

        public void delete(EntityA item)
        {
            // Delete associated EntitiesB
            EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(this.sessionHandle, this.cancellationToken, this.database);
            foreach(EntityB entityB in item.EntitiesB)
            {
                entityBMongoRepository.delete(entityB);
            }

            // Delete EntityA
            MongoEntityA mongoEntityA = new MongoEntityA(item);
            FilterDefinition<MongoEntityA> filter = new BsonDocument("_id", mongoEntityA.Id);
            this.collection.DeleteOne(filter, this.cancellationToken);
        }

        public List<EntityA> find(EntityA item)
        {
            List<EntityA> listEntitiesA = this.findStrategy.find(item);
            return listEntitiesA;
        }

        public EntityA findOne(EntityA item)
        {
            return item;
        }

        public EntityA update(EntityA item)
        {
            return item;
        }

        // -------------------------------------------------------------------------------------------------------------------------- Custom methods
        public void setFindStrategy(IStrategyEntityAMongoRepository<EntityA> strategy)
        {
            this.findStrategy = strategy;
            this.findStrategy.Database = this.database;
            this.findStrategy.SessionHandle = this.sessionHandle;
            this.findStrategy.CancellationToken = this.cancellationToken;
            this.findStrategy.Collection = this.collection;
        }
    }
}



