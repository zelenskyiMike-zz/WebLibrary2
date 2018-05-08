using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebLibrary2.EntitiesLayer.Entities
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfArticlePublish { get; set; }


        [XmlIgnore]
        //[IgnoreDataMember]
        public ArticleGenre ArticleGenres { get; set; }


        [XmlIgnore]
        //[IgnoreDataMember]
        public virtual IEnumerable<Author> Authors { get; set; }

        public Article()
        {
            Authors = new List<Author>();
        }
    }
}
