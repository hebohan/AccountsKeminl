<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationRole.aspx.cs"
    Inherits="Accounts.Web.SysSetting.NavigationRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-size: 12px;
            width: 98%;
            height: auto;
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
        .style1
        {
            color: #FF0000;
            font-size: medium;
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
    <script type="text/javascript">
        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        function msgShow(title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        }
        $(function () {
            var ei = $("#large");
            ei.hide();
            $(".CatalogImg").mousemove(function (e) {
                ei.css({ top: e.pageY, left: e.pageX }).html('<img style="border:0px solid gray;" src="' + this.src + '" />').show();
            }).mouseout(function () {
                ei.hide();
            })
            var RoleId = $('#<%=HdId.ClientID %>').val();
            $.post('../ashx/NavigationRole.ashx?RoleId=' + RoleId, function (msg) {
                var tb = '';
                var str = msg.split('|');
                var s2 = str[0].split(',');
                tb += '<tr style="background-color: #E0ECFF; height: 26px">';
                for (var j = 0; j < s2.length - 1; j++) {
                    tb += '<th>';
                    tb += s2[j];
                    tb += '</th>';
                }
                tb += '</tr>';
                var s3 = str[1].split(',');
                for (var i = 3; i < str.length - 1; i++) {
                    var s = str[i].split(',');
                    if (i % 2 == 0)
                        tb += '<tr style="background-color:White; height: 23px;">';
                    else
                        tb += '<tr style="background-color:#EFF3FB; height: 23px;">';
                    for (var j = 0; j < s.length - 1; j++) {
                        tb += '<td align="center">';
                        if (s[j] == '0')
                            tb += '<input id="" type="checkbox" disabled="disabled" value="' + s3[j] + '" />';
                        else if (s[j] == '1')
                            tb += '<input id="" type="checkbox" value="' + s3[j] + '" />';
                        else
                            tb += '<input id="" type="checkbox" checked="checked" value="' + s3[j] + '"  />';
                        tb += '</td>';
                    }
                    tb += '</tr>';
                }
                $('#ckTable').html(tb);
                var s4 = str[2].split(',');
                $("input[name='ckNavi']").each(function () {
                    for (var i = 0; i < s4.length - 1; i++) {
                        if ($(this).val() == s4[i])
                            $(this).attr("checked", 'true');
                    }
                });
            })
            $('[name=ckNavi]').click(function () {
                var hang = $(this).parent().parent().parent("tr").prevAll().length;
                // var lie = $(this).prevAll().length;
                hang = Number(hang) + 1; //字符串变为数字 
                //lie = Number(lie) + 1;
                //alert("第" + hang + "行" + "第" + lie + "列"); 
                var bol = $(this).is(':checked');
                $.each($('#ckTable tr:eq(' + hang + ')').find("input:checkbox"), function (i, n) {
                    if (!$(this).is(":disabled")) {
                        $(this).attr("checked", bol);
                    }
                })
            });
        });
        function save() {
            var str = '';
            $("input[name='ckNavi']:checked").each(function () {
                var hang = $(this).parent().parent().parent("tr").prevAll().length;
                hang = Number(hang) + 1; //字符串变为数字 
                str += $(this).val() + ',';
                $('#ckTable tr:eq(' + hang + ')').find("input:checkbox:checked").each(function () {
                    str += $(this).val() + ',';
                })
                str += '|';
            })
            var RoleId = $('#<%=HdId.ClientID %>').val();
            $.post('../ashx/SaveNavigationRole.ashx?RoleId=' + RoleId + '&NavId=' + str, function (msg) {
                history.go(-1);
            })
        }
    </script>
</head>
<body style="background: white">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HdId" runat="server" />
    <!--JQTreeTable-->
    <script type="text/javascript">
    $(function(){
        //声明数组
        var map = [<%=strMap %>];
        //声明参数选项
        var options = {openImg: "../images/TreeTable/tv-collapsable.gif", shutImg: "../images/TreeTable/tv-expandable.gif", leafImg: "../images/TreeTable/tv-item.gif", lastOpenImg: "../images/TreeTable/tv-collapsable-last.gif", lastShutImg: "../images/TreeTable/tv-expandable-last.gif", lastLeafImg: "../images/TreeTable/tv-item-last.gif", vertLineImg: "../images/TreeTable/vertline.gif", blankImg: "../images/TreeTable/blank.gif", collapse: true, column: 0, striped: false, highlight: true, onselect: function(target){}};
        if(map!=null&&map.length>0)
        {
          //根据参数显示树
          $("#treetable").jqTreeTable(map, options);
        }
    });
    </script>
    <div class="btabs">
        <div style="float: left; margin-top: 5px">
            <strong>角色名称：<span class="style1"><asp:Label ID="lblRole" runat="server"></asp:Label></span>
            </strong>
        </div>
        <div style="float: right; padding-right: 15px">
            <a href="javascript:void(0)" onclick="save()"><span class="icon icon-log">&nbsp;</span>保存</a>&nbsp;
            <a href="javascript:void(0)" onclick="history.go(-1)"><span class="icon icon-delete">
                &nbsp;</span>取消</a>
        </div>
    </div>
    <div style="height: 2px">
    </div>
    <div style="width: 100%; border: 0px">
        <div style="width: 30%; float: left">
            <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                <HeaderTemplate>
                    <table id="tablemain" cellpadding="0" cellspacing="1px" border="0" style="width: 100.5%;"
                        bgcolor="b5d6e6">
                        <tr style="background-color: #E0ECFF; height: 26px">
                            <th>
                                菜单名称
                            </th>
                        </tr>
                        <tbody id="treetable">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: #EFF3FB; height: 23px;">
                        <td>
                            <span class='<%#Eval("Icon")%>'>&nbsp;</span><%#Eval("MenuName")%><div style="float: right;
                                padding-right: 20px">
                                <input name="ckNavi" type="checkbox" value='<%#Eval("Id")%>' /></div>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color: White; height: 23px;">
                        <td>
                            <span class='<%#Eval("Icon")%>'>&nbsp;</span><%#Eval("MenuName")%><div style="float: right;
                                padding-right: 20px">
                                <input name="ckNavi" type="checkbox" value='<%#Eval("Id")%>' /></div>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </TBODY> </TABLE>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div style="width: 70%; float: left">
            <table id="ckTable" cellpadding="0" cellspacing="1px" border="0" style="width: 100.5%;"
                bgcolor="b5d6e6">
            </table>
        </div>
    </div>
    </form>
</body>
</html>
