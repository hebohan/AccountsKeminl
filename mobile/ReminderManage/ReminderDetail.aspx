<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReminderDetail.aspx.cs" Inherits="Accounts.Web.mobile.ReminderManage.ReminderDetail" %>
<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<!DOCTYPE html>

<html>
	<head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"> 
		<title>提醒详情 - 账单管理系统 Keminl.cn</title>
		<meta name="description" content="">
        <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
        <link type="text/css" rel="stylesheet" href="../../Scripts/DealDetail/NK1.1.css?v=0.7"/>
        <link type="text/css" rel="stylesheet" href="../../Scripts/DealDetail/List.css?v=0.7" />
		<meta name="keywords" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<meta name="renderer" content="webkit">
		<meta http-equiv="Cache-Control" content="no-siteapp" />
		<link rel="icon" type="image/png" href="i/favicon.png">
		<link rel="stylesheet" href="../css/app.css">
        <link rel="stylesheet" href="../css/index.css" />
        <link rel="stylesheet" href="../css/baoliao.css" />
        <link rel="stylesheet" href="../css/owl.carousel.css" />
		<script src="../js/jquery.min.js"></script>
        <link rel="stylesheet" href="../css/amazeui.min.css">
		<script src="../js/amazeui.min.js"></script>
		<script src="../js/layer/layer.js"></script>
        <script language="javascript" type="text/javascript" src="../../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
        <script type="text/javascript">
            function relogin() {
                window.parent.location.href = '../../Login.aspx';
            }

            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);
                }
                else if (type == "delete_success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess2);
                }
                else {
                    $.dialog.alert(msg);
                }
            }

            function DL_Delete() {
                var id = $('#hiddlid').val();
                $.dialog.confirm("请确定是否删除该条提醒事项?", function () {
                    var userid = $('#hiduserid').val();
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=tx_delete",
                        data: { "id": id, "userid": userid },
                        dataType: "text",
                        success: function (data) {
                            if (data == "false") {
                                JsPrint("删除失败，请稍后再试", "error");
                            }
                            else if (data == "true") {
                                JsPrint("删除成功", "delete_success");
                            }
                            else {
                                JsPrint(data, "error");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            JsPrint("删除交易记录成功", "success");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }, function () {

                });

            }

            function callsuccess2() {
                window.location.href = "ReminderList.aspx";
            }
        </script>
	</head>
	<body class="am-background-disable" style="min-width:100%;background-color:white;">
		<header data-am-widget="header" class="am-header am-header-default am-header-fixed">
			<div class="am-header-left am-header-nav">
				<a href='#' onclick="if(window.history.length > 1){window.history.back(-1);return false;}else{window.location.href = '../Default.aspx';}">
					<i class="am-header-icon am-icon-arrow-left" style="height:10px"></i>
				</a>
			</div>
			<h1 class="am-header-title">
    			<a href="#title-link" style="font-size:16px;padding-top:0px;" >提醒详情</a>
  			</h1>
            <%=Menu %>
		</header>
        <form runat="server">
            <div style="padding-top:10px;background-color:white;overflow:hidden;width:100%" id="a_detail" >
                <h1 style="text-align:center;font-size:18px;padding-bottom:10px;padding-top:5px">提醒详情</h1>
                <table class="ListSearch hei12" style="margin:0px auto;" >
                    <%=hidcontent.Value %>
                 </table>
                <div style="text-align:center;padding-top:20px;font-size:15px;">
                    <%=hidbtncontent.Value %>
                    </div>
            </div>
            
        </form>
        
		<footer data-am-widget="footer" class="am-footer am-footer-default" data-am-footer="{ }" style="padding-top:15px">
			<div class="am-footer-switch">
				<span class="am-footer-ysp" data-rel="mobile" data-am-modal="{target: '#am-switch-mode'}">
          手机版
    </span>
				<span class="am-footer-divider"> | </span>
				<a id="godesktop"  class="am-footer-desktop" href="http://account.keminl.cn/">电脑版</a>
			</div>
			<div class="am-footer-miscs">
				<p>由 <a href="http://account.keminl.cn/" title="账单管理系统" target="_blank" class="">账单管理系统</a> 提供技术支持
				</p>
				<p>CopyRight©2016 AllMobilize Inc.</p>
				<p>Keminl.cn 账单管理系统</p>
			</div>
		</footer>
        <input type="hidden" id="hiduserid" runat="server" />
        <input type="hidden" id="hidpid" runat="server" />
        <input type="hidden" id="hidcontent" runat="server" />
        <input type="hidden" id="hiddlid" runat="server" />
        <input type="hidden" id="hidbtncontent" runat="server" />
	</body>
    
</html>
