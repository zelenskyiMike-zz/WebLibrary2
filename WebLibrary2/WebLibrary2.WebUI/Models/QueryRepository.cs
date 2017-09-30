using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Abstract;

namespace WebLibrary2.WebUI.Models
{
    public class QueryRepository
    {
        public QueryRepository()
        {
           EFDbContext context = new EFDbContext();

           public IQueryable<Books> queryBooks = from book in context.Books
                                                  select book;

        }
    }

}