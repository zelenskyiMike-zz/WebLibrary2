using System.Collections.Generic;
using System.Linq;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IArticleRepository
    {
        IQueryable<Article> GetAllArticlesWithGenres();
        void InsertArticle(Article article);

        Article GetArticleDetails(int? id);
        Article GetArticleByID(int? id);
        List<Author> GetAuthorsNotExistInArticle(int id);
        void DeleteArticle(int id); 
        void Save();
    }
}
