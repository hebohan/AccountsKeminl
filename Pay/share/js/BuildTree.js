//���β˵�����js
//youqing 2011-10-26 18:32:01

//����Ŀ���� new
function sonElmOp(OpObj) {

    var OpObjChecked = getSecChild(OpObj).checked;
    var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
    document.getElementById("IfsonElmOp").checked = false;

    //����Ԫ�صȼ�
    var ElmLevel = OpObj.parent.split('|').length - 1;
    var IsSameLevel = true;
    do {
        try {
            var NextObj = getNextObj(OpObj);
            var NextObjLevel = NextObj.parent.split('|').length - 1;
            if (ElmLevel < NextObjLevel) {
                //�������ѡ������һ��
                if (OpObjChecked != getSecChild(NextObj).checked) {
                    getSecChild(NextObj).click();
                }
                OpObj = NextObj;
            }
            else {
                IsSameLevel = false;
            }
        }
        catch (err) {
            IsSameLevel = false;
        }
    } while (IsSameLevel);

    document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
    return;


}





//���ӵ������Ŀ new�� �����ӵ����౸ѡ���ֵ��Ҳ�Ĺ��ܣ���ֹ������
function addRight(i) {
    var OpObj; //��ȡ��������
    var e = (event) ? event : window.event;
    if (window.event) {//IE
        e.cancelBubble = true;
    } else { //FF
        e.stopPropagation();
    }
    var oEvent = e.srcElement;
    
    
    //��ѡ��ťʱ
    if (getSecChild(document.getElementById("left@_" + i)).type == "radio" && !getSecChild(document.getElementById("left@_" + i)).disabled) {
        var HasSelValTxtone = document.getElementById("HasSelValTxt").value.split(',');
        //�Ѵ�ֵ��ѡֵ
        if (HasSelValTxtone.length > 1) {
            for (var k = 0; k < HasSelValTxtone.length; k++) {
                if (HasSelValTxtone[k] != "" && document.getElementById("left@_" + HasSelValTxtone[k])) {

                    getSecChild(document.getElementById("left@_" + HasSelValTxtone[k])).checked = false;
                    document.getElementById("right@_" + HasSelValTxtone[k]).style.display = "none";
                }
            }
            document.getElementById("HasSelValTxt").value = "";
            document.getElementById("HasSelNameTxt").value = "";

        }
        else {
            document.getElementById("left@_" + i).checked = true;
            document.getElementById("right@_" + i).style.display = "block";
        }

    }


    //���������Ŀ ѡ��/ȡ��
    if (document.getElementById("left@_" + i)) {
        //ѡ�����
        if (getSecChild(document.getElementById("left@_" + i)).checked) {
            if (document.getElementById("right@_" + i)) {
                document.getElementById("right@_" + i).style.display = "block";
                HasSelValTxt.value += document.getElementById("right@_" + i).value + ",";
                HasSelNameTxt.value += document.getElementById("right@_" + i).innerText.Trim() + ",";
            }
        }
        //ȡ��ѡ�����
        else {
            if (document.getElementById("right@_" + i)) {
                document.getElementById("right@_" + i).style.display = "none";
                HasSelValTxt.value = HasSelValTxt.value.replace(document.getElementById("right@_" + i).value + ",", "");
                HasSelNameTxt.value = HasSelNameTxt.value.replace(document.getElementById("right@_" + i).innerText.Trim() + ",", "");
            }
        }
    }



    //	        var OpObj; //��ȡ��������
    //	        var e=(event) ? event : window.event;
    //	        if (window.event) {//IE
    //	            e.cancelBubble=true;
    //	        } else { //FF
    //	            e.stopPropagation();
    //	        }
    //	        var oEvent = e.srcElement;
    //	        
    //	        
    //	        //�����ѡ/��ѡ��ťʱ��
    //	        if (oEvent.nodeName == "INPUT") {
    //	            OpObj = oEvent.parentElement;

    //	        }
    //	        else if (oEvent.nodeName == "IMG") {
    //	        return;
    //	        }
    //	        //�������ʱ��
    //	        else {
    //	            OpObj = oEvent;
    //	            //��ѡ��ѡ��ť�����disabledʱ�����ܸ�д��
    //	            if (!getSecChild(OpObj).disabled) {
    //	                if (getSecChild(OpObj).checked) {
    //	                    getSecChild(OpObj).checked = false;
    //	                }
    //	                else {
    //	                    getSecChild(OpObj).checked = true;
    //	                }
    //	            }
    //	        }



    //����Ŀ����(�˴�Ӧ�ü����Ƿ����������ã����û�ѡ����̨����)
    if (document.getElementById("IfsonElmOp") && document.getElementById("IfsonElmOp").checked) {
        var OpObj = document.getElementById("left@_" + i);
        sonElmOp(OpObj);

    }


    return;


    //	        //д�Ҳ���ѡ
    //	        writeSelected(OpObj);


    //	        //�ж�����Դ�Ƿ��ǵ���Ҳ���ѡ������Ŀ
    //	        if (OpObj.id.indexOf("right@_") > -1) {
    //	            getSecChild(document.getElementById(OpObj.id.replace("right@_", "left@_"))).checked = false;
    //	        }


}
//����Ҳ���ѡ new
function ClearRight() {
    var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
    document.getElementById("IfsonElmOp").checked = false;
    for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
        if (getSecChild(document.getElementById("leftLay").childNodes[i]).checked) {
            getSecChild(document.getElementById("leftLay").childNodes[i]).click();
        }
    }
    document.getElementById("HasSelValTxt").value = "";
    document.getElementById("HasSelNameTxt").value = "";

    document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
}

