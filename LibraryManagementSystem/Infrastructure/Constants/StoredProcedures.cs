
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

        #region RemoveUserRentability
        public const string RemoveUserRentability = "SpRemoveUserRentability";
        #endregion

        #region RentBookToUser
        public const string RentBookToUser = "SpRentBookToUser";
        #endregion

        #region RestoreUserRentability
        public const string RestoreUserRentability = "SpRestoreUserRentability";
        #endregion

        #region ReturnBookFromUser
        public const string ReturnBookFromUser = "SpReturnBookFromUser";
        #endregion

        #region NumberOfCopiesInStock
        public const string NumberOfCopiesInStock = "SpNumberOfCopiesInStock";
        #endregion

        #region SearchById
        public const string SearchById = "SpSearchById";
        #endregion

        #region SearchBookByIsbn
        public const string SearchBookByIsbn = "SpSearchBookByIsbn";
        #endregion

        #region SearchBookByTitle
        public const string SearchBookByTitle = "SpSearchBookByTitle";
        #endregion

        #region ShowBooksForRental
        public const string ShowBooksForRental = "SpShowBooksForRental";
        #endregion
        
        #region ShowRentedBooks
        public const string ShowRentedBooks = "SpShowRentedBooks";
        #endregion

        #region ShowNotRentedBooks
        public const string ShowNotRentedBooks = "SpShowNotRentedBooks";
        #endregion
        
        #region NumberOfBooksRentedByUser
        public const string NumberOfBooksRentedByUser = "SpNumberOfBooksRentedByUser";
        #endregion

        #region ShowAllBooks
        public const string ShowAllBooks = "SpShowAllBooks";
        #endregion

    }
}
