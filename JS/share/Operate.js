/// Language: JavaScript 
/// 电子监察系统的各种用户界面操作前台脚本
/// Inspur.com

// 初始化页面的单位选项
function InitDept(dept,deptView)
{
	var objDept = document.all.item(dept);
	var objDeptView = document.all.item(deptView);
	if(objDept != null && objDeptView != null)
	{
		if(objDept.value == "")
		{
			objDeptView.innerHTML = "(所有单位)";
		}
		else
		{
			objDeptView.innerHTML = objDept.value;
		}
	}
}

function InitDeptByTxt(dept,deptView)
{
	var objDept = document.all.item(dept);
	var objDeptView = document.all.item(deptView);
	if(objDept != null && objDeptView != null)
	{
		if(objDept.value == "")
		{
			objDeptView.value = "(所有单位)";
		}
		else
		{
			objDeptView.value = objDept.value;
		}
	}
}

// 初始化页面的日期选项
function InitDate(date,dateView)
{
	var objDate = document.all.item(date);
	var objDateView = document.all.item(dateView);
	if(objDate != null && objDateView != null)
	{
		if(objDate.value == "")
		{
			objDateView.innerHTML = "(不限)";
		}
		else
		{
			var dateValue = objDate.value;
			if(dateValue.length > 10)
			{
				dateValue = dateValue.replace("~"," 至 ");
			}
			else if(dateValue.length == 7)
			{
				dateValue = dateValue.replace("-","年");
				dateValue = dateValue + "月";
			}
			else if(dateValue.length == 4)
			{
				dateValue = dateValue + "年";
			}
			
			objDateView.innerHTML = dateValue;
		}
	}
}

function InitDateByTxt(date,dateTxt)
{
	var objDate = document.all.item(date);
	var objDateTxt = document.all.item(dateTxt);
	if(objDate != null && objDateTxt != null)
	{
		if(objDate.value == "")
		{
			objDateTxt.value = "(不限)";
		}
		else
		{
			var dateValue = objDate.value;
			if(dateValue.length > 10)
			{
				dateValue = dateValue.replace("~"," 至 ");
			}
			else if(dateValue.length == 7)
			{
				dateValue = dateValue.replace("-","年");
				dateValue = dateValue + "月";
			}
			else if(dateValue.length == 4)
			{
				dateValue = dateValue + "年";
			}
			
			objDateTxt.value = dateValue;
		}
	}
}
//刷新页面的时候绑定下拉列表
function InitList(deptName,deptCode,list,listOneName)
{
		if(document.all.item(deptName).value=='所有单位')
			{
				document.all.item(list).selectedIndex = 0;
			}
			else
			{
				if(document.all.item(deptCode).value != "")
				{
				document.all.item(list).value = document.all.item(deptCode).value;
				}
			}
}
// 初始化页面的查询选项下拉按钮
function InitOption(txtOptionControl,tblOption,showControl)
{
	objControl = document.all.item(txtOptionControl);
	objOption = document.all.item(tblOption);
	if(objControl.value == "true")
	{
		objControl.className = "OptionControlMinus";
		objControl.value = "true";
		objControl.title = "收起查询选项";
		objOption.style.display = "block";
		if(showControl != undefined)
		{
			document.all(showControl).style.display = "block";
		}
	}
	else
	{
		objControl.className = "OptionControlPlus";
		objControl.value = "false";
		objControl.title = "展开查询选项";
		objOption.style.display = "none";
		if(showControl != undefined)
		{
			document.all(showControl).style.display = "none";
		}
	}
}

//鼠标移动事件
function MoveMouse(tabnumber)
{	
	if(document.all.item("tab" + tabnumber).className != "TabSelected")
	{
		document.all.item("tab" + tabnumber).className = "TabMoveMouse";
	}		
}
//鼠标移动事件
function MoveOut(tabnumber)
{	
	if(document.all.item("tab" + tabnumber).className == "TabSelected")
	{
		document.all.item("tab" + tabnumber).className = "TabSelected";
	}else
	{
		document.all.item("tab" + tabnumber).className = "TabNormal";
	}
}

