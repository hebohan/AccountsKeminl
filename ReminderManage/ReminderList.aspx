<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReminderList.aspx.cs" Inherits="Accounts.Web.ReminderManage.ReminderList" %>

<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>提醒事项列表</title>
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
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>

    <script src="../Scripts/artdialog/jquery-1.10.2.js"></script>
    <link rel="stylesheet" href="../Scripts/artdialog/ui-dialog.css"/>
    <script src="../Scripts/artdialog/dialog-plus.js"></script>

    <script language="javascript" type="text/javascript">
        function OpenNewForm() {
            window.open("DictionaryDetail.aspx", '_blank', "width=1000,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }
        function backToList() {
            location.href = "../main.aspx";
            //history.go(-1);
        }

        function DelCheck() {
            if (hasChecked(document.all("ListData"))) {
                var hdelid = getCheckVal("chkbox");

                var d = top.dialog({
                    title: '提示',
                    content: '是否确定删除？',
                    okValue: '确定',
                    ok: function () {
                        $("#hdelid").val(hdelid);
                        var btn = document.getElementById('<%=hidBtnDel.ClientID%>');
                        btn.click();
                    },
                    cancelValue: '取消',
                    cancel: function () { }
                });
                d.show();
            }
            else {
                var d =top.dialog({
                    title: '警告',
                    content: '请至少选择一个删除项！',
                    cancel: false,
                    ok: function () { }
                });
                d.show();
            }
        }

        function refresh() {
            window.location.href = "AccountsList.aspx";
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

        function NewReminder(type, id) {
            var d = top.dialog({
                id: 'dialogAddRemark',
                width: '400',
                height: '300',
                fixed: true,
                onremove: function () {
                    window.location.reload();
                },
                url: '/ReminderManage/NewReminder.aspx?type=' + type + '&id=' + id
            }).showModal();
            return false;
        }

        function ShowDetail(id) {
            var d = dialog({
                id: 'dialogShowDetail',
                width: '300',
                height: '200',
                title: '提醒事项详情',
                fixed: true,
                url: 'ReminderDetail.aspx?id=' + id
            }).showModal();

        }
    </script>
    <style>
        .Textbox {
            font-weight:bold;
            color:orange;
            width:60px;
            border:0;
            text-align:center;
        }
        .button {
            width: 40px;
            line-height: 18px;
            text-align: center;
            font-weight: bold;
            color: #fff;
            text-shadow: 1px 1px 1px #333;
            border-radius: 5px;
            overflow: hidden;
        }
        .button.gray {
                color: #8c96a0;
                text-shadow: 1px 1px 1px #fff;
                border: 1px solid #dce1e6;
                box-shadow: 0 1px 2px #fff inset,0 -1px 0 #a8abae inset;
                background: -webkit-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: -moz-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: linear-gradient(top,#f2f3f7,#e4e8ec);
            }
       .button.larrow {
            overflow: visible;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b" style="width:100px">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />提醒事项列表<a id="A1" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->

                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput stitle" style="width:60px">关键字：</td>
                    <td class="ListSearchInput scontent">
                        <input type="text" id="keyword" runat="server" class="detail_edit stext" />
                    </td>
                    
                    <td class="ListSearchInput stitle" style="width:60px">起始日期：
                    </td>
                    <td class="ListSearchInput scontent" style="width:120px">
                        <input type="text" runat="server" id="sDate" readonly="readonly" class="Wdate stext" onclick="WdatePicker()" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:60px">截至日期：</td>
                    <td class="ListSearchInput scontent" style="width:120px">
                        <input type="text" runat="server" id="eDate" readonly="readonly" class="Wdate stext" onclick="WdatePicker()" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px">状态：</td>
                    <td class="ListSearchInput scontent">
                        <asp:DropDownList ID="Status" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnSearch" value="查询" class="btnSearch" id="btnSearch" runat="server"  onclick="btnclick()"/>
                        <asp:Button runat="server" ID="hidbtnSearch" style="display:none" OnClick="btnSearch_OnServerClick"/>
                        <input type="button" onclick="NewReminder('add','0')" value="新增" class="btnAdd" />
                                                <input type="button" name="btnDel" value="删除" class="btnDel" id="btnDel" runat="server" visible="False" onclick="DelCheck()" />
                        <asp:Button ID="hidBtnDel" runat="server" OnClick="btnDel_Click" Text="删除" CssClass="btnDel" Visible="False" Style="display: none;" />
                        <input type="button" onclick="backToList()" value="返回" class="btnBack" />
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
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p onclick="checkClick(ListData);" style="cursor: hand">全选</p>
                            </HeaderTemplate>
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemTemplate>
                                <input type="checkbox" id="chkbox" name="chkbox" value="<%# DataBinder.Eval(Container, "DataItem.bh") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="5%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <asp:Label ID="xuhao" runat="server" Text="<%# WebPager1.PageSize * (WebPager1.CurrentPageIndex-1) + ListData.Rows.Count +1%>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提醒标题">
                            <HeaderStyle Width="20%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="20%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container, "DataItem.title")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提醒内容">
                            <HeaderStyle Width="27%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="27%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container, "DataItem.content")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提醒对象">
                            <HeaderStyle Width="15%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="15%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container, "DataItem.RemindMail")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提醒时间">
                            <HeaderStyle Width="15%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="15%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy-MM-dd HH:mm}",DataBinder.Eval(Container, "DataItem.RemindTime")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="8%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# new Accounts.BLL.BasePage().GetDicName(DataBinder.Eval(Container,"DataItem.status").ToString())%>
                                <%--<%# DataBinder.Eval(Container,"DataItem.status").ToString()%>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="10%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.Status").ToString() == "wait_remind" ? "<a href='javascript:void(0);' onclick='NewReminder(\"edit\",\"" + DataBinder.Eval(Container,"DataItem.bh").ToString() + "\")'>修改</a>" :"" %>
                                <%# "<a href='javascript:void(0);' onclick='ShowDetail("+DataBinder.Eval(Container,"DataItem.bh").ToString()+")')'>查看</a>" %>
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
        <input type="hidden" runat="server" id="hdelid" />
    </form>
</body>
</html>
