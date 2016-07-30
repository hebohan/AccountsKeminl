<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDetail.aspx.cs" Inherits="Accounts.Web.mobile.AccountsManage.AccountDetail" %>
<!DOCTYPE html>

<html>
	<head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"> 
		<title>账单详情 - 账单管理系统 Keminl.cn</title>
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
        <script language="javascript" type="text/javascript" src="../../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
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
                background: #666 url(img/235083-12101020595998.jpg) no-repeat;
                background-size:100% 100%;
            }
        </style>
        <script type="text/javascript">
            function getdata() {
                $("#a_detail").html("<img src='../img/loading.gif' style='width:30px;height:30px;'/>");
                $("#a_detail").attr("style", "padding-top:15px;background-color:white;text-align:center");
                var userid = $('#hiduserid').val();
                var accountid = $('#hidaid').val();
                var tempid = $('#hidtempid').val();
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=getaccountdetail",
                    data: { "userid": userid, "id": accountid ,"tempid":tempid},
                    dataType: "text",
                    success: function (data) {
                        $("#a_detail").attr("style", "padding-top:15px;background-color:white;");
                        $("#a_detail").html(data);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(XMLHttpRequest + "," + textStatus + "," + errorThrown);
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });
            }

            function savedata() {
                var userid = $('#hiduserid').val();
                var accountid = $('#hidaid').val();
                var remark = $('#remark').val();
                var status = $('#ddlIndustry').val();
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=saveaccount",
                    data: { "userid": userid, "id": accountid, "remark": remark, "status": status },
                    dataType: "text",
                    success: function (data) {
                        JsPrint(data, "success");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(XMLHttpRequest + "," + textStatus + "," + errorThrown);
                        JsPrint("操作错误", "error");
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });
            }

            $(document).ready(function () {
                getdata();
            });

            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);
                }
                else if (type == "delete_success")
                {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess2);
                }
                else {
                    $.dialog.alert(msg);
                }
            }

            function callsuccess() {
                window.location.reload();
            }

            function callsuccess2() {
                window.location.href = "AccountsList.aspx";
            }

            function addremark() {
                $('#remark_panel').show();
                $('#RcancleBtn').show();
                $('#remarktip').hide();
                $('#ConfirmBtn').val("提交");
                $('#ConfirmBtn').show();
                $('#remarkbtn').hide();
            }

            function remarkcancle(id) {
                $('#remark_panel').hide();
                $('#RcancleBtn').hide();
                $('#remarktip').show();
                $('#ConfirmBtn').val("认定");
                if (id == "1") {
                    $('#ConfirmBtn').hide();
                }
                $('#remarkbtn').show();
            }

            function NewUrl() {
                $('#new').hide();
                $('#url_title').hide();
                $('#newcancle').show();
                $('#add').show();
                $('#_name_panel').show();
                $('#_url_panel').show();
                $('#u_name').show();
                $('#u_url').show();
            }

            function NewCancle() {
                $('#new').show();
                $('#url_title').show();
                $('#add').hide();
                $('#newcancle').hide();
                $('#_name_panel').hide();
                $('#_url_panel').hide();
                $('#u_name').hide();
                $('#u_url').hide();
            }

            function AddUrl() {
                var id = $('#hidaid').val();
                var u_name = $('#u_name').val();
                var u_url = $('#u_url').val();
                if (u_name == "") {
                    u_name = u_url;
                }
                if (u_url != "") {
                    if (id != "") {
                        $.ajax({
                            type: "post",
                            url: "/tools/do_ajax.ashx?action=addurl",
                            data: { "id": id, "name": u_name, "url": u_url },
                            dataType: "json",
                            success: function (data, textStatus) {
                                if (data.status == "true") {
                                    $('#_name_panel').hide();
                                    $('#u_name').hide();
                                    $('#_url_panel').hide();
                                    $('#u_url').hide();
                                    $('#add').hide();
                                    $('#newcancle').hide();
                                    $('#modify').show();
                                    $('#url_label').show();
                                    $('#url_label').html(data.url);
                                    $('#hidurlname').val(u_name);
                                    $('#hidurl').val(u_url);
                                    $('#url_title').show();
                                } else {
                                    JsPrint("添加链接失败", "error");
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                JsPrint("添加链接成功", "success");
                                window.parent.location.reload();
                            },
                            complete: function (XMLHttpRequest, textStatus) {
                                this; // 调用本次AJAX请求时传递的options参数
                            }
                        });
                    }
                }
                else {
                    JsPrint("请输入网址", "error");
                }
            }

            function relogin() {
                window.parent.location.href = '../../Login.aspx';
            }

            function Confirm() {
                var id = $('#hidaid').val();
                var u_name = $('#u_name').val();
                var u_url = $('#u_url').val();
                if (u_name == "") {
                    u_name = u_url;
                }
                if (u_url != "") {
                    if (id != "") {
                        $.ajax({
                            type: "post",
                            url: "/tools/do_ajax.ashx?action=addurl",
                            data: { "id": id, "name": u_name, "url": u_url },
                            dataType: "json",
                            success: function (data, textStatus) {
                                if (data.status == "true") {
                                    $('#_name_panel').hide();
                                    $('#u_name').hide();
                                    $('#_url_panel').hide();
                                    $('#u_url').hide();
                                    $('#add').hide();
                                    $('#modify').show();
                                    $('#hidurlname').val(u_name);
                                    $('#hidurl').val(u_url);
                                    $('#url_label').show();
                                    $('#url_title').show();
                                    $('#url_label').html(data.url);
                                    $('#confirm').hide();
                                    $('#cancle').hide();
                                } else {
                                    alert("修改链接失败");
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                JsPrint("修改链接成功", "success");
                            },
                            complete: function (XMLHttpRequest, textStatus) {
                                this; // 调用本次AJAX请求时传递的options参数
                            }
                        });
                    }
                }
                else {
                    $('#_name_panel').show();
                    $('#u_name').show();
                    $('#_url_panel').show();
                    $('#u_url').show();
                    $('#add').show();
                    $('#modify').hide();
                    $('#hidurlname').val(u_name);
                    $('#hidurl').val(u_url);
                    $('#url_label').hide();
                    $('#confirm').hide();
                    $('#cancle').hide();
                    $('#url_title').show();
                }
            }

            function ModifyUrl() {
                $('#url_title').hide();
                $('#confirm').show();
                $('#cancle').show();
                $('#_name_panel').show();
                $('#u_name').show();
                $('#_url_panel').show();
                $('#u_url').show();
                $('#add').hide();
                $('#url_label').hide();
                $('#modify').hide();
                $('#u_name').val($('#hidurlname').val());
                $('#u_url').val($('#hidurl').val());
            }

            function Cancle() {
                $('#url_title').show();
                $('#url_label').show();
                $('#confirm').hide();
                $('#cancle').hide();
                $('#_name_panel').hide();
                $('#u_name').hide();
                $('#_url_panel').hide();
                $('#u_url').hide();
                $('#add').hide();
                $('#modify').show();
            }

            function AC_Delete(id) {
                $.dialog.confirm("请确定是否删除该条账单?", function () {
                    var userid = $('#hiduserid').val();
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=ac_delete",
                        data: { "id": id, "userid": userid},
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
                            JsPrint("删除账单成功", "success");
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            this; // 调用本次AJAX请求时传递的options参数
                        }
                    });
                }, function () {

                });
                
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
                select {
                    margin: 0;
                    padding: 0;
                    outline: none;
                    height: 20px;
                    line-height: 25px;
                    width: 75px;
                    border: rgb(191, 204, 220) 1px solid;
                    border-radius: 3px;
                    display: inline-block;
                    font: normal 12px "微软雅黑", "SimSun", "宋体", "Arial";
                    background-size: 5px 5px,5px 5px,25px 25px,10px 25px;
                    background-image: repeating-linear-gradient(225deg,rgb(105,123,149) 0%,rgb(105,123,149) 50%,transparent 50%,transparent 100%), repeating-linear-gradient(135deg,rgb(105,123,149) 0%,rgb(105,123,149) 50%,transparent 50%,transparent 100%), linear-gradient( #FFFFFF 0%,#F8F9Fd, #EFFAFA 100%), repeating-linear-gradient( rgb(191, 204, 220) 0%,rgb(191, 204, 220) 100%);
                    background-repeat: no-repeat;
                    background-position: 55px 7px,60px 7px,right top,92px top;
                    -webkit-appearance: none;
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
    			<a href="#title-link" style="font-size:16px;padding-top:0px;" >账单详情</a>
  			</h1>
            <%=Menu %>
		</header>
        <form runat="server">
            <div style="padding-top:20px;background-color:white;" id="a_detail">
                
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
        <input type="hidden" id="hidaid" runat="server" />
        <input type="hidden" id="hidtempid" runat="server" />
	</body>
    
</html>
