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
function PopSltWin(namestr,size,cstyle,valuestr,isrunat,imgpath,hideValId,hideValName,isorg,url,fieldNumber)
{
	try
	{
	var outstr = "<input readonly type='text' ";
	
	//创建显示输入域
	if(namestr == "" || namestr == undefined)
	{
		outstr += "id='TxtShowSelect' ";
	}
	else
	{
		outstr += "id='" + namestr + "' ";
	}
	if(size == "" || size == undefined)
	{
		outstr += "size='20' ";
	}
	else
	{
		outstr += "size='" + size + "' ";
	}
	if(cstyle != undefined && cstyle != "")
	{
		outstr += "class='" + cstyle + "' ";
	}
	if(valuestr != undefined && valuestr != "")
	{
		outstr += "value='" + valuestr + "' ";
	}
	if(isrunat != undefined && isrunat == true)
	{
		outstr += "runat='server' ";
	}
	outstr += ">";
	
	//创建选择图标
	if(imgpath != undefined && imgpath != "")
	{
		if(isorg)
		{
			outstr += "&nbsp;<img border='0' src='" + imgpath + "' width='16' height='16' style='cursor:hand' title='选择接收单位...'";
		}
		else
		{
			outstr += "&nbsp;<img border='0' src='" + imgpath + "' width='16' height='16' style='cursor:hand' title='选择接收人...'";
		}
		if(fieldNumber!=undefined)
		{
			outstr += " onclick=\"javascript:PopSelectWin('" + hideValId + "','" + hideValName + "','" + namestr + "','" + url + "'," + isorg + ",'','"+fieldNumber+"');\">";
		}else
		{
			outstr += " onclick=\"javascript:PopSelectWin('" + hideValId + "','" + hideValName + "','" + namestr + "','" + url + "'," + isorg + ",'','beb1f0476bc5fb449b98875e6419470d');\">";
		}
		
	}
	
	//输出
	document.write(outstr);
	}catch(e){}
}

// 打开选择窗口
function PopSelectWin(objDeptCode,objDeptName,objDeptView,url,isorg,btn,fieldNumber) 
{
	try
	{
	var listChecked = document.all.item(objDeptCode).value;
	var vsRe = null;
	if(!isorg)
	{
		vsRe = OpenTree(url,"datasource=ad&treetype=user&checktype=radio&root=Key|"+fieldNumber+",Value|"+fieldNumber+"&selected=" + objDeptCode);
	}
	else
	{
		var flag = "";
		vsRe = window.showModalDialog(url + "?list=" + listChecked + "&flag=" + flag, "", "dialogWidth:450px;dialogHeight:400px");
	}
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		document.all.item(objDeptCode).value = ss[0];
		document.all.item(objDeptName).value = ss[1];
		if(ss[1] == "")
		{
			if(isorg)
			{
				document.all.item(objDeptView).value = "所有单位...";
			}
			else
			{
				document.all.item(objDeptView).value = "";
			}
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
	}catch(e){}
}

//为人员赋角色
function PopSelectWin1(objDeptCode,objDeptName,objDeptView,url,isorg,btn,fieldNumber) 
{
	try
	{
	var listChecked = document.all.item(objDeptCode).value;
	var vsRe = null;
	if(!isorg)
	{
		vsRe = OpenTree(url,"datasource=ad&treetype=user&checktype=checkbox&root=Key|"+fieldNumber+",Value|"+fieldNumber+"&selected=" + listChecked);
	}
	else
	{
		var flag = "";
		vsRe = window.showModalDialog(url + "?list=" + listChecked + "&flag=" + flag, "", "dialogWidth:450px;dialogHeight:400px");
	}
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		document.all.item(objDeptCode).value = ss[0];
		document.all.item(objDeptName).value = ss[1];
		if(ss[1] == "")
		{
			if(isorg)
			{
				document.all.item(objDeptView).value = "所有单位...";
			}
			else
			{
				document.all.item(objDeptView).value = "";
			}
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
	else
	{
		document.all.item(objDeptName).value = "undefined";
	}
	}catch(e){}
}