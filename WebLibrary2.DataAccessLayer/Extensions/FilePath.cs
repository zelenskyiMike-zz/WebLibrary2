using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebLibrary2.Domain.Extensions
{
    public static class FilePath
    {
        public static string GetFilePath(HttpPostedFileBase file, string serializeFolderPath)
        {
            string path;
            var fileName = Path.GetFileName(file.FileName);
            path = Path.Combine(serializeFolderPath, fileName);
            return path;

        }
    }
}
