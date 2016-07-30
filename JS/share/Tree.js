
/*****************************************************
 *****************************************************
 * ����Ŀ¼�ķ����ű���
 * LiChun
 *****************************************************
 ****************************************************/
 
/*************************************************
 * ��������ʼ��XML���󣨳�Ա������
 */
var treeXml = null;

try
{
	treeXml = new ActiveXObject("Microsoft.XMLDOM");
	treeXml.async = false;	// ����ͬ��װ�����ݣ�Ĭ�����첽��
	treeXml.loadXML(window.dialogArguments.document.all.item("TreeXmlData").value);	// ��������
}
catch(ex)
{
	alert("����" + ex.description);
}


/*************************************************
 * ���ڵ�Ԫ�أ�����Ŀ¼�Ļ������ԣ�����Ŀ¼�ĸ������ÿ����ڸ��ڵ�Ԫ�������ã�
 */
var rootNode = treeXml.documentElement;

var treeType = rootNode.getAttribute("treetype");	// �����ͣ�power,user��
if(treeType == null || treeType == undefined || treeType == "")
	treeType = "all";

var checkType = rootNode.getAttribute("checktype");	// ѡ�����ͣ�radio,checkbox��
if(checkType == null || checkType == undefined || checkType == "")
	checkType = "checkbox";

var imagesDir = rootNode.getAttribute("imagesdir");	// ͼƬĿ¼
if(imagesDir == null || imagesDir == undefined || imagesDir == "")
	imagesDir = "../Images/Tree/";

rootNode.setAttribute("view","");	// �����ڵ����ʾ���и��ӵ�����


/*************************************************
 * �������������Ŀ¼HTML����
 */
var treeHtml = "";

/*************************************************
 * ˢ������Ŀ¼
 */
function ReloadTree()
{
	document.all.item("root_child").innerHTML = "������...";
	
	// �첽�����������Ŀ¼Html����
	setTimeout(GetTreeView,100);
}

/*************************************************
 * �������Ŀ¼Html����
 */
function GetTreeView()
{
	if(checkType=="checkbox")
	{
		document.all("btnSelectAll").style.display = "";
	}
	// ���ڵ�Ԫ�ص�ʵ��
	treeHtml = "{" + rootNode.nodeName + "_child}";
	GetTreeHtml(rootNode);
	document.all.item("root_child").innerHTML = treeHtml;
	InitSelected();	// ��ʼ����ѡ�б�
}


/*************************************************
 * ��ȡ�ӽڵ�ĵݹ�
 * ������parentNode-��ǰ�ӽڵ�ĸ��ڵ�
 * ����ֵ�������HTML
 */
function GetTreeHtml(parentNode)
{
	// �滻��ǰģ��λ
	treeHtml = treeHtml.replace("{" + parentNode.nodeName + "_child}",GetNodeHtml(parentNode));
	
	var nodes = parentNode.childNodes;
	
	for(var i = 0; i < nodes.length; i++)
	{
		GetTreeHtml(nodes[i]);
	}
	
	return treeHtml;
}


/*************************************************
 * ��ȡĳ���ڵ��������ӽڵ���б�
 * ������parentNode-��ǰ�ӽڵ�ĸ��ڵ�
 * ����ֵ�������HTML
 */
