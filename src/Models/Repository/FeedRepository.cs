using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Phytime.Models
{
  public class FeedRepository : IRepository<Feed>
    {
        private PhytimeContext _context;

        public FeedRepository(PhytimeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Feed item)
        {
            _context.Feeds.Add(item);
            Save();
        }

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

        public Feed Get(int id)
        {
            return _context.Feeds.Find(id);
        }

        public Feed GetInclude(Feed item)
        {
            return _context.Feeds.Include(f => f.Users).FirstOrDefault(f => f.Id == item.Id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Feed item)
        {
            _context.Feeds.Update(item);
            Save();
        }

        public Feed GetBy(string url)
        {
            return _context.Feeds.FirstOrDefault(feed => feed.Url == url);
        }
    }
}
