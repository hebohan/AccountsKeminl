<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMailList.aspx.cs" Inherits="Accounts.Web.SysSetting.SysMailList" %>

<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统邮件管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../share/js/List.js"></script>
    <script src="../CSS/layer/layer.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
    <script type="text/javascript" language="javascript" src="../js/Finix.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/amazeui.min.js"></script>

    <script src="../Scripts/artdialog/jquery-1.10.2.js"></script>
    <link rel="stylesheet" href="../Scripts/artdialog/ui-dialog.css"/>
    <script src="../Scripts/artdialog/dialog-plus.js"></script>
    <link href="../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function OpenNewForm() {
            window.open("DictionaryDetail.aspx", '_blank', "width=1000,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }
        function backToList() {
            location.href = "../main.aspx";
            //history.go(-1);
        }
        function btnclick() {
            var sdate = new Date($('#sDate').val());
            var edate = new Date($('#eDate').val());

            if (sdate > edate) {
                layer.open({
                    content: '截至日期不能小于起始日期!',
                    btn: ['OK']
                });
                return;
            }
            else {
                var btn = document.getElementById('<%=hidbtnSearch.ClientID%>');
                btn.click();
            }
            
        }

        function ShowDetail(id) {
            var _width = '500';
            var _height = '300';
            var d = dialog({
                id: 'dialogShowDetail',
                width: _width,
                height: _height,
                title: '邮件详情',
                fixed: true,
                url: 'MailDetail.aspx?id=' + id
            }).showModal();
        }

        function NewMail() {
            var d = top.dialog({
                id: 'dialogAddRemark',
                width: '400',
                height: '250',
                onremove: function () {
                    window.location.reload();
                },
                fixed: true,
                url: '/SysSetting/NewMail.aspx'
            }).showModal();
            return false;
        }

        function ReSendMail(id) {
            $.ajax({
                type: "post",
                url: "/tools/do_ajax.ashx?action=resendmail",
                data: { "mailid": id},
                dataType: "json",
                success: function (data) {
                    if (data.status == "true") {
                        $.messager.alert('系统提示', '邮件发送成功', 'info');
                    }
                    else {
                        $.messager.alert('系统提示', '邮件发送失败，请稍后再试', 'error');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.messager.alert('系统提示', '邮件发送失败，请检查原因', 'error');
                },
                complete: function (XMLHttpRequest, textStatus) {
                    this; // 调用本次AJAX请求时传递的options参数
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b" style="width:100px">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />系统邮件管理<a id="A1" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->

                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput stitle" style="width:50px;padding-left:20px">关键字：</td>
                    <td class="ListSearchInput scontent"style="width:150px">
                        <input type="text" id="KeyWord" runat="server" class="detail_edit stext" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:60px;padding-left:20px">起始日期：
                    </td>
                    <td class="ListSearchInput scontent" style="width:150px">
                        <input type="text" runat="server" id="sDate" readonly="readonly" class="Wdate stext" onclick="WdatePicker()" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:60px;padding-left:20px">截至日期：</td>
                    <td class="ListSearchInput scontent" style="width:150px">
                        <input type="text" runat="server" id="eDate" readonly="readonly" class="Wdate stext" onclick="WdatePicker()" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px;padding-left:20px">排序：</td>
                    <td class="ListSearchInput scontent"style="width:150px">
                        <asp:DropDownList ID="Order" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:200px;text-align:right">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnSearch" value="查询" class="btnSearch" id="btnSearch" runat="server"  onclick="btnclick()"/>
                        <asp:Button runat="server" ID="hidbtnSearch" style="display:none" OnClick="btnSearch_OnServerClick"/>
                        <input type="button" onclick="NewMail()" value="新增" class="btnAdd" />
                        <!-------------------------END------------------------------>
                    </td>
                    

                </tr>
            </table>
            <div style="padding: 0 10px;">
                <!------------------- 在这里添加自定义的WebGridView ------------------->
                <cc1:WebGridView CssClass="ListTable hei12" ID="ListData" runat="server" AutoGenerateColumns="False"
                    RowHeaderColumn="Action" fixedheader="False" CountFormat="显示条数 从第<b>{0}</b>条到第 <b>{1}</b>条 总条数: <b>{2}</b>"
                    HorizontalAlign="Center" Width="100%">
                    <Columns>
                    <asp:TemplateField HeaderText="序号">
                    <HeaderStyle Width="5%" Wrap="False" />
                    <ItemStyle Wrap="False" Width="5%" />
                        <ItemTemplate>
                            <asp:Label ID="xuhao" runat="server" Text="<%# ListData.PageSize * (WebPager1.CurrentPageIndex-1) + ListData.Rows.Count +1%>"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮件标题" >
                    <HeaderStyle  Width="20%" Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="20%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%#DataBinder.Eval(Container,"DataItem.title").ToString().Length >=50? DataBinder.Eval(Container,"DataItem.title").ToString().Substring(0,50):DataBinder.Eval(Container,"DataItem.title").ToString()%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收件人" >
                    <HeaderStyle  Width="15%" Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="15%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%#DataBinder.Eval(Container,"DataItem.receiver") %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间" >
                    <HeaderStyle  Width="15%" Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="15%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%#  string.Format("{0:yyyy-MM-dd H:mm:ss}",DataBinder.Eval(Container,"DataItem.addtime"))%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划发送时间" >
                    <HeaderStyle  Width="15%" Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="15%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%#  string.Format("{0:yyyy-MM-dd H:mm:ss}",DataBinder.Eval(Container,"DataItem.planstime"))%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发送时间" >
                    <HeaderStyle  Width="15%" Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="15%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%#  string.Format("{0:yyyy-MM-dd H:mm:ss}",DataBinder.Eval(Container,"DataItem.sendtime"))%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否发送">
                    <HeaderStyle Width="5%" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.IsSend").ToString()=="True" ? "已发送" : "未发送" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" >
                    <HeaderStyle  Width="10%" Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="10%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%#  DataBinder.Eval(Container,"DataItem.IsSend").ToString()=="True" ? "<a href='javascript:void(0);' onclick='ReSendMail("+DataBinder.Eval(Container,"DataItem.bh").ToString()+")'>重发</a>" : "<a href='javascript:void(0);' onclick='ReSendMail("+DataBinder.Eval(Container,"DataItem.bh").ToString()+")'>发送</a>" %>&nbsp;&nbsp;<%#  "<a href='javascript:void(0);' onclick='ShowDetail("+DataBinder.Eval(Container,"DataItem.bh").ToString()+")'>查看</a>" %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <MouseOverRow MouseOverColor="" Support="False"></MouseOverRow>
                    <HeaderStyle CssClass="list_grid_title"></HeaderStyle>
                    <AlternatingRowStyle CssClass="list_grid_alternate_row" HorizontalAlign="Center"></AlternatingRowStyle>
                    <CustomColumns>
                        <NumberColumn Visible="False" Width="0" Align="Left" Position="0" Priority="0"></NumberColumn>
                        <MultiChooseColumn Visible="False" Width="0" Align="Left" Position="0" Priority="0"></MultiChooseColumn>
                    </CustomColumns>
                    <RowStyle CssClass="list_grid_row" HorizontalAlign="Center"></RowStyle>
                </cc1:WebGridView>
                <!-------------------------------END----------------------------------->
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
    </form>
</body>
</html>
