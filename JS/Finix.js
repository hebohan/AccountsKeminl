//�ı����ɫ
var oldbackColor = "#FFFFFF";  //Ĭ�������򱳾�ɫ ��ɫ
function appendit() {
    var nodes = document.getElementsByTagName("INPUT");
    for (var i = 0; i < nodes.length; i++) {
        var ctype = nodes[i].getAttribute("type");
        if (ctype == 'text') {
//            nodes[i].onfocus = function() { this.style.backgroundColor = '#e6e7e6'; }
//            nodes[i].onblur = function() { this.style.backgroundColor = '#ffffff'; }
           // nodes[i].onfocus = function() { oldbackColor = this.style.backgroundColor; this.style.backgroundColor = '#e6e7e6'; }
           // nodes[i].onblur = function() { this.style.backgroundColor = oldbackColor; }   
        }
    }
}
//��ǩҳ���㣩
function selectTag(showContent, selfObj) {
    // ������ǩ
    var tag = document.getElementById("tags").getElementsByTagName("li");
    var taglength = tag.length;
    for (i = 0; i < taglength; i++) {
        tag[i].className = "";
    }
    selfObj.parentNode.className = "selectTag";
    // ��������
    for (i = 0; j = document.getElementById("tagContent" + i); i++) {
        j.style.display = "none";
    }
    document.getElementById(showContent).style.display = "block";
}
window.onload = function() { appendit(); }

/*************** ѡ�б�ǩ�����ڵ�����checkbox *****************
created by zhangxiaolei 2008/3/1
**************************************************************/
function checkAll(node) {
    if (!node) return;
    var inputs = node.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            var tr = inputs[i].parentNode.parentNode;
            var display = tr.style.display;
            if (display != "none") {
                inputs[i].checked = true;
            }
        }
    }
}

/*************** ȡ��ѡ�б�ǩ�����ڵ�����checkbox *************
created by zhangxiaolei 2008/3/1
**************************************************************/
function cancelAll(node) {
    if (!node) return;
    var inputs = node.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            inputs[i].checked = false;
        }
    }
}

/*************** ѡ��/ȡ��ѡ�б�ǩ�����ڵ�����checkbox *************
created by zhangxiaolei 2008/3/1
**************************************************************/
function checkClick(node) {
    if (!node) return;
    var inputs = node.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (!inputs[i].checked) {
                checkAll(node);
                return;
            }
        }
    }
    cancelAll(node);
}

function hasChecked(node) {
    if (!node) return false;
    var inputs = node.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].checked) {
                return true;
            }
        }
    }
    return false;
}

//ɾ��ǰ�Ի���
function beforeDelete(tableObj) {
    if (!hasChecked(tableObj)) {
        alert("δѡ��Ҫɾ������Ŀ��");
        return false;
    }
    if (confirm("ȷ��Ҫɾ����"))
        return true;
    else
        return false;
}

//�޸�ǰ�Ի���
function beforeEdit(tableObj) {
    if (!isCheckOne(tableObj)) {
        alert("��ѡ���˶���һ���ļ�¼��");
        return false;
    }
    return true;
}

function isCheckOne(node) {
    if (!node) return false;
    var inputs = node.getElementsByTagName("input");
    var chkCount = 0;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].checked) {
                chkCount++;
                if (chkCount > 1) {
                    return false;
                }
            }
        }
    }
    if (chkCount == 1) return true;
    else return false;
}

function getCheckVal(chkName) {
    var inputs = document.getElementsByTagName("input");
    var val = "";
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox" && inputs[i].id == chkName && inputs[i].checked) {
            if (val.length == 0) {
                val = inputs[i].value;
            }
            else {
                val += "," + inputs[i].value;
            }
        }
    }
    return val;
}


/*************** ����ģʽ�Ի��� *******************************
created by zhangxiaolei 2008/3/1                             */

function popWinSmall(url) {
    popWin(url, 400, 300);
}

function popWinCommon(url) {
    popWin(url, 640, 480);
}

function popWinBig(url) {
    popWin(url, 800, 600);
}

