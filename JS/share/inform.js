//设置接受人
  function setprivilege()
		{
			
			DataToSend = "treetype=org";	
			DataToSend += "&checktype=checkbox";
			DataToSend += "&haschecked=" + document.all.item("h_ReceiveMan").value + "&hascheckedtext=" + document.all.item("ReceiveMan").value;
			var rtnValue = DynamicTree(DataToSend);
			
			if(rtnValue == undefined)
			{
				return false;
			}
			
			var ss = rtnValue.split("|");
			document.all.item("h_ReceiveMan").value = ss[0];
			document.all.item("ReceiveMan").value = ss[1];
			return true;
		}
//增加一行记录
function newRow(){
  var tableObj = document.all("tbl_fujian");
  var otr = tableObj.insertRow();
  var otd = otr.insertCell();
  //otd.width="260";
  otd.innerHTML = "<INPUT type=file name='file' style='WIDTH: 456px; HEIGHT: 22px' size='56' runat=server>&nbsp;&nbsp;&nbsp;<U>&nbsp;</U><A style='CURSOR: hand' href='javascript:deleteRow("+otr.rowIndex+")'><U>删除</U></A>";

}
//删除一行记录
function deleteRow(rowno){
  
 // var Truedelete=window.confirm("是否确认删除当前文件？");
//  if(Truedelete){
    var tableObj = document.all("tbl_fujian");
    if(tableObj==null||tableObj.tagName!="TABLE") return;
	try{
	 tableObj.deleteRow(rowno);
	}  catch(ex)
	   {
	      alert(" 删除一行失败！"+ex.description);
	      return;
       }
   
 //  }
}

function getsplitid(str)
{
	var id="";
	var aaa=str.split(",");
	for(var i=0;i<aaa.length;i++)
	{
		var bbb=aaa[i].split("|");
		id=id+bbb[0]+",";
	}
	//alert(id);
	id=id.substr(0,id.length-1);
	return id;
}
function getsplitname(str)
{
	var name="";
	var aaa=str.split(",");
	for(var i=0;i<aaa.length;i++)
	{
		var bbb=aaa[i].split("|");
		name=name+bbb[1]+",";
	}
	name=name.substr(0,name.length-1);
	return name;
}

function f_change()
{
	document.all("ReceiveMan").value="";
	document.all("h_ReceiveMan").value="";
}
function f_checkAddInform()
{
	var ret=1;
	if (trim(document.all("Title").value)=="")
	{
		alert("标题不能为空!");ret=-1;return ret;
	}
//alert("Content:"+trim(document.all("Content").value));
	if (document.all("Content").value==null||trim(document.all("Content").value)=="")
	{
		alert("内容不能为空!");ret=-1;return ret;
	}
	if (trim(document.all("ReceiveMan").value)=="")
	{
		alert("接收人不能为空!");ret=-1;return ret;
	}
	return ret;

}
//去掉字串左边的空格 
function lTrim(str) 
{ 
	if (str.charAt(0) == " ") 
	{ 
		str = str.slice(1);
		str = lTrim(str);
	} 
	return str; 
} 

//去掉字串右边的空格 
function rTrim(str) 
{ 
	var iLength; 
	iLength = str.length; 
	if (str.charAt(iLength - 1) == " ") 
	{ 
		str = str.slice(0, iLength - 1);
		str = rTrim(str); 
	} 
	return str; 
} 

//去掉字串两边的空格 
function trim(str) 
{
	return lTrim(rTrim(str)); 
}