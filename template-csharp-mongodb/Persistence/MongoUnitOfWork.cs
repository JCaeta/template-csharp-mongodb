using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Persistence.Entities;
using template_csharp_mongodb.Persistence.Repositories;
using template_csharp_mongodb.Persistence.Repositories.StrategiesEntityAMongoRepository;
using template_csharp_mongodb.Persistence.Repositories.StrategiesEntityBMongoRepository;

namespace template_csharp_mongodb.Persistence
{
    public class MongoUnitOfWork : IUnitOfWork
    {
        private MongoClient client;
        private IMongoDatabase database;
        private string uri;
        private string databaseName;
        private IClientSessionHandle session;
        private TransactionOptions transactionOptions;
        private CancellationToken cancellationToken;

        public MongoUnitOfWork(string uri, string databaseName)
        {
            this.uri = uri;
            this.databaseName = databaseName;
            this.client = new MongoClient(this.uri);
            this.database = client.GetDatabase(this.databaseName);
        }

        public void connect()
        {
            /**
             * Create all necessary object to perform a transaction
             */

            this.session = this.client.StartSession();
            this.transactionOptions = new TransactionOptions(
                readPreference: ReadPreference.Primary,
                readConcern: ReadConcern.Local,
                writeConcern: WriteConcern.WMajority);

            this.cancellationToken = CancellationToken.None; // normally a real token would be used
        }

        public void createEntityA(EntityA entityA)
        {

            using (this.session)
            {
                var result = session.WithTransaction(
                (s, ct) =>
                {
                    /*
                    s: IClientSessionHandle object
                    ct: CancellationToken object
                    */
                    EntityAMongoRepository<EntityA> entityAMongoRepository = new EntityAMongoRepository<EntityA>(s, ct, this.database);
                    entityAMongoRepository.create(entityA);
                    return "EntityA inserted";
                },
                this.transactionOptions,
                this.cancellationToken);
            }
        }

        public void createEntityB(EntityB entityB)
        {
            using (this.session)
            {
                var result = session.WithTransaction(
                (s, ct) =>
                {
                    /*
                    s: IClientSessionHandle object
                    ct: CancellationToken object
                    */
                    EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(s, ct, this.database);
                    entityBMongoRepository.create(entityB);
                    return "EntityB inserted";
                },
                this.transactionOptions,
                this.cancellationToken);
            }
        }

        public void createEntitiesB(List<EntityB> entitiesB)
        {
            using (this.session)
            {
                var result = session.WithTransaction(
                (s, ct) =>
                {
                    /*
                    s: IClientSessionHandle object
                    ct: CancellationToken object
                    */
                    EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(s, ct, this.database);

                    foreach(EntityB entityB in entitiesB)
                    {
                        entityBMongoRepository.create(entityB);
                    }
                    return "Entities B inserted";
                },
                this.transactionOptions,
                this.cancellationToken);
            }
        }

        public void deleteEntityB(EntityB entityB)
        {
            using (this.session)
            {
                var result = session.WithTransaction(
                (s, ct) =>
                {
                    /*
                    s: IClientSessionHandle object
                    ct: CancellationToken object
                    */
                    EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(s, ct, this.database);
                    entityBMongoRepository.delete(entityB);
                    return "Inserted into collections in different databases";
                },
                this.transactionOptions,
                this.cancellationToken);
            }
        }

        public void deleteEntitiesB(List<EntityB> entitiesB) 
        {
            using (this.session)
            {
                var result = session.WithTransaction(
                (s, ct) =>
                {
                    /*
                    s: IClientSessionHandle object
                    ct: CancellationToken object
                    */
                    EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(s, ct, this.database);

                    foreach (EntityB entityB in entitiesB)
                    {
                        entityBMongoRepository.delete(entityB);
                    }
                    return "Entities B inserted";
                },
                this.transactionOptions,
                this.cancellationToken);
            }
        }

        public void deleteEntityA(EntityA entityA)
        {
            using (this.session)
            {
                var result = session.WithTransaction(
                (s, ct) =>
                {
                    /*
                    s: IClientSessionHandle object
                    ct: CancellationToken object
                    */
                    EntityAMongoRepository<EntityA> entityAMongoRepository = new EntityAMongoRepository<EntityA>(s, ct, this.database);
                    entityAMongoRepository.delete(entityA);
                    return "Inserted into collections in different databases";
                },
                this.transactionOptions,
                this.cancellationToken);
            }
        }

        public List<EntityB> findEntityBByName(string name) 
        {
            // For this operation is not necessary a transaction
            EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(this.database);
            FindByNameStrategyEntityBMongoRepository<EntityB> findStrategy = new FindByNameStrategyEntityBMongoRepository<EntityB>();
            entityBMongoRepository.setFindStrategy(findStrategy);

            EntityB entityBFilter = new EntityB();
            entityBFilter.Name = name;
            List<EntityB> listEntitiesB = entityBMongoRepository.find(entityBFilter);
            return listEntitiesB;
        }

        public List<EntityA> findEntityAByEntityBName(string entityBName)
        {
            /**
             * Find entitiesA that are associated with entitiesB with a specific name
             */
            EntityB entityB = new EntityB(entityBName);
            EntityA filter = new EntityA();
            filter.EntitiesB.Add(entityB);

            EntityAMongoRepository<EntityA> entityAMongoRepository = new EntityAMongoRepository<EntityA>(this.database);
            FindByEntityBNameStrategyEntityAMongoRepository<EntityA> findStrategy = new FindByEntityBNameStrategyEntityAMongoRepository<EntityA>();
            entityAMongoRepository.setFindStrategy(findStrategy);

            List<EntityA> listEntitiesA = entityAMongoRepository.find(filter);

            return listEntitiesA;
        }

        public List<EntityA> findEntityAByName(string entityAName)
        {
            EntityA filter = new EntityA(entityAName);

            EntityAMongoRepository<EntityA> entityAMongorepository = new EntityAMongoRepository<EntityA>(this.session, this.cancellationToken, this.database);
            FindByNameStrategyEntityAMongoRepository<EntityA> strategy = new FindByNameStrategyEntityAMongoRepository<EntityA>();
            entityAMongorepository.setFindStrategy(strategy);
            List<EntityA> result = entityAMongorepository.find(filter);
            return result;
        }
    }
}









