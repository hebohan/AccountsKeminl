//js基础操作（公用）
//youqing 2011-3-29 23:14:33


/**********取元素***************/
//用法： $("topDv")

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


/**********取某元素后面的元素***************/
//用法： getNextObj("topDv") topDv为当前元素ID

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
/**********取某元素前面的元素（未测）***************/
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

/**********取某元素后面的元素***************/
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

    /**************取得第一个子元素***********/
    function getFirstChild(obj) {
        //obj.childNodes为全部子元素
        for (i = 0; i < obj.childNodes.length; i++) {
            if (obj.childNodes[i].nodeType == 1)
                return obj.childNodes[i];
            else
                continue;
        }
    }

    /**************取得第二个子元素***********/
    function getSecChild(obj) {
        var firstChild = getFirstChild(obj);
        return getNextObj(firstChild);
    }



/********增删元素class***********/
//用法： Share.addClassName($("topDv"), "lan12");          //增加class
//       Share.removeClassName($("topDv"), "lan12");	   //删除class

function Share() { }

Share.hasClassName = function(element, className) {
    if (!element) return;
    //alert(element.innerHTML);
    var elementClassName = element.className;
    if (elementClassName == undefined || elementClassName.length == 0) return false;
    //用正则表达式判断多个class之间是否存在真正的class（前后空格的处理）
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



/********获取滚动条X距离，Y距离***********/
//用法： getScrollX();          //获取X轴滚动滚动的距离
//			 getScrollY();		  //获取Y轴滚动滚动的距离

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


/********获取页面元素X，Y 的绝对坐标（相对于浏览器原点） ***********/
//用法： getX($("topDv"));      //获取topDv X绝对值坐标
//			 getY($("topDv"));	  //获取topDv Y绝对值坐标
//用法： getXbyName("topDv");      //获取topDv X绝对值坐标
//			 getYbyName("topDv");	  //获取topDv Y绝对值坐标


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

/********显示、隐藏第几行、第几列 ***********/
//用法：前台代码
/*
<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="1">    
  
<TR> <TD width="20%">00</TD> <TD width="20%" bgcolor="#E3E3E3">01</TD> <TD width="20%">02</TD></TR>    
<TR> <TD width="20%" bgcolor="red">10</TD> <TD width="20%" bgcolor="green">11</TD> <TD width="20%" bgcolor="red">12</TD></TR>    
<TR> <TD width="20%">20</TD> <TD width="20%" bgcolor="#E3E3E3">21</TD> <TD width="20%">22</TD></TR>    
</TABLE> </P>    
<INPUT id="btnHiddenCol" type="button" value="显示/隐藏第2列" name="btnHiddenCol" onclick="setHiddenCol(document.getElementById('Table1'),1)">    
<INPUT id="btnHiddenRow" type="button" value="显示/隐藏第2行" name="btnHiddenRow" onclick="setHiddenRow(document.getElementById('Table1'),1)">   
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







/********生成Dom元素，适用于非table外的非嵌套关联元素生成；参数含义：标签名、属性、样式、内部html代码 ***********/
//用法：
/*
	function CreateElementComtest()
	{
	 var attr={"name":"myname","id":"myid"};
	 var style={"width":"500px","height":"400px","border":"1px solid purple"};
	 var html="this is my text";
	 CreateElementCom("div",attr,style,html);
} 
传统模式生成Div：
function CreateTable()//传统的方式来动态添加一个DIV
{
 var newElement = document.createElement('div'); 
     var newText = document.createTextNode('这是新建立 div 中的文字。'); 
     document.getElementById("mydiv").appendChild(newElement); //漏了这一句，否则行不通 
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

//生成Dom元素，默认直接加在body下面；参数含义：标签名、属性、样式、内部html代码,适用于非table外的非嵌套关联元素生成
function CreateElementComBody(type, attr, style, html) { //2011-7-9 2:07:28 更名，与下面函数名不能相同（js不允许），即使参数不同
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
//在某个元素内部增加某元素，比上面函数多一个参数
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
//相对于以上方法，传入的是text，不是html
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




/********动态添加TABLE内容 ,方法1：CreateTable(),已经存在标题行、列，只添加数据，方法二：CreateTableAll()全部定义 ***********/
/*用法： 前台代码：
<table id="tb" border=1>
 <tr>
  <td>id</td>
  <td>name</td>
  <td>age</td>
  <td>operate</td>
 </tr>
</table>
<a href="javascript:CreateTable()" title="">生成列</a>
*/
//方法一：存在标题栏，动态生成各数据列
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
 row.style["backgroundColor"]="#e0e0e0"; //第二种设置样式的方法
 tbl.style.setAttribute("backgroundColor","olive");  
 tbl.setAttribute("width","500px");  
 //alert(row.style["backgroundColor"]);
}
function leo()//重定下标
{
 var tbl=document.getElementById("tb");
 for(var k=0;k<tbl.rows.length;k++)
 {
  tbl.rows[k].cells[3].innerHTML="<a href='javascript:deleteRow("+k+")'>delete</a>";
 }
}
function deleteRow(obj)//删除指定的行
{
 var tbl=document.getElementById("tb");
 tbl.deleteRow(obj);
 leo();
} 
//方法二：存在标题栏，动态生成各数据列
function CreateTableAll(){ 
	  var attr={"name":"myname","id":"myid"};
	 var style={"width":"500px","height":"400px","border":"1px solid purple"};
	 var html="this is my text";
	 CreateElementCom("div",attr,style,html);
	  var div = document.getElementById("myid"); 
     var table = document.createElement("table");//创建table 
     var row = table.insertRow();//创建一行 
     var cell = row.insertCell();//创建一个单元 
     cell.width = "150";//更改cell的各种属性 
     cell.style.backgroundColor = "#999999"; 
     cell.innerHTML="你好吗？";
	 cell.onmouseover=function()	  //增加事件
	{
	   this.style.backgroundColor='#e1e8fb'
	   alert(this.innerHTML);
	}

     cell = row.insertCell();//创建一个单元 
     cell.width = "150";//更改cell的各种属性 
     cell.style.backgroundColor = "#1f9"; 
     cell.innerHTML="非常好！"; 
     div.appendChild(table); 
   
 } 






