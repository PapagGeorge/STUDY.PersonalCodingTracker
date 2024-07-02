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

        //[HttpGet]
        //public async Task<ActionResult<NewsApiResponse>> GetNews(string keyword)
        //{
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        return BadRequest("Keyword cannot be null or empty");
        //    }

        //    try
        //    {
        //        var response = await _newsService.GetNewsApiResponseAsync(keyword);

        //        if (response == null)
        //        {
        //            return NotFound("No news found for the given keyword.");
        //        }

        //        return Ok(response);
        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet]
        //public async Task<ActionResult<WeatherData>> GetWeather(string countryCode, string cityName)
        //{
        //    if (string.IsNullOrEmpty(countryCode) && string.IsNullOrEmpty(cityName))
        //    {
        //        return BadRequest("Keyword cannot be null or empty");
        //    }

        //    try
        //    {
        //        var response = await _weatherService.GetWeatherApiResponseAsync(countryCode, cityName);

        //        if (response == null)
        //        {
        //            return NotFound("No news found for the given keyword.");
        //        }

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult<AggregateModel>> GetAggregate(string keyword, string countryCode, string cityName)
        {
            if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(countryCode) && string.IsNullOrEmpty(cityName))
            {
                return BadRequest("Keyword cannot be null or empty");
            }

            try
            {
                var response = await _aggregateService.GetAggregateData(keyword, countryCode, cityName);

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
