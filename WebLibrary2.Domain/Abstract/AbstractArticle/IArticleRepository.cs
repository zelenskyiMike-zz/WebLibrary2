using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity.ArticleEntity;

namespace WebLibrary2.Domain.Abstract.AbstractArticle
{
    public interface IArticleRepository
    {
        IQueryable<Article> GetAllArticlesWithGenres();
    }
}
