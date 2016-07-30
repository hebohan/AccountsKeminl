<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMail.aspx.cs" Inherits="Accounts.Web.SysSetting.NewMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建邮件</title>
    <link type="text/css" rel="stylesheet" href="../Share/css/NK1.1.css?v=0.7"/>
    <link type="text/css" rel="stylesheet" href="../Share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../Share/css/validate.css" />
    <script type="text/javascript" src="../Share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../Share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../Share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../Share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script>
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

        function SendMail() {
            var receiver = $('#receiver').val();
            var title = $('#title').val();
            var content = $('#content').val();
            if (receiver != "" && title != "" && content != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=sendnewmail",
                        data: { "receiver": receiver, "title": title, "content": content },
                        dataType: "text",
                        success: function (data) {
                            if (data == "true") {
                                alert("发送邮件成功");
                                var api = top.dialog.get(window); //获取父窗体对象
                                api.close().remove();
                            } else {
                                alert("发送邮件失败");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("发送邮件成功");
                            var api = top.dialog.get(window); //获取父窗体对象
                            api.close().remove();
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            //window.parent.location.reload();
        }

        function Cancle()
        {
            var api = top.dialog.get(window); //获取父窗体对象
            api.close();
        }
    </script>
</head>
<body style="overflow:hidden" >
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle" style="width:40%">
                <tr>
                    <td class="ListTitleStr hei12b" style="text-align:center">
                        <img src="../Share/images/ListTitleBit.png" class="midImg" />新邮件<a id="lbTile" runat="server"></a>
                    </td>
<%--                    <td  style="text-align:right;font-size:4px">
                        <p id="user">未登录&nbsp;&nbsp;&nbsp;</p>
                    </td>--%>
                </tr>
            </table>
            <table class="ListSearch hei12" style="padding-left:20px" >
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">收件人：
                    </td>           
                    <td><input type="text" id="receiver" style="width:250px;"/></td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">标题：
                    </td>           
                    <td><input type="text" id="title" style="width:250px;"/></td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">内容：
                    </td>           
                    <td><textarea style="width:250px;height:90px" id="content" runat="server"></textarea></td>       
                </tr>
            </table>
            <div style="padding-left:120px">
                <input type="button" id="btnSend" onclick="SendMail();" class="btnOK" value="发送"/>
                <input type="button" id="btnCancle" onclick="Cancle();" class="btnDel" value="取消"/>
            </div>
            
        </div>
        <input type="hidden" id="hidrecordid" runat="server" />
        <input type="hidden" id="hidremark" runat="server" />
        <input type="hidden" id="hidname" runat="server" />
    </form>
    
</body>
</html>
