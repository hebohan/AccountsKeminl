//js�������������ã�
//youqing 2011-3-29 23:14:33


/**********ȡԪ��***************/
//�÷��� $("topDv")

function $(id) {
	try{
	var a = document.getElementById(id);
	}
	catch(e)
	{
		alert(e +"-"+id);
	}
    return a; 
}


/**********ȡĳԪ�غ����Ԫ��***************/
//�÷��� getNextObj("topDv") topDvΪ��ǰԪ��ID

function getNextObj(Element) {
    try {
        if (Element.nextSibling.nodeType == 3) {
            sibling = Element.nextSibling.nextSibling; // Moz. Opera
        }
        else {
            sibling = Element.nextSibling; // IE
        }
        return sibling;
    }
    catch (e) {
        //alert(e + "-" + Element);
    }
}
/**********ȡĳԪ��ǰ���Ԫ�أ�δ�⣩***************/
function getPreObj(Element) {
        try {
            if (Element.previousSibling.nodeType == 3) {
                sibling = Element.previousSibling.previousSibling; // Moz. Opera
            }
            else {
                sibling = Element.previousSibling; // IE
            }
            return sibling;
        }
        catch (e) {
            alert(e + "-" + Element);
        }
    }

/**********ȡĳԪ�غ����Ԫ��***************/
    function getNextObjByID(id) {
        try {
            var obj = document.getElementById(id);
            sibling = getNextObj(obj);
            return sibling;
        }
        catch (e) {
            alert(e + "-" + id);
        }
    }

    /**************ȡ�õ�һ����Ԫ��***********/
    function getFirstChild(obj) {
        //obj.childNodesΪȫ����Ԫ��
        for (i = 0; i < obj.childNodes.length; i++) {
            if (obj.childNodes[i].nodeType == 1)
                return obj.childNodes[i];
            else
                continue;
        }
    }

    /**************ȡ�õڶ�����Ԫ��***********/
    function getSecChild(obj) {
        var firstChild = getFirstChild(obj);
        return getNextObj(firstChild);
    }



/********��ɾԪ��class***********/
//�÷��� Share.addClassName($("topDv"), "lan12");          //����class
//       Share.removeClassName($("topDv"), "lan12");	   //ɾ��class

function Share() { }

Share.hasClassName = function(element, className) {
    if (!element) return;
    //alert(element.innerHTML);
    var elementClassName = element.className;
    if (elementClassName == undefined || elementClassName.length == 0) return false;
    //��������ʽ�ж϶��class֮���Ƿ����������class��ǰ��ո�Ĵ���
    if (elementClassName == className || elementClassName.match(new RegExp("(^|\\s)" + className + "(\\s|$)")))
        return true;
    return false;
}

Share.addClassName = function(element, className) {
    if (!element) return;
    var elementClassName = element.className;
    if (elementClassName.length == 0) {
        element.className = className;
        return;
    }
    if (elementClassName == className || elementClassName.match(new RegExp("(^|\\s)" + className + "(\\s|$)")))
        return;
    element.className = elementClassName + " " + className;
}

Share.removeClassName = function(element, className) {
    if (!element) return;
    var elementClassName = element.className;
    if (elementClassName.length == 0) return;
    if (elementClassName == className) {
        element.className = "";
        return;
    }
    if (elementClassName.match(new RegExp("(^|\\s)" + className + "(\\s|$)")))
        element.className = elementClassName.replace((new RegExp("(^|\\s)" + className + "(\\s|$)")), " ");
}



/********��ȡ������X���룬Y����***********/
//�÷��� getScrollX();          //��ȡX����������ľ���
//			 getScrollY();		  //��ȡY����������ľ���

function getScrollX()
{
	return getScrollXY()[0];
}
function getScrollY()
{
	return getScrollXY()[1];
}

