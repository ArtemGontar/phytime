using System;
using System.Collections.Generic;

namespace Phytime.Repository
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Save();
    }
}
