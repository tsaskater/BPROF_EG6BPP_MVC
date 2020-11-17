using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{

    public interface IRepository<T> where T : new()
    {
        void Add(T item);

        void Delete(T item);

        void Delete(string id);

        T Read(string id);

        IQueryable<T> Read();

        void Update(string oid, T NItem);

        void SaveAll();
    }
}
