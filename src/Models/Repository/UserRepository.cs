using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Phytime.Models
{
    public class UserRepository : IRepository<User>
    {
        private PhytimeContext _db;
        private const string ConnectionString = "Server=localhost;Database=phytime2021db;User Id=SA;Password=TUTdm83b7L1";

        public UserRepository()
        {
            var options = new DbContextOptionsBuilder<PhytimeContext>();
            options.UseSqlServer(ConnectionString);
            _db = new PhytimeContext(options.Options);
        }

        public UserRepository(PhytimeContext context)
        {
            _db = context;
        }

        public void Add(User item)
        {
            _db.Users.Add(item);
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

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
