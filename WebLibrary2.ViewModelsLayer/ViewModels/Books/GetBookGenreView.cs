using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetBookGenreView
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }

        public IEnumerable<GetBookView> Books { get; set; }
    }
}
