using Microsoft.VisualStudio.TestTools.UnitTesting;
using template_csharp_mongodb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Globals;
using MongoDB.Driver;
using System.Threading;

namespace template_csharp_mongodb.Persistence.Repositories.Tests
{
    [TestClass()]
    public class EntityBMongoRepositoryTests
    {
        string uri = Globals.Globals.MONGO_URI_REPLICA_SET;
        string databaseName = Globals.Globals.MONGO_DATABASE_NAME;
        [TestMethod()]
        [Ignore()]
        public void createTest()
        {
            // Necessary connection objets
            //string uri = Globals.MONGO_URI_REPLICA_SET;
            //string databaseName = Globals.MONGO_DATABASE_NAME;
            //MongoClient client = new MongoClient(uri);
            //IMongoDatabase database = client.GetDatabase(databaseName);

            //// Test entity
            //EntityB entityB = new EntityB("name1");

            //EntityBMongoRepository<EntityB> entityBMongoRepository = new EntityBMongoRepository<EntityB>(database, client);
            //var result = entityBMongoRepository.create(entityB);
            //Console.WriteLine(result);

        }

        [TestMethod()]
        [Ignore()]
        public void findIdEntityAByNameEntityBTest()
        {

            //MongoClient client = new MongoClient(this.uri);
            //IMongoDatabase database = client.GetDatabase(this.databaseName);

            //IClientSessionHandle session = client.StartSession();
            //TransactionOptions transactionOptions = new TransactionOptions(
            //    readPreference: ReadPreference.Primary,
            //    readConcern: ReadConcern.Local,
            //    writeConcern: WriteConcern.WMajority);

            //CancellationToken cancellationToken = CancellationToken.None; // normally a real token would be used

            //using (session)
            //{
            //    var result = session.WithTransaction(
            //    (s, ct) =>
            //    {
            //        /*
            //        s: IClientSessionHandle object
            //        ct: CancellationToken object
            //        */
            //        EntityAMongoRepository<EntityA> entityAMongoRepository = new EntityAMongoRepository<EntityA>(s, ct, this.database);
            //        entityAMongoRepository.create(entityA);
            //        return "EntityA inserted";
            //    },
            //    transactionOptions,
            //    cancellationToken);
            //}
        }
    }
}

