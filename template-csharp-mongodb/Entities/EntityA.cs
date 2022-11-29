using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_csharp_mongodb.Entities
{
    public class EntityA
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<EntityB> EntitiesB { get; set; }

        public EntityA(string name, List<EntityB> entitiesB)
        {
            this.Name = name;
            this.EntitiesB = entitiesB;
        }

        public EntityA(List<EntityB> entitiesB)
        {
            this.EntitiesB = entitiesB;
        }

        public EntityA()
        {
            this.EntitiesB = new List<EntityB>();
        }
        public EntityA(string name)
        {
            this.Name = name;
            this.EntitiesB = new List<EntityB>();
        }
    }
}
