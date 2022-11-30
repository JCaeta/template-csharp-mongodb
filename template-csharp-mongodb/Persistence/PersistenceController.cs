using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Globals;
using template_csharp_mongodb.Persistence;

namespace template_csharp_mongodb
{
    public class PersistenceController
    {
        private MongoUnitOfWork mongoUnitOfWork;
        private string uri = Globals.Globals.MONGO_URI;
        private string databaseName = Globals.Globals.MONGO_URI_REPLICA_SET;

        public PersistenceController()
        {
            this.mongoUnitOfWork = new MongoUnitOfWork(this.uri, this.databaseName);
        }

        public void createEntityA(EntityA entityA)
        {
            this.mongoUnitOfWork.connect();
            this.mongoUnitOfWork.createEntityA(entityA);
        }

        public void createEntityB(EntityB entityB)
        {
            this.mongoUnitOfWork.connect();
            this.mongoUnitOfWork.createEntityB(entityB);
        }

        public void createEntitiesB(List<EntityB> entitiesB)
        {
            this.mongoUnitOfWork.connect();
            this.mongoUnitOfWork.createEntitiesB(entitiesB);
        }

        public void deleteEntityB(EntityB entityB)
        {
            this.mongoUnitOfWork.connect();
            this.mongoUnitOfWork.deleteEntityB(entityB);
        }

        public void deleteEntitiesB(List<EntityB> entitiesB)
        {
            this.mongoUnitOfWork.connect();
            this.mongoUnitOfWork.deleteEntitiesB(entitiesB);
        }

        public void deleteEntityA(EntityA entityA)
        {
            this.mongoUnitOfWork.connect();
            this.mongoUnitOfWork.deleteEntityA(entityA);
        }

        public List<EntityB> findEntityBByName(string name)
        {
            this.mongoUnitOfWork.connect();
            return this.mongoUnitOfWork.findEntityBByName(name);
        }

        public List<EntityA> findEntityAByEntityBName(string entityBName)
        {
            this.mongoUnitOfWork.connect();
            return this.mongoUnitOfWork.findEntityAByEntityBName(entityBName);
        }

        public List<EntityA> findEntityAByName(string entityAName)
        {
            this.mongoUnitOfWork.connect();
            return this.mongoUnitOfWork.findEntityAByName(entityAName);
        }
    }
}
