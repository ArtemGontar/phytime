using System;

namespace Phytime.Models
{
    public interface IRepository<T> : IDisposable
    {
        T Get(int id);
        T GetBy(string parameter);
        void Add(T item);
        void Update(T item);
        T GetInclude(T item);
        void Save();
    }
}
