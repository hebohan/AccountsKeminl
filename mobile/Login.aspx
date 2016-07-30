<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Accounts.Web.moblie.Login" %>

<!DOCTYPE html>
<html>
	<head>
		<meta charset="UTF-8">
		<title>账单管理系统</title>
		<meta name="description" content="">
		<meta name="keywords" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<meta name="renderer" content="webkit">
		<meta http-equiv="Cache-Control" content="no-siteapp" />
		<link rel="icon" type="image/png" href="i/favicon.png">
		<link rel="stylesheet" href="css/amazeui.min.css">
		<link rel="stylesheet" href="css/app.css">
		<script src="js/jquery.min.js"></script>
		<script src="js/amazeui.min.js"></script>
		<script src="js/layer/layer.js"></script>
        <script language="javascript" type="text/javascript" src="/share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
        <script>
            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);

                } else {
                    $.dialog.alert(msg);
                }
            }
            function CheckHeadPic() {
                var loginname = $('#loginName_txt').val();
                if (loginname != "") {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=getheadpic",
                        data: { "loginname": loginname },
                        dataType: "text",
                        success: function (data) {
                            $('#head_img').attr("src", "../head_image/" + data);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(XMLHttpRequest + "," + textStatus + "," + errorThrown);
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            }
        </script>
	</head>

	<body class="am-background-disable">
		<header data-am-widget="header" class="am-header am-header-default am-header-fixed">
			<div class="am-header-left am-header-nav">
				<a href="Default.aspx" class="">

					<i class="am-header-icon am-icon-home"></i>
				</a>
			</div>
			<h1 class="am-header-title">
                <%--<img src="skin/default/icon.png" style="width:20px;height:20px" />--%>
    			<a href="#title-link">账单管理系统</a>
                <%--<img src="skin/default/icon.png" width="120px" />--%>
  			</h1>
		</header>
		<div class="am-panel am-padding-top-xl">
			<div class="am-panel-bd am-g">
				<div class="login-ico am-margin-top-xl" style="text-align:center">
					<%--<img src="img/qq1.png" alt="">--%>
                    <%--<img src="img/head.jpg" style="width:150px;height:150px">--%>
                    <img src="../head_image/default.gif" style="width:150px;height:150px" id="head_img">
                </div>
				<!-- 登陆框 -->
				<div class="am-u-sm-11 am-u-sm-centered am-margin-top-xl" >
					<form class="am-form" runat="server">
						<fieldset class="login-form am-form-set">
							<div class="am-form-group am-form-icon">
								<i class="am-icon-user"></i>
								<input id="loginName_txt" type="text" class="login-input-text am-form-field"  placeholder="请输入您的账号" runat="server" onchange="CheckHeadPic();">
							</div>
							<div class="am-form-group am-form-icon">
								<i class="am-icon-lock"></i>
								<input id="pwd_txt" type="password" class="login-input-text am-form-field" placeholder="至少6个字符"  runat="server">
							</div>
						</fieldset>
                        <asp:Button ID="Loginbtn" runat="server" OnClick="login_Btn_Click" class="am-radius-xl am-btn am-btn-success am-btn-block am-margin-bottom-sm" Text="登录"/>
						<div class="am-u-sm-6 am-padding-left-0"><a href="Register.aspx" class="am-radius-xl am-btn am-btn-secondary am-btn-block">立即注册</a></div>
						<div class="am-u-sm-6 am-padding-right-0"><a href="ForgetPass.aspx" class="am-radius-xl am-btn am-btn-danger am-btn-block">忘记密码</a></div>
					</form>
				</div>


			</div>
		</div>

		<footer data-am-widget="footer" class="am-footer am-footer-default" data-am-footer="{ }">
			<div class="am-footer-switch">
				<span class="am-footer-ysp" data-rel="mobile" data-am-modal="{target: '#am-switch-mode'}">
          手机版
    </span>
				<span class="am-footer-divider"> | </span>
				<a id="godesktop"  class="am-footer-desktop" href="http://account.keminl.cn/Login.aspx?type=pc">电脑版</a>
			</div>
			<div class="am-footer-miscs">

				<p>由 <a href="http://account.keminl.cn/" title="账单管理系统" target="_blank" class="">账单管理系统</a> 提供技术支持
				</p>
				<p>CopyRight©2016 AllMobilize Inc.</p>
				<p>Keminl.cn 账单管理系统</p>
			</div>
		</footer>
        <input type="hidden" id="hiduser" runat="server" />
	</body>
    
</html>
