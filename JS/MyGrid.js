// 查询选项控制
function OptionControl(objItem, isChecked)
{
	if(isChecked)
	{
		document.all.item(objItem).style.display = "block";
	}
	else
	{
		document.all.item(objItem).style.display = "none";
	}
}


// 通过外部块的滚动条滚动内部表格
var savedTop=0;
function Table_onScroll(objDiv,objTable)
{
	document.all.item(objTable).style.top = document.all.item(objDiv).scrollTop;
}

// 输出提示信息
document.write('<div id="tips" style="font-size: 10pt; position: absolute; height: 22px; z-index: 100; left: 100px; top: 100px; visibility: hidden; border: 1px solid #000080; background-color: #FFFFEC"></div>');
function showtips(strtips)
{
	if(tips.style.visibility == "hidden")
	{
		var x = document.body.scrollLeft + event.clientX;
		var y = document.body.scrollTop + event.clientY;
		tips.innerHTML = strtips;
		tips.style.left = x + 20;
		
		if ( y > 300)
		{
			tips.style.top = y - 20;
		}
		else
		{
			tips.style.top = y - 10;
		}
		
		tips.style.visibility = "visible";
	}

	if(strtips == undefined || strtips == "")
	{
		tips.style.visibility = "hidden";
	}
}

// 提示信息跟随鼠标移动
function movetips()
{
	if(tips.style.visibility == "visible")
	{
		var x = document.body.scrollLeft + event.clientX;
		var y = document.body.scrollTop + event.clientY;
		tips.style.left = x + 20;
		
		if ( y > 300)
		{
			tips.style.top = y - 20;
		}
		else
		{
			tips.style.top = y - 10;
		}
	}
}

// 全选
function SelectAll(label,checkbox,table)
{
	var objLabel = document.all.item(label);
	var objCheck = document.all.item(checkbox);
	if(objLabel.title == "false")
	{
		SwapLabel(label,"true");
		
		if(objCheck != null && objCheck.length != undefined)
		{
			var i = 0;
			for(i = 0; i < objCheck.length; i++)
			{
				objCheck[i].checked = true;
			}
		}
		else if(objCheck != null)
		{
			objCheck.checked = true;
		}
	}
	else
	{
		SwapLabel(label,"false");
		
		if(objCheck != null && objCheck.length != undefined)
		{
			var i = 0;
			for(i = 0; i < objCheck.length; i++)
			{
				objCheck[i].checked = false;
			}
		}
		else if(objCheck != null)
		{
			objCheck.checked = false;
		}
	}
	
	SwapRecordClass(checkbox,table);
}

// 判断并取消全选
function CancelAll(label,checkbox,table)
{
	SwapRecordClass(checkbox,table);
	
	var objLabel = document.all.item(label);
	var objCheck = document.all.item(checkbox);
	
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
			if(objCheck[i].checked == false)
			{
				SwapLabel(label,"false");
				return;
			}
		}
		
		SwapLabel(label,"true");
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == false)
		{
			SwapLabel(label,"false");
			return;
		}
		
		SwapLabel(label,"true");
	}
}

// 切换记录行样式
function SwapRecordClass(checkbox,table)
{
	var objCheck = document.all.item(checkbox);
	var objTable = document.all.item(table);
	
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
			if(objCheck[i].checked == true)
			{
				objTable[i].className = "MyGridTableSelectedRow";
			}
			else
			{
				objTable[i].className = "MyGridTableRow";
			}
		}
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == true)
		{
			objTable.className = "MyGridTableSelectedRow";
		}
		else
		{
			objTable.className = "MyGridTableRow";
		}
	}
}

// 切换全选标签样式
function SwapLabel(label,check)
{
	var objLabel = document.all.item(label);
	if(objLabel != null && objLabel.title != check)
	{
		if(check == "true")
		{
			objLabel.style.backgroundColor = "#000099";
			objLabel.style.color = "#FFFFFF";
			objLabel.title = "true";
		}
		else
		{
			objLabel.style.backgroundColor = "";
			objLabel.style.color = "";
			objLabel.title = "false"
		}
	}
}

// 判断是否有选择项
function IsHaveChecked(checkbox) {

	var objCheck = document.getElementById(checkbox);
	
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
			if(objCheck[i].checked == true) return true;
		}
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == true) return true;
	}
	
	return false;
}

// 判断是否全选
function IsAllChecked(checkbox)
{
	var objCheck = document.all.item(checkbox);
	
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
			if(objCheck[i].checked == false) return false;
		}
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == false) return false;
	}
	
	return true;
}

// 只允许一条记录被选中
function OnlyOne(checkbox)
{
    var count = 0;
	var objCheck = document.getElementById(checkbox);
	
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
			if(objCheck[i].checked == true) count++;
		}
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == true) count++;
	}

	if(count == 1)
		return true;
	else
		return false;
}

// 只允许一条记录被选中时，得到选中项复选框的值
function GetCheckedValue(checkbox)
{
	var objCheck = document.all.item(checkbox);
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
			if(objCheck[i].checked == true)
				return objCheck[i].value;
		}
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == true)
			return objCheck.value;
	}
}

function BeforeDelete()
{
	if(!IsHaveChecked("chkbox"))
	{
		alert("请选择要删除的记录！");
		document.body.focus();
		return false;
	}
	return window.confirm("是否确认要删除？");
}

function BeforeModify()
{
	if(!OnlyOne("chkbox"))
	{
		alert("请选择要修改的数据项！");
		document.body.focus();
		return false;
	}
	return true;
}

function PopWin(url,w,h)
{
	var vsRe = window.showModalDialog(url,window,"dialogwidth:" + w + "px; dialogheight:" + h + "px","status=no");
	if(vsRe == "true") Refresh();
}
function PopWin1(url,w,h)
{
	var vsRe = window.showModalDialog(url,window,"dialogwidth:" + w + "px; dialogheight:" + h + "px");
	if(vsRe == "true") document.location.reload();
}


//设置部门所用
function PopWin3(url,w,h,checkbox)
{
	if(OnlyOne(checkbox))
	{
		var checkitem = GetCheckedValue(checkbox);
		var vsRe = window.showModalDialog(url+"&SeqNum="+checkitem,window,"dialogwidth:" + w + "px; dialogheight:" + h + "px");
		if(vsRe == "true") Refresh();
		
	}else
	{
		alert("请选择一条记录!");
	}
}

// 只允许一条记录被选中时，得到表里任何一项的值
function GetStateValue(checkbox,State)
{
	var objCheck = document.all.item(checkbox);
	var objState= document.all.item(State);
	
	if(objCheck != null && objCheck.length != undefined)
	{
		var i = 0;
		for(i = 0; i < objCheck.length; i++)
		{
		
			if(objCheck[i].checked == true)
				return objState[i].value;
				
		}
	}
	else if(objCheck != null)
	{
		if(objCheck.checked == true)
			return objState.value;
	}
}
		

function btnViolationTreat(ItemId,url1,Para)
{
	if(!OnlyOne(ItemId))
	{
		alert('请选择一条记录！');
		return false;
	}
	
	var strSeqNum=GetCheckedValue(ItemId);
	var url=url1+"&"+Para+"="+strSeqNum;
	
	window.location=url;
	return true;
}

function OpenDetail(url, width, height) {
    var x = (screen.width - width) / 2;

    var y = (screen.height - height) / 2;

    window.open(url, "", "height=" + height + ",width=" + width + ",toolbar=no,menubar=no,screenX=" + x + ",screenY=" + y + ",scrollbars=no, resizable=no,location=no, status=no");
}