<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Accounts.Web.mobile.Default" %>
<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<!DOCTYPE html>

<html>
	<head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"> 
		<title>个人中心 - 账单管理系统 Keminl.cn</title>
		<meta name="description" content="">
		<meta name="keywords" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<meta name="renderer" content="webkit">
        <%--<link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />--%>
		<meta http-equiv="Cache-Control" content="no-siteapp" />
		<link rel="icon" type="image/png" href="i/favicon.png">
		<link rel="stylesheet" href="css/app.css">
        <link rel="stylesheet" href="css/index.css" />
        <link rel="stylesheet" href="css/baoliao.css" />
        <link rel="stylesheet" href="css/owl.carousel.css" />
		<script src="js/jquery.min.js"></script>
        <link rel="stylesheet" href="css/amazeui.min.css">
		<script src="js/amazeui.min.js"></script>
		<script src="js/layer/layer.js"></script>
        <script src="../../CSS/layer/layer.js"></script>
        <script language="javascript" type="text/javascript" src="../../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
        <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
        <script src="../../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>
        <link href="../../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
        <script>
            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);

                } else {
                    $.dialog.alert(msg);
                }
            }
        </script>
        <script type="text/javascript">
            function getdata(page) {
                //$(".account_more").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='img/loading.gif' style='width:16px;height:16px;'/>");
                $(".am_news_load").html('<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i  class="am-icon-spinner am-icon-spin"></i>正在加载</span>');
                var userid = $('#hiduserid').val();
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=getaccount",
                    data: { "userid": userid, "page": page },
                    dataType: "text",
                    success: function (data) {
                        var array = data.split(",");
                        var nums = [];
                        var flag = 0;
                        if (data != "") {
                            for (var i = 0 ; i < array.length ; i++) {
                                var array2 = array[i].split("&");
                                var nums2 = [];
                                for (var j = 0 ; j < array2.length ; j++) {
                                    var m = (parseInt(page) - 1) * 5 + i;
                                    if (j == 0) {
                                        $("#item" + m + j).onclick = showpanel(array2[j], m);
                                    }
                                    else if (j == 1) {
                                        $("#item" + m + j).attr('src', '../Images/' + array2[j] + '.gif');
                                    }
                                    else {
                                        document.getElementById("item" + m + j).innerHTML = array2[j];
                                    }
                                    $("#dditem" + m + "0").attr("class", "am-accordion-bd am-collapse");
                                    $("#item" + m + "0").attr("class", "am-accordion-item");
                                    $("#item" + m + "0").show();
                                }
                                if (m == 19) {
                                    $('#hidflag').val(m);
                                }
                            }
                            $(".am_news_load").html('<span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;加载更多</span>');
                        }                        else {
                            if (parseInt($('#hidflag').val()) == 19) {
                                $(".am_news_load").html("此处仅显示近期20条记录，请点击<span onclick='jumpto(1)'><font color='red'>账单管理</font></span>查看全部账单");
                            }
                            else {
                                $(".am_news_load").html("<span onclick='jumpto(1)'>&nbsp;&nbsp;已全部加载完成</font>");
                            }
                            
                        }                        for (var m = (page - 1) * 5; m < page * 5 ; m++) {
                            $("#item" + m + "0").show();
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(XMLHttpRequest + "," + textStatus + "," + errorThrown);
                        $(".am_news_load").html("<span onclick='jumpto(1)'>&nbsp;&nbsp;已全部加载完成</font>");
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });
            }

            $(document).ready(function () {
                var page = 1;
                //$('#hidpage').val(page);
                //var total = parseInt($('#hidtotal').val());
                getdata(page);
                

                $('#more').click(function () {
                    page++;
                    //$('#hidpage').val(page);
                    //var total = parseInt($('#hidtotal').val());
                    //var all = parseInt($('#hidallcount').val());
                    getdata(page);
                });
            });

            function relogin() {
                window.parent.location.href = '../../Login.aspx';
            }

            function page(num)
            {
                var page = parseInt(num);
                $('#hidpage').val(page);
                getdata(page);
            }

            function jumpto(url)
            {
                if (url == 1)
                {
                    window.open("AccountsManage/AccountsList.aspx");
                } 
                
            }
            function showpanel(id,i) {
                $("#dditem" + i + "0").html("<span><i  class='am-icon-spinner am-icon-spin'></i> 正在加载</span>");
                //$(".am_news_load").html('');
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=getdetail",
                    data: { "id": id},
                    dataType: "text",
                    success: function (data) {                        $("#dditem" + i + "0").html(data);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });
            }

            function ChangeMoney()
            {
                $('#confirm_b').show();
                $('#cancle_b').show();
                $('#money_dis').hide();
                $('#hidmoney_text').val($('#money_dis').html());
                $('#hidmoney_text').show();
                //$('#').hide();
            }

            function cancle_bb()
            {
                $('#confirm_b').hide();
                $('#cancle_b').hide();
                $('#hidmoney_text').hide();
                $('#money_dis').show();
            }

            function update_cash() {
                var cash = $('#hidmoney_text').val();
                if ($.trim(cash) == '' || isNaN(cash)) {
                    layer.open({
                        content: '请输入正确的金额!',
                        btn: ['OK']
                    });
                    return;
                }
                var his_cash = $('#money_dis').html();
                var d_value = (parseFloat(cash) - parseFloat(his_cash)).toFixed(2);
                var userid = $('#hiduserid').val();
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=updatecash",
                    data: { "userid": userid ,"d_value":d_value},
                    dataType: "text",
                    success: function (data) {
                        if (data == "true") {
                            $.messager.alert('系统提示', '资产更正成功', 'info');
                            $('#confirm_b').hide();
                            $('#cancle_b').hide();
                            $('#money_dis').html(cash);
                            $('#his_total').html((parseFloat($('#his_total').html()) + parseFloat(d_value)).toFixed(2));
                            $('#money_dis').show();
                            $('#hidmoney_text').hide();
                        }
                        else {
                            $.messager.alert('系统提示', '资产更正失败，请稍后再试', 'error');
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.messager.alert('系统提示', '资产更正失败，请联系管理员', 'error');
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });

            }
