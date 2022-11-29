using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_csharp_mongodb.Persistence
{
    public interface IRepository<T>
    {
        T create(T item);
        T update(T item);
        void delete(T item);
        List<T> find(T item);
        T findOne(T item);
    }
}
