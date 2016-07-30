using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;

namespace Accounts.BLL.Common
{
    public class JSONhelper
    {
        /// <summary>
        /// 泛型列表转换为JSON
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <param name="ClassName">属性名称</param>
        /// <returns></returns>
        public static string IListToJSON<T>(IList<T> list, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            foreach (T t in list)
            {
                sb.Append(ModelToJSON(t) + ",");
            }

            string _temp = sb.ToString().TrimEnd(',');
            _temp += "]}";
            return _temp;
        }

        /// <summary>
        /// 泛型列表转换为JSON
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public static string IList2JSON<T>(IList<T> list) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (T t in list)
            {
                sb.Append(ModelToJSON(t) + ",");
            }

            string _temp = sb.ToString().TrimEnd(',');
            _temp += "]";
            return _temp;
        }

        /// <summary>
        /// 数据实体类转换为JSON
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="t">数据实体类</param>
        /// <returns></returns>
        public static string ModelToJSON<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            string json = "";
            if (t != null)
            {
                sb.Append("{");
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    sb.Append("\"" + pi.Name.ToString().ToLower() + "\"");
                    sb.Append(":");
                    sb.Append("\"" + pi.GetValue(t, null).ToString().Replace("\"", "“").Replace("'", "‘").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<", "＜").Replace(">", "＞") + "\"");
                    sb.Append(",");
                }

                json = sb.ToString().TrimEnd(',');
                json += "}";
            }

            return json;
        }

        /// <summary>
        /// 生成压缩的json 字符串
        /// </summary>
        /// <param name="obj">生成json的对象</param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return ToJson(obj, false);
        }
        /// <summary>
        /// 生成JSON字符串
        /// </summary>
        /// <param name="obj">生成json的对象</param>
        /// <param name="formatjson">是否格式化</param>
        /// <returns></returns>
        public static string ToJson(object obj, bool formatjson)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            idtc.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(idtc);
            JsonWriter jw = new JsonTextWriter(sw);

            if (formatjson)
            {
                jw.Formatting = Formatting.Indented;
            }

            serializer.Serialize(jw, obj);

            //JsonConvert.SerializeObject(dt, idtc).ToString();

            return sb.ToString();
        }


        /// <summary>
        /// 将指定串值转换JSON格式
        /// </summary>
        /// <param name="name">Key值</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        public static string StringToJson(string name, string value)
        {
            return "{\"" + name + ":\"" + value + "\"}";
        }

        /// <summary>
        /// 将指定串值转换JSON格式
        /// </summary>
        /// <param name="name">Key值</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        public static string StringToJson(string name, int value)
        {
            return "{\"" + name + "\":" + value.ToString() + "}";
        }

        /// <summary>
        /// 将数组转换为JSON格式的字符串
        /// </summary>
        /// <typeparam name="T">数据类型，如string,int ...</typeparam>
        /// <param name="list">泛型list</param>
        /// <param name="propertyname">JSON的类名</param>
        /// <returns></returns>
        public static string ArrayToJSON<T>(List<T> list, string propertyname)
        {
            StringBuilder sb = new StringBuilder();
            if (list.Count > 0)
            {
                sb.Append("[{\"");
                sb.Append(propertyname);
                sb.Append("\":[");

                foreach (T t in list)
                {
                    sb.Append("\"");
                    sb.Append(t.ToString());
                    sb.Append("\",");
                }

                string _temp = sb.ToString();
                _temp = _temp.TrimEnd(',');

                _temp += "]}]";

                return _temp;
            }
            else
                return "";
        }

        public static string DataTableToJSON(DataTable dt, string dtName)
        {
            string s = DataTableToJSONJquery(dt);
            s = "{\"" + dtName + "\":" + s + "}";
            return s;
        }

        public static string DataTableToJSONJquery(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{");
                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", dt.Columns[n].ColumnName.ToLower(), dt.Rows[i][n].ToString().Replace("\"", "“").Replace("'", "‘").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<", "＜").Replace(">", "＞"));
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1).Insert(0, "[").Append("]");
                return sb.ToString();
            }
            return "";
        }

        public static string DataRowToJSON(DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            if (row == null)
                return "";
            sb.Append("{");
            for (int n = 0; n < row.Table.Columns.Count; n++)
            {
                sb.AppendFormat("\"{0}\":\"{1}\",", row.Table.Columns[n].ColumnName.ToLower(), row[n].ToString().Replace("\"", "“").Replace("'", "‘").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<", "＜").Replace(">", "＞"));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        public static string ArrayToJSON(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.AppendFormat("'{0}':'{1}',", i + 1, strs[i]);
            }
            if (sb.Length > 0)
                return "{" + sb.ToString().TrimEnd(',') + "}";
            return "";
        }



        public static string ArrayToJSON(string[] strs, string var)
        {
            return var + "=" + ArrayToJSON(strs);
        }

        public static DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }


        public static string FormatJSONForJQgrid(int totalpages, int pageindex, int recordcount, IEnumerable rows)
        {
            string json = string.Format("{\"total\":\"{0}\",\"page\":\"{1}\",\"records\":\"{2}\",\"rows\":{3}}",
                        totalpages.ToString(),
                        pageindex.ToString(),
                        recordcount.ToString(),
                        ToJson(rows));

            return json;
        }




        /// <summary>
        /// 格式化EASYUI DATAGRID JSON
        /// </summary>
        /// <param name="recordcount">总记录数</param>
        /// <param name="rows">每页记录的JSON格式</param>
        /// <returns></returns>
        public static string FormatJSONForEasyuiDataGrid(int recordcount, string rows)
        {
            return "{\"total\":" + recordcount.ToString() + ",\"rows\":" + rows + "}";
        }

    }
}
