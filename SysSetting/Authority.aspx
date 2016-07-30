<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authority.aspx.cs" Inherits="Accounts.Web.SysSetting.Authority" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统权限</title>
    <style type="text/css">
        body
        {
            font-size: 12px;
        }
        .btabs
        {
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
    <link href="../Css/default.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.2.4/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.2.4/themes/icon.css" />
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
     <script type="text/javascript" src="../js/btns.js"></script>
    <script type="text/javascript">

        window.onload = windowHeight;
        function windowHeight() {
            var h = document.documentElement.clientHeight;
            var bodyHeight = document.getElementById("content");
            if (h < 598) {
                h = 598;
                bodyHeight.style.height = (h - 130) + "px";
            }
            else bodyHeight.style.height = (h - 130) + "px";

        }
        setInterval(windowHeight, 500)//每半秒执行一次windowHeight函数
        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        function msgShow(title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        }
        $(function () {
            $('#test').datagrid({
                width: $('#content').width()+16,
                height: document.getElementById('content').height,
                nowrap: true,
                singleSelect: true,
                rownumbers: true,
                url: '../ashx/Authority.ashx?NavigaId=0',
                idField: 'Id',
                columns: [[
                    { title: '权限名称', field: 'ButtonName', width: $(this).width() * 0.10 },
					    { field: 'rpt', title: '图标', width: $(this).width() * 0.06, align: 'center',
					        formatter: function (value, rec) {
					            return '<span class="' + rec.Icon + '">&nbsp;</span>';
					        }
					    },
					    { field: 'BtnCode', title: '权限代码', width: $(this).width() * 0.10, align: 'center' },
					    { field: 'Remark', title: '备注', width: $(this).width() * 0.65 }
				    ]]
            });
            $('#dd').dialog({
                closed: true,
                modal: true,
                title: '权限管理'
            });
            $('#dd2').dialog({
                closed: true,
                modal: true,
                title: '图标选取'
            });
        });
        function openIcon() {
            $('#dd2').dialog('open');
        }
        function ss(box) {
            $('#txtIcon').val($(box).attr('class'));
            $('#dd2').dialog('close');
        }
        function closed() {
            $('#dd').dialog('close');
        }
        function edit() {
            var node = $('#test').datagrid('getSelected');
            if (node) {
                $('#hdId').val(node.Id);
                $('#txtName').val(node.ButtonName);
                $('#txtCode').val(node.BtnCode);
                $('#txtIcon').val(node.Icon);
                $('#ss').numberspinner('setValue', node.Sort);
                $('#txtRemark').val(node.Remark);
                $('#dd').dialog('open');
            } else {
                msgShow('系统提示', '请选择要编辑的权限', 'error');
            }
        }
        function add() {
            $('#txtName').val('');
            $('#txtCode').val('');
            $('#txtIcon').val('');
            $('#ss').numberspinner('setValue', 1);
            $('#txtRemark').val('');
            $('#hdId').val('');
            $('#dd').dialog('open');
        }
        function saved() {
            var Id = $('#hdId').val();
            var Name = $('#txtName').val();
            var Code = $('#txtCode').val();
            var Icon = $('#txtIcon').val();
            var sort = $('#ss').val();
            var remark = $('#txtRemark').val();
            if (Id != '') {
                $.post('../ashx/EditAuthority.ashx?type=save&Id=' + Id + '&Name=' + encodeURI(Name) + '&Code=' + encodeURI(Code) + '&Icon=' + Icon + '&sort=' + sort + '&remark=' + encodeURI(remark), function (msg) {
                    msgShow('系统提示', '恭喜，权限修改成功！', 'info');
                    $('#dd').dialog('close');
                    $('#test').datagrid('reload');
                });
            } else {
                $.post('../ashx/EditAuthority.ashx?type=add&Name=' + encodeURI(Name) + '&Code=' + encodeURI(Code) + '&Icon=' + Icon + '&sort=' + sort + '&remark=' + encodeURI(remark), function (msg) {
                    msgShow('系统提示', '恭喜，权限添加成功！', 'info');
                    $('#dd').dialog('close');
                    $('#test').datagrid('reload');
                });
            }
        }
        function del(){
            var node = $('#test').datagrid('getSelected');
            if (node) {
                $.messager.confirm('系统提示', '删除后不可恢复，确定要删除？', function (i) {
                    if (i) {
                        $.post('../ashx/EditAuthority.ashx?type=delete&Id=' + node.Id + '&sort=' + node.Sort, function (msg) {
                            msgShow('系统提示', '恭喜，权限删除成功！', 'info');
                            $('#test').datagrid('reload');
                        });
                    }
                })
            } else {
                msgShow('系统提示', '请选择要编辑的权限', 'error');
            }
        }
    </script>
</head>
<body style="background: white">
    <form id="form1" runat="server">
    <input id="hdId" type="hidden" />
    <div class="btabs">
       
    </div>
    <div style="height: 2px">
    </div>
    <div style="width: 100%" id="content">
        <table id="test">
        </table>
    </div>
    <div id="dd" icon="icon-save" style="padding: 5px; width: 460px; height: 280px;">
        <div id="divU" style="height: 200px">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td style="width: 80px; padding-left: 5px">
                        权限名称
                    </td>
                    <td style="padding: 5px">
                        <input id="txtName" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td style="padding: 5px">
                        权限代码
                    </td>
                    <td style="padding: 5px">
                        <input id="txtCode" type="text" style="border: 1px solid #8DB2E3; width: 200px; height: 20px" />
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td style="padding: 5px">
                        图标
                    </td>
                    <td style="padding: 5px">
                        <input id="txtIcon" type="text" class="txt" readonly="readonly" style="width: 80px" />
                        <a href="javascript:void(0)" onclick="openIcon()">选取</a>
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td style="padding: 5px">
                        排序
                    </td>
                    <td style="padding: 5px">
                        <input id="ss" class="easyui-numberspinner" min="1" max="10" required="true" style="width: 80px;"></input>
                    </td>
                </tr>
                <tr style="background-color: White; height: 50px;">
                    <td style="padding: 5px">
                        备注
                    </td>
                    <td style="padding: 5px">
                        <textarea id="txtRemark" rows="3" style="width: 280px; border: 1px solid #8DB2E3;"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
            <a id="A3" class="easyui-linkbutton" onclick="saved()" icon="icon-ok" href="javascript:void(0)">
                确定</a> <a id="A4" class="easyui-linkbutton" onclick="closed()" icon="icon-cancel"
                    href="javascript:void(0)">取消</a>
        </div>
    </div>
    <div id="dd2" icon="icon-save" style="padding: 5px; width: 304px; height: 184px;">
        <span class="icon icon-edit" onclick="ss(this)">&nbsp;</span> <span class="icon icon-sys"
            onclick="ss(this)">&nbsp;</span> <span class="icon icon-set" onclick="ss(this)">&nbsp;</span>
        <span class="icon icon-add" onclick="ss(this)">&nbsp;</span> <span class="icon icon-nav"
            onclick="ss(this)">&nbsp;</span> <span class="icon icon-users" onclick="ss(this)">&nbsp;</span>
        <span class="icon icon-role" onclick="ss(this)">&nbsp;</span> <span class="icon icon-log"
            onclick="ss(this)">&nbsp;</span> <span class="icon icon-delete" onclick="ss(this)">&nbsp;</span>
        <span class="icon icon-delete2" onclick="ss(this)">&nbsp;</span> <span class="icon icon-magic"
            onclick="ss(this)">&nbsp;</span> <span class="icon icon-database" onclick="ss(this)">
                &nbsp;</span> <span class="icon icon-Distribution" onclick="ss(this)">&nbsp;</span>
        <span class="icon icon-Pass" onclick="ss(this)">&nbsp;</span> <span class="icon icon-Browse"
            onclick="ss(this)">&nbsp;</span>
    </div>
    </form>
</body>
</html>
