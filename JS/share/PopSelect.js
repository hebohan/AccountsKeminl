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
function PopSltWin(namestr,size,cstyle,valuestr,isrunat,imgpath,hideValId,hideValName,isorg,url,fieldNumber)
{
	try
	{
	var outstr = "<input readonly type='text' ";
	
	//������ʾ������
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
	
	//����ѡ��ͼ��
	if(imgpath != undefined && imgpath != "")
	{
		if(isorg)
		{
			outstr += "&nbsp;<img border='0' src='" + imgpath + "' width='16' height='16' style='cursor:hand' title='ѡ����յ�λ...'";
		}
		else
		{
			outstr += "&nbsp;<img border='0' src='" + imgpath + "' width='16' height='16' style='cursor:hand' title='ѡ�������...'";
		}
		if(fieldNumber!=undefined)
		{
			outstr += " onclick=\"javascript:PopSelectWin('" + hideValId + "','" + hideValName + "','" + namestr + "','" + url + "'," + isorg + ",'','"+fieldNumber+"');\">";
		}else
		{
			outstr += " onclick=\"javascript:PopSelectWin('" + hideValId + "','" + hideValName + "','" + namestr + "','" + url + "'," + isorg + ",'','beb1f0476bc5fb449b98875e6419470d');\">";
		}
		
	}
	
	//���
	document.write(outstr);
	}catch(e){}
}

// ��ѡ�񴰿�
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
				document.all.item(objDeptView).value = "���е�λ...";
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

//Ϊ��Ա����ɫ
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
				document.all.item(objDeptView).value = "���е�λ...";
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