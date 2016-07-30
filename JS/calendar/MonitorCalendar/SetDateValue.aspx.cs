using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inspur.Finix.Portal.js.calendar.MonitorCalendar
{
    public partial class SetDateValue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            this.monthsyear.Value = DateTime.Now.Year.ToString();
            this.currentyear.Value = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            this.month.Value = month;
        }
    }
}
