using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;
using Phytime.Models.Feed;

namespace Phytime.Repository
{
    public interface IFeedRepository : IRepository<Feed>
    {
        Feed GetInclude(Feed item);
        Feed GetBy(string url);
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
        public Feed Get(int id)
        {
            return _context.Feeds.Find(id);
        }

        public Feed GetInclude(Feed item)
        {
            return _context.Feeds.Include(f => f.Users).FirstOrDefault(f => f.Id == item.Id);
        }
        public Feed GetBy(string url)
        {
            return _context.Feeds.FirstOrDefault(feed => feed.Url == url);
        }

        public void Add(Feed item)
        {
            _context.Feeds.Add(item);
            Save();
        }

        public void Update(Feed item)
        {
            _context.Feeds.Update(item);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
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
