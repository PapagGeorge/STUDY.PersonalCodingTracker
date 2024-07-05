using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.NewsApiModels;
using Application.Interfaces;
using Domain.Models.WeatherBitApi;
using Domain.Models.AggregateModel;
using Domain.Models.RequestStatistics;

namespace API_Aggregation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAggregationController : ControllerBase
    {
        private readonly IAggregateService _aggregateService;
        private readonly IRequestStatisticsService _requestStatisticsService;

        public ApiAggregationController(IAggregateService aggregateService, IRequestStatisticsService requestStatisticsService)
        {
            _aggregateService = aggregateService;
            _requestStatisticsService = requestStatisticsService;
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
                    return NotFound("No results found.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("request-statistics")]
        public ActionResult<ApiRequestStatistics> GetStatistics()
        {
            try
            {
                var result = _requestStatisticsService.GetRequestStatistics();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
