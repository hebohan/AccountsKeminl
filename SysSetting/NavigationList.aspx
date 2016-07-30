<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationList.aspx.cs" Inherits="Accounts.Web.SysSetting.NavigationList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-size: 12px;
            width: 98%;
            height: auto;
        }

        .btabs {
            border: 1px solid #8DB2E3;
            font-size: 12px;
            height: 26px;
            list-style-type: none;
            margin: 0;
            padding: 4px 0 0 4px;
            width: 100%;
            background-color: #E0ECFF;
        }
    </style>
    <link href="../js/Treetable_files/jqtreetable.css" rel="stylesheet" type="text/css" />
    <link href="../Css/default.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/metro/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/icon.css" />
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
    <script src="../JS/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/Treetable_files/jqtreetable.js"></script>
    <script type="text/javascript" src="../js/btns.js"></script>
    <script type="text/javascript">
        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        function msgShow(title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        }
        $(function () {
            $('#dd').dialog({
                closed: true,
                modal: true,
                title: '菜单管理'
            });
            $('#dd2').dialog({
                closed: true,
                modal: true,
                title: '图标选取'
            });
            $('#dd3').dialog({
                closed: true,
                modal: true,
                title: '权限管理'
            });
            $('#test').datagrid({
                width: 435,
                height: 270,
                nowrap: true,
                singleSelect: false,
                rownumbers: true,
                param: true,
                url: '../ashx/Authority.ashx',
                idField: 'Id',
                columns: [[
                    { field: 'ck', checkbox: true },
                    { title: '权限名称', field: 'ButtonName', width: 80 },
                    {
                        field: 'rpt', title: '图标', width: 50, align: 'center',
                        formatter: function (value, rec) {
                            return '<span class="' + rec.Icon + '">&nbsp;</span>';
                        }
                    },
                    { field: 'BtnCode', title: '权限代码', width: 60, align: 'center' },
                    { field: 'Remark', title: '备注', width: 150 }
                ]]
            });
        });
        $(function () {
            var ei = $("#large");
            ei.hide();
            $(".CatalogImg").mousemove(function (e) {
                ei.css({ top: e.pageY, left: e.pageX }).html('<img style="border:0px solid gray;" src="' + this.src + '" />').show();
            }).mouseout(function () {
                ei.hide();
            })
            //获取根节点
            $.post('../ashx/EditNavigation.ashx?type=Parent', function (msg) {
                $('#selParent').html(msg);
            });
        });
        //获取修改的信息
        function open1(Id) {
            $('#dd').dialog('open');
            $('#HdId').val(Id);
            $.post('../ashx/EditNavigation.ashx?type=edit&Id=' + Id, function (msg) {
                var str = msg.split(',');
                $('#txtMenuName').val(str[0]);
                $('#txtLogo').val(str[1]);
                if (str[2] == "0") {
                    $('#selParent').attr('disabled', 'disabled');
                    $('#txtLink').attr('disabled', 'disabled');
                } else {
                    $('#selParent').val(str[2]);
                    document.getElementById('selParent').disabled = false;
                    document.getElementById('txtLink').disabled = false;
                }
                $('#txtLink').val(str[3]);
                $('#txtIcon').val(str[4]);
                $('#selSort').html(str[5]);
                if (str[6] == '0')
                    $('#chkItem').attr('checked', 'checked');
            })
        }
        //添加菜单
        function add() {
            $('#HdId').val('');
            $('#txtMenuName').val('');
            $('#txtLogo').val('');
            $('#txtLink').val('');
            $('#txtIcon').val('');
            $('#dd').dialog('open');
        }
        //获取子节点的选项
        function sort(box) {
            if (box.value == "0") {
                $('#txtLink').attr('disabled', 'disabled');
            } else {
                document.getElementById('txtLink').disabled = false;
            }
            $.post('../ashx/EditNavigation.ashx?type=child&Id=' + box.value, function (msg) {
                var str = msg.split(',');
                $('#selSort').html(str[0]);
            });
        }
        function closePersonal() {
            $('#dd').dialog('close');
        }
        //保存修改或添加
        function serverEdit() {
            var Id = $('#HdId').val();
            var MenuName = $('#txtMenuName').val();
            var Logo = $('#txtLogo').val();
            var Parent = $('#selParent').val();
            var Link = $('#txtLink').val();
            var Icon = $('#txtIcon').val();
            var sort = $('#selSort').val();
            var IsShow = 1;
            if (document.getElementById('chkItem').checked) {
                IsShow = 0;
            }
            if (Id != '') {
                $.post('../ashx/EditNavigation.ashx?type=save&Id=' + Id + '&Name=' + encodeURI(MenuName) + '&logo=' + encodeURI(Logo) + '&Parent=' + Parent + '&Link=' + encodeURI(Link) + '&icon=' + Icon + '&sort=' + sort + '&Isshow=' + IsShow, function (msg) {
                    msgShow('系统提示', '恭喜，菜单修改成功！', 'info');
                    window.location.href = "NavigationList.aspx";
                });
            } else {
                $.post('../ashx/EditNavigation.ashx?type=add&Name=' + encodeURI(MenuName) + '&logo=' + encodeURI(Logo) + '&Parent=' + Parent + '&Link=' + encodeURI(Link) + '&icon=' + Icon + '&sort=' + sort + '&Isshow=' + IsShow, function (msg) {
                    msgShow('系统提示', '恭喜，菜单添加成功！', 'info');
                    window.location.href = "NavigationList.aspx";
                });
            }
        }
        function ss(box) {
            $('#txtIcon').val($(box).attr('class'));
            $('#dd2').dialog('close');
        }
        function openIcon() {
            $('#dd2').dialog('open');
        }
        function authority(Id) {
            $('#HdId').val(Id);
            //var query = { NavigaId:Id };                        //把查询条件拼接成JSON
            //$("#test").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            //$("#test").datagrid('reload', { NavigaId: Id });                      //重新加载
            $.post('../ashx/Authority.ashx?type=auth&NavigaId=' + Id, function (msg) {
                var str = msg.split(',');
                $("#test").datagrid('clearSelections');
                for (var i = 0; i < str.length; i++) {
                    $("#test").datagrid('selectRecord', str[i]);
                }
            })
            $('#dd3').dialog('open');
        }
        function saved() {
            var nodes = $('#test').datagrid('getSelections');
            var str = '';
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                if (str != '')
                    str += ',';
                str += node.Id;
            }
            var Id = $('#HdId').val();
            $.post('../ashx/Authority.ashx?type=save&Id=' + Id + '&Autid=' + str, function (msg) {
                msgShow('系统提示', '恭喜，权限修改成功！', 'info');
                window.location.href = "NavigationList.aspx";
            });
        }
        function closed() {
            $('#dd3').dialog('close');
        }
    </script>
