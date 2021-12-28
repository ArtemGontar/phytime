using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phytime.Models;
using Phytime.Repository;

namespace Phytime.Services
{
    public interface IRecommendationService
    {
      IEnumerable<Recommendation> GetRecommendations();
      Task<Recommendation> GetRecommendationAsync(int id);
    }
    
    public class RecommendationService : IRecommendationService
    {
        private readonly IRepository<Recommendation> _recommendationRepository;

        public RecommendationService(IRepository<Recommendation> recommendationRepository)
        {
            _recommendationRepository = recommendationRepository ?? throw new ArgumentNullException(nameof(recommendationRepository));
        }

        public IEnumerable<Recommendation> GetRecommendations()
        {
            return _recommendationRepository.GetAll();
        }

        public async Task<Recommendation> GetRecommendationAsync(int id)
        {
            return await _recommendationRepository.GetAsync(id);
        }

        public Task AddRecommendationAsync(Recommendation recommendation)
        {
            _recommendationRepository.AddAsync(recommendation);
            return Task.CompletedTask;
        }

        public Task UpdateRecommendationAsync(Recommendation recommendation)
        {
            _recommendationRepository.UpdateAsync(recommendation);
            return Task.CompletedTask;
        }

        public Task RemoveRecommendationAsync(int id)
        {
            _recommendationRepository.DeleteAsync(id);
            return Task.CompletedTask;
        }
    }
}