</script>
    <style>
        .Textbox {
            font-weight:bold;
            color:orange;
            width:60px;
            border:0;
            text-align:center;
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
	</head>

	<body class="am-background-disable">
		<header data-am-widget="header" class="am-header am-header-default am-header-fixed">
			<div class="am-header-left am-header-nav">
				<a href="Default.aspx" class="">

					<i class="am-header-icon am-icon-home"></i>
				</a>
			</div>
			<h1 class="am-header-title">
    			<a href="#title-link" style="font-size:16px;padding-top:0px">用户中心</a>
  			</h1>

            <%=Menu %>
		</header>
        <form runat="server">
        <!-- 用户头像区域 -->
		<div class="am-panel">
			<%--<div class="am-panel-bd am-g" style="padding-left:40px;background-image:url(../head_image/default.gif);background-repeat:no-repeat">--%>
            <div class="am-panel-bd am-g" style="padding-left:8%;background-color:white;padding-right:8%;padding-top:0px;">
				<div class="login-ico-zdy am-margin-top-xl" style="float:left">
                    <img src="../head_image/<%=headpic == null || headpic == ""? "default.gif":headpic%>" style="width:88px;height:88px;">
                </div>
                <div style="float:right;font-size:12px;padding-top:33px">
                    <ul>
                    <li >
                        <div>
                            <h4>尊敬的用户 <%=username %>，欢迎您！</h4>
                            <div>
                                <a href="javascript:void(0)" onclick="ChangeMoney()">资产更正</a>
                                &nbsp;&nbsp;&nbsp;&nbsp;<a href="UpdatePass.aspx" onclick="OpenImportPass()">修改密码</a>
                            </div>
                        </div>
                        <div style="padding-top:5px;">
                            总资产：<font style="font-weight:bold;color:orange;"><span id="his_total"><%=totalmoney %></span></font>元
                        </div>
                        <div style="padding-top:5px;padding-left:1px">
                            现&nbsp;&nbsp;&nbsp;金：<font style="font-weight:bold;color:orange;"><span id="money_dis"><%=cash %></span></font>
                            <input type="text" id="hidmoney_text" runat="server" style="display:none;width:70px;font-weight:bold;color:orange;"/>元
                        </div>
                        <input type="button" id="confirm_b" runat="server" onclick="update_cash();" value="确定" class="button gray larrow" style="display:none;"/>&nbsp;&nbsp;
                        <input type="button" id="cancle_b" runat="server" onclick="cancle_bb();" value="取消" class="button gray larrow" style="display:none;"/>
                    </li>

                </ul>
                </div>

			</div>
		</div>

        <!-- 账单提示区域 -->
        <div class="am-panel">
            <div class="am-panel-bd am-g" style="padding-left:4%;background-color:white;padding-right:4%;padding-top:15px;padding-bottom:10px">
                <div style="font-size:xx-small">
                    <h1 style="text-align:center;font-size:18px;margin:0">账单提示</h1>
                    <ul style="float:left;font-size:12px">
                        <li>有【<a href="AccountsManage/AccountsList.aspx?status=dq"><font color="red"><%=dq_count %></font></a>】项账单30天内到期</li>
                        <li>有【<a href="AccountsManage/AccountsList.aspx?status=wrc"><font color="red"><%=waitg_count %></font></a>】项账单待收款</li>
                        <li>有【<a href="AccountsManage/AccountsList.aspx?status=wrp"><font color="red"><%=waitr_count %></font></a>】项账单待还款</li>
                    </ul>
                    <ul style="float:right;font-size:12px">
                        <li>有【<a href="AccountsManage/AccountsList.aspx?status=late"><font color="red"><%=late_count %></font></a>】项账单延期中</li>
                        <li>有【<a href="AccountsManage/AccountsList.aspx?status=finish"><font color="red"><%=finish_count %></font></a>】项账单已完成</li>
                        <li>点此【<a href="AccountsManage/AccountRegisterDetail.aspx"><font color="red">创建新账单</font></a>】</li>
                    </ul>
                </div>
			</div>
		</div>

        <!-- 账单显示区域 -->
        <div class="am-panel-bd am-g" style="background-color:white;margin:0;padding-left:0px">
            <h1 style="text-align:center;font-size:18px;padding-bottom:10px">&nbsp;&nbsp;&nbsp;账单预警(共<font color="red"><%=CountNum %></font>条)</h1>
            <div style="font-size:14px;padding-left:8%;padding-right:5%;padding-bottom:10px;text-align:center;font-weight:bold;"><img src="../Images/green.gif" style="width: 15px; height: 15px; vertical-align: middle;" />7-30天账单，<img src="../Images/yellow.gif" style="width: 15px; height: 15px; vertical-align: middle;" />7天内账单，<img src="../Images/red.gif" style="width: 15px; height: 15px; vertical-align: middle;" />超期账单。</div>
            <section data-am-widget="accordion" class="am-accordion am-accordion-gapped" data-am-accordion='{  }' style="width:100%;padding-right:5px" data-accordion-settings="" id="account_list">
                <%=hiddetail.Value %>
                <span id="tip" style="font-size:xx-small;text-align:center"></span>
            </section>
            <%--<div class="bl_more"><span class="account_more" style="font-size:13px" id="more"></span></div>--%>
            <div class="am_news_load am-text-center" id="more" style="font-size:13px"></div>
        </div>
        </form>
        
		<footer data-am-widget="footer" class="am-footer am-footer-default" data-am-footer="{ }">
			<div class="am-footer-switch">
				<span class="am-footer-ysp" data-rel="mobile" data-am-modal="{target: '#am-switch-mode'}">
          手机版
    </span>
				<span class="am-footer-divider"> | </span>
				<a id="godesktop"  class="am-footer-desktop" href="http://account.keminl.cn/Default.aspx?type=pc">电脑版</a>
			</div>
			<div class="am-footer-miscs">

				<p>由 <a href="http://account.keminl.cn/" title="账单管理系统" target="_blank" class="">账单管理系统</a> 提供技术支持
				</p>
				<p>CopyRight©2016 AllMobilize Inc.</p>
				<p>Keminl.cn 账单管理系统</p>
			</div>
		</footer>
        <input type="hidden" id="hiduserid" runat="server" />
        <input type="hidden" id="hiddetail" runat="server" />
        <input type="hidden" id="hidcontent" runat="server" />
        <input type="hidden" id="hidpage" runat="server" />
        <input type="hidden" id="hidtotal" runat="server" />
        <input type="hidden" id="hidallcount" runat="server" /> 
        <input type="hidden" id="hidflag" runat="server" />



	</body>
    
</html>
