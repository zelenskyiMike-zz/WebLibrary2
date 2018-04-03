using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Entity.ArticleEntity;

namespace WebLibrary2.Domain.Concrete.ConcreteArticle
{
    public class EFArticleRepository : IArticleRepository
    {
        EFDbContext context;
        public EFArticleRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }

        public IQueryable<Article> GetAllArticlesWithGenres()
        {
            return context.Articles.Include(g => g.ArticleGenres);
        }
    }
}
