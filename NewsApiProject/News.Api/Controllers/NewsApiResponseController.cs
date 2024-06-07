using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsApiResponseController : ControllerBase
    {
        private readonly INewsApiResponseRepository _newsApiResponseRepository;

        public NewsApiResponseController(INewsApiResponseRepository newsApiResponseRepository)
        {
            _newsApiResponseRepository = newsApiResponseRepository;
        }
        [HttpGet("{keyword}")]
        public ActionResult<NewsApiResponse> GetNewsByKeyword(string keyword)
        {
            if (keyword.IsNullOrEmpty())
            {
                return BadRequest("Keyword cannot be null or empty");
            }

            var response = _newsApiResponseRepository.GetApiResponse(keyword);
            return Ok(response);
        }
    }
}
