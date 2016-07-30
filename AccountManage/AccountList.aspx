<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="Accounts.Web.AccountManage.AccountList" %>

<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>帐号管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../share/js/List.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
    <script type="text/javascript" language="javascript" src="../js/Finix.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script language="javascript" type="text/javascript">

        //打开详细页面
        function showAddDialog(id) {
            window.open("AccountsScheduleDetail.aspx?id=" + id, '_blank', "width=600,height=400,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }

        function showAuditDialog(id) {
            window.open("AccountsScheduleAudit.aspx?id=" + id, '_blank', "width=600,height=400,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }

        function DelCheck() {
            if (hasChecked(document.all("ListData"))) {
                var hdelid = getCheckVal("chkbox");

                $.dialog.confirm("请确定是否删除该条记录?", function () {
                    $("#hdelid").val(hdelid);
                    var btn = document.getElementById('<%=hidBtnDel.ClientID%>');
                    btn.click();
                }, function () {

                });
            }
            else {
                $.dialog.alert("请至少选择一个需要删除的记录！");
            }
        }

        function JsPrint(msg, type, id) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', function () { });

            } else {
                $.dialog.alert(msg);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />帐号列表<a id="A1" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnDel" value="删除" class="btnDel" id="btnDel" runat="server"  onclick="DelCheck()" />
                        <asp:Button ID="hidBtnDel" runat="server" OnClick="btnDel_Click" Text="删除" CssClass="btnDel"  Style="display: none;" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>

            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput stitle">帐号类型：</td>
                    <td class="ListSearchInput scontent">
                        <asp:DropDownList ID="account_type" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="ListSearchInput stitle">关键字：</td>
                    <td class="ListSearchInput scontent">
                        <input type="text" id="account_name" runat="server" class="detail_edit stext" />
                    </td>
                    <td class="ListSearchInput stitle">状态：</td>
                    <td class="ListSearchInput scontent">
                        <asp:DropDownList ID="Status" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td style="width:200px">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnSearch" value="查询" class="btnSearch" id="btnSearch" runat="server" onserverclick="btnSearch_OnServerClick" />
                        
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <div>
	<table class="ListTable hei12" cellspacing="0" align="Center" rules="all" fixedheader="False" border="1" id="ListData" style="width:100%;border-collapse:collapse;">
		<thead>
			<tr class="list_grid_title">
				<%=fieldhead %>
			</tr>
		</thead>
        <tbody>
                <%=fieldcontent%>
		</tbody>
        <tfoot>

		</tfoot>
	</table>
</div>

            <table class="fenye" width="100%">
                <tr>
                    <td width="70%" align="right">
                        <cc2:WebPager ID="WebPager1" runat="server" NumericButtonCount="5" class="meneame" PageSize="10" OnPageChanged="WebPager1_PageChanged">
                        </cc2:WebPager>
                    </td>
                    <td width="30%"><span id="RecordCount" class="meneame" runat="server"></span>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" runat="server" id="hdelid" />
        <input type="hidden" runat="server" id="hidtype" />
    </form>
</body>
</html>
