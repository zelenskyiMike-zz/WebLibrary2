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
    public class AuthorService
    {
        private readonly GenericRepository<Author> genericRepository;
        private readonly DbContext context;
        private readonly BookAuthorRepository bookAuthorRepository;
        private readonly ArticleAuthorsRepository articleAuthorsRepository;
        private readonly MagazineAuthorRepository magazineAuthorRepository;
        private readonly PublicationAuthorsRepository publicationAuthorsRepository;
        private readonly AuthorRepository authorRepository;

        public AuthorService(DbContext context, BookAuthorRepository bookAuthorRepository)
        {
            this.context = context;
            genericRepository = new GenericRepository<Author>(context);
            bookAuthorRepository = new BookAuthorRepository(context) ;
            articleAuthorsRepository = new ArticleAuthorsRepository(context);
            magazineAuthorRepository = new MagazineAuthorRepository(context);
            publicationAuthorsRepository = new PublicationAuthorsRepository(context);
            authorRepository = new AuthorRepository(context);
        }

        public IEnumerable<GetAuthorView> GetAllAuthors()
        {
            var authors = genericRepository.GetAll().ToList();
            return Mapper.Map<IEnumerable<Author>, IEnumerable<GetAuthorView>>(authors);
        }
        public GetAuthorLiteratureView GetAuthor(int id)
        {
            var author = genericRepository.GetByID(id);
            return Mapper.Map<Author, GetAuthorLiteratureView>(author);
        }
        public Author GetAuthorByID(int id)
        {
            return genericRepository.GetByID(id);
        }


        public void CreateAuthor(GetAuthorView authorVM)
        {
            var authorMapped = Mapper.Map<GetAuthorView, Author>(authorVM);
            genericRepository.Create(authorMapped);
        }
        public void DeleteAuthor(int id)
        {
            var author = genericRepository.GetByID(id);
            genericRepository.Remove(author);
        }
        public void Save()
        {
            context.SaveChanges();
        }


        public void EditBooksOFAuthor(int id, int[] bookIDsForDelete, int[] booksIDsForInsert)
        {
            bookAuthorRepository.DeleteBookFromAuthor(id, bookIDsForDelete);
            bookAuthorRepository.AddBookToAuthor(id, booksIDsForInsert);
            Save();
        }
        public void EditArticlesOFAuthor(int id, int[] articlesIDsForDelete, int[] articlesIDsForInsert)
        {
           articleAuthorsRepository.DeleteArticleFromAuthor(id, articlesIDsForDelete);
           articleAuthorsRepository.AddArticleToAuthor(id, articlesIDsForInsert);
            Save();
        }
        public void EditMagazinesOFAuthor(int id, int[] magazinesIDsForDelete, int[] magazinesIDsForInsert)
        {
            magazineAuthorRepository.DeleteMagazineFromAuthor(id, magazinesIDsForDelete);
            magazineAuthorRepository.AddMagazineToAuthor(id, magazinesIDsForInsert);
            Save();
        }
        public void EditPublicationsOFAuthor(int id, int[] publicationsIDsForDelete, int[] publicationsIDsForInsert)
        {
            publicationAuthorsRepository.DeletePublicationFromAuthor(id, publicationsIDsForDelete);
            publicationAuthorsRepository.AddPublicationToAuthor(id, publicationsIDsForInsert);
            Save();
        }




        public List<GetBookView> GetBooksNotExistInAuthor(Author author)
        {
            var listBooks = authorRepository.GetBooksNotExistInAuthor(author);

            var booksMapped = Mapper.Map<List<Book>, List<GetBookView>>(listBooks);
            return booksMapped;
        }
        public List<GetArticleView> GetArticlesNotExistInAuthor(Author author)
        {
            var listArticles = authorRepository.GetArticlesNotExistInAuthor(author);
            var articlesMapped = Mapper.Map<List<Article>, List<GetArticleView>>(listArticles);
            return articlesMapped;
        }
        public List<GetMagazineView> GetMagazinesNotExistInAuthor(Author author)
        {
            var listMagazines = authorRepository.GetMagazinesNotExistInAuthor(author);
            var magazinesMapped = Mapper.Map<List<Magazine>, List<GetMagazineView>>(listMagazines);
            return magazinesMapped;
        }
        public List<GetPublicationView> GetPublicationsNotExistInAuthor(Author author)
        {
            var listPublications = authorRepository.GetPublicationsNotExistInAuthor(author);
            var publicationsMapped = Mapper.Map<List<Publication>, List<GetPublicationView>>(listPublications);
            return publicationsMapped;
        }



        public GetAuthorLiteratureView GetAuthorsBooksDetails(int? id)
        {
            var authorsBookDetails = authorRepository.GetAuthorsBooksDetails(id);
            var authorsBookMapped = Mapper.Map<Author, GetAuthorLiteratureView>(authorsBookDetails);
            return authorsBookMapped;
        }
        public GetAuthorLiteratureView GetAuthorsArticlesDetails(int? id)
        {
            var authorsArticleDetails = authorRepository.GetAuthorsArticlesDetails(id);
            var authorsArticleMapped = Mapper.Map<Author, GetAuthorLiteratureView>(authorsArticleDetails);
            return authorsArticleMapped;
        }
        public GetAuthorLiteratureView GetAuthorsMagazinesDetails(int? id)
        {
            var authorsMagazineDetails = authorRepository.GetAuthorsMagazinesDetails(id);
            var authorsMagazineMapped =  Mapper.Map<Author, GetAuthorLiteratureView>(authorsMagazineDetails);
            return authorsMagazineMapped;
        }
        public GetAuthorLiteratureView GetAuthorsPublicationsDetails(int? id)
        {
            var authorsPublicationDetails = authorRepository.GetAuthorsPublicationsDetails(id);
            var authorsPublicationMapped = Mapper.Map<Author, GetAuthorLiteratureView>(authorsPublicationDetails);
            return authorsPublicationMapped;
        }
    }
}
