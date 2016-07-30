<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPass.aspx.cs" Inherits="Accounts.Web.moblie.ForgetPass" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>找回密码 - 账单管理系统</title>
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

        function callsuccess() {
            location.href = "Login.aspx";
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

        function checklogin() {
            var loginname = $('#username').val();
            if (loginname == '') {
                $.messager.alert('系统提示', '请输入账号', 'error');
                return false;
            }
            else {
                $.post('../../ashx/UsersHandler.ashx?type=checklogin&loginname=' + encodeURI(loginname), function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '账号不存在，请重新输入账号', 'error');
                        $('#username').val('');
                    } else {
                        $('#email_panel').show();
                        $('#finduser').hide();
                        $('#sendcode').show();
                    }
                });
            }
            //成功查询后
            //$('#mm').dialog('open');
        }

        function sendmailcode() {
            var email = $('#email').val();
            var loginname = $('#username').val();
            if (getCookie('random')=="") {
                $.post('../../ashx/UsersHandler.ashx?type=sendcode&loginname=' + encodeURI(loginname) + '&email=' + email, function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '预留信息不符,账号验证错误', 'error');
                        $('#email').val('');
                    } else {
                        setCookie('random', msg, 1);
                        $.messager.alert('系统提示', '验证码已发送，请注意查收，1分钟内有效', 'info');
                        $('#sendcode').hide();
                        $('#resendcode').show();
                        $('#code_panel').show();
                        $('#confirm').show();
                        $('#backtoindex').hide();
                        $("#username").attr("readonly", "readonly");
                        $("#email").attr("readonly", "readonly");
                        clock(60);
                    }
                });
            }
            else {
                alert('验证码每分钟仅能获取一次')
            }
            
        }

        function clock(i) {
            i = i - 1;
            //document.getElementById("resend").innerText = i + "s后重新发送";
            $('#resend').val(i + "s后重新发送");
            //document.getElementById("resend_panel").innerText = i +"s后重新发送";
            $('#resend').disabled;
            if (i > 0) setTimeout("clock("+i+");", 1000);
            else {
                //document.getElementById("resend_panel").innerText = "重新发送";
                $('#resend').val("再次发送");
                $('#resend').disabled;
            }
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

        function code_confirm()
        {
            var code1 = getCookie('random');
            var code2 = $('#code').val();
            if (code1 == code2)
            {
                $('#pass1_panel').show();
                $('#pass2_panel').show();
                $('#updatepass').show();
                $('#sendcode').hide();
                $('#resendcode').hide();
                $('#confirm').hide();
                $('#backtoindex').show();
                $('#code_panel').hide();
            }
            else {
                $.messager.alert('系统提示', '验证码不正确或已失效，请重新获取', 'error');
            }
        }

        function updatepass() {
            var loginname = $('#username').val();
            var email = $('#email').val();
            var pass1 = $('#password1').val();
            var pass2 = $('#password2').val();
            if (loginname == '') {
                $.messager.alert('系统提示', '请输入账号', 'error');
                return false;
            }
            else if (pass1 != pass2) {
                $.messager.alert('系统提示', '两次输入的密码不一致', 'error');
                return false;
            }
            else if (email == '')
            {
                $.messager.alert('系统提示', '邮箱不能为空', 'error');
                return false;
            }
            else {
                $.post('../../ashx/UsersHandler.ashx?type=forgetpass&loginname=' + encodeURI(loginname) + '&email=' + email + '&pass=' + pass1, function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '预留信息不符,账号验证错误', 'error');
                        $('#txtLogin').val('');
                    } else {
                        JsPrint("密码成功，请重新登录！", "success");
                    }
                });
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
            <a href="#title-link">找回密码</a>
            <%--<img src="skin/default/icon.png" width="120px" />--%>
        </h1>
    </header>
    <div class="am-panel">
        <div class="am-panel-bd am-g">
            <!-- 登陆框 -->
            <div class="am-u-sm-11 am-u-sm-centered am-margin-top-xl">
                <form runat="server" action="" class="am-form" id="formtooltip">
                    <fieldset class="login-form am-form-set">
                        <div class="am-form-group am-form-icon">
                            <i class="am-icon-user"></i>
                            <input id="username" type="text" class="login-input-text am-form-field" placeholder="请输入用户名" runat="server" minlength="5" required data-foolish-msg="该用户名已存在">
                        </div>
                        <div class="am-form-group am-form-icon" id="email_panel" style="display: none">
                            <i class="am-icon-envelope"></i>
                            <input id="email" type="text" class="login-input-text am-form-field" placeholder="请输入注册邮箱" runat="server">
                        </div>
                        <div class="am-form-group am-form-icon" id="code_panel" style="display: none">
                            <i class="am-icon-envelope"></i>
                            <input id="code" type="text" class="login-input-text am-form-field" placeholder="请输入验证码" runat="server">
                        </div>
                        <div class="am-form-group am-form-icon" id="pass1_panel" style="display: none">
                            <i class="am-icon-lock"></i>
                            <input id="password1" type="password" class="login-input-text am-form-field" placeholder="请输入新密码" runat="server" minlength="6">
                        </div>
                        <div class="am-form-group am-form-icon" id="pass2_panel" style="display: none">
                            <i class="am-icon-lock"></i>
                            <input id="password2" type="password" class="login-input-text am-form-field" placeholder="再次输入密码" runat="server" minlength="6">
                        </div>
                    </fieldset>
                    <div class="am-u-sm-6 am-padding-left-0" id="finduser"><a href="javascript:void(0);" onclick="checklogin()" class="am-radius-xl am-btn am-btn-success am-btn-block">查找用户</a></div>
                    <div class="am-u-sm-6 am-padding-left-0" id="confirm" style="display: none"><a href="javascript:void(0);" onclick="code_confirm()" class="am-radius-xl am-btn am-btn-success am-btn-block">确认</a></div>
                    <div class="am-u-sm-6 am-padding-left-0" id="sendcode" style="display: none"><a href="javascript:void(0);" onclick="sendmailcode()" class="am-radius-xl am-btn am-btn-success am-btn-block">发送验证码</a></div>
                    <div class="am-u-sm-6 am-padding-left-0" id="updatepass" style="display: none"><a href="javascript:void(0);" onclick="updatepass()" class="am-radius-xl am-btn am-btn-success am-btn-block">确认修改</a></div>
                    <div class="am-u-sm-6 am-padding-right-0" id="backtoindex"><a href="Default.aspx" class="am-radius-xl am-btn am-btn-secondary am-btn-block">返回首页</a></div>
                    <div class="am-u-sm-6 am-padding-right-0" id="resendcode" style="display: none"><input type="button" id="resend" onclick="sendmailcode()" class="am-radius-xl am-btn am-btn-success am-btn-block" value="再次发送"></div>
                </form>
            </div>


        </div>
    </div>

    <footer data-am-widget="footer" class="am-footer am-footer-default" data-am-footer="{ }">
        <div class="am-footer-switch">
            <span class="am-footer-ysp" data-rel="mobile" data-am-modal="{target: '#am-switch-mode'}">手机版
            </span>
            <span class="am-footer-divider">| </span>
            <a id="godesktop" class="am-footer-desktop" href="http://account.keminl.cn/Default.aspx">电脑版</a>
        </div>
        <div class="am-footer-miscs">

            <p>
                由 <a href="http://account.keminl.cn/" title="账单管理系统" target="_blank" class="">账单管理系统</a> 提供技术支持
            </p>
            <p>CopyRight©2016 AllMobilize Inc.</p>
            <p>Keminl.cn 账单管理系统</p>
        </div>
    </footer>
    <input type="hidden" id="hiduser" runat="server" />
</body>

</html>