function getScrollXY() 
{ 
	var scrOfX = 0, scrOfY = 0; 
	if( typeof( window.pageYOffset ) == 'number' ) 
	{ 
		//Netscape compliant 
		scrOfY = window.pageYOffset; 
		scrOfX = window.pageXOffset; 
	} 
	else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) )
	{ 
		//DOM compliant 
		scrOfY = document.body.scrollTop; 
		scrOfX = document.body.scrollLeft; 
	} 
	else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) )
	{ 
		//IE6 standards compliant mode 
		scrOfY = document.documentElement.scrollTop; 
		scrOfX = document.documentElement.scrollLeft; 
	} 
	return  [ scrOfX, scrOfY ]; 
}


/********��ȡҳ��Ԫ��X��Y �ľ������꣨����������ԭ�㣩 ***********/
//�÷��� getX($("topDv"));      //��ȡtopDv X����ֵ����
//			 getY($("topDv"));	  //��ȡtopDv Y����ֵ����
//�÷��� getXbyName("topDv");      //��ȡtopDv X����ֵ����
//			 getYbyName("topDv");	  //��ȡtopDv Y����ֵ����


function getX(element)
{
	return getXY(element)[0];
}
function getY(element)
{
	return getXY(element)[1];
}

function getXbyName(elementName)
{
	return getXY($(elementName))[0];
}
function getYbyName(elementName)
{
	return getXY($(elementName))[1];
}
function getXY(element) 
{            
	var X= element.getBoundingClientRect().left+getScrollX();            
	var Y= element.getBoundingClientRect().top+getScrollY();           
	return  [ X, Y ]; 
} 

/********��ʾ�����صڼ��С��ڼ��� ***********/
//�÷���ǰ̨����
/*
<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="1">    
  
<TR> <TD width="20%">00</TD> <TD width="20%" bgcolor="#E3E3E3">01</TD> <TD width="20%">02</TD></TR>    
<TR> <TD width="20%" bgcolor="red">10</TD> <TD width="20%" bgcolor="green">11</TD> <TD width="20%" bgcolor="red">12</TD></TR>    
<TR> <TD width="20%">20</TD> <TD width="20%" bgcolor="#E3E3E3">21</TD> <TD width="20%">22</TD></TR>    
</TABLE> </P>    
<INPUT id="btnHiddenCol" type="button" value="��ʾ/���ص�2��" name="btnHiddenCol" onclick="setHiddenCol(document.getElementById('Table1'),1)">    
<INPUT id="btnHiddenRow" type="button" value="��ʾ/���ص�2��" name="btnHiddenRow" onclick="setHiddenRow(document.getElementById('Table1'),1)">   
*/

function setHiddenCol(oTable,iCol)  
{    
    for (i=0;i < oTable.rows.length ; i++)    
     {    
         oTable.rows[i].cells[iCol].style.display = oTable.rows[i].cells[iCol].style.display=="none"?"block":"none";    
  }    
}    
function setHiddenRow(oTable,iRow)
{    
     oTable.rows[iRow].style.display = oTable.rows[iRow].style.display == "none"?"block":"none";    
}   







/********����DomԪ�أ������ڷ�table��ķ�Ƕ�׹���Ԫ�����ɣ��������壺��ǩ�������ԡ���ʽ���ڲ�html���� ***********/
//�÷���
/*
	function CreateElementComtest()
	{
	 var attr={"name":"myname","id":"myid"};
	 var style={"width":"500px","height":"400px","border":"1px solid purple"};
	 var html="this is my text";
	 CreateElementCom("div",attr,style,html);
} 
��ͳģʽ����Div��
function CreateTable()//��ͳ�ķ�ʽ����̬���һ��DIV
{
 var newElement = document.createElement('div'); 
     var newText = document.createTextNode('�����½��� div �е����֡�'); 
     document.getElementById("mydiv").appendChild(newElement); //©����һ�䣬�����в�ͨ 
     newElement.id = 'newDiv'; 
     newElement.className = 'newDivClass'; 
     newElement.setAttribute('name ','newDivName'); 
     newElement.style.width = '300px'; 
     newElement.style.height = '200px'; 
     newElement.style.margin = '0 auto'; 
     newElement.style.border = '1px solid #DDD'; 
     newElement.appendChild(newText); 
} 
*/

