using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Entity.ArticleEntity;

namespace WebLibrary2.Domain.Concrete.ConcreteArticle
{
    public class EFArticleAuthorsRepository : IArticeAuthorsRepository
    {
        EFDbContext context;
        public EFArticleAuthorsRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }

        public void AddArticleToAuthor(int authorID, int[] articleIDsForInsert)
        {
            if (articleIDsForInsert != null)
            {
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
        }

        public void AddAuthorToArticle(int articleID, int[] authorIDsForInsert)
        {
            if (authorIDsForInsert != null)
            {
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
        }

        public void DeleteArticleFromAuthor(int authorID, int[] articleIDsForDelete)
        {
            if (articleIDsForDelete != null)
            {
                foreach (var articleID in articleIDsForDelete)
                {
                    var articleToRemove = context.ArticleAuthors.Find(articleID, authorID);
                    context.ArticleAuthors.Remove(articleToRemove);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteAuthorFromArticle(int articleID, int[] authorIDsForDelete)
        {
            if (authorIDsForDelete != null)
            {
                foreach (var authorID in authorIDsForDelete)
                {
                    var articleToRemove = context.ArticleAuthors.Find(articleID, authorID);
                    context.ArticleAuthors.Remove(articleToRemove);
                    context.SaveChanges();
                }
            }
        }
    }
}
