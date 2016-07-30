<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Accounts.Web.SysSetting.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body {
            width: 98%;
            font-size: 12px;
        }

        .btabs {
            border: 1px solid #8DB2E3;
            font-size: 12px;
            height: 26px;
            list-style-type: none;
            margin: 0;
            padding: 4px 0 0 4px;
            width: 99.5%;
            background-color: #E0ECFF;
        }
    </style>
    <link href="../js/Treetable_files/jqtreetable.css" rel="stylesheet" type="text/css" />
    <link href="../Css/default.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/metro/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/icon.css" />
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
    <script src="../js/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/Treetable_files/jqtreetable.js"></script>
    <script type="text/javascript" src="../js/btns.js"></script>
    <script type="text/javascript">
        window.onload = windowHeight; //页面载入完毕执行函数
        function windowHeight() {
            var h = document.documentElement.clientHeight;
            var bodyHeight = document.getElementById("content");
            if (h < 598) {
                h = 598;
                bodyHeight.style.height = (h - 130) + "px";
            }
            //else bodyHeight.style.height = (h - 130) + "px";
        }
        setInterval(windowHeight, 500)//每半秒执行一次windowHeight函数

        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        function msgShow(title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        }
        $(function () {
            $('#dd').dialog({
                closed: true,
                modal: true,
                title: '用户管理'
            });
            $('#dd2').dialog({
                closed: true,
                modal: true,
                title: '修改密码'
            });


            $('#tt').datagrid({
                width: document.getElementById('div1').width,
                height: document.getElementById('div1').heigh,
                nowrap: false,
                striped: true,
                collapsible: true,
                remoteSort: false,
                singleSelect: true,
                sortName: 'Userid',
                sortOrder: 'desc',
                idField: 'Id',
                url: '../ashx/UserListHandler.ashx',
                columns: [[
                { field: 'Sort', title: '排序', width: $(this).width() * 0.10 },
                { field: 'LoginName', title: '用户名', width: $(this).width() * 0.15 },
                { field: 'UserRealName', title: '真实姓名', width: $(this).width() * 0.15 },
                { field: 'Sex', title: '性别', width: $(this).width() * 0.10 },
                { field: 'Mobile', title: '手机号', width: $(this).width() * 0.15 },
                { field: 'Email', title: '邮箱', width: $(this).width() * 0.15 },
                { field: 'Roles', title: '角色', width: $(this).width() * 0.10 },
                { field: 'Status', title: '状态', width: $(this).width() * 0.10 }
                ]],
                pagination: true
            });
            $('#Role').combogrid({
                panelWidth: 280,
                multiple: true,
                idField: 'Id',
                textField: 'Name',
                url: '../ashx/RoleList.ashx',
                columns: [[
					{ checkbox: 'true' },
					{ field: 'Name', title: '角色名称', width: 100 },
                    { field: 'Remark', title: '角色说明', width: 130 }
                ]]
            });
        });
        function close1() {
            $('#dd').dialog('close');
        }
        function close2() {
            $('#dd2').dialog('close');
        }
        function edit() {
            var node = $('#tt').datagrid('getSelected');
            if (node) {
                $('#hdId').val(node.Userid);
                $('#txtLogin').val(node.LoginName);
                $('#txtPass').val("txtPass");
                $('#txtPass2').val("txtPass");
                $('#password1_td').hide();
                $('#password2_td').hide();
                $('#txtName').val(node.UserRealName);
                if (node.Sex == '1')
                    $('#Radio1').attr('checked', 'checked');
                else
                    $('#Radio2').attr('checked', 'checked');
                $('#txtMail').val(node.Mail);
                $('#txtTel').val(node.Tel);
                $('#txtMobile').val(node.Mobile);
                $('#sort').val(node.Sort);
                if (node.Status == '禁用') {
                    $('#Radio4').attr('checked', 'checked');
                } else if (node.Status == '正常') {
                    $('#Radio3').attr('checked', 'checked');
                } else {
                    $('#Radio5').attr('checked', 'checked');
                }
                $("#Role").combogrid("clear");
                var str = new Array();
                $.post('../ashx/UsersHandler.ashx?type=edit&Id=' + node.Userid, function (msg) {
                    str = msg.split(',');
                    for (var i = 0; i < str.length; i++) {
                        $('#Role').combogrid('setValue', str[i]);
                    }
                });
                $('#dd').dialog('open');
            } else {
                $.messager.alert('系统提示', '请选择要编辑的用户', 'error');
            }
        }
        function del() {
            var selected = $('#tt').datagrid('getSelected');
            if (selected) {
                $.messager.confirm('系统提示', '删除后不可恢复，您确定要删除吗?', function (r) {
                    if (r) {
                        $.post('../ashx/UsersHandler.ashx?type=del&Id=' + selected.Userid, function (msg) {
                            if (msg == 'true') {
                                $.messager.alert('系统提示', '删除成功', 'info');
                                $('#tt').datagrid('reload');
                            } else {
                                $.messager.alert('系统提示', '删除失败，请稍后重试', 'info');
                            }
                        });
                    }
                });

            } else {
                $.messager.alert('系统提示', '请选择要删除的用户', 'error');
            }
        }
        function Save() {
            var login = $('#txtLogin').val();
            var name = $('#txtName').val();
            var sex = $("input[name='rd']:checked").val();
            var email = $('#txtMail').val();
            var tel = $('#txtTel').val();
            var mobile = $('#txtMobile').val();
            var sort = $('#sort').val();
            var status = $("input[name='rdStatus']:checked").val();
            if (login == '') {
                $.messager.alert('系统提示', '请输入账号', 'error');
                return false;
            } else if (name == '') {
                $.messager.alert('系统提示', '请输入姓名', 'error');
                return false;
            }
            else {
                var Role = '';
                var nodes = $('#Role').combogrid('getValues');
                for (var i = 0; i < nodes.length; i++) {
                    if (Role != '')
                        Role += ",";
                    Role += nodes[i];
                }
                
                if (Role != '') {
                    var userid = $('#hdId').val();
                    $.post('../ashx/UsersHandler.ashx?type=save&Userid=' + userid + '&login=' + encodeURI(login) + "&name=" + encodeURI(name) + "&sex=" + sex + "&email=" + email + "&tel=" + tel + "&mobile=" + mobile + "&sort=" + sort + "&status=" + status + '&role=' + Role, function (msg) {
                        if (userid != null && userid != "") {
                            $.messager.alert('系统提示', '修改成功', 'info');
                            $('#dd').dialog('close');
                            $('#tt').datagrid('reload');
                        } else {
                            if (msg == 'false') {
                                $.messager.alert('系统提示', '账号已存在，请重新输入账号', 'error');
                                $('#txtLogin').val('');
                            } else {
                                $.messager.alert('系统提示', '添加成功', 'info');
                                $('#dd').dialog('close');
                                $('#tt').datagrid('reload');
                            }
                        }
                    });
                } else {
                    $.messager.alert('系统提示', '请选用户角色', 'error');
                    return false;
                }
            }
        }

        function Upd1() {       //修改密码
            var node = $('#tt').datagrid('getSelected');
            $('#newPass').val('');
            if (node) {
                $('#hdId').val(node.Userid);
                $('#dd2').dialog('open');
            } else {
                $.messager.alert('系统提示', '请选择要编辑的用户', 'error');
            }
        }

        function saveRole() {
            var nodes = $('#tt3').tree('getChecked');
            var s = '';
            for (var i = 0; i < nodes.length; i++) {
                if (s != '') s += ',';
                s += nodes[i].id;
            }
            var selected = $('#tt').datagrid('getSelected');
            $.post('../ashx/UsersHandler.ashx?type=role&UserId=' + selected.Userid + '&roleId=' + s, function (msg) {
                $.messager.alert('系统提示', '分配角色成功', 'info');
                close4();
            });
        }
        function savePass() {
            var selected = $('#tt').datagrid('getSelected');
            var pass = $('#newPass').val();
            var pass2 = $('#newPass2').val();
            if (pass == '') {
                $.messager.alert('系统提示', '请输入新密码', 'error');
            } else {
                if (pass == pass2) {
                    $.post('../ashx/UsersHandler.ashx?type=pass&UserId=' + selected.Userid + '&pass=' + pass, function (msg) {
                        if (msg == "true") {
                            $.messager.alert('系统提示', '密码修改成功,新密码为:' + pass, 'info');
                            close2();
                        } else {
                            $.messager.alert('系统提示', '密码修改失败，请稍后重试', 'info');
                            close2();
                        }
                    });
                } else {
                    $.messager.alert('系统提示', '两次输入密码不一致，请重新输入', 'error');
                    $('#newPass2').val('');
                    $('#newPass').val('');
                }
            }
        }
    </script>
