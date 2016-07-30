using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using System.Net;
using work.MD5Hash;
using System.Configuration;
using Accounts.BLL.Common;
using Accounts.BLL;

namespace Accounts.Web.AccountsManage
{
    public partial class PayRemark : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack && GetLoginUser() != null)
           {
               if (!string.IsNullOrEmpty(DTRequest.GetQueryString("orderid")))
               {
                   string orderid = DTRequest.GetQueryString("orderid");
                   if (CheckAuthority(GetLoginUser().Userid,orderid))
                   {
                       DataTable dt = GetPayRecord(orderid);
                       hidname.Value = dt.Rows[0]["PayRemark"].ToString();
                       hidrecordid.Value = orderid;
                       this.remark.InnerHtml = dt.Rows[0]["UserRemark"].ToString();
                   }
                   else
                   {
                       Response.Write("<script>alert('这不是您的交易记录哦！');window.opener = null;window.open('', '_self');window.close();</script>");
                   }
               }
               else
               {
                   Response.Write("<script>alert('参数错误！');window.opener = null;window.open('', '_self');window.close();</script>");
               }
           }
        }

        public DataTable GetPayRecord(string recordid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select PayRemark,UserRemark from History_Money where id='{0}'", recordid);
            return select.ExecuteDataSet().Tables[0];
        }

        public bool CheckAuthority(string userid,string recordid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select id from History_Money where Creator='{0}' and id='{1}'", userid,recordid);
            if (select.ExecuteDataSet().Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}