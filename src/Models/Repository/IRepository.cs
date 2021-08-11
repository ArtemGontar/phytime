using Microsoft.Extensions.Hosting;
using System;

namespace Phytime.Models
{
    public interface IRepository<T, P> : IDisposable
    {
        T Get(int id);
        void Add(T item);
        void Update(T item);
        T GetInclude(T item);
        T GetBy(string parameter);
        bool ContainsItem(T contains, P item);
        void AddItemToContains(T contains, P item);
        void RemoveItemFromContains(T contains, P item);
        P GetContainedItemBy(string parameter);
        void Save();
    }
}
