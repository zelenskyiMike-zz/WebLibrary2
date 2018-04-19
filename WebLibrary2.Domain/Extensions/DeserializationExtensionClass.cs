using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebLibrary2.Domain.Entity.ArticleEntity;

namespace WebLibrary2.Domain.Extensions
{
    public static class DeserializationExtensionClass
    {
        //public static List<object> DeserializeJSON(string filePath)
        //{
        //    List<object> articlesViewData = new List<object>();

        //    using (StreamReader streamReader = new StreamReader(new FileStream(filePath, FileMode.Open)))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        articlesViewData = JsonConvert.DeserializeObject<List<object>>(streamReader.ReadToEnd());
        //    }

        //    return articlesViewData;
        //}

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

            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<TEntity>));
            List<TEntity> authors = new List<TEntity>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                authors = (List<TEntity>)XmlSerializer.Deserialize(fs);
            }
            return authors;
        }

    }
}
