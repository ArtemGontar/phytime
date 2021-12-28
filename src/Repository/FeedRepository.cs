using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;
using Phytime.Models.Feed;

namespace Phytime.Repository
{
    public interface IFeedRepository : IRepository<Feed>
    {
        Task<Feed> GetIncludeAsync(Feed item);
        Task<Feed> GetByAsync(string url);
    }

    public class FeedRepository : IFeedRepository
    {
        private PhytimeContext _context;

        public FeedRepository(PhytimeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Feed> GetAll()
        {
            return _context.Feeds;
        }
        public async Task<Feed> GetAsync(int id)
        {
            return await _context.Feeds.FindAsync(id);
        }

        public async Task<Feed> GetIncludeAsync(Feed item)
        {
            return await _context.Feeds.Include(f => f.Users).FirstOrDefaultAsync(f => f.Id == item.Id);
        }

        public async Task<Feed> GetByAsync(string url)
        {
            return await _context.Feeds.FirstOrDefaultAsync(feed => feed.Url == url);
        }

        public async Task AddAsync(Feed item)
        {
            await _context.Feeds.AddAsync(item);
            SaveAsync();
        }

        public async Task UpdateAsync(Feed item)
        {
            _context.Feeds.Update(item);
            SaveAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Feeds.FindAsync(id);
            _context.Feeds.Remove(item);
            SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Disposable
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        #endregion
    }
}
