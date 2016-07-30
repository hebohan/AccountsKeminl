<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReminderList.aspx.cs" Inherits="Accounts.Web.mobile.ReminderManage.ReminderList" %>
<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<!DOCTYPE html>

<html>
	<head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"> 
		<title>提醒事项 - 账单管理系统 Keminl.cn</title>
		<meta name="description" content="">
        <link type="text/css" rel="stylesheet" href="../../share/css/NK1.1.css?v=0.7" />
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
        <script language="javascript" type="text/javascript" src="/share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
        <script language="javascript" type="text/javascript" src="/share/js/My97DatePicker/WdatePicker.js"></script>

        <script src="../../Scripts/artdialog/jquery-1.10.2.js"></script>
        <link rel="stylesheet" href="../../Scripts/artdialog/ui-dialog.css"/>
        <script src="../../Scripts/artdialog/dialog-plus.js"></script>

        <script>
            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);

                } else {
                    $.dialog.alert(msg);
                }
            }
        </script>
        <style >
            .background {
                /*width: 200px;
                height: 100px;*/
                -moz-background-size:cover;
                -webkit-background-size:cover;
                -o-background-size:cover;
                background-size:cover;
                /*color: #fff;*/
                font-size: 12px;
                /*border: 10px dotted #333;*/
                /*padding: 10px;*/
                background: #666 url(img/background.png) no-repeat;
                background-size:100% 100%;
            }
        </style>
        <script type="text/javascript">
            function getdata(page) {
                $(".am_news_load").html('<span><i  class="am-icon-spinner am-icon-spin"></i>正在加载</span>');
                var userid = $('#hiduserid').val();
                var keyword = $('#keyword').val();
                var type = $('#ReminderType').val();
                var order = $('#Order').val();
                var sdate = $('#sDate').val();
                var edate = $('#eDate').val();
                var ssdate = new Date(sdate);
                var eedate = new Date(edate);
                if (ssdate > eedate) {
                    layer.open({
                        content: '截至日期不能小于起始日期!',
                        btn: ['OK']
                    });
                    return;
                }
                
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=getrlist",
                    data: { "userid": userid, "page": page, "order": order, "sdate": sdate, "edate": edate, "type": type, "keyword": keyword },
                    dataType: "text",
                    success: function (data) {
                        var array = data.split("*&*");
                        var content = array[0];
                        var totalcount = array[1];
                        if (content != "") {
                            $(".am_news_load").html('<span> 加载更多</span>');
                            $('#list_panel').append(content);
                            $('#countnum').text(totalcount);
                        }
                        else {
                            $(".am_news_load").html("已全部加载完成");
                            $('#countnum').text(totalcount);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(XMLHttpRequest + "," + textStatus + "," + errorThrown);
                        $(".am_news_load").html("已全部加载完成");
                        $('#countnum').text(totalcount);
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });
            }

            function relogin() {
                window.parent.location.href = '../../Login.aspx';
            }

            $(document).ready(function () {
                var page = 1;
                getdata(page);

                $('#more').click(function () {
                    page++;
                    getdata(page);
                });

                $('#btnSearch').click(function () {
                    page = 1;
                    $('#list_panel').html("");
                    getdata(page);
                });
            });

            function jumpto(url)
            {
                if (url == 1)
                {
                    window.open("../Default.aspx");
                } 
                
            }

            function ShowDetail(id) {
                var d = dialog({
                    id: 'dialogShowDetail',
                    width: '300',
                    height: '200',
                    title: '交易详情',
                    fixed: true,
                    url: 'DealDetail.aspx?id=' + id
                }).showModal();
            }
</script>

            <style>
        img {
            width: 32px;
            height: 32px;
        }

        .mainbox {
            /*width: 100%;*/
        }

        .infoleft {
            margin-left: 10px;
            margin-bottom: 10px;
        }

        .inforight {
            margin-right: 10px;
            margin-bottom: 10px;
        }

        .infoall {
            width: 100%;
            margin: 0;
            border: #d3dbde solid 1px;
            height: 200px;
        }

        .newlist {
            padding-top:5px;
        }
        h1 {
            margin: 0;
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
    			<a href="#title-link" style="font-size:16px;padding-top:0px">提醒事项</a>
  			</h1>
            <%=Menu %>
		</header>
        <form runat="server">

        <!-- 账单显示区域 -->
            <table class="hei12" style="font-size:10px;height:30px;width:100%;">
                <tr>
                    <td style="width:18%;padding-left:15px;padding-top:5px">提醒类型：</td>
                    <td style="padding-top:5px">
                            <div style="width:90%">
                                <select ID="ReminderType" runat="server"></select>
                            </div>
                    </td>
                    <td style="width:18%;padding-top:5px">关键字：</td>
                    <td style="padding-top:5px">
                        <input type="text" id="keyword" runat="server" style="height:20px;width:60%" placeholder="请输入关键字" />
                    </td>
                </tr>
                <tr>
                    <td style="width:18%;padding-left:15px;padding-top:5px">起始日期：</td>
                    <td style="padding-top:5px">
                            <div style="width:90%">
                                <input type="text" runat="server" id="sDate" readonly="readonly" class="Wdate stext" onclick="WdatePicker()" style="width:75%"/>
                            </div>
                    </td>
                    <td style="width:18%;padding-top:5px">截至日期：</td>
                    <td style="padding-top:5px">
                            <div style="width:90%">
                                <input type="text" runat="server" id="eDate" readonly="readonly" class="Wdate stext" onclick="WdatePicker()" style="width:75%"/>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td style="width:18%;padding-left:15px;padding-top:5px">排序：</td>
                    <td style="padding-top:5px">
                            <div style="width:90%">
                                <select ID="Order" runat="server"></select>
                            </div>
                    </td>
                    <td class="stitle" style="width:12%;padding-top:5px">搜索：</td>
                    <td style="height:20px;padding-top:5px">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnSearch" class="btnSearch_diy" id="btnSearch" runat="server" style="height:28px;width:30px;"/>
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>

            <div style="padding-top:10px;background-color:white;padding-bottom:10px;">
                <h1 style="text-align:center;font-size:15px">提醒事项(共<font color="red" id="countnum"></font>条)</h1>
                 <section data-am-widget="accordion" class="am-accordion am-accordion-gapped" data-am-accordion='{}' id="list_panel">
                     
                 </section>
            </div>
            <div class="am_news_load am-text-center" id="more" style="font-size:13px;background-color:white"></div>
            <%--<div class="bl_more" style="background-color:white"><span class="deal_more" style="font-size:13px;text-align:center" id="more"></span></div>--%>
        </form>
        
		<footer data-am-widget="footer" class="am-footer am-footer-default" data-am-footer="{ }">
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
        <input type="hidden" id="hiddetail" runat="server" />
        <input type="hidden" id="hidcontent" runat="server" />
        <input type="hidden" id="hidpage" runat="server" />
        <input type="hidden" id="hidtotal" runat="server" />
        <input type="hidden" id="hidtotalpage" runat="server" />
        <input type="hidden" id="hidallcount" runat="server" /> 
        <input type="hidden" id="hidflag" runat="server" />
	</body>
    
</html>
