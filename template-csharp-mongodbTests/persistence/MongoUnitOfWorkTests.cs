using Microsoft.VisualStudio.TestTools.UnitTesting;
using template_csharp_mongodb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template_csharp_mongodb.Entities;

namespace template_csharp_mongodb.Persistence.Tests
{
    [TestClass()]
    public class MongoUnitOfWorkTests
    {
        string uri = Globals.Globals.MONGO_URI_REPLICA_SET;
        string databaseName = Globals.Globals.MONGO_DATABASE_NAME;

        [TestMethod()]
        [Ignore()]
        public void createEntityATest()
        {
            EntityB entityB0 = new EntityB("test entityB0");
            EntityB entityB1 = new EntityB("test entityB1");

            List<EntityB> listEntitiesB = new List<EntityB>();
            listEntitiesB.Add(entityB0);
            listEntitiesB.Add(entityB1);

            EntityA entityA = new EntityA("test entityA", listEntitiesB);

            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
            mongoUnitOfWork.connect();
            mongoUnitOfWork.createEntityA(entityA);

        }

        [TestMethod()]
        [Ignore()]
        public void createEntityBTest()
        {
            EntityB entityB = new EntityB("test name 1");
            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
            mongoUnitOfWork.connect();
            mongoUnitOfWork.createEntityB(entityB);
        }

        [TestMethod()]
        public void deleteEntityBTest()
        {
            // Create an entitiy to delete
            EntityB entityB = new EntityB("entityB to delete");
            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
            mongoUnitOfWork.connect();
            mongoUnitOfWork.createEntityB(entityB);

            // Delete entity
            mongoUnitOfWork.connect();
            mongoUnitOfWork.deleteEntityB(entityB);
        }

        [TestMethod()]
        public void deleteEntityATest()
        {
            // Create entityA to delete
            EntityB entityB0 = new EntityB("test entityB0");
            EntityB entityB1 = new EntityB("test entityB1");

            List<EntityB> listEntitiesB = new List<EntityB>();
            listEntitiesB.Add(entityB0);
            listEntitiesB.Add(entityB1);

            EntityA entityA = new EntityA("test entityA", listEntitiesB);

            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
            mongoUnitOfWork.connect();
            mongoUnitOfWork.createEntityA(entityA);

            // Delete EntityA
            mongoUnitOfWork.connect();
            mongoUnitOfWork.deleteEntityA(entityA);
        }

        [TestMethod()]
        public void createEntitiesBTest()
        {
            EntityB entityB0 = new EntityB("name1");
            EntityB entityB1 = new EntityB("name1");
            EntityB entityB2 = new EntityB("name2");
            EntityB entityB3 = new EntityB("name3");
            EntityB entityB4 = new EntityB("name1");
            EntityB entityB5 = new EntityB("name4");

            List<EntityB> listEntitiesB = new List<EntityB>();
            listEntitiesB.Add(entityB0);
            listEntitiesB.Add(entityB1);
            listEntitiesB.Add(entityB2);
            listEntitiesB.Add(entityB3);
            listEntitiesB.Add(entityB4);
            listEntitiesB.Add(entityB5);

            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
            mongoUnitOfWork.connect();
            mongoUnitOfWork.createEntitiesB(listEntitiesB);

            mongoUnitOfWork.connect();
            mongoUnitOfWork.deleteEntitiesB(listEntitiesB);
        }

        [TestMethod()]
        [Ignore()]
        public void findEntityBByNameTest()
        {

            EntityB entityB0 = new EntityB("name1");
            EntityB entityB1 = new EntityB("name1");
            EntityB entityB2 = new EntityB("name2");
            EntityB entityB3 = new EntityB("name3");
            EntityB entityB4 = new EntityB("name1");
            EntityB entityB5 = new EntityB("name4");

            List<EntityB> listEntitiesB = new List<EntityB>();
            listEntitiesB.Add(entityB0);
            listEntitiesB.Add(entityB1);
            listEntitiesB.Add(entityB2);
            listEntitiesB.Add(entityB3);
            listEntitiesB.Add(entityB4);
            listEntitiesB.Add(entityB5);

            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
            mongoUnitOfWork.connect();
            mongoUnitOfWork.createEntitiesB(listEntitiesB);

            mongoUnitOfWork.connect();
            List<EntityB> listResult = mongoUnitOfWork.findEntityBByName("name1");
        }