function GetNodeHtml(parentNode)
{
	var nodes = parentNode.childNodes;
	var nodeHtml = "";
	
	for(var i = 0; i < nodes.length; i++)
	{
		// ��ǰ�ڵ����Ϣ�����ж��Ƿ񶥽ڵ��׽ڵ㣬���ж��ڵ���Ҫ�ж��Ƿ�β�ڵ�
		var currentType = "2";	// �б���
		
		// ���ӽڵ���б���
		if(nodes[i].childNodes.length > 0)
		{
			currentType = "3";
		}
		
		// ���ڵ�
		if(i == 0)
		{
			if(nodes[i].nodeName == rootNode.nodeName)
			{
				currentType = "9";	// ���ڵ�
			}
			else
			{
				if(nodes[i].childNodes.length > 0)
					currentType = "4";	// ���ڵ�
				else
					currentType = "6";	// ���ӽڵ�Ķ��ڵ�
			}
		}

		if(i == nodes.length - 1)
		{
			if(nodes[i].childNodes.length > 0)
				currentType = "5";	// β�ڵ�
			else
				currentType = "7";	// ���ӽڵ��β�ڵ�
		}

		// �ڵ���ʾ�����ַ���
		var currentView = GetNodeView(parentNode.getAttribute("view"),currentType);
		nodes[i].setAttribute("view",currentView);	// ����ǰ��ʾ���и��ӵ���ǰ�ڵ�����
		
		// ===========================================
		// ����ڵ��Html����
		nodeHtml += "<div id=\"" + nodes[i].nodeName + "\" parent=\"" + parentNode.nodeName + "\"> \n";
		nodeHtml += "	<table> \n";
		nodeHtml += "		<tr height=\"20\"> \n";
		
		// ������ʾ�������HTML
		for(var j = 0; j < currentView.length; j++)
		{
			switch(currentView.substring(j,j + 1))
			{
				case "0":
					nodeHtml += "\t\t\t<td width=\"20\"></td> \n";
					break;

				case "1":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "list.gif\"></td> \n";
					break;

				case "2":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "node.gif\"></td> \n";
					break;
				
				case "3":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "plus.gif\" id=\"" + nodes[i].nodeName + "_image\" onclick=\"javascript:NodeClick(this,'" + nodes[i].nodeName + "');\" style=\"cursor: hand\"></td> \n";
					break;
				
				case "4":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "plustop.gif\" id=\"" + nodes[i].nodeName + "_image\" onclick=\"javascript:NodeClick(this,'" + nodes[i].nodeName + "');\" style=\"cursor: hand\"></td> \n";
					break;
				
				case "5":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "plusbottom.gif\" id=\"" + nodes[i].nodeName + "_image\" onclick=\"javascript:NodeClick(this,'" + nodes[i].nodeName + "');\" style=\"cursor: hand\"></td> \n";
					break;
				
				case "6":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "nodetop.gif\"></td> \n";
					break;
				
				case "7":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "nodebottom.gif\"></td> \n";
					break;
				
				case "9":
					nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + "plusroot.gif\" id=\"" + nodes[i].nodeName + "_image\" onclick=\"javascript:NodeClick(this,'" + nodes[i].nodeName + "');\" style=\"cursor: hand\"></td> \n";
					break;
			}
		}
		
		// ���ѡ���
		if((treeType == "all") || (treeType == "power" && nodes[i].getAttribute("type") == "power") || (treeType == "user" && nodes[i].getAttribute("type") == "user"))
		{
			nodeHtml += "\t\t\t<td width=\"20\"><input type=\"" + checkType + "\" id=\"" + nodes[i].nodeName + "_check\" name=\"check\" ";
			if(nodes[i].getAttribute("checked") == "true")
			{
				nodeHtml += " checked ";
			}
			nodeHtml += " node=\"" + nodes[i].nodeName + "\" nodetype=\"" + nodes[i].getAttribute("type") + "\" key=\"" + nodes[i].getAttribute("key") + "\" value=\"" + nodes[i].getAttribute("value") + "\" text=\"" + nodes[i].getAttribute("text") + "\" ";
			nodeHtml += " onclick=\"javascript:Check_Click(this);\"></td> \n";
		}
		
		// ���ͼ��
		nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + nodes[i].getAttribute("type") + ".gif\"></td> \n";
		
		// ����ı�
		nodeHtml += "\t\t\t<td id=\"lblNode\" node=\"" + nodes[i].nodeName + "\">" + nodes[i].getAttribute("text") + "</td> \n";
		
		nodeHtml += "		</tr> \n";
		nodeHtml += "	</table> \n";
		nodeHtml += "</div> \n";
		
		nodeHtml += "<div id=\"" + nodes[i].nodeName + "_child\" count=\"" + nodes[i].childNodes.length + "\" style=\"display: none\"> \n";
		nodeHtml += "{" + nodes[i].nodeName + "_child}";	// ����ýڵ���ӽڵ�ģ��λ
		nodeHtml += "</div> \n";
	}
	
	return nodeHtml;
}

