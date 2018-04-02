using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Entity.ArticleEntity
{
    public class ArticleGenre
    {
        [Key]
        public int ArticeGenreID { get; set; }
        [Required]
        public string ArticleGenreName { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public ArticleGenre()
        {
            Articles = new List<Article>();
        }
    }
}
