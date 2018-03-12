using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete
{
    public class EFAuthorRepository : IAuthorsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Author> Authors
        {
            get { return context.Authors; }
        }

        public void makeJson(List<Author> author)
        {
            using (StreamWriter file = File.CreateText(@"C:\Users\Anuitex-53\Documents\Visual Studio 2017\WebLibrary2-master\WebLibrary2\authors.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, author);
            }
        }
    }
}