/*************************************************
 * ����ڵ���ʾ�ִ�
 * null��0,�հף�line��1,���ߣ�list��2,�б���Ŀ��plus��3,����ڵ㣩top��4,���ڵ㣩bottom��5,β�ڵ㣩
 * nodetop��6,���ӽڵ�Ķ��ڵ㣩nodebottom��7,���ӽڵ��β�ڵ㣩root��9,���ڵ㣩
 */
function GetNodeView(parentView,currentType)
{
	var currentView = "";

	for(var i = 0; i < parentView.length; i++)
	{
		// ���縸�ڵ㲻��β�ڵ��հף���̳и��ڵ���Ϣ���������
		if(parentView.substring(i,i + 1) != "5" && parentView.substring(i,i + 1) != "0")
		{
			currentView += "1";
		}
		// �����β�ڵ��հף�������հ�
		else
		{
			currentView += "0";
		}
	}

	// ���縸�ڵ��Ǹ����򲻱����գ�������Ǹ�����Ҫ��һ��
	if(parentView != "")
	{
		if(treeType != "user")
		{
			currentView += "0";
		}
	}
	
	currentView += currentType;
	
	return currentView;
}

/*************************************************
 * �������չ���Ľڵ�
 */
function NodeClick(objNodeImage,node)
{
	// �л� + �� - ��ͼƬ������������ɼ���
	var objChild = document.all.item(node + "_child");
	if(objNodeImage.src.indexOf("plus") >= 0)
	{
		objNodeImage.src = objNodeImage.src.replace("plus","minus");
		objChild.style.display = "block";
		
		// ����һ������������Ϊ�ɼ�
		for(var i = 1; i < parseInt(objChild.count) + 1; i++)
		{
			var objChildNode = document.all.item(node + "_" + i);
			//objChildNode.style.display = "block";alert("kk");
		}
	}
	else
	{
		objNodeImage.src = objNodeImage.src.replace("minus","plus");
		objChild.style.display = "none";
	}
}

/*************************************************
 * ���µݹ�չ�������ӽڵ�ķ���
 */
function NodeAllExpand(parentNode)
{
	var objNode = document.all.item(parentNode.nodeName);
	var objChild = document.all.item(parentNode.nodeName + "_child");
	var objImage = document.all.item(parentNode.nodeName + "_image");
	
	objNode.style.display = "block";
	objChild.style.display = "block";
	if(objImage != null && objImage != undefined && objImage.src.indexOf("plus") > -1)
	{
		objImage.src = objImage.src.replace("plus","minus");
	}
	
	var nodes = parentNode.childNodes;
	for(var i = 0; i < nodes.length; i++)
	{
		NodeAllExpand(nodes[i]);
	}
}
 
/*************************************************
 * ���µݹ����������ӽڵ�ķ���
 */
function NodeAllSetBy(parentNode)
{
	var objNode = document.all.item(parentNode.nodeName);
	var objChild = document.all.item(parentNode.nodeName + "_child");
	var objImage = document.all.item(parentNode.nodeName + "_image");
	
	objNode.style.display = "block";
	objChild.style.display = "none";
	if(objImage != null && objImage != undefined && objImage.src.indexOf("minus") > -1)
	{
		objImage.src = objImage.src.replace("minus","plus");
	}
	
	var nodes = parentNode.childNodes;
	for(var i = 0; i < nodes.length; i++)
	{
		NodeAllSetBy(nodes[i]);
	}
}

/*************************************************
 * �����һ�����������ӽڵ�ķ���
 */
function NodeFirstLevelSetBy()
{
	var nodes = rootNode.childNodes;
	for(var i = 0; i < nodes.length; i++)
	{
		NodeAllSetBy(nodes[i]);
	}
}

/*************************************************
 * ���ϵݹ�չ�����и��ڵ�ķ���
 */