//����DomԪ�أ�Ĭ��ֱ�Ӽ���body���棻�������壺��ǩ�������ԡ���ʽ���ڲ�html����,�����ڷ�table��ķ�Ƕ�׹���Ԫ������
function CreateElementComBody(type, attr, style, html) { //2011-7-9 2:07:28 �����������溯����������ͬ��js����������ʹ������ͬ
    var element = document.createElement(type);
    document.body.appendChild(element);
    for (var k in attr) {
        element.setAttribute(k, attr[k]);
    }
    for (var k in style) {
        element.style[k] = style[k];
    }

    element.appendChild(document.createTextNode(html));
    
    
    
}
//��ĳ��Ԫ���ڲ�����ĳԪ�أ������溯����һ������
function CreateElementCom(fatherObjName,type, attr, style, html) {
    var element = document.createElement(type);
    $(fatherObjName).appendChild(element);
    for (var k in attr) {
        element.setAttribute(k, attr[k]);
    }
    for (var k in style) {
        element.style[k] = style[k];
    }
    element.innerHTML  = html;
}
//��������Ϸ������������text������html
function CreateElementComText(fatherObjName, type, attr, style, text) {
    var element = document.createElement(type);
    $(fatherObjName).appendChild(element);
    for (var k in attr) {
        element.setAttribute(k, attr[k]);
    }
    for (var k in style) {
        element.style[k] = style[k];
    }
    element.appendChild(document.createTextNode(text));
}




/********��̬���TABLE���� ,����1��CreateTable(),�Ѿ����ڱ����С��У�ֻ������ݣ���������CreateTableAll()ȫ������ ***********/
/*�÷��� ǰ̨���룺
<table id="tb" border=1>
 <tr>
  <td>id</td>
  <td>name</td>
  <td>age</td>
  <td>operate</td>
 </tr>
</table>
<a href="javascript:CreateTable()" title="">������</a>
*/
//����һ�����ڱ���������̬���ɸ�������
function CreateTable()
{
 var tbl=document.getElementById("tb");
 var row=tbl.insertRow(tbl.rows.length);
 
 var cell1=row.insertCell(row.cells.length);
 var cell2=row.insertCell(row.cells.length);
 var cell3=row.insertCell(row.cells.length);
 var cell4=row.insertCell(row.cells.length);
 cell1.innerHTML="a";
 cell2.innerHTML="b";
 cell3.innerHTML="c";
 cell4.innerHTML="<a href='javascript:deleteRow("+(tbl.rows.length-1)+")'>delete</a>";
 
 cell1.setAttribute("width","150px");
 cell2.setAttribute("width","150px");
 cell3.setAttribute("width","150px");
 cell4.setAttribute("width","150px");
 //row.style.setAttribute("backgroundColor","#e0e0e0");
 row.style["backgroundColor"]="#e0e0e0"; //�ڶ���������ʽ�ķ���
 tbl.style.setAttribute("backgroundColor","olive");  
 tbl.setAttribute("width","500px");  
 //alert(row.style["backgroundColor"]);
}
function leo()//�ض��±�
{
 var tbl=document.getElementById("tb");
 for(var k=0;k<tbl.rows.length;k++)
 {
  tbl.rows[k].cells[3].innerHTML="<a href='javascript:deleteRow("+k+")'>delete</a>";
 }
}
function deleteRow(obj)//ɾ��ָ������
{
 var tbl=document.getElementById("tb");
 tbl.deleteRow(obj);
 leo();
} 
//�����������ڱ���������̬���ɸ�������
function CreateTableAll(){ 
	  var attr={"name":"myname","id":"myid"};
	 var style={"width":"500px","height":"400px","border":"1px solid purple"};
	 var html="this is my text";
	 CreateElementCom("div",attr,style,html);
	  var div = document.getElementById("myid"); 
     var table = document.createElement("table");//����table 
     var row = table.insertRow();//����һ�� 
     var cell = row.insertCell();//����һ����Ԫ 
     cell.width = "150";//����cell�ĸ������� 
     cell.style.backgroundColor = "#999999"; 
     cell.innerHTML="�����";
	 cell.onmouseover=function()	  //�����¼�
	{
	   this.style.backgroundColor='#e1e8fb'
	   alert(this.innerHTML);
	}

     cell = row.insertCell();//����һ����Ԫ 
     cell.width = "150";//����cell�ĸ������� 
     cell.style.backgroundColor = "#1f9"; 
     cell.innerHTML="�ǳ��ã�"; 
     div.appendChild(table); 
   
 } 






