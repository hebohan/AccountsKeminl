using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.SessionState;
using System.Data;
using Inspur.Finix.DAL.SQL;
using work.MD5Hash;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// UsersHandler 的摘要说明
    /// </summary>
    public class UsersHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Accounts.BLL.View_Users bll = new Accounts.BLL.View_Users();
            if (context.Request.QueryString["type"] == "edit")//获取要编辑的用户信息
            { 
                string Userid=context.Request.QueryString["Id"];
                ISelectDataSourceFace select =new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText =string.Format(
                    "SELECT a.Id,a.RolesName FROM dbo.Tb_Roles a JOIN dbo.Tb_RolesAddUser b ON a.Id=b.RolesId WHERE b.UserId='{0}'",Userid);
                DataSet ds = select.ExecuteDataSet();
                string IdList = "";
                foreach (DataRow dr in ds.Tables[0].Rows) {
                    if (IdList != "")
                        IdList += ",";
                    IdList += dr["Id"].ToString() ;
                }
                context.Response.Write(IdList);  
            }

            else if (context.Request.QueryString["type"] == "register")//用户注册
            {
                string login = context.Request.QueryString["login"];
                string pass = context.Request.QueryString["pass"];
                string name = context.Request.QueryString["name"];
                string sex = context.Request.QueryString["sex"];
                string email = context.Request.QueryString["email"];
                string tel = context.Request.QueryString["tel"];
                string mobile = context.Request.QueryString["mobile"];
                Accounts.BLL.Com_UserInfos bll1 = new Accounts.BLL.Com_UserInfos();
                Accounts.BLL.Com_UserLogin bll2 = new Accounts.BLL.Com_UserLogin();
                Accounts.Model.Com_UserInfos item1 = new Accounts.Model.Com_UserInfos();
                Accounts.Model.Com_UserLogin model = new Accounts.Model.Com_UserLogin();
                string Userid = bll1.GetMaxId("Userid", "Com_UserLogin").ToString();
                string Sort = bll1.GetMaxId("Sort", "Com_UserInfos").ToString();
                if (Userid == "1")
                {
                    Userid = "1000000000";
                }
                item1.Sort = Sort;
                item1.AddUser = Userid;
                item1.AddDate = DateTime.Now;
                item1.Email = email;
                item1.Mobile = mobile;
                item1.Sex = sex;
                item1.Tel = tel;
                item1.Userid = Userid;
                item1.UserRealName = name;
                model.Status = 1;
                model.LoginName = login;
                model.LoginPassword = MD5up.MD5(pass);
                model.UserId = Userid;
                if (bll2.Exists(login))
                {
                    context.Response.Write("false");
                }
                else
                {
                    bll1.Add(item1);
                    bll2.Add(model);
                }
                saveRole(Userid, "8"); //普通用户
            }
            else if (context.Request.QueryString["type"] == "del")//删除用户信息，同时删除用户角色表，用户部门表
            {
                string Userid = context.Request.QueryString["Id"];
                string sql = "delete Com_UserInfos where Userid='"+Userid+"'";
                string sql2 = "delete Com_UserLogin where Userid='" + Userid + "'";
                string sql4 = "delete Tb_RolesAddUser where UserId='" + Userid + "'";
                List<string> list = new List<string>();
                list.Add(sql);
                list.Add(sql2);
                list.Add(sql4);
                if (Accounts.DBUtility.DbHelperSQL.ExecuteSqlTran(list) > 0)
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }

            else if (context.Request.QueryString["type"] == "checklogin")//找回密码检查用户名是否存在
            {
                string loginname = context.Request.QueryString["loginname"];
                string sql = "select LoginName from Com_UserLogin where LoginName='" + loginname + "'";
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = sql;
                if (select.ExecuteDataSet().Tables[0].Rows.Count>0)
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }

            else if (context.Request.QueryString["type"] == "pass")//管理员修改密码
            {
                string Userid = context.Request.QueryString["UserId"];
                string pass = context.Request.QueryString["pass"];
                Accounts.BLL.Com_UserLogin bll2 = new Accounts.BLL.Com_UserLogin();
                Accounts.Model.Com_UserLogin model = new Accounts.Model.Com_UserLogin();
                model = bll2.GetModel(Userid);
                model.LoginPassword = MD5up.MD5(pass);
                if (bll2.UpdatePass(model))
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
            else if (context.Request.QueryString["type"] == "user_pass")//用户修改密码
            {
                string oldpass = context.Request.QueryString["oldpassword"];
                string newpass = context.Request.QueryString["newpass"];
                Accounts.BLL.Com_UserLogin bll2 = new Accounts.BLL.Com_UserLogin();
                Accounts.Model.Com_UserLogin model = new Accounts.Model.Com_UserLogin();
                model = bll2.GetModel(new BLL.BasePage().GetLoginUser().Userid);
                if (model.LoginPassword == MD5up.MD5(oldpass)){
                    model.LoginPassword = MD5up.MD5(newpass);
                    if (bll2.UpdatePass(model))
                    {
                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }
                else
                {
                    context.Response.Write("false");
                }
                
                
            }
            else if (context.Request.QueryString["type"] == "sendcode")
            {
                string loginname = context.Request.QueryString["loginname"];
                string email = context.Request.QueryString["email"];
                Accounts.BLL.Com_UserLogin bll2 = new Accounts.BLL.Com_UserLogin();
                string userid = bll2.GetUserId_Validate(loginname, email);
                if (userid == null)
                {
                    context.Response.Write("false");
                }
                else
                {
                    string randomnum = GetRandomString(6, 1, "");
                    string title = "账单管理系统-找回密码";
                    string content = string.Format(@"<div >尊敬的<font color='blue'>{0}</font></div>
                                <div style='padding-top:20px'>&nbsp;&nbsp;&nbsp;&nbsp;您申请找回密码的验证码为<font color='red'>{1}</font>，请不要泄漏给他人，若不是您本人操作，请检查帐号安全。</div><div style='padding-top:20px;'>账单管理系统&nbsp;&nbsp;{2}</div>", loginname, randomnum, string.Format("{0:yyyy年MM月dd日}", DateTime.Now));
                    new BLL.BasePage().SendMail(email, title, content);
                    //InsertHisMailRecord(email, title, content,"1");
                    context.Response.Write(randomnum);
                }
            }
            else if (context.Request.QueryString["type"] == "registercode")
            {
                try
                {
                    string email = context.Request.QueryString["email"];
                    ISelectDataSourceFace select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.CommandText = string.Format(@"select Email from Com_UserInfos where Email='{0}'", email);
                    if (select.ExecuteDataSet().Tables[0].Rows.Count == 0)
                    {
                        string randomnum = GetRandomString(6, 1, "");
                        string title = "账单管理系统-邮箱验证";
                        string content = string.Format(@"<div >尊敬的用户</div>
                                <div style='padding-top:20px'>&nbsp;&nbsp;&nbsp;&nbsp;您正在申请注册账单管理系统，本次验证码为<font color='red'>{0}</font>，请不要泄漏给他人，验证码有效时间1分钟。</div><div style='padding-top:20px;'>账单管理系统&nbsp;&nbsp;{1}</div>", randomnum, string.Format("{0:yyyy年MM月dd日}", DateTime.Now));
                        new BLL.BasePage().SendMail(email, title, content);
                        //InsertHisMailRecord(email, title, content, "1");
                        context.Response.Write(randomnum);
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }
                catch(Exception e){
                    context.Response.Write("error");
                }
                
               
            }
            else if (context.Request.QueryString["type"] == "forgetpass")//忘记密码_修改密码
            {
                string loginname = context.Request.QueryString["loginname"];
                string email = context.Request.QueryString["email"];
                string pass = context.Request.QueryString["pass"];
                Accounts.BLL.Com_UserLogin bll2 = new Accounts.BLL.Com_UserLogin();
                Accounts.Model.Com_UserLogin model = new Accounts.Model.Com_UserLogin();

                string userid = bll2.GetUserId_Validate(loginname, email);
                if (userid == null)
                {
                    context.Response.Write("false");
                }
                else
                {
                    model = bll2.GetModel(userid);
                    model.LoginPassword = MD5up.MD5(pass);
                    if (bll2.UpdatePass(model))
                    {
                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }

            }
            else if (context.Request.QueryString["type"] == "save")//保存编辑信息
            {
                string login = context.Request.QueryString["login"];
                string name = context.Request.QueryString["name"];
                string sex = context.Request.QueryString["sex"];
                string email = context.Request.QueryString["email"];
                string tel = context.Request.QueryString["tel"];
                string mobile = context.Request.QueryString["mobile"];
                string status = context.Request.QueryString["status"];
                string role = context.Request.QueryString["role"];
                string sort = context.Request.QueryString["sort"];
                Accounts.BLL.Com_UserInfos bll1 = new Accounts.BLL.Com_UserInfos();
                Accounts.BLL.Com_UserLogin bll2 = new Accounts.BLL.Com_UserLogin();
                Accounts.Model.Com_UserInfos item1 = new Accounts.Model.Com_UserInfos();
                Accounts.Model.Com_UserLogin model = new Accounts.Model.Com_UserLogin();
                string Userid = context.Request.QueryString["Userid"];

                item1 = bll1.GetModel(Userid);
                model = bll2.GetModel(Userid);

                item1.Email = email;
                item1.Mobile = mobile;
                item1.Sex = sex;
                item1.Tel = tel;
                item1.Sort = sort;
                item1.Userid = Userid;
                item1.UserRealName = name;

                model.LoginName = login;
                model.Status = int.Parse(status);
                model.UserId = Userid;
                bll1.Update(item1);
                bll2.Update(model);
                saveRole(Userid, role);
            }
        }
        public bool saveRole(string Userid, string role)
        {
            List<string> list = new List<string>();
            string sql2 = "delete Tb_RolesAddUser where UserId='" + Userid + "'";
            list.Add(sql2);
            if (role != null && role != "")
            {
                string[] str = role.Split(',');
                foreach (string s in str)
                {
                    string sql = "insert into Tb_RolesAddUser(RolesId,UserId) values(" + s + ",'" + Userid + "')";
                    list.Add(sql);
                }
            }
            if (Accounts.DBUtility.DbHelperSQL.ExecuteSqlTran(list) > 0)
                return true;
            else
                return false;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">随机字符串中英文字符个数</param>
        /// <param name="count">需要获取的个数</param>
        /// <param name="separator">分隔符</param>
        static string GetRandomString(int length, int count, string separator)
        {
            char[] Chars = { '0','1','2','3','4','5','6','7','8','9' };
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            string yymmdd = DateTime.Now.ToString("yyMMdd");
            for (int i = 0; i < count; i++)
            {

                for (int j = 0; j < length; j++)
                {
                    sb.Append(Chars[rd.Next(0, Chars.Length)]);
                }
                if (i < count - 1)
                {
                    sb.Append(separator);
                }
            }
            return sb.ToString();
        }

        public void InsertHisMailRecord(string receiver, string title, string content,string status)
        {
            IInsertDataSourceFace insert = new InsertSQL("History_Mail");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("receiver", receiver);
            insert.AddFieldValue("title", title);
            insert.AddFieldValue("content", content);
            insert.AddFieldValue("addtime", DateTime.Now);
            insert.AddFieldValue("IsSend", status);
            insert.ExecuteNonQuery();
        }
    }
}