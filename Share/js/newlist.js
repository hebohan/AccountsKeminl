//列表页面前台UI加载
//youqing 2011-6-24 21:22:57



//动态装载loading(2011-11-25 0:11:57 屏蔽)
document.write("<base target=\"_self\" />");
document.write(" <div id=\"loading\" class=\"bakloading\" style=\"position:absolute;z-index:9999;width:100%;height:100%;left:0px;top:0px;filter:alpha(opacity=80)\"></div>");
document.write(" <div id=\"loading1\" style=\"border:1 solid #0099CC;z-index:9999;position:absolute;top:expression((this.parentElement.offsetHeight-this.offsetHeight)/2);left:expression((this.parentElement.offsetWidth-this.offsetWidth)/2); background-color:#DDEDFB;  padding-right:5px; padding-left:5px;\"><table style=\"font-size:12px;\"><tr><td class=\"imgloading\" width=20 height=20>&nbsp;</td><td valign=middle>页面加载中，请稍等...</td></tr></table></div>");
document.onreadystatechange = function () {
    if (document.readyState == "complete" || document.readyState == "interactive") {
        if (document.getElementById("loading") != null) {
            document.getElementById("loading").style.display = "none";
        }
        if (document.getElementById("loading1") != null) {
            document.getElementById("loading1").style.display = "none";
        }


        //表格配色；TableChangeBg("表格名称","奇数行背景","偶数行背景","鼠标经过背景","选中后背景","经过时字色","经过后字色",开始变色的行号);
        TableChangeBg("ListData", "#EBF6FA", "#EBF6FA", "#EBF6FA", "#EBF6FA", "#333", "#333", 2);
        TableChangeBg("ListAdvSearch", "#FAFEFF", "#EBF6FA", "#FFFFA4", "#FC6", "#333", "#333", 1);


        addtdTitle("ListData", 1);
        addtdTitle("ListTableFlow", 2);
        addtdTitle("ListAdvSearch", 0);
        FixFlowTitle("ListTableFlow", "ListData");
        resizetHead("ListTableFlow");
        resizetHead("ListData");

        //模式窗口不支持获取焦点
        if (document.getElementById("NowPage")) {
            document.getElementById("NowPage").focus();
        }

        if (document.getElementById("MainCont")) {
            if (document.getElementById("ListTableFlow")) {
                document.getElementById("MainCont").onscroll = function () {
                    if (parseInt(document.getElementById("MainCont").scrollTop) > 95) {
                        //alert(document.getElementById("TitleCont").outerHTML);
                        document.getElementById("ListTableFlow").style.display = "block";

                    }
                    else {
                        document.getElementById("ListTableFlow").style.display = "none";
                    }
                }
            }
        }

    }
}
window.onresize = function () {
    if (document.getElementById("ListTableFlow")) {
        FixFlowTitle("ListTableFlow", "ListData");
        if (document.getElementById("MainCont")) {
            if (parseInt(document.getElementById("MainCont").scrollTop) > 95) {
                //alert(document.getElementById("TitleCont").outerHTML);
                document.getElementById("ListTableFlow").style.display = "block";

            }
            else {
                document.getElementById("ListTableFlow").style.display = "none";
            }
        }
    }
}


/********* 给列表标题增加拖动**********/
function resizetHead(o) {
    if (document.getElementById(o)) {
        var obj = document.getElementById(o).getElementsByTagName("thead");
        if (obj == null || obj.length <= 0) return;
        var t = obj[0].getElementsByTagName("td");
        for (var i = 0; i < t.length; i++) {
            t[i].onmousedown = MouseDownToResize;
            t[i].onmousemove = MouseMoveToResize;
            t[i].onmouseup = MouseUpToResize;
        }
    }
}

/********* 表格变色，鼠标感应**********/
function TableChangeBg(o, a, b, c, d, e, f, g) {
    if (document.getElementById(o)) {
        var t = document.getElementById(o).getElementsByTagName("tr");
        for (var i = g - 1; i < t.length; i++) {
            t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
            //        点击事件
            //        t[i].onclick = function() {
            //            if (this.x != "1") {
            //                this.x = "1"; //本来打算直接用背景色判断，FF获取到的背景是RGB值，不好判断
            //                this.style.backgroundColor = d;
            //            } else {
            //                this.x = "0";
            //                this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
            //            }
            //        }
            t[i].onmouseover = function () {
                if (this.x != "1") {
                    this.style.backgroundColor = c;
                    this.style.color = e;
                }
            }
            t[i].onmouseout = function () {
                if (this.x != "1") {
                    this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
                    this.style.color = f;
                }
            }
        }
    }
}


