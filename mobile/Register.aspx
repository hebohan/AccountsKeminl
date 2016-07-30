<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Accounts.Web.moblie.Register" %>

<!DOCTYPE html>
<html>
	<head>
		<meta charset="UTF-8">
		<title>用户注册 - 账单管理系统</title>
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
        <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/metro/easyui.css" />
        <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/icon.css" />
        <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
        <script>
            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);
                    
                } else {
                    $.dialog.alert(msg);
                }
            }

            function callsuccess()
            {
                location.href = "Login.aspx";
            }

            function setCookie(c_name, value, expiredays) {
                var exdate = new Date()
                exdate.setTime(exdate.getTime() + expiredays * 60 * 1000)
                document.cookie = c_name + "=" + escape(value) +
                ((expiredays == null) ? "" : "; expires=" + exdate.toGMTString())
            }

            function getCookie(c_name) {
                if (document.cookie.length > 0) {
                    c_start = document.cookie.indexOf(c_name + "=")
                    if (c_start != -1) {
                        c_start = c_start + c_name.length + 1
                        c_end = document.cookie.indexOf(";", c_start)
                        if (c_end == -1) c_end = document.cookie.length
                        return unescape(document.cookie.substring(c_start, c_end))
                    }
                }
                return "";
            }

            $(function () {
                $('#formtooltip').validator({
                    onValid: function (validity) {
                        $(validity.field).closest('.am-form-group').find('.am-alert').hide();
                    },

                    onInValid: function (validity) {
                        var $field = $(validity.field);
                        var $group = $field.closest('.am-form-group');
                        var $alert = $group.find('.am-alert');
                        // 使用自定义的提示信息 或 插件内置的提示信息
                        var msg = $field.data('validationMessage') || this.getValidationMessage(validity);

                        if (!$alert.length) {
                            $alert = $('<div class="am-alert am-alert-danger"></div>').hide().
                              appendTo($group);
                        }
                        $alert.html(msg).show();
                    }
                });
            });

            function Save() {
                var login = $('#username').val();
                var Pass = $('#password1').val();
                var Pass2 = $('#password2').val();
                var name = $('#realname').val();
                var sex = $("input[name='docInlineRadio']:checked").val();
                var email = $('#email').val();
                var mobile = $('#phone').val();
                var code1 = $('#code').val();
                var code2 = getCookie('random');
                if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(email)) {
                    $.messager.alert('系统提示', '请输入正确的邮箱', 'error');
                    return false;
                }
                else if (!checkMobile(mobile)) {
                    $.messager.alert('系统提示', '请输入正确的手机号', 'error');
                    return false;
                }
                else if (login == '') {
                    $.messager.alert('系统提示', '请输入账号', 'error');
                    return false;
                } else if (Pass == '') {
                    $.messager.alert('系统提示', '请输入密码', 'error');
                    return false;
                }
                else if (email == '') {
                    $.messager.alert('系统提示', '请输入邮箱', 'error');
                    return false;
                } else if (Pass != Pass2) {
                    $.messager.alert('系统提示', '两次秘密输入不一致，请重新输入', 'error');
                    $('#txtPass2').val('');
                    $('#txtPass').val('');
                    return false;
                } else if (name == '') {
                    $.messager.alert('系统提示', '请输入姓名', 'error');
                    return false;
                } else if (sex != '1' && sex !='2') {
                    $.messager.alert('系统提示', '请选择性别', 'error');
                    return false;
                }
                else if (code2 == '' || code1 == '') {
                    $.messager.alert('系统提示', '请输入验证码', 'error');
                    return false;
                }
                else if (code1 != code2) {
                    $.messager.alert('系统提示', '验证码不正确', 'error');
                    return false;
                }
                else {
                    $.post('../../ashx/UsersHandler.ashx?type=register&login=' + encodeURI(login) + "&pass=" + Pass + "&name=" + encodeURI(name) + "&sex=" + sex + "&email=" + email + "&tel=" + "&mobile=" + mobile, function (msg) {
                        if (msg == 'false') {
                            $.messager.alert('系统提示', '账号已存在，请重新输入账号', 'error');
                            setCookie('random', "", 1);
                            $('#txtLogin').val('');
                        } else {
                            //$.messager.alert('系统提示', '注册成功，请登录', 'info');
                            JsPrint("注册成功！", "success");
                            $('#dd').dialog('close');
                            //window.location.href = 'Login.aspx';
                        }
                    });

                }
            }

            function checkMobile(s) {
                var regu = /^[1][0-9][0-9]{9}$/;
                var re = new RegExp(regu);
                if (re.test(s)) {
                    return true;
                } else {
                    return false;
                }
            }

            function sendmailcode() {
                var email = $('#email').val();
                if (email == '') {
                    $.messager.alert('系统提示', '请输入邮箱', 'error');
                }
                else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(email)) {
                    $.messager.alert('系统提示', '请输入正确的邮箱', 'error');
                    return false;
                }
                else {
                    if (getCookie('random') == "") {
                        $.post('../ashx/UsersHandler.ashx?type=registercode&email=' + email, function (msg) {
                            if (msg == 'false') {
                                $.messager.alert('系统提示', '验证码发送失败，邮箱已存在', 'error');
                                $('#username').val('');
                            }
                            else if (msg == "error") {
                                $.messager.alert('系统提示', '验证码发送失败，请稍候再试', 'error');
                            }
                            else {
                                setCookie('random', msg, 1);
                                $.messager.alert('系统提示', '验证码已发送，请注意查收，1分钟内有效', 'info');
                                $('#code_panel').show();
                                clock(60);
                            }
                        });
                    }
                    else {
                        alert('验证码每分钟仅能获取一次')
                    }
                }
            }

            function clock(i) {
                i = i - 1;
                $('#sendcodebtn').val(i + "s后重发");
                if (i > 0) setTimeout("clock(" + i + ");", 1000);
                else {
                    $('#sendcodebtn').val("重发");
                }
            }
        </script>

        <style>
            #vld-tooltip {
                position: absolute;
                z-index: 1000;
                padding: 5px 10px;
                background: #F37B1D;
                min-width: 150px;
                color: #fff;
                transition: all 0.15s;
                box-shadow: 0 0 5px rgba(0,0,0,.15);
                display: none;
            }

                #vld-tooltip:before {
                    position: absolute;
                    top: -8px;
                    left: 50%;
                    width: 0;
                    height: 0;
                    margin-left: -8px;
                    content: "";
                    border-width: 0 8px 8px;
                    border-color: transparent transparent #F37B1D;
                    border-style: none inset solid;
                }
