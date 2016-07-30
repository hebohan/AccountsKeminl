using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accounts.BLL;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL.Login;
using Accounts.BLL.Common;
using Accounts.Model;
using System.Text;

namespace Accounts.Web.AccountManage
{
    public partial class AccountList : BasePage
    {
        public string fieldcontent = "";
        public string fieldhead = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {

                    BindControl();
                    if (!string.IsNullOrEmpty(DTRequest.GetString("atype")))
                    {
                        account_type.SelectedValue = DTRequest.GetString("atype");
                    }
                    BindDataSource(1);
                    

            }
        }

        private void BindControl()
        {
            //绑定帐号类型
            account_type.Items.Clear();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select id,name from AccountTemplate where is_account=0 and Creator='" + GetLoginUser().Userid + "' order by sort_id";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                account_type.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
            }


            //绑定账单状态
            Status.Items.Clear();
            Status.Items.Add(new ListItem("全部", ""));
            ISelectDataSourceFace select1 = new SelectSQL();
            select1.DataBaseAlias = "common";
            select1.CommandText = "select DicName,DicCode from Dictionary where ParentId = '89' and (Is_Sys=1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys=0))";
            DataTable dt1 = select1.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Status.Items.Add(new ListItem(dt1.Rows[i]["DicName"].ToString(), dt1.Rows[i]["DicCode"].ToString()));
            }
            

        }

        public string Where
        {
            get
            {
                string where = "and IsDelete=0 and Creator='" + GetLoginUser().Userid + "'";
                if (!string.IsNullOrEmpty(account_type.SelectedValue))
                {

                    where += string.Format(" and TempId={0}", account_type.SelectedValue);
                }
                //if (!string.IsNullOrEmpty(account_name.Value))
                //{
                //    where += string.Format(" and zd_name like '%{0}%'", account_name.Value.Trim());
                //}
                if (!string.IsNullOrEmpty(Status.SelectedValue))
                {
                    where += string.Format(" and (status = '{0}' or  zh_status = '{0}' or Account_Status = '{0}')", Status.SelectedValue);
                }
                return where;
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex)
        {
            fieldcontent = "";
            fieldhead="";
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT * FROM View_Account where TempId <> '5' {0}", Where);

            if (!string.IsNullOrEmpty(account_name.Value))
            {
                sql += " and (";
                List<Model.FormField> ls = GetTemplateFields(account_type.SelectedValue);
                for(int count=0;count < ls.Count;count++)
                {
                    Model.FormField modelt = ls[count];
                    if (modelt != null){
                        if (modelt.control_type.ToString().IndexOf("text") >= 0)
                        {
                            sql += string.Format(" {1} like '%{0}%' or", account_name.Value.Trim(), modelt.name);
                        }
                    }
                }
                sql = sql.Substring(0, sql.Length - 2) +")";
            }

            select.CommandText = sql;
            select.AddOrderBy("CreateTime", Sort.Ascending);
            DataSet ds = select.ExecuteDataSet(WebPager1.PageSize, pageIndex);
            DataTable dt = ds.Tables[0];
            hidtype.Value = account_type.SelectedValue;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                List<Model.FormField> ls = GetTemplateFields(dt.Rows[i]["TempId"].ToString());
                for(int count=0;count < ls.Count;count++)
                {   
                    Model.FormField modelt = ls[count];
                    if (modelt != null)
                    {
                            if(count==0)
                            {
                                if (i == 0)
                                {
                                    fieldhead += string.Format(@"
                                    <th scope='col' style='width:5%;'>
                                        <p onclick='checkClick(ListData);' style='cursor: hand'>全选</p>
                                    </th><th scope='col' style='width:5%;white-space:nowrap;'>序号</th>");
                                }
                                fieldcontent += string.Format(@"
                                    <tr class='list_grid_row' align='center'>
                                    <td>
                                        <input type='checkbox' id='chkbox' name='chkbox' value='{0}' />
                                    </td>
                                    <td align='center' style='width:5%;white-space:nowrap;'>
                                        <span id='ListData_ctl02_xuhao'>{1}</span>
                                    </td>
                                ", dt.Rows[i]["id"].ToString(), (WebPager1.PageSize * (WebPager1.CurrentPageIndex - 1))+i+1);
                            }
                            if(i==0)
                            {
                                fieldhead += string.Format(@"<th scope='col' style='{0}white-space:nowrap;'>{1}</th>", modelt.display_width == 0 ? "" : "width:" + modelt.display_width + "%;", modelt.title);
                            }
                            fieldcontent += string.Format(@"
                                <td align='center' white-space:nowrap;'>
                                    {0}
                                </td>
                            ", (modelt.control_type == "date" || modelt.control_type == "datetime") ? (string.Format("{0:yyyy-MM-dd}".ToString(), Convert.ToDateTime(modelt.control_type == "number" ? "<font color='orange' style='font-weight:bold;'>" + (modelt.name.ToString().IndexOf("status") > 0 ? new Accounts.BLL.BasePage().GetDicName(dt.Rows[i][modelt.name.Trim()].ToString()) : dt.Rows[i][modelt.name.Trim()].ToString()) + "</font>" : (modelt.name.ToString().IndexOf("status") > 0 ? new Accounts.BLL.BasePage().GetDicName(dt.Rows[i][modelt.name.Trim()].ToString()) : dt.Rows[i][modelt.name.Trim()].ToString())))) : (modelt.control_type == "number" ? "<font color='orange' style='font-weight:bold;'>" + (modelt.name.ToString().IndexOf("status") > 0 ? new Accounts.BLL.BasePage().GetDicName(dt.Rows[i][modelt.name.Trim()].ToString()) : dt.Rows[i][modelt.name.Trim()].ToString()) + "</font>" : (modelt.name.ToString().IndexOf("status") > 0 ? new Accounts.BLL.BasePage().GetDicName(dt.Rows[i][modelt.name.Trim()].ToString()) : dt.Rows[i][modelt.name.Trim()].ToString())));
                    }
                }
                if (i == 0)
                {
                    fieldhead += string.Format(@"<th scope='col' style='width:10%;white-space:nowrap;'>操作</th>");
                }
                
                fieldcontent += string.Format(@"
                                <td align='center' style='width:10%;white-space:nowrap;'>
                                    <a href='../AccountsManage/AccountRegisterDetail.aspx?action=edit&id={0}&tempid={1}&type=0&atype={2}'>修改</a>
                                    <a href='AccountDetail.aspx?id={0}&tempid={1}'>查看</a>
                                </td>
			            </tr>", dt.Rows[i]["id"].ToString(), dt.Rows[i]["TempId"].ToString(), hidtype.Value);
            }

            fieldcontent = fieldcontent == "" ? "<td colspan='99' style='text-align:center'>未找到相关记录<td>" : fieldcontent;

            #region 得到总条目数
            select = new SelectSQL();
            select.DataBaseAlias = "common";
            sql = string.Format(@"  SELECT count(0) FROM View_Account where TempId <> '5' {0}", Where);
            select.CommandText = sql;
            int CountNum = select.ExecuteCount();
            this.WebPager1.RecordCount = CountNum;
            RecordCount.InnerText = string.Format("共{0}条", CountNum);
            if (WebPager1.PageCount <= 1)
            {
                RecordCount.Visible = false;
            }
            #endregion
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WebPager1_PageChanged(object sender, EventArgs e)
        {
            BindDataSource(WebPager1.CurrentPageIndex);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string[] deleIds = hdelid.Value.Split(',');
            IUpdateDataSourceFace delete = null;
            foreach (string deleId in deleIds)
            {
                delete = new UpdateSQL("Account");
                delete.DataBaseAlias = "common";
                delete.AddWhere("Id", deleId);
                delete.AddFieldValue("IsDelete", 1);
                delete.Transaction = tran;
                delete.ExecuteNonQuery();
            }
            tran.Commit();
            BindDataSource(WebPager1.CurrentPageIndex);
        }

        protected string GetTemplateName(int tempid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("AccountTemplate");
            select.SelectColumn("name");
            select.AddWhere("id", tempid);
            object obj = select.ExecuteScalar();
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }

        protected void btnSearch_OnServerClick(object sender, EventArgs e)
        {
            WebPager1.CurrentPageIndex = 1;
            BindDataSource(1);
        }

        /**/
        /// <summary>
        /// 根据索引和pagesize返回记录
        /// </summary>
        /// <param name="dt">记录集 DataTable</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="pagesize">一页的记录数</param>
        /// <returns></returns>
        public DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
        #region 获取模板字段
        /// <summary>
        /// 获取模板字段
        /// </summary>
        /// <returns></returns>
        private List<Model.FormField> GetTemplateFields(string id)
        {
            List<Model.FormField> ls = new List<Model.FormField>();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("AccountTemplate");
            select.SelectColumn("fields");
            select.AddWhere("id", id);
            object obj = select.ExecuteScalar();

            if (obj != null)
            {
                List<string> fields = obj.ToString().Split(',').ToList();
                if (fields.Count > 0)
                {
                    foreach (string field in fields)
                    {
                        ls.Add(new BLL.FormField().GetModel(int.Parse(field.Trim())));
                    }
                }
            }
            return ls;
        }

        #endregion
    }
}