using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accounts.Web.mobile.AccountsManage
{
    public partial class DealList : BasePage
    {
        public static string Menu = string.Empty;
        public static string userid = string.Empty;
        public static int CountNum = 0; //总记录数
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                BindControl();
                hiduserid.Value = GetLoginUser().Userid;
                Menu = GetMenu(hiduserid.Value, 1);
            }
        }

        public string Where
        {
            get
            {
                string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId ='5' and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30", GetLoginUser().Userid);
                where += " and isFinish=0";
                return where;
            }
        }

        private void BindControl()
        {
            Order.Items.Clear();
            Order.Items.Add(new ListItem("交易时间降序", ""));
            //Order.Items.Add(new ListItem("总资产从高到低", "1"));
            //Order.Items.Add(new ListItem("总资产从低到高", "2"));
            //Order.Items.Add(new ListItem("现金从高到低", "3"));
            //Order.Items.Add(new ListItem("现金从低到高", "4"));
            Order.Items.Add(new ListItem("收支为正", "5"));
            Order.Items.Add(new ListItem("收支为负", "6"));
            Order.Items.Add(new ListItem("收支为总资产", "7"));
            Order.Items.Add(new ListItem("收支为现金", "8"));
            Order.Items.Add(new ListItem("交易时间升序", "9"));
        }
    }
}