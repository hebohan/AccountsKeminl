<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsScheduleAudit.aspx.cs" Inherits="Accounts.Web.AccountManage.AccountsScheduleAudit" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>
<%@ Register TagPrefix="cc1" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid" Assembly="Inspur.Finix.WebFramework.Controls4AspNet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>帐号信息</title>
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/validate.css?v1.0" />
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <link href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/jquery-easyui-1.3.4/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../share/js/List.js"></script>
    <script type="text/javascript" language="javascript" src="../js/Finix.js"></script>
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
                location.href = "AccountList.aspx";

            }
            else if (type == "error") {
                $.dialog.tips(msg, 1, 'error.gif', backToList);

            } else if (type == "save") {
                $.dialog.tips(msg, 1, 'success.gif');
                window.parent.location.href = "AccountList.aspx";
            } else {
                $.dialog.alert(msg);
            }
        }

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
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
        .comments {
            width: 100%; /*自动适应父布局宽度*/
            overflow: auto;
            word-break: break-all;
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
                        <asp:Button ID="btnSave" runat="server" Text="状态认定" OnClientClick="FormValide();" OnClick="btnSave_Click" class="btnSave"/>
                        <input type="button" onclick="backToList()" value="返回" class="btnBack"/>
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <div class="clear" style="clear: both;"></div>
            <table id="field_tab_content" runat="server" class="ListSearch hei12">
            <%--<tr>
                <td class="ListSearchInput">账单月份：</td>
                <td class="inputtdline" colspan="3">
                    <asp:Label runat="server" ID="month"></asp:Label>
                </td>
            </tr>--%>
<%--            <tr>
                <td class="ListSearchInput">账单详情：</td>
                <td class="inputtdline" colspan="3">
                   <textarea class="input normal" rows="5" id="detail" runat="server" datatype="*"></textarea>
                </td>
            </tr>--%>
                <tr>
                <td class="ListSearchInput">帐号录入人：</td>
                <td class="inputtdline">
                    <asp:Label runat="server" ID="logger"></asp:Label>
                </td>
                     <td class="ListSearchInput">录入时间：</td>
                <td class="inputtdline">
                    <asp:Label runat="server" ID="logtime"></asp:Label>
                </td>
                    <td class="ListSearchInput">状态认定：</td>
                    <td class="inputtdline" colspan="3">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input normal" datatype="*">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
            </tr>
                <tr>
                    <%--<td class="ListSearchInput">账单详情：</td>
                    <td class="inputtdline">
                        <textarea class="input normal" rows="5" id="detail" runat="server" datatype="*"></textarea>
                    </td>--%>
                    <td class="ListSearchInput">帐号备注：</td>
                    <td class="inputtdline"  colspan="6" >
                        <textarea class="input normal" rows="8" id="zd_remark" runat="server" style="width: 100%" ></textarea>
                    </td>
            </tr>
                <tr>

                </tr>
            </table>
            <input id="hidid" runat="server" type="hidden" />
            <input id="hidaid" runat="server" type="hidden" />
        </div>
    </form>
</body>
</html>
