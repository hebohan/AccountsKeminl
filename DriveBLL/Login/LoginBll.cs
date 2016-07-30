using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Accounts.DAL.Login;
using Inspur.Finix.DAL.SQL;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;

namespace Accounts.BLL.Login
{
    public class LoginBll
    {
        public string ValidateUser(string loginname, string password)
        {
            string flag = "false";
            DataTable dt = LoginDal.ValidateUser(loginname, password);
            
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "1")
                {
                    flag = "true";
                    Accounts.Model.View_Users item = new Accounts.Model.View_Users();
                    Accounts.BLL.View_Users vbll = new Accounts.BLL.View_Users();
                    item = vbll.GetModel(dt.Rows[0]["userId"].ToString());
                    HttpContext.Current.Session.Add("User", item);
                    SetLogin(dt.Rows[0]["userId"].ToString(),"success");

                }
                else if (dt.Rows[0]["Status"].ToString() == "0")
                {
                    flag = "unable";
                    SetLogin(dt.Rows[0]["userId"].ToString(), "unable");
                }
                else
                {
                    flag = "forbidden";
                    SetLogin(dt.Rows[0]["userId"].ToString(), "forbidden");
                }
                
            }
            else
            {
                flag = "false";
            }
            return flag;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string MenuString(string userId)
        {
            string str = "";
            DataTable parentDt = LoginDal.GetParentMenuTable(userId);
            for (int i = 0; i < parentDt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    str += string.Format("<div class=\"title01\" title=\"{0}\" data-options=\"iconCls:'icon-save',selected:false\" style=\"overflow-x: hidden;\"><ul>", parentDt.Rows[i]["MenuName"].ToString());
                }
                else
                {
                    str += string.Format("<div class=\"title01\" title=\"{0}\" data-options=\"iconCls:'icon-save'\" style=\"overflow-x: hidden;\"><ul>", parentDt.Rows[i]["MenuName"].ToString());
                }
                DataTable childDt = LoginDal.GetChildMenu(parentDt.Rows[i]["Id"].ToString(), userId);
                foreach (DataRow temp in childDt.Rows)
                {
                    str += string.Format("<li onclick=\"AddTab('{0}','{1}')\"><cite></cite><a>{1}</a><i></i></li>", temp["LinkAddress"].ToString(), temp["MenuName"].ToString());
                }
                str += "</ul> </div>";
            }
            return str;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string RoleString(string userId)
        {
            string str = "";
            DataTable roleDt = LoginDal.GetRoleTable(userId);
            foreach (DataRow item in roleDt.Rows)
            {
                str += item["RolesName"].ToString() + "|";
            }
            if (!string.IsNullOrEmpty(str))
                str = str.TrimEnd('|');
            return str;
        }

        public void SetLogin(string userid, string status)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            IInsertDataSourceFace insert = new InsertSQL("Com_LoginLog");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("Userid", userid);
            insert.AddFieldValue("LoginIP", ip);
            insert.AddFieldValue("LoginDate", DateTime.Now);
            insert.AddFieldValue("Status", status);
            insert.ExecuteNonQuery();

            IUpdateDataSourceFace update = new UpdateSQL("Com_UserLogin");
            update.DataBaseAlias = "common";
            update.AddFieldValue("LastLoginIP", ip);
            update.AddFieldValue("LastLoginDate", DateTime.Now);
            update.AddWhere("UserId", userid);
            update.ExecuteNonQuery();
        }

        //public string IpLocation(string ipAddress)
        //{
        //    string[] result;
        //    if (string.IsNullOrEmpty(ipAddress.Trim()))
        //    {
        //        return null;
        //    }
        //    WebClient client = new WebClient();
        //    client.Encoding = System.Text.Encoding.GetEncoding("GB2312");
        //    string url = "http://www.ip138.com/ips.asp";
        //    string post = "ip=" + ipAddress + "&action=2";
        //    client.Headers.Set("Content-Type", "application/x-www-form-urlencoded");
        //    string response = client.UploadString(url, post);

        //    string p = @"<li>参考数据二：(?<location>[^<>]+?)</li>";
        //    Match match = Regex.Match(response, p);
        //    string m_Location = match.Groups["location"].Value.Trim();
        //    result = m_Location.Split(' ');
        //    return result[0];
        //}
    }
}
