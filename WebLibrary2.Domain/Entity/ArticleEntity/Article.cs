using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity.ArticleEntity
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }
        [Required]
        public int ArticleGenreID { get; set; }
        [Required]
        public string ArticleName { get; set; }
        [Required]
        public DateTime DateOfArticlePublish { get; set; }

        public ArticleGenre ArticleGenres { get; set; }

        public virtual IEnumerable<Author> Authors { get; set; }

        public Article()
        {
            Authors = new List<Author>();
        }
    }
}
