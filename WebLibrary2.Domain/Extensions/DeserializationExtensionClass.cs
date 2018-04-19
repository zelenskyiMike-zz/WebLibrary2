using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;

namespace WebLibrary2.Domain.Extensions
{
    public static class DeserializationExtensionClass
    {
        public static List<TEntity> DeserializeJSON<TEntity>(string filePath)
        {
            List<TEntity> articlesViewData = new List<TEntity>();

            using (StreamReader streamReader = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                JsonSerializer serializer = new JsonSerializer();
                articlesViewData = JsonConvert.DeserializeObject<List<TEntity>>(streamReader.ReadToEnd());
            }

            return articlesViewData;
        }
        public static List<TEntity> DeserializeXML<TEntity>(string filePath)
        {
            Type[] types = new Type[4];

            types[0] = typeof(List<Article>);
            types[1] = typeof(List<Magazine>);
            types[2] = typeof(List<Publication>);
            types[3] = typeof(List<Book>);


            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<DictionaryEntry>), types);
            List<TEntity> authors = new List<TEntity>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                authors = (List<TEntity>)XmlSerializer.Deserialize(fs);
            }
            return authors;
        }

    }
}