// 查询选项展开或收起
function OptionControl(objControl,objOption,showControl)
{
	if(objControl.value == "false")
	{
		objControl.className = "OptionControlMinus";
		objControl.value = "true";
		objControl.title = "收起查询选项";
		objOption.style.display = "block";
		if(showControl != undefined)
		{
			showControl.style.display = "block";
		}
	}
	else
	{
		objControl.className = "OptionControlPlus";
		objControl.value = "false";
		objControl.title = "展开查询选项";
		objOption.style.display = "none";
		if(showControl != undefined)
		{
			showControl.style.display = "none";
		}
	}
}
// 弹出树型目录
document.write("<input type=\"hidden\" id=\"TreeXmlData\" value=\"\">");
function OpenTree(url,request)
{
	var vsRe = null;
	
	// 读取数据的等待窗口
	if(request != undefined && request != "")
	{
		url = url + "?" + request
	}
	
	vsRe = window.showModalDialog(url, window, "dialogWidth:220px;dialogHeight:50px");
	if(vsRe == undefined)
	{
		alert("数据读取操作被用户取消！");
		return;
	}
	
	if(vsRe.length > 5 && vsRe.substring(0,5) == "error")
	{
		alert("错误：" + vsRe.substring(6,vsRe.length));
		return;
	}
	
	url = url.replace("GetTreeData","Tree");
	
	// 树形目录窗口
	vsRe = window.showModalDialog(url, window, "dialogWidth:500px;dialogHeight:400px");
	return vsRe;
}

// 选择委办局下的人员列表
function SetDeptUser(hidUserId,hidUserName,divUserView,dir,btn)
{
	var url = "";
	if(dir == undefined || dir == "")
	{
		url = "../Includes/GetTreeData.aspx";
	}
	else
	{
		url = dir + "/GetTreeData.aspx";
	}
	
	// 已选项目列表
	var selected = document.all.item(hidUserId).value;
	var request = "datasource=deptaduser&selected=" + selected;
	var vsRe = OpenTree(url,request);
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		document.all.item(hidUserId).value = ss[0];
		document.all.item(hidUserName).value = ss[1];
		if(ss[1] == "")
		{
			document.all.item(divUserView).innerHTML = "(请选择人员)";
		}
		else
		{
			document.all.item(divUserView).innerHTML = ss[1];
		}
		
		if(btn != "" && btn != undefined)
		{
			if(document.all.item(btn))
			{
				document.all.item(btn).click();
			}
		}
	}
}

