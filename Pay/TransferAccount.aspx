<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferAccount.aspx.cs" Inherits="Accounts.Web.Pay.TransferAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>转账</title>
    <link type="text/css" rel="stylesheet" href="share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="share/css/validate.css" />
    <script type="text/javascript" src="share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
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

        function CheckPay() {
            var orderid = $('#hidorderid').val();
            if (orderid != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/pay_ajax.ashx?action=checkpay",
                        data: { "orderid": orderid },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                alert("支付成功，谢谢您的支持");
                            } else {
                                alert("支付尚未成功，请重新支付");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("支付结果检测中，请稍后刷新页面查看");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            window.parent.location.reload();
        }

        function PayConfirm() {
            $('#btnPaySuccess').show();
            $('#btnPayFail').show();
            $('#btnPayConfirm').hide();
            $('#btnCancle').hide();
            var url = $('#hidurl').val();
            window.open(url);
        }

        function close2(){
            var api = top.dialog.get(window); //获取父窗体对象
            alert("支付失败，用户放弃支付！");
            api.close().remove();
        }
    </script>
</head>
<body style="overflow:hidden" >
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle" style="width:40%">
                <tr>
                    <td class="ListTitleStr hei12b" style="text-align:center">
                        <img src="Share/images/ListTitleBit.png" class="midImg" />订单支付<a id="lbTile" runat="server"></a>
                    </td>
<%--                    <td  style="text-align:right;font-size:4px">
                        <p id="user">未登录&nbsp;&nbsp;&nbsp;</p>
                    </td>--%>
                </tr>
            </table>
            <table class="ListSearch hei12" style="padding-left:70px">
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">订单编号：
                    </td>           
                    <td><%=hidorderid.Value %></td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">收款人：
                    </td>           
                    <td style="color:gray"><%=hidinuser.Value %></td>       
                </tr>
                <tr>
                    <td class="ListSearchInput" style="padding-left:10px">支付金额：
                    </td>           
                    <td><font color="red"><%=hidprice.Value %></font>元</td>       
                </tr>
                <tr >
                    <td  colspan="2" style="text-align:center">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" id="btnPaySuccess" onclick="CheckPay()" class="btnOK" value="支付完成" style="display:none"/>
                        <input type="button" id="btnPayFail" onclick="CheckPay()" class="btnDel" value="支付失败" style="display:none"/>
                        <input type="button" id="btnPayConfirm" onclick="PayConfirm()" class="btnOK" value="确认支付"/>
                        <input type="button" id="btnCancle" onclick="close2()" class="btnDel" value="放弃支付"/>
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" id="hidorderid" runat="server" />
        <input type="hidden" id="hidresult" runat="server" />
        <input type="hidden" id="hidinfo" runat="server" />
        <input type="hidden" id="hidprice" runat="server" />
        <input type="hidden" id="hidurl" runat="server" />
        <input type="hidden" id="hidinuser" runat="server" />
    </form>
    
</body>
</html>
