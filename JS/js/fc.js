/**
*值比较
*返回值：true or false
*增加人：李宁
*/
function compareValue(comvalue1,operator,comvalue2,datatype)
{ 
   	
   if(datatype == "number"){
     	
    var  value1 = parseFloat(comvalue1)
    var  value2 = parseFloat(comvalue2)
   }
   else if(datatype == "date"){
   
     if(comvalue1.isDate()&&comvalue2.isDate()){
     	var value1 = comvalue1
        var value2 = comvalue2  		
     
     }else
       return false; 
   }
   else{
     	
     var value1 = comvalue1
     var value2 = comvalue2 
   }
  	
   if(operator=="=")
     if(value1==value2)
       return true;
     else
       return false;
   if(operator=="<>")
     if(value1!=value2)
       return true;
     else
       return false;
   if(operator=="<")
     if(value1<value2)
       return true;
     else
       return false;  
   if(operator=="<=")
     if(value1<=value2)
       return true;
     else
       return false; 
   if(operator==">")
     if(value1>value2)
       return true;
     else
       return false; 
   if(operator==">=")
     if(value1>=value2)
       return true;
     else
       return false; 
   if(operator=="isnull")
     if(value1==null||value1=="")
       return true;
     else
       return false; 
   if(operator=="notnull")
     if(value1==null||value1=="")
       return false;
     else
       return true; 
   if(operator=="in")
     if(value2.indexOf(value1)!="-1")
       return true;
     else
       return false; 
   if(operator=="notin")
     if(value2.indexOf(value1)=="-1")
       return true;
     else
       return false; 
   if(operator=="beginwith")
     if(value1.indexOf(value2)=="0")
       return true;
     else
       return false; 
   if(operator=="notin")
     if(value1.indexOf(value2)!="0")
       return true;
     else
       return false;                                        
}


/**
*动态行增加一行
* 参数: tableObj
*/
function addrow(tableObjname,rowno)
{ var tableObj = document.all.item(tableObjname);

 if(tableObj==null||tableObj.tagName!="TABLE") return;
      var tr=tableObj.rows[1];
      if(tr!=null)
      {
      	
      	for(j=1;j<=rowno;j++){
	try
	{
	  var ntr=tableObj.insertRow();
          for(var i=0;i<tr.cells.length;i++)
	  {
	     var otd=ntr.insertCell();
	     otd.innerHTML=tr.cells[i].innerHTML;
	     otd.children[0].value="";
	     otd.children[0].checked=false;
	     otd.width=tr.cells[i].width;
	     otd.index=tr.cells[i].index;
	     otd.style.display=tr.cells[i].style.display;
	  }
	
	
	}  catch(ex)
	   {
	      alert("增加一行失败！"+ex.description);
	      return;
	   }
	}
       }	
	
	
}

/**
*动态行删除一行
* 参数: tableObj
*/
function delrow(tableObj)
{ 
 if(tableObj==null||tableObj.tagName!="TABLE") return;

   for(var i=2;i<tableObj.rows.length;i++){
     var otd = tableObj.rows[i].cells[0];
     if (otd.children[0].checked ) {
	try{
	 tableObj.deleteRow(i);
	 i=i-1;
	}  catch(ex)
	   {
	      alert(" 删除一行失败！"+ex.description);
	      return;
          }
       }   	   
    }
	
}
/**
*动态行增加一行
* 参数: tableObj
*/
function addrow2(tableObj)
{
 if(tableObj==null||tableObj.tagName!="TABLE") return;
      var tr=tableObj.rows[0];
      if(tr!=null)
      {
	try
	{
	 // tableObj.rows[1].style.display="block";
	 var ntr=tableObj.insertRow();
	 ntr.onmouseover  = tr.onmouseover ;
	  ntr.onmouseout  = tr.onmouseout ;
          for(var i=0;i<tr.cells.length;i++)
	  {
	     var otd=ntr.insertCell();
	     otd.innerHTML=tr.cells[i].innerHTML;
	     otd.width=tr.cells[i].width;
	     otd.index=tr.cells[i].index;
	     otd.style.display=tr.cells[i].style.display;
	     otd.style.borderStyle=tr.cells[i].style.borderStyle;
	      otd.style.borderWidth=tr.cells[i].style.borderWidth;
	  } 
	}  catch(ex)
	   {
	      alert("增加一行失败！"+ex.description);
	      return;
	   }
	}
}
//currRowIndex 选定行
var currRowIndex = 0;
var currDivTable ;
//选定指定行
function SelectRow(objTD)
{
var objTR =objTD.parentElement;
currRowIndex = objTR.rowIndex;
currDivTable =  objTR.parentElement;
}


