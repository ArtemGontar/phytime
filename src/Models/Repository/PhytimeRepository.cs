using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phytime.Models
{
    public class PhytimeRepository : IRepository
    {
        private PhytimeContext _db;

        public PhytimeRepository() { }

        public PhytimeRepository(IConfiguration config)
        {
            string connection = config.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<PhytimeContext>();
            options.UseSqlServer(connection);
            _db = new PhytimeContext(options.Options);
        }

        public void AddUserToFeed(Feed feedValue, User userValue)
        {
            feedValue.Users.Add(userValue);
            Save();
        }

        public void RemoveUserFromFeed(Feed feedValue, User userValue)
        {
            var dbuser = GetUserListInclude().FirstOrDefault(user => user.Id == userValue.Id);
            Feed dbFeed = _db.Feeds.FirstOrDefault(feed => feed.Id == feedValue.Id);
            dbuser.Feeds.Remove(dbFeed);
            Save();
        }

        public Feed GetFeedByUrl(string url)
        {
            return _db.Feeds.FirstOrDefault(feed => feed.Url == url);
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(user => user.Email == email);
        }

        public IEnumerable<Feed> GetFeedList()
        {
            return _db.Feeds.ToList();
        }

        public User GetUserIncudeFeeds(User userValue)
        {
            return _db.Users.Include(f => f.Feeds).FirstOrDefault(f => f.Id == userValue.Id);
        }

        public Feed GetFeedIncudeUsers(Feed feedValue)
        {
            return _db.Feeds.Include(f => f.Users).FirstOrDefault(f => f.Id == feedValue.Id);
        }

        public IEnumerable<Feed> GetFeedListInclude()
        {
            return _db.Feeds.Include(c => c.Users).ToList();
        }

        public IEnumerable<User> GetUserListInclude()
        {
            return _db.Users.Include(c => c.Feeds).ToList();
        }

        public bool FeedContainsUser(Feed feedValue, User userValue)
        {
            return GetFeedListInclude().FirstOrDefault(feed => feed.Id == feedValue.Id)
                .Users.Select(u => u.Id).Contains(userValue.Id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Add(User item)
        {
            _db.Add(item);
            Save();
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Add(Feed item)
        {
            _db.Add(item);
            Save();
        }

        public void Update(Feed item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public Feed GetFeed(int id)
        {
            return _db.Feeds.Find(id);
        }

        public User GetUser(int id)
        {
            return _db.Users.Find(id);
        }

        public async Task<User> GetUserAsync(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
