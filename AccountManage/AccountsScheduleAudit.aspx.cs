using Accounts.BLL;
using Accounts.BLL.Common;
using Accounts.Model;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using View_Users = Accounts.BLL.View_Users;
using Accounts.BLL.Login;

namespace Accounts.Web.AccountManage
{
    public partial class AccountsScheduleAudit : BasePage
    {
        private int id = 0;
        private int tempid = 0;
        public int temp_num = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");
            tempid = DTRequest.GetQueryInt("tempid");
            if (!IsPostBack)
            {
                    if (id > 0)
                    {
                        if (CheckAuthority(id))
                        {
                            hidid.Value = id.ToString();
                            BindControl();
                            BindData();
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "参数传递不正确，请勿错误操作！", "error"), true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint",
                            string.Format("JsPrint('{0}','{1}')", "参数不对！", "error"), true);
                    }
                }
        }

        private void BindControl()
        {
            

            //绑定账单状态
            ddlStatus.Items.Clear();
            ISelectDataSourceFace select1 = new SelectSQL();
            select1.DataBaseAlias = "common";
            select1.CommandText = "select DicName,DicCode from Dictionary where ParentId = '89'";
            DataTable dt1 = select1.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                ddlStatus.Items.Add(new ListItem(dt1.Rows[i]["DicName"].ToString(), dt1.Rows[i]["DicCode"].ToString()));
            }
        }


        private void BindData()
        {
            temp_num = IsExist_Schedule(hidid.Value.ToString());
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format("Select * from View_Account where Id={0}", id);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                //detail.Value = dr["zd_detail"].ToString();
                //month.Text = string.Format("{0:yyyyMM}", dr["CreateTime"]);
                logger.Text = dr["Creator"]!=DBNull.Value?new View_Users().GetModel(dr["Creator"].ToString()).UserRealName:"";
                logtime.Text = dr["CreateTime"].ToString();
                if (!string.IsNullOrEmpty(dr["Account_Status"].ToString()))
                {
                    ddlStatus.SelectedValue = dr["Account_Status"].ToString();
                }
                if (temp_num > 0)
                {
                    zd_remark.Value = dr["Remark"].ToString();
                }
                //hidaid.Value = dr["AccountId"].ToString();
            }
            else
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "进度不存在!", "error");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            temp_num = IsExist_Schedule(hidid.Value.ToString());
            if (temp_num == 0)
            {
                IInsertDataSourceFace insert = new InsertSQL("Account_Schedule");
                insert.DataBaseAlias = "common";
                insert.AddFieldValue("AccountId", hidid.Value.Trim());
                //insert.AddFieldValue("AccountMonth", month.Text.ToString());
                insert.AddFieldValue("Remark", zd_remark.Value.ToString());
                insert.AddFieldValue("Creator", GetLoginUser().Userid);
                insert.AddFieldValue("FinishTime", DateTime.Now);
                insert.ExecuteNonQuery();
            }
            else
            {
                IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                update1.DataBaseAlias = "common";
                update1.AddFieldValue("Remark", zd_remark.Value.ToString());
                update1.AddWhere("AccountId", hidid.Value.Trim());
                update1.ExecuteNonQuery();
            }
            

            IUpdateDataSourceFace update = new UpdateSQL("Account");
            update.DataBaseAlias = "common";
            update.AddFieldValue("Account_Status", ddlStatus.SelectedValue);
            update.AddFieldValue("StatusName", GetDicName(ddlStatus.SelectedValue));
            update.AddWhere("Id", hidid.Value.Trim());
            update.ExecuteNonQuery();

            update = new UpdateSQL("FormContent");
            update.DataBaseAlias = "common";
            //update.AddFieldValue("zd_detail", detail.Value.ToString());
            update.AddFieldValue("zh_status", ddlStatus.SelectedValue);
            update.AddWhere("info_id", hidid.Value.Trim());
            update.ExecuteNonQuery();


            string msg = string.Format("JsPrint('{0}','{1}')", "认定成功！", "save");
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        }

        protected int IsExist_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select id  from Account_Schedule where AccountId ='"+id.ToString()+"'");
            select.CommandText = sql;
            int temp_num = select.ExecuteDataSet().Tables[0].Rows.Count;
            return temp_num;
        }

        public bool CheckAuthority(int accountid)
        {

            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select * from View_Account where id='{0}' and Creator='{1}'", accountid, GetLoginUser().Userid);
            if (select.ExecuteDataSet().Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}