//ȫѡ��� new
function SelectLeftAll() {
    var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
    document.getElementById("IfsonElmOp").checked = false;
    for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
        if (!getSecChild(document.getElementById("leftLay").childNodes[i]).checked) {
            getSecChild(document.getElementById("leftLay").childNodes[i]).click();
        }
    }
    document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
}




//����Ҳ���ѡ�� new
function removeRight(i) {
    if (document.getElementById("left@_" + i)) {
        getSecChild(document.getElementById("left@_" + i)).checked = false;
        if (document.getElementById("right@_" + i)) {
            document.getElementById("right@_" + i).style.display = "none";
            HasSelValTxt.value = HasSelValTxt.value.replace(document.getElementById("right@_" + i).value + ",", "");
            HasSelNameTxt.value = HasSelNameTxt.value.replace(document.getElementById("right@_" + i).innerText.Trim() + ",", "");

        }
    }
}

//��ʾ/��������
function ShowHiddenSon() {
    var OpObj; //��ȡ��������
    var e = (event) ? event : window.event;
    if (window.event) {//IE
        e.cancelBubble = true;
    } else { //FF
        e.stopPropagation();
    }
    var oEvent = e.srcElement;
    //alert(oEvent.outerHTML);

    if (oEvent.src.indexOf("up.gif") > -1) {
        for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
            var sonElm = document.getElementById("leftLay").childNodes[i];
            //ֱ�����ڵ�ID
            var sonElmLastParentID = sonElm.parent.split('|')[sonElm.parent.split('|').length - 2];
            //alert(sonElmLastParentID+"--"+getNextObjByID(oEvent).value);
            if (sonElmLastParentID == getNextObj(oEvent).value) {
                sonElm.style.display = "block";
            }
        }
        oEvent.src = "../images/Tree/down.gif";
    }
    else if (oEvent.src.indexOf("down.gif") > -1) {

        for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
            var sonElm = document.getElementById("leftLay").childNodes[i];
            //alert(getNextObjByID(oEvent).outerHTML);
            if (sonElm.parent.indexOf(getNextObj(oEvent).value) > -1) {
                sonElm.style.display = "none";
                if (getFirstChild(sonElm).src.indexOf("down.gif") > -1) {
                    getFirstChild(sonElm).src = "../images/Tree/up.gif";
                }
            }
        }
        oEvent.src = "../images/Tree/up.gif";


    }
    else { } //none.gifʱ
}

//	    //��������
//	    function HidenSon() {
//	        var OpObj; //��ȡ��������
//	        var e = (event) ? event : window.event;
//	        if (window.event) {//IE
//	            e.cancelBubble = true;
//	        } else { //FF
//	            e.stopPropagation();
//	        }
//	        var oEvent = e.srcElement;