</style>
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
    			<a href="#title-link">用户注册</a>
                <%--<img src="skin/default/icon.png" width="120px" />--%>
  			</h1>
		</header>
		<div class="am-panel">
			<div class="am-panel-bd am-g">
				<!-- 登陆框 -->
				<div class="am-u-sm-11 am-u-sm-centered am-margin-top-xl" >
					<form runat="server" action="" class="am-form" id="formtooltip">
						<fieldset class="login-form am-form-set">
							<div class="am-form-group am-form-icon" >
								<i class="am-icon-user"></i>
								<input id="username" type="text" class="login-input-text am-form-field"  placeholder="请输入用户名" runat="server" minlength="5" required data-foolish-msg="该用户名已存在">
							</div>
							<div class="am-form-group am-form-icon">
								<i class="am-icon-lock"></i>
								<input id="password1" type="password" class="login-input-text am-form-field" placeholder="请输入密码"  runat="server" minlength="6">
							</div>
                            <div class="am-form-group am-form-icon">
								<i class="am-icon-lock"></i>
								<input id="password2" type="password" class="login-input-text am-form-field" placeholder="再次输入密码"  runat="server" minlength="6">
							</div>
                            <div class="am-form-group am-form-icon">
								<i class="am-icon-info"></i>
								<input id="realname" type="text" class="login-input-text am-form-field" placeholder="请输入姓名"  runat="server" minlength="2" >
							</div>
                            <div class="am-form-group am-form-icon">
								<i class="am-icon-mobile"></i>
								<input id="phone" type="text" class="login-input-text am-form-field" placeholder="请输入手机号"  runat="server" >
							</div>
                            <div class="am-g">
                                <div class="am-u-sm-7">
                                    <div class="am-form-group am-form-icon" style="margin-top: -4px;">
                                        <i class="am-icon-envelope"></i>
                                        <input id="email" type="text" class="login-input-text am-form-field" placeholder="请输入邮箱">
                                    </div>
                                </div>
                                <div class="am-u-sm-5">
                                    <input type="button" class="am-radius-xs am-btn am-btn-secondary am-margin-top-xs" onclick="sendmailcode();" value="发送验证码" id="sendcodebtn"/>
                                </div>
                            </div>
                            <div class="am-form-group am-form-icon" style="display:none" id="code_panel">
								<i class="am-icon-mobile"></i>
								<input id="code" type="text" class="login-input-text am-form-field" placeholder="请输入验证码"  runat="server" >
							</div>
                            <div class="am-form-group" style="text-align:center">
                                <label class="am-radio-inline">
                                    <input type="radio"  value="1" name="docInlineRadio" > 我是男生
                                </label>
                                <label class="am-radio-inline">
                                    <input type="radio"  value="2" name="docInlineRadio" > 我是女生
                                </label>
                            </div>
						</fieldset>
                        <input type="button" id="RegisterBtn" runat="server" onclick="Save()" class="am-radius-xl am-btn am-btn-success am-btn-block am-margin-bottom-sm" value="立即注册" />
						<div class="am-u-sm-6 am-padding-left-0"><a href="Login.aspx" class="am-radius-xl am-btn am-btn-secondary am-btn-block">返回登录</a></div>
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
				<a id="godesktop"  class="am-footer-desktop" href="http://account.keminl.cn/Default.aspx">电脑版</a>
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