// 选择单位
function DeptSelect(objDeptCode,objDeptName,objDeptView,url,btn) 
{
	/*
	if(url == "" || url == undefined)
	{
		url = "../Includes/ListDept.aspx";
	}
	
	var listChecked = document.all.item(objDeptCode).value;
	
	var flag = "";
	var vsRe = window.showModalDialog(url + "?list=" + listChecked + "&flag=" + flag, "", "dialogWidth:450px;dialogHeight:400px");
	*/
	
	if(url == "" || url == undefined)
	{
		url = "../Includes/GetTreeData.aspx";
	}
	else if(url.indexOf("ListDept") > -1)
	{
		url = url.replace("ListDept","GetTreeData");
	}
	// 已选项目列表
	//var selected = document.all.item(objDeptCode).value;
	
	//var vsRe = OpenTree(url,"datasource=deptdb&selected=" + selected);
	document.all.item("TreeXmlData").haschecked = document.all.item(objDeptCode).value;
	var vsRe = OpenTree(url,"datasource=deptdb");
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		document.all.item(objDeptCode).value = ss[0];
		document.all.item(objDeptName).value = ss[1];
		if(ss[1] == "")
		{
			document.all.item(objDeptView).innerHTML = "(所有单位)";
		}
		else
		{
			document.all.item(objDeptView).innerHTML = ss[1];
		}
		
		if(btn != "" && btn != undefined)
		{
			if(document.all.item(btn))
			{
				document.all.item(btn).click();
			}
		}
	}
}
function DeptSelectByTxt(objDeptCode,objDeptName,objDeptView,url,btn) 
{
	if(url == "" || url == undefined)
	{
		url = "../Includes/GetTreeData.aspx";
	}
	else if(url.indexOf("ListDept") > -1)
	{
		url = url.replace("ListDept","GetTreeData");
	}
	// 已选项目列表
	document.all.item("TreeXmlData").haschecked = document.all.item(objDeptCode).value;
	var vsRe = OpenTree(url,"datasource=deptdb");
	//var selected = document.all.item(objDeptCode).value;
	
	//var vsRe = OpenTree(url,"datasource=deptdb&selected=" + selected);
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		document.all.item(objDeptCode).value = ss[0];
		document.all.item(objDeptName).value = ss[1];
		if(ss[1] == "")
		{
			document.all.item(objDeptView).value = "(所有单位)";
		}
		else
		{
			document.all.item(objDeptView).value = ss[1];
		}
		
		if(btn != "" && btn != undefined)
		{
			if(document.all.item(btn))
			{
				document.all.item(btn).click();
			}
		}
	}
}
// 弹出设置日期对话框
function SetDateValue(objHidden,objView,url,btn)
{
	if(url == "" || url == undefined)
	{
		url = "../Includes/SetDateValue.aspx";
	}
	
	var vsRe = window.showModalDialog(url, "", "dialogWidth:420px;dialogHeight:300px");
	
	if(vsRe != null && vsRe != undefined)
	{
		document.all.item(objHidden).value = vsRe;
		
		if(vsRe == "")
		{
			document.all.item(objView).innerHTML = "(不限)";
		}
		else
		{
			if(vsRe.length > 10)
			{
				vsRe = vsRe.replace("~"," 至 ");
			}
			else if(vsRe.length == 7)
			{
				vsRe = vsRe.replace("-","年");
				vsRe = vsRe + "月";
			}
			else if(vsRe.length == 4)
			{
				vsRe = vsRe + "年";
			}
			
			document.all.item(objView).innerHTML = vsRe;
		}
		
		if(btn != "" && btn != undefined)
		{
			document.all.item(btn).click();
		}
	}
}

function SetDateValueByTxt(objHidden,objTxt,url,btn)
{
	if(url == "" || url == undefined)
	{
		url = "../Includes/SetDateValue.aspx";
	}
	
	var vsRe = window.showModalDialog(url, "", "dialogWidth:420px;dialogHeight:300px");
	
	if(vsRe != null && vsRe != undefined)
	{
		document.all.item(objHidden).value = vsRe;
		
		if(vsRe == "")
		{
			document.all.item(objTxt).value = "(不限)";
		}
		else
		{
			if(vsRe.length > 10)
			{
				vsRe = vsRe.replace("~"," 至 ");
			}
			else if(vsRe.length == 7)
			{
				vsRe = vsRe.replace("-","年");
				vsRe = vsRe + "月";
			}
			else if(vsRe.length == 4)
			{
				vsRe = vsRe + "年";
			}
			
			document.all.item(objTxt).value = vsRe;
		}
		
		if(btn != "" && btn != undefined)
		{
			document.all.item(btn).click();
		}
	}
}

// author:limq
// 政务公开条件设置和只能选取一个单位通用javascript
function TypeSelect(objDeptCode,objDeptName,objDeptView,url,message) 
{
	var listChecked = document.all.item(objDeptCode).value;
	var flag = "";
	var vsRe = window.showModalDialog(url + "?list=" + listChecked + "&flag=" + flag, "", "dialogWidth:450px;dialogHeight:400px");
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		document.all.item(objDeptCode).value = ss[0];
		document.all.item(objDeptName).value = ss[1];
		if(ss[1] == "")
		{
			document.all.item(objDeptView).innerHTML = message;
		}
		else
		{
			document.all.item(objDeptView).innerHTML = ss[1];
		}
	}
}

