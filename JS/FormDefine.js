
/*
 * �ֹ������Զ���
**/
function SaveCheck()
{
    if(!check(form1))
    {
        return false;
    }
    if(!CheckFieldName())
    {
        return false;
    }
    if(document.all("CheckTableName").disabled == false)
    {
        if(!CheckTableNameVal("no"))
        {
            return false;
        }
    }
    return true;
}

function CheckFieldName()
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var otd = objTable.rows[i].cells[1];
            for(var j = parseInt(i) + parseInt("1"); j < objTable.rows.length; j++)
            {
                var nextotd = objTable.rows[j].cells[1];
                if(otd.children[0].value == nextotd.children[0].value)
                {
                    alert("��" + parseInt(i + 1) + "�����" + parseInt(j + 1) + "�е��ֶ������ظ������޸ģ�");
                    nextotd.children[0].focus();
                    nextotd.children[0].select();
                    return false;
                }
            }
        }
    }
    return true;
}

function CheckTableNameVal(flag)
{
    var id = document.all("TxtTableName").value;
    if(id == "")
    {
        document.all("TxtTableName").focus();
        return false;
    }
    return true;
}

function AddRow()
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        var objTR = objTable.insertRow();
        var objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "4%";
        objTitle.innerHTML = "&nbsp;<input type=\"checkbox\" id=\"chkbox\" name=\"chkbox\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "26%";
        objTitle.innerHTML = "<input id=\"Field\" name=\"Field\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"�ֶ�����\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "40%";
        objTitle.innerHTML = "<input id=\"FieldTitle\" name=\"FieldTitle\" class=\"detail_edit\" v_type=\"string\" v_must=\"0\" v_name=\"�ֶ���������\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "20%";
        var HTMLStr = "<select id=\"FieldDataType\" name=\"FieldDataType\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"�ֶ�����\">";
        HTMLStr += "<option value=\"\" selected=true>��ѡ��</option>";
        HTMLStr += "<option value=\"varchar\">varchar</option>";
        HTMLStr += "<option value=\"text\">text</option>";
        HTMLStr += "<option value=\"int\">int</option>";
        HTMLStr += "<option value=\"datetime\">datetime</option>";
        HTMLStr += "<option value=\"float\">float</option>";
        HTMLStr += "<option value=\"image\">image</option>";
        HTMLStr += "</select>";
        objTitle.innerHTML = HTMLStr;
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "10%";
        objTitle.innerHTML = "<input id=\"FieldLength\" name=\"FieldLength\" class=\"detail_edit\"  v_must=\"0\" v_name=\"�ֶγ���\" />";
        
        document.body.focus();
    }
}

function RemoveRow()
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        var isSelected = false;
        for(var i = 0; i < objTable.rows.length; i++)
        {
            var otd = objTable.rows[i].cells[0];
            if(otd.children[0].checked)
            {
                isSelected = true;
                break;
            }
        }
        if(window.confirm("�Ƿ�ȷ��ɾ����"))
        {
            for(var i = 0; i < objTable.rows.length; i++)
            {
                var otd = objTable.rows[i].cells[0];
                if(otd.children[0].checked)
                {
                    objTable.deleteRow(i);
                    if(i > 0)
                    {
                        i = i - 1;
                    }
                }
            }
        }
    }
    document.body.focus();
}
