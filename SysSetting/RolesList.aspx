<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolesList.aspx.cs" Inherits="Accounts.Web.SysSetting.RolesList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-size: 12px;
            width: 98%;
        }
        .btabs
        {
            border: 1px solid #8DB2E3;
            font-size: 12px;
            height: 26px;
            list-style-type: none;
            margin: 0;
            padding: 4px 0 0 4px;
            width: 100%;
            background-color: #E0ECFF;
        }
        .selColor2
        {
            background-color: #EFF3FB;
        }
        .selColor3
        {
            background-color: White;
        }
        .selColor4
        {
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
    <%--<script type="text/javascript" src="../js/Treetable_files/jqtreetable.js"></script>--%>
    <script type="text/javascript" src="../js/btns.js"></script>
    <script type="text/javascript">
        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        function msgShow(title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        }
        var type = '';
        $(function () {
            $('#dd').dialog({
                closed: true,
                title: '角色成员'
            });
            $('#tt').datagrid({
                width: document.getElementById('div1').width,
                height: document.getElementById('div1').heigh,
                nowrap: false,
                striped: true,
                collapsible: true,
                remoteSort: false,
                singleSelect: true,
                sortName: 'Id',
                sortOrder: 'desc',
                idField: 'Id',
                url: '../ashx/RoleList.ashx',
                columns: [[
                { field: 'Name', title: '角色名称', width: $(this).width() * 0.2 },
                { field: 'Remark', title: '描述', width: $(this).width() * 0.75 }
                ]],
                pagination: true
            });
        });
        function closedd() {
            $('#dd').dialog('close');
        }
        function ss(userId) {
            $('#HdUserId').val(userId);
            $('#divUser div').css({ "background-color": '' });
            $('#divUser').find('input').each(function () {
                if ($(this).val() == userId) {
                    $(this).parent('div').css({ "background-color": '#3399ff' });
                }
            })
        }
        function openIcon() {
            $('#dd2').dialog('open');
        }
        function add() {
            type = 'add';
            $('#txtName').val('请在此输入角色名称');
            $('#txtRemark').val('');
            $('#divUser').html('');
            $('#dd').dialog('open');
        }
        function edit() {
            var node = $('#tt').datagrid('getSelected');
            if (node != null) {
                //获取角色信息
                $.post('../ashx/EditRoles.ashx?type=edit&Id=' + node.Id, function (msg) {
                    var str = msg.split(',');
                    $('#txtName').val(str[0]);
                    $('#txtRemark').val(str[1]);
                    $('#divUser').html(str[2]);
                    $('#dd').dialog('open');
                    type = 'save';
                });
            } else {
                msgShow('系统提示', '请选择要编辑的角色', 'error');
            }
        }
        //添加/保存角色信息
        function saveRole() {
            var RoleName = $('#txtName').val();
            if (RoleName != '' && RoleName != '请在此输入角色名称') {
                var remark = $('#txtRemark').val();
                var Id = null;
                var node = $('#tt').datagrid('getSelected');
                if (node != null)
                    Id = node.Id;
                $.post('../ashx/EditRoles.ashx?type='+type+'&Id=' + Id + '&name=' + encodeURI(RoleName) + '&remark=' + encodeURI(remark), function (msg) {
                    msgShow('系统提示', '角色编辑成功', 'info');
                    $('#dd').dialog('close');
                    $('#tt').datagrid('reload');
                });
            } else {
                msgShow('系统提示', '请输入角色名称', 'error');
            }
        }
        function del() {
            var node = $('#tt').datagrid('getSelected');
            if (node != null) {
                $.messager.confirm('系统提示', '删除后不可恢复，确定要删除？', function (i) {
                    if (i) {
                        $.post('../ashx/EditRoles.ashx?type=delRole&Id=' + node.Id, function (msg) {
                            if (msg) {
                                msgShow('系统提示', '角色删除成功', 'info');
                                window.location.href = "RolesList.aspx";
                                $('#tt').datagrid('reload');
                            } else {
                                msgShow('系统提示', '删除失败，请稍后重试！', 'info');
                            }
                        });
                    }
                })
                
            } else {
                msgShow('系统提示', '请选择要删除的角色', 'error');
            }
        }
        function Distribution1() {
            var node = $('#tt').datagrid('getSelected');
            if (node != null) {
                window.location.href = 'NavigationRole.aspx?RoleId=' + node.Id;
            } else {
                msgShow('系统提示', '请选择要授权的角色', 'error');
            }
        }
        function clear() { 
               var RoleName = $('#txtName').val();
               if (RoleName == '请在此输入角色名称') {
                   $('#txtName').val('');
               }
        }
    </script>
</head>
<body style="background: white">
    <form id="form1" runat="server">
    <input id="HdId" type="hidden" />
    <input id="HdUserId" type="hidden" />
    <div class="btabs">
        <span class="icon icon-Distribution">&nbsp;</span><a href="javascript:void(0)" onclick="Distribution1();return false;">授权</a>
    </div>
    <div style="height: 2px">
    </div>
    <div id="div1" style="width: 100.5%">
         <table id="tt"></table>
    </div>
    <div id="dd" icon="icon-save" style="padding: 5px; width: 360px; height: 380px;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
            <tr>
                <td style="padding: 5px">
                    <span class="icon icon-users">&nbsp;</span>
                    <input id="txtName" type="text" style="border: 0px solid; width: 240px; height: 18px;
                        font-size: medium;" value="请在此角色名称" onfocus="clear()" onclick="clear()" />
                    <br />
                    <hr />
                    角色描述:<input id="txtRemark" type="text" style="border: 1px solid gray; width: 239px;
                        height: 18px" /><br />
                    成员：<br />
                    <div id="divUser" style="width: 93%; overflow: auto; height: 200px; border: 1px solid gray">
                    </div>
                </td>
            </tr>
        </table>
        <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
            <a id="A1" class="easyui-linkbutton" onclick="saveRole()" icon="icon-ok" href="javascript:void(0)">
                确定</a> <a id="A2" class="easyui-linkbutton" onclick="closedd()" icon="icon-cancel"
                    href="javascript:void(0)">取消</a>
        </div>
    </div>
    </form>
</body>
</html>