function popWin(url, w, h) {
    var vs = window.showModalDialog(url, window, "dialogwidth:" + w + "px; scroll:no ; dialogheight:" + h + "px;status=no");
    if (vs == "true") {
        if (document.all.item("btnQuery") != null && document.all.item("btnQuery") != undefined) {
            document.all.item("btnQuery").click();
        }
        else {
            document.location.reload();
        }
    }
}
function popScrollWin(url, w, h) {
    var vs = window.showModalDialog(url, window, "dialogwidth:" + w + "px; scroll:yes ; dialogheight:" + h + "px;status=no");
    if (vs == "true") {
        if (document.all.item("btnQuery") != null && document.all.item("btnQuery") != undefined) {
            document.all.item("btnQuery").click();
        }
        else {
            document.location.reload();
        }
    }
}
/****************
/��������ѡ��ҳ��
*****************/
function popCalendar(inputname, leave) {
    var dot = "";
    for (var i = 0; i < leave; i++) {
        dot += "../"
    }
    popWin(dot + "sys/toolkits/calendar.html?input=" + inputname, 200, 200);
}
//----------------------------------------------------------
/***********************************************
���������û���
LiChun
2006-03-23
***********************************************/

//�����̬���ؼ�������Ҫ���ã�
document.write("<input type=\"hidden\" id=\"treeparameter\" style=\"position:absolute;left=0;top=0;\" orgroot=\"\" treetype=\"\" checktype=\"\" haschecked=\"\" hascheckedtext=\"\" trueorg=\"\">");
function DynamicTree(DataToSend, pageLevel) {
    var directory = "";
    for (var i = 0; i < pageLevel; i++) {
        directory += "../";
    }
    var vsRe1 = window.showModalDialog(directory + "components/tree/GetTreeData.aspx?" + DataToSend + "", window, "dialogwidth:220px; dialogheight:100px");

    if (vsRe1 == undefined) {
        alert("���ݶ�ȡ�������û�ȡ����");
        return;
    }

    if (vsRe1.substring(0, 5) == "error") {
        alert("����" + vsRe1.substring(6, vsRe1.length));
        return;
    }

    var parameter = "";

    //Ŀ¼����Ĭ��Ϊ�գ�
    parameter += "&orgroot=" + document.all.item("treeparameter").orgroot;

    //Ŀ¼���ͣ�all,org,user��Ĭ��Ϊall��
    parameter += "&treetype=" + document.all.item("treeparameter").treetype;

    //ѡ������ͣ�checkbox,radio��Ĭ��Ϊcheckbox��
    parameter += "&checktype=" + document.all.item("treeparameter").checktype;

    // �Ƿ�ֻѡ��λ
    parameter += "&trueorg=" + document.all.item("treeparameter").trueorg;

    var vsRe2 = window.showModalDialog(directory + "components/tree/Tree.aspx?" + parameter + "", "dd", "dialogwidth:400px; dialogheight:450px");
    return vsRe2;
}

function Dynamic(DataToSend, pageLevel) {
    var directory = "";
    for (var i = 0; i < pageLevel; i++) {
        directory += "../";
    }
    var vsRe1 = window.showModalDialog(directory + "components/tree/GetDeptree.aspx?" + DataToSend + "", window, "dialogwidth:220px; dialogheight:100px");

    if (vsRe1 == undefined) {
        alert("���ݶ�ȡ�������û�ȡ����");
        return;
    }

    if (vsRe1.substring(0, 5) == "error") {
        alert("����" + vsRe1.substring(6, vsRe1.length));
        return;
    }

    var parameter = "";

    //Ŀ¼����Ĭ��Ϊ�գ�
    parameter += "&orgroot=" + document.all.item("treeparameter").orgroot;

    //Ŀ¼���ͣ�all,org,user��Ĭ��Ϊall��
    parameter += "&treetype=" + document.all.item("treeparameter").treetype;

    //ѡ������ͣ�checkbox,radio��Ĭ��Ϊcheckbox��
    parameter += "&checktype=" + document.all.item("treeparameter").checktype;

    // �Ƿ�ֻѡ��λ
    parameter += "&trueorg=" + document.all.item("treeparameter").trueorg;

    var vsRe2 = window.showModalDialog(directory + "components/tree/Tree.aspx?" + parameter + "", "dd", "dialogwidth:400px; dialogheight:450px");
    return vsRe2;
}
/********************************************************
����: wgj
ʱ��: 2005��3��25��
�ص�: �˳����
********************************************************/