function Pager(NowPage) {
    document.getElementById("NowPage").value = NowPage;
    document.getElementById("ClickFenye").value = "1";
    document.getElementById("TrueSearch").click();
}
//模式窗口不支持获取焦点
//    if (document.getElementById("NowPage")) {
//        document.getElementById("NowPage").focus();
//    }
function PagerChange() {
    //按回车确定翻页
    if (event.keyCode == 13) {
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("TrueSearch").click();
    }
    //按左右键翻页
    if (event.keyCode == 37) {
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("NowPage").value = parseInt(document.getElementById("NowPage").value) - 1;
        document.getElementById("TrueSearch").click();
    }
    if (event.keyCode == 39) {
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("NowPage").value = parseInt(document.getElementById("NowPage").value) + 1;
        document.getElementById("TrueSearch").click();
    }

}



/********* 给td增加title**********/
function addtdTitle(tableID, ListTable) {
    if (document.getElementById(tableID)) {
        var tab = document.getElementById(tableID);
        for (var i = 0; i < tab.rows.length; i++) {
            for (var j = 0; j < tab.rows[i].cells.length; j++) {
                if (i == 0 && ListTable == 1) {
                    tab.rows[i].cells[j].title = "列名：" + tab.rows[i].cells[j].innerText;
                    //                tab.rows[i].cells[j].title = "列名：" + tab.rows[i].cells[j].innerText + "\r\n\r\n提示：按住鼠标左键左右拖动可改变本列长短";
                    //tab.rows[i].cells[j].style.cursor = "e-resize";
                }

                else if (i == 0 && ListTable == 2) {
                    tab.rows[i].cells[j].title = "提示：调整列宽度，滚动条将定位到第一条数据位置。";
                }
                else if (j > 0 && ListTable == 0) {
                    //高级搜索只显示第一列
                }
                else {
                    tab.rows[i].cells[j].title = tab.rows[i].cells[j].innerText;
                }
            }
        }
    }
}


/***********thaed拖动************/
//cursor:e-resize;

function MouseDownToResize() {
    if (this.parentElement.parentElement.parentElement.id != "ListData") {
        document.getElementById("MainCont").scrollTop = 95;

    }
    this.mouseDownX = event.clientX;
    this.pareneTdW = this.offsetWidth;
    //obj.pareneTableW=theObjTable.offsetWidth;
    this.setCapture();
}
function MouseMoveToResize() {
    if (!this.mouseDownX) return false;
    var newWidth = this.pareneTdW * 1 + event.clientX * 1 - this.mouseDownX;
    if (newWidth > 0) {
        this.style.width = newWidth;
        //theObjTable.style.width=obj.pareneTableW*1+event.clientX*1-obj.mouseDownX;
    }
}
function MouseUpToResize() {
    this.releaseCapture();
    this.mouseDownX = 0;
    //   if (this.parentElement.parentElement.parentElement.id == "ListData") {
    FixFlowTitle("ListTableFlow", "ListData");
    //    }
    //    else {
    //        alert(1);
    //        //FixFlowTitle("ListData", "ListTableFlow");
    //        document.getElementById("MainCont").scrollTop = 50;
    //    }

}
/***********高级搜索 确定************/
function AdvSearchClick() {
    document.getElementById("ClickAdvSearch").value = "1";
    //清空简单搜索输入域的值；
    var SimpleSearchAllInput = document.getElementById("SimpleSearch").getElementsByTagName("input");
    for (i = 0; i < SimpleSearchAllInput.length; i++) {
        SimpleSearchAllInput[i].value = "";
    }
    document.getElementById("TrueSearch").click();

}
/***********简单搜索域回车提交************/
function EnterToSimpSubmit(BtnID) {
    if (event.keyCode == 13 && event.srcElement.type != 'submit') {
        event.returnValue = false;
        document.getElementById(BtnID).click();
    }

}




