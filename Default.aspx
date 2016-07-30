<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Accounts.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>账单管理系统
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta http-equiv="Content-Language" content="zh-CN" />
    <link href="CSS/JgIndex.css" rel="stylesheet" type="text/css" />
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="share/css/NK1.1.css?v=0.7" />
    <link href="JS/jquery-easyui-1.3.4/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <link href="scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="favicon.ico"/>
    <script src="JS/jquery-easyui-1.3.4/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="JS/jquery-easyui-1.3.4/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="JS/jquery-easyui-1.3.4/easyui-tabs-loading.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8" src="scripts/artdialog/dialog-plus-min.js"></script>
    <link rel="stylesheet" type="text/css" href="JS/jquery-easyui-1.3.4/themes/metro/easyui.css"/>
    <%--<link rel="stylesheet" type="text/css" href="JS/jquery-easyui-1.3.4/themes/icon.css" />--%>
<%--    <script type="text/javascript" src="JS/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>--%>
    
    <script type="text/javascript">
        $(function () {
            if ($('#hftip').val() == '0') {
                closeDiv();
                document.getElementById("tip").style.display = "none";
            }
            $('#mm').menu({
                onClick: function (item) {
                    closeTab(item.id);
                }
            });
            $('.easyui-accordion li').click(function () {
                $('.easyui-accordion li').removeClass("font_dj");
                $(this).addClass("font_dj");
            }).hover(function () {
                $(this).addClass("font_jg");
            }, function () {
                $(this).removeClass("font_jg");
            });
            $('#aa').find('div:eq(0)').find('div:eq(0)').click();
        });
        var i = 1;

        function AddTab(url, subtitle) {
            if ($('#ttt').tabs('exists', subtitle)) {
                $('#ttt').tabs('close', subtitle);
            }
            if (!$('#ttt').tabs('exists', subtitle)) {
                $('#ttt').tabs('addIframeTab', {
                    tab: { title: subtitle, closable: true },
                    iframe: { src: url }
                });
                $(".tabs-inner").bind('contextmenu', function (e) {
                    $('#mm').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                    return false;
                });
            }
            else {
                $('#ttt').tabs('select', subtitle);
            }
        }

        function createFrame(src) {
            if (!src)
                return '<iframe width="100%" height="100%" frameborder="0"  src="' + src + 'DataGridList.aspx" style="width:100%;height:100%;"></iframe>';
        }

        function closeTab(action) {
            var alltabs = $('#ttt').tabs('tabs');
            var currentTab = $('#ttt').tabs('getSelected');
            var allTabtitle = [];
            $.each(alltabs, function (i, n) {
                allTabtitle.push($(n).panel('options').title);
            });
            var onlyOpenTitle = allTabtitle.length === 1;

            switch (action) {
                case "refresh":
                    var iframe = $(currentTab.panel('options').content);
                    var src = iframe.attr('src');
                    $('#ttt').tabs('updateIframeTab', {
                        which: currentTab.panel('options').title
                    });
                    break;
                case "close":
                    var currtab_title = currentTab.panel('options').title;
                    $('#ttt').tabs('close', currtab_title);
                    break;
                case "closeall":
                    $.each(allTabtitle, function (i, n) {
                        if (n != onlyOpenTitle) {
                            $('#ttt').tabs('close', n);
                        }
                    });
                    break;
                case "closeother":
                    var currtab_title = currentTab.panel('options').title;
                    $.each(allTabtitle, function (i, n) {
                        if (n != currtab_title && n != onlyOpenTitle) {
                            $('#ttt').tabs('close', n);
                        }
                    });
                    break;
                case "closeright":
                    var tabIndex = $('#ttt').tabs('getTabIndex', currentTab);

                    if (tabIndex == alltabs.length - 1) {
                        alert('亲，后边没有啦 ^@^!!');
                        return false;
                    }
                    $.each(allTabtitle, function (i, n) {
                        if (i > tabIndex) {
                            if (n != onlyOpenTitle) {
                                $('#ttt').tabs('close', n);
                            }
                        }
                    });

                    break;
                case "closeleft":
                    var tabIndex = $('#ttt').tabs('getTabIndex', currentTab);
                    if (tabIndex == 0) {
                        alert('亲，前边没有啦 ^@^!!');
                        return false;
                    }
                    $.each(allTabtitle, function (i, n) {
                        if (i < tabIndex) {
                            if (n != onlyOpenTitle) {
                                $('#ttt').tabs('close', n);
                            }
                        }
                    });

                    break;
                case "exit":
                    $('#closeMenu').menu('hide');
                    break;
            }
        }

    </script>
    <script>
        $(function () {
            $("#paydialog").dialog('close');
            $('#psbtnCancel').bind('click', function () {
                $("#paydialog").dialog('close');
            });
        });
        function CheckUserName()
        {
            var loginname = $('#pay_username').val();
            //$('#pay_name').html('<span><i class="am-icon-spinner am-icon-spin"></i>正在加载</span>');
            $.ajax({
                type: "post",
                url: "/tools/pay_ajax.ashx?action=checkusername",
                data: { "loginname": loginname },
                dataType: "text",
                success: function (data) {
                    if (data == "none")
                    {
                        $.messager.alert('系统提示', '该用户不存在，请检查用户名！', 'error');
                        $('#pay_username').val('');
                        $('#pay_name').val('');
                    }
                    else if (data == "false") {
                        $.messager.alert('系统提示', '系统错误，请稍后再试！', 'error');
                    }
                    else {
                        //$('#pay_name').val(data);
                        var array2 = data.split('$');
                        $('#pay_name').val(array2[0]);
                        $('#hiduseridout').val(array2[1]);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest + "," + textStatus + "," + errorThrown);
                },
                complete: function (XMLHttpRequest, textStatus) {
                    this; // 调用本次AJAX请求时传递的options参数
                }
            });
        }

        function CheckMoney()
        {
            var money = $('#money').val();
            if ($.trim(money) == '' || isNaN(money)) {
                $.messager.alert('系统提示', '请输入正确的金额！', 'error');
                $('#money').val('');
                return;
            }
            else {
                $('#money').val(parseFloat(money).toFixed(2) + "(元)");
            }
        }
    </script>
    <script type="text/javascript">
        function loginout() {
            window.opener = null;
            window.open('', '_self', '');
            window.close();
        }

        function relogin() {
            window.parent.location.href = 'Login.aspx';
        }

        function sendml() {
            var username = $('#hidusername').val();
            var userid = $('#hiduserid').val();
            $.post('tools/pay_ajax.ashx?type=payml&username=' + encodeURI(username) + '&userid=' + encodeURI(userid), function (msg) {
                if (msg == 'full') {
                    $.messager.alert('系统提示', '发送失败，今日发送次数达到最大', 'error');
                    $('#txtLogin').val('');
                } else if (msg == 'true') {
                    $.messager.alert('系统提示', '发送成功，请注意查收邮件', 'info');
                } else {
                    $.messager.alert('系统提示', '发送失败，请稍后再试', 'error');
                }
            });
        }

        function pay() {
            $('#paydialog').dialog('open');
            $('#pay_username').val('');
            $('#pay_name').val('');
            $('#money').val('');
            $('#pay_remark').val('');
        }

        function TransferAccount() {
            var outuserid = $('#hiduserid').val();
            var inuserid = $('#hiduseridout').val();
            var money = $('#money').val();
            var remark = $('#pay_remark').val();

            if ($('#pay_username').val() == "")
            {
                $.messager.alert('系统提示', '收款人不能为空！', 'error');
                return;
            }
            else if (money == "") {
                $.messager.alert('系统提示', '转账金额不能为空！', 'error');
                return;
            }
            else if (inuserid == outuserid) {
                $.messager.alert('系统提示', '不能向自己转账！', 'error');
                $('#pay_username').val('');
                $('#pay_name').val('');
                return;
            }
            else {
                $.ajax({
                    type: "post",
                    url: "/tools/pay_ajax.ashx?action=suborder",
                    data: { "inuserid": inuserid, "outuserid": outuserid, "money": money, "remark": remark },
                    dataType: "text",
                    success: function (data) {
                        if (data == "false") {
                            $.messager.alert('系统提示', '提交失败，请稍后再试！', 'error');
                            return;
                        }
                        else {
                            $('#hidorderid').val(data);
                            $("#paydialog").dialog('close');
                            var openurl = 'Pay/TransferAccount.aspx?orderid=' + $('#hidorderid').val();
                            var d = top.dialog({
                                id: 'dialogOrderProcess',
                                width: '400',
                                height: '200',
                                url: openurl
                            }).showModal();
                            return false;
                        }
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
    <script type="text/javascript">
        if ($('#hidtype').val() != 'pc') {
            var mobileAgent = new Array("iphone", "ipod", "ipad", "android", "mobile", "blackberry", "webos", "incognito", "webmate", "bada", "nokia", "lg", "ucweb", "skyfire");
            var browser = navigator.userAgent.toLowerCase();
            var isMobile = false;
            for (var i = 0; i < mobileAgent.length; i++) {
                if (browser.indexOf(mobileAgent[i]) != -1) {
                    isMobile = true;
                    //alert(mobileAgent[i]); 
                    location.href = 'mobile/Default.aspx';
                    break;
                }
            }
        }
    </script>
    <style>
        .popBtn {
            width: 18px;
            height: 18px;
            cursor: pointer;
            float: right;
            margin-left: 1px;
            margin-top: 12px;
            display: inline;
            background: url(images/popbuttons.png) no-repeat;
        }

        .popClose {
            margin-right: 10px;
            width: 15px;
            background-position: -39px 0;
        }

            .popClose:hover {
                background-position: -39px -20px;
            }

        .popShow {
            background-position: 0px 0;
        }

            .popShow:hover {
                background-position: 0px -20px;
            }

        .popHide {
            background-position: -19px 0;
        }

            .popHide:hover {
                background-position: -19px -20px;
            }
    </style>
    <style>
        #PcPoPmarket img {
            border: 0;
            display: block;
            clear: both;
        }

        #PcPoPmarket i, #PcPoPmarket em {
            font-style: normal;
        }
        /* link */
        #PcPoPmarket a {
            color: #333;
            text-decoration: none;
        }

            #PcPoPmarket a:hover {
                color: #f60;
                text-decoration: underline;
            }

            #PcPoPmarket a.hot {
                color: #f00;
            }

                #PcPoPmarket a.hot:hover {
                    color: #f60;
                }

        #PcPoPmarket .clear {
            clear: both;
        }

        #lmt {
            width: 444px;
            height: 232px;
            padding: 3px;
            overflow: hidden;
            background: url(images/popbg_open.png) no-repeat;
        }

            #lmt .top {
                width: 450px;
                height: 36px;
                line-height: 36px;
                overflow: hidden;
                text-indent: 10px;
                text-align: left;
            }

                #lmt .top span {
                    font-weight: bold;
                    color: #c00;
                    font-size: 14px;
                }

                #lmt .top a:hover span, #lmt .top a:hover {
                    color: #f60;
                    text-decoration: underline;
                }

            #lmt .body_l {
                text-align: center;
                clear: both;
                height: 208px;
                width: 100px;
                float: left;
                padding: 0 4px;
                line-height: 18px;
                padding-top: 3px;
                _padding-top: 0px;
            }

                #lmt .body_l img {
                    border: 1px solid #dedede;
                    padding: 3px;
                    background: #fff;
                }

            #lmt .body_r {
                text-align: left;
                width: 235px;
                overflow: hidden;
                float: left;
                margin: 0 auto;
            }

                #lmt .body_r h2 {
                    color: #d00;
                    padding: 5px 0 5px 0px;
                }

                    #lmt .body_r h2 a {
                        color: #d00;
                        font-size: 16px;
                        font-family: "Microsoft Yahei";
                    }

                #lmt .body_r em, #lmt .body_r em a {
                    color: #777;
                }

            #lmt li {
                list-style: none;
                text-align: left;
                margin: 10px 10px;
            }

                #lmt li a {
                    color: #777;
                    font-size: 13px;
                    line-height: 18px;
                    font-family: "Microsoft Yahei";
                    font-weight: bold;
                }

                #lmt li img {
                    width: 15px;
                    height: 14px;
                    margin: 2px 0;
                    float: left;
                }

                #lmt li a span {
                    font-size: 13px;
                    line-height: 18px;
                    font-family: "Microsoft Yahei";
                    font-weight: bold;
                    margin-left: 7px;
                }

            #lmt .MsgUL {
                margin: 5px 0;
            }

                #lmt .MsgUL li {
                    float: left;
                    width: 222px;
                    margin: 0;
                    padding: 0;
                    line-height: 35px;
                    height: 30px;
                    text-align:center;
                    font-size: 13px;
                }

                    #lmt .MsgUL li font {
                        font-size: 13px;
                    }

        .dd {
            clear: both;
            border-top: 1px solid #EBE9E3;
            margin: 0 35px;
        }

        .red {
            color: red;
        }
    </style>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">


        <div data-options="region:'north',border:false" style="height: 90px; background: url(Images/topbg.gif) repeat-x;">
            <div class="topleft">
                <img src="Images/logod.png" title="系统首页" />
            </div>

            <div class="topright">
                <ul>
                    <li><span>
                        <img src="images/house.png" title="首页" class="helpimg" /></span><a href="#" onclick="AddTab('main.aspx','系统首页')">首页</a></li>
                    <%-- <li><a href="#">关于</a></li>--%>
                    <li>
                        <a href="javascript:pay()">转账</a>
                    </li>
                    <li>
                        <a href="javascript:sendml()">发送密令</a>
                    </li>
                    <li>
                        <a href="mobile/Default.aspx">手机版</a>
                    </li>
                    <li>
                        <a href="javascript:relogin()">退出</a></li>
                    <li>
                        <a href="javascript:loginout()" target="_parent">关闭</a></li>
                </ul>
                <div class="user">
                    <span><%=NameData %></span>
                    <i onclick="AddTab('UpdatePassWord.aspx','账户密码修改')">
                        <img src="Images/d02.png" title="密码修改" /></i>
                    <i id="tip" onclick="showDiv()">
                        <img src="Images/dp.png" title="提醒" /></i>
                </div>

            </div>
        </div>
        <div data-options="region:'west',split:true,title:'导航菜单'" style="width: 200px;" id="westreg">
            <div id="aa" class="easyui-accordion" data-options="fit:true" border="false">
                <%=MenuData %>
            </div>
        </div>

        <div data-options="region:'center'" style="border: 0px;">
            <div id="ttt" class="easyui-tabs" data-options="fit:true">
                <div title="系统首页" iconcls="icon-remove">

                    <iframe width="100%" height="100%" frameborder="0" src="main.aspx" style="width: 100%; height: 100%;"></iframe>
                </div>
            </div>
            <div id="mm" class="easyui-menu" style="width: 150px;">
                <div id="refresh">
                    刷新
                </div>
                <div class="menu-sep">
                </div>
                <div id="close">
                    关闭
                </div>
                <div id="closeall">
                    全部关闭
                </div>
                <div id="closeother">
                    除此之外全部关闭
                </div>
                <div class="menu-sep">
                </div>
                <div id="closeright">
                    当前页右侧全部关闭
                </div>
                <div id="closeleft">
                    当前页左侧全部关闭
                </div>
                <div class="menu-sep">
                </div>
                <div id="exit">
                    退出
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function showDiv(innerCall) {
                if (document.getElementById('PcPoPmarket') == null) return;
                document.getElementById('PcPoPmarket').style.display = 'block';
                document.getElementById('PcPoPmarket').style.width = 400 + 'px';
                document.getElementById('PcPoPmarket').style.height = 238 + 'px';
                document.getElementById('showvod').style.display = 'none';
                document.getElementById('hidevod').style.display = 'block';
                var lmt = document.getElementById('lmt');
                if (lmt != null) {
                    lmt.style.backgroundImage = 'url(images/popbg_open.png)';
                }
            }

            function hideDiv(innerCall) {
                if (document.getElementById('PcPoPmarket') == null) return;

                document.getElementById('PcPoPmarket').style.width = 226 + 'px';
                document.getElementById('PcPoPmarket').style.height = 37 + 'px';
                document.getElementById('hidevod').style.display = 'none';
                document.getElementById('showvod').style.display = 'block';
                var lmt = document.getElementById('lmt');
                if (lmt != null) {
                    lmt.style.backgroundImage = 'url(images/popbg_min.png)';
                }
            }

            function closeDiv(innerCall) {
                if (document.getElementById('PcPoPmarket') == null) return;
                document.getElementById('PcPoPmarket').style.display = 'none';
            }
        </script>
        <div style="z-index: 1000; right: 0px; bottom: 0px; overflow-x: hidden; overflow-y: hidden; position: fixed; width: 450px; height: 238px;" id="PcPoPmarket">
            <div id="popTop" style="z-index: 1000; position: absolute; right: 0; height: 30px; overflow: hidden;">
                <span class="popBtn popClose" onclick="closeDiv()"></span>
                <span class="popBtn popShow" onclick="showDiv()" id="showvod" style="display: none;"></span>
                <span class="popBtn popHide" onclick="hideDiv()" id="hidevod" style="display: block;"></span>
            </div>
            <div id="popFrame" width="450" height="238">
                <div id="ivy_div" style="display: none;"></div>
                <div id="lmt">
                    <div class="top"><span>消息提醒</span></div>
                    <div>
                        <%=MsgData %>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>

        <!--修改密码-->
            <div id="paydialog" class="easyui-dialog" title="转账支付" style="width: 250px; height: 220px;" data-options="buttons:'#uppass'" >
            <table cellpadding="0" cellspacing="1px" style="width: 100%;" border="1">
                <tr style="height: 32px;">
                    <td align="right">收款帐号:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px;">
                        <input type="text" id="pay_username" onchange="CheckUserName();" runat="server"/>
                    </td>
                </tr>
                <tr style="height: 32px;">
                    <td align="right">收&nbsp;款&nbsp;人:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <input type="text" id="pay_name" runat="server" readonly="readonly" style="color:gray;"/>
                    </td>
                </tr>
                   <tr style="height: 32px;"> 
                    <td align="right">金&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;额:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <input type="text" id="money" runat="server" style="color:orange;width:127px;font-weight:bold;" onchange="CheckMoney();"/>
                    </td>
                </tr>
                <tr style="height:32px;"> 
                    <td align="right">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <input type="text" id="pay_remark" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
            <div id="uppass" style="text-align:center">
                <a id="psbtnConfirm" href="#"  class="easyui-linkbutton" runat="server" onclick="TransferAccount();">确认支付</a>
                <a id="psbtnCancel" href="#" class="easyui-linkbutton" >取消</a>
            </div>
        <input type="hidden" id="hftip" runat="server" value="" />
        <input type="hidden" id="hiduserid" runat="server" />
        <input type="hidden" id="hiduseridout" runat="server" />
        <input type="hidden" id="hidusername" runat="server" />
        <input type="hidden" id="hidtype" runat="server" />
        <input type="hidden" id="hidorderid" runat="server" /> 
    </form>
</body>
</html>

