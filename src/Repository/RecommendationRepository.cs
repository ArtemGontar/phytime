using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phytime.Models;

namespace Phytime.Repository
{
  public class RecommendationRepository : IRepository<Recommendation>
    {
        private PhytimeContext _context;

        public RecommendationRepository(PhytimeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IEnumerable<Recommendation> GetAll()
        {
            return _context.Recommendations;
        }

        public async Task<Recommendation> GetAsync(int id)
        {
            return await _context.Recommendations.FindAsync(id);
        }

        public async Task AddAsync(Recommendation item)
        {
            await _context.Recommendations.AddAsync(item);
            SaveAsync();
        }

        public async Task UpdateAsync(Recommendation item)
        {
            _context.Recommendations.Update(item);
            SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Recommendations.FindAsync(id);
            _context.Recommendations.Remove(item);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Dispose
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
