using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using Inspur.Finix.ExceptionManagement;
using FormField = Accounts.Model.FormField;
using System.Text.RegularExpressions;

namespace Accounts.Web.AccountsManage
{
    public partial class AccountRegisterDetail : BasePage
    {
        private int id = 0;
        private int tempid = 0;
        private int type = 0;
        protected string msg = string.Empty;
        public string action = string.Empty;
        public string userid = string.Empty;
        //页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            try
            {
                this.tempid = DTRequest.GetQueryInt("tempid");
                this.action = DTRequest.GetQueryString("action");
                userid = GetLoginUser().Userid;
                tid.Value = tempid.ToString();
                acid.Value = action.ToString();
                CreateField(); //动态生成相应的扩展字段
            }
            catch (Exception ex)
            {
                ExceptionManager.Handle(ex);
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "账单表单生成错误！", "error"), true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");
            tempid = DTRequest.GetQueryInt("tempid");
            action = DTRequest.GetQueryString("action");
            if (!IsPostBack)
            {
                if (id > 0)
                {
                    aid.Value = id.ToString();
                    BindData();
                }
                else
                {
                    tid.Value = tempid.ToString();
                }
            }
        }

        #region 创建表单字段=========================
        private void CreateField()
        {

            List<Model.FormField> ls = GetTemplateFields();
            foreach (Model.FormField modelt in ls)
            {
                if (modelt != null)
                {   

                    //创建一个dl标签
                    HtmlTableRow htmlDL = new HtmlTableRow();
                    HtmlTableCell htmlDT = new HtmlTableCell();
                    HtmlTableCell htmlDD = new HtmlTableCell();
                    htmlDT.InnerHtml = modelt.title + "：";
                    htmlDT.Attributes.Add("class", "ListSearchInput");
                    htmlDD.Attributes.Add("class", "inputtdline");

                    switch (modelt.control_type)
                    {
                        case "single-text": //单行文本
                            //创建一个TextBox控件
                            TextBox txtControl = new TextBox();
                            txtControl.ID = "field_control_" + modelt.name;
                            //CSS样式及TextMode设置
                            if (modelt.control_type == "single-text") //单行
                            {
                                txtControl.CssClass = "input normal";
                                //是否密码框
                                if (modelt.is_password == 1)
                                {
                                    txtControl.TextMode = TextBoxMode.Password;
                                }
                            }
                            else if (modelt.control_type == "multi-text") //多行
                            {
                                txtControl.CssClass = "input normal";
                                txtControl.TextMode = TextBoxMode.MultiLine;
                            }
                            else if (modelt.control_type == "number") //数字
                            {
                                txtControl.CssClass = "input small";
                            }
                            else if (modelt.control_type == "date") //日期
                            {
                                txtControl.CssClass = "input normal Wdate date";
                                //txtControl.ReadOnly = true;
                            }
                            else if (modelt.control_type == "datetime") //日期时间
                            {
                                txtControl.CssClass = "input normal Wdate datetime";
                                //txtControl.ReadOnly = true;
                            }
                            else if (modelt.control_type == "images" || modelt.control_type == "video") //图片视频
                            {
                                txtControl.CssClass = "input normal upload-path";
                            }
                            //设置默认值
                            txtControl.Text = modelt.default_value;
                            //验证提示信息
                            if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                            {
                                txtControl.Attributes.Add("tipmsg", modelt.valid_tip_msg);
                            }
                            //验证失败提示信息
                            if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                            {
                                txtControl.Attributes.Add("errormsg", modelt.valid_error_msg);
                            }
                            //验证表达式
                            if (!string.IsNullOrEmpty(modelt.valid_pattern))
                            {
                                txtControl.Attributes.Add("datatype", modelt.valid_pattern);
                                txtControl.Attributes.Add("sucmsg", " ");
                            }
                            else if (modelt.is_required == 1)
                            {
                                txtControl.Attributes.Add("datatype", "*");
                                txtControl.Attributes.Add("sucmsg", " ");
                            }
                            //创建一个Label控件
                            Label labelControl = new Label();
                            labelControl.CssClass = "Validform_checktip";
                            labelControl.Text = modelt.valid_tip_msg;

                            //将控件添加至DD中
                            htmlDD.Controls.Add(txtControl);
                            //如果是图片则添加上传按钮
                            if (modelt.control_type == "images")
                            {
                                HtmlGenericControl htmlBtn = new HtmlGenericControl("div");
                                htmlBtn.Attributes.Add("class", "upload-box upload-img");
                                htmlBtn.Attributes.Add("style", "margin-left:4px;");
                                htmlDD.Controls.Add(htmlBtn);
                            }
                            //如果是视频则添加上传按钮
                            if (modelt.control_type == "video")
                            {
                                HtmlGenericControl htmlBtn = new HtmlGenericControl("div");
                                htmlBtn.Attributes.Add("class", "upload-box upload-video");
                                htmlBtn.Attributes.Add("style", "margin-left:4px;");
                                htmlDD.Controls.Add(htmlBtn);
                            }
                            htmlDD.Controls.Add(labelControl);
                            break;
                        case "multi-text": //多行文本
                            goto case "single-text";
                        case "editor": //编辑器
                            HtmlTextArea txtTextArea = new HtmlTextArea();
                            txtTextArea.ID = "field_control_" + modelt.name;
                            txtTextArea.Attributes.Add("style", "visibility:hidden;");
                            //是否简洁型编辑器
                            if (modelt.editor_type == 1)
                            {
                                txtTextArea.Attributes.Add("class", "editor-mini");
                            }
                            else
                            {
                                txtTextArea.Attributes.Add("class", "editor");
                            }
                            txtTextArea.Value = modelt.default_value; //默认值
                            //验证提示信息
                            if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                            {
                                txtTextArea.Attributes.Add("tipmsg", modelt.valid_tip_msg);
                            }
                            //验证失败提示信息
                            if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                            {
                                txtTextArea.Attributes.Add("errormsg", modelt.valid_error_msg);
                            }
                            //验证表达式
                            if (!string.IsNullOrEmpty(modelt.valid_pattern))
                            {
                                txtTextArea.Attributes.Add("datatype", modelt.valid_pattern);
                                txtTextArea.Attributes.Add("sucmsg", " ");
                            }
                            else if (modelt.is_required == 1)
                            {
                                txtTextArea.Attributes.Add("datatype", "*");
                                txtTextArea.Attributes.Add("sucmsg", " ");
                            }
                            //创建一个Label控件
                            Label labelControl2 = new Label();
                            labelControl2.CssClass = "Validform_checktip";
                            labelControl2.Text = modelt.valid_tip_msg;
                            //将控件添加至DD中
                            htmlDD.Controls.Add(txtTextArea);
                            htmlDD.Controls.Add(labelControl2);
                            break;
                        case "images": //图片上传
                            goto case "single-text";
                        case "video": //视频上传
                            goto case "single-text";
                        case "number": //数字
                            goto case "single-text";
                        case "date": //日期
                            goto case "single-text";
                        case "datetime": //日期时间
                            goto case "single-text";
                        case "checkbox": //复选框
                            CheckBox cbControl = new CheckBox();
                            cbControl.ID = "field_control_" + modelt.name;
                            //默认值
                            if (modelt.default_value == "1")
                            {
                                cbControl.Checked = true;
                            }
                            HtmlGenericControl htmlDiv1 = new HtmlGenericControl("div");
                            htmlDiv1.Attributes.Add("class", "rule-single-checkbox");
                            htmlDiv1.Controls.Add(cbControl);
                            //将控件添加至DD中
                            htmlDD.Controls.Add(htmlDiv1);
                            if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                            {
                                //创建一个Label控件
                                Label labelControl3 = new Label();
                                labelControl3.CssClass = "Validform_checktip";
                                labelControl3.Text = modelt.valid_tip_msg;
                                htmlDD.Controls.Add(labelControl3);
                            }
                            break;
                        case "multi-radio": //多项单选
                            RadioButtonList rblControl = new RadioButtonList();
                            rblControl.ID = "field_control_" + modelt.name;
                            rblControl.RepeatDirection = RepeatDirection.Horizontal;
                            rblControl.RepeatLayout = RepeatLayout.Flow;
                            HtmlGenericControl htmlDiv2 = new HtmlGenericControl("div");
                            htmlDiv2.Attributes.Add("class", "rule-multi-radio");
                            htmlDiv2.Controls.Add(rblControl);
                            //赋值选项
                            string[] valArr = modelt.item_option.Split(new string[] {"\r\n", "\n"},
                                StringSplitOptions.None);
                            for (int i = 0; i < valArr.Length; i++)
                            {
                                string[] valItemArr = valArr[i].Split('|');
                                if (valItemArr.Length == 2)
                                {
                                    rblControl.Items.Add(new ListItem(valItemArr[0], valItemArr[1]));
                                }
                            }
                            rblControl.SelectedValue = modelt.default_value; //默认值
                            //创建一个Label控件
                            Label labelControl4 = new Label();
                            labelControl4.CssClass = "Validform_checktip";
                            labelControl4.Text = modelt.valid_tip_msg;
                            //将控件添加至DD中
                            htmlDD.Controls.Add(htmlDiv2);
                            htmlDD.Controls.Add(labelControl4);
                            break;
                        case "multi-checkbox": //多项多选
                            CheckBoxList cblControl = new CheckBoxList();
                            cblControl.ID = "field_control_" + modelt.name;
                            cblControl.RepeatDirection = RepeatDirection.Horizontal;
                            cblControl.RepeatLayout = RepeatLayout.Flow;
                            HtmlGenericControl htmlDiv3 = new HtmlGenericControl("div");
                            htmlDiv3.Attributes.Add("class", "rule-multi-checkbox");
                            htmlDiv3.Controls.Add(cblControl);
                            //赋值选项
                            string[] valArr2 = modelt.item_option.Split(new string[] {"\r\n", "\n"},
                                StringSplitOptions.None);
                            for (int i = 0; i < valArr2.Length; i++)
                            {
                                string[] valItemArr2 = valArr2[i].Split('|');
                                if (valItemArr2.Length == 2)
                                {
                                    cblControl.Items.Add(new ListItem(valItemArr2[0], valItemArr2[1]));
                                }
                            }
                            cblControl.SelectedValue = modelt.default_value; //默认值
                            //创建一个Label控件
                            Label labelControl5 = new Label();
                            labelControl5.CssClass = "Validform_checktip";
                            labelControl5.Text = modelt.valid_tip_msg;
                            //将控件添加至DD中
                            htmlDD.Controls.Add(htmlDiv3);
                            htmlDD.Controls.Add(labelControl5);
                            break;
                        case "dropdownlist":  //下拉列表
                            DropDownList dropControl = new DropDownList();
                            dropControl.ID = "field_control_" + modelt.name;
                            dropControl.Attributes.Add("class", "input normal");
                            //绑定状态
                            dropControl.Items.Clear();

                            #region 旧代码
                            //ISelectDataSourceFace select1 = new SelectSQL();
                            //select1.DataBaseAlias = "common";
                            //select1.CommandText = "select DicName,DicCode from Dictionary where ParentId = '80'";
                            //DataTable dt1 = select1.ExecuteDataSet().Tables[0];
                            //for (int i = 0; i < dt1.Rows.Count; i++)
                            //{
                            //    dropControl.Items.Add(new ListItem(dt1.Rows[i]["DicName"].ToString(), dt1.Rows[i]["DicCode"].ToString()));
                            //}
                            //foreach (string item_option in Regex.Split(modelt.item_option.Trim(), "\r\n", RegexOptions.IgnoreCase))
                            //{
                            //    string a = string.Empty;
                            //    string b = string.Empty;
                            //    int countnum = 1;
                            //    foreach (string item in item_option.ToString().Trim().Split('|'))
                            //    {
                            //        if(countnum % 2 == 1)
                            //        {
                            //            a = item.ToString();
                            //        }
                            //        else
                            //        {
                            //            b = item.ToString();
                            //        }
                            //        countnum++;
                            //    }
                            //    dropControl.Items.Add(new ListItem(a.ToString(), b.ToString()));
                            //}
                            #endregion
                            //赋值操作
                            
                                string[] valArr_item_option = modelt.item_option.Split(new string[] { "\r\n", "\n" },
                                StringSplitOptions.None);
                                for (int i = 0; i < valArr_item_option.Length; i++)
                                {
                                    string[] valItemArr = valArr_item_option[i].Split('|');
                                    if (valItemArr.Length == 2)
                                    {
                                        dropControl.Items.Add(new ListItem(valItemArr[0], valItemArr[1]));
                                    }
                                }
                                htmlDD.Controls.Add(dropControl);
                                if (acid.Value.ToString() == "edit" && modelt.name=="status")
                                {
                                    dropControl.Enabled = false;
                                }

                            break;

                    }

                    //将DT和DD添加到DL中
                    htmlDL.Controls.Add(htmlDT);
                    htmlDL.Controls.Add(htmlDD);
                    //将DL添加至field_tab_content中
                    field_tab_content.Controls.Add(htmlDL);
                }
            }


        }
        #endregion

