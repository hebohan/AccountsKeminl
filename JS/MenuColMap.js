function trim(str)
{
    for(var i = 0; i<str.length && str.charAt(i) == " "; i++);
    for(var j = str.length; j>0 && str.charAt(j-1) == " "; j--) ;
    if(i > j) return "";
    return str.substring(i,j);  
}

/*
//�˵��ͻ�����ʾ��֮���ӳ��
*/
function PopSelect()
{
    var tblname = document.all("TxtTableName").value;
    if(tblname == null || trim(tblname).length == 0)
    {
        alert("��ָ����ʾ������������");
        document.all("TxtTableName").value = "";
        document.all("TxtTableName").focus();
        return;
    }
    var vsRe = OpenTree2("../../components/generaltree/","SqlTableCol","tablename=" + tblname + "&selecteditem=" + ItemValue() + "&checktype=checkbox");
    if(vsRe != undefined && vsRe != "")
    {
        var param = vsRe.split('|');
        if(param.length == 2)
        {
            vsRe = param[0];
            Parse(vsRe);
        }
    }
}

function ItemValue()
{
    var itemstr = "";
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        var list = new Array();
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var objTableName = objTable.rows[i].cells[0];
            var objColName = objTable.rows[i].cells[1];
            if(itemstr.length == 0)
            {
                itemstr = objTableName.children[0].value + "$" + objColName.children[0].value;
            }
            else
            {
                itemstr = itemstr + "|" + objTableName.children[0].value + "$" + objColName.children[0].value;
            }
        }
    }
    return itemstr;
}

function Parse(vsRe)
{
    DelExcrescent(vsRe);
    //
    var cols = vsRe.split(',');
    if(cols.length > 0)
    {
        var key = "";
        for(var i = 0; i < cols.length; i++)
        {
            var tablename;
            var colname;
            var data = cols[i].split('$');
            if(data.length == 2)
            {
                tablename = data[0];
                colname = data[1];
                InsertRow(tablename, colname, i);
            }
        }
    }
}

function InsertRow(tablename, colname, index)
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        if(Validate(colname, index))
        {
            return;
        }
        var objTR = objTable.insertRow();
        var objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "25%";
        objTitle.innerHTML = "<input id=\"TableName\" name=\"TableName\" class=\"detail_edit\" readonly value=\"" + tablename + "\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "20%";
        objTitle.innerHTML = "<input id=\"ColName\" name=\"ColName\" class=\"detail_edit\" readonly value=\"" + colname + "\" />";

        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "35%";
        objTitle.innerHTML = "<input id=\"DisplayName\" name=\"DisplayName\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"����ʾ����\" />";

        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "8%";
        objTitle.innerHTML = "<input id=\"DisplayPercent\" name=\"DisplayPercent\" class=\"detail_edit\" style=\"width:70%\" v_type=\"int\" v_must=\"1\" v_name=\"��ʾ�ٷֱ�\" />%";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "8%";
        objTitle.innerHTML = "<input id=\"SortNum\" name=\"SortNum\" class=\"detail_edit\" value=\"" + index + "\" v_type=\"int\" v_must=\"1\" v_name=\"��ʾ˳��\" />";

        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "4%";
        objTitle.innerHTML = "&nbsp;<img src=\"../../images/shanchu.gif\" alt=\"ɾ����ʾ��\" style=\"cursor:hand\" onclick=\"javascript:DelRow(this);\" />";
    }
}

function Validate(colname, index)
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var objcol = objTable.rows[i].cells[1];
            if(objcol.children[0].value == colname)
            {
                objTable.rows[i].cells[4].children[0].value = index;
                return true;
            }
        }
    }
    return false;
}

function DelExcrescent(vsRe)
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        var list = new Array();
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var objcol = objTable.rows[i].cells[1];
            if(Detect(vsRe, objcol.children[0].value) == false)
            {
                list.push(i);
            }
        }
        if(list.length > 0)
        {
            while(list.length > 0)
            {
                var index = list.pop();
                objTable.deleteRow(index);
            }
        }
    }
}

function Detect(vsRe, colname)
{
    var cols = vsRe.split(',');
    if(cols.length > 0)
    {
        for(var i = 0; i < cols.length; i++)
        {
            var data = cols[i].split('$');
            if(data.length == 2)
            {
                if(colname == data[1])
                {
                    return true;
                }
            }
        }
    }
    return false;
}

function DelRow(objimg)
{
    var row = objimg.parentElement.parentElement;
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        objTable.deleteRow(row.rowIndex);
    }
}

function CheckSave()
{
    if(!ValidTableName())
    {
        alert("��ʾ��ֻ��ȡ��һ����һ�ı���ӵ�һ����ѡ��");
        document.body.focus();
        return false;
    }
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null && objTable.rows.length > 0)
    {
        if(!check(form1))
        {
            return false;
        }
    }
    else
    {
        return false;
    }
    //var percent = GetPercent();
    //if(percent != 100 && percent != 95)
    //{
   //     alert("�����õ���ʾ�ٷֱ�֮��Ϊ" + percent + "��\r\n���ҳ�����ѡ���뽫�ٷֱ�֮����Ϊ95��\r\n���ҳ�治��Ҫ����ѡ���뽫�ٷֱ�֮����Ϊ100��\r\n��������ʾ��İٷֱȣ�");
   //     document.body.focus();
  //      return false;
   // }
    return true;
}

function GetPercent()
{
    var percent = parseInt("0");
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var objcol = objTable.rows[i].cells[3];
            percent += parseInt(objcol.children[0].value);
        }
    }
    return percent;
}

function ValidTableName()
{
    var tmp = "";
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var objcol = objTable.rows[i].cells[0];
            if(tmp.length == 0)
            {
                tmp = objcol.children[0].value;
            }
            else
            {
                if(tmp != objcol.children[0].value)
                {
                    return false;
                }
            }
        }
    }
    return true;
}

