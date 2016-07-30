<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountsList.aspx.cs" Inherits="Accounts.Web.AccountsManage.MyAccountsList" %>

<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>账单预警</title>
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
    <script language="javascript" type="text/javascript">
        function OpenNewForm() {
            window.open("DictionaryDetail.aspx", '_blank', "width=1000,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }
        function backToList() {
            location.href = "../main.aspx";
            //history.go(-1);
        }
        //打开详细页面
        function OpenNewEdit() {
            if (hasChecked(document.all("ListData")) && isCheckOne(document.all("ListData"))) {
                window.open("DictionaryDetail.aspx?dictionaryid=" + getCheckVal("chkbox").split('|')[0], '_blank', "width=1000,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
            }
            else {
                $.dialog.alert("请选择一项进行修改！");
            }
        }
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

            } else if(type == "success_update")
            {
                $.dialog.tips(msg, 1, 'success.gif', function () { });
                setTimeout('refresh()', 500);
            }
            else if (type == "fail_update") {
                $.dialog.tips(msg, 1, 'error.gif', function () { });
            }
            else {
                $.dialog.alert(msg);
            }
        }
        function refresh() {
            window.location.href = "AccountsList.aspx";
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
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />账单预警<a id="A1" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" onclick="backToList()" value="返回" class="btnBack" />
                        <input type="button" name="btnDel" value="删除" class="btnDel" id="btnDel" runat="server" visible="False" onclick="DelCheck()" />
                        <asp:Button ID="hidBtnDel" runat="server" OnClick="btnDel_Click" Text="删除" CssClass="btnDel" Visible="False" Style="display: none;" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput stitle" style="width:60px">账单类型：</td>
                    <td class="ListSearchInput scontent">
                        <asp:DropDownList ID="ddlTemp" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="ListSearchInput stitle" style="width:60px">项目名称：</td>
                    <td class="ListSearchInput scontent">
                        <input type="text" id="work" runat="server" class="detail_edit stext" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px">状态：</td>
                    <td class="ListSearchInput scontent">
                        <asp:DropDownList ID="Status" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td style="width:100px">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnSearch" value="查询" class="btnSearch" id="btnSearch" runat="server" onserverclick="btnSearch_OnServerClick" />
                        
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
                                <input type="checkbox" id="chkbox" name="chkbox" value="<%# DataBinder.Eval(Container, "DataItem.Id") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="5%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%--<%# DataBinder.Eval(Container, "DataItem.zd_xh") %>--%>
                                <asp:Label ID="xuhao" runat="server" Text="<%# WebPager1.PageSize * (WebPager1.CurrentPageIndex-1) + ListData.Rows.Count +1%>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称">
                            <HeaderStyle Width="10%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%#"<a href='AccountDetail.aspx?id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"'>" + DataBinder.Eval(Container, "DataItem.zd_name") +"</a>"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资金额">
                            <HeaderStyle Width="6%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="6%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <font color="orange" style="font-weight:bold;"><%# DataBinder.Eval(Container, "DataItem.tz_money").ToString()+"元" %> </font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目详情">
                            <HeaderStyle Wrap="False" Width="30%" HorizontalAlign="Center" />
                            <ItemStyle Wrap="False" Width="30%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.zd_detail")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资天数">
                            <HeaderStyle Width="5%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <font color="lightblue" style="font-weight:bold;"><%# DataBinder.Eval(Container, "DataItem.tz_day").ToString()+"天" %> </font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资人">
                            <HeaderStyle Width="6%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="6%" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.tz_people").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="待收金额">
                            <HeaderStyle Width="6%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="6%" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <font color="orange" style="font-weight:bold;"><%# DataBinder.Eval(Container, "DataItem.ds_money").ToString()+"元" %> </font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="到期时间">
                            <HeaderStyle Width="10%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy-MM-dd}",DataBinder.Eval(Container, "DataItem.dq_time")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <HeaderStyle Width="10%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <%# int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString()) >= 0 ? DataBinder.Eval(Container, "DataItem.dq_day").ToString()+"天后"+(DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_receive" ? "待收款": (DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_repay" ? "待还款": (DataBinder.Eval(Container,"DataItem.status").ToString() == "wait_deposit" ? "待存款":"到期") )) : "已超期"+ System.Math.Abs(int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString())) +"天"%>
                                <%--<%# int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString()) >= 0 ? DataBinder.Eval(Container, "DataItem.dq_day").ToString()+"天后"+(DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_receive" ? "待收款": (DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_repay" ? "待还款": (DataBinder.Eval(Container,"DataItem.status").ToString() == "wait_deposit" ? "待存款":"到期") )) : "已超期"+ System.Math.Abs(int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString())) +"天"%>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="10%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# "<a href='AccountRegisterDetail.aspx?action=edit&id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"&type=3'>修改</a>"%>
<%--                                <%#"<a href='AccountsScheduleAudit.aspx?id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"'>填报</a>"%>--%>
                                <%#"<a href='AccountDetail.aspx?id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"'>查看</a>"%>
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
        <input type="hidden" runat="server" id="hcash" />
        <input type="hidden" runat="server" id="htotal" />
    </form>
</body>
</html>
