using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;

namespace Accounts.Web.AccountManage
{
    public partial class AccountTemplateDetail : BasePage
    {

        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");

            if (!Page.IsPostBack && GetLoginUser() != null)
            { 
                FieldBind();
                if (id>0) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定扩展字段=============================
        private void FieldBind()
        {
            BLL.FormField bll = new BLL.FormField();


            DataTable dt = bll.GetList(0, "is_sys=1 or (Creator='" + GetLoginUser().Userid + "' and is_sys=0)", "is_sys asc,sort_id asc").Tables[0];

            this.cblField.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.cblField.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            ISelectDataSourceFace select=new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select * from AccountTemplate where id=" + _id;
            DataTable dt = select.ExecuteDataSet().Tables[0];

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["name"].ToString();
                txtSortId.Text = dt.Rows[0]["sort_id"].ToString();
                    txtRemark.Text= dt.Rows[0]["remark"].ToString();
                List<string> fields= dt.Rows[0]["fields"].ToString().Split(',').ToList();
                foreach (ListItem item in cblField.Items)
                {
                    if (fields.Exists(p => p == item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "不存在该账单模板！", "success"), true);
            }

        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            string fields = string.Empty;
            foreach (ListItem item in cblField.Items)
            {
                if(item.Selected)
                {
                    fields += string.Format("{0},", item.Value);
                }
            }
            fields = fields.TrimEnd(',');
            IInsertDataSourceFace insert=new InsertSQL("AccountTemplate");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("name",txtName.Text);
            insert.AddFieldValue("fields", fields);
            insert.AddFieldValue("sort_id", txtSortId.Text);
            insert.AddFieldValue("remark", txtRemark.Text);
            insert.AddFieldValue("is_sys", 0);
            insert.AddFieldValue("is_account", 0);
            insert.AddFieldValue("Creator", GetLoginUser().Userid);
            if (insert.ExecuteNonQuery() > 0)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            string fields = string.Empty;
            foreach (ListItem item in cblField.Items)
            {
                if (item.Selected)
                {
                    fields += string.Format("{0},", item.Value);
                }
            }
            fields = fields.TrimEnd(',');
            IUpdateDataSourceFace update = new UpdateSQL("AccountTemplate");
            update.DataBaseAlias = "common";
            update.AddFieldValue("name", txtName.Text);
            update.AddFieldValue("fields", fields);
            update.AddFieldValue("sort_id", txtSortId.Text);
            update.AddFieldValue("remark", txtRemark.Text);
            update.AddWhere("id", _id);
            if (update.ExecuteNonQuery() > 0)
            {
                result = true;
            }
            return result;
        }
        #endregion




        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (id>0) //修改
            {
                if (!DoEdit(this.id))
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "保存过程中发生错误！", "fail");

                }
                else
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "修改账单分类成功！", "success");
                }
            }
            else //添加
            {
                if (!DoAdd())
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "保存过程中发生错误！", "fail");
                }
                else
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "添加账单分类成功！", "success");
                }
            }
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        }

    }
}