/********取得URL参数值，传入参数名称 ***********/
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


/********显示定制列、行 ***********/

//隐藏列；oTable:table名称，iCol：显示第几列，from：从第几列开始隐藏(既是不需要隐藏的前几列）
function setHiddenCol(oTable, iCol, From) {
    oTable = $(oTable);
    for (i = 0; i < oTable.rows.length; i++) {
        for (j = parseInt(From); j < oTable.rows[i].cells.length; j++) {//前三列不隐藏
            oTable.rows[i].cells[j].style.display = "none";
            oTable.rows[i].cells[iCol].style.display = "block";
        }
    }
}


//隐藏空白数据行；oTable:table名称，whichCol：第几列匹配空值，
function setHiddenRows(oTable, whichCol) {
    oTable = $(oTable);
    for (i = 0; i < oTable.rows.length; i++) {
        //alert(oTable.rows[i].cells[whichCol].innerText);
        if (oTable.rows[i].cells[whichCol].innerText == "") {
            oTable.rows[i].style.display = "none";
        }
    }
}
/*其他方法：隐藏、显示 行/列
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






/********************   Ajax方法 *******************************/
//用法： Aget($("topDv"));      //

//用法： getXbyName("topDv");      //

function Aget(url) {
    Ajax_Get_xml(url, "text");
}
function Apost(url, postStr) {
    Ajax_Post_xml(url, "text", postStr);
}
//服务之星查询ajax
function Ajax_Post_Send(url, postStr) {
    if (url.indexOf("?") > 0)
    { url += "&randnum=" + Math.random(); }
    else
    { url += "?randnum=" + Math.random(); }
    //var postStr = "user_name=" + escape(userName) + "&user_age=" + userAge + "&user_sex=" + userSex;
    ajax.open("post", url, false); //设置请求方式，请求文件，异步请求
    ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded;charset=gb2312");
    ajax.send(postStr);
    xmlData = ajax.responseText;
    if (xmlData == "yes") {
        //alert("修改成功！");
        //history.go(0);
        //用在更新列表
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("TrueSearch").click();

    }
    else {
        alert("修改失败！");
        //history.go(0);
        //用在更新列表
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("TrueSearch").click();
    }
}

