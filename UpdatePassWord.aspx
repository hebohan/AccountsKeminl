<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassWord.aspx.cs" Inherits="Accounts.Web.UpdatePassWord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账户密码修改</title>
    <link type="text/css" rel="stylesheet" href="/share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="/share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="/share/css/validate.css" />
    <script language="javascript" type="text/javascript" src="/share/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="/share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="/share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
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
        }
    </script>
     <style>
         #MainCont{ width: 100%;}
          td {
              line-height: 25px;
          }
          .ListSearch{ width: 100%;}
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
                        <img src="/share/images/ListTitleBit.png" class="midImg" />账户密码修改<a id="lbTile" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btnSave" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">旧&nbsp;密&nbsp;码：
                    </td>
                    <td class="inputtdline">
                        <input type="password" runat="server" id="oldpassword" value="" name="oldpassword" class="left detail_edit" datatype="*" nullmsg="请输入旧密码！" />
                    </td>                    
                </tr>
                 <tr>
                    <td class="ListSearchInput" style="text-align: right;">新&nbsp;密&nbsp;码：
                    </td>
                    <td class="inputtdline">
                        <input type="password" runat="server" id="userpassword" value="" name="userpassword" class="left detail_edit" datatype="*6-16" errormsg="密码范围在6~16位之间！" />
                    </td>                    
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">确认密码：
                    </td>
                    <td class="inputtdline">
                        <input type="password" runat="server" id="userpassword2" value="" name="userpassword2" class="left detail_edit" datatype="*" recheck="userpassword" errormsg="您两次输入的密码不一致！" />
                    </td>                    
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