function isNumber(obj) {
    var strValue = trimqh(obj.value);
    var pattern = /^(\d)*$/;
    return pattern.test(strValue);
}
/************************************
���������ַ�
************************************/
function LimitCharInput() {
    //	alert(event.keyCode);
    if (((event.keyCode >= 33) && (event.keyCode <= 39)) || ((event.keyCode >= 43) && (event.keyCode <= 47)) || ((event.keyCode >= 59) && (event.keyCode <= 63))) {
        //		alert(event.keyCode);
        event.keyCode = 0;
        return false;
    }
    return true;
}

/**********************
remove the space in the str
add by wgj 2005
*********************/
function trim(str) {
    var i = 0;
    var viStartPos = 0;
    var viEndPos = -1;
    var chr;
    if (typeof (str) != 'undefined') {
        if (typeof (str) != 'string')
            str = str.toString();
        for (i = 0; i < str.length - 1; i++) {
            chr = str.charAt(i);
            if (chr != " ") {
                viStartPos = i;
                break;
            }
        }
        for (i = str.length - 1; i >= 0; i--) {
            chr = str.charAt(i);
            if (chr != " ") {
                viEndPos = i
                break;
            }
        }
    }
    if (viStartPos <= viEndPos) {
        return str.substring(viStartPos, viEndPos + 1);
    }
    else {
        return "";
    }
}
/************************************
tab the focus control on enter fired

by wgj 2005 
*************************************/
function FocusNext() {
    if (event.keyCode == 13) event.keyCode = 9
}
/***********************************
only input the number

add by wgj 12/7/2005
***********************************/
function InputNum() {
    if ((event.keyCode == 13) || (event.keyCode == 9)) {
        event.keyCode = 9;
        return true;
    }
    if ((event.keyCode == 8) || (event.keyCode == 46) || (event.keyCode == 39) || (event.keyCode == 37)) {
        return true;
    }
    if ((event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 229)) {
        return true;
    }
    if ((event.keyCode < 48 || event.keyCode > 57)) {
        event.keyCode = 0;
        return false;
    }
}

/**************************************
this function is for single Grid that inclued
Asp CheckBox Control

get the checked count

add by wgj 12/7/2005
**************************************/

function GetAspCheckedListCount(objDataGrid) {
    if (!objDataGrid) {
        alert("Grid��Ч��");
        return false;
    }
    var strResult = "";
    var count = 0;
    for (var i = 1; i < objDataGrid.rows.length; i++) {
        var oCheckbox = objDataGrid.rows(i).cells(0).children[0];
        if (oCheckbox.checked == true) {
            count++;
        }
    }
    return count;
}

/***********************************************
this function is for single Grid that inclued
checkbox Control

return the checkbox control's value 

the primary key (id) must be stored in the checkbox's value attribute

***********************************************/

function GetCheckedList(objDataGrid) {
    if (!objDataGrid) {
        alert("Grid��Ч��");
        return false;
    }
    var strResult = "";

    for (var i = 1; i < objDataGrid.rows.length; i++) {
        var oCheckbox = objDataGrid.rows(i).cells(0).children[0];
        if (oCheckbox.checked == true) {
            var strId = oCheckbox.value;
            strResult = strResult + "," + strId;
        }
    }
    return strResult;
}
/***********************************************
�������ڱ��ز�����������
this function is for single Grid that inclued
checkbox Control

return the checkbox control's value 

the primary key (id) must be stored in the checkbox's value attribute

***********************************************/

/***********************************************
this function is for single Grid that inclued
checkbox Control

clear all the checkbox control's state 

the primary key (id) must be stored in the checkbox's value attribute

***********************************************/