</head>
<body style="background: white">
    <form id="form1" runat="server">
        <input id="HdId" type="hidden" />
        <!--JQTreeTable-->
        <script type="text/javascript">
            $(function () {
                //声明数组
                var map = [<%=strMap %>];
           //声明参数选项
           var options = { openImg: "../images/TreeTable/tv-collapsable.gif", shutImg: "../images/TreeTable/tv-expandable.gif", leafImg: "../images/TreeTable/tv-item.gif", lastOpenImg: "../images/TreeTable/tv-collapsable-last.gif", lastShutImg: "../images/TreeTable/tv-expandable-last.gif", lastLeafImg: "../images/TreeTable/tv-item-last.gif", vertLineImg: "../images/TreeTable/vertline.gif", blankImg: "../images/TreeTable/blank.gif", collapse: true, column: 0, striped: false, highlight: true, onselect: function (target) { } };
           if (map != null && map.length > 0) {
               //根据参数显示树
               $("#treetable").jqTreeTable(map, options);
           }
       });
        </script>
        <div class="btabs">
        </div>
        <div style="height: 2px"></div>
        <div style="width: 100%; border: 0px">
            <asp:Repeater ID="rptList" runat="server"
                OnItemDataBound="rptList_ItemDataBound">
                <HeaderTemplate>
                    <table id="tablemain" cellpadding="0" cellspacing="1px" border="0" style="width: 100.5%;" bgcolor="b5d6e6">
                        <thead>
                            <tr style="background-color: #E0ECFF; height: 26px">
                                <th style="width: 25%">名称</th>
                                <th style="width: 10%">标识</th>
                                <th style="width: 20%">链接地址</th>
                                <th style="width: 10%">是否显示</th>
                                <th style="width: 10%">排序</th>
                                <th style="width: 25%">管理</th>
                            </tr>
                        </thead>
                        <tbody id="treetable">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: #EFF3FB; height: 23px;">
                        <td>
                            <span class='<%#Eval("Icon")%>'>&nbsp;</span><%#Eval("MenuName")%></td>
                        <td align="center">
                            <%#Eval("Pagelogo")%>
                        </td>
                        <td align="center">
                            <%#Eval("LinkAddress")%>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblShow" runat="server" Text=' <%#Eval("IsShow")%>'></asp:Label>
                        </td>
                        <td align="center">
                            <%#Eval("Sort")%>
                        </td>
                        <td align="center">
                            <span class="icon icon-edit">&nbsp;</span><a href="javascript:void(0)" onclick='<%#"open1("+Eval("Id")+")" %>'>编辑</a>
                            <span class="icon icon-delete2">&nbsp;</span>
                            <asp:LinkButton ID="lbtnDel" OnClientClick="return confirm('确定要删除该记录吗？');" OnCommand="LinkButton2_Command" CommandArgument='<%#Eval("Id")%>' runat="server">删除</asp:LinkButton>
                            <span class="icon icon-Distribution">&nbsp;</span><a href="javascript:void(0)" onclick='<%#"authority("+Eval("Id")+")" %>'>权限分配</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color: White; height: 23px;">
                        <td>
                            <span class='<%#Eval("Icon")%>'>&nbsp;</span><%#Eval("MenuName")%></td>
                        <td align="center">
                            <%#Eval("Pagelogo")%>
                        </td>
                        <td align="center">
                            <%#Eval("LinkAddress")%>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblShow" runat="server" Text=' <%#Eval("IsShow")%>'></asp:Label>
                        </td>
                        <td align="center"><%#Eval("Sort")%>
                        </td>
                        <td align="center">
                            <span class="icon icon-edit">&nbsp;</span><a href="javascript:void(0)" onclick='<%#"open1("+Eval("Id")+")" %>'>编辑</a>
                            <span class="icon icon-delete2">&nbsp;</span>
                            <asp:LinkButton ID="lbtnDel" OnClientClick="return confirm('确定要删除该记录吗？');" OnCommand="LinkButton2_Command" CommandArgument='<%#Eval("Id")%>' runat="server">删除</asp:LinkButton>
                            <span class="icon icon-Distribution">&nbsp;</span><a href="javascript:void(0)" onclick='<%#"authority("+Eval("Id")+")" %>'>权限分配</a>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </TBODY> </TABLE>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div id="dd" icon="icon-save" style="padding: 5px; width: 360px; height: 355px;">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right" style="width: 80px;">菜单名称:&nbsp;&nbsp;</td>
                    <td style="padding: 5px">
                        <input id="txtMenuName" type="text" class="txt" style="width: 120px" /></td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td align="right">页面标识:&nbsp;&nbsp;</td>
                    <td style="padding: 5px">
                        <input id="txtLogo" type="text" class="txt" style="width: 120px" /></td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">上级菜单:&nbsp;&nbsp;</td>
                    <td style="padding: 5px">
                        <select id="selParent" class="txt" onchange="sort(this)">
                        </select></td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">链接地址:&nbsp;&nbsp;</td>
                    <td style="padding: 5px">
                        <input id="txtLink" type="text" class="txt" style="width: 200px" /></td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">图标:&nbsp;&nbsp;</td>
                    <td style="padding: 5px">
                        <input id="txtIcon" type="text" class="txt" readonly="readonly" style="width: 80px" /><a href="javascript:void(0)" onclick="openIcon()">选取</a></td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">排序:&nbsp;&nbsp;</td>
                    <td style="padding: 5px">
                        <select id="selSort" class="txt">
                            <option selected value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                        </select></td>
                </tr>
                <tr style="background-color: White; height: 30px;">
                    <td>&nbsp;</td>
                    <td>
                        <input id="chkItem" type="checkbox" />显示菜单</td>
                </tr>
            </table>
            <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
                <a id="A1" class="easyui-linkbutton" onclick="serverEdit()" icon="icon-ok" href="javascript:void(0)">确定</a> <a id="A2" class="easyui-linkbutton" onclick="closePersonal()" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
        </div>
        <div id="dd3" icon="icon-save" style="padding: 5px; width: 460px; height: 355px;">
            <table id="test">
            </table>
            <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
                <a id="A3" class="easyui-linkbutton" onclick="saved()" icon="icon-ok" href="javascript:void(0)">确定</a> <a id="A4" class="easyui-linkbutton" onclick="closed()" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
        </div>
        <div id="dd2" icon="icon-save" style="padding: 5px; width: 304px; height: 184px;">
            <span class="icon icon-edit" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-sys" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-set" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-add" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-nav" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-users" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-role" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-log" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-delete" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-delete2" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-magic" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-database" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-Distribution" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-Pass" onclick="ss(this)">&nbsp;</span>
            <span class="icon icon-Browse" onclick="ss(this)">&nbsp;</span>
        </div>
    </form>
</body>
</html>
