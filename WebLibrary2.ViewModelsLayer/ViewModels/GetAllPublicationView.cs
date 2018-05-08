using System;
using System.Collections.Generic;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetAllPublicationView
    {
        public int PublicationID { get; set; }
        public int PublicationGenreID { get; set; }
        public string PublicationGenreName { get; set; }
        public string PublicationName { get; set; }
        public DateTime DateOfPublicationPublish { get; set; }

        public IEnumerable<GetAuthorView> Authors { get; set; }
    }
}
