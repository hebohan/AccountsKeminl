<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountRegisterDetail.aspx.cs" Inherits="Accounts.Web.AccountsManage.AccountRegisterDetail" %>

<%@ Import Namespace="System.ComponentModel" %>
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

        function FormValide() {
            //初始化表单验证
            $("#form1").initValidform();

        }
        function backToList() {
            //var id = $('#aid').val();
            //if (id != '') {
            //    location.href = "AccountsList.aspx";

            //} else {
                history.go(-1);
            //}

        }
        $(function () {
            $('.date').bind('click', function () {
                WdatePicker();
            });
            $('.datetime').bind('click', function () {
                WdatePicker();
            });
            $('.backtip .tiptitle a').bind('click', function () {
                var d = $('#backinfo').css('display');
                if (d == "none") {
                    $('.backtip .tiptitle a').html('-');
                    $('#backinfo').show();
                } else {
                    $('.backtip .tiptitle a').html('+');
                    $('#backinfo').hide();
                }
            });
            
           
        });
        function JsPrint(msg, type, id) {
            if (type == "success_0") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess0);

            }
            else if (type == "success_1") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess1);

            }
            else if (type == "success_2") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess2);

            }
            else if (type == "success_3") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess3);

            } else if (type == "error") {
                $.dialog.tips(msg, 1, 'error.gif', callsuccess1);

            } else if (type == "save") {
                $.dialog.tips(msg, 1, 'success.gif');

            } else {
                $.dialog.alert(msg);
            }
        }
        function callsuccess3() {
            location.href = "MyAccountsList.aspx";
        }
        function callsuccess2() {
            location.href = "SummaryDisplayList.aspx";
        }
        function callsuccess1() {
            location.href = "AccountsList.aspx";
        }
        function callsuccess0() {
            var type = $('#hidtype').val();
            location.href = "../AccountManage/AccountList.aspx?atype=" + $('#hidtype').val();
        }
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

    </script>
    <style type="text/css">
        #form1{ width: 100%;height: auto;}

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
            margin: 5px 0 5px 10px ; 
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
        .hei12{
              margin-top: 50px;margin-bottom: 10px;
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
       
        #MainCont {
            width: 100%;
        }
        .ListTitle{ position: fixed;top: 0;}
        .backtip {
            position: fixed;top:45px; right: 0; width: 250px; border: 1px solid #c0c0c0; background-color: #ffffff;
        }
        .backtip .tiptitle {
            height: 30px; width: 100%; background-color: #00bfff; color: #ffffff; font-size: 13px;font-weight: bolder; 
        }
        .backtip .tiptitle span {
            margin-left: 5px; height: 30px;line-height: 30px;
        }
        .backtip .tiptitle a {
            height: 30px;line-height: 30px;float: right; margin-right: 5px;text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
        <table class="ListTitle">
            <tr>
                <td class="ListTitleStr hei12b">
                    <img src="../share/images/ListTitleBit.png" class="midImg" /><%=acid.Value == "mbadd" ? "账单信息(通过模板录入)" : "账单信息"%><a id="lbTile" runat="server"></a>
                </td>
                
                <td class="ListTitleBtn">
                    <!--------------- 在这里添加自定义的操作 ------------------->
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="FormValide();" OnClick="btnSave_Click" class="btnSave"/>
                    <input type="button" onclick="backToList()" value="取消" class="btnBack" />
                    <!-------------------------END------------------------------>
                </td>
            </tr>
        </table>
            
        <%=msg %>
            <div class="clear" style="clear:both;"></div>  
            <table id="field_tab_content" runat="server" class="ListSearch hei12 mtop">
            </table>
        <input id="aid" runat="server" type="hidden"/>
         <input id="tid" runat="server" type="hidden"/>
            <input id="acid" runat="server" type="hidden"/>
              <input type="hidden" runat="server" id="hidtype" />
            </div>
    </form>
</body>
</html>
