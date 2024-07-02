using Domain.Models.AstronomyPictureModel;

namespace Application.Interfaces
{
    public interface IAstronomyPictureService
    {
        public Task<IEnumerable<AstronomyPicture>> GetAstronomyPictures
            (string startDate = null,
            string endDate = null,
            string sortBy = "date",
            bool ascending = true);
    }
}
