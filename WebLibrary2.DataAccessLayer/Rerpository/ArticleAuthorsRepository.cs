using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class ArticleAuthorsRepository : IArticeAuthorsRepository
    {
        DbContext context;
        public ArticleAuthorsRepository(DbContext contextParam)
        {
            context = contextParam;
        }

        public void AddArticleToAuthor(int authorID, int[] articleIDsForInsert)
        {
            if (articleIDsForInsert == null)
            {
                return;
            }
            foreach (var articleID in articleIDsForInsert)
            {
                ArticleAuthor articleToAdd = new ArticleAuthor()
                {
                    ArticleID = articleID,
                    AuthorID = authorID
                };
                context.ArticleAuthors.Add(articleToAdd);
                context.SaveChanges();
            }
        }

        public void AddAuthorToArticle(int articleID, int[] authorIDsForInsert)
        {
            if (authorIDsForInsert == null)
            {
                return;
            }
            foreach (var authorID in authorIDsForInsert)
            {
                ArticleAuthor articleToAdd = new ArticleAuthor()
                {
                    ArticleID = articleID,
                    AuthorID = authorID
                };
                context.ArticleAuthors.Add(articleToAdd);
                context.SaveChanges();
            }

        }

        public void DeleteArticleFromAuthor(int authorID, int[] articleIDsForDelete)
        {
            if (articleIDsForDelete == null)
            {
                return;
            }
            foreach (var articleID in articleIDsForDelete)
            {
                var articleToRemove = context.ArticleAuthors.Find(articleID, authorID);
                context.ArticleAuthors.Remove(articleToRemove);
                context.SaveChanges();
            }

        }

        public void DeleteAuthorFromArticle(int articleID, int[] authorIDsForDelete)
        {
            if (authorIDsForDelete == null)
            {
                return;
            }
            foreach (var authorID in authorIDsForDelete)
            {
                var articleToRemove = context.ArticleAuthors.Find(articleID, authorID);
                context.ArticleAuthors.Remove(articleToRemove);
                context.SaveChanges();
            }

        }
    }
}
