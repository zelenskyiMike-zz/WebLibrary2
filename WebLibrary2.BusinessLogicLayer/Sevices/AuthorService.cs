using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class AuthorService
    {
        private readonly IUnitOfWork dbUnitOfWork;
        //private readonly IMapper mapper;

        public AuthorService(IUnitOfWork unitOfWork /*,IMapper mapper*/)
        {
            dbUnitOfWork = unitOfWork;
            //this.mapper = mapper;
        }

        public IEnumerable<GetAuthorView> GetAuthorViews()
        {
            var authors = dbUnitOfWork.AuthorsRepository.GetAllAuthors().ToList();
            return Mapper.Map<IEnumerable<Author>, IEnumerable<GetAuthorView>>(authors);
        }
        public GetAuthorLiteratureView GetAuthor(int id)
        {
            var author = dbUnitOfWork.AuthorsRepository.GetAuthorByID(id);
            return Mapper.Map<Author, GetAuthorLiteratureView>(author);
        }
        //public GetAuthorLiteratureView GetAuthorsDetails(int id)
        //{
        //    var author = dbUnitOfWork.AuthorsRepository.GetAuthorByID(id);
        //    var authorMapped = Mapper.Map<Author, GetAuthorLiteratureView>(author);
        //    return dbUnitOfWork.AuthorsRepository.GetAuthorsDetails(id);
        //}
        public Author GetAuthorByID(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorByID(id);
        }




        public void CreateAuthor(GetAuthorView authorVM)
        {
            var authorMapped = Mapper.Map<GetAuthorView, Author>(authorVM);
            dbUnitOfWork.AuthorsRepository.CreateAuthor(authorMapped);
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




        public List<GetBookView> GetBooksNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetBooksNotExistInAuthor(id);
        }
        public List<GetArticleView> GetArticlesNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetArticlesNotExistInAuthor(id);
        }
        public List<GetMagazineView> GetMagazinesNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetMagazinesNotExistInAuthor(id);
        }
        public List<GetPublicationView> GetPublicationsNotExistInAuthor(int id)
        {
            return dbUnitOfWork.AuthorsRepository.GetPublicationsNotExistInAuthor(id);
        }



        public GetAuthorLiteratureView GetAuthorsBooksDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsBooksDetails(id);
        }
        public GetAuthorLiteratureView GetAuthorsArticlesDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsArticlesDetails(id);
        }
        public GetAuthorLiteratureView GetAuthorsMagazinesDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsMagazinesDetails(id);
        }
        public GetAuthorLiteratureView GetAuthorsPublicationsDetails(int? id)
        {
            return dbUnitOfWork.AuthorsRepository.GetAuthorsPublicationsDetails(id);
        }
    }
}
