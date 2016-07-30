<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="successpay.aspx.cs" Inherits="Intermediary.Web.Pay.successpay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支付成功</title>
    <link type="text/css" rel="stylesheet" href="Share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="Share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="Share/css/validate.css" />
    <script type="text/javascript" src="Share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="Share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="Share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <style>
         #MainCont{ width: 100%;}
          td {
              line-height: 25px;
          }
          .ListSearch{ width: 100%;}
         .detail_edit {
             width: 70%;
             height: 25px;
             line-height: 25px;
         }
         .ListSearchInput {
             width: 100%;
         }
     </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b" style="text-align:center">
                        <font font-size:40px>支付结果</font>
                    </td>
                </tr>
            </table>
            <div class="ListSearch hei12" >
                 <div  style="padding-top:30px;text-align:center;font-size:30px"><img src="images/BtnOK.png" style="width:22px;height:22px;">您的订单已支付完成!&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>  
                <div style="padding-top:10px;text-align:center;" id="tip" >本窗口将在10s后关闭&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            </div>
        </div>
    </form>
        <script>
            function clock() {
                i = i - 1;
                document.getElementById("tip").innerHTML = "本页面将在" + i + "s后跳转回原商户页面&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                if (i > 0) setTimeout("clock();", 1000);
                else {
                    var url = $('#hidurl').val();
                    if (url != '') {
                        window.location.href = url;
                    }
                    else {
                        window.opener = null;
                        window.open('', '_self');
                        window.close();
                    }
                    
                }
            }
            var i = 6
            clock();
    </script>
    <input type="hidden" id="hidurl" runat="server"/>
</body>
</html>
