﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountRegisterList.aspx.cs" Inherits="Accounts.Web.AccountManage.AccountRegisterList" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>账单登记</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../share/js/List.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
    <script type="text/javascript" language="javascript" src="../js/Finix.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <link href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/jquery-easyui-1.3.4/themes/icon.css" rel="stylesheet" type="text/css" />

    <script src="../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script>
        $(function() {
            $("#impdialog").dialog('close');
            $('#btnImpCancel').bind('click', function () {
                $("#impdialog").dialog('close');
            });
        });
        function OpenImport(id) {
            $('#hidtempid').val(id);
            $("#impdialog").dialog('open');
            $('#impdialog').parent().appendTo($("form:first"));
        }
        function ImportExcel(filename,tempid) {
            $.messager.progress({
                title: '请稍后',
                msg: '正在导入，请稍后...'
            });
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../ashx/admin_ajax.ashx?action=import&filename=" + filename + "&tempid=" + tempid ,
                async: true,
                beforeSend: function (XMLHttpRequest) {
                },
                complete: function (data) {

                },
                success: function (data) {
                    if (data.status == "1") {
                        $("#impdialog").dialog('close');
                        //$.messager.show({
                        //    title: '提示',
                        //    msg: data.msg,
                        //    showType: 'show'
                        //});
                        //setTimeout(function () {
                        //    $.messager.progress('close');
                        //}, 1000);
                        $.messager.progress('close');
                        $.dialog.alert(data.msg);
                    } else {
                        $.messager.show({
                            title: '提示',
                            msg: data.msg,
                            showType: 'show'
                        });
                        setTimeout(function () {
                            $.messager.progress('close');
                        }, 1000);
                        
                    }


                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.messager.show({
                        title: '提示',
                        msg: "导入数据出错，请稍后重试！",
                        showType: 'show'
                    });
                    setTimeout(function () {
                        $.messager.progress('close');
                    }, 1000);
                }
            });
        }
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess);

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
                        <img src="../share/images/ListTitleBit.png" class="midImg" />账单登记模板选择
                    </td>   
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        
                        <!-------------------------END------------------------------>
                    </td>  
                </tr>
            </table>
            
                <table class="ListSearch hei12">
                    <tr>
                        <td class="ListSearchInput stitle">账单模板名称：</td>
                        <td class="ListSearchInput scontent">
                            <input type="text" id="name" runat="server" class="detail_edit stext" />
                        </td>
                        <td>
                            <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" id="btnSearch" value="查询" class="btnSearch" runat="server" OnServerClick="btnSearch_OnServerClick"/>
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
                    <asp:TemplateField HeaderText="账单模板名称" >
                    <HeaderStyle  Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="30%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                        <a href="../AccountsManage/AccountRegisterDetail.aspx?tempid=<%# DataBinder.Eval(Container,"DataItem.id")%>"><%# DataBinder.Eval(Container,"DataItem.name")%></a>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="is_sys" HeaderText="系统内置">
                        <HeaderStyle Wrap="False" Width="20%" />
                        <ItemStyle Wrap="False" Width="20%" HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField DataField="remark" HeaderText="说明">
                        <HeaderStyle Wrap="False" Width="35%" />
                        <ItemStyle Wrap="False" Width="35%" HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                     <asp:TemplateField HeaderText="操作" >
                    <HeaderStyle  Wrap="False"  HorizontalAlign="Center" />
                    <ItemStyle Wrap="False" Width="10%"  HorizontalAlign="Center" />
                    <ItemTemplate >
                         <asp:LinkButton runat="server" ID="Export" Text="导出模板" OnClick="Export_OnClick" ToolTip='<%# DataBinder.Eval(Container,"DataItem.id")%>'></asp:LinkButton>&nbsp;
                        <a href="javascript:void(0)" onclick="OpenImport('<%# DataBinder.Eval(Container,"DataItem.id")%>')">导入</a>
                        <a href="../AccountsManage/AccountRegisterDetail.aspx?type=0&tempid=<%# DataBinder.Eval(Container,"DataItem.id")%>">录入</a>
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
                    <td width="30%" ><span id="RecordCount" class="meneame" runat="server"></span>
                    </td>
                </tr>
            </table>
        </div>
         <div id="impdialog" class="easyui-dialog" title="Excel批量导入账单" style="width: 400px; height: 150px;"
            data-options="resizable:true,modal:true,buttons:'#impbtn'">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right">Excel文档:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <asp:FileUpload ID="excelfile" CssClass="" runat="server"/><font color="#FF0000">*</font>
                    </td>
                </tr>
               <%-- <tr style="background-color: White; height: 32px;">
                    <td align="right">账单单位:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <asp:DropDownList ID="ddlADept" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
            </table>
        </div>
        <div id="impbtn">
             <a id="btnImport" href="#"  class="easyui-linkbutton" runat="server" OnServerClick="btnImport_OnServerClick">导入</a>
            <a id="btnImpCancel" href="#" class="easyui-linkbutton">取消</a>
        </div>
        <input type="hidden" runat="server" id="hidtempid" />
        <input type="hidden" runat="server" id="hdelid" />
    </form>
</body>
</html>
