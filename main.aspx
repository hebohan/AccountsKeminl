<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Accounts.Web.main" %>

<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="share/css/List.css?v=0.7" />
    <script type="text/javascript" src="share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="share/js/List.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
    <script type="text/javascript" language="javascript" src="js/Finix.js"></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jsapi.js"></script>
    <script type="text/javascript" src="js/zh_CN.js"></script>
    <script type="text/javascript" src="js/jquery.gvChart-1.0.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.ba-resize.min.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <link href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>


    <script type="text/javascript">
        gvChartInit();

        jQuery(document).ready(function () {
        });

        function link(url, subtitle) {
            window.parent.AddTab(url, subtitle);
        }


        function hidden() {
            document.getElementById("div").style.display = "none";
        }

        function dcgztb_show() {
            document.getElementById("dcgztb").style.display = "block";
            document.getElementById("dcjgg").style.display = "none";
        }

        function JsPrint(msg, type, id) {
            if (type == "success_update") {
                $.dialog.tips(msg, 1, 'success.gif', function () { });
                setTimeout('refresh()', 500);
            }
            else if (type == "fail_update") {
                $.dialog.tips(msg, 1, 'error.gif', function () { });
            }
            else {
                $.dialog.alert(msg);
            }
        }
        function refresh() {
            window.location.href = "main.aspx";
        }

        function UpCheck() {
            var oldpassword = $('#oldpassword').val();
            var newpass1 = $('#newpass1').val();
            var newpass2 = $('#newpass2').val();
            if (oldpassword == '' || newpass1=='' || newpass2=='') {
                $.messager.alert('系统提示', '请不要输入空信息', 'error');
                return false;
            }
            else if (newpass1 != newpass2) {
                $.messager.alert('系统提示', '两次输入的密码不一致', 'error');
                return false;
            }
            else{
                $.post('ashx/UsersHandler.ashx?type=user_pass&oldpassword=' + encodeURI(oldpassword) + '&newpass=' + encodeURI(newpass1), function (msg) {
                    if (msg == 'false') {
                        $.messager.alert('系统提示', '原密码错误，请重新输入！', 'error');
                        $('#txtLogin').val('');
                    } else {
                        $.messager.alert('系统提示', '密码修改成功！', 'info');
                        $('#updialog').dialog('close');
                    }
                });
            }
        }
    </script>
    <script>
        $(function () {
            $("#impdialog").dialog('close');
            $("#updialog").dialog('close');
            $('#btnImpCancel').bind('click', function () {
                $("#impdialog").dialog('close');
            });
            $('#psbtnCancel').bind('click', function () {
                $("#updialog").dialog('close');
            });
        });
        function OpenImportPic() {
            $("#impdialog").dialog('open');
            $('#impdialog').parent().appendTo($("form:first"));
        }
        function OpenImportPass() {
            $('#newpass1').val('');
            $('#newpass2').val('');
            $("#updialog").dialog('open');
            $('#updialog').parent().appendTo($("form:first"));
            $('#oldpassword').val('');
        }

        function ChangeMoney() {
            $('#confirm_b').show();
            $('#cancle_b').show();
            $('#money_dis').hide();
            $('#hidmoney_text').val($('#money_dis').html());
            $('#hidmoney_text').show();
            //$('#').hide();
        }

        function cancle_bb() {
            $('#confirm_b').hide();
            $('#cancle_b').hide();
            $('#hidmoney_text').hide();
            $('#money_dis').show();
        }

        function update_cash() {
            var cash = $('#hidmoney_text').val();
            if ($.trim(cash) == '' || isNaN(cash)) {
                $.messager.alert('系统提示', '请输入正确的金额！', 'error');
                return;
            }
            var his_cash = $('#money_dis').html();
            var d_value = (parseFloat(cash) - parseFloat(his_cash)).toFixed(2);
            var userid = $('#hiduserid').val();
            $.ajax({
                type: "post",
                url: "/tools/do_ajax.ashx?action=updatecash",
                data: { "userid": userid, "d_value": d_value },
                dataType: "text",
                success: function (data) {
                    if (data == "true") {
                        $.messager.alert('系统提示', '资产更正成功！', 'info');
                        $('#confirm_b').hide();
                        $('#cancle_b').hide();
                        $('#money_dis').html(parseFloat(cash).toFixed(2));
                        $('#money_dis').show();
                        $('#histotal').html((parseFloat($('#histotal').html()) + parseFloat(d_value)).toFixed(2));
                        $('#hidmoney_text').hide();
                        //window.location.reload();
                    }
                    else {
                        $.messager.alert('系统提示', '更正失败，请稍候再试！', 'error');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.messager.alert('系统提示', '更正失败，请联系管理员！', 'error');
                },
                complete: function (XMLHttpRequest, textStatus) {
                    this; // 调用本次AJAX请求时传递的options参数
                }
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
    </style>
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
<body>
    <form runat="server">
        <div class="mainbox">
            <div id="dcjgg" class="infoleft" style="display: block;">

                <div class="listtitle">
                    <a href="javascript:void(0)" onclick="link('/main.aspx','系统首页')" class="more1">刷新</a><div style="float: left;">用户中心</div>
                </div>
                <ul class="newlist">
                    <li >
                            <div class="img-box" style="float:left;">
                                <img src="head_image/<%=headpic == null || headpic == ""? "default.gif":headpic%>" style="width:100px;height:100px" />
                            </div>
                            <div>
                            <h3>尊敬的用户 <%=username %>，欢迎您！</h3>
                            <p>
                                <a href="javascript:void(0)" onclick="ChangeMoney()">资产变更</a>
                                &nbsp;<a href="javascript:void(0)" onclick="OpenImportPic()">设置头像</a>
                                &nbsp;<a href="javascript:void(0)" onclick="OpenImportPass()">修改密码</a>
                            </p>
                            </div>
                        <div>
                            总资产：<font style="font-weight:bold;color:orange;" id="histotal"><%=totalmoney %></font>元
                        </div>
                        <div>
                            现金：<font style="font-weight:bold;color:orange;" id="money_dis"><%=cash %></font><input type="text" id="hidmoney_text" runat="server" style="display:none;width:70px;font-weight:bold;color:orange;"/>元
                            &nbsp;<input type="button" id="confirm_b" runat="server" onclick="update_cash();" value="确定" class="button gray larrow" style="display:none;"/>
                        <input type="button" id="cancle_b" runat="server" onclick="cancle_bb();" value="取消" class="button gray larrow" style="display:none;"/>
                        </div>
                    </li>
                </ul>

            </div>
            <div id="impdialog" class="easyui-dialog" title="上传头像" style="width: 400px; height: 150px;" data-options="resizable:true,modal:true,buttons:'#upbtn'" >
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right">选择图片:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <asp:FileUpload ID="picfile" CssClass="" runat="server"/><font color="#FF0000">*</font>
                    </td>
                </tr>
            </table>
            </div>
            <div id="upbtn">
                <a id="btnConfirm" href="#"  class="easyui-linkbutton" runat="server" OnServerClick="btnImport_OnServerClick">上传</a>
                <a id="btnImpCancel" href="#" class="easyui-linkbutton">取消</a>
            </div>
            <!--修改密码-->
            <div id="updialog" class="easyui-dialog" title="修改密码" style="width: 250px; height: 180px;" data-options="buttons:'#uppass'" >
            <table cellpadding="0" cellspacing="1px" border="0" style="width: 100%;" bgcolor="b5d6e6">
                <tr style="background-color: White; height: 32px;">
                    <td align="right">原密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <asp:TextBox ID="oldpassword" Text="" runat="server" TextMode="SingleLine" onfocus="this.type='password'"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: White; height: 32px;">
                    <td align="right">新密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <asp:TextBox ID="newpass1" Text="" runat="server" TextMode="SingleLine" onfocus="this.type='password'"></asp:TextBox>
                    </td>
                </tr>
                   <tr style="background-color: White; height: 32px;"> 
                    <td align="right">重复密码:&nbsp;&nbsp;
                    </td>
                    <td style="padding: 5px">
                         <asp:TextBox ID="newpass2" Text="" runat="server" TextMode="SingleLine" onfocus="this.type='password'"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
            <div id="uppass" style="text-align:center">
                <a id="psbtnConfirm" href="#"  class="easyui-linkbutton" runat="server" onclick="UpCheck()">修改</a>
                <a id="psbtnCancel" href="#" class="easyui-linkbutton">取消</a>
            </div>

            <div id="gztx" class="inforight">
                <div class="listtitle">
                    <a href="javascript:void(0)" onclick="link('/AccountsManage/AccountsList.aspx','账单')" class="more1">更多</a><div style="float: left;">账单提示</div>
                </div>
                <ul class="newlist">
                    <li>有【<a href="AccountsManage/AccountsList.aspx?status=dq"><font color="red"><%=dq_count %></font></a>】项账单即将到期</li>
                    <li>有【<a href="AccountsManage/AccountsList.aspx?status=wait_receive"><font color="red"><%=waitg_count %></font></a>】项账单待收款</li>
                    <li>有【<a href="AccountsManage/AccountsList.aspx?status=wait_repay"><font color="red"><%=waitr_count %></font></a>】项账单待还款</li>
                    <li>有【<a href="AccountsManage/AccountsList.aspx?status=late_pay"><font color="red"><%=late_count %></font></a>】项账单延期中</li>
                    <li>有【<a href="AccountsManage/SummaryDisplayList.aspx"><font color="red"><%=finish_count %></font></a>】项账单已完成</li>
                </ul>
            </div>


            <div style="margin: 10px; clear: both;">
                
                <div class="listtitle" style="font-size:10px">
                    账单预警&nbsp;&nbsp;&nbsp;(<img src="../Images/green.gif" style="width: 15px; height: 15px; vertical-align: middle;" />表示7-30天账单，<img src="../Images/yellow.gif" style="width: 15px; height: 15px; vertical-align: middle;" />表示7天内账单，<img src="../Images/red.gif" style="width: 15px; height: 15px; vertical-align: middle;" />表示超期账单，)<a href="javascript:void(0)" onclick="link('/AccountsManage/MyAccountsList.aspx','账单预警')" class="more1">更多</a><div style="float: left;"></div>
                    </div>
                <!------------------- 在这里添加自定义的WebGridView ------------------->
                <cc1:WebGridView CssClass="ListTable hei12" ID="ListData" runat="server" AutoGenerateColumns="False"
                    RowHeaderColumn="Action" fixedheader="False" CountFormat="显示条数 从第<b>{0}</b>条到第 <b>{1}</b>条 总条数: <b>{2}</b>"
                    HorizontalAlign="Center" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="预警预警">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="8%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <img src="../Images/<%# int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString()) <= 7 ? (int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString()) >= 0 ? "yellow" :"red"):"green"%>.gif" style="width: 20px; height: 20px; vertical-align: middle;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="5%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="xuhao" runat="server" Text="<%#ListData.Rows.Count +1%>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                            <ItemStyle Wrap="False" Width="20%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <a href='AccountsManage/AccountDetail.aspx?id=<%# DataBinder.Eval(Container,"DataItem.id")%>&tempid=<%# DataBinder.Eval(Container,"DataItem.TempId")%>'><%# DataBinder.Eval(Container,"DataItem.zd_name")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="待收金额">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <font color="orange" style="font-weight:bold;"><%#DataBinder.Eval(Container, "DataItem.ds_money").ToString()%>元</font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资人">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container, "DataItem.tz_people").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="到期时间">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy-MM-dd}",DataBinder.Eval(Container, "DataItem.dq_time")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString()) >= 0 ? DataBinder.Eval(Container, "DataItem.dq_day").ToString()+"天后"+(DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_receive" ? "收款": (DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_repay" ? "还款": (DataBinder.Eval(Container,"DataItem.status").ToString() == "wait_deposit" ? "存款":"到期") )) : "已超期"+ System.Math.Abs(int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString())) +"天"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <MouseOverRow MouseOverColor="" Support="False"></MouseOverRow>
                    <HeaderStyle CssClass="list_grid_title"></HeaderStyle>
                    <AlternatingRowStyle CssClass="list_grid_alternate_row" HorizontalAlign="Center"></AlternatingRowStyle>
                    <CustomColumns>
                        <NumberColumn Visible="False" Width="0" Align="Left" Position="0" Priority="0"></NumberColumn>
                        <MultiChooseColumn Visible="False" Width="0" Align="Left" Position="0" Priority="0"></MultiChooseColumn>
                    </CustomColumns>
                    <RowStyle CssClass="list_grid_row" HorizontalAlign="Center"></RowStyle>
                </cc1:WebGridView>
                <!-------------------------------END----------------------------------->
                <table class="fenye" width="100%">
                    <tr>
                        <td width="90%" align="right">
                            <cc2:WebPager ID="WebPager1" runat="server" NumericButtonCount="3" class="meneame" PageSize="5" OnPageChanged="WebPager1_PageChanged">
                            </cc2:WebPager>
                        </td>
                        <td width="10%"><span id="RecordCount" class="meneame" runat="server"></span>
                        </td>
                    </tr>
                </table>

            </div>
            </div>
    </form>
    <script type="text/javascript">
        setWidth();
        $(window).resize(function () {
            setWidth();
        });
        function setWidth() {
            var width = ($('.mainbox').width() - 40) / 2;
            $('.infoleft,.inforight').width(width);
        }
    </script>
    <input type="hidden" id="hiduserid" runat="server" />
</body>
</html>
