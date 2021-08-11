using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Phytime.Models
{
    public class UserRepository : IRepository<User, Feed>
    {
        private PhytimeContext _db;
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=phytime2021db;Trusted_Connection=True;";

        public UserRepository()
        {
            var options = new DbContextOptionsBuilder<PhytimeContext>();
            options.UseSqlServer(ConnectionString);
            _db = new PhytimeContext(options.Options);
        }

        public void Add(User item)
        {
            _db.Users.Add(item);
            Save();
        }

        public void AddItemToContains(User contains, Feed item)
        {
            contains.Feeds.Add(item);
            Save();
        }

        public bool ContainsItem(User contains, Feed item)
        {
            return _db.Users.Include(c => c.Feeds).ToList().
                FirstOrDefault(user => user.Id == contains.Id)
                .Feeds.Select(f => f.Id).Contains(item.Id);
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

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public User GetBy(string email)
        {
            return _db.Users.FirstOrDefault(user => user.Email == email);
        }

        public User GetInclude(User item)
        {
            return _db.Users.Include(u => u.Feeds).FirstOrDefault(u => u.Id == item.Id);
        }

        public void RemoveItemFromContains(User contains, Feed item)
        {
            var dbFeed = _db.Feeds.Include(c => c.Users).ToList()
                .FirstOrDefault(feed => feed.Id == item.Id);
            User dbUser = _db.Users.FirstOrDefault(user => user.Id == contains.Id);
            dbFeed.Users.Remove(dbUser);
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public async Task<User> GetUserAsync(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Feed GetContainedItemBy(string url)
        {
            return _db.Feeds.FirstOrDefault(feed => feed.Url == url);
        }
    }
}
