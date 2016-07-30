<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Accounts.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账单管理系统</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta http-equiv="Content-Language" content="zh-CN" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="JS/jquery-easyui-1.3.4/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="JS/jquery-easyui-1.3.4/themes/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="/share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="/share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script src="js/cloud.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="favicon.ico"/>
    <link href="../js/Treetable_files/jqtreetable.css" rel="stylesheet" type="text/css" />
    <link href="../Css/default.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/metro/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-easyui-1.3.4/themes/icon.css" />
    <script type="text/javascript" src="../js/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
    <script src="../js/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script>
        $(function () {
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            $(window).resize(function() {
                $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            });
            $(document).keydown(function (event) {
                if (event.keyCode == 13) {
                    $("#login_Btn").click();
                }
            });
        });
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', callsuccess);

            } else {
                $.dialog.alert(msg);
            }
        }

        function callsuccess() {

        }
    </script>
    <script type="text/javascript">
        if ($('#hidtype').val() != 'pc') {
            var mobileAgent = new Array("iphone", "ipod", "ipad", "android", "mobile", "blackberry", "webos", "incognito", "webmate", "bada", "nokia", "lg", "ucweb", "skyfire");
            var browser = navigator.userAgent.toLowerCase();
            var isMobile = false;
            for (var i = 0; i < mobileAgent.length; i++) {
                if (browser.indexOf(mobileAgent[i]) != -1) {
                    isMobile = true;
                    location.href = 'mobile/Login.aspx';
                    break;
                }
            }
        }
