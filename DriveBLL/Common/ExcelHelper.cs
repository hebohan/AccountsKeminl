using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Inspur.Finix.ExceptionManagement;


namespace Accounts.BLL.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// Excel转换成DataTable,不指定工作表名，默认第一个
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath)
        {
            return ExcelToDataTable(excelPath, null);
        }

        /// <summary>
        /// Excel转换成DataTable,可指定工作表名
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName)
        {
            string conStr = GetConStr(excelPath);
            if (string.IsNullOrEmpty(conStr))
                return null;
            OleDbConnection connection = new OleDbConnection(conStr);
            connection.Open();
            if (string.IsNullOrEmpty(sheetName))
                sheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            else if (!sheetName.Contains("$"))
                sheetName = sheetName + "$";
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [" + sheetName + "]", conStr);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "[" + sheetName + "$]");
            connection.Close();
            return dataSet.Tables[0];
        }

        /// <summary>
        /// 获取Excel数据链接
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        private static string GetConStr(string excelPath)
        {
            string path = excelPath;
            if (!File.Exists(path))
                return null;
            string str2 = Path.GetExtension(path).ToLower();
            if ((str2 != ".xls") && (str2 != ".xlsx"))
                return null;
            //string str3 = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + path + "; Extended Properties=Excel 8.0";
            //if (str2 == ".xlsx")
             string   str3 = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + path + "; Extended Properties=Excel 12.0";
            return str3;
        }

        /// <summary>
        /// If the supplied excel File does not exist then Create it
        /// </summary>
        /// <param name="FileName"></param>
        public static bool CreateExcelFile(string fileName, string title, List<string> heads)
        {
            bool issuccess = true;
            //create
            object Nothing = System.Reflection.Missing.Value;
            var app = new Excel.Application();
            try
            {
                app.Visible = false;
                Excel.Workbook workBook = app.Workbooks.Add(Nothing);
                Excel.Worksheet worksheet = (Excel.Worksheet) workBook.Sheets[1];
                for (int j = 2; j <= workBook.Sheets.Count; j++)
                {
                    ((Excel.Worksheet)workBook.Worksheets[j]).Delete();
                }
                worksheet.Name = string.IsNullOrEmpty(title) ? "导出数据" : title;
                //headline
                int i = 1;
                if (!string.IsNullOrEmpty(title))
                {
                    worksheet.Cells[i, 1] = title;
                    Excel.Range m =
                        worksheet.get_Range("A1", Convert.ToString((char) ((int) 'A' + heads.Count - 1)) + "1") as
                            Excel.Range;
                    m.Merge();
                    m.Font.Size = 15;
                    m.Borders.LineStyle = 1;
                    m.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick,
                        Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
                    m.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    m.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    m.EntireColumn.AutoFit();
                    i++;
                }
                for (int j = 0; j < heads.Count; j++)
                {
                    Excel.Range rng = (Excel.Range) worksheet.Columns[j + 1, Type.Missing]; //设置单元格格式
                    rng.NumberFormatLocal = "@"; //字符型格式
                    rng.Borders.LineStyle = 1;
                    rng.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick,
                        Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
                    Excel.Range cell = worksheet.Cells[i, j + 1] as Excel.Range;
                    worksheet.Cells[i, j + 1] = heads[j];
                    cell.Interior.ColorIndex = 15;
                    cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    cell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    cell.EntireColumn.AutoFit();
                }

                worksheet.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing);
                workBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                ExceptionManager.Handle(ex);
                issuccess = false;
            }
            app.Quit();
            return issuccess;
        }

        /// <summary>
        /// open an excel file,then write the content to file
        /// </summary>
        /// <param name="FileName">file name</param>
        /// <param name="findString">first cloumn</param>
        /// <param name="replaceString">second cloumn</param>
        public static void WriteToExcel(string excelName, DataTable dt)
        {
            //open
            object Nothing = System.Reflection.Missing.Value;
            var app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook mybook = app.Workbooks.Open(excelName, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
            Excel.Worksheet mysheet = (Excel.Worksheet)mybook.Worksheets[1];
            mysheet.Activate();
            //get activate sheet max row count
            int row = mysheet.UsedRange.Rows.Count + 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    mysheet.Cells[i + row, j + 1] = dt.Rows[i][j];
                }
            }
            mybook.Save();
            mybook.Close(false, Type.Missing, Type.Missing);
            mybook = null;
            //quit excel app
            app.Quit();
        }
    }
}
