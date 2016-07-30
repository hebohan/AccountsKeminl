<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextAdd.aspx.cs" Inherits="Accounts.Web.AccountsManage.TextAdd" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测试1-SUM</title>
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
        function OpenImport() {
            //$('#hidtempid').val(id);
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
                url: "../ashx/admin_ajax.ashx?action=sumtext&filename=" + filename,
                async: true,
                beforeSend: function (XMLHttpRequest) {
                },
                complete: function (data) {

                },
                success: function (data) {
                    if (data.status == "1") {
                        $("#impdialog").dialog('close');
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
                        <img src="../share/images/ListTitleBit.png" class="midImg" />测试1-SUM
                    </td>   
                    <td style="font-size:10px">
                        <span style="padding-left:50px">计算若干个自然数的和，</span>
                        <a href="javascript:void(0)" onclick="OpenImport()" >点击导入</a>
                    </td>
                </tr>
            </table>
            
                <%--<table class="ListSearch hei12">
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
                </table>--%>
            <div style="padding: 0 10px;">
            <!------------------- 在这里添加自定义的WebGridView ------------------->
            <!-------------------------------END----------------------------------->
            </div>
<%--            <table class="fenye" width="100%">
                <tr>
                    <td width="70%" align="right">
                        <cc2:WebPager ID="WebPager1" runat="server" NumericButtonCount="5" class="meneame" PageSize="10" OnPageChanged="WebPager1_PageChanged">
                        </cc2:WebPager>
                    </td>
                    <td width="30%" ><span id="RecordCount" class="meneame" runat="server"></span>
                    </td>
                </tr>
            </table>--%>
        </div>
         <div id="impdialog" class="easyui-dialog" title="导入文本" style="width: 400px; height: 150px;"
            data-options="resizable:true,modal:true,buttons:'#impbtn'">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right">TxT文档:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <asp:FileUpload ID="excelfile" CssClass="" runat="server"/><font color="#FF0000">*</font>
                    </td>
                </tr>
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
