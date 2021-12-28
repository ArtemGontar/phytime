using System;
using System.Collections.Generic;
using Phytime.Models;
using Phytime.Repository;

namespace Phytime.Services
{
    public interface IRecommendationService
    {
      IEnumerable<Recommendation> GetRecommendations();
      Recommendation GetRecommendation(int id);
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

        public Recommendation GetRecommendation(int id)
        {
            return _recommendationRepository.Get(id);
        }
    }
}