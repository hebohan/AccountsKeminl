using System;
using System.Web.UI;
using Accounts.BLL;
using Accounts.BLL.Common;
using System.Text.RegularExpressions;

namespace Accounts.Web.SysSetting
{
    public partial class FormFieldDetail: BasePage
    {

        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");
           
            if (!Page.IsPostBack)
            {
                dlIsPassWord.Visible = dlIsHtml.Visible = dlEditorType.Visible = dlDataType.Visible
                    = dlDataLength.Visible = dlDataPlace.Visible = dlItemOption.Visible = false; //隐藏相应控件
                if (id>0) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.FormField bll = new BLL.FormField();
            Model.FormField model = bll.GetModel(_id);

            txtName.Enabled = false;
            txtName.Attributes.Remove("ajaxurl");
            txtName.Attributes.Remove("datatype");
            ddlControlType.SelectedValue = model.control_type;
            Display_Width.Text = model.display_width.ToString(); //显示列宽
            showControlHtml(model.control_type); //显示对应的HTML
            txtSortId.Text = model.sort_id.ToString();
            txtName.Text = model.name;
            txtTitle.Text = model.title;
            if (model.is_required == 1)
            {
                cbIsRequired.Checked = true;
            }
            else
            {
                cbIsRequired.Checked = false;
            }
            if (model.is_password == 1)
            {
                cbIsPassword.Checked = true;
            }
            else
            {
                cbIsPassword.Checked = false;
            }
            if (model.is_html == 1)
            {
                cbIsHtml.Checked = true;
            }
            else
            {
                cbIsHtml.Checked = false;
            }
            rblEditorType.SelectedValue = model.editor_type.ToString();
            rblDataType.SelectedValue = model.data_type;
            txtDataLength.Text = model.data_length.ToString();
            ddlDataPlace.SelectedValue = model.data_place.ToString();
            txtItemOption.Text = model.item_option;
            txtDefaultValue.Text = model.default_value;
            txtValidPattern.Text = model.valid_pattern;
            txtValidTipMsg.Text = model.valid_tip_msg;
            txtValidErrorMsg.Text = model.valid_error_msg;
            //if (model.is_sys == 1)
            //{
            //    ddlControlType.Enabled = false;
            //}

        }
        #endregion

        #region 显示对应的控件===========================
        private void showControlHtml(string control_type)
        {
            if (id > 0)
            {
                txtName.Attributes.Remove("ajaxurl");
                txtName.Attributes.Remove("datatype");
            }
            dlIsPassWord.Visible = dlIsHtml.Visible = dlEditorType.Visible = dlDataType.Visible
                    = dlDataLength.Visible = dlDataPlace.Visible = dlItemOption.Visible = dlValidPattern.Visible = dlValidErrorMsg.Visible = false; //隐藏相应控件
            switch (control_type)
            {
                case "single-text": //单行文本
                    dlIsPassWord.Visible = dlDataLength.Visible = dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "multi-text": //多行文本
                    dlIsHtml.Visible = dlDataLength.Visible = dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "editor": //编辑器
                    dlEditorType.Visible = dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "images": //图片上传
                    dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "video": //视频上传
                    dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "number": //数字
                    dlDataPlace.Visible = dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "date": //日期
                    dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "datetime": //日期时间
                    dlValidPattern.Visible = dlValidErrorMsg.Visible = true;
                    break;
                case "checkbox": //复选框
                    break;
                case "multi-radio": //多项单选
                    dlDataType.Visible = dlDataLength.Visible = dlItemOption.Visible = true;
                    break;
                case "multi-checkbox": //多项多选
                    dlDataLength.Visible = dlItemOption.Visible = true;
                    break;
                case "dropdownlist": //下拉列表
                    dlItemOption.Visible = dlDataLength.Visible = true;
                    break;

            }

        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.FormField model = new Model.FormField();
            BLL.FormField bll = new BLL.FormField();

            model.control_type = ddlControlType.SelectedValue;
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.name = txtName.Text.Trim();
            model.title = txtTitle.Text;
            if (cbIsRequired.Checked == true)
            {
                model.is_required = 1;
            }
            else
            {
                model.is_required = 0;
            }
            if (cbIsPassword.Checked == true)
            {
                model.is_password = 1;
            }
            else
            {
                model.is_password = 0;
            }
            if (cbIsHtml.Checked == true)
            {
                model.is_html = 1;
            }
            else
            {
                model.is_html = 0;
            }
            model.editor_type = Utils.StrToInt(rblEditorType.SelectedValue, 0);
            model.data_length = Utils.StrToInt(txtDataLength.Text.Trim(), 0);
            model.data_place = Utils.StrToInt(ddlDataPlace.SelectedValue, 0);
            model.data_type = rblDataType.SelectedValue;
            model.item_option = txtItemOption.Text.Trim();
            model.default_value = txtDefaultValue.Text.Trim();
            model.valid_pattern = txtValidPattern.Text.Trim();
            model.valid_tip_msg = txtValidTipMsg.Text.Trim();
            model.valid_error_msg = txtValidErrorMsg.Text.Trim();
            model.display_width = Utils.StrToInt(Display_Width.Text.Trim(), 0);
            model.creator = GetLoginUser().Userid;
            if (bll.Add(model) > 0)
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
            BLL.FormField bll = new BLL.FormField();
            Model.FormField model = bll.GetModel(_id);

            //if (model.is_sys == 0)
          //  {
                model.control_type = ddlControlType.SelectedValue;
                model.data_length = Utils.StrToInt(txtDataLength.Text.Trim(), 0);
                model.data_place = Utils.StrToInt(ddlDataPlace.SelectedValue, 0);
                model.data_type = rblDataType.SelectedValue;
                
            /*}*/
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.title = txtTitle.Text;
            if (cbIsRequired.Checked == true)   //必填
            {
                model.is_required = 1;
            }
            else
            {
                model.is_required = 0;
            }
            if (cbIsPassword.Checked == true)
            {
                model.is_password = 1;
            }
            else
            {
                model.is_password = 0;
            }
            if (cbIsHtml.Checked == true)
            {
                model.is_html = 1;
            }
            else
            {
                model.is_html = 0;
            }
            model.editor_type = Utils.StrToInt(rblEditorType.SelectedValue, 0);
            model.item_option = txtItemOption.Text.Trim();
            model.default_value = txtDefaultValue.Text.Trim();
            model.valid_pattern = txtValidPattern.Text.Trim();
            model.valid_tip_msg = txtValidTipMsg.Text.Trim();
            model.valid_error_msg = txtValidErrorMsg.Text.Trim();
            model.display_width = Utils.StrToInt(Display_Width.Text.Trim(),0);
            model.creator = GetLoginUser().Userid;
            if (bll.Update(model))
            {
                result = true;
            }

            return result;
        }
        #endregion

        //根据选择的控件类型显示相应部分
        protected void ddlControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            showControlHtml(ddlControlType.SelectedValue);
        }

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
                    msg = string.Format("JsPrint('{0}','{1}')", "修改表单字段成功！", "success");
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
                    msg = string.Format("JsPrint('{0}','{1}')", "添加表单字段成功！", "success");
                }
            }
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        }

    }
}