/**
*动态行删除一行
* 参数: tableObj
*/
function delrow2(tableObj)
{ 
 if(tableObj==null||tableObj.tagName!="TABLE") return;

     if(true )	{
	try{

	 tableObj.deleteRow(currRowIndex);
	 currRowIndex = 0;
	}  catch(ex)
	   {
	      alert(" 删除失败！"+ex.description);
	      return;
          }
       }   	   
}	

function changestyle(Field,ifhidden,ifitalic,fontweight,Color,bkcolor){
  if(ifhidden==1){ 
    var oldhidden= Field.getAttribute("oldhidden")
    if(oldhidden==null){	
      oldhidden = Field.style.hidden
      Field.setAttribute("oldhidden",oldhidden)	
     
    }
    Field.style.display='none'
  }
  if(ifitalic==1){
   var olditalic =Field.getAttribute("olditalic")
    if(olditalic==null){ 
      olditalic = Field.style.fontStyle;
      Field.setAttribute("olditalic",olditalic)	
    }
      Field.style.fontStyle='italic'
   
  }
  if(fontweight==1){ 
    var oldfontweight= Field.getAttribute("oldfontweight")
    if(oldfontweight ==null){ 
      oldfontweight = Field.style.fontWeight;
      Field.setAttribute("oldfontweight",oldfontweight)	
      
     }
    Field.style.fontWeight='bolder'
  }
  if(Color!=""){ 
    var oldColor= Field.getAttribute("oldColor")
    if(oldColor ==null){ 
     oldColor = Field.style.color;
     Field.setAttribute("oldColor",oldColor)	
     
    }
    Field.style.color=Color
  }
  if(bkcolor!=""){ 
    var oldbkcolor= Field.getAttribute("oldbkcolor")
    if(oldbkcolor==null){ 
      oldbkcolor = Field.style.backgroundColor;
      Field.setAttribute("oldbkcolor",oldbkcolor)	
     
     }
      Field.style.backgroundColor=bkcolor
   }
}
function restorestyle(Field){
     var oldhidden =  Field.getAttribute("oldhidden")	
     
     if(oldhidden!=null) { Field.style.display=oldhidden;}
     		
     var olditalic =  Field.getAttribute("olditalic")	
     if(olditalic!=null) {Field.style.fontStyle=olditalic;}
     	
     var oldfontweight =  Field.getAttribute("oldfontweight")	
     if(oldfontweight!=null) Field.style.fontWeight=oldfontweight;
     
     var oldColor =  Field.getAttribute("oldColor")	
     if(oldColor!=null) Field.style.color=oldColor;
     
     var oldbkColor =  Field.getAttribute("oldbkcolor")	
     if(oldbkColor!=null) Field.style.backgroundColor=oldbkColor;		
}



function hideallpage(pagesdiv){
	
	for(v=0;v<document.all.item(pagesdiv).children.length;v++){
		document.all.item(pagesdiv).children[v].style.display="none";
	}
}
function showpage(pagesdiv,pagedivid){
        hideallpage(pagesdiv);
	document.all.item(pagedivid).style.display="";

	
}

function initpages(){
	var page=document.getElementsByTagName("div");
	for(i=0;i<page.length;i++){
	  if(page[i].id.indexOf("pages")==0)
	    showpage(page[i].id,document.all.item(page[i].id).children[0].id);	
	  	
	}
	var tabs=document.getElementsByTagName("table");
	for(i=0;i<tabs.length;i++){ 
	  if(tabs[i].id.indexOf("tab")==0)
	    inittabpage(tabs[i].id);	  	
	
	}
	
}
function initdynas(){
		var url = document.URL.split('?');
		var queryString;
		var isnew;
		if(url.length>1){
			queryString = url[1].split('&'); 
			for(var i=0;i<queryString.length;i++)
			{
				if(queryString[i].indexOf("Type")>-1){
					isnew = queryString[i].substring(queryString[i].indexOf("=")+1);
				}
			}
		}
		
     var dynas=document.getElementsByTagName("FIELDSET");
	for(i=0;i<dynas.length;i++){ 
	   var oType = dynas[i].getAttribute("elementType");
	   var rowno = dynas[i].getAttribute("rowno");
	   if (oType=="dyna")
	     document.all.item("table_"+dynas[i].name).rows[1].style.display="none";
	     if(isnew=="new") addrow("table_"+dynas[i].name,rowno);	
  
	}	
	
}
function doTabpageClick(tabpage)
{
var tabtab=document.all(tabpage);
for(var i=0;i<tabtab.rows[0].cells.length;i++)
{
var ootd=tabtab.rows[0].cells[i];
ootd.className="taboffcss";

}
var otd=event.srcElement;
otd.className="taboncss";

}