function NodeExpand(node)
{
	var objNode = document.all.item(node);
	var objChild = document.all.item(node + "_child");
	var objImage = document.all.item(node + "_image");
	
	objNode.style.display = "block";
	objChild.style.display = "block";
	if(objImage != null && objImage != undefined && objImage.src.indexOf("plus") > -1)
	{
		objImage.src = objImage.src.replace("plus","minus");
	}
	
	if(objNode.parent != null && objNode.parent != undefined && objNode.parent != "")
	{
		NodeExpand(objNode.parent);
	}
}

/*************************************************
 * ��ʾ����һ���ڵ�
 */
function NodeShow(node)
{
	var objNode = document.all.item(node);
	objNode.style.display = "block";
	
	// �ݹ�չ�������и��ڵ�
	NodeExpand(node);
	
	// �����ӽڵ�
	var objChild = document.all.item(node + "_child");
	var objImage = document.all.item(node + "_image");
	objChild.style.display = "none";
	if(objImage != null && objImage != undefined && objImage.src.indexOf("minus") > -1)
	{
		objImage.src = objImage.src.replace("minus","plus");
	}
}

/*************************************************
 * ��������һ���ڵ�
 */
function NodeHidden(node)
{
	var objNode = document.all.item(node);
	objNode.style.display = "none";
}

/*************************************************
 * ���������ť
 */
var text = "";	// �����ַ���
function btnSearch_Click(txtSearch,SearchWaitting,treeContainer)
{
	objText = document.all.item("txtSearch");
	objWaitting = document.all.item("SearchWaitting");
	objContainer = document.all.item("root_child");
	MyTrim(objText);
	text = objText.value;
	
	// �Ƚ�Ŀ¼�����أ���ʾ�ȴ���ʾ
	objWaitting.innerHTML = "<font color='#0000FF'>��������...</font>";
	objWaitting.style.display = "block";
	objContainer.style.display = "none";

	// �첽����ģ����������
	setTimeout(NodeSearch,100);
}

/*************************************************
 * ģ������
 */
function NodeSearch()
{
	var countResult = 0;
	var objLabel = document.all.item("lblNode");
	if(objLabel != null)
	{
		// ֻ��һ���ڵ�����
		if(objLabel.length == null || objLabel.length == undefined)
		{
			if(objLabel.innerHTML.indexOf(text) > -1)
			{
				NodeShow(objLabel.node);
				countResult++;
			}
			else
			{
				NodeHidden(objLabel.node);
			}
		}
		// �ڵ�Ϊ��������
		else
		{
			for(var i = 0; i < objLabel.length; i++)
			{
				if(objLabel[i].innerHTML.indexOf(text) > -1)
				{
					NodeShow(objLabel[i].node);
					countResult++;
				}
				else
				{
					NodeHidden(objLabel[i].node);
				}
			}
		}
	}
	
	if(countResult > 0)
	{
		// ��Ŀ¼����ʾ�����صȴ���ʾ
		objWaitting.style.display = "none";
		objContainer.style.display = "block";
	}
	else
	{
		objWaitting.innerHTML = "<font color='#FF0000'>��ƥ����Ŀ</font>";
		objContainer.style.display = "block";
	}
}

/*************************************************
 * �������������
 */
function ClearSearch()
{
	objText = document.all.item("txtSearch");
	objWaitting = document.all.item("SearchWaitting");
	
	objText.value = "";
	objWaitting.style.display = "none";
}

/*************************************************
 * ��ѡ������һ��
 */
function InsertRow(table,node,type,key,value,text)
{
	var content = "";
	content += "<table class=\"mouseout\" onclick=\"javascript:DeleteRow('" + table + "','" + node + "','" + key + "');\" ";
	content += " onmouseover=\"javascript:this.className='mouseover';\" onmouseout=\"javascript:this.className='mouseout';\" ";
	content += " onmousedown=\"javascript:this.className='mousedown';\" ";
	content += " width=\"100%\" height=\"22\">";
	content += "<tr><td width=\"20\">";
	content += "<img src=\"" + imagesDir + type + ".gif\">";
	content += "</td><td>" + text + "</td></tr></table>";
	
	var objTable = document.all.item(table);
	var objTr = objTable.insertRow();
	
	var objTd = objTr.insertCell();
	objTd.innerHTML = content;
	objTd.key = key;
	objTd.value = value;
	objTd.name = name;
}

