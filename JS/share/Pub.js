
//----------------------------------------------------------
/***********************************************
���������û���
LiChun
2006-03-23
***********************************************/

//�����̬���ؼ�������Ҫ���ã�
document.write("<input type=\"hidden\" id=\"treeparameter\" orgroot=\"\" treetype=\"\" checktype=\"\" haschecked=\"\" hascheckedtext=\"\">");

function DynamicTree(DataToSend)
{
	var vsRe1 = window.showModalDialog("../DynamicTree/GetTreeData.aspx?" + DataToSend + "",window,"dialogwidth:220px; dialogheight:100px");
	
	if(vsRe1 == undefined)
	{
		alert("���ݶ�ȡ�������û�ȡ����");
		return;
	}
	
	if(vsRe1.substring(0,5) == "error")
	{
		alert("����" + vsRe1.substring(6,vsRe1.length));
		return;
	}
	
	var parameter = "";
	
	//Ŀ¼����Ĭ��Ϊ�գ�
	parameter += "&orgroot=" + document.all.item("treeparameter").orgroot;
	
	//Ŀ¼���ͣ�all,org,user��Ĭ��Ϊall��
	parameter += "&treetype=" + document.all.item("treeparameter").treetype;
	
	//ѡ������ͣ�checkbox,radio��Ĭ��Ϊcheckbox��
	parameter += "&checktype=" + document.all.item("treeparameter").checktype;
	
	var vsRe2 = window.showModalDialog("../DynamicTree/Tree.aspx?" + parameter + "",window,"dialogwidth:400px; dialogheight:450px");
	
	return vsRe2;
}

//----------------------------------------------------------

//----------------------------------------------------------
/*********************************
 * �����ʽ����Աѡ��򣨵�����λ��
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
 * �����ʽ����Աѡ��򣨶����λ��
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