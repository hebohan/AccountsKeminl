<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReminderRegisterDetail.aspx.cs" Inherits="Accounts.Web.moblie.ReminderManage.ReminderRegisterDetail" %>

<!DOCTYPE html>
<html>
	<head>
		<meta charset="UTF-8">
		<title>提醒<%=hidaction.Value == "edit" ? "修改" :"新增" %> - 账单管理系统</title>
		<meta name="description" content="">
		<meta name="keywords" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<meta name="renderer" content="webkit">
		<meta http-equiv="Cache-Control" content="no-siteapp" />
		<link rel="icon" type="image/png" href="i/favicon.png">
		<link rel="stylesheet" href="../css/amazeui.min.css">
		<link rel="stylesheet" href="../css/app.css">
		<script src="../js/jquery.min.js"></script>
		<script src="../js/amazeui.min.js"></script>
		<script src="../js/layer/layer.js"></script>
        <script language="javascript" type="text/javascript" src="/share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
        <link rel="stylesheet" type="text/css" href="../../js/jquery-easyui-1.3.4/themes/metro/easyui.css" />
        <link rel="stylesheet" type="text/css" href="../../js/jquery-easyui-1.3.4/themes/icon.css" />
        <script type="text/javascript" src="../../js/jquery-easyui-1.2.4/jquery.easyui.min.js"></script>
        <%--<script language="javascript" type="text/javascript" src="/share/js/My97DatePicker/WdatePicker.js"></script>--%>
        <link rel="stylesheet" type="text/css" href="../js/DateTimePicker/src/DateTimePicker.css" />
		<script type="text/javascript" src="../js/DateTimePicker/jquery-1.11.0.min.js"></script>
		<script type="text/javascript" src="../js/DateTimePicker/src/DateTimePicker.js"></script>
        <script>
            function JsPrint(msg, type) {
                if (type == "success") {
                    $.dialog.tips(msg, 1, 'success.gif', callsuccess);
                    
                }
                else if (type == "close")
                {
                    $.dialog.tips(msg, 1, 'error.gif', callfail);
                }
                else {
                    $.dialog.alert(msg);
                }
            }

            function callsuccess()
            {
                if ($('#hidid').val() != "" && $('#hidaction').val() != "mbadd") {
                    window.location.href = "ReminderDetail.aspx?id=" + $('#hidid').val();
                }
                else {
                    window.location.href = "ReminderList.aspx";
                }
                
            }

            function callfail()
            {
                window.location.href = '../Default.aspx';
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

            //$(document).ready(function () {
            //    var action = $('#hidaction').val();
            //    if (action == "edit")
            //    {
            //        $('#ddlAcType').selected('disable');
            //        $('#ddlStatus').selected('disable');
            //    }
            //});

            function Save() {
                var reminder = $('#reminder').val();
                var r_title = $('#r_title').val();
                var r_detail = $('#r_detail').val();
                //var reminddate = $('#reminddate').val();
                //var remindclock = $('#remindtime').val();
                var action = $('#hidaction').val();
                var user = $('#hiduser').val();
                var id = $('#hidid').val();
                var remindtime = $('#reminddate').val() + " " + $('#remindtime').val();
                if (reminder == '' || r_title == '' || r_detail == '' || $('#reminddate').val() == '' || $('#remindtime').val() == '') {
                    $.messager.alert('系统提示', '请不要输入空信息', 'error');
                    return false;
                }
                else if (Date.parse(remindtime) < new Date().getTime())
                {
                    $.dialog.alert("提醒时间必须大于当前时间！");
                    return;
                }
                else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(reminder)) {
                    $.dialog.alert("提醒对象请输入邮箱！");
                    return;
                }
                else {
                    $.ajax({
                        type: "post",
                        url: "/tools/do_ajax.ashx?action=reminder_add",
                        data: { "reminder": reminder, "r_title": r_title, "r_detail": r_detail, "action": action, "userid": user, "remindtime": remindtime, "id": id },
                        dataType: "text",
                        success: function (data) {
                            if (data != "false") {
                                JsPrint(data, "success");
                            }
                            else {
                                JsPrint("操作失败，请稍后再试", "error");
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
    			<a href="#title-link">提醒<%=hidaction.Value == "edit" ? "修改" :"新增" %></a>
                <%--<img src="skin/default/icon.png" width="120px" />--%>
  			</h1>
            <%=Menu %>
		</header>
		<div class="am-panel">
			<div class="am-panel-bd am-g">
				<!-- 登陆框 -->
                <h1 style="text-align:center;font-size:18px;padding-bottom:10px;padding-top:5px">提醒<%=hidaction.Value == "edit" ? "修改" :(hidaction.Value == "mbadd"? "录入(通过模板录入)": "新增") %></h1>
				<div class="am-u-sm-11 am-u-sm-centered" >
					<form runat="server" action="" class="am-form" id="formtooltip" style="font-size:13px">
						<%--<fieldset class="login-form am-form-set">--%>

                        <label class="am-form-label" for="doc-ipt-success">提醒对象：</label>
                        <div class="am-form-group">
                            <input type="text" id="reminder"  runat="server" class="am-form-field login-input-text" minlength="1">
                        </div>

                        <label class="am-form-label" for="doc-ipt-success">提醒标题：</label>
                        <div class="am-form-group">
                            <input type="text" id="r_title" runat="server" class="am-form-field login-input-text" minlength="1">
                        </div>

                        <label class="am-form-label" for="doc-ipt-success">提醒内容：</label>
                        <div class="am-form-group">
                            <textarea id="r_detail"  runat="server" class="am-form-field login-input-text"></textarea>
                        </div>

                        <label class="am-form-label" for="doc-ipt-success">提醒日期：</label>
                        <div class="am-form-group ">
                            <%--<input type="text" runat="server" id="sDate1" class="am-form-field login-input-text" data-field="date" readonly"/>--%>
                            <input type="text" runat="server" id="reminddate" class="am-form-field login-input-text" data-field="date" readonly/>
                        </div>

                        <label class="am-form-label" for="doc-ipt-success">提醒时间：</label>
                        <div class="am-form-group ">
                            <input type="text" runat="server" id="remindtime" class="am-form-field login-input-text" data-field="time" readonly/>
                        </div>
                        <div id="dtBox"></div>
                        <script type="text/javascript">

                            $(document).ready(function () {
                                $("#dtBox").DateTimePicker(
                                {
                                    dateFormat: "yyyy-MM-dd",
                                    dateTimeFormat: "yyyy-MM-dd HH:mm:ss",
                                    timeFormat: "HH:mm",
                                    shortDayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
                                    fullDayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
                                    shortMonthNames: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
                                    fullMonthNames: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
                                    titleContentDate: "设置日期",
                                    titleContentTime: "设置时间",
                                    titleContentDateTime: "设置日期和时间",
                                    buttonsToDisplay: ["HeaderCloseButton", "SetButton", "ClearButton"],
                                    setButtonContent: "确定",
                                    clearButtonContent: "取消"
                                });
                            });

		                </script>
                        <%--<label class="am-form-label">状态：</label>
                        <div class="am-form-group">
                            <select data-am-selected="{btnWidth: '100%',btnSize: 'sm'}" id="ddlStatus" runat="server">
                            </select>
                        </div>--%>
						<%--</fieldset>--%>
                        <input type="button" id="SaveBtn" runat="server" onclick="Save()" class="am-radius-xl am-btn am-btn-success am-btn-block am-margin-bottom-sm" value="立即新增" />
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
				<a id="godesktop"  class="am-footer-desktop" href="http://account.keminl.cn/">电脑版</a>
			</div>
			<div class="am-footer-miscs">

				<p>由 <a href="http://account.keminl.cn/" title="账单管理系统" target="_blank" class="">账单管理系统</a> 提供技术支持
				</p>
				<p>CopyRight©2016 AllMobilize Inc.</p>
				<p>Keminl.cn 账单管理系统</p>
			</div>
		</footer>
        <input type="hidden" id="hiduser" runat="server" />
        <input type="hidden" id="hidaction" runat="server" />
        <input type="hidden" id="hidid" runat="server" />
	</body>
    
</html>
