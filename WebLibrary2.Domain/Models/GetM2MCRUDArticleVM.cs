using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Models
{
    public class GetM2MCRUDArticleVM
    {
        public int ArticleID { get; set; }
        public int ArticleGenreID { get; set; }
        public string ArticleGenreName { get; set; }
        public string ArticleName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfArticlePublish { get; set; }

        public IEnumerable<Author> Authors { get; set; }
    }
}
