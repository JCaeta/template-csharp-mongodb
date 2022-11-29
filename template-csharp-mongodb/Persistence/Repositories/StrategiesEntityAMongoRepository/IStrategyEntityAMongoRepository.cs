using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using template_csharp_mongodb.Entities;
using template_csharp_mongodb.Persistence.Entities;

namespace template_csharp_mongodb.Persistence.Repositories.StrategiesEntityAMongoRepository
{
    public interface IStrategyEntityAMongoRepository<EntityA>
    where EntityA : template_csharp_mongodb.Entities.EntityA
    {
        IMongoDatabase Database { get; set; }
        IMongoCollection<MongoEntityA> Collection { get; set; }
        IClientSessionHandle SessionHandle { get; set; }
        CancellationToken CancellationToken { get; set; }

        List<EntityA> find(EntityA item);
        //List<EntityB> findEntitiesBByEntityAId(Object Id);
    }
}