var ajax = Ajax_xmlhttp(); //将xmlhttprequest对象赋值给一个变量
function Ajax_xmlhttp() {
    //在IE中创建xmlhttpRequest,适用于IE5.0以上所有版本
    var msXmlhttp = new Array("Msxml2.XMLHTTP.5.0", "Msxml2.XMLHTTP.4.0", "Msxml2.XMLHTTP.3.0", "Msxml2.XMLHTTP", "Microsoft.XMLHTTP");
    for (var i = 0; i < msXmlhttp.length; i++) {
        try {
            _xmlhttp = new ActiveXObject(msXmlhttp[i]);
        }
        catch (e) {
            _xmlhttp = null;
        }
    }
    //如果非IE浏览器，则创建基于FireFox等浏览器的xmlhttpRequest 
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
    ajax.open("get", url, false); //设置请求方式，请求文件，异步请求
    ajax.send(null);
    xmlData = ajax.responseText;
    if (xmlData == "yes") {
        alert("修改成功！");
        history.go(0);
    }
    else {
        alert("修改失败！");
        history.go(0);
    }
}
//返回xml方法待补充。。。todo
function Ajax_Post_xml(url, backType, postStr) {
    if (url.indexOf("?") > 0)
    { url += "&randnum=" + Math.random(); }
    else
    { url += "?randnum=" + Math.random(); }
    //var postStr = "user_name=" + escape(userName) + "&user_age=" + userAge + "&user_sex=" + userSex;
    ajax.open("post", url, false); //设置请求方式，请求文件，异步请求
    ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded;charset=gb2312");
    ajax.send(postStr);
    xmlData = ajax.responseText;
    if (xmlData == "yes") {
        //alert("修改成功！");
        //history.go(0);
        //用在更新列表
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("BtnSearch").click();
        
    }
    else {
        alert("修改失败！");
        //history.go(0);
        //用在更新列表
        document.getElementById("ClickFenye").value = "1";
        document.getElementById("BtnSearch").click();
    }
}







/********************   比较时间方法 *******************************/
//用法： compTime("09:01:00","21:12:23");   返回false
//返回True，false

function compTime(t1, t2) {
    var res = false;
    if (parseInt(t1.replace(":", "")) > parseInt(t2.replace(":", ""))) {
        res = true;
    }
    return res;
}



/********************   遮罩层 *******************************/
//用法： onclick="MaskDivShow()" 盖住整个页面,含滚动条下
//返回：

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
/********************   弹出高级搜索框 *******************************/
function AdvSearchStart() {
    MaskDivShow();
    document.getElementById("AdvSearch").style.display = "block";
    
}
function AdvSearchClose() {
    MaskDivHiden();
    document.getElementById("AdvSearch").style.display = "none";

}


/************字符串处理**************/
//去掉字符开头、结尾空格
//用法：" a12b ".Trim() 结果为"a12b"
String.prototype.Trim = function() {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
//去掉字符开头空格
String.prototype.LTrim = function() {
    return this.replace(/(^\s*)/g, "");
}
//去掉字符结尾空格
String.prototype.RTrim = function() {
    return this.replace(/(\s*$)/g, "");
}
