using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Accounts.BLL;

namespace Accounts.Web.SysSetting
{
    public partial class NavigationRole : BasePage
    {
        protected string strMap = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                try{
                    string RoleId = Request.QueryString["RoleId"];
                    Accounts.BLL.Tb_Roles bll = new Accounts.BLL.Tb_Roles();
                    Accounts.Model.Tb_Roles model = bll.GetModel(int.Parse(RoleId));
                    lblRole.Text = model.RolesName;
                    HdId.Value = RoleId;
                    BindDataList();
                }catch{
                    Response.Redirect("RolesList.aspx");
                }
            }
        }
        private void BindDataList()
        {
            //根据OrderId,NodeCode asc 排序获得的数据记录集
            List<Accounts.Model.Tb_Navigation> list = new List<Accounts.Model.Tb_Navigation>();
            Accounts.BLL.Tb_Navigation bll = new Accounts.BLL.Tb_Navigation();
            List<Accounts.Model.Tb_Navigation> list1 = bll.GetModelList(" ParentId=0");
            //对原有的数据进行重新排序
            foreach (Accounts.Model.Tb_Navigation model in list1)
            {
                if (strMap == "")
                    strMap = "0";
                else strMap += ",0";
                list.Add(model);
                int i = list.Count;
                List<Accounts.Model.Tb_Navigation> list2 = bll.GetModelList(" ParentId=" + model.Id);
                foreach (Accounts.Model.Tb_Navigation item in list2)
                {
                    strMap += "," + i;
                    list.Add(item);
                }
            }
            rptList.DataSource = list;
            rptList.DataBind();
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblShow = e.Item.FindControl("lblShow") as Label;
            if (lblShow != null)
            {
                if (lblShow.Text == "0")
                {
                    lblShow.Text = "<img src='../images/checked1.gif' alt='' />";
                }
                else
                {
                    lblShow.Text = "<img src='../images/checked2.gif' alt='' />";
                }
            }
        }
        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int Id = int.Parse(e.CommandArgument.ToString());
            Accounts.BLL.Tb_Navigation bll = new Accounts.BLL.Tb_Navigation();
            Accounts.Model.Tb_Navigation model = bll.GetModel(Id);
            string sql = "update Tb_Navigation set Sort=Sort-1 where ParentId=" + model.ParentId + " and Sort>" + model.Sort;
            Accounts.DBUtility.DbHelperSQL.ExecuteSql(sql);
            if (bll.Delete(Id))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>msgShow('系统提示', '菜单删除成功！', 'info');window.location.href='NavigationList.aspx';</script>");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>msgShow('系统提示', '菜单删除失败，请稍后重试！', 'error');</script>");
            }
        }
    }
}