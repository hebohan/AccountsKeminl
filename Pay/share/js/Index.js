//��ҳǰ̨UI����
//youqing 2011-8-24 17:13:18
var H1s,H2s,H3s, h4s;
//��̬װ��loading
//document.write("<base target=\"_self\" />");
//document.write(" <div id=\"loading\" class=\"bakloading\" style=\"position:absolute;z-index:9999;width:100%;height:100%;left:0px;top:0px;filter:alpha(opacity=80)\"></div>");
//document.write(" <div id=\"loading1\" style=\"border:1 solid #0099CC;z-index:9999;position:absolute;top:expression((this.parentElement.offsetHeight-this.offsetHeight)/2);left:expression((this.parentElement.offsetWidth-this.offsetWidth)/2); background-color:#DDEDFB;  padding-right:5px; padding-left:5px;\"><table style=\"font-size:12px;\"><tr><td class=\"imgloading\" width=20 height=20>&nbsp;</td><td valign=middle>ҳ������У����Ե�...</td></tr></table></div>");
document.onreadystatechange = function() {
    if (document.readyState == "complete" || document.readyState == "interactive") {
//        if (document.getElementById("loading") != null) {
//            document.getElementById("loading").style.display = "none";
//        }
//        if (document.getElementById("loading1") != null) {
//            document.getElementById("loading1").style.display = "none";
//        }


        H3s = document.getElementsByTagName("h3");
        H4s = document.getElementsByTagName("h4");
        for (var i = 0; i < H3s.length; i++) {
            EventUtil.addEventHandler(H3s[i], "click", HidShowH3);
            H3s[i].title = H3s[i].innerText + " ";
            H3s[i].style.cursor = "hand";
        }


        H1s = document.getElementsByTagName("h1");
        for (var i = 0; i < H1s.length; i++) {
            EventUtil.addEventHandler(H1s[i], "click", HidShowH1);
            H1s[i].title = H1s[i].innerText + " ";
            H1s[i].style.cursor = "hand";
        }
        H2s = document.getElementsByTagName("h2");
        for (var i = 0; i < H2s.length; i++) {
            EventUtil.addEventHandler(H2s[i], "click", HidShowH2);
            H2s[i].title = H2s[i].innerText.replace('��', '') + " ";
            H2s[i].style.cursor = "hand";
        }

        



    }
}
window.onresize = function() {
}


/********* ��ҳTopMenu�˵����**********/
function HidShowH3() {
    for (var i = 0; i < H3s.length; i++) {
        Share.removeClassName(H3s[i], "h3Yes");
    }
    for (var i = 0; i < H4s.length; i++) {
        Share.removeClassName(H4s[i], "h4Yes");
    }
    var obj = window.event.srcElement;
    //���ݵ��ͼƬ����Ԫ�أ�
    if (obj.nodeName == "SPAN") {
        obj = obj.parentNode;
    }
    Share.addClassName(obj, "h3Yes");
    Share.addClassName(getNextObj(obj), "h4Yes");

}
function ShowLoginInfo() {
    $("LoginInfo").style.display = "block";
    $("Buju").style.display = "none";
}
function ShowBuju() {
    $("LoginInfo").style.display = "none";
    $("Buju").style.display = "block";
}
function hiddenLoginInfo() {
    $("LoginInfo").style.display = "none";
}
function hiddenBuju() {
    $("Buju").style.display = "none";
}


function ChangeLayOut(i) {
    for (var y = 1; y < 4; y++) {
        $("LayOutBtn" + y).src = "Share/images/Index/LayOut" + y + "No.gif";
    }
    $("LayOutBtn" + i).src = "Share/images/Index/LayOut" + i + "Yes.gif";
    if (i == 1) {
        $("IndexLogo").style.display = "block";
        window.frmcenter.document.getElementById('fraContent').cols = '180,*';
        $("LoginInfo").style.top = "103px";
        $("LoginInfo").style.right = "23px";
        $("buju").style.top = "103px";
        $("buju").style.right = "28px";
    }
    else if (i == 2) {
        $("IndexLogo").style.display = "none";
        window.frmcenter.document.getElementById('fraContent').cols = '180,*';
        $("LoginInfo").style.top = "23px";
        $("LoginInfo").style.right = "23px";
        $("buju").style.top = "23px";
        $("buju").style.right = "28px";
    }
    else if (i == 3) {
        $("IndexLogo").style.display = "none";
        window.frmcenter.document.getElementById('fraContent').cols = '0,100%';
        $("LoginInfo").style.top = "23px";
        $("LoginInfo").style.right = "23px";
        $("buju").style.top = "23px";
        $("buju").style.right = "28px";

    }
}

/********* LeftMenu�˵����**********/
//������˵���ʾ�������л�
function HidShowH1() {

    for (var i = 0; i < H1s.length; i++) {
        Share.removeClassName(H1s[i], "leftMenuH1Yes");
        Share.addClassName(H1s[i], "leftMenuH1No");
        getNextObj(H1s[i]).style.display = "none";
    }
    var obj = window.event.srcElement;
    //���ݵ�
    if (obj.nodeName == "SPAN") {
        obj = obj.parentNode;
    }
    //���ݵ��ͼƬ����Ԫ�أ�
    if (obj.nodeName == "IMG") {
        obj = obj.parentNode.parentNode;
    }
    Share.removeClassName(obj, "leftMenuH1No");
    Share.addClassName(obj, "leftMenuH1Yes");
    
    getNextObj(obj).style.display = "block";
}

//���β˵�ѡ�С�δѡ���л�
function HidShowH2() {
    for (var i = 0; i < H2s.length; i++) {
        Share.removeClassName(H2s[i], "leftMenuH2Yes");
    }
    var obj = window.event.srcElement;
    //���ݵ��ͼƬ����Ԫ�أ�
    if (obj.nodeName == "SPAN") {
        obj = obj.parentNode;
    }
    Share.addClassName(obj, "leftMenuH2Yes");

}

//�����˵�����
function recall(url) {
    window.parent.frames["main"].location = url;
}
    
        

