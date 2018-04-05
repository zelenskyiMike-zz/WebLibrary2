using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract.AbstractArticle
{
    public interface IArticleRepository
    {
        IQueryable<Article> GetAllArticlesWithGenres();
        void InsertArticle(ArticleViewModel articleVM);

        GetM2MCRUDArticleVM GetArticleDetails(int? id);
        Article GetArticleByID(int? id);
        List<Author> GetAuthorsNotExistInArticle(int id);
        void DeleteArticle(int id); 
        void Save();
    }
}
