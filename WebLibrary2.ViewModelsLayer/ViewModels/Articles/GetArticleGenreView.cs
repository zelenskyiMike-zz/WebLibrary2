using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetArticleGenreView
    {
        public int ArticleGenreID { get; set; }
        public string ArticleGenreName { get; set; }

        public IEnumerable<GetArticleView> Articles { get; set; }
    }
}
