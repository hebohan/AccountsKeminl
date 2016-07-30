
function ExTargetSelect(objDeptCode,objDeptName,objDeptView,url,btn) 
{

  
	if(url == "" || url == undefined)
	{
		url = "../Includes/ListExTarget.aspx";
	}
	
	
	// ��ѡ��Ŀ�б�
	var selected = document.all.item(objDeptCode).value;
	
	var vsRe = OpenTargetTree(url,"datasource=deptdb&selected=" + selected);
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");

		
        insertText(ss[1],"["+ss[0]+"]");
		
		if(ss[1] == "")
		{
			document.all.item(objDeptView).innerHTML = "";
			
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
// ��������Ŀ¼
//document.write("<input type=\"hidden\" id=\"TreeXmlData\" value=\"\">");
function OpenTargetTree(url,request)
{
	var vsRe = null;
	
	// ��ȡ���ݵĵȴ�����
	if(request != undefined && request != "")
	{
		url = url + "?" + request
	}

	vsRe = window.showModalDialog(url, window, "dialogWidth:220px;dialogHeight:50px");
	if(vsRe == undefined)
	{
		alert("���ݶ�ȡ�������û�ȡ����");
		return;
	}
	if(vsRe.length > 5 && vsRe.substring(0,5) == "error")
	{
		alert("����" + vsRe.substring(6,vsRe.length));
		return;
	}
	
	url = url.replace("ListExTarget","ExTargetTree");
	
	// ����Ŀ¼����
    vsRe = window.showModalDialog(url, window, "dialogWidth:500px;dialogHeight:400px");

	return vsRe;
}
function insertText(name,code)
{
    var str1 = document.all("ExTarFormulaName").value;
    document.all("ExTarFormulaName").value = str1 + name;
    var str2 = document.all("ExFormula").value;
    document.all("ExFormula").value = str2 + code;
}
		
function OpenTreeTar(url,request)
{
	var vsRe = null;
	
	// ��ȡ���ݵĵȴ�����
	if(request != undefined && request != "")
	{
		url = url + "?" + request
	}
	
	vsRe = window.showModalDialog(url, window, "dialogWidth:220px;dialogHeight:50px");
	if(vsRe == undefined)
	{
		alert("���ݶ�ȡ�������û�ȡ����");
		return;
	}
	
	if(vsRe.length > 5 && vsRe.substring(0,5) == "error")
	{
		alert("����" + vsRe.substring(6,vsRe.length));
		return;
	}

	url = url.replace("GetTreeData","Tree");

	// ����Ŀ¼����
  
	vsRe = window.showModalDialog(url, window, "dialogWidth:500px;dialogHeight:400px");
	return vsRe;
}	
// ѡ��λ
function DeptSelectTar(objDeptCode,objDeptName,objDeptView,url,btn) 
{
	
  
	if(url == "" || url == undefined)
	{
		url = "../Includes/GetTreeData.aspx";
	}
	else if(url.indexOf("ListDept") > -1)
	{
		url = url.replace("ListDept","GetTreeData");
	}
	
	// ��ѡ��Ŀ�б�
	var selected = document.all.item(objDeptCode).value;
	
	var vsRe = OpenTreeTar(url,"datasource=deptdb&selected=" + selected+"&checktype=radio");
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		
		document.all.item(objDeptCode).value = ss[0];
		
		document.all.item(objDeptName).value = ss[1];
	
		
		if(ss[1] == "")
		{
			document.all.item(objDeptView).innerHTML = "";
			
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
function ExTargetSelectTar(objDeptCode,objDeptName,objDeptView,url,btn,ExObjectId) 
{

  
	if(url == "" || url == undefined)
	{
		url = "../Includes/ListExTarget.aspx";
	}
	
	
	// ��ѡ��Ŀ�б�
	var selected = document.all.item(objDeptCode).value;
	
	var vsRe = OpenTargetTree(url,"datasource=deptdb&selected=" + selected+"&ExObjectId="+ExObjectId);
	
	if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		
		document.all.item(objDeptCode).value = ss[0];
		
		document.all.item(objDeptName).value = ss[1];

		
		if(ss[1] == "")
		{
			document.all.item(objDeptView).innerHTML = "";
			
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
function clareall(objDeptCode,objDeptName)
{
document.all.item(objDeptCode).innerHTML = '';
document.all.item(objDeptName).innerHTML = '';
}
function selFormula()
{
var vsRe = null;
vsRe = window.showModalDialog("../Includes/SelectExFormula.aspx", window, "dialogWidth:500px;dialogHeight:400px");
if(vsRe != "" && vsRe != undefined)
	{
		var ss=vsRe.split("|");
		
		insertText(ss[1],"{"+ss[0]+"}");

		
	}
}
function returnFormula(code,name) {
    
    window.returnValue = code + "|" + name;
    
	window.close();
}
function returnStencil(code,name,str)
{
window.returnValue = code + "|" + name + "|" + str;
	window.close();
}
//ѡ�����
function OnSetOrg() {
    var item = document.all("DeptCode").value;
    item = item.replace(',', '');
    var rtn = OpenTree2('../components/generaltree/', 'OrgTreeView', 'selecteditem=' + item + '&checktype=radio');

    if (rtn) {
        document.all.item("DeptCode").value = rtn.split('|')[0];
        document.all.item("DeptName").value = rtn.split('|')[1];
        document.all.item("DeptView").innerHTML = rtn.split('|')[1];
        return true;
    }
    return false;
}

function OnSetOrg1() {
    var item = document.all("ExObjectId").value;
    item = item.replace(',', '');
    var rtn = OpenTree2('../components/generaltree/', 'OrgTreeView', 'selecteditem=' + item + '&checktype=check');

    if (rtn) {
        document.all.item("ExObjectId").value = rtn.split('|')[0];
        document.all.item("ExObjectName").value = rtn.split('|')[1];
        document.all.item("DeptView").innerHTML = rtn.split('|')[1];
        return true;
    }
    return false;
}
function OnSetTarget() {
    var item = document.all("ExTargetId").value;
    item = item.replace(',', '');
    var rtn = OpenTree2('../components/generaltree/', 'TargetTreeView', 'selecteditem=' + item + '&checktype=radio');

    if (rtn) {
        document.all.item("ExTargetId").value = rtn.split('|')[0];
        document.all.item("ExTargetName").value = rtn.split('|')[1];
        document.all.item("TargetView").innerHTML = rtn.split('|')[1];
        return true;
    }
    return false;
}
