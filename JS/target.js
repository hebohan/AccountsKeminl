//选择适配器
function SearchAdapter(w,h)
{
    var type = document.form1.all("BusinessType").value;
    var url = "SelectAdapter.aspx?BusinessType=" + type;
	var vsRe = window.showModalDialog(url,window,"dialogwidth:" + w + "px; dialogheight:" + h + "px");

	if(vsRe != undefined && vsRe != null && vsRe != "")
	{
	    var vals = vsRe.split('&');
	    if(vals.length == 4)
	    {
		    document.all("AdapterName").value = vals[0];
		    document.all("AdapterDesc").innerText = vals[1];
		    document.all("ParameterNote").innerText = vals[3];
		    
		    var pars = vals[2].split(',');
		    if(pars.length > 0)
		    {
		        var strs = "<table border=0 width='100%'>";
		        //
		        for(var i = 0; i < pars.length; i++)
		        {
		            if(pars[i] != "")
		            {
		                var inputHTML = "<input type='text' name='Parameter' style='width:100%' class='input150' />";
		                if(i % 2 == 0)
		                {
		                    strs += "<tr>";
		                }
		                strs += "<td class='labeltdline' style='width: 15%;'>" + pars[i] + "：</td>";
		                strs += "<td class='inputtdline' style='width: 35%;'>" + inputHTML + "</td>";
		                i++;
		                if(i < pars.length)
		                {
		                    strs += "<td class='labeltdline' style='width: 15%;'>" + pars[i] + "：</td>";
		                    strs += "<td class='inputtdline' style='width: 35%;'>" + inputHTML + "</td>";
		                }
		                else
		                {
		                    strs += "<td class='inputtdline' style='width: 15%;'></td>";
		                    strs += "<td class='inputtdline' style='width: 35%;'></td>";
		                }
		                if(i % 2 == 0)
		                {
		                    strs += "</tr>";
		                }
		            }
		        }
		        //
		        strs += "</table>";
		        document.all("divParameter").innerHTML = strs;
		    }
	    }
	}
}

//封装指标参数
function PackageParameter()
{
    var objs = document.getElementsByName("Parameter");
    if(objs.length > 0)
    {
        var vals = "";
        for(var i = 0; i < objs.length; i++)
        {
            if(objs[i].value != null && objs[i].value != "")
            {
                if(vals.length == 0)
                {
                    vals += objs[i].value;
                }
                else
                {
                    vals += "|" + objs[i].value;
                }
            }
            else
            {
                if(vals.length == 0)
                {
                    vals += "@";
                }
                else
                {
                    vals += "|@";
                }
            }
        }
        document.all("Parameters").value = vals;
    }
}