        [TestMethod()]
        [Ignore()]
        public void findEntityAByEntityBNameTest()
        {
            // Create test entities
            EntityB entityB0 = new EntityB("name1");
            EntityB entityB1 = new EntityB("name1");
            EntityB entityB2 = new EntityB("name2");
            EntityB entityB3 = new EntityB("name3");
            EntityB entityB4 = new EntityB("name1");
            EntityB entityB5 = new EntityB("name4");
            EntityB entityB6 = new EntityB("name1");

            List<EntityB> listEntitiesB0 = new List<EntityB>();
            listEntitiesB0.Add(entityB0);
            listEntitiesB0.Add(entityB1);
            listEntitiesB0.Add(entityB2);
            EntityA entityA0 = new EntityA("entityA0", listEntitiesB0);

            List<EntityB> listEntitiesB1 = new List<EntityB>();
            listEntitiesB1.Add(entityB3);
            listEntitiesB1.Add(entityB4);
            EntityA entityA1 = new EntityA("entityA1", listEntitiesB1);

            List<EntityB> listEntitiesB2 = new List<EntityB>();
            listEntitiesB2.Add(entityB5);
            listEntitiesB2.Add(entityB6);
            EntityA entityA2 = new EntityA("entityA2", listEntitiesB2);

            List<EntityA> entitiesA = new List<EntityA>();
            entitiesA.Add(entityA0);
            entitiesA.Add(entityA1);
            entitiesA.Add(entityA2);

            // Find by entityB name
            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);

            foreach (EntityA entityA in entitiesA)
            {
                mongoUnitOfWork.connect();
                mongoUnitOfWork.createEntityA(entityA);
            }

            mongoUnitOfWork.connect();
            List<EntityA> listResult = mongoUnitOfWork.findEntityAByEntityBName("name1");
        }

        [TestMethod()]
        public void findEntityAByNameTest()
        {
            // Create test entities
            EntityB entityB0 = new EntityB("name1");
            EntityB entityB1 = new EntityB("name1");
            EntityB entityB2 = new EntityB("name2");
            EntityB entityB3 = new EntityB("name3");
            EntityB entityB4 = new EntityB("name1");
            EntityB entityB5 = new EntityB("name4");
            EntityB entityB6 = new EntityB("name1");

            List<EntityB> listEntitiesB0 = new List<EntityB>();
            listEntitiesB0.Add(entityB0);
            listEntitiesB0.Add(entityB1);
            listEntitiesB0.Add(entityB2);
            EntityA entityA0 = new EntityA("entityA0", listEntitiesB0);

            List<EntityB> listEntitiesB1 = new List<EntityB>();
            listEntitiesB1.Add(entityB3);
            listEntitiesB1.Add(entityB4);
            EntityA entityA1 = new EntityA("entityA1", listEntitiesB1);

            List<EntityB> listEntitiesB2 = new List<EntityB>();
            listEntitiesB2.Add(entityB5);
            listEntitiesB2.Add(entityB6);
            EntityA entityA2 = new EntityA("entityA2", listEntitiesB2);

            List<EntityA> entitiesA = new List<EntityA>();
            entitiesA.Add(entityA0);
            entitiesA.Add(entityA1);
            entitiesA.Add(entityA2);

            // Find by entityB name
            MongoUnitOfWork mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);

            foreach (EntityA entityA in entitiesA)
            {
                mongoUnitOfWork.connect();
                mongoUnitOfWork.createEntityA(entityA);
            }

            mongoUnitOfWork.connect();
            List<EntityA> listResult = mongoUnitOfWork.findEntityAByName("entityA1");

        }
    }
}