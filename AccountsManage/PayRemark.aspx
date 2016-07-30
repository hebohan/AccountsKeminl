<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayRemark.aspx.cs" Inherits="Accounts.Web.AccountsManage.PayRemark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易记录备注</title>
    <link type="text/css" rel="stylesheet" href="../Share/css/NK1.1.css?v=0.7"/>
    <link type="text/css" rel="stylesheet" href="../Share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../Share/css/validate.css" />
    <script type="text/javascript" src="../Share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../Share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../Share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../Share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script>
        function backToList() {
            window.parent.close();
        }
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess);

            } else {
                $.dialog.alert(msg);
            }
        }

        function callsuccess() {
        }

        function AddRemark() {
            var recordid = $('#hidrecordid').val();
            var remark = $('#remark').val();
            if (recordid != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=addremark",
                        data: { "recordid": recordid, "remark": remark },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                alert("添加备注成功");
                            } else {
                                alert("添加备注失败");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("添加备注成功");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            window.parent.location.reload();
        }
    </script>
</head>
<body style="overflow:hidden" >
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle" style="width:40%">
                <tr>
                    <td class="ListTitleStr hei12b" style="text-align:center">
                        <img src="../Share/images/ListTitleBit.png" class="midImg" />添加备注<a id="lbTile" runat="server"></a>
                    </td>
<%--                    <td  style="text-align:right;font-size:4px">
                        <p id="user">未登录&nbsp;&nbsp;&nbsp;</p>
                    </td>--%>
                </tr>
            </table>
            <table class="ListSearch hei12" style="padding-left:20px" >
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">交易明细：
                    </td>           
                    <td><font color="green"><%=hidname.Value %></font></td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">交易备注：
                    </td>           
                    <td><textarea style="width:250px;height:90px" id="remark" runat="server"></textarea></td>       
                </tr>
            </table>
            <div style="padding-left:150px">
                <input type="button" id="btnPayConfirm" onclick="AddRemark();" class="btnOK" value="添加备注"/>
            </div>
            
        </div>
        <input type="hidden" id="hidrecordid" runat="server" />
        <input type="hidden" id="hidremark" runat="server" />
        <input type="hidden" id="hidname" runat="server" />
    </form>
    
</body>
</html>
