<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetDateValue.aspx.cs" Inherits="Inspur.Finix.Portal.js.calendar.MonitorCalendar.SetDateValue" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>设置时间范围</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Framework.css" type="text/css" rel="stylesheet">
    <link href="Page.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="Popcalendar.js"></script>

    <style>
        .TSelect
        {
            background: #edf6fd;
            cursor: hand;
            font-weight: bold;
            text-align: center;
            width: 60;
            border-left: 1px solid blue;
            border-right: 1px solid blue;
            border-top: 1px solid blue;
            border-bottom: none;
        }
        .TNormal
        {
            cursor: hand;
            text-align: center;
            width: 60;
            border-bottom: 1px solid blue;
            border-right: none;
        }
        .TOver
        {
            cursor: hand;
            text-align: center;
            width: 60;
            background: #edf6fd;
            border-bottom: 1px solid blue;
        }
        .TOut
        {
        }
        .TabCss
        {
            border-left: 1px solid blue;
            border-bottom: 1px solid blue;
            border-right: 1px solid blue;
        }
    </style>
    <base target="_self">

    <script language="javascript">
        function TabChange(Number) {
            for (i = 0; i < 5; i++) {
                document.all("Tab" + i).className = "TNormal";
                document.all("aaa" + i).style.display = "none";
            }
            document.all("Tab" + Number).className = "TSelect";
            document.all("aaa" + Number).style.display = "block";

        }

        function TabOver(Number) {
            if (document.all("Tab" + Number).className != "TSelect")
                document.all("Tab" + Number).className = "TOver";
        }

        function TabOut(Number) {
            if (document.all("Tab" + Number).className != "TSelect")
                document.all("Tab" + Number).className = "TNormal";
        }

        function WinReturn() {
            var rtnValue = "";
            if (aaa0.style.display == "block") {
                if (document.all.item("realizing").value == "") {
                    alert("请选择日期！");
                    return;
                }

                if (vbCheckDate(document.all.item("realizing").value) != "") {
                    alert("日期输入有错误！");
                    return;
                }

                window.returnValue = document.all.item("realizing").value;
                window.close();
            }
            else if (aaa1.style.display == "block") {
                if (document.all.item("datebegin").value == "") {
                    alert("请选择开始日期！");
                    return;
                }

                if (document.all.item("dateend").value == "") {
                    alert("请选择结尾日期！");
                    return;
                }

                if (vbCheckDate(document.all.item("datebegin").value) != "") {
                    alert("开始日期输入有错误！");
                    return;
                }

                if (vbCheckDate(document.all.item("dateend").value) != "") {
                    alert("结尾日期输入有错误！");
                    return;
                }

                window.returnValue = document.all.item("datebegin").value + "~" + document.all.item("dateend").value;
                window.close();
            }
            else if (aaa2.style.display == "block") {
                if (document.all.item("monthsyear").value == "") {
                    alert("请输入年份！");
                    return;
                }

                if (vbCheckDate(document.all.item("monthsyear").value + "-01-01") != "") {
                    alert("年份输入有错误！");
                    return;
                }

                window.returnValue = document.all.item("monthsyear").value + "-" + document.all.item("month").value;
                window.close();
            }
            else if (aaa3.style.display == "block") {
                if (document.all.item("currentyear").value == "") {
                    alert("请输入年份！");
                    return;
                }

                if (vbCheckDate(document.all.item("currentyear").value + "-01-01") != "") {
                    alert("年份输入有错误！");
                    return;
                }
                window.returnValue = document.all.item("currentyear").value;
                window.close();
            }
            else if (aaa4.style.display == "block") {
                window.returnValue = "";
                window.close();
            }
        }
    </script>

    <script language="vbscript">
function vbCheckDate(strDate)
	if IsDate(strDate) = false then
		vbCheckDate = "时间格式错误"
		exit function
	end if
	
	vbCheckDate = ""
end function
    </script>