function ClearCheckedListState(objDataGrid) {
    if (!objDataGrid) {
        alert("Grid��Ч��");
        return false;
    }
    for (var i = 1; i < objDataGrid.rows.length; i++) {
        var oCheckbox = objDataGrid.rows(i).cells(0).children[0];
        if (oCheckbox.checked == true) {
            oCheckbox.checked = false; ;
        }
    }
    return true;
}
/***********************************************
this function is for single Grid that inclued Asp.net
checkbox Control

return the checkbox control's value 

the primary key (id) must be stored in the checkbox's id attribute

by wgj 2005

***********************************************/
function GetAspCheckedList(objDataGrid) {
    if (!objDataGrid) {
        alert("Grid��Ч��");
        return false;
    }
    var strResult = "";

    for (var i = 1; i < objDataGrid.rows.length; i++) {
        var oCheckbox = objDataGrid.rows(i).cells(0).children[0];
        if (oCheckbox.checked == true) {
            var strId = oCheckbox.tid;
            strResult = strResult + "," + strId;
        }
    }
    return strResult;
}
/***********************************************
this function is for single Grid that inclued Asp.net
checkbox Control

Select all the checkboxes in the grid

the checkbox must in the header template

***********************************************/
function SelectAllCheckboxes(spanChk) {
    var oItem = spanChk.children;
    var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;
    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
        //elm[i].click();
        if (elm[i].checked != xState)
            elm[i].click();
        //elm[i].checked=xState;
    }
}
/***********************************************
this function is for post data to http



hehehe 
wgj 2005
***********************************************/
function PostToXmlHttp(DataToSend, Url) {
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", Url, false);
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlhttp.send(DataToSend);
    return xmlhttp.responseXml.xml;
}
//�ύ����
function PostMeg(DataToSend, Url) {
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", Url, false);
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded" );
    xmlhttp.send(DataToSend);
    return xmlhttp.responseText;
}
/***********************************************
this function is for popup a new window



the return value is str

***********************************************/

function ShowPop(url) {
    var window_w = 306;
    var window_h = 408;
    var sFeatures = "toolbar:no;location:no;status:no;menubar:no;scroll:no;resizable:no:left=250;top=250;dialogWidth:" + window_w + "px;dialogHeight:" + window_h + "px;";
    return window.showModalDialog(url, "", sFeatures);
}
//���ݲ�� data--���������;separator--�ָ�����
function subData(data, separator) {
    Score = new Array();
    var strData = data;
    var countS = 0;
    var countE = 0;
    var countNext = 0;
    var n = 0;
    var countMax = data.length;
    while (true) {
        var countNext = strData.indexOf(separator, countS);
        if (countNext == -1) {
            Score[n] = strData.substring(countS, countMax);
            break;
        }
        countE = countNext;
        Score[n] = strData.substring(countS, countE);
        countS = countE + 1;
        n++;
    }
    return Score;
}
/***********************************************
this function is html server button



the return value is str by wgj 2006 2

***********************************************/
function HtmlBtnClickOnce(sender) {
    if (sender) {
        if (sender.disabled) {
            return false;
        }
        sender.disabled = true;
    }
}
/***********************************************
���ذ�ť���ص�ָ��ҳ��


***********************************************/
function Bback() {
    window.history.go(-(window.history.length - 1));
}






var pointY;
var pointX
function show(e, id) {
    if (document.getElementById(id) != null) {
        document.getElementById(id).style.posTop = e.clientY;
        document.getElementById(id).style.posLeft = e.clientX;
        document.getElementById(id).style.display = "";
    }
}
function hidden(id) {
    if (document.getElementById(id) != null) {
        document.getElementById(id).style.display = "none";
    }
}
function goback() {
    document.getElementById('goback').href = document.all('backUrl').value;
    document.getElementById('goback').click();
}

function getUrlvalue(value) {
    var url = document.URL.split('?');
    var queryString;
    var name
    if (url.length > 1) {
        queryString = url[1].split('&');
        for (var i = 0; i < queryString.length; i++) {
            if (queryString[i].indexOf(value) > -1) {
                name = queryString[i].substring(queryString[i].indexOf("=") + 1);
                return name;
            }
        }
    }
    return "";
}

//��ʾ��
function messagebox(title, message) {
    if (title == null || title == "") {
        title = "��Ϣ";
    }
    Ext.onReady(function() {
        Ext.MessageBox.alert(title, message, '');
    });
}
//ѡ���
function messageconfirm(name, title, message) {
    if (title == null || title == "") {
        title = "��Ϣ";
    }
    var returnvalue;
    Ext.onReady(function() {

        //Ext.MessageBox.confirm(title,message,showResult);
        Ext.get(name).on('click', function(e) {
            Ext.MessageBox.confirm(title, message, function(btn) {
                return btn;
            });
        });

    });

}
//��̬װ��js
function loadjavascript(fileContent) {
    var fileref = document.createElement('script')//������ǩ 
    fileref.setAttribute("type", "text/javascript")//��������type��ֵΪtext/javascript 
    //fileref.setAttribute("src", filename)//�ļ��ĵ�ַ 
    //alert(fileref.innerText);
    fileref.text = fileContent;

    if (typeof fileref != "undefined") {
        document.getElementsByTagName("head")[0].appendChild(fileref)
    }
}


