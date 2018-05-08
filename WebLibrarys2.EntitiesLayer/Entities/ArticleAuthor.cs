using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.EntitiesLayer.Entities
{
    public class ArticleAuthor
    {
        [Key,Column(Order = 1)]
        public int ArticleID { get; set; }
        [Key, Column(Order = 2)]
        public int AuthorID { get; set; }

        public Article Articles { get; set; }
        public Author Authors { get; set; }
    }
}
