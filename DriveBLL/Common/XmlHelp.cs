using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Accounts.BLL.Common
{
    public class XmlHelp
    {
        #region 反序列化
        public static string ObjectToXML<T>(T t)
        {
            return ObjectToXML<T>(t, Encoding.UTF8);
        }


        /// <summary>
        /// 将object对象序列化成XML
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ObjectToXML<T>(T t, Encoding encoding)
        {
            XmlSerializer ser = new XmlSerializer(t.GetType());
            Encoding utf8EncodingWithNoByteOrderMark = new UTF8Encoding(false);
            using (MemoryStream mem = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(mem, utf8EncodingWithNoByteOrderMark))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    ser.Serialize(writer, t, ns);
                    return encoding.GetString(mem.ToArray());
                }
            }
        }
        #endregion

        #region 序列化
        public static T XMLToObject<T>(string source)
        {
            return XMLToObject<T>(source, Encoding.UTF8);
        }


        public static T XMLToObject<T>(string source, Encoding encoding)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(encoding.GetBytes(source)))
            {
                return (T)mySerializer.Deserialize(stream);
            }
        }
        #endregion
    }
}
