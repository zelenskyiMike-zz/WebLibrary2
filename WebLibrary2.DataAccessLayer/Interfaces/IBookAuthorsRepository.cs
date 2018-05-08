namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IBookAuthorsRepository
    {
        void DeleteBookFromAuthor(int authorID, int [] bookIDsForDelete);
        void DeleteAuthorFromBook(int bookID, int[] authorIDsForDelete);
        void AddAuthorToBook(int bookID, int [] authorIDsForInsert);
        void AddBookToAuthor(int authorID, int[] bookIDsForInsert);
    }
}
