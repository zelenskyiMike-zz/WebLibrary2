using System;
using System.Collections.Generic;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetAllMagazineView
    {
        public int MagazineID { get; set; }
        public int MagazineGenreID { get; set; }
        public string MagazineGenreName { get; set; }
        public string MagazineName { get; set; }
        public DateTime DateOfMagazinePublish { get; set; }

        public IEnumerable<GetAuthorView> Authors { get; set; }
    }
}
