//���ⲿ���ýӿ� ר����Ӫ�����ⲿ����
//youqing 2011-11-2 10:54:59


//��Ӫ������֯��������ѡ(�����򡢲���ID��)
function SelAllOrgMulti(path,selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "checkbox";
        var Method = "getOrgTree";
        var HasSel = selValueID;
        var rtn = SelTree(path,RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("����:" + selNameID + "," + selValueID);
    }
}
//��Ӫ������֯��������ѡ(�����򡢲���ID��)
function SelAllOrgSingle(path, selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "radio";
        var Method = "getOrgTree";
        var HasSel = selValueID;
        var rtn = SelTree(path, RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("����:" + selNameID + "," + selValueID);
    }
}


//��Ӫ������Ա����ѡ(��Ա����ԱID��)
function SelAllUserMulti(path, selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "checkbox";
        var Method = "getUserTree";
        var HasSel = selValueID;
        var rtn = SelTree(path, RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("����:" + selNameID + "," + selValueID);
    }
}

//��Ӫ������Ա����ѡ(��Ա����ԱID��)
function SelContactUsers(path, selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "checkbox";
        var Method = "getUserTree";
        var HasSel = selValueID;
        var rtn = SelTree(path, RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("����:" + selNameID + "," + selValueID);
    }
}

//��Ӫ������Ա����ѡ(��Ա����ԱID��)
function SelAllUserSingle(path, selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "radio";
        var Method = "getUserTree";
        var HasSel = selValueID;
        var rtn = SelTree(path, RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("����:" + selNameID + "," + selValueID);
    }
}



//��ѡ�����������(�����attendance�ļ��е�·�������ڵ�ID����ѡ/��ѡ��������[��Ա������������]��ǰ̨��ѡ����Ŀ)
function SelTree(_path, _RootID, _ItemType, _Method, _hasSel) {

    var ua = navigator.userAgent.toLowerCase(); //�ͻ����������Ϣ
    var size = "";
    if (window.ActiveXObject)//�ж�IE�����     
    {
        size = ua.match(/msie ([\d.]+)/)[1]; //�ó�IE�İ汾��С
    }
    if (size == "6.0") {
        var rtn = window.showModalDialog(_path + "Attendance/Share/SelectTree/Tree.html?RootID=" + _RootID + "&ItemType=" + _ItemType + "&getMethod=" + _Method + "&HasSel=" + _hasSel, window, "dialogWidth:606px;dialogHeight:525px");
    }
    else {
        var rtn = window.showModalDialog(_path + "Attendance/Share/SelectTree/Tree.html?RootID=" + _RootID + "&ItemType=" + _ItemType + "&getMethod=" + _Method + "&HasSel=" + _hasSel, window, "dialogWidth:600px;dialogHeight:480px");
    }
    return rtn;
}