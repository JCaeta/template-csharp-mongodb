using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_csharp_mongodb.Entities
{
    public class EntityB
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public EntityB(string name)
        {
            this.Name = name;
        }
        public EntityB(){}
    }
}
