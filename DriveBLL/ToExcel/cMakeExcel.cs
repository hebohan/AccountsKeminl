namespace Accounts.BLL.ToExcel
{
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.UI;

    public class cMakeExcel : System.Web.UI.Page
    {
        private DateTime afterTime;
        private ArrayList AppendRows = new ArrayList();
        private DateTime beforeTime;
        private string ColId = string.Empty;
        private int colIndex = 0;
        private int ColNum = 0;
        private int ColSpan = 0;
        private cEnContentParams ContentParams = null;
        private Application excel = null;
        private bool IsSortColumn = false;
        private int MaxRowCount = 0;
        private int rowIndex = 0;
        private int RowNum = 0;
        private bool SearchFlag = false;
        private cEnTitleParams TitleParams = null;
        private ArrayList VisitList = new ArrayList();
        private _Workbook xBk = null;
        public StringBuilder xlsStr = new StringBuilder();
        private _Worksheet xSt = null;

        public cMakeExcel(cEnTitleParams titleParams, cEnContentParams contentParams)
        {
            this.TitleParams = titleParams;
            this.ContentParams = contentParams;
        }

        public void AppendXlsRow(ArrayList txtRow)
        {
            this.AppendRows.Add(txtRow);
        }

        private void ColsCount(string titleId)
        {
            DataRow[] rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleParentIdColName + "='" + titleId + "'");
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                foreach (DataRow row in rowArray)
                {
                    if (!this.VisitList.Contains(titleId))
                    {
                        this.ColsCount(row[this.TitleParams.TitleIdColName].ToString());
                    }
                }
            }
            else
            {
                this.ColSpan++;
            }
            if (!this.VisitList.Contains(titleId))
            {
                this.VisitList.Add(titleId);
            }
        }

        private void CurrentColNum(string rootId)
        {
            try
            {
                DataRow[] rowArray = null;
                if (this.IsSortColumn)
                {
                    rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleParentIdColName + "='" + rootId + "'", this.TitleParams.TitleSortColName + " ASC");
                }
                else
                {
                    rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleParentIdColName + "='" + rootId + "'");
                }
                if (rowArray.Length > 0)
                {
                    foreach (DataRow row in rowArray)
                    {
                        if (!row[this.TitleParams.TitleIdColName].ToString().Equals(this.ColId))
                        {
                            if (!this.SearchFlag)
                            {
                                if (!this.IsChildTitle(row[this.TitleParams.TitleIdColName].ToString()))
                                {
                                    this.ColNum++;
                                }
                                this.CurrentColNum(row[this.TitleParams.TitleIdColName].ToString());
                            }
                        }
                        else
                        {
                            this.ColNum++;
                            this.SearchFlag = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        private void CurrentRowsNum(string titleId)
        {
            DataRow[] rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleIdColName + "='" + titleId + "'");
            if (rowArray.Length > 0)
            {
                this.RowNum++;
                if (!rowArray[0][this.TitleParams.TitleParentIdColName].ToString().Equals(this.TitleParams.TitleParentIdDefaultValue))
                {
                    this.CurrentRowsNum(rowArray[0][this.TitleParams.TitleParentIdColName].ToString());
                }
            }
        }

        private bool ExistFiles(string strPath)
        {
            FileInfo[] files = new DirectoryInfo(strPath).GetFiles();
            foreach (FileInfo info2 in files)
            {
                if (((info2.Extension.ToString() == ".xls") || (info2.Extension.ToString() == ".csv")) && info2.Name.Equals(this.ContentParams.FileName))
                {
                    return true;
                }
            }
            return false;
        }

        private ArrayList GetNextTitle(ArrayList pList)
        {
            ArrayList list = new ArrayList();
            if ((pList != null) && (pList.Count > 0))
            {
                for (int i = 0; i < pList.Count; i++)
                {
                    DataRow[] rowArray = null;
                    if (this.IsSortColumn)
                    {
                        rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleParentIdColName + "='" + pList[i].ToString() + "'", this.TitleParams.TitleSortColName + " ASC");
                    }
                    else
                    {
                        rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleParentIdColName + "='" + pList[i].ToString() + "'");
                    }
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        foreach (DataRow row in rowArray)
                        {
                            list.Add(row[this.TitleParams.TitleIdColName].ToString());
                        }
                    }
                }
            }
            return list;
        }

        private string GetTempPath(HttpRequest request)
        {
            string str = string.Empty;
            string localPath = string.Empty;
            localPath = request.Url.LocalPath;
            if (localPath.Length > 0)
            {
                str = localPath.Substring(0, localPath.IndexOf("/", (int) (localPath.IndexOf("/") + 1)) + 1);
            }
            return str;
        }

        private string GetTitleText(string titleId)
        {
            DataRow[] rowArray = this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleIdColName + "='" + titleId + "'");
            if (rowArray.Length > 0)
            {
                return rowArray[0][this.TitleParams.TitleNameColName].ToString();
            }
            return "";
        }

        private string GetUrlPath(HttpRequest request)
        {
            string str = string.Empty;
            str = request.Url.ToString();
            if (str.Length > 8)
            {
                str = str.Substring(0, str.IndexOf("/", (int) (str.IndexOf("/", 8) + 1)) + 1);
            }
            return str;
        }

        private void InitVariable()
        {
            this.VisitList.Clear();
            this.RowNum = 0;
            this.ColNum = 0;
            this.SearchFlag = false;
            this.ColSpan = 0;
        }

        private bool IsChildTitle(string titleId)
        {
            return (this.TitleParams.TitleDataSet.Tables[0].Select(this.TitleParams.TitleParentIdColName + "='" + titleId + "'").Length > 0);
        }

        public void KillExcelProcess()
        {
            Process[] processesByName = Process.GetProcessesByName("Excel");
            foreach (Process process in processesByName)
            {
                DateTime startTime = process.StartTime;
                if ((startTime > this.beforeTime) && (startTime < this.afterTime))
                {
                    process.Kill();
                }
            }
        }

        public string MakeExcel(HttpResponse response, HttpRequest request, string type, string showUrl, string tmpRelativePath)
        {
            string str = null;
            Exception exception;
            try
            {
                str = this.ValidateParam();
                if (str != null)
                {
                    return str;
                }
                if (type.ToLower().Equals("html"))
                {
                    if (this.ContentParams.FileName.Equals(string.Empty))
                    {
                        this.ContentParams.FileName = "统计报表(" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString() + ").xls";
                    }
                    else
                    {
                        this.TransFileName();
                    }
                }
                if (type.ToLower().Equals("file"))
                {
                    this.ContentParams.FileName = string.Concat(new object[] { DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day, " ", DateTime.Now.Hour, "-", DateTime.Now.Minute, "-", DateTime.Now.Second, "-", DateTime.Now.Millisecond, ".xls" });
                }
                if (type.ToLower().Equals("html"))
                {
                    response.Cache.SetNoStore();
                    response.Clear();
                    response.ClearHeaders();
                    response.ClearContent();
                    response.Buffer = true;
                    response.Charset = "UTF-8";
                    response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.ContentParams.FileName, Encoding.UTF8));
                    response.ContentType = "application/ms-excel";
                    this.XlsHtmlTitle();
                    this.XlsHtmlData();
                    response.Write(this.xlsStr.ToString());
                    response.End();
                }
                if (!type.ToLower().Equals("file"))
                {
                    return str;
                }
                string tempPath = this.GetTempPath(request);
                string strPath = string.Empty;
                this.beforeTime = DateTime.Now;
                try
                {
                    this.excel = new ApplicationClass();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    response.Write("<script language=javascript>alert(\"请检查本机器是否安装 Excel！\r\n" + exception.Message + "\");</script>");
                    return null;
                }
                this.afterTime = DateTime.Now;
                this.xBk = this.excel.Workbooks.Add(true);
                this.xSt = (_Worksheet) this.xBk.ActiveSheet;
                if (tempPath.Equals(string.Empty))
                {
                    return str;
                }
                strPath = base.Server.MapPath(tempPath);
                this.RemoveFiles(strPath);
                this.XlsFileTitle();
                this.XlsFileData();
                this.xSt.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                if (!((tmpRelativePath == null) || tmpRelativePath.Equals("")))
                {
                    this.xBk.SaveAs(strPath + tmpRelativePath + "/" + this.ContentParams.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                else
                {
                    this.xBk.SaveAs(strPath + this.ContentParams.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                Marshal.ReleaseComObject(this.xBk);
                this.xBk = null;
                Marshal.ReleaseComObject(this.excel);
                this.excel = null;
                this.KillExcelProcess();
                Thread.Sleep(0x1388);
                FileInfo info = new FileInfo(strPath + this.ContentParams.FileName);
                if ((showUrl == null) || showUrl.Equals(""))
                {
                    response.Clear();
                    response.ClearHeaders();
                    response.Buffer = false;
                    response.ContentType = "application/octet-stream";
                    response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(info.FullName, Encoding.UTF8));
                    response.AppendHeader("Content-Length", info.Length.ToString());
                    response.WriteFile(info.FullName);
                    response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return str;
                }
                response.Charset = "UTF-8";
                if (!((tmpRelativePath == null) || tmpRelativePath.Equals("")))
                {
                    response.Write("<script language=javascript>window.open(\"" + showUrl + this.ContentParams.FileName + "\",\"_main\");</script>");
                }
                else
                {
                    response.Write("<script language=javascript>window.open(\"" + showUrl + this.GetUrlPath(request) + this.ContentParams.FileName + "\",\"_main\");</script>");
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Console.WriteLine(exception.Message);
            }
            return str;
        }

        public string MakeExcel2(HttpResponse response, HttpRequest request, string type, string showUrl, string tmpRelativePath)
        {
            string str = null;
            Exception exception;
            try
            {
                str = this.ValidateParam();
                if (str != null)
                {
                    return str;
                }
                if (type.ToLower().Equals("html"))
                {
                    if (this.ContentParams.FileName.Equals(string.Empty))
                    {
                        this.ContentParams.FileName = "统计报表(" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString() + ").xls";
                    }
                    else
                    {
                        this.TransFileName();
                    }
                }
                if (type.ToLower().Equals("file"))
                {
                    this.ContentParams.FileName = string.Concat(new object[] { DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day, " ", DateTime.Now.Hour, "-", DateTime.Now.Minute, "-", DateTime.Now.Second, "-", DateTime.Now.Millisecond, ".xls" });
                }
                if (type.ToLower().Equals("html"))
                {
                    response.Cache.SetNoStore();
                    response.Clear();
                    response.ClearHeaders();
                    response.ClearContent();
                    response.Buffer = true;
                    response.Charset = "GB2312";
                    response.ContentEncoding = Encoding.GetEncoding("GB2312");
                    response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.ContentParams.FileName, Encoding.UTF8));
                    response.ContentType = "application/ms-excel";
                    this.XlsHtmlTitle();
                    this.XlsHtmlData();
                    response.Write(this.xlsStr.ToString());
                    response.End();
                }
                if (!type.ToLower().Equals("file"))
                {
                    return str;
                }
                string tempPath = this.GetTempPath(request);
                string strPath = string.Empty;
                this.beforeTime = DateTime.Now;
                try
                {
                    this.excel = new ApplicationClass();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    response.Write("<script language=javascript>alert(\"请检查本机器是否安装 Excel！\r\n" + exception.Message + "\");</script>");
                    return null;
                }
                this.afterTime = DateTime.Now;
                this.xBk = this.excel.Workbooks.Add(true);
                this.xSt = (_Worksheet)this.xBk.ActiveSheet;
                if (tempPath.Equals(string.Empty))
                {
                    return str;
                }
                strPath = base.Server.MapPath(tempPath);
                this.RemoveFiles(strPath);
                this.XlsFileTitle();
                this.XlsFileData();
                this.xSt.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                if (!((tmpRelativePath == null) || tmpRelativePath.Equals("")))
                {
                    this.xBk.SaveAs(strPath + tmpRelativePath + "/" + this.ContentParams.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                else
                {
                    this.xBk.SaveAs(strPath + this.ContentParams.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                Marshal.ReleaseComObject(this.xBk);
                this.xBk = null;
                Marshal.ReleaseComObject(this.excel);
                this.excel = null;
                this.KillExcelProcess();
                Thread.Sleep(0x1388);
                FileInfo info = new FileInfo(strPath + this.ContentParams.FileName);
                if ((showUrl == null) || showUrl.Equals(""))
                {
                    response.Clear();
                    response.ClearHeaders();
                    response.Buffer = false;
                    response.ContentType = "application/octet-stream";
                    response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(info.FullName, Encoding.UTF8));
                    response.AppendHeader("Content-Length", info.Length.ToString());
                    response.WriteFile(info.FullName);
                    response.Flush();
                    response.End();
                    return str;
                }
                response.Charset = "GB2312";
                if (!((tmpRelativePath == null) || tmpRelativePath.Equals("")))
                {
                    response.Write("<script language=javascript>window.open(\"" + showUrl + this.ContentParams.FileName + "\",\"_main\");</script>");
                }
                else
                {
                    response.Write("<script language=javascript>window.open(\"" + showUrl + this.GetUrlPath(request) + this.ContentParams.FileName + "\",\"_main\");</script>");
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Console.WriteLine(exception.Message);
            }
            return str;
        }

        private void MakeFileTitleColumn(ArrayList dtList)
        {
            try
            {
                if ((dtList != null) && (dtList.Count > 0))
                {
                    for (int i = 0; i < dtList.Count; i++)
                    {
                        this.InitVariable();
                        this.ColsCount(dtList[i].ToString());
                        this.ColId = dtList[i].ToString();
                        this.CurrentColNum(this.TitleParams.TitleParentIdDefaultValue);
                        this.CurrentRowsNum(dtList[i].ToString());
                        this.xSt.Cells[this.RowNum + this.rowIndex, this.ColNum] = this.GetTitleText(dtList[i].ToString());
                        if (this.IsChildTitle(dtList[i].ToString()))
                        {
                            this.SetTitleStyle(this.RowNum + this.rowIndex, this.ColNum, this.RowNum + this.rowIndex, (this.ColNum + this.ColSpan) - 1);
                        }
                        else if (this.RowNum < this.MaxRowCount)
                        {
                            this.SetTitleStyle(this.RowNum + this.rowIndex, this.ColNum, this.MaxRowCount + this.rowIndex, (this.ColNum + this.ColSpan) - 1);
                        }
                        else
                        {
                            this.SetTitleStyle(this.RowNum + this.rowIndex, this.ColNum, this.RowNum + this.rowIndex, (this.ColNum + this.ColSpan) - 1);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            if ((dtList != null) && (dtList.Count > 0))
            {
                this.MakeFileTitleColumn(this.GetNextTitle(dtList));
            }
        }

        private void MakeHtmlTitleColumn(ArrayList dtList)
        {
            try
            {
                if ((dtList != null) && (dtList.Count > 0))
                {
                    this.xlsStr.Append("<tr height=" + this.TitleParams.TitleHeight.ToString() + " bgcolor=\"" + this.TitleParams.TitleBGColor + "\">");
                    for (int i = 0; i < dtList.Count; i++)
                    {
                        this.InitVariable();
                        this.ColsCount(dtList[i].ToString());
                        this.CurrentRowsNum(dtList[i].ToString());
                        this.xlsStr.Append("<td align=" + this.TitleParams.TitleAlign + " colspan=" + Convert.ToString(this.ColSpan) + " ");
                        if (this.IsChildTitle(dtList[i].ToString()))
                        {
                            this.xlsStr.Append(">");
                        }
                        else if (this.RowNum < this.MaxRowCount)
                        {
                            this.xlsStr.Append("rowspan=" + Convert.ToString((int) ((this.MaxRowCount - this.RowNum) + 1)) + ">");
                        }
                        else
                        {
                            this.xlsStr.Append(">");
                        }
                        this.InitVariable();
                        this.xlsStr.Append("<font size=" + this.TitleParams.TitleFontSize.ToString() + " face=\"" + this.TitleParams.TitleFontFace + "\" color=" + this.TitleParams.TitleFontColor + ">");
                        this.xlsStr.Append(this.GetTitleText(dtList[i].ToString()));
                        this.xlsStr.Append("</font>");
                        this.xlsStr.Append("</td>");
                    }
                    this.xlsStr.Append("</tr>");
                }
            }
            catch (Exception exception)
            {
                this.xlsStr.Append(exception.Message);
            }
            if ((dtList != null) && (dtList.Count > 0))
            {
                this.MakeHtmlTitleColumn(this.GetNextTitle(dtList));
            }
        }

        private void MaxRows(string titleId)
        {
            foreach (DataRow row in this.TitleParams.TitleDataSet.Tables[0].Rows)
            {
                this.InitVariable();
                this.CurrentRowsNum(row[this.TitleParams.TitleIdColName].ToString());
                if (this.MaxRowCount < this.RowNum)
                {
                    this.MaxRowCount = this.RowNum;
                }
                this.InitVariable();
            }
        }

        private void RemoveFiles(string strPath)
        {
            FileInfo[] files = new DirectoryInfo(strPath).GetFiles();
            foreach (FileInfo info2 in files)
            {
                if ((info2.Extension.ToString() == ".xls") || (info2.Extension.ToString() == ".csv"))
                {
                    TimeSpan span = new TimeSpan(0, 0, 60, 0, 0);
                    if (info2.CreationTime < DateTime.Now.Subtract(span))
                    {
                        info2.Delete();
                    }
                }
            }
        }

        private void SetDataAreaStyle(int startRow, int startCol, int endRow, int endCol)
        {
            try
            {
                if ((startRow <= endRow) && (startCol <= endCol))
                {
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Select();
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Borders.LineStyle = 1;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Font.Name = this.ContentParams.DataFongFace;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Font.Size = this.ContentParams.DataFontSize;
                    if (this.ContentParams.DataAlign.ToLower().Equals("left"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }
                    if (this.ContentParams.DataAlign.ToLower().Equals("center"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                    }
                    if (this.ContentParams.DataAlign.ToLower().Equals("right"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                    }
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).WrapText = true;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).EntireRow.AutoFit();
                    if (this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Rows.RowHeight.ToString().Equals("14.25"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Rows.RowHeight = this.ContentParams.DataHeight;
                    }
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).EntireColumn.AutoFit();
                    if (this.ContentParams.Alternate)
                    {
                        int num = 0;
                        for (int i = startRow; i <= endRow; i++)
                        {
                            num++;
                            if ((num % 2) == 0)
                            {
                                this.xSt.get_Range(this.xSt.Cells[i, startCol], this.xSt.Cells[i, endCol]).Select();
                                this.xSt.get_Range(this.xSt.Cells[i, startCol], this.xSt.Cells[i, endCol]).Interior.ColorIndex = 0x13;
                            }
                        }
                    }
                    this.xSt.get_Range(this.xSt.Cells[1, 1], this.xSt.Cells[1, 1]).Select();
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        private void SetDataStyle(int startRow, int startCol, int endRow, int endCol, bool isAlternate)
        {
            try
            {
                if ((startRow <= endRow) && (startCol <= endCol))
                {
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).MergeCells = true;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Columns.ColumnWidth = this.ContentParams.DataWidth;
                    if (this.ContentParams.Alternate && !isAlternate)
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Interior.ColorIndex = 0x13;
                    }
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Borders.LineStyle = 1;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Font.Name = this.ContentParams.DataFongFace;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Font.Size = this.ContentParams.DataFontSize;
                    if (this.ContentParams.DataAlign.ToLower().Equals("left"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }
                    if (this.ContentParams.DataAlign.ToLower().Equals("center"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                    }
                    if (this.ContentParams.DataAlign.ToLower().Equals("right"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                    }
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).WrapText = true;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).EntireRow.AutoFit();
                    if (this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Rows.RowHeight.ToString().Equals("14.25"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Rows.RowHeight = this.ContentParams.DataHeight;
                    }
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).EntireColumn.AutoFit();
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        private void SetTitleStyle(int startRow, int startCol, int endRow, int endCol)
        {
            try
            {
                if ((startRow <= endRow) && (startCol <= endCol))
                {
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).MergeCells = true;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Interior.ColorIndex = 15;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Borders.LineStyle = 1;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Font.Name = this.TitleParams.TitleFontFace;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Font.Size = this.TitleParams.TitleFontSize;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).WrapText = true;
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).EntireRow.AutoFit();
                    if (this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Rows.RowHeight.ToString().Equals("14.25"))
                    {
                        this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).Rows.RowHeight = this.TitleParams.TitleHeight;
                    }
                    this.xSt.get_Range(this.xSt.Cells[startRow, startCol], this.xSt.Cells[endRow, endCol]).EntireColumn.AutoFit();
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        private void TransFileName()
        {
            if (this.ContentParams.FileName.Length > 4)
            {
                if (!this.ContentParams.FileName.Substring(this.ContentParams.FileName.Length - 4, 4).ToLower().Equals(".xls"))
                {
                    this.ContentParams.FileName = this.ContentParams.FileName + ".xls";
                }
            }
            else
            {
                this.ContentParams.FileName = this.ContentParams.FileName + ".xls";
            }
        }

        private string ValidateParam()
        {
            if ((((this.TitleParams != null) && (this.TitleParams.TitleDataSet != null)) && (this.TitleParams.TitleDataSet.Tables.Count > 0)) && (this.TitleParams.TitleDataSet.Tables[0].Rows.Count > 0))
            {
                if (this.TitleParams.TitleDataSet.DataSetName.Equals("HeaderDataSet"))
                {
                    this.TitleParams.TitleIdColName = "Id";
                    this.TitleParams.TitleNameColName = "Name";
                    this.TitleParams.TitleParentIdColName = "ParentId";
                    this.TitleParams.TitleSortColName = "Rank";
                }
                else
                {
                    if (this.TitleParams.TitleIdColName.Equals(string.Empty))
                    {
                        return "未设置标题Id列名称";
                    }
                    if (this.TitleParams.TitleNameColName.Equals(string.Empty))
                    {
                        return "未设置标题名称列名称";
                    }
                    if (this.TitleParams.TitleParentIdColName.Equals(string.Empty))
                    {
                        return "未设置标题父Id列名称";
                    }
                }
                if (this.TitleParams.TitleDataSet.Tables[0].Columns[this.TitleParams.TitleIdColName] == null)
                {
                    return ("标题数据中不包括" + this.TitleParams.TitleIdColName);
                }
                if (this.TitleParams.TitleDataSet.Tables[0].Columns[this.TitleParams.TitleNameColName] == null)
                {
                    return ("标题数据中不包括" + this.TitleParams.TitleNameColName);
                }
                if (this.TitleParams.TitleDataSet.Tables[0].Columns[this.TitleParams.TitleParentIdColName] == null)
                {
                    return ("标题数据中不包括" + this.TitleParams.TitleParentIdColName);
                }
                if (this.TitleParams.TitleDataSet.Tables[0].Columns[this.TitleParams.TitleSortColName] != null)
                {
                    this.IsSortColumn = true;
                }
            }
            return null;
        }

        private void XlsFileData()
        {
            try
            {
                int num;
                int num2;
                if ((this.ContentParams.ContentDataSet != null) && (this.ContentParams.ContentDataSet.Tables.Count > 0))
                {
                    for (num = 0; num < this.ContentParams.ContentDataSet.Tables.Count; num++)
                    {
                        System.Data.DataTable table = this.ContentParams.ContentDataSet.Tables[num];
                        if (table.Rows.Count > 0)
                        {
                            ArrayList list = new ArrayList();
                            foreach (DataColumn column in table.Columns)
                            {
                                list.Add(column.ColumnName.ToString());
                            }
                            if ((this.ContentParams.HiddenColumn != null) && (this.ContentParams.HiddenColumn.Count > 0))
                            {
                                num2 = 0;
                                while (num2 < this.ContentParams.HiddenColumn.Count)
                                {
                                    if (list.Contains(this.ContentParams.HiddenColumn[num2]))
                                    {
                                        list.Remove(this.ContentParams.HiddenColumn[num2]);
                                    }
                                    num2++;
                                }
                            }
                            string sort = string.Empty;
                            if (((this.ContentParams.DataSortColumn != null) && (this.ContentParams.DataSortColumn.Count > 0)) && !this.ContentParams.DataSortMode.Equals(string.Empty))
                            {
                                StringBuilder builder = new StringBuilder();
                                num2 = 0;
                                while (num2 < this.ContentParams.DataSortColumn.Count)
                                {
                                    builder.Append(this.ContentParams.DataSortColumn[num2].ToString() + ",");
                                    num2++;
                                }
                                builder.Remove(builder.ToString().Length - 1, 1);
                                sort = builder.ToString() + " " + this.ContentParams.DataSortMode;
                            }
                            DataRow[] rowArray = null;
                            if (!sort.Equals(string.Empty))
                            {
                                rowArray = table.Select("", sort);
                            }
                            else
                            {
                                rowArray = table.Select("");
                            }
                            this.rowIndex = this.MaxRowCount + this.rowIndex;
                            int startRow = this.rowIndex + 1;
                            foreach (DataRow row in rowArray)
                            {
                                this.rowIndex++;
                                num2 = 0;
                                while (num2 < list.Count)
                                {
                                    this.xSt.Cells[this.rowIndex, num2 + 1] = row[list[num2].ToString()].ToString();
                                    num2++;
                                }
                            }
                            this.InitVariable();
                            this.ColsCount(this.TitleParams.TitleParentIdDefaultValue);
                            this.SetDataAreaStyle(startRow, 1, (startRow + rowArray.Length) - 1, this.ColSpan);
                            this.InitVariable();
                        }
                    }
                }
                if (this.AppendRows.Count > 0)
                {
                    int num4 = 0;
                    bool isAlternate = false;
                    for (num = 0; num < this.AppendRows.Count; num++)
                    {
                        ArrayList list2 = (ArrayList) this.AppendRows[num];
                        if ((list2 != null) && (list2.Count > 0))
                        {
                            if (this.ContentParams.Alternate)
                            {
                                if ((num4 % 2) == 0)
                                {
                                    isAlternate = true;
                                }
                                num4++;
                            }
                            this.rowIndex++;
                            int startCol = 1;
                            for (num2 = 0; num2 < list2.Count; num2++)
                            {
                                if (list2[num2].GetType().Name.Equals("cXlsColumn"))
                                {
                                    cXlsColumn column2 = (cXlsColumn) list2[num2];
                                    this.xSt.Cells[this.rowIndex, startCol] = column2.Text;
                                    this.SetDataStyle(this.rowIndex, startCol, this.rowIndex, (startCol + column2.Colspan) - 1, isAlternate);
                                    if (column2.Align.ToLower().Equals("left"))
                                    {
                                        this.xSt.get_Range(this.xSt.Cells[this.rowIndex, startCol], this.xSt.Cells[this.rowIndex, (startCol + column2.Colspan) - 1]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    }
                                    if (column2.Align.ToLower().Equals("center"))
                                    {
                                        this.xSt.get_Range(this.xSt.Cells[this.rowIndex, startCol], this.xSt.Cells[this.rowIndex, (startCol + column2.Colspan) - 1]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                                    }
                                    if (column2.Align.ToLower().Equals("right"))
                                    {
                                        this.xSt.get_Range(this.xSt.Cells[this.rowIndex, startCol], this.xSt.Cells[this.rowIndex, (startCol + column2.Colspan) - 1]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                                    }
                                    startCol += column2.Colspan;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.xlsStr.Append(exception.Message);
            }
        }

        private void XlsFileTitle()
        {
            try
            {
                if ((this.TitleParams.TitleDataSet != null) && (this.TitleParams.TitleDataSet.Tables.Count > 0))
                {
                    System.Data.DataTable table = this.TitleParams.TitleDataSet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow[] rowArray = null;
                        if (this.IsSortColumn)
                        {
                            rowArray = table.Select(this.TitleParams.TitleParentIdColName + "='" + this.TitleParams.TitleParentIdDefaultValue + "'", this.TitleParams.TitleSortColName + " ASC");
                        }
                        else
                        {
                            rowArray = table.Select(this.TitleParams.TitleParentIdColName + "='" + this.TitleParams.TitleParentIdDefaultValue + "'");
                        }
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            this.InitVariable();
                            this.MaxRows(this.TitleParams.TitleParentIdDefaultValue);
                            if (!this.TitleParams.TitleHeaderName.Equals(string.Empty))
                            {
                                this.ColsCount(this.TitleParams.TitleParentIdDefaultValue);
                                if (this.ColSpan > 0)
                                {
                                    this.rowIndex++;
                                    this.colIndex++;
                                    this.xSt.Cells[1, 1] = this.TitleParams.TitleHeaderName;
                                    this.xSt.get_Range(this.xSt.Cells[this.rowIndex, this.colIndex], this.xSt.Cells[this.rowIndex, this.ColSpan]).MergeCells = true;
                                    this.xSt.get_Range(this.xSt.Cells[this.rowIndex, this.colIndex], this.xSt.Cells[this.rowIndex, this.ColSpan]).Rows.RowHeight = this.TitleParams.TitleHeaderHeight;
                                    this.xSt.get_Range(this.xSt.Cells[this.rowIndex, this.colIndex], this.xSt.Cells[this.rowIndex, this.ColSpan]).Font.Name = this.TitleParams.TitleHeaderFontFace;
                                    this.xSt.get_Range(this.xSt.Cells[this.rowIndex, this.colIndex], this.xSt.Cells[this.rowIndex, this.ColSpan]).Font.Size = this.TitleParams.TitleHeaderFontSize;
                                    this.xSt.get_Range(this.xSt.Cells[this.rowIndex, this.colIndex], this.xSt.Cells[this.rowIndex, this.ColSpan]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                                }
                                this.InitVariable();
                            }
                            ArrayList dtList = new ArrayList();
                            foreach (DataRow row in rowArray)
                            {
                                dtList.Add(row[this.TitleParams.TitleIdColName].ToString());
                            }
                            this.MakeFileTitleColumn(dtList);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.xlsStr.Append(exception.Message);
            }
        }

        public void XlsHtmlData()
        {
            try
            {
                int num;
                int num2;
                int num3;
                if ((this.ContentParams.ContentDataSet != null) && (this.ContentParams.ContentDataSet.Tables.Count > 0))
                {
                    for (num = 0; num < this.ContentParams.ContentDataSet.Tables.Count; num++)
                    {
                        System.Data.DataTable table = this.ContentParams.ContentDataSet.Tables[num];
                        if (table.Rows.Count > 0)
                        {
                            ArrayList list = new ArrayList();
                            foreach (DataColumn column in table.Columns)
                            {
                                list.Add(column.ColumnName.ToString());
                            }
                            if ((this.ContentParams.HiddenColumn != null) && (this.ContentParams.HiddenColumn.Count > 0))
                            {
                                num2 = 0;
                                while (num2 < this.ContentParams.HiddenColumn.Count)
                                {
                                    if (list.Contains(this.ContentParams.HiddenColumn[num2]))
                                    {
                                        list.Remove(this.ContentParams.HiddenColumn[num2]);
                                    }
                                    num2++;
                                }
                            }
                            string sort = string.Empty;
                            if (((this.ContentParams.DataSortColumn != null) && (this.ContentParams.DataSortColumn.Count > 0)) && !this.ContentParams.DataSortMode.Equals(string.Empty))
                            {
                                StringBuilder builder = new StringBuilder();
                                num2 = 0;
                                while (num2 < this.ContentParams.DataSortColumn.Count)
                                {
                                    builder.Append(this.ContentParams.DataSortColumn[num2].ToString() + ",");
                                    num2++;
                                }
                                builder.Remove(builder.ToString().Length - 1, 1);
                                sort = builder.ToString() + " " + this.ContentParams.DataSortMode;
                            }
                            DataRow[] rowArray = null;
                            if (!sort.Equals(string.Empty))
                            {
                                rowArray = table.Select("", sort);
                            }
                            else
                            {
                                rowArray = table.Select("");
                            }
                            this.xlsStr.Append("<table border=1>");
                            num3 = 0;
                            foreach (DataRow row in rowArray)
                            {
                                if (this.ContentParams.Alternate)
                                {
                                    if ((num3 % 2) == 0)
                                    {
                                        this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                    }
                                    else
                                    {
                                        this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataSecondBGColor + ">");
                                    }
                                    num3++;
                                }
                                else
                                {
                                    this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                }
                                num2 = 0;
                                while (num2 < list.Count)
                                {
                                    if (!table.Columns[list[num2].ToString()].DataType.Name.ToLower().Equals("datetime"))
                                    {
                                        this.xlsStr.Append("<td sytle=\"mso-number-format:\\@\" aligh=" + this.ContentParams.DataAlign + " width=" + this.ContentParams.DataWidth.ToString() + ">");
                                    }
                                    else
                                    {
                                        this.xlsStr.Append("<td aligh=" + this.ContentParams.DataAlign + " width=" + this.ContentParams.DataWidth.ToString() + ">");
                                    }
                                    this.xlsStr.Append("<font face=\"" + this.ContentParams.DataFongFace + "\" size=" + this.ContentParams.DataFontSize.ToString() + " color=\"" + this.ContentParams.DataFontColor + "\">");
                                    this.xlsStr.Append("");
                                    this.xlsStr.Append(row[list[num2].ToString()].ToString());
                                    this.xlsStr.Append("</font>");
                                    this.xlsStr.Append("</td>");
                                    num2++;
                                }
                                this.xlsStr.Append("</tr>");
                            }
                            this.xlsStr.Append("</table>");
                        }
                    }
                }
                if (this.AppendRows.Count > 0)
                {
                    for (num = 0; num < this.AppendRows.Count; num++)
                    {
                        this.xlsStr.Append("<table border=1>");
                        ArrayList list2 = (ArrayList) this.AppendRows[num];
                        if ((list2 != null) && (list2.Count > 0))
                        {
                            num3 = 0;
                            if (this.ContentParams.Alternate)
                            {
                                if ((num3 % 2) == 0)
                                {
                                    this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                }
                                else
                                {
                                    this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataSecondBGColor + ">");
                                }
                                num3++;
                            }
                            else
                            {
                                this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                            }
                            for (num2 = 0; num2 < list2.Count; num2++)
                            {
                                if (list2[num2].GetType().Name.Equals("cXlsColumn"))
                                {
                                    cXlsColumn column2 = (cXlsColumn) list2[num2];
                                    this.xlsStr.Append(string.Concat(new object[] { "<td colspan=", column2.Colspan, " rowspan=", column2.Rowspan, " bgcolor=\"", column2.BGColor, "\" align=", column2.Align, ">" }));
                                    this.xlsStr.Append(string.Concat(new object[] { "<font face=\"", column2.FontFace, "\" color=\"", column2.FontColor, "\" size=", column2.FontSize, ">" }));
                                    this.xlsStr.Append(column2.Text);
                                    this.xlsStr.Append("</font>");
                                    this.xlsStr.Append("</td>");
                                }
                            }
                            this.xlsStr.Append("</tr>");
                        }
                        this.xlsStr.Append("</table>");
                    }
                }
            }
            catch (Exception exception)
            {
                this.xlsStr.Append(exception.Message);
            }
        }

        public void XlsHtmlTitle()
        {
            try
            {
                if ((this.TitleParams.TitleDataSet != null) && (this.TitleParams.TitleDataSet.Tables.Count > 0))
                {
                    System.Data.DataTable table = this.TitleParams.TitleDataSet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow[] rowArray = null;
                        if (this.IsSortColumn)
                        {
                            rowArray = table.Select(this.TitleParams.TitleParentIdColName + "='" + this.TitleParams.TitleParentIdDefaultValue + "'", this.TitleParams.TitleSortColName + " ASC");
                        }
                        else
                        {
                            rowArray = table.Select(this.TitleParams.TitleParentIdColName + "='" + this.TitleParams.TitleParentIdDefaultValue + "'");
                        }
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            this.InitVariable();
                            this.MaxRows(this.TitleParams.TitleParentIdDefaultValue);
                            if (!this.TitleParams.TitleHeaderName.Equals(string.Empty))
                            {
                                this.ColsCount(this.TitleParams.TitleParentIdDefaultValue);
                                if (this.ColSpan > 0)
                                {
                                    this.xlsStr.Append("<table border = 0>");
                                    this.xlsStr.Append("<tr height=" + this.TitleParams.TitleHeaderHeight.ToString() + ">");
                                    this.xlsStr.Append("<td align=center colspan=" + Convert.ToString(this.ColSpan) + ">");
                                    this.xlsStr.Append("<font face=\"" + this.TitleParams.TitleHeaderFontFace + "\" ");
                                    this.xlsStr.Append("size=" + this.TitleParams.TitleHeaderFontSize.ToString() + " ");
                                    this.xlsStr.Append("color=" + this.TitleParams.TitleHeaderFontColor + ">");
                                    this.xlsStr.Append(this.TitleParams.TitleHeaderName);
                                    this.xlsStr.Append("</font></td></tr></table>");
                                }
                                this.InitVariable();
                            }
                            this.xlsStr.Append("<table border=1>");
                            ArrayList dtList = new ArrayList();
                            foreach (DataRow row in rowArray)
                            {
                                dtList.Add(row[this.TitleParams.TitleIdColName].ToString());
                            }
                            this.MakeHtmlTitleColumn(dtList);
                            this.xlsStr.Append("</table>");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.xlsStr.Append(exception.Message);
            }
        }

        public void PrintHtmlData()
        {
            try
            {
                if ((this.TitleParams.TitleDataSet != null) && (this.TitleParams.TitleDataSet.Tables.Count > 0))
                {
                    System.Data.DataTable table = this.TitleParams.TitleDataSet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow[] rowArray = null;
                        if (this.IsSortColumn)
                        {
                            rowArray = table.Select(this.TitleParams.TitleParentIdColName + "='" + this.TitleParams.TitleParentIdDefaultValue + "'", this.TitleParams.TitleSortColName + " ASC");
                        }
                        else
                        {
                            rowArray = table.Select(this.TitleParams.TitleParentIdColName + "='" + this.TitleParams.TitleParentIdDefaultValue + "'");
                        }
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            this.InitVariable();
                            this.MaxRows(this.TitleParams.TitleParentIdDefaultValue);
                            if (!this.TitleParams.TitleHeaderName.Equals(string.Empty))
                            {
                                this.ColsCount(this.TitleParams.TitleParentIdDefaultValue);
                                if (this.ColSpan > 0)
                                {
                                    this.xlsStr.Append("<table border = 0>");
                                    this.xlsStr.Append("<tr height=" + this.TitleParams.TitleHeaderHeight.ToString() + ">");
                                    this.xlsStr.Append("<td align=center colspan=" + Convert.ToString(this.ColSpan) + ">");
                                    this.xlsStr.Append("<font face=\"" + this.TitleParams.TitleHeaderFontFace + "\" ");
                                    this.xlsStr.Append("size=" + this.TitleParams.TitleHeaderFontSize.ToString() + " ");
                                    this.xlsStr.Append("color=" + this.TitleParams.TitleHeaderFontColor + ">");
                                    this.xlsStr.Append(this.TitleParams.TitleHeaderName);
                                    this.xlsStr.Append("</font></td></tr></table>");
                                }
                                this.InitVariable();
                            }
                            this.xlsStr.Append("<table border=1>");
                            ArrayList dtList = new ArrayList();
                            foreach (DataRow row in rowArray)
                            {
                                dtList.Add(row[this.TitleParams.TitleIdColName].ToString());
                            }
                            this.MakeHtmlTitleColumn(dtList);
                            int num;
                            int num2;
                            int num3;
                            if ((this.ContentParams.ContentDataSet != null) && (this.ContentParams.ContentDataSet.Tables.Count > 0))
                            {
                                for (num = 0; num < this.ContentParams.ContentDataSet.Tables.Count; num++)
                                {
                                    System.Data.DataTable table2 = this.ContentParams.ContentDataSet.Tables[num];
                                    if (table2.Rows.Count > 0)
                                    {
                                        ArrayList list = new ArrayList();
                                        foreach (DataColumn column in table2.Columns)
                                        {
                                            list.Add(column.ColumnName.ToString());
                                        }
                                        if ((this.ContentParams.HiddenColumn != null) && (this.ContentParams.HiddenColumn.Count > 0))
                                        {
                                            num2 = 0;
                                            while (num2 < this.ContentParams.HiddenColumn.Count)
                                            {
                                                if (list.Contains(this.ContentParams.HiddenColumn[num2]))
                                                {
                                                    list.Remove(this.ContentParams.HiddenColumn[num2]);
                                                }
                                                num2++;
                                            }
                                        }
                                        string sort = string.Empty;
                                        if (((this.ContentParams.DataSortColumn != null) && (this.ContentParams.DataSortColumn.Count > 0)) && !this.ContentParams.DataSortMode.Equals(string.Empty))
                                        {
                                            StringBuilder builder = new StringBuilder();
                                            num2 = 0;
                                            while (num2 < this.ContentParams.DataSortColumn.Count)
                                            {
                                                builder.Append(this.ContentParams.DataSortColumn[num2].ToString() + ",");
                                                num2++;
                                            }
                                            builder.Remove(builder.ToString().Length - 1, 1);
                                            sort = builder.ToString() + " " + this.ContentParams.DataSortMode;
                                        }
                                        DataRow[] rowArray2 = null;
                                        if (!sort.Equals(string.Empty))
                                        {
                                            rowArray2 = table2.Select("", sort);
                                        }
                                        else
                                        {
                                            rowArray2 = table2.Select("");
                                        }
                                        num3 = 0;
                                        foreach (DataRow row in rowArray2)
                                        {
                                            if (this.ContentParams.Alternate)
                                            {
                                                if ((num3 % 2) == 0)
                                                {
                                                    this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                                }
                                                else
                                                {
                                                    this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataSecondBGColor + ">");
                                                }
                                                num3++;
                                            }
                                            else
                                            {
                                                this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                            }
                                            num2 = 0;
                                            while (num2 < list.Count)
                                            {
                                                if (!table2.Columns[list[num2].ToString()].DataType.Name.ToLower().Equals("datetime"))
                                                {
                                                    this.xlsStr.Append("<td sytle=\"mso-number-format:\\@\" aligh=" + this.ContentParams.DataAlign + " width=" + this.ContentParams.DataWidth.ToString() + ">");
                                                }
                                                else
                                                {
                                                    this.xlsStr.Append("<td aligh=" + this.ContentParams.DataAlign + " width=" + this.ContentParams.DataWidth.ToString() + ">");
                                                }
                                                this.xlsStr.Append("<font face=\"" + this.ContentParams.DataFongFace + "\" size=" + this.ContentParams.DataFontSize.ToString() + " color=\"" + this.ContentParams.DataFontColor + "\">");
                                                this.xlsStr.Append(row[list[num2].ToString()].ToString());
                                                this.xlsStr.Append("</font>");
                                                this.xlsStr.Append("</td>");
                                                num2++;
                                            }
                                            this.xlsStr.Append("</tr>");
                                        }
                                    }
                                }
                            }
                            if (this.AppendRows.Count > 0)
                            {
                                for (num = 0; num < this.AppendRows.Count; num++)
                                {
                                    ArrayList list2 = (ArrayList)this.AppendRows[num];
                                    if ((list2 != null) && (list2.Count > 0))
                                    {
                                        num3 = 0;
                                        if (this.ContentParams.Alternate)
                                        {
                                            if ((num3 % 2) == 0)
                                            {
                                                this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                            }
                                            else
                                            {
                                                this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataSecondBGColor + ">");
                                            }
                                            num3++;
                                        }
                                        else
                                        {
                                            this.xlsStr.Append("<tr height=" + this.ContentParams.DataHeight.ToString() + " bgcolor=" + this.ContentParams.DataFirstBGColor + ">");
                                        }
                                        for (num2 = 0; num2 < list2.Count; num2++)
                                        {
                                            if (list2[num2].GetType().Name.Equals("cXlsColumn"))
                                            {
                                                cXlsColumn column2 = (cXlsColumn)list2[num2];
                                                this.xlsStr.Append(string.Concat(new object[] { "<td colspan=", column2.Colspan, " rowspan=", column2.Rowspan, " bgcolor=\"", column2.BGColor, "\" align=", column2.Align, ">" }));
                                                this.xlsStr.Append(string.Concat(new object[] { "<font face=\"", column2.FontFace, "\" color=\"", column2.FontColor, "\" size=", column2.FontSize, ">" }));
                                                this.xlsStr.Append(column2.Text);
                                                this.xlsStr.Append("</font>");
                                                this.xlsStr.Append("</td>");
                                            }
                                        }
                                        this.xlsStr.Append("</tr>");
                                    }
                                }
                            }
                            this.xlsStr.Append("</table>");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.xlsStr.Append(exception.Message);
            }
        }
    }
}

