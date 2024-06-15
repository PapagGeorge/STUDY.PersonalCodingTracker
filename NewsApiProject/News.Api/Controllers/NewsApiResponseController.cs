using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces;
using Infrastructure.Constants;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsApiResponseController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsApiResponseController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{keyword}")]
        public async Task<ActionResult<NewsApiResponse>> GetNewsByKeyword(string keyword)
        {
            if (keyword.IsNullOrEmpty())
            {
                return BadRequest("Keyword cannot be null or empty");
            }

            if (!TechnicalKeywords.IsValid(keyword))
            {
                return BadRequest("Invalid keyword. Please provide a valid technical keyword.");
            }

            var response = await _newsService.GetNewsApiResponse(keyword);
            return Ok(response);
        }

        [HttpGet("valid-keywords")]
        public ActionResult<IEnumerable<string>> GetValidKeywords()
        {
            var validKeywords = TechnicalKeywords.GetAll();
            return Ok(validKeywords);
        }
    }
}