/**
*��̬������һ��
* ����: tableObj
*/
function addrow(tableObjname, rowno) {
    var tableObj = document.all.item(tableObjname);
    //var tablerowcount=document.all.RowCount.value;
    if (tableObj == null || tableObj.tagName != "TABLE") return;
    //var tdstring =new Array("<INPUT id='Id' type='checkbox' name='Id'>","<input id='ExpertId' name='ExpertId' class='detail_edit'style='width:97%'/>","<input id='ExpertName' name='ExpertName' class='detail_edit'style='width:97%'/>");
    //var tdwidth = new Array("6%","40","54%");
    var tr = tableObj.rows[1];
    if (tr != null) {

        for (j = 1; j <= rowno; j++) {
            try {
                var ntr = tableObj.insertRow();
                ntr.ondblclick = tr.ondblclick;
                for (var i = 0; i < tr.cells.length; i++) {
                    var otd = ntr.insertCell();

                    //�������ڿؼ�
                    var temhtml;
                    if (tr.cells[i].innerHTML.indexOf("datetime") > 0) {
                        temhtml = tr.cells[i].innerHTML.replace(/rowIndex/g, ntr.rowIndex);
                        otd.innerHTML = temhtml.replace("l_must", "v_must");
                    } else
                        otd.innerHTML = tr.cells[i].innerHTML.replace("l_must", "v_must");

                    otd.width = tr.cells[i].width;
                    otd.style.display = tr.cells[i].style.display;
                    otd.align = tr.cells[i].align;
                }
            } catch (ex) {
                alert("����һ��ʧ�ܣ�" + ex.description);
                return;
            }
        }
    }

}
/**
*��̬��ɾ��һ��
* ����: tableObj
*/
function delrow(tableObj) {
    if (tableObj == null || tableObj.tagName != "TABLE") return;
    for (var i = 1; i < tableObj.rows.length; i++) {
        var otd = tableObj.rows[i].cells[0];
        if (otd.children[0].checked) {
            try {
                tableObj.deleteRow(i);
                i = i - 1;
            } catch (ex) {
                alert(" ɾ��һ��ʧ�ܣ�" + ex.description);
                return;
            }
        }
    }
}

/*******************
�һ������ڵ������Ӷ���
*******************/
function disableChilds(obj) {
    if (!obj) return;
    for (var i = 0; i < obj.childNodes.length; i++) {
        if (obj.childNodes(i).tagName == "INPUT" || obj.childNodes(i).tagName == "TEXTAREA" || obj.childNodes(i).tagName == "IMG" || obj.childNodes(i).tagName == "SELECT") {

            obj.childNodes(i).disabled = true;
            obj.childNodes(i).setAttribute("CanWrite", '0');

        }
        disableChilds(obj.childNodes(i));
    }

}

/*******************
���һ������ڵ������Ӷ���
*******************/
function enableChilds(obj) {
    if (!obj) return;
    for (var i = 0; i < obj.childNodes.length; i++) {
        if (obj.childNodes(i).tagName == "INPUT" || obj.childNodes(i).tagName == "TEXTAREA" || obj.childNodes(i).tagName == "SELECT") {

            obj.childNodes(i).disabled = false;
            if (obj.childNodes(i).name != "rtnName") {
                obj.childNodes(i).value = obj.childNodes(i).value.replace(",", "��");
            }
        }
        enableChilds(obj.childNodes(i));
    }
    actionClicked = true;

}