</head>
<body ms_positioning="GridLayout" onload="javascript:this.focus();">
    <form id="Form1" method="post" runat="server">
    <table class="tableall" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <!--标题及操作工具栏部分（开始）-->
                <table class="PageTitleTable">
                    <tr>
                        <td class="PageTitleTdImg">
                            <img src="PageTitle.gif" />
                        </td>
                        <td class="PageTitleTdTxt">
                            设置查询日期
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdfieldset">
                <table class="PageCommandTable" style="border: none">
                    <tr>
                        <td class="tdbuttonleft">
                        </td>
                        <td class="tdbutton">
                            <input class="normal-button" id="btnConfirm" onclick="javascript:WinReturn();" type="button"
                                value="确定" name="btnModify" runat="server">
                        </td>
                        <td class="tdbutton">
                            <input class="normal-button" id="btnCancel" onclick="javasctipt:window.close();"
                                type="button" value="取消" name="btnSubmit" runat="server">
                        </td>
                    </tr>
                </table>
                <!--标题及操作工具栏部分（结束）-->
            </td>
        </tr>
        <tr>
            <td class="tdcontent">
                <div class="content">
                    <table class="tablecontent" style="border: none" cellspacing="0">
                        <tr>
                            <td align="left">
                                <table id="table5" cellpadding="5" height="25" width="100%" bgcolor="#e4e4e4" style="border-collapse: collapse">
                                    <tr>
                                        <td id="Tab0" class="TSelect">
                                            <a onclick="javascript:TabChange(0);" onmouseover="javascript:TabOver(0);" onmouseout="javascript:TabOut(0);">
                                                具体日期</a>
                                        </td>
                                        <td id="Tab1" class="TNormal">
                                            <a onclick="javascript:TabChange(1);" onmouseover="javascript:TabOver(1);" onmouseout="javascript:TabOut(1);">
                                                按时间段</a>
                                        </td>
                                        <td id="Tab2" class="TNormal">
                                            <a onclick="javascript:TabChange(2);" onmouseover="javascript:TabOver(2);" onmouseout="javascript:TabOut(2);">
                                                按月份</a>
                                        </td>
                                        <td id="Tab3" class="TNormal">
                                            <a onclick="javascript:TabChange(3);" onmouseover="javascript:TabOver(3);" onmouseout="javascript:TabOut(3);">
                                                按年份</a>
                                        </td>
                                        <td id="Tab4" class="TNormal">
                                            <a onclick="javascript:TabChange(4);" onmouseover="javascript:TabOver(4);" onmouseout="javascript:TabOut(4);">
                                                (不限)</a>
                                        </td>
                                        <td width="100" style="border-bottom: 1px solid blue">
                                        </td>
                                    </tr>
                                </table>
                                <div id="aaa0" style="display: block">
                                    <table cellspacing="0" cellpadding="0" width="100%" class="TabCss" bgcolor="#edf6fd"
                                        border="0">
                                        <tr>
                                            <td colspan="2" height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="20%">
                                                选择日期：
                                            </td>
                                            <td>
                                                <input id="realizing" type="text" size="10">

                                                <script language="javascript">                                                    arrowtag("date1", 20, "", "", 0, "realizing");</script>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" height="135" align="center">
                                                <font color="red">您可以选择一个具体的日期作为查询的条件。</font>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="aaa1" style="display: none">
                                    <table cellspacing="0" cellpadding="0" width="100%" class="TabCss" bgcolor="#edf6fd"
                                        border="0">
                                        <tr>
                                            <td colspan="4" height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="center">
                                                <input id="datebegin" type="text" size="10">

                                                <script language="javascript">                                                    arrowtag("date2", 20, "", "", 0, "datebegin");</script>

                                            </td>
                                            <td align="center" width="5%">
                                                至
                                            </td>
                                            <td width="30%" align="center">
                                                <input id="dateend" type="text" size="10">

                                                <script language="javascript">                                                    arrowtag("date3", 20, "", "", 0, "dateend");</script>

                                            </td>
                                            <td width="35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" height="135" align="center">
                                                <font color="red">您可以选择一段时间作为查询条件。</font>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="aaa2" style="display: none">
                                    <table cellspacing="0" cellpadding="0" width="100%" class="TabCss" bgcolor="#edf6fd"
                                        border="0">
                                        <tr>
                                            <td height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <input id="monthsyear" type="text" maxlength="4" size="4" runat="server" name="monthsyear">年
                                                <select id="month" size="1" runat="server" name="month">
                                                    <option value="01" selected>1月</option>
                                                    <option value="02">2月</option>
                                                    <option value="03">3月</option>
                                                    <option value="04">4月</option>
                                                    <option value="05">5月</option>
                                                    <option value="06">6月</option>
                                                    <option value="07">7月</option>
                                                    <option value="08">8月</option>
                                                    <option value="09">9月</option>
                                                    <option value="10">10月</option>
                                                    <option value="11">11月</option>
                                                    <option value="12">12月</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="135" align="center">
                                                <font color="red">您可以选择某个具体月份作为查询条件。</font>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="aaa3" style="display: none">
                                    <table cellspacing="0" cellpadding="0" class="TabCss" width="100%" bgcolor="#edf6fd"
                                        border="0">
                                        <tr>
                                            <td height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                输入年份：<input type="text" id="currentyear" size="4" maxlength="4" runat="server" name="currentyear">年
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="135" align="center">
                                                <font color="red">您可以查询某一具体年份的数据。</font>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="aaa4" style="display: none">
                                    <table cellspacing="0" cellpadding="0" class="TabCss" width="100%" bgcolor="#edf6fd"
                                        border="0">
                                        <tr>
                                            <td height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <input type="text" style="visibility: hidden">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="135" align="center">
                                                <font color="red">清空原来数值，不限制日期。</font>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
