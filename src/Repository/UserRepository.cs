using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phytime.Models;

namespace Phytime.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetIncludeAsync(User item);
    }
    public class UserRepository : IUserRepository
    {
        private PhytimeContext _context;

        public UserRepository(PhytimeContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetIncludeAsync(User item)
        {
            return await _context.Users.Include(u => u.Feeds).FirstOrDefaultAsync(u => u.Id == item.Id);
        }

        public async Task AddAsync(User item)
        {
            await _context.Users.AddAsync(item);
            SaveAsync();
        }

        public async Task UpdateAsync(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Users.FindAsync(id);
            _context.Users.Remove(item);
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
