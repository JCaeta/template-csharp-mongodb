using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Persistence.Entities;
using template_csharp_mongodb.Persistence.Repositories.StrategiesEntityBMongoRepository;

namespace template_csharp_mongodb.Persistence.Repositories
{
    public class EntityBMongoRepository<EntityB> : IRepository<EntityB>
    where EntityB : template_csharp_mongodb.Entities.EntityB, new()
    {
        protected IMongoCollection<MongoEntityB> collection;
        protected IMongoDatabase database;
        protected MongoClient client;
        protected IClientSessionHandle sessionHandle;
        protected CancellationToken cancellationToken;
        public IStrategyEntityBMongoRepository<EntityB> findStrategy;

        public EntityBMongoRepository(IClientSessionHandle sessionHandle, CancellationToken cancellationToken, IMongoDatabase database)
        {
            // This constructor is for write operations. A transaction is necessary
            this.sessionHandle = sessionHandle;
            this.cancellationToken = cancellationToken;
            this.database = database;
            this.collection = this.database.GetCollection<MongoEntityB>("entitiesB");
        }

        public EntityBMongoRepository(IMongoDatabase database)
        {
            // This constructor is just for only read operations
            this.database = database;
            this.collection = this.database.GetCollection<MongoEntityB>("entitiesB");
        }


        public EntityB create(EntityB item)
        {
            MongoEntityB mongoEntityB = new MongoEntityB(item);
            this.collection.InsertOne(this.sessionHandle, mongoEntityB, cancellationToken: this.cancellationToken);
            item.Id = mongoEntityB.Id.ToString();
            return item;
        }

        public EntityB create(EntityB item, ObjectId idEntityA)
        {
            MongoEntityB mongoEntityB = new MongoEntityB(item);
            mongoEntityB.IdEntityA = idEntityA;
            this.collection.InsertOne(this.sessionHandle, mongoEntityB, cancellationToken: this.cancellationToken);
            item.Id = mongoEntityB.Id.ToString();
            return item;
        }

        public void delete(EntityB item)
        {
            MongoEntityB mongoEntityB = new MongoEntityB(item);
            FilterDefinition<MongoEntityB> filter = new BsonDocument("_id", mongoEntityB.Id);

            this.collection.DeleteOne(filter, this.cancellationToken);
        }

        public List<EntityB> find(EntityB item)
        {
            this.findStrategy.Collection = this.collection;
            List<EntityB> listEntitiesB = findStrategy.find(item);

            return listEntitiesB;
        }

        public EntityB findOne(EntityB item)
        {
            throw new NotImplementedException();
        }

        public EntityB update(EntityB item)
        {
            throw new NotImplementedException();
        }

        // ---------------------------------------------------------------------------- Custom methods
        public List<ObjectId> findIdEntityAByNameEntityB(string name)
        {
            FilterDefinition<MongoEntityB> filter = new BsonDocument("name", name);

            var result = this.collection.Find(filter).ToList();

            List<ObjectId> ids = new List<ObjectId>();
            foreach(var r in result)
            {
                ids.Add(r.IdEntityA);
            }
            ids = ids.Distinct().ToList();
            return ids;
        }

        public void setFindStrategy(IStrategyEntityBMongoRepository<EntityB> strategy)
        {
            this.findStrategy = strategy;
            this.findStrategy.SessionHandle = this.sessionHandle;
            this.findStrategy.CancellationToken = this.cancellationToken;
            this.findStrategy.Database = this.database;
            this.findStrategy.Collection = this.collection;
        }

        public void setIdEntityAStrategy(ObjectId idEntityA)
        {
            this.findStrategy.IdEntityA = idEntityA;
        }
    }
}
