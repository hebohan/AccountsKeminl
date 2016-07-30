7<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsList.aspx.cs" Inherits="Accounts.Web.AccountsManage.AccountsList" %>

<%@ Import Namespace="Accounts.BLL" %>
<%@ Import Namespace="Accounts.BLL.Common" %>

<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebPager"
    TagPrefix="cc2" %>
<%@ Register Assembly="Inspur.Finix.WebFramework.Controls4AspNet" Namespace="Inspur.Finix.WebFramework.Controls4AspNet.WebGrid"
    TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>账单管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../share/js/List.js"></script>
    <script src="../CSS/layer/layer.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
    <script type="text/javascript" language="javascript" src="../js/Finix.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/amazeui.min.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <link href="../JS/jquery-easyui-1.3.4/themes/metro/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-easyui-1.3.4/jquery.easyui.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function OpenNewForm() {
            window.open("DictionaryDetail.aspx", '_blank', "width=1000,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }
        function backToList() {
            location.href = "../main.aspx";
            //history.go(-1);
        }
        //打开详细页面
        function OpenNewEdit() {
            if (hasChecked(document.all("ListData")) && isCheckOne(document.all("ListData"))) {
                window.open("DictionaryDetail.aspx?dictionaryid=" + getCheckVal("chkbox").split('|')[0], '_blank', "width=1000,height=700,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
            }
            else {
                $.dialog.alert("请选择一项进行修改！");
            }
        }
        function showAddDialog(id) {
            window.open("AccountsScheduleDetail.aspx?id=" + id, '_blank', "width=600,height=400,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
        }

        function DelCheck() {
            if (hasChecked(document.all("ListData"))) {
                var hdelid = getCheckVal("chkbox");

                $.dialog.confirm("请确定是否删除该条记录?", function () {
                    $("#hdelid").val(hdelid);
                    var btn = document.getElementById('<%=hidBtnDel.ClientID%>');
                    btn.click();
                }, function () {

                });
            }
            else {
                $.dialog.alert("请至少选择一个需要删除的记录！");
            }
        }

        function FinishCheck() {
            if (hasChecked(document.all("ListData"))) {
                var hidfid = getCheckVal("chkbox");
                $.dialog.confirm("请确定是否认定选定的记录为完成?", function () {
                    $("#hidfid").val(hidfid);
                    var btn = document.getElementById('<%=HidbtnFinish.ClientID%>');
                    btn.click();
                }, function () {

                });
            }
            else {
                $.dialog.alert("请至少选择一个需要认定为已完成的账单！");
            }
        }

        function CountCheck() {
            if (hasChecked(document.all("ListData"))) {
                var hidfid = getCheckVal("chkbox");
                var userid = $('#hiduserid').val();
                //var openUrl = "AccountsCount.aspx?id=" + hidfid;//弹出窗口的url
                //var iWidth = 300; //弹出窗口的宽度;
                //var iHeight = 300; //弹出窗口的高度;
                //var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
                //var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
                //window.open(openUrl, "", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft);
                $.ajax({
                    type: "post",
                    url: "/tools/do_ajax.ashx?action=ac_count",
                    data: { "userid": userid, "hidfid": hidfid },
                    dataType: "text",
                    success: function (data) {
                        if (data != "false") {
                            $.messager.alert('统计结果(共<font color=\'green\'>' + hidfid.split(',').length + '</font>条记录)', '<br>' + data, 'info');
                        }
                        else {
                            $.messager.alert('系统提示', '统计失败，请稍后再试！', 'error');
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.messager.alert('系统提示', '统计失败，请联系管理员！', 'error');
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });


            }
            else {
                $.dialog.alert("请至少选择一项！");
            }
        }

        function JsPrint(msg, type, id) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', function () { });

            } else if(type == "success_update")
            {
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
            window.location.href = "AccountsList.aspx";
        }
    </script>
    <script>
        $(function () {

            $('#confirm_b').hide();
            $('#cancle_b').hide();
            $('updatemoney').show();
            $('#total').attr("readonly", "readonly");
            $('#cash').attr("readonly", "readonly");

            $('#updatemoney').click(function () {
                $('#confirm_b').show();
                $('#cancle_b').show();
                $('#updatemoney').hide();
                //$('#total').removeAttr("readonly")
                $('#cash').removeAttr("readonly")
            });

            //当checkboxY有值时
            //$("input[type='checkbox']").click(function () {
            //    if ($("input[type='checkbox']:checked").length > 0) {
            //        var hidfid = getCheckVal("chkbox");
            //        var array = hidfid.split(",");
            //        var nums = [];
            //        var flag = 0;
            //        if (hidfid != "") {
            //            for (var i = 0 ; i < array.length ; i++) {
            //                array[i];
            //            }

            //        }
            //        $('#CountArea').show();
            //    } else {
            //        $('#CountArea').hide();
            //    }
            //})

            $('#confirm_b').click(function () {
                var cash = $('#cash').val();
                var his_cash = $('#money_his').val();
                if ($.trim(cash) == '' || isNaN(cash)) {
                    layer.open({
                        content: '请输入正确的金额!',
                        btn: ['OK']
                    });
                    return;
                }
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
                            $('#updatemoney').show();
                            $('#money_his').val(parseFloat(cash).toFixed(2));
                            $('#cash').val(parseFloat(cash).toFixed(2));
                            $('#total').val((parseFloat($('#total').val()) + parseFloat(d_value)).toFixed(2));
                            $('#hidmoney_text').hide();
                            $('#cash').attr("readonly", "readonly");
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
            });

            $('#cancle_b').click(function () {
                $('#confirm_b').hide();
                $('#cancle_b').hide();
                $('#updatemoney').show();
                $('#total').attr("readonly", "readonly");
                $('#cash').attr("readonly", "readonly");
            });
        });
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
<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b" style="width:80px">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />账单列表<a id="A1" runat="server"></a>
                    </td>
                    <td class="hei12b">
                        (此处显示账单为本年内未完成账单，历史账单请点击<a href="SummaryDisplayList.aspx"><font color="red">综合查询</font></a>)
                    </td>
                    <%--<td>
                        <span style="font-size:xx-small;background-color:white" id="CountArea"></span> 
                    </td>--%>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" onclick="backToList()" value="返回" class="btnBack" />
                        <input type="button" name="btnFinish" value="批量完成" class="btnSave" id="btnFinish" runat="server" onclick="FinishCheck()" />
                        <asp:Button ID="HidbtnFinish" runat="server" OnClick="HidbtnFinish_Click" Text="批量完成" Style="display: none;" />

                        <input type="button" name="btnCount" value="统计" class="btnGraph" id="btnCount" runat="server" onclick="CountCheck()" />
                        <%--<asp:Button ID="HidbtnCount" runat="server" OnClick="HidbtnCount_Click" Text="统计" Style="display: none;" />--%>

                        <input type="button" name="btnDel" value="删除" class="btnDel" id="btnDel" runat="server" visible="False" onclick="DelCheck()" />
                        <asp:Button ID="hidBtnDel" runat="server" OnClick="btnDel_Click" Text="删除" CssClass="btnDel" Visible="False" Style="display: none;" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput stitle" style="width:60px">账单类型：</td>
                    <td class="ListSearchInput scontent" style="width:100px">
                        <asp:DropDownList ID="ddlTemp" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px">关键字：</td>
                    <td class="ListSearchInput scontent" style="width:100px">
                        <input type="text" id="work" runat="server" class="detail_edit stext" />
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px">投资人：</td>
                    <td class="ListSearchInput scontent" style="width:100px">
                        <asp:DropDownList ID="tz_people" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px">状态：</td>
                    <td class="ListSearchInput scontent">
                        <asp:DropDownList ID="Status" runat="server" CssClass="stext">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td style="width:100px">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <input type="button" name="btnSearch" value="查询" class="btnSearch" id="btnSearch" runat="server" onserverclick="btnSearch_OnServerClick" />
                        
                        <!-------------------------END------------------------------>
                    </td>

                    <td class="ListSearchInput stitle" style="width:50px;text-align:center">总资产：</td>
                    <td class="ListSearchInput scontent" style="width:60px">
<%--                        <asp:TextBox ID="total_money_text" runat="server" Text="10000.00" CssClass="Textbox" ReadOnly="true"></asp:TextBox>--%>
                        <input type="text" readonly="readonly" id="total" runat="server" text="" class="Textbox"/>
                    </td>
                    <td class="ListSearchInput stitle" style="width:50px;text-align:center"">现金：</td>
                    <td class="ListSearchInput scontent" style="width:60px">
                        <%--<asp:TextBox ID="cash_money" runat="server" Text="10000.00" CssClass="Textbox" ReadOnly="true"></asp:TextBox>--%>
                        <input type="hidden" id="money_his" runat="server" />
                        <input type="text" readonly="readonly" id="cash" runat="server" text="" class="Textbox"/>
                    </td>
                    <td>
                        <input type="button" id="updatemoney" runat="server" onclick="" value="更正" class="button gray larrow"/>
                        <input type="button" id="confirm_b" runat="server" onclick="" value="确定" class="button gray larrow"/>
                        <input type="button" id="cancle_b" runat="server" onclick="" value="取消" class="button gray larrow"/>
                    </td>
                    
                    

                </tr>
            </table>
            <div style="padding: 0 10px;">
                <!------------------- 在这里添加自定义的WebGridView ------------------->
                <cc1:WebGridView CssClass="ListTable hei12" ID="ListData" runat="server" AutoGenerateColumns="False"
                    RowHeaderColumn="Action" fixedheader="False" CountFormat="显示条数 从第<b>{0}</b>条到第 <b>{1}</b>条 总条数: <b>{2}</b>"
                    HorizontalAlign="Center" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p onclick="checkClick(ListData);" style="cursor: hand">全选</p>
                            </HeaderTemplate>
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemTemplate>
                                <input type="checkbox" id="chkbox" name="chkbox" value="<%# DataBinder.Eval(Container, "DataItem.Id") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="4%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="4%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%--<%# DataBinder.Eval(Container, "DataItem.zd_xh") %>--%>
                                <asp:Label ID="xuhao" runat="server" Text="<%# WebPager1.PageSize * (WebPager1.CurrentPageIndex-1) + ListData.Rows.Count +1%>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称">
                            <HeaderStyle Width="14%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="14%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <%#"<a href='AccountDetail.aspx?id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"'>" + DataBinder.Eval(Container, "DataItem.zd_name") +"</a>"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资金额">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="8%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <font color="orange" style="font-weight:bold;"><%# DataBinder.Eval(Container, "DataItem.tz_money").ToString()+"元" %> </font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目详情">
                            <HeaderStyle Wrap="False" Width="20%" HorizontalAlign="Center" />
                            <ItemStyle Wrap="False" Width="20%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.zd_detail")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资天数">
                            <HeaderStyle Width="4%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="4%" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <font color="lightblue" style="font-weight:bold;"><%# DataBinder.Eval(Container, "DataItem.tz_day").ToString()+"天" %> </font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投资人">
                            <HeaderStyle Width="6%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="6%" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.tz_people").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="待收金额">
                            <HeaderStyle Width="8%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="8%" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <font color="orange" style="font-weight:bold;"><%# DataBinder.Eval(Container, "DataItem.ds_money").ToString()+"元" %> </font>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="到期时间">
                            <HeaderStyle Width="7%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="7%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy-MM-dd}",DataBinder.Eval(Container, "DataItem.dq_time")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收支情况">
                            <HeaderStyle Width="7%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="7%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_repay" ? (Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.tz_money").ToString())) - Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ds_money").ToString())) >= 0 ? ("<font color='green' >+" + Math.Abs(Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ds_money").ToString())) - Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.tz_money").ToString()))) + "元</font>") : ("<font color='red' >-" + Math.Abs(Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ds_money").ToString())) - Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.tz_money").ToString()))) + "元</font>")):(Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ds_money").ToString())) - Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.tz_money").ToString())) >= 0 ? ("<font color='green' >+" + Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ds_money").ToString()) - Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.tz_money").ToString()))) + "元</font>") : ("<font color='red' >-" + Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ds_money").ToString()) - Math.Abs(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.tz_money").ToString()))) + "元</font>"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <HeaderStyle Width="10%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%--<%# new Accounts.BLL.BasePage().GetDicName(DataBinder.Eval(Container,"DataItem.status").ToString())%>--%>
                                <%# int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString()) >= 0 ? DataBinder.Eval(Container, "DataItem.dq_day").ToString()+"天后"+(DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_receive" ? "待收款": (DataBinder.Eval(Container, "DataItem.status").ToString() =="wait_repay" ? "待还款": (DataBinder.Eval(Container,"DataItem.status").ToString() == "wait_deposit" ? "待存款":"到期") )) : "已超期"+ System.Math.Abs(int.Parse(DataBinder.Eval(Container, "DataItem.dq_day").ToString())) +"天"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="7%" Wrap="False" />
                            <ItemStyle Wrap="False" Width="7%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.IsFinish").ToString() == "False" ? "<a href='AccountRegisterDetail.aspx?action=edit&id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"&type=1'>修改</a>" :"" %>
                                <%--<%#"<a href='AccountsScheduleAudit.aspx?id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"'>填报</a>"%>--%>
                                <%#"<a href='AccountDetail.aspx?id="+DataBinder.Eval(Container,"DataItem.id")+"&tempid="+DataBinder.Eval(Container,"DataItem.TempId")+"'>查看</a>"%>
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
            </div>
            <table class="fenye" width="100%">
                <tr>
                    <td width="70%" align="right">
                        <cc2:WebPager ID="WebPager1" runat="server" NumericButtonCount="5" class="meneame" PageSize="10" OnPageChanged="WebPager1_PageChanged">
                        </cc2:WebPager>
                    </td>
                    <td width="30%"><span id="RecordCount" class="meneame" runat="server"></span>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" runat="server" id="hdelid" />
        <input type="hidden" runat="server" id="hcash" />
        <input type="hidden" runat="server" id="htotal" />
        <input type="hidden" runat="server" id="hidkeyword" />
        <input type="hidden" runat="server" id="hiduserid" />
        <input type="hidden" runat="server" id="hidfid" />
    </form>
</body>
</html>
