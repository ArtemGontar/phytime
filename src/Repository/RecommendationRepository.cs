using System;
using System.Collections.Generic;
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

        public Recommendation Get(int id)
        {
            return _context.Recommendations.Find(id);
        }

        public void Add(Recommendation item)
        {
            _context.Recommendations.Add(item);
            Save();
        }

        public void Update(Recommendation item)
        {
            _context.Recommendations.Update(item);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
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
