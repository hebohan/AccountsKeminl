<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsCount.aspx.cs" Inherits="Accounts.Web.AccountsManage.AccountsCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账单统计</title>
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
        <div id="MainCont" style="overflow-y:hidden;height:300px;min-width:100%;">
            <h1 style="text-align:center;font-size:18px;margin:0;padding-top:20px">账单统计</h1>
            <table class="ListSearch hei12" style="table-layout:fixed;width:100%;margin:0px 0px 0px 0px !important" >
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
