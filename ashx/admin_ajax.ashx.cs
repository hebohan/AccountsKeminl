using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.SessionState;
using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using Inspur.Finix.ExceptionManagement;
using System.IO;
using System.Text;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {

                case "form_field_validate": //验证表单字段是否重复
                    form_field_validate(context);
                    break;
                case "import":
                    import(context);
                    break;
                case "sumtext":
                    sumtext(context);
                    break;
            }
        }


        #region 验证表单字段是否重复============================
        private void form_field_validate(HttpContext context)
        {
            string column_name = DTRequest.GetString("param");
            if (string.IsNullOrEmpty(column_name))
            {
                context.Response.Write("{ \"info\":\"名称不可为空\", \"status\":\"n\" }");
                return;
            }
            BLL.FormField bll = new BLL.FormField();
            if (bll.Exists(column_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            string[] flag = { "fid", "id", "TempId", "Creator", "CreateTime", "Account_Status", "StatusName", "IsDelete", "isFinish", "IsExcel", "AccountId", "AccountMonth", "Remark", "FinishTime", "cash", "total_money", "FinishTime", "cash", };
            List<string> list = new List<string>(flag);
            if (list.Contains(column_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion



        #region 上传Excel文档导入账单数据============================
        /// <summary>
        /// 上传Excel文档导入账单数据
        /// </summary>
        /// <param name="context"></param>
        public void import(HttpContext context)
        {
            if (new BasePage().GetLoginUser() == null)
            {
                context.Response.Write("{ \"status\": \"0\", \"msg\":\"用户登录信息失效，无法获取用户信息！\"}");
                return;
            }
            Model.View_Users user = new BasePage().GetLoginUser();
            string filename = DTRequest.GetString("filename");
            int tempid = DTRequest.GetInt("tempid", 0);
            string path = context.Server.MapPath("~") + "Excel\\Account\\" + filename;
            try
            {
                DataTable dt = ExcelHelper.ExcelToDataTable(path);
                //int failbind = 0;
                int fail = 0;
                int total = dt.Rows.Count;
                string faillist = string.Empty;

                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.SelectFromTable("AccountTemplate");
                select.SelectColumns("name", "fields");
                select.AddWhere("id", tempid);
                DataTable obj = select.ExecuteDataSet().Tables[0];
                if (obj.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(obj.Rows[0]["fields"].ToString()))
                    {
                        BLL.FormField bll = new BLL.FormField();
                        DataSet ds = bll.GetList(0, string.Format(" id in ({0})", obj.Rows[0]["fields"]), "id asc");
                        List<Model.FormField> heads = bll.DataTableToList(ds.Tables[0]);
                        if (heads.Count > 0)
                        {
                            Model.FormField field0 = new Model.FormField();
                            field0.title = "序号";
                            heads.Add(field0);
                            ExceptionManager.Handle(new Exception(string.Format("模板导入：heads.Count:{0},dt.Columns.Count:{1}", heads.Count, dt.Columns.Count)));
                            if (heads.Count != dt.Columns.Count)
                            {
                                context.Response.Write("{ \"status\": \"0\", \"msg\":\"导入的Excel模板和所选的账单模板不匹配！\"}");
                                return;
                            }
                            foreach (DataColumn col in dt.Columns)
                            {
                                ExceptionManager.Handle(new Exception(string.Format("模板导入：col.ColumnName:{0} && col.ColumnName == p.title,第{1}列", col.ColumnName, dt.Columns.IndexOf(col))));
                                if (!heads.Exists(p => ((col.ColumnName == "序号" && col.ColumnName == p.title) || col.ColumnName == (p.title + "#" + p.name))))
                                {
                                    ExceptionManager.Handle(new Exception(string.Format("异常出在第{0}列", dt.Columns.IndexOf(col))));
                                    context.Response.Write("{ \"status\": \"0\", \"msg\":\"导入的Excel模板和所选的账单模板不匹配！\"}");
                                    return;
                                }
                            }
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["序号"] == null || dr["序号"] == DBNull.Value || string.IsNullOrEmpty(dr["序号"].ToString()))
                                {
                                    total--;
                                    continue;
                                }
                                IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
                                try
                                {
                                    bool iscontinue = true;
                                    
                                    BLL.Com_UserInfos userbll = new BLL.Com_UserInfos();
                                    //if (olist.Count == 1)
                                    //{

                                        #region 增加账单基本信息

                                        IInsertDataSourceFace insert = new InsertSQL("Account");
                                        insert.DataBaseAlias = "common";
                                        insert.Transaction = tran;
                                        insert.AddFieldValue("TempId", tempid);
                                        //insert.AddFieldValue("Account_Status", "待收款");
                                        insert.AddFieldValue("IsDelete", 0);
                                        insert.AddFieldValue("IsExcel", 1);
                                        insert.AddFieldValue("Creator", user.Userid);
                                        insert.AddFieldValue("CreateTime", DateTime.Now);
                                        insert.ExecuteNonQuery();

                                        select = new SelectSQL();
                                        select.DataBaseAlias = "common";
                                        select.Transaction = tran;
                                        select.CommandText = "SELECT @@IDENTITY";
                                        object nid = select.ExecuteScalar();

                                        insert = new InsertSQL("FormContent");
                                        insert.DataBaseAlias = "common";
                                        insert.Transaction = tran;
                                        insert.AddFieldValue("info_id", nid);
                                        string _money = "0";
                                        foreach (Model.FormField item in heads)
                                        {
                                            if (!string.IsNullOrEmpty(item.name))
                                            {
                                                object objv = dr[item.title + "#" + item.name];
                                                if (item.data_type == "int" || item.data_type == "tinyint")
                                                {
                                                    if (!string.IsNullOrEmpty(objv.ToString()))
                                                    {
                                                        int num = 0;
                                                        if (!int.TryParse(objv.ToString(), out num))
                                                        {
                                                            iscontinue = false;
                                                            break;
                                                        }
                                                    }else if (item.is_required == 1)
                                                    {
                                                        iscontinue = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        objv = 0;
                                                    }
                                                }
                                                else if (item.data_type == "datetime")
                                                {
                                                    if (!string.IsNullOrEmpty(objv.ToString()))
                                                    {
                                                        DateTime dtime = new DateTime();
                                                        if (!DateTime.TryParse(objv.ToString(), out dtime))
                                                        {
                                                            iscontinue = false;
                                                            break;
                                                        }
                                                    }
                                                    else if (item.is_required == 1)
                                                    {
                                                        iscontinue = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        objv = DBNull.Value;
                                                    }
                                                }
                                                else if (item.data_type.StartsWith("decimal"))
                                                {
                                                    if (!string.IsNullOrEmpty(objv.ToString()))
                                                    {
                                                        decimal dd = 0;
                                                        if (!decimal.TryParse(objv.ToString(), out dd))
                                                        {
                                                            iscontinue = false;
                                                            break;
                                                        }
                                                    }
                                                    else if (item.is_required == 1)
                                                    {
                                                        iscontinue = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        objv = 0;
                                                    }
                                                }
                                                else if (item.control_type == "single-user")
                                                {
                                                    if (!string.IsNullOrEmpty(objv.ToString()))
                                                    {
                                                        List<Model.Com_UserInfos> userlist =
                                                            userbll.GetModelList(string.Format("UserRealName='{0}'",
                                                                objv.ToString().Trim()));
                                                        if (userlist != null && userlist.Count == 1)
                                                        {
                                                            objv = userlist[0].Userid;
                                                        }
                                                        else
                                                        {
                                                            iscontinue = false;
                                                            break;
                                                        }
                                                    }
                                                    else if (item.is_required == 1)
                                                    {
                                                        iscontinue = false;
                                                        break;
                                                    }
                                                }
                                                else if (item.control_type == "multi-user")
                                                {
                                                    if (!string.IsNullOrEmpty(objv.ToString()))
                                                    {
                                                        string[] strs =
                                                            objv.ToString().Trim().Split(new char[] {',', '，', '、'});
                                                        string temp = "";
                                                        foreach (string str in strs)
                                                        {
                                                            List<Model.Com_UserInfos> userlist =
                                                                userbll.GetModelList(string.Format("UserRealName='{0}'",
                                                                    str.Trim()));
                                                            if (userlist != null && userlist.Count == 1)
                                                            {
                                                                objv = userlist[0].Userid + ",";
                                                            }
                                                            else
                                                            {
                                                                iscontinue = false;
                                                                break;
                                                            }
                                                        }
                                                        objv = temp.TrimEnd(',');
                                                    }
                                                    else if (item.is_required == 1)
                                                    {
                                                        iscontinue = false;
                                                        break;
                                                    }
                                                }
                                                
                                                insert.AddFieldValue(item.name, objv);
                                                if (item.name == "ds_money")
                                                {
                                                    _money = objv.ToString();
                                                }
                                                if (item.name == "status")
                                                {
                                                    string[] flag = { "already_getmoney", "already_repay", "already_deposit", "bad_pay" };
                                                    List<string> list = new List<string>(flag);
                                                    IUpdateDataSourceFace update = new UpdateSQL("Account");
                                                    update.DataBaseAlias = "common";
                                                    update.Transaction = tran;
                                                    update.AddFieldValue("Account_Status", objv.ToString());
                                                    update.AddFieldValue("StatusName", new Accounts.BLL.BasePage().GetDicName(objv.ToString()));
                                                    if (list.Contains(objv.ToString()))
                                                    {
                                                        if(_money != "0")
                                                        {
                                                            if (objv.ToString() == "already_repay" || objv.ToString() == "bad_pay")
                                                            {
                                                                new BLL.BasePage().UpdateCash("cash", _money, "-", user.Userid, "Excel导入还款账单", "-1", 0, "special");
                                                            }
                                                            else
                                                            {
                                                                new BLL.BasePage().UpdateCash("cash", _money, "+", user.Userid, "Excel导入收款账单", "-1", 0, "special");
                                                            }
                                                            
                                                        }
                                                        IInsertDataSourceFace _insert = new InsertSQL("Account_Schedule");
                                                        _insert.DataBaseAlias = "common";
                                                        _insert.Transaction = tran;
                                                        _insert.AddFieldValue("FinishTime", DateTime.Now);
                                                        _insert.AddFieldValue("AccountId", nid);
                                                        _insert.AddFieldValue("Creator", new BLL.BasePage().GetLoginUser().Userid);
                                                        _insert.AddFieldValue("AccountMonth",string.Format("{0:yyyyMM}", DateTime.Now));
                                                        _insert.ExecuteNonQuery();

                                                        update.AddFieldValue("isFinish", "1");
                                                    }
                                                    else
                                                    {
                                                        update.AddFieldValue("isFinish", "0");
                                                    }
                                                    update.AddWhere("Id", nid);
                                                    update.ExecuteNonQuery();
                                                }
                                                
                                            }
                                        }


                                        insert.ExecuteNonQuery();

                                        insert = new InsertSQL("Account_Log");
                                        insert.DataBaseAlias = "common";
                                        insert.Transaction = tran;
                                        insert.AddFieldValue("AccountId", nid);
                                        insert.AddFieldValue("Actor", user.Userid);
                                        insert.AddFieldValue("Action", "Import");
                                        insert.AddFieldValue("Opinion", "");
                                        insert.AddFieldValue("ActTime", DateTime.Now);
                                        insert.ExecuteNonQuery();


                                        #endregion
                                    //}
                                    //else
                                    //{
                                        //iscontinue = false;
                                    //}
                                    if (!iscontinue)
                                    {
                                        tran.Rollback();
                                        fail++;
                                        faillist += dr["序号"].ToString() + ",";
                                    }
                                    else
                                    {
                                        tran.Commit();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Handle(ex);
                                    tran.Rollback();
                                    fail++;
                                    faillist += dr["序号"].ToString() + ",";
                                }
                            }
                            string msg = string.Format("总共执行导入了{0}条账单信息，其中{1}条账单信息导入成功，{2}条账单信息导入失败。{3}！", total, total - fail, fail, fail > 0 ? string.Format("导入失败的账单序号分别是{0},原因可能是数据和字段的类型不匹配或者单位人员未找到，请仔细检查后再从新导入", faillist.TrimEnd(',')) : "");
                            if (total > 0)
                            {
                                if (fail == 0)
                                {
                                    msg = string.Format("全部导入成功，总共是{0}条账单信息。", total);
                                }
                            }
                            else
                            {
                                msg = "在导入的Excel文档中不存在账单信息，不执行导入。";
                            }
                            context.Response.Write("{ \"status\": \"1\", \"msg\":\"" + msg + "\"}");
                        }
                        else
                        {
                            context.Response.Write("{ \"status\": \"0\", \"msg\":\"所选账单模板未定义字段！\"}");
                        }
                    }
                    else
                    {
                        context.Response.Write("{ \"status\": \"0\", \"msg\":\"所选账单模板未定义字段！\"}");
                    }
                }
                else
                {
                    context.Response.Write("{ \"status\": \"0\", \"msg\":\"不存在所选账单模板！\"}");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Handle(ex);
                context.Response.Write("{ \"status\": \"0\", \"msg\":\"导入数据出错，请联系管理员！\"}");
            }
        }
        #endregion


        #region 获取上传的文本
        public void sumtext(HttpContext context)
        {
            try
            {
                string filename = DTRequest.GetString("filename");
                string path = context.Server.MapPath("~") + "Excel\\Account\\" + filename;
                int countnum = 0;
                int sum = 0;  //总数
                string numstring = string.Empty;
                int i = 0;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        try
                        {
                            countnum = Convert.ToInt32(line);
                        }
                        catch
                        {
                            context.Response.Write("{ \"status\": \"0\", \"msg\":\"导入数据出错-->(输入正整数个数有误)！\"}");
                        }
                    }
                    else if (i == 1)
                    {
                        numstring = line;
                    }
                    i++;
                }
                string[] nump = numstring.Split(' ');
                for (int j = 0; j < countnum; j++)
                {
                    sum += Convert.ToInt32(nump[j]);
                }
                context.Response.Write("{ \"status\": \"1\", \"msg\":\"导入数据成功,总和是:<font color='red'>" + sum + "</font>\"}");
            }
            catch
            {
                context.Response.Write("{ \"status\": \"0\", \"msg\":\"导入数据出错-->(程序处理过程出错)！\"}");
            }

        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}