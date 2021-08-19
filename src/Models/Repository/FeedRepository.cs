using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phytime.Models
{
    public class FeedRepository : IRepository<Feed>
    {
        private PhytimeContext _db;
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=phytime2021db;Trusted_Connection=True;";

        public FeedRepository()
        {
            var options = new DbContextOptionsBuilder<PhytimeContext>();
            options.UseSqlServer(ConnectionString);
            _db = new PhytimeContext(options.Options);
        }

        public FeedRepository(PhytimeContext context)
        {
            _db = context;
        }

        public void Add(Feed item)
        {
            _db.Feeds.Add(item);
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
            _db.Feeds.Update(item);
            Save();
        }

        public Feed GetBy(string url)
        {
            return _db.Feeds.FirstOrDefault(feed => feed.Url == url);
        }
    }
}
