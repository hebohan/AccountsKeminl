//树外部调用接口
//youqing 2011-10-26 21:21:09

//运营管理组织机构树多选(部门域、部门ID域)
function SelAllOrgMulti(selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "checkbox";
        var Method = "getOrgTree";
        var HasSel = selValueID;
        var rtn = SelTree(RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("请检查:" + selNameID + "," + selValueID);
    }
}
//运营管理组织机构树单选(部门域、部门ID域)
function SelAllOrgSingle(selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "radio";
        var Method = "getOrgTree";
        var HasSel = selValueID;
        var rtn = SelTree(RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("请检查:" + selNameID + "," + selValueID);
    }
}


//运营管理人员树多选(人员域、人员ID域)
function SelAllUserMulti(selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "checkbox";
        var Method = "getUserTree";
        var HasSel = selValueID;
        var rtn = SelTree(RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("请检查:" + selNameID + "," + selValueID);
    }
}

//运营管理人员树单选(人员域、人员ID域)
function SelAllUserSingle(selNameID, selValueID) {
    if (document.getElementById(selNameID) && document.getElementById(selValueID)) {
        var RootID = "7C500081-3CDD-4A1F-A58A-44D144714D0C";
        var ItemType = "radio";
        var Method = "getUserTree";
        var HasSel = selValueID;
        var rtn = SelTree(RootID, ItemType, Method, HasSel);
        if (rtn != "" && rtn != undefined) {
            document.getElementById(selNameID).value = rtn.split('|')[0];
            document.getElementById(selValueID).value = rtn.split('|')[1];
        }
    }
    else {
        alert("请检查:" + selNameID + "," + selValueID);
    }
}



//树选择基础方法；(根节点ID、单选/多选、哪类树[人员、机构或其他]、前台已选择项目)
function SelTree(_RootID, _ItemType, _Method, _hasSel) {

    var ua = navigator.userAgent.toLowerCase(); //客户端浏览器信息
    var size = "";
    if (window.ActiveXObject)//判断IE浏览器     
    {
        size = ua.match(/msie ([\d.]+)/)[1]; //得出IE的版本大小
    }
    if (size == "6.0") {
        var rtn = window.showModalDialog("../../Share/SelectTree/Tree.html?RootID=" + _RootID + "&ItemType=" + _ItemType + "&getMethod=" + _Method + "&HasSel=" + _hasSel, window, "dialogWidth:606px;dialogHeight:525px");
    }
    else {
        var rtn = window.showModalDialog("../../Share/SelectTree/Tree.html?RootID=" + _RootID + "&ItemType=" + _ItemType + "&getMethod=" + _Method + "&HasSel=" + _hasSel, window, "dialogWidth:600px;dialogHeight:480px");
    }
    return rtn;
}