</script>
     <style type="text/css">
        /*body {
            width: 98%;
            font-size: 12px;
        }*/

        .btabs {
            border: 1px solid #8DB2E3;
            font-size: 12px;
            height: 26px;
            list-style-type: none;
            margin: 0;
            padding: 4px 0 0 4px;
            width: 99.5%;
            background-color: #E0ECFF;
        }

        .button {
            width: 40px;
            line-height: 18px;
            text-align: center;
            font-weight: bold;
            color: #fff;
            text-shadow: 1px 1px 1px #333;
            border-radius: 5px;
            overflow: hidden;
        }
        .button.gray {
                color: #8c96a0;
                text-shadow: 1px 1px 1px #fff;
                border: 1px solid #dce1e6;
                box-shadow: 0 1px 2px #fff inset,0 -1px 0 #a8abae inset;
                background: -webkit-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: -moz-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: linear-gradient(top,#f2f3f7,#e4e8ec);
            }
       .button.larrow {
            overflow: visible;
        }
    </style>
    <script type="text/javascript">
        window.onload = windowHeight; //页面载入完毕执行函数
        function windowHeight() {
            var h = document.documentElement.clientHeight;
            var bodyHeight = document.getElementById("content");
            if (h < 598) {
                h = 598;
                bodyHeight.style.height = (h - 130) + "px";
            }
            else bodyHeight.style.height = (h - 130) + "px";
        }
        setInterval(windowHeight, 500)//每半秒执行一次windowHeight函数

        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        function msgShow(title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        }
        $(function () {
            $('#dd').dialog({
                closed: true,
                modal: true,
                title: '用户注册'
            });
            $('#mm').dialog({
                closed: true,
                modal: true,
                title: '密码修改'
            });

        });
        function close1() {
            $('#dd').dialog('close');
            $('#code_panel').hide();
        }
        function close2() {
            $('#mm').dialog('close');
            $('#B1').hide();
            $('#B3').show();
            $('#txtAccount').removeAttr("readonly");
            $('#txtlMail_td').hide();
            $('#newpassword1_td').hide();
            $('#newpassword2_td').hide();
            $('#code_panel2').hide();
        }
        function add() {
            $('#txtLogin').val('');
            $('#txtPass').val('');
            $('#txtPass2').val('');
            $('#txtName').val('');
            $('#txtMail').val('');
            $('#txtTel').val('');
            $('#txtMobile').val('');
            $('#dd').dialog('open');
        }
        function forgetpass() {
            var loginname = $('#loginName_txt').val();
            $('#txtAccount').val(loginname);
            $('#txtlMail').val('');
            $('#txtNewPass1').val('');
            $('#txtNewPass2').val('');
            $('#mm').dialog('open');
        }
        function updatepass() {
            var code1 = getCookie('random1');
            var code2 = $('#code2').val();
            var loginname = $('#txtAccount').val();
            var email = $('#txtlMail').val();
            var pass1 = $('#txtNewPass1').val();
            var pass2 = $('#txtNewPass2').val();
            if (loginname == '') {
                $.messager.alert('系统提示', '请输入账号', 'error');
                return false;
            }
            else if (email == '') {
                $.messager.alert('系统提示', '请输入预留邮箱', 'error');
                return false;
            }
            else if (pass1 != pass2) {
                $.messager.alert('系统提示', '两次输入的密码不一致', 'error');
                return false;
            }
            else if (code2 == '') {
                $.messager.alert('系统提示', '请输入验证码', 'error');
                return false;
            }
            else if (code2 != code1) {
                $.messager.alert('系统提示', '验证码不正确', 'error');
                return false;
            }
            else {
                $.post('ashx/UsersHandler.ashx?type=forgetpass&loginname=' + encodeURI(loginname) +'&email=' + email +'&pass=' + pass1, function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '预留信息不符,账号验证错误', 'error');
                        $('#txtLogin').val('');
                    } else {
                        $.messager.alert('系统提示', '密码修改成功，请重新登录', 'info');
                        $('#mm').dialog('close');
                        $('#txtAccount').removeAttr("readonly");
                        $('#B1').hide();
                        $('#B3').show();
                        $('#txtlMail_td').hide();
                        $('#newpassword1_td').hide();
                        $('#newpassword2_td').hide();
                    }
                });
            }
        }
        function checklogin() {
            
            var loginname = $('#txtAccount').val();
            if (loginname == '') {
                $.messager.alert('系统提示', '请输入账号', 'error');
                return false;
            }
            else {
                $.post('ashx/UsersHandler.ashx?type=checklogin&loginname=' + encodeURI(loginname), function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '账号不存在，请重新输入账号', 'error');
                        $('#txtLogin').val('');
                    } else {
                        $('#B3').hide();
                        $('#B1').show();
                        $('#txtAccount').attr("readonly","readonly");
                        $('#txtlMail_td').show();
                        $('#newpassword1_td').show();
                        $('#newpassword2_td').show();
                    }
                });
            }
            //成功查询后
            
            
            //$('#mm').dialog('open');
        }
        function Save() {

            var code1 = getCookie('random');
            var code2 = $('#code').val();
            var login = $('#txtLogin').val();
            var Pass = $('#txtPass').val();
            var Pass2 = $('#txtPass2').val();
            var name = $('#txtName').val();
            var sex = $("input[name='rd']:checked").val();
            
            var email = $('#txtMail').val();
            var tel = $('#txtTel').val();
            var mobile = $('#txtMobile').val();
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
            }
            else if (code2=='') {
                $.messager.alert('系统提示', '请输入验证码', 'error');
                return false;
            }
            else if (code1 != code2) {
                $.messager.alert('系统提示', '验证码不正确', 'error');
                return false;
            }
            else {
                $.post('ashx/UsersHandler.ashx?type=register&login=' + encodeURI(login) + "&pass=" + Pass + "&name=" + encodeURI(name) + "&sex=" + sex + "&email=" + email + "&tel=" + tel + "&mobile=" + mobile, function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '账号已存在，请重新输入账号', 'error');
                        $('#txtLogin').val('');
                    } else {
                        $.messager.alert('系统提示', '注册成功，请登录', 'info');
                        $('#dd').dialog('close');
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
            var email = $('#txtMail').val();
            if (email == '')
            {
                $.messager.alert('系统提示', '请输入邮箱', 'error');
            }
            else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(email)) {
                $.messager.alert('系统提示', '请输入正确的邮箱', 'error');
                return false;
            }
            else {
                if (getCookie('random') == "") {
                    $.post('ashx/UsersHandler.ashx?type=registercode&email=' + email, function (msg) {
                        if (msg == 'false') {
                            $.messager.alert('系统提示', '验证码发送失败，邮箱已存在', 'error');
                            $('#txtMail').val('');
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

        function sendmailcode2() {
            var loginname = $('#txtAccount').val();
            var email = $('#txtlMail').val();
            if (getCookie('random1') == "") {
                $.post('ashx/UsersHandler.ashx?type=sendcode&loginname='+ loginname + '&email=' + email, function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '验证码发送失败，预留邮箱不正确', 'error');
                        $('#txtlMail').val('');
                    }
                    else {
                        setCookie('random1', msg, 1);
                        $.messager.alert('系统提示', '验证码已发送，请注意查收，1分钟内有效', 'info');
                        $('#code_panel2').show();
                        clock2(60);
                    }
                });
            }
            else {
                alert('验证码每分钟仅能获取一次')
            }

        }

        function clock(i) {
            i = i - 1;
            $('#sendcodebtn').val(i + "s");
            if (i > 0) setTimeout("clock(" + i + ");", 1000);
            else {
                $('#sendcodebtn').val("重发");
            }
        }

        function clock2(i) {
            i = i - 1;
            $('#sendcodebtn2').val(i + "s");
            if (i > 0) setTimeout("clock2(" + i + ");", 1000);
            else {
                $('#sendcodebtn2').val("重发");
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
    </script>
</head>
<body style="background-color: #1c77ac; background-image: url(images/light.png); background-repeat: no-repeat; background-position: center top; overflow: hidden;">
    <form id="form1" runat="server">

        <div id="mainBody">
            <div id="cloud1" class="cloud"></div>
            <div id="cloud2" class="cloud"></div>
        </div>


        <div class="logintop">
            <span>欢迎登录账单管理系统</span>
            <ul>
                <%--<li><a href="#">回首页</a></li>
    <li><a href="#">帮助</a></li>
    <li><a href="#">关于</a></li>--%>
            </ul>
        </div>

        <div class="loginbody">

            <span class="systemlogo"></span>
            <img id="lbg" style="width: 100%;height:322px;margin-top:30px; position: absolute; left: 0;" src="Images/loginbg.jpg"/>

            <div class="loginbox">

                <ul>
                    <li class="login_un">
                        <input id="loginName_txt" type="text" runat="server" class="loginuser" value="" placeholder="请输入用户名"  /></li>
                    <li>
                        <div class="login_pwd" style="white-space:nowrap">
                            <input id="pwd_txt" runat="server" type="password" class="loginpwd" value="" placeholder="请输入密码" style="width:60%;"/>
                            <%--<img src="VerifyCode.aspx" id="verifycode" runat="server" onclick="this.src='VerifyCode.aspx?d='+Math.random();"/>--%>
                        </div>
                    </li>
                    
                    <li>
                        <input id="login_Btn" type="button" class="loginbtn" value="登录" runat="server" onserverclick="login_Btn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="reset" class="loginbtn" value="重置" />
                        <label>><a href="#" onclick="forgetpass();">忘记密码？</a></label></li>
                    <li>
                        <a onclick="add();" href="#"  >还没有账户？点这里注册</a>&nbsp;&nbsp;&nbsp;
                        <a href="http://www.keminl.cn/"  >备用服务器</a>&nbsp;&nbsp;&nbsp;
                        <a href="mobile/Login.aspx"  >手机版 Beta 1.0</a>
                    </li>
                </ul>


            </div>
        </div>



        <div class="loginbm">Copyright © 2016 Keminl.cn 账单管理系统 版权所有</div>

    </form>
    <form action="../ashx/UsersHandler.ashx?type=save" id="form1" >
        <div id="dd" icon="icon-save" style="padding: 5px; width: 300px; height: 400px;">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right" style="width: 80px;">账号:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtLogin" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;" id="password1_td">
                    <td align="right" style="width: 80px;">密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtPass" type="password" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;" id="password2_td">
                    <td align="right" style="width: 80px;">密码确认:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtPass2" type="password" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td align="right" style="width: 80px;">姓名:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtName" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td align="right">性别:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input name="rd" type="radio" value="1" checked="checked"/>男&nbsp;
                        <input name="rd" type="radio" value="2" />女
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">Email:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtMail" type="text" style="border: 1px solid #8DB2E3; width: 100px; height: 20px" />*
                        <input type="button" value="验证" onclick="sendmailcode()" class="button gray larrow" id="sendcodebtn"/>
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;display:none" id="code_panel">
                    <td align="right">验证码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="code" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">电话:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtTel" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;">
                    <td align="right">手机:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtMobile" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
            </table>
            <div region="south" border="false" style="text-align:center;height: 30px; line-height: 30px;">
                <a id="A1" class="easyui-linkbutton" onclick="Save()" icon="icon-ok" href="javascript:void(0)">确定</a> 
                <a id="A2" class="easyui-linkbutton" onclick="close1()" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
        </div>
    </form>


    <form action="../ashx/UsersHandler.ashx?type=save" id="form1" >
        <div id="mm" icon="icon-save" style="padding: 5px; width: 300px; height: 250px;">
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right" style="width: 80px;">账号:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtAccount" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;display:none" id="txtlMail_td">
                    <td align="right" style="width: 80px;">预留邮箱:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtlMail" type="text" style="border: 1px solid #8DB2E3; width: 100px; height: 20px" />*
                        <input type="button" value="验证" onclick="sendmailcode2()" class="button gray larrow" id="sendcodebtn2"/>
                    </td>
                </tr>
                <tr style="background-color: White; height: 26px;display:none" id="code_panel2">
                    <td align="right">验证码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="code2" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;display:none" id="newpassword1_td">
                    <td align="right" style="width: 80px;">新密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtNewPass1" type="password" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;display:none" id="newpassword2_td">
                    <td align="right" style="width: 80px;">新密码确认:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtNewPass2" type="password" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
            </table>
            <div region="south" border="false" style="text-align:center;height: 30px; line-height: 30px;">
                <a id="B3" class="easyui-linkbutton" onclick="checklogin()" icon="icon-search" href="javascript:void(0)">查找</a>
                <a id="B1" class="easyui-linkbutton" onclick="updatepass()" icon="icon-ok" href="javascript:void(0)" style="display:none">修改</a> 
                <a id="B2" class="easyui-linkbutton" onclick="close2()" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidtype" runat="server" />
    </form>
</body>
</html>
