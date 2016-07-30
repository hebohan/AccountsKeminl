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

namespace Accounts.Web.mobile.ReminderManage
{
    public partial class ReminderList : BasePage
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
            ReminderType.Items.Clear();
            ReminderType.Items.Add(new ListItem("全部", ""));
            ReminderType.Items.Add(new ListItem("待提醒", "1"));
            ReminderType.Items.Add(new ListItem("已提醒", "2"));

            Order.Items.Clear();
            Order.Items.Add(new ListItem("提醒时间升序", "1"));
            Order.Items.Add(new ListItem("提醒时间降序", "2"));
            Order.Items.Add(new ListItem("创建时间升序", "3"));
            Order.Items.Add(new ListItem("创建时间降序", "4"));
        }
    }
}