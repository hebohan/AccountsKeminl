
/*****************************************************
 *****************************************************
 * 树形目录的方法脚本集
 * LiChun
 *****************************************************
 ****************************************************/
 
/*************************************************
 * 创建并初始化XML对象（成员变量）
 */
var treeXml = null;

try
{
	treeXml = new ActiveXObject("Microsoft.XMLDOM");
	treeXml.async = false;	// 设置同步装载数据（默认是异步）
	treeXml.loadXML(window.dialogArguments.document.all.item("TreeXmlData").value);	// 载入数据
}
catch(ex)
{
	alert("错误：" + ex.description);
}


/*************************************************
 * 根节点元素，树形目录的基本属性（树形目录的各种设置可以在根节点元素中设置）
 */
var rootNode = treeXml.documentElement;

var treeType = rootNode.getAttribute("treetype");	// 树类型（power,user）
if(treeType == null || treeType == undefined || treeType == "")
	treeType = "all";

var checkType = rootNode.getAttribute("checktype");	// 选择类型（radio,checkbox）
if(checkType == null || checkType == undefined || checkType == "")
	checkType = "checkbox";

var imagesDir = rootNode.getAttribute("imagesdir");	// 图片目录
if(imagesDir == null || imagesDir == undefined || imagesDir == "")
	imagesDir = "../Images/Tree/";

rootNode.setAttribute("view","");	// 将根节点的显示序列附加到属性


/*************************************************
 * 定义输出的树形目录HTML代码
 */
var treeHtml = "";

/*************************************************
 * 刷新树形目录
 */
function ReloadTree()
{
	document.all.item("root_child").innerHTML = "载入中...";
	
	// 异步调用输出树形目录Html代码
	setTimeout(GetTreeView,100);
}

/*************************************************
 * 输出树形目录Html代码
 */
function GetTreeView()
{
	if(checkType=="checkbox")
	{
		document.all("btnSelectAll").style.display = "";
	}
	// 根节点元素的实体
	treeHtml = "{" + rootNode.nodeName + "_child}";
	GetTreeHtml(rootNode);
	document.all.item("root_child").innerHTML = treeHtml;
	InitSelected();	// 初始化已选列表
}


/*************************************************
 * 获取子节点的递归
 * 参数：parentNode-当前子节点的根节点
 * 返回值：输出的HTML
 */
function GetTreeHtml(parentNode)
{
	// 替换当前模版位
	treeHtml = treeHtml.replace("{" + parentNode.nodeName + "_child}",GetNodeHtml(parentNode));
	
	var nodes = parentNode.childNodes;
	
	for(var i = 0; i < nodes.length; i++)
	{
		GetTreeHtml(nodes[i]);
	}
	
	return treeHtml;
}


/*************************************************
 * 获取某个节点下所有子节点的列表
 * 参数：parentNode-当前子节点的根节点
 * 返回值：输出的HTML
 */
function GetNodeHtml(parentNode)
{
	var nodes = parentNode.childNodes;
	var nodeHtml = "";
	
	for(var i = 0; i < nodes.length; i++)
	{
		// 当前节点的信息，先判断是否顶节点或底节点，其中顶节点又要判断是否尾节点
		var currentType = "2";	// 列表项
		
		// 有子节点的列表项
		if(nodes[i].childNodes.length > 0)
		{
			currentType = "3";
		}
		
		// 顶节点
		if(i == 0)
		{
			if(nodes[i].nodeName == rootNode.nodeName)
			{
				currentType = "9";	// 根节点
			}
			else
			{
				if(nodes[i].childNodes.length > 0)
					currentType = "4";	// 顶节点
				else
					currentType = "6";	// 无子节点的顶节点
			}
		}

		if(i == nodes.length - 1)
		{
			if(nodes[i].childNodes.length > 0)
				currentType = "5";	// 尾节点
			else
				currentType = "7";	// 无子节点的尾节点
		}

		// 节点显示序列字符串
		var currentView = GetNodeView(parentNode.getAttribute("view"),currentType);
		nodes[i].setAttribute("view",currentView);	// 将当前显示序列附加到当前节点属性
		
		// ===========================================
		// 输出节点的Html代码
		nodeHtml += "<div id=\"" + nodes[i].nodeName + "\" parent=\"" + parentNode.nodeName + "\"> \n";
		nodeHtml += "	<table> \n";
		nodeHtml += "		<tr height=\"20\"> \n";
		
		// 根据显示序列输出HTML
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
		
		// 输出选项框
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
		
		// 输出图标
		nodeHtml += "\t\t\t<td width=\"20\"><img src=\"" + imagesDir + nodes[i].getAttribute("type") + ".gif\"></td> \n";
		
		// 输出文本
		nodeHtml += "\t\t\t<td id=\"lblNode\" node=\"" + nodes[i].nodeName + "\">" + nodes[i].getAttribute("text") + "</td> \n";
		
		nodeHtml += "		</tr> \n";
		nodeHtml += "	</table> \n";
		nodeHtml += "</div> \n";
		
		nodeHtml += "<div id=\"" + nodes[i].nodeName + "_child\" count=\"" + nodes[i].childNodes.length + "\" style=\"display: none\"> \n";
		nodeHtml += "{" + nodes[i].nodeName + "_child}";	// 输出该节点的子节点模版位
		nodeHtml += "</div> \n";
	}
	
	return nodeHtml;
}

