<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormFieldDetail.aspx.cs" Inherits="Accounts.Web.SysSetting.FormFieldDetail" ValidateRequest="false" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>表单字段编辑</title>
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/validate.css?v1.0" />
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
        function backToList() {
            location.href = "FormFieldList.aspx";
        }
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', backToList);

            } else {
                $.dialog.alert(msg);
            }
        }
    </script>
    <style>
        .ListSearch {
            padding: 20px 15px;
            font-size: 12px;
            color: #666;
            box-sizing: border-box;
            overflow: hidden;
            min-height: 400px;
            width: 100%;
        }

        .ListSearch tr td {
            padding: 3px 0;
        }
                .ListSearchInput {
                    display: block;
                    float: left;
                    width: 35%;
                    text-align: right;
                    color: #6d7e86;
                    font-weight: bold;
                }

                .inputtdline {
                    position: relative;
                    width: 63%;
                    float: right;
                }

        .input.small {
            width: 50px;
        }

        .input.normal {
            width: 300px;
        }

        .input {
            padding: 5px 4px;
            line-height: 20px;
            border: 1px solid #eee;
            background: #fff;
            vertical-align: middle;
            color: #333;
            font-size: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            float: left;
        }

        #MainCont {
            width: 100%;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />表单字段编辑<a id="lbTile" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <asp:Button ID="btnSubmit" runat="server" Text="保存" OnClick="btnSubmit_Click" class="btnSave" />
                        <input type="button" onclick="backToList()" value="返回" class="btnBack" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">控件类型：
                    </td>
                    <td class="inputtdline">
                        <asp:DropDownList ID="ddlControlType" runat="server" datatype="*"
                            errormsg="请选择控件类型！" sucmsg=" " AutoPostBack="True"
                            OnSelectedIndexChanged="ddlControlType_SelectedIndexChanged" CssClass="input normal">
                            <asp:ListItem Value="">请选择类型...</asp:ListItem>
                            <asp:ListItem Value="single-text">单行文本</asp:ListItem>
                            <asp:ListItem Value="multi-text">多行文本</asp:ListItem>
                            <asp:ListItem Value="number">数字</asp:ListItem>
                             <asp:ListItem Value="date">日期</asp:ListItem>
                            <asp:ListItem Value="datetime">日期时间</asp:ListItem>
                            <asp:ListItem Value="checkbox">复选框</asp:ListItem>
                            <asp:ListItem Value="dropdownlist">下拉列表</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">排序数字：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                        <span class="Validform_checktip">*数字，越小越向前</span>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">字段列名：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" " nullmsg="请输入列名" ajaxurl="../ashx/admin_ajax.ashx?action=form_field_validate"></asp:TextBox>
                        <span class="Validform_checktip">*字母、下划线，不可修改</span>
                    </td>
                </tr>
                <tr id="valueTR" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">字段标题：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
                        <span class="Validform_checktip">*中文标题，做为备注</span>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">是否必填：
                    </td>
                    <td class="inputtdline">
                        <asp:CheckBox ID="cbIsRequired" runat="server" />
                    </td>
                </tr>
                <tr id="dlIsPassWord" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">是否密码框：
                    </td>
                    <td class="inputtdline">
                        <asp:CheckBox ID="cbIsPassword" runat="server" />
                    </td>
                </tr>
                <tr id="dlIsHtml" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">是否允许HTML：
                    </td>
                    <td class="inputtdline">
                        <asp:CheckBox ID="cbIsHtml" runat="server" />
                    </td>
                </tr>
                <tr id="dlEditorType" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">编辑器类型：
                    </td>
                    <td class="inputtdline">
                        <asp:RadioButtonList ID="rblEditorType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="input normal">
                            <asp:ListItem Value="0" Selected="True">标准型</asp:ListItem>
                            <asp:ListItem Value="1">简洁型</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="dlDataType" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">字段类型：
                    </td>
                    <td class="inputtdline">
                        <asp:RadioButtonList ID="rblDataType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="input normal">
                            <asp:ListItem Value="nvarchar" Selected="True">字符串</asp:ListItem>
                            <asp:ListItem Value="int">整型数字</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="dlDataLength" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">字符串长度：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtDataLength" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>
                        <span class="Validform_checktip">*数字，默认50个字符</span>
                    </td>
                </tr>
                <tr id="dlDisplay_width" runat="server">
                    <td class="ListSearchInput" style="text-align: right;"> 显示列宽：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="Display_Width" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>
                        <span class="Validform_checktip">*%，百分比，默认10%</span>
                    </td>
                </tr>
                <tr id="dlDataPlace" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">小数点位数：
                    </td>
                    <td class="inputtdline">
                        <asp:DropDownList ID="ddlDataPlace" runat="server" CssClass="input normal">
                            <asp:ListItem Value="0">无小数点</asp:ListItem>
                            <asp:ListItem Value="1">一位</asp:ListItem>
                            <asp:ListItem Value="2">二位</asp:ListItem>
                            <asp:ListItem Value="3">三位</asp:ListItem>
                            <asp:ListItem Value="4">四位</asp:ListItem>
                            <asp:ListItem Value="5">五位</asp:ListItem>
                        </asp:DropDownList>
                        <span class="Validform_checktip">*无小数点为整型，否则浮点数</span>
                    </td>
                </tr>
                <tr id="dlItemOption" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">选项列表：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtItemOption" runat="server" CssClass="input normal" TextMode="MultiLine" datatype="*" sucmsg=" " Height="100"></asp:TextBox>
                        <span class="Validform_checktip">*以换行为一行</span>
                        <div>*填写说明：选项名称|值，以回车换行为一行。</div>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">默认值：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtDefaultValue" runat="server" CssClass="input normal"></asp:TextBox>
                        <span class="Validform_checktip">*控件的默认字符，可为空</span>
                    </td>
                </tr>
                <tr id="dlValidPattern" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">验证正则表达式：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtValidPattern" runat="server" CssClass="input normal" TextMode="MultiLine"></asp:TextBox>
                        <span class="Validform_checktip">*不填写则不验证</span>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">验证提示信息：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtValidTipMsg" runat="server" CssClass="input normal" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr id="dlValidErrorMsg" runat="server">
                    <td class="ListSearchInput" style="text-align: right;">验证失败信息：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtValidErrorMsg" runat="server" CssClass="input normal" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
