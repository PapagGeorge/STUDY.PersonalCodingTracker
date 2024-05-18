namespace Application.Interfaces
{
    public interface IOMDbToDatabase
    {
        Task AddMovieFromIOMDbToDatabase(string imdbId);
    }
}
