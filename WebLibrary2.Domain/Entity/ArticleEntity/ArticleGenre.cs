using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity.ArticleEntity
{
    public class ArticleGenre
    {
        [Key]
        public int ArticleGenreID { get; set; }
        public string ArticleGenreName { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public ArticleGenre()
        {
            Articles = new List<Article>();
        }
    }
}