function inittabpage(tabpage)
{ var tabtab=document.all(tabpage);
  if  (tabtab.rows[0].cells.length==1){
  	tabtab.style.display="none"
  	return;
  }
  for(var i=0;i<tabtab.rows[0].cells.length;i++)
  {
  var ootd=tabtab.rows[0].cells[i];
  if (i==0)
    ootd.className="taboncss";
  else
    ootd.className="taboffcss";
  }
}
function openpromptwin(obj,dicname){
// var rtnValue = showModalDialog("ShowPromptWin.aspx?dicname="+dicname,"","dialogHeight:500px;dialogWidth:500px;help: no;status:no"); 
var vsRe=window.showModalDialog("../../organize/org.aspx?dic_type="+dicname+"&state=11","","toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,left=250,top=250,width=300,height=400");
if(vsRe==1){
	var rtnValue=window.showModalDialog("../../tree/tree2.aspx","","toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,left=250,top=250,width=300,height=400");
	if (rtnValue!=""&&rtnValue!=null) 
          obj.value = rtnValue;	
       }
}
function selectOrg(obj,level){
var returnType = obj.getAttribute("returntype");
var vsRe=window.showModalDialog("../../tree/Formtree.aspx?level="+level+"&returntype="+returnType +"&state=1","","toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,left=250,top=250,width=300,height=400");

if (vsRe!=""&&vsRe!=null) 
          obj.value = vsRe;	
     
}
function selectPerson(obj,level){
var returnType = obj.getAttribute("returntype");
var vsRe=window.showModalDialog("../../tree/Formtree.aspx?level="+level+"&returntype="+returnType +"&state=3","","toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,left=250,top=250,width=300,height=400");

if (vsRe!=""&&vsRe!=null) 
          obj.value = vsRe;
}

