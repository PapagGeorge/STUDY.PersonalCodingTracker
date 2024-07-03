using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.NewsApiModels;
using Application.Interfaces;
using Domain.Models.WeatherBitApi;
using Domain.Models.AggregateModel;

namespace API_Aggregation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAggregationController : ControllerBase
    {
        private readonly IAggregateService _aggregateService;

        public ApiAggregationController(IAggregateService aggregateService)
        {
            _aggregateService = aggregateService;
        }

        

        [HttpGet]
        public async Task<ActionResult<AggregateModel>> GetAggregate(string newsKeyword, string startDateAstronomyPicture = null,
            string endDateAstronomyPicture = null, string sortByAstronomyPicture = "date", bool ascendingAstronomyPicture = true, bool ascendingNews = true,
            string sortByTemperature = "temperature", bool ascendingTemperature = true, string sortByNews = null)
        {
            if (string.IsNullOrEmpty(newsKeyword))
            {
                return BadRequest("News Keyword cannot be null or empty");
            }

            try
            {
                var response = await _aggregateService.GetAggregateData(newsKeyword, ascendingTemperature, sortByTemperature,
                    startDateAstronomyPicture, endDateAstronomyPicture, sortByAstronomyPicture,
                    ascendingAstronomyPicture, sortByNews, ascendingNews);

                if (response == null)
                {
                    return NotFound("No news found for the given keyword.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
