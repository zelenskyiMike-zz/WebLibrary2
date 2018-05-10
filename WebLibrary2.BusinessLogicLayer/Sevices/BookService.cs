using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Concrete;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class BookService
    {
        private readonly GenericRepository<Book> genericRepository;
        private readonly BookRepository bookRepository;
        private readonly BookAuthorRepository bookAuthorRepository;
        private readonly DbContext context;

        public BookService(DbContext context)
        {
            this.context = context;
            genericRepository = new GenericRepository<Book>(context);
            bookRepository = new BookRepository(context);
            bookAuthorRepository = new BookAuthorRepository(context);
        }

        public IEnumerable<GetBookView> GetAllBooks()
        {
            IEnumerable<Book> books = genericRepository.GetAll().ToList();
            var booksMapped = Mapper.Map< IEnumerable< Book>, IEnumerable< GetBookView >> (books);
            return booksMapped;
        }
        public IEnumerable<GetBookGenreView> GetAllGenres()
        {
            var genres = context.Genres.ToList();
            var genresMapped = Mapper.Map<IEnumerable<BookGenre>, IEnumerable<GetBookGenreView>>(genres);
            return genresMapped;
        }
        public GetBookView GetbookByID(int id)
        {
            var book = genericRepository.GetByID(id);
            var bookMapped = Mapper.Map<Book,GetBookView>(book);
            return bookMapped;
        }
        public IEnumerable<GetAllBooksView> GetAllBooksWithGenres()
        {
            var books = bookRepository.GetAllBooksWithGenres();
            var booksMapped = Mapper.Map<IEnumerable<Book>, IEnumerable<GetAllBooksView>>(books);
            return booksMapped;
        }
        public void CreateBook(GetBookView book)
        {
            var bookMapped = Mapper.Map<GetBookView, Book>(book);
            bookRepository.Create(bookMapped);
        }
        public GetAllBooksView GetBookDetails(int id)
        {
            var book = bookRepository.GetByID(id);
            var bookDetails = bookRepository.GetBooksDetails(book);
            var bookMapped = Mapper.Map<Book, GetAllBooksView>(bookDetails);
            return bookMapped;
        }
        public IEnumerable<GetAuthorView> GetAuthorsNotExistInBook(GetAllBooksView book)
        {
            var bookMapped = Mapper.Map<GetAllBooksView,Book>(book);
            var authors = bookRepository.GetAuthorsNotExistInBook(bookMapped);
            var authorsMapped = Mapper.Map<IEnumerable<Author>, IEnumerable<GetAuthorView>> (authors);
            return authorsMapped;
        }
        public void EditBook(GetBookView bookFromView, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            var bookToUpdate = Mapper.Map<GetBookView,Book>(bookFromView);
            genericRepository.Update(bookToUpdate);
            bookAuthorRepository.DeleteAuthorFromBook(bookFromView.BookID, authorIDsForDelete);
            bookAuthorRepository.AddAuthorToBook(bookFromView.BookID, authorIDsForInsert);
            context.SaveChanges();
        }
        public void DeleteBook(GetAllBooksView book)
        {
            var bookMaped = Mapper.Map<GetAllBooksView,Book>(book); 
            genericRepository.Remove(bookMaped);
        }
    }
}
