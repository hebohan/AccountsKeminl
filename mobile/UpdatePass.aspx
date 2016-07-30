<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePass.aspx.cs" Inherits="Accounts.Web.moblie.UpdatePass" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>密码修改 - 账单管理系统</title>
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
            location.href = "Default.aspx";
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

        function updatepass() {
            var oldpassword = $('#old_pass').val();
            var newpass1 = $('#password1').val();
            var newpass2 = $('#password2').val();
            if (oldpassword == '' || newpass1 == '' || newpass2 == '') {
                $.messager.alert('系统提示', '请不要输入空信息', 'error');
                return false;
            }
            else if (newpass1 != newpass2) {
                $.messager.alert('系统提示', '两次输入的密码不一致', 'error');
                return false;
            }
            else {
                $.post('../ashx/UsersHandler.ashx?type=user_pass&oldpassword=' + encodeURI(oldpassword) + '&newpass=' + encodeURI(newpass1), function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '原密码错误，请重新输入！', 'error');
                        $('#old_pass').val('');
                    } else {
                        JsPrint("密码修改成功，即将跳转回首页！", "success");
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
				<a href='#' onclick="if(window.history.length > 1){window.history.back(-1);return false;}else{window.location.href = '../Default.aspx';}">
					<i class="am-header-icon am-icon-arrow-left" style="height:10px"></i>
				</a>
			</div>
        <h1 class="am-header-title">
            <%--<img src="skin/default/icon.png" style="width:20px;height:20px" />--%>
            <a href="#title-link">密码修改</a>
            <%--<img src="skin/default/icon.png" width="120px" />--%>
        </h1>
        <%=Menu %>
    </header>
    <div class="am-panel">
        <div class="am-panel-bd am-g">
            <!-- 登陆框 -->
            <div class="am-u-sm-11 am-u-sm-centered am-margin-top-xl">
                <form runat="server" action="" class="am-form" id="formtooltip">
                    <fieldset class="login-form am-form-set">
                        <div class="am-form-group am-form-icon">
                            <i class="am-icon-lock"></i>
                            <input id="old_pass" type="password" class="login-input-text am-form-field" placeholder="请输入原密码" runat="server" minlength="6">
                        </div>
                        <div class="am-form-group am-form-icon">
                            <i class="am-icon-lock"></i>
                            <input id="password1" type="password" class="login-input-text am-form-field" placeholder="请输入新密码" runat="server" minlength="6">
                        </div>
                        <div class="am-form-group am-form-icon">
                            <i class="am-icon-lock"></i>
                            <input id="password2" type="password" class="login-input-text am-form-field" placeholder="再次输入密码" runat="server" minlength="6">
                        </div>
                    </fieldset>
                    <div class="am-u-sm-6 am-padding-left-0"><a href="javascript:void(0);" onclick="updatepass()" class="am-radius-xl am-btn am-btn-success am-btn-block">确认修改</a></div>
                    <div class="am-u-sm-6 am-padding-right-0"><a href="Default.aspx" class="am-radius-xl am-btn am-btn-secondary am-btn-block">返回首页</a></div>
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
