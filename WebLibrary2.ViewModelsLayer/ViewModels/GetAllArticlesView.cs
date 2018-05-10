using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetAllArticlesView
    {
        public int ArticleID { get; set; }
        public int ArticleGenreID { get; set; }
        public string ArticleGenreName { get; set; }
        public string ArticleName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfArticlePublish { get; set; }

        public IEnumerable<GetAuthorView> Authors { get; set; }
    }
}