/********ȡ��URL����ֵ������������� ***********/
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


/********��ʾ�����С��� ***********/

//�����У�oTable:table���ƣ�iCol����ʾ�ڼ��У�from���ӵڼ��п�ʼ����(���ǲ���Ҫ���ص�ǰ���У�
function setHiddenCol(oTable, iCol, From) {
    oTable = $(oTable);
    for (i = 0; i < oTable.rows.length; i++) {
        for (j = parseInt(From); j < oTable.rows[i].cells.length; j++) {//ǰ���в�����
            oTable.rows[i].cells[j].style.display = "none";
            oTable.rows[i].cells[iCol].style.display = "block";
        }
    }
}


//���ؿհ������У�oTable:table���ƣ�whichCol���ڼ���ƥ���ֵ��
function setHiddenRows(oTable, whichCol) {
    oTable = $(oTable);
    for (i = 0; i < oTable.rows.length; i++) {
        //alert(oTable.rows[i].cells[whichCol].innerText);
        if (oTable.rows[i].cells[whichCol].innerText == "") {
            oTable.rows[i].style.display = "none";
        }
    }
}
/*�������������ء���ʾ ��/��
function setHiddenCol(oTable, iCol)
{

    for (i = 0; i < oTable.rows.length; i++) {
        oTable.rows[i].cells[iCol].style.display = oTable.rows[i].cells[iCol].style.display == "none" ? "block" : "none";
    }
}
function setHiddenRow(oTable, iRow)
{
    oTable.rows[iRow].style.display = oTable.rows[iRow].style.display == "none" ? "block" : "none";
}
*/






/********************   Ajax���� *******************************/
//�÷��� Aget($("topDv"));      //

//�÷��� getXbyName("topDv");      //

function Aget(url) {
    Ajax_Get_xml(url, "text");
}
function Apost(url, postStr) {
    Ajax_Post_xml(url, "text", postStr);
}
//����֮�ǲ�ѯajax
function Ajax_Post_Send(url, postStr) {
    if (url.indexOf("?") > 0)
    { url += "&randnum=" + Math.random(); }
    else
    { url += "?randnum=" + Math.random(); }
    //var postStr = "user_name=" + escape(userName) + "&user_age=" + userAge + "&user_sex=" + userSex;
    ajax.open("post", url, false); //��������ʽ�������ļ����첽����
    ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded;charset=gb2312");
    ajax.send(postStr);
    xmlData = ajax.responseText;
    if (xmlData == "yes") {
        //alert("�޸ĳɹ���");
        //history.go(0);
        //���ڸ����б�
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("TrueSearch").click();

    }
    else {
        alert("�޸�ʧ�ܣ�");
        //history.go(0);
        //���ڸ����б�
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("TrueSearch").click();
    }
}

