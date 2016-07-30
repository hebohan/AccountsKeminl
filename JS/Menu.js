
function OpenNewForm()
{
	popWinCommon("SuperMenuPanel.aspx?pParentId="+document.all("hParentid").value+"&pCommand=A")
}
function OpenEditForm()
{
	if(hasChecked(document.all("divgrid")) && isCheckOne(document.all("divgrid")))
	{
		popWinCommon("SuperMenuPanel.aspx?pMenuId="+getCheckVal("chkbox")+"&pParentId="+document.all("hParentid").value+"&pCommand=E")
	}
	else
	{
		alert("请选择一个菜单进行修改！");
	}	
}
function DelCheck()
{
	if(hasChecked(document.all("divgrid")))
	{
		document.all("hdelid").value = getCheckVal("chkbox");
		return true;
	}
	else
	{
		alert("请至少选择一个需要删除的菜单！");
		return false;
	}
}
function MenuUp()
{
	window.location = "SuperMenuList.aspx?pParentId=" + document.all("hReturnid").value;
}
 function OnOpenTree()
{
    document.all.item("SuperMenuId").value="";
    document.all.item("rtnValue").value="";
    if(!isCheckOne(divgrid))
    {alert('请选择一项!');return false;}
    else
    {
        var itemId = getCheckVal("chkbox").split(',');
        var url = document.URL.split('?');
        var strurl="";
        if(url.length>1){
          strurl=url[1]+"&SuperMenuId="+itemId[0];
        }
        else
        {
          strurl="SuperMenuId="+itemId[0];
        }
        var rtn = OpenTree2("../../components/generaltree/","GetmenuContain",strurl);
        if(rtn)
        {
            var rtn1=rtn.split('|');
            if(rtn1.length>1)
            {
                document.all.item("rtnValue").value =rtn1[0];
                document.all.item("SuperMenuId").value=itemId[0];
            }
            return true;
        }
        return false;
    }
}
