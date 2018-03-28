using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract
{
    public interface IAuthorBooksRepository
    {
        //now only one row per action
        void InsertBookAuthors(Book book);
    }
}
