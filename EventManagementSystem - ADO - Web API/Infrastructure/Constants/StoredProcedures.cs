namespace Infrastructure.Constants
{
    public class StoredProcedures
    {
        #region GetAll
        public const string GetAll = "spGetAll";
        #endregion

        #region SoftDelete
        public const string SoftDelete = "spSoftDelete";
        #endregion

        #region GetById
        public const string GetById = "spGetById";
        #endregion

        #region InsertEntity
        public const string InsertEntity = "spInsertEntity";
        #endregion

        #region InsertUser
        public const string InsertUser = "spAddNewUser";
        #endregion

        #region BulkInsertUsers
        public const string BulkInsertUsers = "spBulkInsertUsers";
        #endregion

        #region BulkInsertRegistrations
        public const string BulkInsertRegistrations = "spBulkInsertRegistrations";
        #endregion
    }
}
