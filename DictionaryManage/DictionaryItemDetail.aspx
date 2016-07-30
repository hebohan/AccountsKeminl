<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictionaryItemDetail.aspx.cs" Inherits="Accounts.Web.DictionaryManage.DictionaryItemDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>字典项管理</title>
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/validate.css" />
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script>
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
        function backToList() {
            window.parent.close();
        }
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess);

            } else {
                $.dialog.alert(msg);
            }
        }

        function callsuccess() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }
    </script>
     <style>
         html,body{width: 100%;min-width: 0;}
         #MainCont{ width: 100%;min-width: 0;}
         .ListSearch{ width: 100%;}
          td {
              line-height: 25px;
          }
         .detail_edit {
             width: 70%;
             height: 25px;
             line-height: 25px;
         }
         .ListSearchInput {
             width: 30%;
         }
     </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />字典项管理<a id="lbTile" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btnSave" />
                        <asp:Button ID="btnBack" runat="server" OnClientClick="backToList()" Text="返回" CssClass="btnBack" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                 <tr>
                    <td class="ListSearchInput" style="text-align: right;">名&nbsp;&nbsp;&nbsp;&nbsp;称：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="tb_name" runat="server" class="left detail_edit" datatype="*2-20" nullmsg="请输入名称"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">代&nbsp;&nbsp;&nbsp;&nbsp;码：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="tb_code" runat="server" class="left detail_edit" datatype="*" nullmsg="请输入代码"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">是否目录：
                    </td>
                    <td class="inputtdline">
                        <asp:DropDownList runat="server" ID="ddlCatalog" class="left detail_edit" AutoPostBack="True" OnSelectedIndexChanged="ddlCatalog_OnSelectedIndexChanged">
                            <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                            <asp:ListItem Value="True" >是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="valueTR" runat="server">
                     <td class="ListSearchInput" style="text-align: right;">字&nbsp;典&nbsp;值：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="tb_value" runat="server" class="detail_edit"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">系统内置：
                    </td>
                    <td class="inputtdline">
                        <asp:DropDownList runat="server" ID="ddlSys" class="left detail_edit" >
                            <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                            <asp:ListItem Value="True" >是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">排&nbsp;&nbsp;&nbsp;&nbsp;序：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="tb_Order" runat="server" class="left detail_edit" datatype="n" nullmsg="请输入排序号"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                   <td class="ListSearchInput" style="text-align: right;">描&nbsp;&nbsp;&nbsp;&nbsp;述：
                    </td>
                    <td class="inputtdline">
                        <textarea id="tb_decride" runat="server" class="detail_edit" style="height: 150px;"></textarea>
                    </td>  
                </tr>
            </table>
            <input type="hidden" runat="server" id="parentid" />
        </div>
    </form>
</body>
</html>
