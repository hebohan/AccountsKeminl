<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeminlPay.aspx.cs" Inherits="Accounts.Web.Pay.KeminlPay" %>

<!DOCTYPE html>
<html>
    <head>
    <meta charset="utf-8">
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <title>KeminlPay-请选择支付方式</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type">
    <link rel="stylesheet" type="text/css" href="paycss/main.css?v=201605271">
    <script type="text/javascript" src="paycss/jquery-1.6.4-min.js?v=201605271" ></script>
    <link rel="stylesheet" type="text/css" href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="../JS/jquery-easyui-1.3.4/themes/icon.css" />
    <script type="text/javascript" src="../JS/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function login() {
            var loginname = $('#loginName_txt').val();
            $('#txtAccount').val(loginname);
            $('#txtlMail').val('');
            $('#txtNewPass1').val('');
            $('#txtNewPass2').val('');
            $('#mm').dialog('open');
        }
        function close2() {
            $('#mm').dialog('close');
        }
        $(function () {
            $('.j_bankUsed').show();
            $('#mm').dialog({
                closed: true,
                modal: true,
                title: ''
            });

        });
    </script>
    <script>
                function checkCookie() {
                    var username = getCookie('username');
                    var password = getCookie('pass');
                    if (username != null && password != "") {
                        $.ajax({
                            type: "post",
                            url: "/tools/pay_ajax.ashx?action=selectmoney",
                            data: { "username": username, "loginpass": password },
                            dataType: "json",
                            success: function (data, textStatus) {
                                document.getElementById("userinfo").innerHTML = username + ' 余额：<font color=\'orange\'>' + data.cash + '</font>元&nbsp;&nbsp;&nbsp;<a href=\'#\' onclick=\'DelCookie();\'>退出</a>';
                                $('#user_totalmoney').html(data.cash);
                                $('#btnLogin').hide();
                                $('#LoginInfoPanel1').hide();
                                $('#LoginInfoPanel2').show();
                                $('#payLogin').hide();
                                $('#paySubmit').show();
                                $('#RePanple').hide();
                                $('#LoginPassShow').hide();
                                $('#LoginPassDiv').hide();
                                $('#payCardBoxDiv').show();
                                $('#PayPassDiv').show();
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("支付失败");
                            },
                            complete: function (XMLHttpRequest, textStatus) {
                                this; // 调用本次AJAX请求时传递的options参数
                            }
                        });

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
                    return ""
                }
                function clearCookie(name) {
                    setCookie(name, "", -1);
                }

                function DelCookie() {
                    clearCookie('username');
                    clearCookie('cash');
                    clearCookie('pass');
                    window.location.reload();
                }
                $(function () {
                    var orderid = $('#hidorderid').val();
                    var total = $('#hidprice').val();
                    var CheckKey = $('#hidCheckKey').val();
                    if (($.trim(orderid) == '' || isNaN(orderid)) && ($.trim(total) == '' || isNaN(total)) && ($.trim(CheckKey) == '' || isNaN(CheckKey)))
                    {
                        alert("支付参数错误！请勿错误操作！");
                        window.opener = null;
                        window.open('', '_self');
                        window.close();
                    }
                    //初始化表单验证
                    checkCookie();
                });
                function backToList() {
                    window.parent.close();
                }

                function callsuccess() {
                }

                function SetPay() {
                    var username = getCookie('username');
                    var paypass = $('#paypass').val();
                    var price = $('#hidprice').val();
                    var payid = $('#hidPayid').val();
                    var mode = $('#hidMode').val();
                    var bussinesscode = $('#hidBussinessCode').val();
                    $.ajax({
                        type: "post",
                        url: "/tools/pay_ajax.ashx?action=keminl_pay_validate",
                        data: { "orderid": $('#hidorderid').val(), "username": username, "paypass": paypass, "price": price, "bussinesscode": bussinesscode, "payid": payid, "mode": mode },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                window.location.href = "successpay.aspx?return_url=" + data.url;
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("支付失败");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }

                function SetLogin1() {
                    var username = $('#username_input').val();
                    var loginpass = $('#loginpass_input').val();
                    $.ajax({
                        type: "post",
                        url: "/tools/pay_ajax.ashx?action=Login",
                        data: { "username": username, "loginpass": loginpass },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                document.getElementById("userinfo").innerHTML = username + ' 余额：<font color=\'orange\'>' + data.cash + '</font>元&nbsp;&nbsp;&nbsp;<a href=\'#\' onclick=\'DelCookie();\'>退出</a>';
                                $('#user_totalmoney').html(data.cash);
                                setCookie('username', username, 5)
                                setCookie("cash", data.cash, 5)
                                setCookie("pass", data.pass, 5)

                                $('#btnLogin').hide();
                                $('#LoginInfoPanel1').hide();
                                $('#LoginInfoPanel2').show();
                                $('#payLogin').hide();
                                $('#paySubmit').show();
                                $('#RePanple').hide();
                                $('#LoginPassShow').hide();
                                $('#LoginPassDiv').hide();
                                $('#payCardBoxDiv').show();
                                $('#PayPassDiv').show();
                                
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("登录失败");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }

                function SetLogin2() {
                    var username = $('#txtAccount').val();
                    var loginpass = $('#txtNewPass1').val();
                    $.ajax({
                        type: "post",
                        url: "/tools/pay_ajax.ashx?action=Login",
                        data: { "username": username, "loginpass": loginpass },
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.status == "true") {
                                document.getElementById("userinfo").innerHTML = username + ' 余额：<font color=\'orange\'>' + data.cash + '</font>元&nbsp;&nbsp;&nbsp;<a href=\'#\' onclick=\'DelCookie();\'>退出</a>';
                                $('#user_totalmoney').html(data.cash);
                                setCookie('username', username, 5)
                                setCookie("cash", data.cash, 5)
                                setCookie("pass", data.pass, 5)

                                $('#btnLogin').hide();
                                $('#LoginInfoPanel1').hide();
                                $('#LoginInfoPanel2').show();
                                $('#payLogin').hide();
                                $('#paySubmit').show();
                                $('#RePanple').hide();
                                $('#LoginPassShow').hide();
                                $('#LoginPassDiv').hide();
                                $('#payCardBoxDiv').show();
                                $('#PayPassDiv').show();

                                $('#mm').dialog('close');
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("登录失败");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }
            
                function Ordernonexistent() {
                    alert("订单记录不存在！");
                    //window.location.href = "http://account.keminl.cn/login.aspx";
                }
    </script>
 	</head>
    <body >
    <div class="shortcut">
      <div class="w">
          <ul class="s-right">
              <li id="loginbar" class="s-item fore1"></li>
              <li class="s-item fore2" id="LoginInfoPanel1">
                  <a class="op-i-ext" href="#" onclick="login();" >登录</a>
              </li>
              <li class="s-item fore2" id="LoginInfoPanel2" style="display:none">
                  <p class="op-i-ext" id="userinfo"></p>
              </li>
              <li class="s-item fore2" id="RePanple">
                  <a class="op-i-ext" href="http://account.keminl.cn" >注册</a>
              </li>
              <li class="s-div">|</li>
              <li class="s-item fore3">
                  <a class="op-i-ext" target="_blank" href="http://account.keminl.cn" >账单管理系统</a>
              </li>
          </ul>
          <span class="clr"></span>
      </div>
    </div>    
    <div class="p-header">
            <div class="w">
                <div id="logo">
                    <img width="170" height="28" src="paycss/i/KeminlPay.png" alt=" 收银台">
                </div>
            </div>
    </div>
    <div class="main"> 
	    <div class="w"> 
        <!-- order 订单信息 -->
            <div class="order">
                <div class="o-left">
                    <h3 class="o-title">
                        订单提交成功，请您尽快付款！订单号：<%=hidorderid.Value %>
                    </h3>
                    <p class="o-tips">
                        请您在<span class="font-red">12小时</span>内付清款项，否则订单会被自动取消。		            											</p>
                </div>
            <div class="o-right">
                <div class="o-price">
                    <em>应付金额</em><strong><%=hidprice.Value %></strong><em>元</em>
                </div>
            </div>
        <div class="clr"></div>
    </div>
    <!-- order 订单信息 end -->
                <!-- payment 支付方式选择 -->
     <div class="payment">
                    <!--京东支付图标-->
                    <div class="jp-logo-wrap">
                        <span class="jp-logo"></span>
                    </div>
                    <!--京东支付图标 end-->
                    <!--收银台公告-->
                    <div class="jp-notice" style="background:none;">
                        <div class="jp-tips">以下支付方式由KeminlPay提供&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="black">支付流水号：<%=hidPayid.Value %></font></div>
                    </div>
                    <!--收银台公告 end-->



<!-- 未登录显示用户名输入界面 begin-->
<div class="paybox j_paybox paybox-selected" id="LoginPassShow">
    <div class="p-wrap">
        <i class="p-border"></i>
        <div class="p-key">

            <!-- 输入用户名 begin-->
            <span class="p-k-check">
                <i class="ui-checkbox-L j_paymentCheck"><em class="selected"></em></i>
                <strong>请输入用户名</strong>
            </span>

            <!-- 输入用户名 end -->
        </div>
        <div class="p-value">
            <div class="p-v-line" style="height:35px">

                <!-- 用户名输入框 显示区域 begin -->
                <div class="j_bankUsed" style="display:none" >
                    <div class="p-amount" style="padding-right:550px">
                        <input type="text" id="username_input" />
                    </div>
                    <div class="clr"></div>
                </div>
                <!-- 用户名输入框 显示区域 end -->
            </div>
        </div>
        <div class="p-amount">
            <em>支付</em><strong id="cardPayAmountStrong2"><%=hidprice.Value %></strong><em>元</em>
        </div>
    </div>
</div>
<div class="paybox j_paybox paybox-selected" id="LoginPassDiv">
    <div class="p-wrap">
        <i class="p-border"></i>
        <div class="p-key">

            <!-- 输入登录密码 begin-->
            <span class="p-k-check">
                <i class="ui-checkbox-L j_paymentCheck"><em class="selected"></em></i>
                <strong>请输入登录密码</strong>
            </span>

            <!-- 输入登录密码 end -->
        </div>
        <div class="p-value">
            <div class="p-v-line" style="height:35px">

                <!-- 登录密码输入框 显示区域 begin -->
                <div class="j_bankUsed" style="display:none" >
                    <div class="p-amount" style="padding-right:550px">
                        <input type="password" id="loginpass_input" />
                    </div>
                    <div class="clr"></div>
                </div>
                <!-- 登录密码输入框 显示区域 end -->
            </div>
        </div>
    </div>
</div>
<!-- 未登录显示登录密码区域 end-->


<!-- 已登录用户 -->
<div class="paybox j_paybox paybox-selected" id="payCardBoxDiv" style="display:none">
    <div class="p-wrap">
        <i class="p-border"></i>
        <div class="p-key">

            <!-- 支付方式选择 -->
            <span class="p-k-check" id="p-k-check-payCard">
                <i class="ui-checkbox-L j_paymentCheck" id="ui-checkbox-L-payCard"><em class="selected"></em></i>
                <strong>账户余额</strong>
            </span>
            <!-- 支付方式选择 end -->
        </div>


        <div class="p-value">
            <div class="p-v-line" style="height:35px">
                <!-- 账户余额-金额 显示区域 begin -->
                <div class="j_bankUsed" style="display:none" >
                    <div class="p-amount" style="padding-right:650px">
                        <em>总计</em><strong id="cardPayAmountStrong1"><font color="orange"><span id="user_totalmoney"></span></font></strong><em>元</em>
                    </div>
                    <div class="clr"></div>
                </div>
                <!-- 账户余额-金额 显示区域 end -->
            </div>
        </div>

        <div class="p-amount">
            <em>支付</em><strong id="cardPayAmountStrong"><%=hidprice.Value %></strong><em>元</em>
        </div>
    </div>
</div>
<!-- 输入支付密码区域 -->
<div class="paybox j_paybox paybox-selected" id="PayPassDiv" style="display:none">
    <div class="p-wrap">
        <i class="p-border"></i>
        <div class="p-key">

            <!-- 输入支付密码 begin-->
            <span class="p-k-check">
                <i class="ui-checkbox-L j_paymentCheck"><em class="selected"></em></i>
                <strong>请输入支付密码</strong>
            </span>

            <!-- 输入支付密码 end -->
        </div>
        <div class="p-value">
            <div class="p-v-line" style="height:35px">

                <!-- 支付密码输入框 显示区域 begin -->
                <div class="j_bankUsed" style="display:none" >
                    <div class="p-amount" style="padding-right:550px">
                        <input type="password" id="paypass" />
                    </div>
                    <div class="clr"></div>
                </div>
                <!-- 支付密码输入框 显示区域 end -->
            </div>
        </div>
    </div>
</div>
<!-- 已登录用户 -->






    <!-- pay-verify 支付验证提交 -->
    <div class="pay-verify" id="pay-verify-typeCredit">
        <div class="pv-button" id="pv-button-submitPay" >
            <input type="submit" value="登录支付" id="payLogin" class="ui-button ui-button-XL" onclick="SetLogin1()" >
            <input type="submit" value="立即支付" id="paySubmit" class="ui-button ui-button-XL" onclick="SetPay()"  style="display:none">
        </div>
    </div>
    <!-- pay-verify 支付验证提交 end -->
</div>
    <!-- payment 支付方式选择 end -->

    </div>
</div>



    <div class="p-footer">
      <div class="pf-wrap w">
          <div class="pf-line">
              <span class="pf-l-copyright">Copyright &copy; 2015-2016  账单管理系统 版权所有</span>
              <%--<img width="185" height="20" src="../misc/css/i/footer-auth.png">--%>
          </div>
      </div>
</div>
    <form action="../ashx/UsersHandler.ashx?type=save" id="form1" >
        <div id="mm"  style="padding: 5px; width: 300px; height: 200px;">
            <br />
            <div style="text-align:center;"><strong><font style="font-size:15px">用户登录</font></strong></div>
            <br />
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right" >账号:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtAccount" type="text" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px" id="newpassword1_td">
                    <td align="right" style="width: 80px;">登录密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                        <input id="txtNewPass1" type="password" style="border: 1px solid #8DB2E3; width: 150px; height: 20px" />*
                    </td>
                </tr>

            </table>
            <br />
            <div region="south" border="false" style="text-align:center;height: 30px; line-height: 30px;">
                <a id="B3" class="easyui-linkbutton" onclick="SetLogin2()" icon="icon-ok" href="javascript:void(0)">登录</a>
                <a id="B2" class="easyui-linkbutton" onclick="close2()" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidorderid" runat="server" />
        <input type="hidden" id="hidprice" runat="server" />
        <input type="hidden" id="hidCheckKey" runat="server" />
        <input type="hidden" id="hidBussinessCode" runat="server" />
        <input type="hidden" id="hidBussinessId" runat="server" />
        <input type="hidden" id="hidPayid" runat="server" />
        <input type="hidden" id="hidMode" runat="server" />
    </form>
        
	</body>
</html>
