/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
namespace Phi.Repository
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Содержит вспомогательные методы для работы с XML.
    /// </summary>
    public class XmlHelpers
    {
        /// <summary>
        /// Возвращает сериализованный объект в строку.
        /// </summary>
        /// <exception cref="ArgumentNullException">Если параметр response - нулевая ссылка.</exception>
        public static string ConvertXMLObjectToString(Type type, Object response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            string result = null;

            var xmlSerializer = new XmlSerializer(type);
            using (var stream = new MemoryStream())
            {
                using (var textWriter = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(textWriter, response);

                    using (var internalStream = (MemoryStream)textWriter.BaseStream)
                    {
                        var encoding = new UTF8Encoding();

                        result = encoding.GetString(internalStream.ToArray());
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Возвращает объект десериализованный из строки.
        /// </summary>
        /// <exception cref="ArgumentNullException">Если параметр response - нулевая ссылка.</exception>
        public static Object ConvertStringToXMLObject(Type type, string response)
        {
            // Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(response));
            Object result = null;

            var xmlSerializer = new XmlSerializer(type);

            var bytes = Encoding.UTF8.GetBytes(response);

            using (var stream = new MemoryStream(bytes))
            {
                using (var textReader = new XmlTextReader(stream))
                {
                    result = xmlSerializer.Deserialize(textReader);
                }
            }

            return result;
        }
    }
}
