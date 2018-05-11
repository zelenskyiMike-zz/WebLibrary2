using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.ViewModelsLayer.ViewModels
{
    public class GetPublicationGenreView
    {
        public int PublicationGenreID { get; set; }

        public string PublicationGenreName { get; set; }

        public virtual IEnumerable<GetPublicationView> Publications { get; set; }
    }
}