/***********将简单搜索域值赋值到高级搜索************/
//2011-11-25 0:37:06

function SimpleSearchClick() {

    if (document.getElementById("ClickAdvSearch")) {
        document.getElementById("ClickAdvSearch").value = "0";
        var SimpleSearchAllInput = document.getElementById("SimpleSearch").getElementsByTagName("input");
        var SimpleSearchAllSelect = document.getElementById("SimpleSearch").getElementsByTagName("select");

        //清空高级搜索值； 目前仅支持：input，select赋值，后期增加其他支持
        var AdvSearchAllInput = document.getElementById("AdvSearchTableOut").getElementsByTagName("input");
        for (i = 0; i < AdvSearchAllInput.length; i++) {
            if (AdvSearchAllInput[i].IsAdvInput == "1" && !AdvSearchAllInput[i].readOnly) {
                AdvSearchAllInput[i].value = "";
            }
        }
        var AdvSearchAllSelect = document.getElementById("AdvSearchTableOut").getElementsByTagName("select");
        for (i = 10; i < AdvSearchAllSelect.length; i++) {
            if (AdvSearchAllSelect[i].IsAdvInput == "1" && AdvSearchAllSelect[i].readonly != "readonly") {
                AdvSearchAllSelect[i].value = "";
            }
        }



        for (i = 0; i < SimpleSearchAllInput.length; i++) {
            var ssInputId = SimpleSearchAllInput[i].id;
            var ssInputVal = SimpleSearchAllInput[i].value;
            var ColId = ssInputId.replace("_SimpleSearch", "");

            if (document.getElementById(ColId + "_Min") && document.getElementById(ColId + "_Max")) {
                document.getElementById(ColId + "_Min").value = ssInputVal;
                document.getElementById(ColId + "_Max").value = ssInputVal;
            }
            else if (document.getElementById(ColId)) {
                document.getElementById(ColId).value = ssInputVal;
            }
        }
        for (i = 0; i < SimpleSearchAllSelect.length; i++) {
            var ssInputId = SimpleSearchAllSelect[i].id;
            var ssInputVal = SimpleSearchAllSelect[i].value;
            var ColId = ssInputId.replace("_SimpleSearch", "");

            if (document.getElementById(ColId + "_Min") && document.getElementById(ColId + "_Max")) {
                document.getElementById(ColId + "_Min").value = ssInputVal;
                document.getElementById(ColId + "_Max").value = ssInputVal;
            }
            else if (document.getElementById(ColId)) {
                document.getElementById(ColId).value = ssInputVal;
            }
        }


        document.getElementById("TrueSearch").click();
    }
}


/********* 校正浮动标题**********/
function FixFlowTitle(tableID, OldTable) {
    if (document.getElementById(tableID) && document.getElementById(OldTable)) {
        var tab = document.getElementById(tableID);
        var tabMain = document.getElementById(OldTable);
        for (var i = 0; i < tab.rows.length; i++) {
            for (var j = 0; j < tab.rows[i].cells.length; j++) {
                if (j == 0) {
                    //alert(parseInt(tab.rows[i].cells[j].offsetWidth));
                    tab.rows[i].cells[j].style.width = parseInt(tabMain.rows[i].cells[j].offsetWidth) + 16;
                    tab.rows[i].cells[j].style.textIndent = "16px";
                }
                else {
                    tab.rows[i].cells[j].style.width = parseInt(tabMain.rows[i].cells[j].offsetWidth);
                }

            }
        }
    }
}

$(function () {
    //除了表头（第一行）以外所有的行添加click事件.
    $("tr").bind("click", function (event) {
        if (!/^input$/i.test(event.target.nodeName)) {
            //判断td标记的背景颜色和body的背景颜色是否相同;
            if ($(this).children().first().children().is(':checked')) {
                //如果相同，CheckBox.checked=true;
                $(this).find("input:checkbox").prop("checked", false);


            } else {
                //如果不同,CheckBox.checked=false;
                $(this).find("input:checkbox").prop("checked", true);
            }
        }
    });

    //$("tr").bind("dblclick", function () {
    //    $("tr").each(function () {
    //        $(this).find("input:checkbox").prop("checked", false);
    //    });
    //    $(this).find("input:checkbox").prop("checked", true);
    //    alert(11111);
    //});

});
