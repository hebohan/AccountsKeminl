/// Language:javascript
/// 输出Flash标签，这样可以去掉虚线框
var flashTabs;
var flashUrls = new Array();
var flashRedirect = false;

// 初始化FlashTab
function initflash()
{
	if(document.all.item("tab") == null || document.all.item("tabworkarea") == null)
	{
		window.location.reload();
	}
	else
	{
		var objFlash = document.all.item("tab");
		objFlash.SetVariable("_root.tabvalue",flashTabs); //在此填写tab页选项内容
		if(document.all.item("checktab") != null)		
		{
			viewtab(document.all.item("checktab").value);
		}
		else
		{
			viewtab(0);//在此设定当前tab选项,索引从0开始.
		}
	}
}

// 点击标签项
function viewtab(index)
{
	var objFlash = document.all.item("tab");
	objFlash.SetVariable("_root.selecttab",index);
	
	var objFrame = document.all.item("tabworkarea");
	if(flashRedirect)
	{
		objFrame.src = "Redirect.aspx?src=" + flashUrls[index];
	}
	else
	{
		objFrame.src = flashUrls[index];
	}
}

function GetFlash(str,url,dir)
{
	flashTabs = str;
	flashUrls = url;
	if(dir == undefined || dir == "")
	{
		dir = "../Images/Share/tab.swf";
	}
	document.write("<OBJECT id=\"tab\" codeBase=\"../../invokeobj/swflash.cab#version=6,0,0,0\"");
	document.write(" height=\"20\" width=\"100%\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" VIEWASTEXT> \n");
	document.write("	<PARAM NAME=\"_cx\" VALUE=\"18653\"> \n");
	document.write("	<PARAM NAME=\"_cy\" VALUE=\"529\"> \n");
	document.write("	<PARAM NAME=\"FlashVars\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"Movie\" VALUE=\"" + dir + "\"> \n");
	document.write("	<PARAM NAME=\"Src\" VALUE=\"" + dir + "\"> \n");
	document.write("	<PARAM NAME=\"WMode\" VALUE=\"Transparent\"> \n");
	document.write("	<PARAM NAME=\"Play\" VALUE=\"-1\"> \n");
	document.write("	<PARAM NAME=\"Loop\" VALUE=\"-1\"> \n");
	document.write("	<PARAM NAME=\"Quality\" VALUE=\"High\"> \n");
	document.write("	<PARAM NAME=\"SAlign\" VALUE=\"LT\"> \n");
	document.write("	<PARAM NAME=\"Menu\" VALUE=\"-1\"> \n");
	document.write("	<PARAM NAME=\"Base\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"AllowScriptAccess\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"Scale\" VALUE=\"NoScale\"> \n");
	document.write("	<PARAM NAME=\"DeviceFont\" VALUE=\"0\"> \n");
	document.write("	<PARAM NAME=\"EmbedMovie\" VALUE=\"0\"> \n");
	document.write("	<PARAM NAME=\"BGColor\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"SWRemote\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"MovieData\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"SeamlessTabbing\" VALUE=\"1\"> \n");
	document.write("	<PARAM NAME=\"Profile\" VALUE=\"0\"> \n");
	document.write("	<PARAM NAME=\"ProfileAddress\" VALUE=\"\"> \n");
	document.write("	<PARAM NAME=\"ProfilePort\" VALUE=\"0\"> \n");
	document.write("</OBJECT> \n");
}