var ajax = Ajax_xmlhttp(); //��xmlhttprequest����ֵ��һ������
function Ajax_xmlhttp() {
    //��IE�д���xmlhttpRequest,������IE5.0�������а汾
    var msXmlhttp = new Array("Msxml2.XMLHTTP.5.0", "Msxml2.XMLHTTP.4.0", "Msxml2.XMLHTTP.3.0", "Msxml2.XMLHTTP", "Microsoft.XMLHTTP");
    for (var i = 0; i < msXmlhttp.length; i++) {
        try {
            _xmlhttp = new ActiveXObject(msXmlhttp[i]);
        }
        catch (e) {
            _xmlhttp = null;
        }
    }
    //�����IE��������򴴽�����FireFox���������xmlhttpRequest 
    if (!_xmlhttp && typeof XMLHttpRequest != "undefined") {
        _xmlhttp = new XMLHttpRequest();
    }
    return _xmlhttp;
}
function Ajax_Get_xml(url, backType) {
    if (url.indexOf("?") > 0)
    { url += "&randnum=" + Math.random(); }
    else
    { url += "?randnum=" + Math.random(); }
    ajax.open("get", url, false); //��������ʽ�������ļ����첽����
    ajax.send(null);
    xmlData = ajax.responseText;
    if (xmlData == "yes") {
        alert("�޸ĳɹ���");
        history.go(0);
    }
    else {
        alert("�޸�ʧ�ܣ�");
        history.go(0);
    }
}
//����xml���������䡣����todo
function Ajax_Post_xml(url, backType, postStr) {
    if (url.indexOf("?") > 0)
    { url += "&randnum=" + Math.random(); }
    else
    { url += "?randnum=" + Math.random(); }
    //var postStr = "user_name=" + escape(userName) + "&user_age=" + userAge + "&user_sex=" + userSex;
    ajax.open("post", url, false); //��������ʽ�������ļ����첽����
    ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded;charset=gb2312");
    ajax.send(postStr);
    xmlData = ajax.responseText;
    if (xmlData == "yes") {
        //alert("�޸ĳɹ���");
        //history.go(0);
        //���ڸ����б�
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("BtnSearch").click();
        
    }
    else {
        alert("�޸�ʧ�ܣ�");
        //history.go(0);
        //���ڸ����б�
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("BtnSearch").click();
    }
}







/********************   �Ƚ�ʱ�䷽�� *******************************/
//�÷��� compTime("09:01:00","21:12:23");   ����false
//����True��false

function compTime(t1, t2) {
    var res = false;
    if (parseInt(t1.replace(":", "")) > parseInt(t2.replace(":", ""))) {
        res = true;
    }
    return res;
}



/********************   ���ֲ� *******************************/
//�÷��� onclick="MaskDivShow()" ��ס����ҳ��,����������
//���أ�

function MaskDivShow() {
    if (!document.getElementById("Masker")) {
        var attr = { "id": "Masker", "name": "Masker" };
        var style = { };
        var html = "";
        CreateElementComBody("Div", attr, style, html);
    }
    else {
        document.getElementById("Masker").style.display = "";
    }

}
function MaskDivHiden() {
    if (document.getElementById("Masker")) {
        document.getElementById("Masker").style.display = "none";
    }

}
/********************   �����߼������� *******************************/
function AdvSearchStart() {
    MaskDivShow();
    document.getElementById("AdvSearch").style.display = "block";
    
}
function AdvSearchClose() {
    MaskDivHiden();
    document.getElementById("AdvSearch").style.display = "none";

}


/************�ַ�������**************/
//ȥ���ַ���ͷ����β�ո�
//�÷���" a12b ".Trim() ���Ϊ"a12b"
String.prototype.Trim = function() {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
//ȥ���ַ���ͷ�ո�
String.prototype.LTrim = function() {
    return this.replace(/(^\s*)/g, "");
}
//ȥ���ַ���β�ո�
String.prototype.RTrim = function() {
    return this.replace(/(\s*$)/g, "");
}
