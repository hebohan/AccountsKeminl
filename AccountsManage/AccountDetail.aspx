<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDetail.aspx.cs" Inherits="Accounts.Web.AccountsManage.AccountDetail" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Accounts.BLL.Common" %>
<%@ Import Namespace="Accounts.BLL" %>
<%@ Register TagPrefix="cc1" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid" Assembly="Inspur.Finix.WebFramework.Controls4AspNet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账单信息</title>
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
        function backToList() {
            //location.href = "AccountsList.aspx";
            history.go(-1);
        }


        $(function () {
            $("#backdialog").dialog('close');
            $("#rejectdialog").dialog('close');
            $('#btnBackCancel').bind('click', function () {
                $("#backdialog").dialog('close');
            });
            $('#btnRejectCancel').bind('click', function () {
                $("#rejectdialog").dialog('close');
            });


        });
        function JsPrint(msg, type, id) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess);

            }
            else if (type == "error") {
                $.dialog.tips(msg, 1, 'error.gif', callsuccess);

            } else if (type == "save") {
                $.dialog.tips(msg, 1, 'success.gif');

            }
              else {
                $.dialog.alert(msg);
            }
        }

        function callsuccess() {
            location.href = "AccountsList.aspx";
        }

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        function removeSchedule(id) {
            $("#rid").val(id);
            $("#btnRemoveSchedule").click();
        }

        function createacc()
        {
            var id = $('#aid').val();
            var tempid = $('#tid').val();
            window.location.href = 'AccountRegisterDetail.aspx?tempid='+tempid +"&id=" + id + "&action=mbadd&type=1";
        }
    </script>
    <style type="text/css">
        #form1 {
            width: 100%;
            height: auto;
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

        .hei12 {
            margin-top: 50px;
            margin-bottom: 10px;
        }

        .ListSearch {
            width: auto;
        }

            .ListSearch tr td {
                border: 1px solid #a0c6e5;
                border-collapse: collapse;
            }

        .ListSearchInput {
            width: 30%;
            text-align: right;
            color: #0b76ac;
            font-weight: bold;
            background-color: rgba(14, 144, 210, .115);
        }

        .inputtdline {
            width: 70%;
            word-break: break-all;
        }

            .inputtdline span {
                display: block;
                margin: 5px;
                word-break: break-all;
            }

        #MainCont {
            width: 100%;
        }

        .ListTitle {
            position: fixed;
            top: 0;
        }

        #field_tab_content {
            text-align: center;
        }

        .hidden {
            display: none;
        }

        #field_tab_content  tr th {
            white-space: nowrap;
            word-break: nowrap;
        }

        #field_tab_content tbody tr td {
            padding: 0 35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />帐单信息<a id="lbTile" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" onclick="createacc()" value="以此账单为模板创建账单" class="btnAdd" />
                        <input type="button" onclick="backToList()" value="返回" class="btnBack" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <div class="clear" style="clear: both;"></div>
            <div style="width: 100%; overflow-x: scroll;">
                <table id="field_tab_content" class="ListTable ListSearch hei12">
                    <%=field_tab_content %>
                </table>
            </div>
            <div style="width:100%;height:100%">
                <%=iframe_content %>
            </div>
            <input id="aid" runat="server" type="hidden" />
            <input id="tid" runat="server" type="hidden" />
            <input id="rid" runat="server" type="hidden" />
        </div>
        
    </form>
</body>
</html>
