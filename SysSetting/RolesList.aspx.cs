using System;
using Accounts.BLL;
namespace Accounts.Web.SysSetting
{
    public partial class RolesList : BasePage
    {
        protected string strMap = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // dataBind();
            }
        }
        //protected void dataBind()
        //{
        //    Accounts.BLL.Tb_Roles bll = new Accounts.BLL.Tb_Roles();
        //    DataSet ds = bll.GetAllList();
        //    rptList.DataSource = ds;
        //    rptList.DataBind();
        //}
    }
}