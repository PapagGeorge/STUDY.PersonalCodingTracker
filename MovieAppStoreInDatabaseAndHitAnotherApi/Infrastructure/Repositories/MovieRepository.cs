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
        public Movie GetMovieByImdbId(int id)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.GetMoviesByImdbId, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ImdbId", id);

                var reader = command.ExecuteReader();

                Movie movie = null;
                if (reader.Read())
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

        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
