using System;
using System.Collections.Generic;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete
{
    public class EFAuthorRepository : IAuthorsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Authors> Authors 
        {
            get { return context.Authors; }
        }

    }
}
