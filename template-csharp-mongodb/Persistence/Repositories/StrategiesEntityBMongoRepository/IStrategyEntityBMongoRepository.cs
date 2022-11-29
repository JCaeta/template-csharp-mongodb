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

namespace template_csharp_mongodb.Persistence.Repositories.StrategiesEntityBMongoRepository
{

    public interface IStrategyEntityBMongoRepository<EntityB>
    where EntityB: template_csharp_mongodb.Entities.EntityB
    {
        IMongoCollection<MongoEntityB> Collection { get; set; }
        IMongoDatabase Database { get; set; }
        IClientSessionHandle SessionHandle { get; set; }
        CancellationToken CancellationToken { get; set; }

        ObjectId IdEntityA { get; set; }

        List<EntityB> find(EntityB item);

    }
}
