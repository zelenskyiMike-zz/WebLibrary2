using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return null; //context.Articles.Include(a => a.);
        }
    }
}
