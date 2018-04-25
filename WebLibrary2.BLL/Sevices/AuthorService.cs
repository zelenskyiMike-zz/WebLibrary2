using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractUnitOfWork;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.BLL.Sevices
{
    public class AuthorService
    {
        private readonly IUnitOfWork dbUnitOfWork;
        private readonly IMapper mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            dbUnitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<AuthorView> GetAuthorViews()
        {
            var authors = dbUnitOfWork.AuthorsRepository.GetAllAuthors().ToList();
            return Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorView>>(authors);
        }
        public GetAuthorLiteratureVM GetAuthorsDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsDetails(id);
        }
        public Author GetAuthorByID(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorByID(id);
        }



        public void CreateAuthor(AuthorView authorVM)
        {
            dbUnitOfWork.AuthorsRepository.CreateAuthor(authorVM);
        }
        public void DeleteAuthor(int id)
        {
            dbUnitOfWork.AuthorsRepository.DeleteAuthor(id);
        }
        public void Save()
        {
            dbUnitOfWork.AuthorsRepository.Save();
        }


        public void EditBooksOFAuthor(int id, int[] bookIDsForDelete, int[] booksIDsForInsert)
        {
            dbUnitOfWork.BookAuthorsRepository.DeleteBookFromAuthor(id, bookIDsForDelete);
            dbUnitOfWork.BookAuthorsRepository.AddBookToAuthor(id, booksIDsForInsert);
            Save();
        }
        public void EditArticlesOFAuthor(int id, int[] articlesIDsForDelete, int[] articlesIDsForInsert)
        {
            dbUnitOfWork.ArticeAuthorsRepository.DeleteArticleFromAuthor(id, articlesIDsForDelete);
            dbUnitOfWork.ArticeAuthorsRepository.AddArticleToAuthor(id, articlesIDsForInsert);
            Save();
        }
        public void EditMagazinesOFAuthor(int id, int[] magazinesIDsForDelete, int[] magazinesIDsForInsert)
        {
            dbUnitOfWork.MagazineAuthorsRepository.DeleteMagazineFromAuthor(id, magazinesIDsForDelete);
            dbUnitOfWork.MagazineAuthorsRepository.AddMagazineToAuthor(id, magazinesIDsForInsert);
            Save();
        }
        public void EditPublicationsOFAuthor(int id, int[] publicationsIDsForDelete, int[] publicationsIDsForInsert)
        {
            dbUnitOfWork.PublicationAuthorsRepository.DeletePublicationFromAuthor(id, publicationsIDsForDelete);
            dbUnitOfWork.PublicationAuthorsRepository.AddPublicationToAuthor(id, publicationsIDsForInsert);
            Save();
        }




        public List<Book> GetBooksNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetBooksNotExistInAuthor(id);
        }
        public List<Article> GetArticlesNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetArticlesNotExistInAuthor(id);
        }
        public List<Magazine> GetMagazinesNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetMagazinesNotExistInAuthor(id);
        }
        public List<Publication> GetPublicationsNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetPublicationsNotExistInAuthor(id);
        }



        public GetAuthorLiteratureVM GetAuthorsBooksDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsBooksDetails(id);
        }
        public GetAuthorLiteratureVM GetAuthorsArticlesDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsArticlesDetails(id);
        }
        public GetAuthorLiteratureVM GetAuthorsMagazinesDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsMagazinesDetails(id);
        }
        public GetAuthorLiteratureVM GetAuthorsPublicationsDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsPublicationsDetails(id);
        }
    }
}
