<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewReminder.aspx.cs" Inherits="Accounts.Web.ReminderManage.NewReminder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增提醒事项</title>
    <link type="text/css" rel="stylesheet" href="../Share/css/NK1.1.css?v=0.7"/>
    <link type="text/css" rel="stylesheet" href="../Share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../Share/css/validate.css" />
    <script type="text/javascript" src="../Share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../Share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../Share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../Share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script language="javascript" type="text/javascript" src="../Share/js/My97DatePicker/WdatePicker.js"></script>
    <script>
        function backToList() {
            window.parent.close();
        }
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess);

            }
            else if(type == "close")
            {
                $.dialog.tips(msg, 1, 'error.gif', callfail);
            }
            else {
                $.dialog.alert(msg);
            }
        }

        function callsuccess() {

        }

        function callfail() {
            var api = top.dialog.get(window); //获取父窗体对象
            api.close().remove();
        }
        function AddReminder() {
            var remindman = $('#remindman').val();
            var title = $('#title').val();
            var content = $('#content').val();
            var userid = $('#hiduserid').val();
            var type = $('#hidtype').val();
            var remindid = $('#hidremindid').val();
            var remindtime = $('#Date').val() + " " + $('#hour').val() + ":" + $('#minute').val();
            if (remindman == "" || title == "" || content == "" || $('#Date').val() == "(点击选择日期)"|$('#Date').val() == "" || $('#hour').val() == "" || $('#minute').val() == "") {
                $.dialog.alert("请不要留空信息！");
                return;
            }
            else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(remindman)) {
                $.dialog.alert("提醒对象请输入邮箱！");
                return;
            }
            else if (Date.parse(remindtime) < new Date().getTime()) {
                $.dialog.alert("提醒时间必须大于当前时间！");
                return;
            }
            if (remindman != "" && title != "" && content != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=addreminder",
                        data: { "remindman": remindman, "title": title, "content": content, "type": type, "userid": userid, "remindtime": remindtime, "editid": remindid },
                        dataType: "text",
                        success: function (data) {
                            if (data == "add_true") {
                                alert("添加提醒成功");
                                var api = top.dialog.get(window); //获取父窗体对象
                                api.close().remove();
                            } else if (data == "edit_true") {
                                alert("修改提醒成功");
                                var api = top.dialog.get(window); //获取父窗体对象
                                api.close().remove();
                            }
                            else {
                                alert(data);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("添加提醒成功");
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
            api.close().remove();
        }
    </script>
</head>
<body style="overflow:hidden" >
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle" style="width:40%">
                <tr>
                    <td class="ListTitleStr hei12b" style="text-align:center">
                        <img src="../Share/images/ListTitleBit.png" class="midImg" /><%=hidtype.Value == "add"?"新增":"修改" %>提醒事项<a id="lbTile" runat="server"></a>
                    </td>
<%--                    <td  style="text-align:right;font-size:4px">
                        <p id="user">未登录&nbsp;&nbsp;&nbsp;</p>
                    </td>--%>
                </tr>
            </table>
            <table class="ListSearch hei12" style="padding-left:20px;" >
                <tr>
                    <td class="ListSearchInput" >提醒对象：
                    </td>           
                    <td><input type="text" id="remindman" style="width:250px;" runat="server"/>
                        <label id="remindman_label" runat="server" style="display:none;width:250px;"></label>
                    </td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" >标&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;题：
                    </td>           
                    <td><input type="text" id="title" style="width:250px;" runat="server"/>
                        <label id="title_label" runat="server" style="display:none;width:250px;"></label>
                    </td>       
                </tr>
                <tr>
                    <td class="ListSearchInput">内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：
                    </td>           
                    <td><textarea style="width:250px;height:90px" id="content" runat="server"></textarea>
                    </td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" >提醒时间：
                    </td>           
                    <td><div id="time_panel" runat="server"><input type="text" runat="server" id="Date" readonly="readonly" class="am-form-field login-input-text" onclick="WdatePicker()" value="(点击选择日期)" style="width:120px" />
                        &nbsp;<select id="hour" runat="server"></select>&nbsp;时
                        <select id="minute" runat="server"></select>&nbsp;分</div>
                        <label id="remindtime_label" runat="server" style="display:none;width:250px;"></label>
                    </td>       
                </tr>
            </table>
            <div style="padding-left:120px;" runat="server" id="btnpanel">
                <input type="button" id="btnSend" onclick="AddReminder();" class="btnOK" value="添加" runat="server"/>
                <input type="button" id="btnCancle" onclick="Cancle();" class="btnDel" value="取消" runat="server"/>
            </div>
            
        </div>
        <input type="hidden" id="hidremindid" runat="server" />
        <input type="hidden" id="hidremark" runat="server" />
        <input type="hidden" id="hidname" runat="server" />
        <input type="hidden" id="hiduserid" runat="server" />
        <input type="hidden" id="hidtype" runat="server" />

    </form>
    
</body>
</html>
