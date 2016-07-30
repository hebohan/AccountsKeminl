<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountTemplateDetail.aspx.cs" Inherits="Accounts.Web.SysSetting.AccountTemplateDetail" ValidateRequest="false" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账单分类编辑</title>
    <link type="text/css" rel="stylesheet" href="../share/css/NK1.1.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/List.css?v=0.7" />
    <link type="text/css" rel="stylesheet" href="../share/css/validate.css?v1.0" />
    <script language="javascript" type="text/javascript" src="../share/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../share/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../share/js/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../share/js/layout.js"></script>
    <script language="javascript" type="text/javascript" src="../share/js/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
        function backToList() {
            location.href = "AccountTemplateList.aspx";
        }
        function JsPrint(msg, type) {
            if (type == "success") {
                $.dialog.tips(msg, 1, 'success.gif', backToList);

            } else {
                $.dialog.alert(msg);
            }
        }
    </script>
    <style>
        .ListSearch {
            padding: 20px 15px;
            font-size: 12px;
            color: #666;
            box-sizing: border-box;
            overflow: hidden;
            min-height: 400px;
            width: 80%;
        }

        .ListSearch tr td {
            padding: 3px 0;
        }
                .ListSearchInput {
                    display: block;
                    float: left;
                    width: 35%;
                    text-align: right;
                    color: #6d7e86;
                    font-weight: bold;
                }

                .inputtdline {
                    position: relative;
                    width: 63%;
                    float: right;
                }

        .input.small {
            width: 50px;
        }

        .input.normal {
            width: 300px;
        }

        .input {
            padding: 5px 4px;
            line-height: 20px;
            border: 1px solid #eee;
            background: #fff;
            vertical-align: middle;
            color: #333;
            font-size: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            float: left;
        }
        #cblField label{ margin-right: 10px;}

        #MainCont {
            width: 100%;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="MainCont">
            <table class="ListTitle">
                <tr>
                    <td class="ListTitleStr hei12b">
                        <img src="../share/images/ListTitleBit.png" class="midImg" />账单模板编辑<a id="lbTile" runat="server"></a>
                    </td>
                    <td class="ListTitleBtn">
                        <!--------------- 在这里添加自定义的操作 ------------------->
                        <asp:Button ID="btnSubmit" runat="server" Text="保存" OnClick="btnSubmit_Click" class="btnSave" />
                        <input type="button" onclick="backToList()" value="返回" class="btnBack" />
                        <!-------------------------END------------------------------>
                    </td>
                </tr>
            </table>
            <table class="ListSearch hei12">
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">账单模板名称：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">排序数字：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                        <span class="Validform_checktip">*数字，越小越向前</span>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">说明：
                    </td>
                    <td class="inputtdline">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="input normal" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ListSearchInput" style="text-align: right;">表单字段：
                    </td>
                    <td class="inputtdline">
                        <asp:CheckBoxList ID="cblField" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