</head>
<body style="background-color: White;">
    <input id="hdId" type="hidden" />
    <div style="height: 100%">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>

                <td valign="top">
                    <div class="btabs">
                        <a href="javascript:void(0)" onclick="Upd1()"><span class="icon icon-Pass">&nbsp;</span>修改密码</a>
                    </div>
                    <div style="height: 2px">
                    </div>
                    <div id="div1" style="width: 100%; height: 100%">
                        <table id="tt">
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <form action="../ashx/UsersHandler.ashx?type=save" id="form1">
        <div id="dd" icon="icon-save" style="padding: 5px; width: 360px; height: 530px;">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right" style="width: 80px;">账号:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtLogin" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;" id="password1_td">
                    <td align="right" style="width: 80px;">密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtPass" type="password" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;" id="password2_td">
                    <td align="right" style="width: 80px;">密码确认:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtPass2" type="password" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td align="right" style="width: 80px;">姓名:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtName" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td align="right">性别:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input name="rd" id="Radio1" type="radio" value="1" checked="checked"/>男&nbsp;
                        <input name="rd" id="Radio2" type="radio" value="2" />女
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">排序:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="sort" class="easyui-numberspinner" min="1" max="10000" required="true" value="1" style="width: 80px;"></input>
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">角色:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <select id="Role" name="dept" style="width: 200px;"></select>*
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">Email:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtMail" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">电话:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtTel" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">手机:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtMobile" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">状态:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="Radio3" name="rdStatus" type="radio" value="1" />启用&nbsp;
                    <input id="Radio4" name="rdStatus" type="radio" value="0" />禁用
                    <input id="Radio5" name="rdStatus" type="radio" value="2" />禁止登录
                    </td>
                </tr>
            </table>
            <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
                <a id="A1" class="easyui-linkbutton" onclick="Save()" icon="icon-ok" href="javascript:void(0)">确定</a> <a id="A2" class="easyui-linkbutton" onclick="close1()" icon="icon-cancel"
                    href="javascript:void(0)">取消</a>
            </div>
        </div>
    </form>
    <div id="dd2" icon="icon-save" style="padding: 5px; width: 360px; height: 177px;">
        <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
            <tr style="background-color: White; height: 32px;">
                <td align="right" style="width: 80px;">新密码:&nbsp;&nbsp;
                </td>
                <td style="padding: 5px">
                    <input id="newPass" type="password" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                </td>
            </tr>
            <tr style="background-color: White; height: 32px;">
                <td align="right" style="width: 80px;">密码确认:&nbsp;&nbsp;
                </td>
                <td style="padding: 5px">
                    <input id="newPass2" type="password" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                </td>
            </tr>
        </table>
        <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
            <a id="A3" class="easyui-linkbutton" onclick="savePass()" icon="icon-ok" href="javascript:void(0)">确定</a> <a id="A4" class="easyui-linkbutton" onclick="close2()" icon="icon-cancel"
                href="javascript:void(0)">取消</a>
        </div>
    </div>
</body>
</html>
