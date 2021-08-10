using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phytime.Models
{
    public class FeedRepository : IRepository<Feed, User>
    {
        private PhytimeContext _db;

        public FeedRepository() { }

        public FeedRepository(IConfiguration config)
        {
            string connection = config.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<PhytimeContext>();
            options.UseSqlServer(connection);
            _db = new PhytimeContext(options.Options);
        }

        public void Add(Feed item)
        {
            _db.Add(item);
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
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public Feed Get(int id)
        {
            return _db.Feeds.Find(id);
        }

        public Feed GetInclude(Feed item)
        {
            return _db.Feeds.Include(f => f.Users).FirstOrDefault(f => f.Id == item.Id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Feed item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public Feed GetBy(string url)
        {
            return _db.Feeds.FirstOrDefault(feed => feed.Url == url);
        }
    }
}