/*************************************************
 * ��ѡ��ɾ��һ��
 */
function DeleteRow(table,node,key)
{
	var objTable = document.all.item(table);
	var objCheck = document.all.item(node + "_check");
	for(var i = 0; i < objTable.rows.length; i++)
	{
		if(objTable.rows[i].cells[0].key == key)
		{
			objTable.deleteRow(i);
			objCheck.checked = false;
			return;
		}
	}
}

/*************************************************
 * �����ѡ��Ŀ�б�
 */
function ClearRow(table)
{
	var objTable = document.all.item(table);
	for(var i = 0; i < objTable.rows.length; i++)
	{
		objTable.deleteRow(i);
		i = i - 1;
	}
}

/*************************************************
 * �����ѡ��Ŀѡ���
 */
function ClearCheck()
{
	var objCheck = document.getElementsByName("check");
	for(var i = 0; i < objCheck.length; i++)
	{
		if(objCheck[i].checked == true)
		{
			objCheck[i].checked = false;
		}
	}
}

/*************************************************
 * �����հ�ť
 */
function btnClearAll_Click()
{
	// ����б�
	ClearRow("tblSelected");
	
	// ���ѡ���
	ClearCheck();
}

/*************************************************
 * ���ȫѡ��ť���˴���ȫѡ���ܻ��ǽ�����������ѡ�����ȫѡ��ע���˴��Ƿ�Ҫֻȫѡ��ǰ�ɼ��δչ��������Ҫ��Ҫѡ���أ���
 */
function btnSelectAll_Click()
{
	var objCheck = document.getElementsByName("check");
	for(var i = 0; i < objCheck.length; i++)
	{
		var objNode = document.all.item(objCheck[i].node);
		if(objNode.style.display != "none" && objCheck[i].checked == false)
		{
			objCheck[i].checked = true;
			InsertRow("tblSelected",objCheck[i].node,objCheck[i].nodetype,objCheck[i].key,objCheck[i].value,objCheck[i].text);
		}
	}
}

/*************************************************
 * �����ѡ���ѡ��
 */
function Check_Click(objCheck)
{
	if(checkType == "radio")
	{
		// ����ǵ�ѡ����������б�
		ClearRow("tblSelected");
	}
	
	if(objCheck.checked == true)
	{
		InsertRow("tblSelected",objCheck.node,objCheck.nodetype,objCheck.key,objCheck.value,objCheck.text);
	}
	else if(objCheck.checked == false)
	{
		DeleteRow("tblSelected",objCheck.node,objCheck.key);
	}
}

/*************************************************
 * ��ʼ����ѡ��Ŀ�б� 
 */
function InitSelected()
{
	ClearRow("tblSelected");
	var objCheck = document.getElementsByName("check");
	for(var i = 0; i < objCheck.length; i++)
	{
		if(objCheck[i].checked == true)
		{
			InsertRow("tblSelected",objCheck[i].node,objCheck[i].nodetype,objCheck[i].key,objCheck[i].value,objCheck[i].text);
		}
	}
}

/*************************************************
 * ���ȷ����ȡ�÷���ֵ 
 */
function Return()
{
	var rtnValue = "";
	var rtnText = "";
	
	var objCheck = document.getElementsByName("check");
	for(var i = 0; i < objCheck.length; i++)
	{
		if(objCheck[i].checked == true)
		{
			rtnValue += objCheck[i].value + ",";
			rtnText += objCheck[i].text + ",";
		}
	}
	
	if(rtnValue.length > 0) rtnValue = rtnValue.substring(0,rtnValue.length - 1);
	if(rtnText.length > 0) rtnText = rtnText.substring(0,rtnText.length - 1);
	window.returnValue = rtnValue + "|" + rtnText;
	window.close();
}
// ===


