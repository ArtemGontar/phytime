using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;

namespace Phytime.Repository
{
    public class UserRepository : IRepository<User>
    {
        private PhytimeContext _context;

        public UserRepository(PhytimeContext context)
        {
            _context = context;
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
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

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetBy(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public User GetInclude(User item)
        {
            return _context.Users.Include(u => u.Feeds).FirstOrDefault(u => u.Id == item.Id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
