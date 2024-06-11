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
            var response = await _newsService.GetNewsApiResponse(keyword);
            return Ok(response);
        }
    }
}
