
namespace Infrastructure.Constants
{
    public static class StoredProcedures
    {
        #region CanUserRentMoreBooks
        public const string CanUserRentMoreBooks = "SpCanUserRentMoreBooks";
        #endregion

        #region DeleteUser
        public const string DeleteUser = "SpDeleteUser";
        #endregion

        #region InsertUser
        public const string InsertUser = "SpInsertUser";
        #endregion

        #region SearchUserById
        public const string SearchUserById = "SpSearchUserById";
        #endregion

        #region SearchUserByMobilePhone
        public const string SearchUserByMobilePhone = "SpSearchUserByMobilePhone";
        #endregion

        #region
        public const string IncreaseBookInventory = "SpIncreaseBookInventory";
        #endregion

        #region
        public const string DecreaseBookInventory = "SpDecreaseBookInventory";
        #endregion

        #region
        public const string InsertNewBook = "SpInsertNewBook";
        #endregion
        
        #region
        public const string CheckIfBookExists = "SpCheckIfBookExists";
        #endregion
    }
}
