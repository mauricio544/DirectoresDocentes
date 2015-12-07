using System.Collections.Generic;

namespace DirectoresDocentes.Models.Repositories
{
    internal interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T item);
        void Remove(int id);
        bool Update(int id, T item);
    }
}