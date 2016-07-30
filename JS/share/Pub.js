
//----------------------------------------------------------
/***********************************************
弹出机构用户树
LiChun
2006-03-23
***********************************************/

//输出动态树控件（不需要调用）
document.write("<input type=\"hidden\" id=\"treeparameter\" orgroot=\"\" treetype=\"\" checktype=\"\" haschecked=\"\" hascheckedtext=\"\">");

function DynamicTree(DataToSend)
{
	var vsRe1 = window.showModalDialog("../DynamicTree/GetTreeData.aspx?" + DataToSend + "",window,"dialogwidth:220px; dialogheight:100px");
	
	if(vsRe1 == undefined)
	{
		alert("数据读取操作被用户取消！");
		return;
	}
	
	if(vsRe1.substring(0,5) == "error")
	{
		alert("错误：" + vsRe1.substring(6,vsRe1.length));
		return;
	}
	
	var parameter = "";
	
	//目录根（默认为空）
	parameter += "&orgroot=" + document.all.item("treeparameter").orgroot;
	
	//目录类型（all,org,user，默认为all）
	parameter += "&treetype=" + document.all.item("treeparameter").treetype;
	
	//选择框类型（checkbox,radio，默认为checkbox）
	parameter += "&checktype=" + document.all.item("treeparameter").checktype;
	
	var vsRe2 = window.showModalDialog("../DynamicTree/Tree.aspx?" + parameter + "",window,"dialogwidth:400px; dialogheight:450px");
	
	return vsRe2;
}

//----------------------------------------------------------

//----------------------------------------------------------
/*********************************
 * 金宏样式的人员选择框（单个单位）
 */
function JhOneOrgSelectUser(curDir,orgId) 
{
	if(curDir == null || curDir == undefined)
	{
		curDir = "";
	}
	var orgId=orgId;
	var sFeatures="dialogHeight: 500px;dialogWidth:600px;";
	var str = window.showModalDialog(curDir + "Includes/SelectUser.aspx?orgId="+orgId+"&type=Single","",sFeatures);
	return str;
}

/*********************************
 * 金宏样式的人员选择框（多个单位）
 */
function JhMultiOrgSelectUser(curDir,orgId)
{
	if(curDir == null || curDir == undefined)
	{
		curDir = "";
	}
    var orgId=orgId;
	var sFeatures="dialogHeight: 500px;dialogWidth:600px;";
	var str = window.showModalDialog(curDir + "Includes/SelectUser.aspx?orgId="+orgId+"&type=List","",sFeatures);
	return str;
}