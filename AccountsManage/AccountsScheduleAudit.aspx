<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsScheduleAudit.aspx.cs" Inherits="Accounts.Web.AccountsManage.AccountsScheduleAudit" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>
<%@ Register TagPrefix="cc1" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid" Assembly="Inspur.Finix.WebFramework.Controls4AspNet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目信息</title>
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/validate.css?v1.0" />
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../share/js/layout.js"></script>
    <script src="../CSS/layer/layer.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <link href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/jquery-easyui-1.3.4/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../share/js/List.js"></script>
    <script type="text/javascript" language="javascript" src="../js/Finix.js"></script>
        <style>
        .Textbox {
            font-weight:bold;
            color:orange;
            width:60px;
            border:0;
            text-align:center;
        }
        .button {
            width: 30px;
            line-height: 18px;
            text-align: center;
            font-weight: bold;
            color: #fff;
            text-shadow: 1px 1px 1px #333;
            border-radius: 5px;
            overflow: hidden;
        }
        .button.gray {
                color: #8c96a0;
                text-shadow: 1px 1px 1px #fff;
                border: 1px solid #dce1e6;
                box-shadow: 0 1px 2px #fff inset,0 -1px 0 #a8abae inset;
                background: -webkit-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: -moz-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: linear-gradient(top,#f2f3f7,#e4e8ec);
            }
       .button.larrow {
            overflow: visible;
        }
    </style>
    <script>
        function FormValide() {
            //初始化表单验证
            $("#form1").initValidform();

        }
        function backToList() {
            history.go(-1);
        }

        function showAddDialog(id) {
            $('#addid').val(id);
            $("#adddialog").dialog('open');
            $('#adddialog').parent().appendTo($("form:first"));
        }
        function showAuditDialog(id) {
            $('#auditid').val(id);
            $("#auditdialog").dialog('open');
            $('#auditdialog').parent().appendTo($("form:first"));
        }

        function JsPrint(msg, type, id) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', backToList);
                location.href = "AccountsList.aspx";

            }
            else if (type == "error") {
                $.dialog.tips(msg, 1, 'error.gif', backToList);

            }
            else if (type == "error_1") {
                $.dialog.tips(msg, 1, 'error.gif');

            } else if (type == "save") {
                $.dialog.tips(msg, 1, 'success.gif');
                window.parent.location.href = "AccountsList.aspx";
            }
            else if (type == "remark") {
                $.dialog.tips(msg, 1, 'success.gif');
                window.parent.location.href = "SummaryDisplayList.aspx";
            } else {
                $.dialog.alert(msg);
            }
        }

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        function AddUrl() {
            var id = $('#hidid').val();
            var u_name = $('#u_name').val();
            var u_url = $('#u_url').val();
            if (u_name == "")
            {
                u_name = u_url;
            }
            if (u_url != "") {
                if (id != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=addurl",
                        data: { "id": id, "name": u_name, "url": u_url },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                $('#_name_panel').hide();
                                $('#u_name').hide();
                                $('#_url_panel').hide();
                                $('#u_url').hide();
                                $('#add').hide();
                                $('#modify').show();
                                $('#url_label').show();
                                $('#url_label').html(data.url);
                                $('#hidurlname').val(u_name);
                                $('#hidurl').val(u_url);
                            } else {
                                alert("添加链接失败");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("添加链接成功");
                            window.parent.location.reload();
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            }
            else {
                alert("请输入网址");
            }
        }

        function Confirm() {
            var id = $('#hidid').val();
            var u_name = $('#u_name').val();
            var u_url = $('#u_url').val();
            if (u_name == "")
            {
                u_name = u_url;
            }
            if (u_url != "") {
                if (id != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=addurl",
                        data: { "id": id, "name": u_name, "url": u_url },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                $('#_name_panel').hide();
                                $('#u_name').hide();
                                $('#_url_panel').hide();
                                $('#u_url').hide();
                                $('#add').hide();
                                $('#modify').show();
                                $('#hidurlname').val(u_name);
                                $('#hidurl').val(u_url);
                                $('#url_label').show();
                                $('#url_label').html(data.url);
                                $('#confirm').hide();
                                $('#cancle').hide();
                            } else {
                                alert("修改链接失败");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("修改链接成功");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            }
            else {
                $('#_name_panel').show();
                $('#u_name').show();
                $('#_url_panel').show();
                $('#u_url').show();
                $('#add').show();
                $('#modify').hide();
                $('#hidurlname').val(u_name);
                $('#hidurl').val(u_url);
                $('#url_label').hide();
                $('#confirm').hide();
                $('#cancle').hide();
            }
        }

        function ModifyUrl() {
            $('#confirm').show();
            $('#cancle').show();
            $('#_name_panel').show();
            $('#u_name').show();
            $('#_url_panel').show();
            $('#u_url').show();
            $('#add').hide();
            $('#url_label').hide();
            $('#modify').hide();
            $('#u_name').val($('#hidurlname').val());
            $('#u_url').val($('#hidurl').val());
        }

        function Cancle() {
            $('#url_label').show();
            $('#confirm').hide();
            $('#cancle').hide();
            $('#_name_panel').hide();
            $('#u_name').hide();
            $('#_url_panel').hide();
            $('#u_url').hide();
            $('#add').hide();
            $('#modify').show();
            //$('#u_name').val($('#hidurlname').val());
            //$('#u_url').val($('#hidurl').val());
        }
    </script>
    <style type="text/css">
         html,body{ min-width: 0;}
         #form1 {
             width: 100%;
             height: auto;
             min-width: 0;
         }

        .Wdate {
            height: auto;
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
            margin: 5px 0 5px 10px;
        }

        .ListSearch {
            font-size: 12px;
            color: #666;
            box-sizing: border-box;
            overflow: hidden;
            width: 100%;
            padding: 0;
            margin: 0;
            border: 1px solid #a0c6e5;
            border-collapse: collapse;
        }

        .ListSearch tr td {
            border: 1px solid #a0c6e5;
            border-collapse: collapse;
        }

        .ListSearchInput {
            width: 20%;
            text-align: right;
            color: #0b76ac;
            font-weight: bold;
            background-color: rgba(14, 144, 210, .115);
        }

        .hei12 {
            margin-top: 50px;
            margin-bottom: 10px;
        }

        .inputtdline {
            width: 30%;
            word-break: break-all;
        }

        .inputtdline span {
            display: block;
            margin: 5px;
            word-break: break-all;
        }

        #MainCont {
            width: 100%;
            min-width: 0;
        }

        .ListTitle {
            position: fixed;
            top: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />账单进度认定<a id="lbTile" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <%--<input type="button" id="btnRemark" text="添加备注" runat="server" onclick="FormValide_Remark();" />--%>
                        <asp:Button ID="btnRemark" runat="server" Text="添加备注" OnClick="btnRemark_Click" class="btnEdit" Visible="false"/>
                        <asp:Button ID="btnSave" runat="server" Text="进度认定" OnClientClick="FormValide();" OnClick="btnSave_Click" class="btnSave"/>
                        <input type="button" onclick="backToList()" value="返回" class="btnBack"/>
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <div class="clear" style="clear: both;"></div>
            <table id="field_tab_content" runat="server" class="ListSearch hei12">
            <tr>
                <td class="ListSearchInput">账单月份：</td>
                <td class="inputtdline">
                    <asp:Label runat="server" ID="month"></asp:Label>
                </td>
                <td class="ListSearchInput">账单状态：</td>
                <td class="inputtdline">
                   <asp:Label runat="server" ID="zd_status"></asp:Label>
                </td>
            </tr>
                <tr>
                <td class="ListSearchInput">录入人员：</td>
                <td class="inputtdline">
                    <asp:Label runat="server" ID="logger"></asp:Label>
                </td>
                     <td class="ListSearchInput">录入时间：</td>
                <td class="inputtdline">
                    <asp:Label runat="server" ID="logtime"></asp:Label>
                </td>
            </tr>
                <tr>
                    <td class="ListSearchInput">账单详情：</td>
                    <td class="inputtdline">
                        <textarea class="input normal" rows="5" id="detail" runat="server" datatype="*"></textarea>
                    </td>
                    <td class="ListSearchInput">备注：</td>
                    <td class="inputtdline">
                        <textarea class="input normal" rows="5" id="zd_remark" runat="server"></textarea>
                    </td>
            </tr>
            <tr>
                <td class="ListSearchInput">进度认定：</td>
                <td class="inputtdline">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input normal" datatype="*">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td class="ListSearchInput">活动网址：</td>
                <td class="inputtdline">
                   <label id="url_label" runat="server" style="text-align:left"></label>
                    <label runat="server" id="_name_panel" >名称:</label><input type="text" id="u_name" runat="server" style="width:98px"/> 
                    <label runat="server" id="_url_panel" >网址:</label> <input type="text" id="u_url" runat="server" style="width:98px"/>
                    <input type="button" id="add" runat="server" onclick="AddUrl();" value="添加" class="button gray larrow"/>
                    <input type="button" id="modify" runat="server" onclick="ModifyUrl();" value="修改" class="button gray larrow"/>
                    <input type="button" id="confirm" runat="server" onclick="Confirm();" value="确定" class="button gray larrow" style="display:none"/>
                    <input type="button" id="cancle" runat="server" onclick="Cancle();" value="取消" class="button gray larrow"  style="display:none;padding-left:1px"/>
                </td>
            </tr>
            </table>
            <h1 style="text-align:center;display:none" id="tip" runat="server">该账单已被删除！仅供查看！</h1>

            <input id="hidid" runat="server" type="hidden" />
            <input id="hidaid" runat="server" type="hidden" />
            <input id="hidmoney" runat="server" type="hidden" />
            <input id="hiddq_day" runat="server" type="hidden" />
            <input id="hiddaycount" runat="server" type="hidden" />
            <input id="hidzd_name" runat="server" type="hidden" />
            <input id="hidurlname" runat="server" type="hidden" />
            <input id="hidurl" runat="server" type="hidden" />
            <input id="hidtip" runat="server" type="hidden" />
        </div>
    </form>
</body>
</html>
