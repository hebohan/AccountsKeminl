//显示查询条件
function ShowSearchBar(divName, btnShow, btnHide)
{
    document.all(divName).style.display = "block";
    document.all(btnShow).style.display = "none";
    document.all(btnHide).style.display = "block";
    document.all('Grid1').style.height=document.body.clientHeight - 90;
    document.body.focus();
}
//隐藏查询条件
function HideSearchBar(divName, btnShow, btnHide)
{
    document.all(divName).style.display = "none";
    document.all(btnShow).style.display = "block";
    document.all(btnHide).style.display = "none";
    document.all('Grid1').style.height=document.body.clientHeight - 36;
    document.body.focus();
}
/* FlexCell数据装箱
// 如果使用FlexCell模板，模板XML文件放到隐藏域TmplXmlStr中
// 封装好的XML数据放到隐藏域ElementValue中
// 每个坐标的数据之间用","分隔
// 每个原数据的坐标与显示值间用"|"分隔
// 每个坐标的X轴与Y轴间用":"分隔
*/
function loadXml()
{
    var tmplXmlStr = document.all("TmplXmlStr").value;
    if(tmplXmlStr != null && tmplXmlStr != "")
    {
	    document.all("Grid1").LoadFromXMLString(tmplXmlStr);
	}
	//
	var dataStr = document.all("ElementValue").value;
	if(dataStr.length > 0)
	{
		var elemdata = dataStr.split(',');
		for(var i = 0; i < elemdata.length; i++)
		{
			var elemval = elemdata[i].split('|');
			if(elemval.length == 2)
			{
				var location = elemval[0].split(':');
				if(elemval[1] == "true")
				{
					document.all("Grid1").Cell(location[0], location[1]).Text = "1";
				}
				else if(elemval[1] == "false")
				{
					document.all("Grid1").Cell(location[0], location[1]).Text = "0";
				}
				else
				{
					document.all("Grid1").Cell(location[0], location[1]).Text = elemval[1];
				}
			}
		}
	}
	document.all("Grid1").ReadOnly = true;
	document.all('Grid1').style.height=document.body.clientHeight;
}

//打印预览
function preview()
{
	document.all("Grid1").PrintPreview();
	document.body.focus();
}

//导出Excel
function exportXls(filename)
{
    document.all("Grid1").ExportToExcel(filename);
    alert("导出成功！");
    document.body.focus();
}
