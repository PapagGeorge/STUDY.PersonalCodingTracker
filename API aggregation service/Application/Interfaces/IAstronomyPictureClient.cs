using Domain.Models.AstronomyPictureModel;

namespace Application.Interfaces
{
    public interface IAstronomyPictureClient
    {
        Task<IEnumerable<AstronomyPicture>> GetAstronomyPicturesAsync(string startDate = null, string endDate = null);
    }
}
