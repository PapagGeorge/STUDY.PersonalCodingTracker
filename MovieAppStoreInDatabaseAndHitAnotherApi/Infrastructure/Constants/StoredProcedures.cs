﻿namespace Infrastructure.Constants
{
    public class StoredProcedures
    {
        #region GetMoviesByImdbId
        public const string GetMoviesByImdbId = "spGetMovieById";
        #endregion

        #region GetMoviesByTitle
        public const string GetMoviesByTitle = "spGetMoviesByTitle";
        #endregion

        #region AddNewMovie
        public const string AddNewMovie = "spAddNewMovie";
        #endregion
    }
}
