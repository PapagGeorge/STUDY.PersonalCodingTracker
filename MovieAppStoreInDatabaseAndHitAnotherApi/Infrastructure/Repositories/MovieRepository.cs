using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Constants;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace Infrastructure.Repositories
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(DatabaseConfiguration dbConfiguration) : base(dbConfiguration)
        {
            
        }
        public async Task <Movie> GetMovieByIdAsync(string movieId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.GetMoviesByImdbId, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ImdbId", movieId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    Movie movie = null;
                    if (await reader.ReadAsync())
                    {
                        movie = new Movie
                        {
                            Title = reader["Title"].ToString(),
                            Year = Convert.ToInt32(reader["Year"]),
                            Rated = reader["Rated"].ToString(),
                            Released = reader["Released"].ToString(),
                            Runtime = reader["Runtime"].ToString(),
                            Genre = reader["Genre"].ToString(),
                            Director = reader["Director"].ToString(),
                            Writer = reader["Writer"].ToString(),
                            Actors = reader["Actors"].ToString(),
                            Plot = reader["Plot"].ToString(),
                            Language = reader["Language"].ToString(),
                            Country = reader["Country"].ToString(),
                            Awards = reader["Awards"].ToString(),
                            Poster = reader["Poster"].ToString(),
                            Metascore = Convert.ToInt32(reader["Metascore"]),
                            ImdbRating = Convert.ToDouble(reader["ImdbRating"]),
                            ImdbVotes = Convert.ToInt32(reader["ImdbVotes"]),
                            ImdbID = reader["ImdbID"].ToString(),
                            Type = reader["Type"].ToString(),
                            DVD = reader["DVD"].ToString(),
                            BoxOffice = reader["BoxOffice"].ToString(),
                            Production = reader["Production"].ToString(),
                            Website = reader["Website"].ToString(),
                            Response = Convert.ToBoolean(reader["Response"])
                        };
                    }
                    return movie;
                }

                    
            }
        }

        public async Task AddNewMovieAsync(Movie movie)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.AddNewMovie, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Year", (Convert.ToInt32(movie.Year)));
                command.Parameters.AddWithValue("@Rated", movie.Rated);
                command.Parameters.AddWithValue("@Released", movie.Released);
                command.Parameters.AddWithValue("@Runtime", movie.Runtime);
                command.Parameters.AddWithValue("@Genre", movie.Genre);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@Writer", movie.Writer);
                command.Parameters.AddWithValue("@Actors", movie.Actors);
                command.Parameters.AddWithValue("@Plot", movie.Plot);
                command.Parameters.AddWithValue("@Language", movie.Language);
                command.Parameters.AddWithValue("@Country", movie.Country);
                command.Parameters.AddWithValue("@Awards", movie.Awards);
                command.Parameters.AddWithValue("@Poster", movie.Poster);
                command.Parameters.AddWithValue("@Metascore", (Convert.ToInt32(movie.Metascore)));
                command.Parameters.AddWithValue("@ImdbRating", (Convert.ToDouble(movie.ImdbRating)));
                command.Parameters.AddWithValue("@ImdbVotes", (Convert.ToInt32(movie.ImdbVotes)));
                command.Parameters.AddWithValue("@ImdbID", movie.ImdbID);
                command.Parameters.AddWithValue("@Type", movie.Type);
                command.Parameters.AddWithValue("@DVD", movie.DVD);
                command.Parameters.AddWithValue("@BoxOffice", movie.BoxOffice);
                command.Parameters.AddWithValue("@Production", movie.Production);
                command.Parameters.AddWithValue("@Website", movie.Website);
                command.Parameters.AddWithValue("@Response", (Convert.ToBoolean(movie.Response)));

                await command.ExecuteNonQueryAsync();
            }
        }

        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
