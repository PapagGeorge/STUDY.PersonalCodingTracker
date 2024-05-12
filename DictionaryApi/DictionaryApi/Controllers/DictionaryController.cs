using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Domain;
using DictionaryApi.DTOs;

namespace DictionaryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IUrbanDictionaryService _urbanDictionaryService;

        public DictionaryController(IUrbanDictionaryService urbanDictionaryService)
        {
            _urbanDictionaryService = urbanDictionaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDefinition(string term)
        {
            try
            {
                var responseBody = await _urbanDictionaryService.GetDefinitionAsync(term);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var definitionList = JsonSerializer.Deserialize<List<DefinitionModel>>(responseBody, options);
                var definitionDtoList = new List<DefinitionDTO>();

                foreach(var item in definitionList)
                {
                    var definitionDto = new DefinitionDTO()
                    {
                        Author = item.Author,
                        Definition = item.Definition,
                        Example = item.Example,
                        ThumbsUp = item.ThumbsUp,
                        ThumbsDown = item.ThumbsDown
                    };
                    
                    definitionDtoList.Add(definitionDto);
                }
                return Ok(definitionDtoList);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