//ֻ����������
function OnlyNumeric(obj) {
    //�Ȱѷ����ֵĶ��滻�����������ֺ�.
    obj.value = obj.value.replace(/[^\d.]/g, "");
    //���뱣֤��һ��Ϊ���ֶ�����.
    obj.value = obj.value.replace(/^\./g, "");
    //��ֻ֤�г���һ��.��û�ж��.
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //��֤.ֻ����һ�Σ������ܳ�����������
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
}
function strlen(str) {
    var len = 0
    for (var i = 0; i < str.length; i++) {
        var tmp = str.charAt(i);
        if (escape(str.charAt(i)) != tmp) {
            len += 2;
        }
        else {
            len++;
        }
    }
    return len;
}
//�����ı���ؼ����ַ���
function checkAbstract(obj, len) {
    var ab = obj.value;
    if (strlen(ab) > len) {
        var len1 = 0;
        for (var i = 0; i < ab.length; i++) {
            var tmp = ab.charAt(i);
            if (escape(ab.charAt(i)) != tmp) {
                len1 += 1;
            }
            else {
                len1 += 1;
            }
        }
        if (len1 > 0) {
            len1 = len1 - 1;
        }
        obj.value = obj.value.substr(0, len1);
    }
}

//****************************************
// �������õ��Ľű�
//*****************************************
function WF_XmlHttpPost(DataToSend, Url) {
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", Url, false);
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlhttp.setRequestHeader("charset", "utf-8");
    xmlhttp.send(DataToSend);
    //alert(DataToSend);
    return xmlhttp.responseXml.xml;
}
//������ѡ�����Ի��⴦���˺��ύ
function IndirectWfSubmit(ActionName) {
    if (!check(Form1)) return false;
    var strXML = "<?xml version=\"1.0\"?><xDocuments>";
    for (var i = 0; i < document.all.length; i++) {
        if (document.all(i).type) {
            if (document.all(i).id != "") {
                strXML = strXML + "<xFiles><xName>" + document.all(i).id + "</xName><xValue>" + document.all(i).value + "</xValue></xFiles>";
            }
        }
    }
    strXML = strXML + "</xDocuments>";
    strXML = escape(strXML);
    var url = document.URL.split('?');
    if (url.length > 1) {
        WF_XmlHttpPost("strXML=" + strXML, "../../workflow/action/wfSession.aspx");
        //����������auto�������ƴ�����Զ��ر�
        var srsub = document.all;
        if (url.length > 1) {
            var ok = window.showModalDialog("../../workflow/action/wfOpinion.aspx?auto=1&" + url[1] + "&sourcerefenceId=" + document.all("docId").value, srsub, "dialogHeight:0px;dialogWidth:0px;help:no;status:no;dialogLeft:300px;dialogTop:100px")//"width=500,height=400,top=140,left=280")
        }

        var rtnValue = showModalDialog("../../workflow/action/wfsubmit.aspx?" + url[1], "", "dialogHeight:430px;dialogWidth:600px;help:no;status:no");
        if (rtnValue != null && rtnValue != undefined) {
            document.all('rtnName').value = rtnValue;
            var param = url[1].split('&');
            var action = "CommandKey=FormSubmitCmd";
            for (var i = 0; i < param.length; i++) {
                var paramName = param[i].split('=')[0];
                if (paramName != "CommandKey" && paramName != "IsSave") {
                    action += "&" + param[i];
                }
            }
            enableChilds(Form1);
            document.Form1.action = "SendCommand.aspx?" + action;
            window.Form1.submit();

        }
    }
}


