using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using Phytime.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phytime.Controllers
{
    [ApiController]
    [Route("api/recommendations")]
    public class RecommendationsController : ControllerBase
    {
        private IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }
        
        [HttpGet]
        public IEnumerable<Recommendation> GetRecommendations()
        {
            return _recommendationService.GetRecommendations();
        }

        [HttpGet("{id:int}")]
        public async Task<Recommendation> GetRecommendation(int id)
        {
            return await _recommendationService.GetRecommendationAsync(id);
        }
    }
}