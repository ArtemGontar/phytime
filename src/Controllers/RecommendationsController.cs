using Microsoft.AspNetCore.Mvc;
using Phytime.Models;
using Phytime.Services;
using System.Collections.Generic;

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
        private IEnumerable<Recommendation> GetRecommendations()
        {
            return _recommendationService.GetRecommendations();
        }

        [HttpGet("{id:int}")]
        public Recommendation GetRecommendation(int id)
        {
            return _recommendationService.GetRecommendation(id);
        }
    }
}