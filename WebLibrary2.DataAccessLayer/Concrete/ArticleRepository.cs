using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.DataAccessLayer.Interfaces;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class ArticleRepository : IArticleRepository
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

        public IQueryable<Article> GetAllArticlesWithGenres()
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
            var articleGenreName = (from ag in context.ArticleGenres
                                    where ag.ArticleGenreID == article.ArticleGenreID
                                    select ag.ArticleGenreName).SingleOrDefault();
            var authorsList = context.ArticleAuthors.Include(x => x.Authors).Where(x => x.ArticleID == article.ArticleID).Select(x => x.Authors).ToList();

            Article articleVM = new Article()
            {
                ArticleID = article.ArticleID,
                ArticleName = article.ArticleName,
                ArticleGenreName = articleGenreName,
                Authors = authorsList,
                DateOfArticlePublish = article.DateOfArticlePublish
            };
            return articleVM;
        }

        public List<Author> GetAuthorsNotExistInArticle(int id)
        {
            var currArticle = GetArticleByID(id);

            var initArticleAuthorList = context.ArticleAuthors.Where(x => x.ArticleID == currArticle.ArticleID).Select(x => x.Authors).ToList();

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
