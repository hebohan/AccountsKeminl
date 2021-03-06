﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailDetail.aspx.cs" Inherits="Accounts.Web.SysSetting.MailDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>邮件详情</title>
    <link type="text/css" rel="stylesheet" href="../Scripts/DealDetail/NK1.1.css?v=0.7"/>
    <link type="text/css" rel="stylesheet" href="../Scripts/DealDetail/List.css?v=0.7" />

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

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainCont" style="overflow-y:scroll;height:300px;min-width:530px;padding-left:30px">
            <table class="ListSearch hei12" style="table-layout:fixed;" >
                    <%=hidcontent.Value %>
            </table>
        </div>
        <input type="hidden" id="hidpayid" runat="server" />
        <input type="hidden" id="hidcontent" runat="server" />
        <input type="hidden" id="hidinfo" runat="server" />
        <input type="hidden" id="hidprice" runat="server" />
    </form>
    
</body>
</html>
