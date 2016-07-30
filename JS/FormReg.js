/*
 * �ֹ���ע��
**/
function SaveCheck()
{
    if(!check(form1))
    {
        return false;
    }
    if(!CheckActionId())
    {
        return false;
    }
    if(document.all("CheckFormId").disabled == false)
    {
        if(!CheckFormIdVal("no"))
        {
            return false;
        }
    }
    return true;
}

function CheckActionId()
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
                    alert("��" + parseInt(i + 1) + "�����" + parseInt(j + 1) + "�еı��ز��������ظ������޸ģ�");
                    nextotd.children[0].focus();
                    nextotd.children[0].select();
                    return false;
                }
            }
        }
    }
    return true;
}

function CheckFormIdVal(flag)
{
    var id = document.all("TxtFormId").value;
    if(id == "")
    {
        document.all("TxtFormId").focus();
        return false;
    }
    var rlt = window.showModalDialog("CheckFormId.aspx?FormId=" + id, window, "dialogWidth:1px;dialogHeight:1px");
    if(rlt == "-1")
    {
        alert("������Ϊ���ַ�����δ���壡");
        document.all("TxtFormId").focus();
        return false;
    }
    if(rlt == "true")
    {
        alert("�������Ѿ����ڣ������¶��壡");
        document.all("TxtFormId").focus();
        return false;
    }
    if(rlt == "false" && flag == "yes")
    {
        alert("��������Ч��");
        document.all("TxtFormId").focus();
        return true;
    }
    return true;
}

function OrgTree()
{
    var item = document.all("TxtOrganizeId").value;
    item = item.replace(',', '');
    var vsRe = OpenTree2('../../components/generaltree/','OrgTreeView','selecteditem=' + item + '&checktype=radio');
    if(vsRe != undefined)
    {
        var orgs = vsRe.split('|');
        if(orgs.length == 2)
        {
            document.all("TxtOrganizeId").value = orgs[0];
            document.all("TxtOrganizeName").value = orgs[1];
        }
    }
    //document.all("TxtTableName").focus();
}

function AddFieldRow() {
    var objTable = document.getElementById("FieldList");
    if (objTable != undefined && objTable != null) {
        var objTR = objTable.insertRow();
        var objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "10%";
        objTitle.innerHTML = "&nbsp;<input type=\"checkbox\" id=\"chkbox\" name=\"chkbox\" />";

        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "40%";
        objTitle.innerHTML = "<input id=\"FieldName\" name=\"FieldName\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"����\" />";

        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "50%";
        objTitle.innerHTML = "<input id=\"FieldCaption\" name=\"FieldCaption\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"��������\" />";

        document.body.focus();
    }
}

function RemoveFieldRow() {
    var objTable = document.getElementById("FieldList");
    if (objTable != undefined && objTable != null) {
        for (var i = 0; i < objTable.rows.length; i++) {
            var otd = objTable.rows[i].cells[0];
            if (otd.children[0].checked) {
                objTable.deleteRow(i);
                if (i > 0) {
                    i = i - 1;
                }
            }
        }
    }
    document.body.focus();
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
        objTitle.style.width = "20%";
        objTitle.innerHTML = "<input id=\"ActionId\" name=\"ActionId\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"���ز�������\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "28%";
        objTitle.innerHTML = "<input id=\"ActionName\" name=\"ActionName\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"���ز�������\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "40%";
        objTitle.innerHTML = "<input id=\"ActionDesc\" name=\"ActionDesc\" class=\"detail_edit\" v_type=\"string\" v_must=\"0\" v_name=\"���ز�������\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "8%";
        objTitle.innerHTML = "<input id=\"SortNum\" name=\"SortNum\" class=\"detail_edit\" v_type=\"int\" v_must=\"1\" value=\"" + objTable.rows.length + "\" v_name=\"�����\" />";
        
        document.body.focus();
    }
}
function AddMenuRow()
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
    {
        var objTR = objTable.insertRow();
        var objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "6%";
        objTitle.innerHTML = "&nbsp;<input type=\"checkbox\" id=\"chkbox\" name=\"chkbox\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "25%";
        objTitle.innerHTML = "<input id=\"TagName\" name=\"TagName\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"��ǩ����\"  />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "15%";
        objTitle.innerHTML = "<input id=\"PageUrl\" name=\"PageUrl\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"ҳ���ַ\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "20%";
        objTitle.innerHTML = "<input id=\"PageParameter\" name=\"PageParameter\" class=\"detail_edit\" v_type=\"string\" v_must=\"0\" v_name=\"ҳ�����\"/>";
         
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "14%";
        objTitle.innerHTML = "<input id=\"SortNum\" name=\"SortNum\" class=\"detail_edit\" v_type=\"int\" v_must=\"1\" v_name=\"�����\"  value=\"" + objTable.rows.length + "\" v_name=\"�����\" />";
        
        document.body.focus();
    }
}
function RemoveRow()
{
    var objTable = document.getElementById("DisplayItem");
    if(objTable != undefined && objTable != null)
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
    document.body.focus();
}


function AddTableRow()
{
    var objTable = document.getElementById("TableList");
    if(objTable != undefined && objTable != null)
    {
        var objTR = objTable.insertRow();
        var objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "10%";
        objTitle.innerHTML = "&nbsp;<input type=\"checkbox\" id=\"chkbox\" name=\"chkbox\" />";
        
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "40%";
        objTitle.innerHTML = "<input id=\"TableName\" name=\"TableName\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"����\" />";
       
        objTitle = objTR.insertCell();
        objTitle.className = "inputtdline";
        objTitle.style.width = "50%";
        objTitle.innerHTML = "<input id=\"TableCaption\" name=\"TableCaption\" class=\"detail_edit\" v_type=\"string\" v_must=\"1\" v_name=\"��������\" />";
       
        document.body.focus();
    }
}

function RemoveTableRow()
{
    var objTable = document.getElementById("TableList");
    if(objTable != undefined && objTable != null)
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
    document.body.focus();
}