//�����ύ
function WfSubmit(ActionName, ifcheck) {
    if (ifcheck == false) {
    }
    else {
        if (!check(form1)) return false;
    }

    var url = document.URL.split('?');
    var ifOk;
    //�������׻��ڻ�û�С��������������򲻵��������
//    if (url[1].indexOf("ActivityInstanceID") > 0 && document.all("btnOpinion") != null) {
//        ifOk = OpenAudit("1");
//        if (ifOk != "ok") return false;
//    }
     	var strXML = "<?xml version=\"1.0\"?><xDocuments>";
    	for(var i=0;i<document.all.length;i++)
    	{
    		if(document.all(i).tagName=="INPUT" ||document.all(i).tagName=="TEXTAREA" ||document.all(i).tagName=="SELECT" )
    		{
    			if(document.all(i).id != "")
    			{
    				strXML = strXML + "<xFiles><xName>" + document.all(i).id + "</xName><xValue>" + document.all(i).value + "</xValue></xFiles>";
    			}
    		}
    	}
    	strXML = strXML + "</xDocuments>";
    	strXML =escape(strXML);
    	//    	PostToXmlHttp("strXML=" + strXML, "../../workflow/action/wfsubmit.aspx");
    	PostToXmlHttp(url[1], "../../workflow/action/wfsubmit.aspx");
//    var rtnValue = showModalDialog("../../workflow/action/wfsubmit.aspx?" + url[1], "", "dialogHeight:430px;dialogWidth:600px;help:no;status:no");
//    var rtnValue = PostMeg(url[1], "../../workflow/action/wfsubmit.aspx");
//    if (rtnValue != null && rtnValue != undefined) {
//        document.all('rtnName').value = rtnValue;
        return true;
//    }
//    return false;

}
//������
function OpenAudit(ifSubmit) {
    var url = document.URL.split('?');
    var srsub = document.all;
    if (url.length > 1) {
        var openWinUrl = "../../Integrated/Public/Opinion.aspx?" + url[1] + "&sourcerefenceId=&submit=" + ifSubmit;
        var ok = window.showModalDialog(openWinUrl, srsub, "dialogHeight:500px;dialogWidth:660px;help:no;status:no;dialogLeft:170px;dialogTop:90px")
        return ok;
    }
}
//�ύ����
function OpenAttachFile(businessType, businessState, entityName) {
    var url = document.URL.split('?');
    //window.open("../../Resources/Tab_Fujian.aspx?EntityType=CustomForm&"+url[1]+"&BusinessType="+businessType+"&BuinessState="+businessState+"&EntityName="+entityName);
    popWinBig("../../Resources/Tab_Fujian.aspx?EntityType=CustomForm&" + url[1] + "&BusinessType=" + businessType + "&BuinessState=" + businessState + "&EntityName=" + entityName);
}
//��ӡ
function PrintDoc(printmodalId, primaryKeys) {
    var docUrl = "../../../FormPrint/CommonPrint.aspx?PrintModalId=" + printmodalId + "&primaryKeys=" + primaryKeys;
    window.open(docUrl, '��ӡ', 'toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=1000,height=700');
}

//����
function huitui() {
    var count = 0;
    var url = document.URL.split('?');
    var queryString;
    if (url.length > 1) {
        queryString = url[1];
    }
    var ifOk;
    //�������׻��ڻ�û�С��������������򲻵��������
    /*if(url[1].indexOf("ActivityInstanceID")> 0 || document.all("btnOpinion")!=null )
    { 
    ifOk = OpenAudit("1");
    if(ifOk != "ok") return false;
    }*/
    var ret = OpenTree2("../../../components/generaltree/", "ActivityTree", queryString);
    if (ret != null && ret != "undefined") {
        queryString += "&ActionType=huitui&HuiTuiStepId=" + ret.split('|')[0];
        PostToXmlHttp(queryString, "../../workflow/action/wfCommonAction.aspx");
        window.location = "../../Public/PublicList.aspx?ListType=flow&" + queryString;
    }
}

//�˻�
function retreat() {
    var count = 0;
    var url = document.URL.split('?');
    var queryString;
    if (url.length > 1) {
        queryString = url[1];
    }
//    var ifOk;
//    //�������׻��ڻ�û�С��������������򲻵��������
//    if (url[1].indexOf("ActivityInstanceID") > 0 || document.all("btnOpinion") != null) {
//        ifOk = OpenAudit("1");
//        if (ifOk != "ok") return false;
//    }
    queryString += "&ActionType=tuihui&mode=custom";
    PostToXmlHttp(queryString, "../../workflow/action/wfCommonAction.aspx");
//    window.location = "../../Public/PublicList.aspx?ListType=flow&" + queryString;
}
function tuihui() {
    var count = 0;
    var url = document.URL.split('?');
    var queryString;
    if (url.length > 1) {
        queryString = url[1];
    }
    var ifOk;
    //�������׻��ڻ�û�С��������������򲻵��������
    if (url[1].indexOf("ActivityInstanceID") > 0 || document.all("btnOpinion") != null) {
        ifOk = OpenAudit("1");
        if (ifOk != "ok") return false;
    }
    queryString += "&ActionType=tuihui";
    PostToXmlHttp(queryString, "../../workflow/action/wfCommonAction.aspx");
   // window.location = "../../workflow/workitem/worktodolist.aspx";
}  

