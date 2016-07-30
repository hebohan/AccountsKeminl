using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Inspur.Finix.DAL.SQL;
using System.Web;
namespace Accounts.BLL.Common
{
    public class DictionaryHelp
    {
        public static string GetValueByCode(string code,string defvalue)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Dictionary");
            select.SelectColumn("Value");
            select.AddWhere("DicCode", code);
            object objValue = select.ExecuteScalar();
            return objValue != null ? objValue.ToString() : defvalue;
        }

        public static string GetValueByCode(string code)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Dictionary");
            select.SelectColumn("Value");
            select.AddWhere("DicCode", code);
            object objValue = select.ExecuteScalar();
            return objValue != null ? objValue.ToString() : "";
        }

        public static string GetNameById(int id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Dictionary");
            select.SelectColumn("DicName");
            select.AddWhere("DicId", id);
            object objValue = select.ExecuteScalar();
            return objValue != null ? objValue.ToString() : "";
        }

        public static DataTable GetDicByParent(string code)
        {
            int id = GetIdByCode(code);
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Dictionary");
            select.AddWhere("ParentId", id);
            select.AddOrderBy("OrderNum",Sort.Ascending);
            return select.ExecuteDataSet().Tables[0];
        }

        public static int GetIdByCode(string code)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Dictionary");
            select.SelectColumn("DicId");
            select.AddWhere("DicCode", code);
            object objValue = select.ExecuteScalar();
            return objValue != null ? int.Parse(objValue.ToString()) : -1;
        }

        public static double GetDiscount(double money)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"  SELECT TOP 1
                                                CONVERT(FLOAT, a.Value) AS discount
                                      FROM      dbo.Dictionary a
                                                LEFT JOIN dbo.Dictionary b ON a.ParentId = b.DicId
                                      WHERE     ISNUMERIC(a.DicName) > 0
                                                AND CONVERT(FLOAT, a.Value) > 0
                                                AND b.DicCode = 'Discount'
                                                AND CONVERT(FLOAT, a.DicName) <= {0}
                                      ORDER BY  CONVERT(FLOAT, a.DicName) DESC ,
                                                CONVERT(FLOAT, a.Value) ASC ",money);
            object obj = select.ExecuteScalar();
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return Convert.ToDouble(obj);
            }
            return 1;
        }

        public static string GetNameByCode(string code)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Dictionary");
            select.SelectColumn("DicName");
            select.AddWhere("DicCode", code);
            object objValue = select.ExecuteScalar();
            return objValue != null ? objValue.ToString() : "";
        }
    }
}
