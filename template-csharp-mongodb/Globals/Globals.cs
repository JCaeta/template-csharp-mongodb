using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_csharp_mongodb.Globals
{
    public static class Globals
    {
        public const string MONGO_URI = "mongodb://localhost:27017";
        public const string MONGO_URI_REPLICA_SET = "mongodb://localhost:27017/?readPreference=primary&replicaSet=myReplicaSet0";
        public const string MONGO_DATABASE_NAME = "CSharpDatabase";
    }
}
