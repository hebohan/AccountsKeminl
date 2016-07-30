<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictionaryItemList.aspx.cs" Inherits="Accounts.Web.DictionaryManage.DictionaryItemList" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>字典项列表</title>
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
        function OpenNewForm() {
            window.open("DictionaryItemDetail.aspx?parentid=" + document.getElementById("parentid").value, '_blank', "width=800,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }

        //打开详细页面
        function OpenNewEdit() {
            if (hasChecked(document.all("ListData")) && isCheckOne(document.all("ListData"))) {
                var items = getCheckVal("chkbox").split(',');
                var issys = 0;
                var hdelid = "";
                for (var i = 0; i < items.length; i++) {
                    var item = items[i].split('|');
                    if (item.length == 2) {
                        if (i < (items.length - 1)) {
                            hdelid += item[0] + ",";
                        } else {
                            hdelid += item[0];
                        }
                        if (item[1] == "是") {
                            issys = 1;
                        }
                    }
                }
                if (issys == 1) {
                    $.dialog.alert("不能修改系统内置的字典项！");
                    return;
                }
                window.open("DictionaryItemDetail.aspx?parentid=" + document.getElementById("parentid").value + "&dictionaryid=" + getCheckVal("chkbox").split('|')[0], '_blank', "width=800,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
            }
            else {
                $.dialog.alert("请选择一项进行修改！");
            }
        }

        function OpenPre() {
            location.href = "DictionaryItemList.aspx?id=" + $("#backid").val();
        }


        function DelCheck() {
            if (hasChecked(document.all("ListData"))) {
                var items = getCheckVal("chkbox").split(',');
                var issys = 0;
                var hdelid = "";
                for (var i = 0; i < items.length; i++) {
                    var item = items[i].split('|');
                    if (item.length == 2) {
                        if (i < (items.length - 1)) {
                            hdelid += item[0] + ",";
                        } else {
                            hdelid += item[0];
                        }
                        if (item[1] == "是") {
                            issys = 1;
                        }
                    }
                }
                if (issys == 1) {
                    $.dialog.alert("不能删除系统内置的字典项！");
                    return;
                }
                $.dialog.confirm("请确定是否删除该条记录?", function () {
                    document.all("hdelid").value = hdelid;
                    var btn = document.getElementById('<%=Button2.ClientID%>');
                     btn.click();
                 }, function () {

                 });
             }
             else {
                 $.dialog.alert("请至少选择一个需要删除的记录！");
             }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>

                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnAdd" value="增加" class="btnAdd" onclick="OpenNewForm()" />
                        
                        <input type="button" name="btnEdit" value="编辑" class="btnEdit" onclick="OpenNewEdit()" />
                        
                        <input type="button" name="btnDel" value="删除" class="btnDel" onclick="DelCheck()" />
                        <asp:Button ID="Button2" runat="server"  OnClick="btnDel_Click" Text="删除" CssClass="btnDel" style="display:none" />
                        
                        <input type="button" name="btnUp" value="上一级" class="btnExport" onclick="OpenPre()" />
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
                            <input type="checkbox" id="chkbox" name="chkbox" value="<%# DataBinder.Eval(Container, "DataItem.DicId") %>|<%# DataBinder.Eval(Container, "DataItem.Is_Sys") %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <HeaderStyle Width="5%" Wrap="False" />
                        <ItemStyle Wrap="False" Width="5%" />
                        <ItemTemplate>
                            <asp:Label ID="xuhao" runat="server" Text="<%# ListData.PageSize * (WebPager1.CurrentPageIndex-1) + ListData.Rows.Count +1%>"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="名称" >
                    <HeaderStyle  Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="15%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <%# DataBinder.Eval(Container,"DataItem.IsCatalog").ToString()=="True"?"<a href=\"DictionaryItemList.aspx?id="+ DataBinder.Eval(Container,"DataItem.Dicid")+"\">"+DataBinder.Eval(Container,"DataItem.DicName")+"</a>":DataBinder.Eval(Container,"DataItem.DicName")%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DicCode" HeaderText="代码">
                        <HeaderStyle Wrap="False" Width="15%" />
                        <ItemStyle Wrap="False" Width="15%" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Value" HeaderText="字典值">
                        <HeaderStyle Wrap="False" Width="15%" />
                        <ItemStyle Wrap="False" Width="15%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Is_Sys" HeaderText="系统内置">
                        <HeaderStyle Wrap="False" Width="15%" />
                        <ItemStyle Wrap="False" Width="15%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Describe" HeaderText="描述">
                        <HeaderStyle Wrap="False" Width="30%" />
                        <ItemStyle Wrap="False" Width="30%" HorizontalAlign="Left" />
                    </asp:BoundField>
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
                    <td width="30%" ><span id="RecordCount" class="meneame" runat="server"></span>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" runat="server" id="hdelid" />
        <input type="hidden" runat="server" id="parentid" />
        <input type="hidden" runat="server" id="backid" />
    </form>
</body>
</html>
