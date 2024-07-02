using Application.Interfaces;
using Domain.Models.AstronomyPictureModel;

namespace Application.Services
{
    public class AstronomyPictureService : IAstronomyPictureService
    {
        private readonly IAstronomyPictureClient _astronomyPictureClient;

        public AstronomyPictureService(IAstronomyPictureClient astronomyPictureClient)
        {
            _astronomyPictureClient = astronomyPictureClient;
        }
        public async Task<IEnumerable<AstronomyPicture>> GetAstronomyPictures(string startDate = null, string endDate = null, string sortBy = "date", bool ascending = true)
        {
            var content = await _astronomyPictureClient.GetAstronomyPicturesAsync(startDate, endDate);

            if(sortBy == "date")
            {
                content = ascending ? content.OrderBy(item => item.Date) : content.OrderByDescending(item => item.Date);
            }

            return content;
        }
    }
}
