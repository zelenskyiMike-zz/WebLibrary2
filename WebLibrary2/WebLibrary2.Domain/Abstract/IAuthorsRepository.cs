using System;
using System.Collections.Generic;
using WebLibrary2.Domain.Entity;


namespace WebLibrary2.Domain.Abstract
{
    public interface IAuthorsRepository
    {
        IEnumerable<Author> Authors { get; }
    }
}
