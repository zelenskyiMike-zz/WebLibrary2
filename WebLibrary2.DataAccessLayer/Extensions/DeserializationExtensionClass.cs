using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace WebLibrary2.Domain.Extensions
{
    public static class DeserializationExtensionClass
    {
        public static List<TEntity> DeserializeJSON<TEntity>(string filePath)
        {
            var articlesViewData = new List<TEntity>();
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream(filePath, FileMode.Open)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    articlesViewData = JsonConvert.DeserializeObject<List<TEntity>>(streamReader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                return null;
            }
            

            return articlesViewData;
        }
        public static List<TEntity> DeserializeXML<TEntity>(string filePath)
        {

            var xmlSerializer = new XmlSerializer(typeof(List<TEntity>));
            List<TEntity> authors = new List<TEntity>();
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    authors = (List<TEntity>)xmlSerializer.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }
           
            return authors;
        }

    }
}