/*************************************************
 * 输出节点显示字串
 * null（0,空白）line（1,竖线）list（2,列表项目）plus（3,三叉节点）top（4,顶节点）bottom（5,尾节点）
 * nodetop（6,无子节点的顶节点）nodebottom（7,无子节点的尾节点）root（9,根节点）
 */
function GetNodeView(parentView,currentType)
{
	var currentView = "";

	for(var i = 0; i < parentView.length; i++)
	{
		// 假如父节点不是尾节点或空白，则继承父节点信息，输出竖线
		if(parentView.substring(i,i + 1) != "5" && parentView.substring(i,i + 1) != "0")
		{
			currentView += "1";
		}
		// 如果是尾节点或空白，则输出空白
		else
		{
			currentView += "0";
		}
	}

	// 假如父节点是根，则不必留空，如果不是根，则要空一列
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
 * 点击可以展开的节点
 */
function NodeClick(objNodeImage,node)
{
	// 切换 + 和 - 的图片，并控制子项可见性
	var objChild = document.all.item(node + "_child");
	if(objNodeImage.src.indexOf("plus") >= 0)
	{
		objNodeImage.src = objNodeImage.src.replace("plus","minus");
		objChild.style.display = "block";
		
		// 将下一级的子项设置为可见
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
 * 向下递归展开所有子节点的方法
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
 * 向下递归收起所有子节点的方法
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
 * 收起第一层以下所有子节点的方法
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
 * 向上递归展开所有父节点的方法
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
 * 显示任意一个节点
 */
function NodeShow(node)
{
	var objNode = document.all.item(node);
	objNode.style.display = "block";
	
	// 递归展开其所有父节点
	NodeExpand(node);
	
	// 收起子节点
	var objChild = document.all.item(node + "_child");
	var objImage = document.all.item(node + "_image");
	objChild.style.display = "none";
	if(objImage != null && objImage != undefined && objImage.src.indexOf("minus") > -1)
	{
		objImage.src = objImage.src.replace("minus","plus");
	}
}

/*************************************************
 * 隐藏任意一个节点
 */
function NodeHidden(node)
{
	var objNode = document.all.item(node);
	objNode.style.display = "none";
}

/*************************************************
 * 点击搜索按钮
 */
var text = "";	// 搜索字符串
function btnSearch_Click(txtSearch,SearchWaitting,treeContainer)
{
	objText = document.all.item("txtSearch");
	objWaitting = document.all.item("SearchWaitting");
	objContainer = document.all.item("root_child");
	MyTrim(objText);
	text = objText.value;
	
	// 先将目录树隐藏，显示等待提示
	objWaitting.innerHTML = "<font color='#0000FF'>正在搜索...</font>";
	objWaitting.style.display = "block";
	objContainer.style.display = "none";

	// 异步调用模糊搜索过程
	setTimeout(NodeSearch,100);
}

/*************************************************
 * 模糊搜索
 */
function NodeSearch()
{
	var countResult = 0;
	var objLabel = document.all.item("lblNode");
	if(objLabel != null)
	{
		// 只有一个节点的情况
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
		// 节点为数组的情况
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
		// 将目录树显示，隐藏等待提示
		objWaitting.style.display = "none";
		objContainer.style.display = "block";
	}
	else
	{
		objWaitting.innerHTML = "<font color='#FF0000'>无匹配项目</font>";
		objContainer.style.display = "block";
	}
}

/*************************************************
 * 将搜索界面清空
 */
function ClearSearch()
{
	objText = document.all.item("txtSearch");
	objWaitting = document.all.item("SearchWaitting");
	
	objText.value = "";
	objWaitting.style.display = "none";
}

/*************************************************
 * 已选项增加一行
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
 * 已选项删除一行
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
 * 清空已选项目列表
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
 * 清空已选项目选择框
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
 * 点击清空按钮
 */
function btnClearAll_Click()
{
	// 清空列表
	ClearRow("tblSelected");
	
	// 清空选择框
	ClearCheck();
}

/*************************************************
 * 点击全选按钮（此处的全选可能会是将搜索出来的选项进行全选。注：此处是否要只全选当前可见项？未展开的子项要不要选中呢？）
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
 * 点击单选框或复选框
 */
function Check_Click(objCheck)
{
	if(checkType == "radio")
	{
		// 如果是单选，则先清空列表
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
 * 初始化已选项目列表 
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
 * 点击确定，取得返回值 
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


