using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL;

namespace Accounts.Web.DictionaryManage
{
    public partial class DictionaryItemDetail : BasePage
    {
        private string _dictionaryid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dictionaryid = Request.QueryString["dictionaryid"];
            if (!IsPostBack && GetLoginUser() != null)
            {
                parentid.Value = Request.QueryString["parentid"].ToString().Trim();
                if (!string.IsNullOrEmpty(_dictionaryid))
                    BindControl();
            }
        }

        private void BindControl()
        {
            tb_code.ReadOnly = true;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select *
                                                   from Dictionary
                                                  where DicId = '{0}'", _dictionaryid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                tb_name.Text = row["DicName"].ToString();
                tb_code.Text = row["DicCode"].ToString();
                tb_decride.Value = row["Describe"].ToString();
                tb_Order.Text = row["OrderNum"].ToString();
                ddlSys.SelectedValue = row["Is_sys"].ToString();
                ddlCatalog.SelectedValue = row["IsCatalog"].ToString();
                if (row["IsCatalog"].ToString() == "False")
                {
                    tb_value.Text = row["Value"].ToString();
                }
                else
                {
                    valueTR.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (ExsitCode(tb_code.Text.Trim()))
            {
                msg = string.Format("JsPrint('{0}','{1}')", "存在相同代码的字典项！", "fail");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                return;
            }
            if (tb_code.Text.Trim() == "BookMaxNum" && tb_value.Text.Trim() == "1")
            {
                msg = string.Format("JsPrint('{0}','{1}')", "最大预约个数不能为1，请输入大于1的数字！", "fail");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                return;
            }
            if (!string.IsNullOrEmpty(_dictionaryid))//修改
            {
                IUpdateDataSourceFace update = new UpdateSQL();
                update.DataBaseAlias = "common";
                update.UpdateTable = "Dictionary";
                update.AddFieldValue("DicName", tb_name.Text.Trim());
                update.AddFieldValue("DicCode", tb_code.Text.Trim());
                update.AddFieldValue("Describe", tb_decride.Value.Trim());
                update.AddFieldValue("OrderNum", tb_Order.Text.Trim());
                update.AddFieldValue("Value", ddlCatalog.SelectedValue == "False" ? tb_value.Text.Trim() : "");
                update.AddFieldValue("Is_sys", ddlSys.SelectedValue);
                update.AddFieldValue("IsCatalog", ddlCatalog.SelectedValue);
                update.AddWhere("DicId", _dictionaryid);
                int i = update.ExecuteNonQuery();
                if (i > 0)
                    msg = string.Format("JsPrint('{0}','{1}')", "字典项更新成功！", "success");
                else
                    msg = string.Format("JsPrint('{0}','{1}')", "学字典项更新失败！", "fail");
            }
            else
            {
                IInsertDataSourceFace insert = new InsertSQL("Trainees");
                insert.DataBaseAlias = "common";
                insert.InsertTable = "Dictionary";
                insert.AddFieldValue("DicName", tb_name.Text.Trim());
                insert.AddFieldValue("DicCode", tb_code.Text.Trim());
                insert.AddFieldValue("Describe", tb_decride.Value.Trim());
                insert.AddFieldValue("OrderNum", tb_Order.Text.Trim());
                insert.AddFieldValue("Value", ddlCatalog.SelectedValue=="False"?tb_value.Text.Trim():"");
                insert.AddFieldValue("Is_sys", ddlSys.SelectedValue);
                insert.AddFieldValue("IsCatalog", ddlCatalog.SelectedValue);
                insert.AddFieldValue("Creator", new BLL.BasePage().GetLoginUser().Userid);
                insert.AddFieldValue("ParentId", parentid.Value);
                int i = insert.ExecuteNonQuery();
                if (i > 0)
                    msg = string.Format("JsPrint('{0}','{1}')", "字典项新增成功！", "success");
                else
                    msg = string.Format("JsPrint('{0}','{1}')", "字典项新增失败！", "fail");
            }
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        }

        public bool ExsitCode(string code)
        {
            bool flag = false;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format("select count(0) from Dictionary where DicCode='{0}' ",code);
            if (!string.IsNullOrEmpty(_dictionaryid))
            {
                sql += string.Format("  and DicId!={0}", _dictionaryid);
            }
            select.CommandText = sql;
            object objnum = select.ExecuteScalar();
            if (objnum != null)
            {
                int num = Convert.ToInt32(objnum);
                if (num > 0) flag = true;
            }
            return flag;
        }

        protected void ddlCatalog_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCatalog.SelectedValue == "True")
            {
                valueTR.Visible = false;
            }
            else
            {
                valueTR.Visible = true;
            }
        }
    }
}