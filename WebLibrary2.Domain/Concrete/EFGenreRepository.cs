using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Abstract;

namespace WebLibrary2.Domain.Concrete
{
    public class EFGenreRepository : IGenreRepository
    {
        public EFDbContext context = new EFDbContext();

        public IEnumerable<Genre> Genres
        {
            get { return context.Genres; }
        }

        

    }
}