//	        for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
//	            var sonElm = document.getElementById("leftLay").childNodes[i];

//	            
//	        }
//	        
//	        oEvent.onclick = "ShowSon()";
//	    }


//���������� new
function OpenAll() {
    for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
        document.getElementById("leftLay").childNodes[i].style.display = "block";
        if (getFirstChild(document.getElementById("leftLay").childNodes[i]).src.indexOf("up.gif") > -1) {
            getFirstChild(document.getElementById("leftLay").childNodes[i]).src = "../images/Tree/down.gif";
        }

    }
}


//���������� new
function CloseAll() {
    var AllImg = document.getElementById("DownUpImg").value;
    if (AllImg != "") {
        for (var i = 0; i < AllImg.split('|').length - 1; i++) {
            var oneImg = document.getElementById(AllImg.split('|')[i]);
            if (oneImg.src.indexOf("down.gif") > -1) {
                oneImg.click();
            }
        }
    }
}




//��ȡ�Ҳ�ѡ�� new
function getAllSel() {

    var names = document.getElementById("HasSelNameTxt").value;
    var values = document.getElementById("HasSelValTxt").value;
    if (names.indexOf(",") > -1) {
        names = names.substring(0, parseInt(names.length) - 1);
        values = values.substring(0, parseInt(values.length) - 1);
    }

    window.returnValue = names + "|" + values;
    window.close();

}
//���ǰ̨ѡ��
function ClearFrontInput() {
    window.returnValue = "|";
    window.close();
}


//���ش���ѡ����Ŀ
function LoadHasSel() {
    var FrontValueID = getUrlvalue("HasSel");
    if (window.dialogArguments&&window.dialogArguments.document.getElementById(FrontValueID)) {
        //document.getElementById("HasSelValTxt").value = window.dialogArguments.document.getElementById(FrontValueID).value;
        var HasSelVal = window.dialogArguments.document.getElementById(FrontValueID).value;
        //alert(HasSelVal);
        var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
        document.getElementById("IfsonElmOp").checked = false;
        if (HasSelVal != "") {
            for (var i = 0; i < HasSelVal.split(',').length; i++) {
                var oneSelVal = HasSelVal.split(',')[i];
                if (oneSelVal != "" && document.getElementById("left@_" + oneSelVal)) {
                    getSecChild(document.getElementById("left@_" + oneSelVal)).click();
                }
            }
        }
        document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
    }
}




//���ҹؼ���
var NS4 = (document.layers);
var IE4 = (document.all);
var win = this;
var n = 0;

function searchKeywords() {
    var charCode = (window.event || e).keyCode;
    if (charCode == 13) {
        document.getElementById("searchBtn").click();
        //����ͨ����ȡ�������ð�ť����Ϊ��ʱ�����������еĹؼ�����
    }
}


function findInPage(str) {
    if (str == "") {
        return;
    }


    //����ǰ����ȫ��չ��
    OpenAll();

    var txt, i, found;
    if (str == "" || str == "")
        return false;
    try {
        if (NS4) {
            if (!win.find(str))
                while (win.find(str, false, true))
                n++;
            else
                n++;
            if (n == 0) alert("û��ѡ���[ " + str + " ]" + "ƥ�䣬�����������ؼ���!");
        }
        if (IE4) {
            txt = win.document.body.createTextRange();
            //alert(win.document.body.outerHTML);
            //txt = document.getElementById("leftLay").createTextRange();
            for (i = 0; i <= n && (found = txt.findText(str)) != false; i++) {
                txt.moveStart("character", 1);
                txt.moveEnd("textedit");
            }
            if (found) {
                txt.moveStart("character", -1);
                txt.findText(str);
                txt.select();
                txt.scrollIntoView();
                n++;
            }
            else {
                if (n > 0) {
                    n = 0;
                    findInPage(str);
                }
                else
                    alert("û��ѡ���[ " + str + " ]" + "ƥ�䣬�����������ؼ���!");
            }
        }
        return false;
    }
    catch (Error) {
        alert("������ϣ�");
        n = 0;
        return false;
    }
}