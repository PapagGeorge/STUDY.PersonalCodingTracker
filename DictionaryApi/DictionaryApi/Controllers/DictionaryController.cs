using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var definition = await _urbanDictionaryService.GetDefinitionAsync(term);
                return Ok(definition);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
