using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accounts.BLL.Login;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL;
using System.Text;
using work.MD5Hash;

namespace Accounts.Web
{
    public partial class _Default : BasePage
    {
        public string MenuData
        {
            get;
            set;
        }

        public string RoleData
        {
            get;
            set;
        }

        public string NameData
        {
            get;
            set;
        }

        public string MsgData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("type")))
                {
                    hidtype.Value = DTRequest.GetQueryString("type");
                }
                if (Session["User"] != null)
                {
                    Accounts.Model.View_Users item = Session["User"] as Accounts.Model.View_Users;
                    LoginBll loginbll = new LoginBll();
                    MenuData = loginbll.MenuString(item.Userid);
                    RoleData = loginbll.RoleString(item.Userid);
                    NameData = item.UserRealName;
                    hftip.Value = "0";
                    hiduserid.Value = GetLoginUser().Userid;
                    hidusername.Value = GetLoginUser().LoginName;
                    //MsgData = GetMsg();
                    //if (MsgData.Trim().Length > 0)
                    //{
                    //    hftip.Value = "1";
                    //}
                }
                else
                {
                    //Response.Write("<script language=\"javascript\">alert('登录已失效或没有登录，请登录！');window.parent.location.href='/Login.aspx';</script>");
                    Response.Redirect("/Login.aspx");
                }

            }
        }


        public string Where
        {
            get
            {
                string where = string.Format("Where a.AccountMonth='{0}' and b.IsDelete=0", DateTime.Now.ToString("yyyyMM"));

                if (GetLoginUser().LoginName.ToLower().IndexOf("admin") > -1)
                {
                    //管理员统计全部
                }
                //else if (CheckAuthority("AccountsScheduleList.aspx?type=Audit", "audit"))
                //{
                //    where += string.Format(" and b.Creator={0}", GetLoginUser().Userid);
                //}
                //else if (CheckAuthority("AccountsScheduleList.aspx?type=Add", "add"))
                //{
                //    where += string.Format(" and b.OrgId={0} ",
                //        new BLL.Com_Organization().GetModel(GetLoginUser().Userid).Id);
                //}
                //else
                //{
                //    where += " and b.OrgId=0 ";
                //}

                return where;
            }
        }

        private string GetMsg()
        {
            int count = 0;  //账单总数
            int jdwb = 0;   //进度未报
            int jdws = 0;   //进度未审
            int jzhm = 0;   //进展缓慢
            int wwc = 0;    //未完成
            int zztj = 0;   //正在推进
            int ywc = 0;    //已完成
            int wjz = 0;    //无进展

            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT  a.Id*1 as Id ,
                                                b.Id as AId,
                                                b.Creator,
                                                b.Status as Status
                                        FROM    dbo.Account_Schedule a
                                                LEFT JOIN dbo.View_Account b ON a.AccountId = b.Id
                                        {0}", Where);
            select.CommandText = sql;
            DataTable dtAccounts = select.ExecuteDataSet().Tables[0];

            count = dtAccounts.Rows.Count;

            if (GetLoginUser().LoginName.ToLower().IndexOf("admin") > -1)
                jdwb = dtAccounts.Select("Status = 'Pass' or Status = 'NotReported' ").Count();
            //else if (new LoginBll().RoleString(GetLoginUser().Userid).IndexOf("账单监督员") > -1)
            //    jdwb = dtAccounts.Select(" Creator='"+GetLoginUser().Userid+"' and ( (ScheduleInfo ='' or Status = 'Pass') or Status = 'NotReported') ").Count();
            else if (CheckAuthority("AccountsScheduleList.aspx?type=Add", "add"))
                jdwb = dtAccounts.Select("and Status = 'Pass' or Status = 'NotReported') ").Count();
            jdws = dtAccounts.Select(" Status = 'ScheduleAudit' ").Count();
            jzhm = dtAccounts.Select(" Status = 'SlowProgress' ").Count();
            wwc = dtAccounts.Select("  Status = 'NotCompleted' ").Count();
            zztj = dtAccounts.Select(" Status = 'Advancing' ").Count();
            ywc = dtAccounts.Select("  Status = 'Completed'").Count();
            wjz = dtAccounts.Select("  Status = 'NoProgress' ").Count();

            string msg = string.Format(@"<ul class='MsgUL'>
                                            <li>共有账单【<a href=""javascript:AddTab('/AccountsManage/MyAccountsList.aspx','账单列表')""><font class='red'>{0}</font></a>】项</li>
                                        </ul>
                                        <div class='dd'></div>
                                        <ul class='MsgUL'>
                                            <li>进度未报【<a href=""javascript:AddTab('/AccountsManage/AccountsScheduleList.aspx?type=Add&statusIdx=1','账单列表')""><font class='red'>{1}</font></a>】项</li>
                                            <li>进度未审【<a href=""javascript:AddTab('/AccountsManage/AccountsScheduleList.aspx?type=Audit&statusIdx=6','账单列表')""><font class='red'>{2}</font></a>】项</li>
                                        </ul>
                                        <div class='dd'></div>
                                        <ul class='MsgUL'>
                                            <li>进展缓慢【<a href=""javascript:AddTab('/AccountsManage/MyAccountsList.aspx?statusIdx=4','账单列表')""><font class='red'>{3}</font></a>】项</li>
                                            <li>未完成&nbsp;&nbsp;&nbsp;【<a href=""javascript:AddTab('/AccountsManage/MyAccountsList.aspx?statusIdx=3','账单列表')""><font class='red'>{4}</font></a>】项</li>
                                            <li>正在推进【<a href=""javascript:AddTab('/AccountsManage/MyAccountsList.aspx?statusIdx=5','账单列表')""><font class='red'>{5}</font></a>】项</li>
                                            <li>已完成&nbsp;&nbsp;&nbsp;【<a href=""javascript:AddTab('/AccountsManage/MyAccountsList.aspx?statusIdx=2','账单列表')""><font class='red'>{6}</font></a>】项</li>
                                        </ul>", count, jdwb, jdws, jzhm, wwc, zztj, ywc);
            return msg;
        }
    }
    
}