        #region 获取模板字段

        private List<FormField> GetTemplateFields()
        {
            List<Model.FormField> ls = new List<FormField>();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("AccountTemplate");
            select.SelectColumn("fields");
            select.AddWhere("id", tid.Value);
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

        #region 扩展字段赋值=============================
        private Dictionary<string, string> SetFieldValues()
        {
            List<Model.FormField> ls = GetTemplateFields();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (Model.FormField model in ls)
            {
                //查找相应的控件
                switch (model.control_type)
                {
                    case "single-text": //单行文本
                        TextBox txtControl = FindControl("field_control_" + model.name) as TextBox;
                        if (txtControl != null)
                        {
                            dic.Add(model.name, txtControl.Text.Trim());

                        }
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea htmlTextAreaControl = FindControl("field_control_" + model.name) as HtmlTextArea;
                        if (htmlTextAreaControl != null)
                        {
                            dic.Add(model.name, htmlTextAreaControl.Value);
                        }
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "video": //视频上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "date": //日期
                        goto case "single-text";
                    case "datetime": //日期时间
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = FindControl("field_control_" + model.name) as CheckBox;
                        if (cbControl != null)
                        {
                            if (cbControl.Checked == true)
                            {
                                dic.Add(model.name, "1");
                            }
                            else
                            {
                                dic.Add(model.name, "0");
                            }
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = FindControl("field_control_" + model.name) as RadioButtonList;
                        if (rblControl != null)
                        {
                            dic.Add(model.name, rblControl.SelectedValue);
                        }
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = FindControl("field_control_" + model.name) as CheckBoxList;
                        if (cblControl != null)
                        {
                            StringBuilder tempStr = new StringBuilder();
                            for (int i = 0; i < cblControl.Items.Count; i++)
                            {
                                if (cblControl.Items[i].Selected)
                                {
                                    tempStr.Append(cblControl.Items[i].Value.Replace(',', '，') + ",");
                                }
                            }
                            dic.Add(model.name, Utils.DelLastComma(tempStr.ToString()));
                        }
                        break;
                    case "dropdownlist": //下拉列表
                        DropDownList dropControl = FindControl("field_control_" + model.name) as DropDownList;
                        if (dropControl != null)
                        {
                            dic.Add(model.name, dropControl.Text.Trim());
                        }
                        break;
                }
            }
            return dic;
        }
        #endregion


        void BindData()
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format("Select * from View_Account where Id={0} and Creator='{1}'", id, userid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                List<Model.FormField> ls1 = GetTemplateFields();
                foreach (Model.FormField modelt1 in ls1)
                {
                    switch (modelt1.control_type)
                    {
                        case "single-text": //单行文本
                            TextBox txtControl = FindControl("field_control_" + modelt1.name) as TextBox;
                            if (txtControl != null && dt.Columns.Contains(modelt1.name))
                            {
                                if (modelt1.is_password == 1)
                                {
                                    txtControl.Attributes.Add("value", dr[modelt1.name].ToString());
                                }
                                else
                                {
                                    if (modelt1.control_type == "date")
                                    {
                                        txtControl.Text = String.Format("{0:yyyy-MM-dd}", dr[modelt1.name]);
                                    }
                                    else if (modelt1.control_type == "datetime")
                                    {
                                        txtControl.Text = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr[modelt1.name]);
                                    }
                                    else
                                    {
                                        txtControl.Text = dr[modelt1.name].ToString();
                                    }

                                }
                            }
                            break;
                        case "multi-text": //多行文本
                            goto case "single-text";
                        case "editor": //编辑器
                            HtmlTextArea txtAreaControl = FindControl("field_control_" + modelt1.name) as HtmlTextArea;
                            if (txtAreaControl != null && dt.Columns.Contains(modelt1.name))
                            {
                                txtAreaControl.Value = dr[modelt1.name].ToString();
                            }
                            break;
                        case "images": //图片上传
                            goto case "single-text";
                        case "video": //视频上传
                            goto case "single-text";
                        case "number": //数字
                            goto case "single-text";
                        case "date": //日期
                            goto case "single-text";
                        case "datetime": //日期时间
                            goto case "single-text";
                        case "checkbox": //复选框
                            CheckBox cbControl = FindControl("field_control_" + modelt1.name) as CheckBox;
                            if (cbControl != null && dt.Columns.Contains(modelt1.name))
                            {
                                if (dr[modelt1.name].ToString() == "1")
                                {
                                    cbControl.Checked = true;
                                }
                                else
                                {
                                    cbControl.Checked = false;
                                }
                            }
                            break;
                        case "multi-radio": //多项单选
                            RadioButtonList rblControl = FindControl("field_control_" + modelt1.name) as RadioButtonList;
                            if (rblControl != null && dt.Columns.Contains(modelt1.name))
                            {
                                rblControl.SelectedValue = dr[modelt1.name].ToString();
                            }
                            break;
                        case "multi-checkbox": //多项多选
                            CheckBoxList cblControl = FindControl("field_control_" + modelt1.name) as CheckBoxList;
                            if (cblControl != null && dt.Columns.Contains(modelt1.name))
                            {
                                string[] valArr = dr[modelt1.name].ToString().Split(',');
                                for (int i = 0; i < cblControl.Items.Count; i++)
                                {
                                    cblControl.Items[i].Selected = false; //先取消默认的选中
                                    foreach (string str in valArr)
                                    {
                                        if (cblControl.Items[i].Value == str)
                                        {
                                            cblControl.Items[i].Selected = true;
                                        }
                                    }
                                }
                            }
                            break;
                        case "dropdownlist": //下拉列表
                            DropDownList dropControl = FindControl("field_control_" + modelt1.name) as DropDownList;
                            if (dropControl != null)
                            {
                                if (action == "mbadd")
                                {
                                    if (dr[modelt1.name].ToString() == "already_repay" || dr[modelt1.name].ToString() == "wait_repay")
                                    {
                                        dropControl.SelectedValue = "wait_repay";
                                    }
                                    else
                                    {
                                        dropControl.SelectedValue = "wait_receive";
                                    }
                                }
                                else
                                {
                                    dropControl.SelectedValue = dr[modelt1.name].ToString();
                                }
                            }
                            break;
                    }
                }

               

            }
            else
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "账单不存在或该账单不属于你", "error");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(aid.Value) || action=="mbadd")
                {
                    string userid = GetLoginUser().Userid;

                    IInsertDataSourceFace insert = new InsertSQL("Account");
                    insert.DataBaseAlias = "common";
                    insert.Transaction = tran;
                    insert.AddFieldValue("TempId", tempid);
                    insert.AddFieldValue("IsDelete", 0);
                    insert.AddFieldValue("Creator", userid);
                    insert.AddFieldValue("CreateTime", DateTime.Now);
                    insert.ExecuteNonQuery();


                    ISelectDataSourceFace select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.Transaction = tran;
                    select.CommandText = "SELECT @@IDENTITY";
                    object obj = select.ExecuteScalar();

                    insert = new InsertSQL("FormContent");
                    insert.DataBaseAlias = "common";
                    insert.Transaction = tran;
                    insert.AddFieldValue("info_id", obj);
                    Dictionary<string, string> dic = SetFieldValues();
                    //int sign = 0;
                    string zd_name = string.Empty; //账单名称
                    string zd_status = string.Empty;    //账单状态
                    foreach (KeyValuePair<string, string> item in dic)
                    {
                        insert.AddFieldValue(item.Key, item.Value);
                        if(item.Key == "status")
                        {   
                            zd_status = item.Value;
                            string[] flag = { "already_getmoney", "already_repay", "already_deposit", "bad_pay" };
                            List<string> list = new List<string>(flag);
                            IUpdateDataSourceFace update = new UpdateSQL("Account");
                            update.DataBaseAlias = "common";
                            update.Transaction = tran;
                            update.AddFieldValue("Account_Status", item.Value);
                            update.AddFieldValue("StatusName",GetDicName(item.Value));
                            if (list.Contains(item.Value))
                            {
                                update.AddFieldValue("isFinish","1");
                            }
                            else
                            {
                                update.AddFieldValue("isFinish", "0");
                            }
                            update.AddWhere("Id", obj);
                            update.ExecuteNonQuery();
                        }
                        else if(item.Key == "zd_name")
                        {
                            zd_name = item.Value;
                        }
                    }
                    insert.ExecuteNonQuery();
                    //获取最新插入的这条数据
                    string calculatorflag = zd_status=="wait_receive" || zd_status=="wait_deposit" || zd_status=="late_pay" ? "+" :(zd_status=="wait_repay" ? "-":"");
                    if(calculatorflag!="")
                    {
                        DataTable dt = GetNewestAccount(userid,zd_name,tran);
                        if(dt.Rows.Count>0)
                        {
                            if (Convert.ToDecimal(dt.Rows[0]["tz_money"].ToString()) != 0)
                            {
                                UpdateCash("cash", dt.Rows[0]["tz_money"].ToString(), calculatorflag == "+" ? "-" : "+", userid, "账单<a href='AccountDetail.aspx?id=" + dt.Rows[0]["id"].ToString() + "&tempid=" + GetTempId(dt.Rows[0]["id"].ToString(), tran) + "'><font color='red'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>" + (zd_status == "wait_repay" ? "预支收入" : "投资扣款"), dt.Rows[0]["id"].ToString(), 0, "special");
                            }
                            UpdateCash("total_money", dt.Rows[0]["ds_money"].ToString(), calculatorflag, userid, "账单<a href='AccountDetail.aspx?id=" + dt.Rows[0]["id"].ToString() + "&tempid=" + GetTempId(dt.Rows[0]["id"].ToString(), tran) + "'><font color='green'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>录入", dt.Rows[0]["id"].ToString(), 1, "normal");
                        }
                    }
                    //写入日志
                    //insert = new InsertSQL("Account_Log");
                    //insert.DataBaseAlias = "common";
                    //insert.Transaction = tran;
                    //insert.AddFieldValue("AccountId", obj);
                    //insert.AddFieldValue("Actor", GetLoginUser().Userid);
                    //insert.AddFieldValue("Action", "Save");
                    //insert.AddFieldValue("Opinion", "");
                    //insert.AddFieldValue("ActTime", DateTime.Now);
                    //insert.ExecuteNonQuery();
                    tran.Commit();
                    aid.Value = obj.ToString();
                }
                else
                {
                    string zd_name = string.Empty;
                    IUpdateDataSourceFace update = new UpdateSQL("FormContent");
                    update.DataBaseAlias = "common";
                    update.Transaction = tran;
                    Dictionary<string, string> dic = SetFieldValues();
                    foreach (KeyValuePair<string, string> item in dic)
                    {
                        update.AddFieldValue(item.Key, item.Value);
                        if (item.Key == "status")
                        {
                            IUpdateDataSourceFace update1 = new UpdateSQL("Account");
                            update1.DataBaseAlias = "common";
                            update1.Transaction = tran;
                            update1.AddFieldValue("Account_Status", item.Value);
                            update1.AddFieldValue("StatusName", GetDicName(item.Value));
                            update1.AddWhere("Id", aid.Value.Trim());
                            update1.ExecuteNonQuery();
                        }
                        if(item.Key == "zd_name")
                        {
                            zd_name = item.Value;
                        }

                        DataTable Adt = null;  //存储账单信息
                        string status = string.Empty; //账单状态
                        if(item.Key=="ds_money")
                        {
                            Adt = GetAInfo(aid.Value.Trim(), tran);
                            status = Adt.Rows[0]["status"].ToString();
                            string old_ds_money = Adt.Rows[0]["ds_money"].ToString();
                            decimal d_value = 0;
                            if(status == "wait_repay")
                            {
                                d_value = Convert.ToDecimal(old_ds_money) - Convert.ToDecimal(item.Value);
                            }
                            else
                            {
                                d_value = Convert.ToDecimal(item.Value) - Convert.ToDecimal(old_ds_money);
                            }
                            if (d_value != 0)
                            {
                                UpdateCash("total_money", d_value.ToString(), d_value > 0 ? "+" : "-", userid, "账单<a href='AccountDetail.aspx?id=" + aid.Value + "&tempid=" + GetTempId(aid.Value.Trim(), tran) + "'><font color='#2222DD'>" + zd_name + "</font></a>修改" + (status == "wait_repay" ? "待还" : "待收") + "金额", aid.Value.Trim(), 1, "normal");
                            }
                        }
                        if (item.Key == "tz_money")
                        {
                            if(Adt == null)
                            {
                                Adt = GetAInfo(aid.Value.Trim(), tran);
                                status = Adt.Rows[0]["status"].ToString();
                            }
                            string old_tz_money = Adt.Rows[0]["tz_money"].ToString();
                            decimal d_value = 0;
                            if (status != "wait_repay") 
                            {
                                d_value = Convert.ToDecimal(old_tz_money) - Convert.ToDecimal(item.Value);
                            }
                            else
                            {
                                d_value = Convert.ToDecimal(item.Value) - Convert.ToDecimal(old_tz_money);
                            }
                            if (d_value != 0)
                            {
                                UpdateCash("cash", d_value.ToString(), d_value > 0 ? "+" : "-", userid, "账单<a href='AccountDetail.aspx?id=" + aid.Value + "&tempid=" + GetTempId(aid.Value.Trim(), tran) + "'><font color='#2222DD'>" + zd_name + "</font></a>修改" + (status == "wait_repay" ? "借款" : "投资") + "金额", aid.Value.Trim(), 0, "special");
                            }
                        }
                    }
                    update.AddWhere("info_id", aid.Value.Trim());
                    update.ExecuteNonQuery();
                    tran.Commit();

                }
                this.type = DTRequest.GetQueryInt("type");
                if(this.type ==0)
                {
                    hidtype.Value = DTRequest.GetQueryString("atype");
                    msg = string.Format("JsPrint('{0}','{1}')", "保存成功！", "success_0");
                }
                else if(this.type ==1)
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "保存成功！", "success_1");
                }
                else if (this.type == 2)
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "保存成功！", "success_2");
                }
                else if (this.type == 3)
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "保存成功！", "success_3");
                }
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                

            }
            catch (Exception ex)
            {
                ExceptionManager.Handle(ex);
                tran.Rollback();
                msg = string.Format("JsPrint('{0}','{1}')", "保存失败！", "fail");
            }
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        }

        public DataTable GetNewestAccount(string userid,string zd_name,IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = string.Format(@"select top 1 id,zd_name,ds_money,tz_money from View_Account where Creator='{0}' and TempId='5' and zd_name='{1}' and IsDelete =0  order by CreateTime desc", userid, zd_name);
            return select.ExecuteDataSet().Tables[0];
        }

        public DataTable GetAInfo(string id, IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = string.Format(@"select ds_money,status,tz_money from View_Account where info_id='{0}'", id);
            return select.ExecuteDataSet().Tables[0];
        }

        public int GetTempId(string id, IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = string.Format(@"select tempid from Account where id='{0}'", id);
            return Convert.ToInt32(select.ExecuteDataSet().Tables[0].Rows[0]["tempid"].ToString());
        }
    }
}