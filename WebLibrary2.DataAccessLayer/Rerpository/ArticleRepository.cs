using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.DataAccessLayer.Interfaces;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class ArticleRepository
    {
        DbContext context;
        public ArticleRepository(DbContext contextParam)
        {
            context = contextParam;
        }

        public void DeleteArticle(int id)
        {
            var articleToDelete = GetArticleByID(id);
            context.Articles.Remove(articleToDelete);
            Save();
        }

        public IEnumerable<Article> GetAllArticlesWithGenres()
        {
            return context.Articles.Include(g => g.ArticleGenres);
        }

        public Article GetArticleByID(int? id)
        {
            return context.Articles.Find(id);
        }

        public Article GetArticleDetails(int? id)
        {
            Article article = GetArticleByID(id);
           
           ArticleGenre articleGenre = context.ArticleGenres.Where(x => x.ArticleGenreID == article.ArticleGenreID).SingleOrDefault();

           var authorsList = context.ArticleAuthors.Include(x => x.Authors).Where(x => x.ArticleID == article.ArticleID).Select(x => x.Authors).ToList();

            Article articleVM = new Article()
            {
                ArticleID = article.ArticleID,
                ArticleName = article.ArticleName,
                ArticleGenres = articleGenre,
                Authors = authorsList,
                DateOfArticlePublish = article.DateOfArticlePublish
            };
            return articleVM;
        }

        public List<Author> GetAuthorsNotExistInArticle(Article article)
        {
            var initArticleAuthorList = context.ArticleAuthors.Where(x => x.ArticleID == article.ArticleID).Select(x => x.Authors).ToList();

            List<Author> finalListOfAuthors = new List<Author>();

            foreach (var item in context.Authors.ToList())
            {
                if (!initArticleAuthorList.Contains(item))
                {
                    finalListOfAuthors.Add(item);
                }
            }

            return finalListOfAuthors;
        }

        public void InsertArticle(Article articleVM)
        {
            Article article = new Article()
            {
                ArticleGenreID = articleVM.ArticleGenreID,
                ArticleName = articleVM.ArticleName,
                DateOfArticlePublish = articleVM.DateOfArticlePublish
            };

            context.Articles.Add(article);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