//����
function bohui() {
    var count = 0;
    var url = document.URL.split('?');
    var queryString;
    if (url.length > 1) {
        queryString = url[1];
    }
    var ifOk;
    //�������׻��ڻ�û�С��������������򲻵��������
    if (url[1].indexOf("ActivityInstanceID") > 0 || document.all("btnOpinion") != null) {
        ifOk = OpenAudit("1");
        if (ifOk != "ok") return false;
    }
    queryString += "&ActionType=bohui";
    PostToXmlHttp(queryString, "../../../workflow/action/wfCommonAction.aspx");
    window.location = "../../Public/PublicList.aspx?ListType=flow&" + queryString;
}


//****************�ţ��*********************
//��̬������������
function WfSubmitEnd(name, IsDynamicEnd) {

    if (!check(Form1)) return false;
    var strXML = "<?xml version=\"1.0\"?><xDocuments>";
    for (var i = 0; i < document.all.length; i++) {
        if (document.all(i).type) {
            if (document.all(i).id != "") {
                //strXML = strXML + "<xFiles><xName>" + document.all(i).id + "</xName><xValue>" + document.all(i).value + "</xValue></xFiles>";
                if (document.all(i).type == "radio") {
                    var obj = document.all(i);
                    if (obj.checked) {
                        strXML = strXML + "<xFiles><xName>" + document.all(i).id + "</xName><xValue>" + document.all(i).value + "</xValue></xFiles>";

                    }

                }
                else
                    strXML = strXML + "<xFiles><xName>" + document.all(i).id + "</xName><xValue>" + document.all(i).value + "</xValue></xFiles>";

            }
        }
    }
    strXML = strXML + "</xDocuments>";
    strXML = escape(strXML);
    var url = document.URL.split('?');
    if (url.length > 1) {
        PostToXmlHttp("strXML=" + strXML, "../../workflow/action/wfSession.aspx");
        //����������auto�������ƴ�����Զ��ر�
        var srsub = document.all;
        if (url.length > 1) {
            var ok = window.showModalDialog("../../Integrated/Public/Opinion.aspx?auto=1&" + url[1] + "&sourcerefenceId=" + document.all("docId").value, srsub, "dialogHeight:0px;dialogWidth:0px;help:no;status:no;dialogLeft:300px;dialogTop:100px")//"width=500,height=400,top=140,left=280")
        }
        //var formOption = showModalDialog("../../workflow/action/wfsubmit.aspx?"+url[1],"","dialogHeight:430px;dialogWidth:600px;help:no;status:no");
        var rtnValue = "";
        var rtnValue1 = showModalDialog("../FormAction/DynamicShow.aspx?" + url[1], "", "dialogHeight:430px;dialogWidth:600px;help:no;status:no");
        if (rtnValue1 != null && rtnValue1 != undefined) {
            if (rtnValue1 == "no")
            { return; }
            rtnValue = showModalDialog("../../workflow/action/wfsubmit.aspx?" + url[1] + "&action=auto", "", "dialogHeight:430px;dialogWidth:600px;help:no;status:no");
        }
        else {
            rtnValue = showModalDialog("../../workflow/action/wfsubmit.aspx?" + url[1], "", "dialogHeight:430px;dialogWidth:600px;help:no;status:no");
        }

        if (rtnValue != null && rtnValue != undefined) {


            if (IsDynamicEnd == true)
            { DynamicEnd(name); OpenDynamic('EndDynamic'); }
            else
            { OpenDynamic('StartDynamic'); } //��̬������������}
            document.all('rtnName').value = rtnValue;
            var param = url[1].split('&');
            var action = "CommandKey=FormSubmitCmd";
            for (var i = 0; i < param.length; i++) {
                var paramName = param[i].split('=')[0];
                if (paramName != "CommandKey" && paramName != "IsSave") {
                    action += "&" + param[i];
                }
            }
            //���뱾�ز�����ʷ��Ϣ
            PostToXmlHttp(url[1] + "&ActionName=" + name + "&sourcerefenceId=" + document.all("docId").value, "../../WorkFlow/Action/ActionContrl.aspx");
            enableChilds(Form1);
            document.Form1.action = "SendCommand.aspx?" + action;
            window.Form1.submit();
        }
    }
}
function DisableAll(id) {
    var node = document.getElementById(id);
    if (!node) return;
    var inputs = node.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "button") {
            inputs[i].disabled = true;
        }
    }
}
