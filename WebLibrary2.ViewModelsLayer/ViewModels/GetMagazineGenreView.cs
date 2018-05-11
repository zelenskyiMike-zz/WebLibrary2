using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetMagazineGenreView
    {
        public int MagazineGenreID { get; set; }
        public string MagazineGenreName { get; set; }

        public IEnumerable<GetMagazineView> Magazines { get; set; }
    }
}
