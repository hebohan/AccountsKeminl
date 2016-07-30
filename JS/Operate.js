/// Language: JavaScript 
/// ���Ӽ��ϵͳ�ĸ����û��������ǰ̨�ű�
/// Inspur.com

// ��ʼ��ҳ��ĵ�λѡ��
function InitDept(dept,deptView)
{
	var objDept = document.all.item(dept);
	var objDeptView = document.all.item(deptView);
	if(objDept != null && objDeptView != null)
	{
		if(objDept.value == "")
		{
			objDeptView.innerHTML = "(���е�λ)";
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
			objDeptView.value = "(���е�λ)";
		}
		else
		{
			objDeptView.value = objDept.value;
		}
	}
}

// ��ʼ��ҳ�������ѡ��
function InitDate(date,dateView)
{
	var objDate = document.all.item(date);
	var objDateView = document.all.item(dateView);
	if(objDate != null && objDateView != null)
	{
		if(objDate.value == "")
		{
			objDateView.innerHTML = "(����)";
		}
		else
		{
			var dateValue = objDate.value;
			if(dateValue.length > 10)
			{
				dateValue = dateValue.replace("~"," �� ");
			}
			else if(dateValue.length == 7)
			{
				dateValue = dateValue.replace("-","��");
				dateValue = dateValue + "��";
			}
			else if(dateValue.length == 4)
			{
				dateValue = dateValue + "��";
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
			objDateTxt.value = "(����)";
		}
		else
		{
			var dateValue = objDate.value;
			if(dateValue.length > 10)
			{
				dateValue = dateValue.replace("~"," �� ");
			}
			else if(dateValue.length == 7)
			{
				dateValue = dateValue.replace("-","��");
				dateValue = dateValue + "��";
			}
			else if(dateValue.length == 4)
			{
				dateValue = dateValue + "��";
			}
			
			objDateTxt.value = dateValue;
		}
	}
}
//ˢ��ҳ���ʱ��������б�
function InitList(deptName,deptCode,list,listOneName)
{
		if(document.all.item(deptName).value=='���е�λ')
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
// ��ʼ��ҳ��Ĳ�ѯѡ��������ť
function InitOption(txtOptionControl,tblOption,showControl)
{
	objControl = document.all.item(txtOptionControl);
	objOption = document.all.item(tblOption);
	if(objControl.value == "true")
	{
		objControl.className = "OptionControlMinus";
		objControl.value = "true";
		objControl.title = "�����ѯѡ��";
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
		objControl.title = "չ����ѯѡ��";
		objOption.style.display = "none";
		if(showControl != undefined)
		{
			document.all(showControl).style.display = "none";
		}
	}
}

//����ƶ��¼�
function MoveMouse(tabnumber)
{	
	if(document.all.item("tab" + tabnumber).className != "TabSelected")
	{
		document.all.item("tab" + tabnumber).className = "TabMoveMouse";
	}		
}
//����ƶ��¼�
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

// ��ѯѡ��չ��������
function OptionControl(objControl,objOption,showControl)
{
	if(objControl.value == "false")
	{
		objControl.className = "OptionControlMinus";
		objControl.value = "true";
		objControl.title = "�����ѯѡ��";
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
		objControl.title = "չ����ѯѡ��";
		objOption.style.display = "none";
		if(showControl != undefined)
		{
			showControl.style.display = "none";
		}
	}
}
// ��������Ŀ¼
document.write("<input type=\"hidden\" id=\"TreeXmlData\" value=\"\">");
function OpenTree(url,request)
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

// ѡ��ί����µ���Ա�б�
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
	
	// ��ѡ��Ŀ�б�
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
			document.all.item(divUserView).innerHTML = "(��ѡ����Ա)";
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

// ѡ��λ
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
	// ��ѡ��Ŀ�б�
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
			document.all.item(objDeptView).innerHTML = "(���е�λ)";
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
	// ��ѡ��Ŀ�б�
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
			document.all.item(objDeptView).value = "(���е�λ)";
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
// �����������ڶԻ���
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
			document.all.item(objView).innerHTML = "(����)";
		}
		else
		{
			if(vsRe.length > 10)
			{
				vsRe = vsRe.replace("~"," �� ");
			}
			else if(vsRe.length == 7)
			{
				vsRe = vsRe.replace("-","��");
				vsRe = vsRe + "��";
			}
			else if(vsRe.length == 4)
			{
				vsRe = vsRe + "��";
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
			document.all.item(objTxt).value = "(����)";
		}
		else
		{
			if(vsRe.length > 10)
			{
				vsRe = vsRe.replace("~"," �� ");
			}
			else if(vsRe.length == 7)
			{
				vsRe = vsRe.replace("-","��");
				vsRe = vsRe + "��";
			}
			else if(vsRe.length == 4)
			{
				vsRe = vsRe + "��";
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
// ���񹫿��������ú�ֻ��ѡȡһ����λͨ��javascript
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

//��һ���б������ӵ���һ���б�
//sName Դ�б�ؼ�����
//oName Ŀ���б�ؼ�����
//isDel ��ѡ������Ӻ��Ƿ��Դ�б�ؼ���ɾ��
//isAdd �Ƿ�ѡ�е��б�����ӵ�Ŀ��ؼ�
function AddOption(sName, oName, isDel, isAdd)
{
	var rows = 0;
	var isExist = false;

	for(i = 0; i < document.all(sName).options.length; i++)
	{
		var sObj = document.all(sName).options[i];
		if (sObj.selected)
		{
			//�����
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
					alert("��Ҫ����б����Ѵ��ڣ�");
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
		alert("��ѡ���б��");
		return;
	}
}

//url  �򿪴��ڵĵ�ַ
//w  ���ڵĿ��
//h  ���ڵĸ߶�
//isRet  �Ƿ񷵻�ֵ
//inputTxt  ���淵��ֵ�Ŀؼ�����
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
 * �����ʽ����Աѡ��򣨵�����λ��
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
 * �����ʽ����Աѡ��򣨶����λ��
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
// ����ѡ��������
// ����˵����
// namestr ��ʾ�����������(�Զ�����)
// size ��ʾ������Ĵ�С
// cstyle css��ʽ
// valuestr ��ʾ�������Ĭ����ʾֵ
// isrunat �Ƿ�������˿ؼ�
// imgpath ѡ������ͼ��
// hideValId ����Id������������
// hideValName ����Name������������
// isorg �Ƿ��ǻ���ѡ�񴰿�,������Ϊ�Ǵ�����ѡ�񴰿�
// url ����ѡ��������ѡ�񴰿ڵĵ�ַ
// ����������ѡ����Id
// ����������ѡ��������
//


// ��ѡ�񴰿�
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
//	alert("��ѡ�������Ա.");
	return false;}
}
//*************end add by guoxd 2007-2-27***********************