//显示重复节菜单
var divmenu = document.createElement("<div class=contextmenu>");
function showMenu(regroupname) 
{ document.body.appendChild(divmenu);

  divmenu.style.position='absolute';
  divmenu.style.width='40px';
  divmenu.style.height='50px'; 
  divmenu.innerHTML = "<A href=\"javascript:addrow2(table_" + regroupname + ")\">增加</A><BR><A href=\"javascript:delrow2(table_" + regroupname + ")\">删除</A>";
  divmenu.style.left=event.clientX;
  divmenu.style.top=event.clientY; 
  divmenu.style.visibility='visible';
  event.returnValue=false;
  event.calcelBubble=true;

} 
function document.onclick() 
{  if( divmenu != null)
     divmenu.style.visibility="hidden"; 
} 
//******************************杨常茂
function WfSubClick(field)
{
	
	var url = document.URL.split('?');
	if(url.length>1){
		//var rtnValue = showModalDialog("../../business/ActivitySelectUser.aspx?ActivityModelGuid="+ActivityModelGuid+"&ActivitySchemeGuid="+ActivitySchemeGuid+"&ActivityModelStepId="+ActivityModelStepId+"&ActivityInstanceID="+ActivityInstanceID+"&ActivityTaskBatchID="+ActivityTaskBatchID);
		var rtnValue = showModalDialog("../../business/ActivitySelectUser.aspx?"+url[1],"","dialogHeight:500px;dialogWidth:480px;help:no;status:no");
		//alert(rtnValue);
		if(rtnValue !=null && rtnValue != undefined){
			document.all(field).value=rtnValue;
			if(url.length>1){
				var actionUrl = document.Form1.action.split('?');
				var addUrl = url[1].replace(actionUrl[1],"");
				addUrl = addUrl.replace("&SaveType=save","");
				if(rtnValue == "sub"){
					document.Form1.action=document.Form1.action+"&"+addUrl; 
				}else{
					document.Form1.action=document.Form1.action+"&"+addUrl; 
				}
			}
			window.Form1.submit();
		}
	}
     
}
function lochost()
{
	var url = document.URL.split('?');
	var queryString;
	var ActivityInstanceID;
	var ActivityTaskBatchID;
	if(url.length>1){
		queryString = url[1].split('&'); 
		for(var i=0;i<queryString.length;i++)
		{
			if(queryString[i].indexOf("ActivityInstanceID")>-1){
				ActivityInstanceID = queryString[i].substring(queryString[i].indexOf("=")+1);
			}
			if(queryString[i].indexOf("ActivityTaskBatchID")>-1){
				ActivityTaskBatchID = queryString[i].substring(queryString[i].indexOf("=")+1);
			}
		}
	}
	window.location="../pages/LocalHandleEcho.aspx?BtnType=release&ActivityInstanceID="+ActivityInstanceID+"&ActivityTaskBatchID="+ActivityTaskBatchID
}
function FormSubmit(ActionName){
//alert(document.URL);
	if(check(Form1)){
		var url = document.URL.split('?');
		var queryString;
		var SubType="";
		if(url.length>1){
			queryString = url[1].split('&'); 
			for(var i=0;i<queryString.length;i++)
			{
				if(queryString[i].indexOf("Type")>-1){
					SubType = queryString[i].substring(queryString[i].indexOf("=")+1);
				}
			}
			//alert(SubType);
			if(url[1].indexOf("formname")>-1){
				document.Form1.action=(document.Form1.action).split('?')[0]+"?";
			}
			if(SubType == "new"){
				document.Form1.action=document.Form1.action+"&SaveType=save&"+url[1]+"&url="+url[0]; 
			}
			else{
				if(url[1].indexOf("SaveType")>-1){
					document.Form1.action=document.Form1.action+"&"+url[1]+"&url="+url[0]; 
				}else{
					document.Form1.action=document.Form1.action+"&SaveType=save&"+url[1]+"&url="+url[0]; 
				}
			}
			//alert(document.Form1.action);
		}
		 //加入本地操作历史信息
      PostToXmlHttp(url[1]+"&ActionName="+ActionName+"&sourcerefenceId="+document.all("docId").value,"../pages/ActionContrl.aspx");
		window.Form1.submit();
	}else{
	}
}
function OpenAudit(){
	var url = document.URL.split('?');
	if(url.length>1){
		window.open("../LocalCortrol/Opinion.aspx?"+url[1]+"&sourcerefenceId="+document.all("docId").value,"","width=500,height=400,top=140,left=280");
	}
}
function OpenAtt(attString){
	window.open("../../business/FujianManage.aspx?StrPara="+attString,"","width=500,height=400,top=140,left=280");	
}
function printdoc(){
	
		window.location ="../../FormPrint/CommonPrint.aspx?formid="+document.all("formid").value+"&docid="+document.all("docId").value;
	
}
function SelectPic(picfield){
  var filename = showModalDialog("../pages/selectpic.htm","","dialogHeight:200px;dialogWidth:300px;help:no;status:no");
  if (filename!=null && filename!=""){
   //document.all("pic_"+picfield).src=filename;
   dataObj = document.all("base64obj");
   dataObj.filename = filename;
   dataObj.encode();
   document.all(picfield).value = dataObj.encodeString;
   document.all("pic_"+picfield).src=filename;
  }
}

/*
function SubmitToWebService()
{
	if(check(Form1))
	{
		var url = document.URL.split('?');
		var queryString;
		if(url.length>1)
		{
			queryString = url[1];
			document.Form1.action="../../../LocalAction/LocalActionProxy.aspx?"+queryString;
		}
		window.Form1.submit();
	}
}

function SubmitToDll()
{
	if(check(Form1))
	{
		var url = document.URL.split('?');
		var queryString;
		if(url.length>1)
		{
			queryString = url[1];
			document.Form1.action="../../../LocalAction/LocalActionDllProxy.aspx?"+queryString;
		}
		window.Form1.submit();
	}
}
*/

