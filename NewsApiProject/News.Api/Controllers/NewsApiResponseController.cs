using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces;
using Domain.TechnicalKeywords;
using Domain.DTO;

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

            try
            {
                var response = await _newsService.GetNewsApiResponse(keyword);

                if (response == null)
                {
                    return NotFound("No news found for the given keyword.");
                }

                var articlesDtoList = response.Articles.Select(article => new ArticleDto
                {
                    Author = article.Author,
                    Title = article.Title,
                    Description = article.Description,
                    Url = article.Url,
                    UrlToImage = article.UrlToImage,
                    PublishedAt = article.PublishedAt,
                    Content = article.Content,
                    SourceName = article.SourceName
                }).ToList();

                var responseDto = new NewsApiResponseDto
                {
                    Status = response.Status,
                    TotalResults = response.TotalResults,
                    Articles = articlesDtoList
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }
    
        [HttpGet("valid-keywords")]
        public ActionResult<IEnumerable<string>> GetValidKeywords()
        {
            var validKeywords = TechnicalKeywords.GetAll();
            return Ok(validKeywords);
        }
    }
}