//把一个列表的项添加到另一个列表
//sName 源列表控件名称
//oName 目标列表控件名称
//isDel 将选中项添加后是否从源列表控件中删除
//isAdd 是否将选中的列表项添加到目标控件
function AddOption(sName, oName, isDel, isAdd)
{
	var rows = 0;
	var isExist = false;

	for(i = 0; i < document.all(sName).options.length; i++)
	{
		var sObj = document.all(sName).options[i];
		if (sObj.selected)
		{
			//添加项
			if (!isDel)
			{
				for(j = 0; j < document.all(oName).options.length; j++)
				{
					var oObj = document.all(oName).options[j];
					if (oObj.value == sObj.value)
					{
						isExist = true;
					}
				}
			}
			if (isAdd)
			{
				if (!isExist)
				{
					var oOption = document.createElement("OPTION");
					document.all(oName).options.add(oOption);
					oOption.innerText = sObj.text;
					oOption.value = sObj.value;
				}
				else
				{
					alert("您要添加列表项已存在！");
					return;
				}
			}
			//
			if (isDel)
			{
				document.all(sName).options.remove(i);
			}
			//
			rows++;
		}
	}

	if (rows == 0)
	{
		alert("请选择列表项！");
		return;
	}
}

//url  打开窗口的地址
//w  窗口的宽度
//h  窗口的高度
//isRet  是否返回值
//inputTxt  保存返回值的控件名称
function PopWindow(url,w,h,isRet,inputName)
{
	var vsRe = window.showModalDialog(url,window,"dialogwidth:" + w + "px; dialogheight:" + h + "px");
	if(isRet == true)
	{
		if (vsRe != undefined && vsRe != "")
		{
			document.all(inputName).value = vsRe;
		}
	}
}

/*********************************
 * 金宏样式的人员选择框（单个单位）
 */
function JhOneOrgSelectUser(curDir) 
{
	if(curDir == null || curDir == undefined)
	{
		curDir = "";
	}
	var orgId="9c4b1adbb658d045955d8c4298fdda44";
	var sFeatures="dialogHeight: 445px;dialogWidth:580px;";
	window.showModalDialog(curDir + "Includes/SelectUser.aspx?orgId="+orgId+"&type=Single","",sFeatures);
}

/*********************************
 * 金宏样式的人员选择框（多个单位）
 */
function JhMultiOrgSelectUser(curDir)
{
	if(curDir == null || curDir == undefined)
	{
		curDir = "";
	}
    var orgId="9c4b1adbb658d045955d8c4298fdda44|51ed45711feffd4681d04512cc26503e";
	var sFeatures="dialogHeight: 445px;dialogWidth:580px;";
	window.showModalDialog(curDir + "Includes/SelectUser.aspx?orgId="+orgId+"&type=List","",sFeatures);
}
//*************start add by guoxd 2007-2-27***********************
//
// 机构选择对象输出
// 参数说明：
// namestr 显示输入域的名称(自动创建)
// size 显示输入域的大小
// cstyle css样式
// valuestr 显示输入域的默认显示值
// isrunat 是否服务器端控件
// imgpath 选择所用图标
// hideValId 保存Id的隐藏域名称
// hideValName 保存Name的隐藏域名称
// isorg 是否是机构选择窗口,否则认为是处理人选择窗口
// url 机构选择或办理人选择窗口的地址
// 弹出窗口所选数据Id
// 弹出窗口所选数据名称
//


// 打开选择窗口
function ExPopSelectWin(objDeptCode,objDeptName,url,isorg,fieldNumber) 
{

	var listChecked = document.all.item(objDeptCode).value;
	var vsRe = null;
	
	if(!isorg)
	{
		vsRe = OpenTree(url,"datasource=ad&treetype=user&checktype=checkbox&root=Key|"+fieldNumber+",Value|"+fieldNumber+"&selected=" + objDeptCode);
	}
	else
	
	{
		var flag = "";
		vsRe = window.showModalDialog(url + "?list=" + listChecked + "&flag=" + flag, "", "dialogWidth:450px;dialogHeight:400px");
	}

	if(vsRe != "" && vsRe != undefined&& vsRe != "|")
	{
		var ss=vsRe.split("|");
		document.all.item(objDeptCode).value = ss[0];
		document.all.item(objDeptName).value = ss[1];
		return true;
	}
	else{
//	alert("请选择审核人员.");
	return false;}
}
//*************end add by guoxd 2007-2-27***********************