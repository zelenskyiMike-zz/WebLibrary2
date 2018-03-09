using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Abstract
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> Genres { get; }
    }
}
