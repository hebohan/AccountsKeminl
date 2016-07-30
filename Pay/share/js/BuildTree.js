//树形菜单基础js
//youqing 2011-10-26 18:32:01

//子项目联动 new
function sonElmOp(OpObj) {

    var OpObjChecked = getSecChild(OpObj).checked;
    var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
    document.getElementById("IfsonElmOp").checked = false;

    //遍历元素等级
    var ElmLevel = OpObj.parent.split('|').length - 1;
    var IsSameLevel = true;
    do {
        try {
            var NextObj = getNextObj(OpObj);
            var NextObjLevel = NextObj.parent.split('|').length - 1;
            if (ElmLevel < NextObjLevel) {
                //如果子项选择和主项不一致
                if (OpObjChecked != getSecChild(NextObj).checked) {
                    getSecChild(NextObj).click();
                }
                OpObj = NextObj;
            }
            else {
                IsSameLevel = false;
            }
        }
        catch (err) {
            IsSameLevel = false;
        }
    } while (IsSameLevel);

    document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
    return;


}





//增加到左侧项目 new； 不增加点击左侧备选文字到右侧的功能，防止误点击；
function addRight(i) {
    var OpObj; //获取操作对象
    var e = (event) ? event : window.event;
    if (window.event) {//IE
        e.cancelBubble = true;
    } else { //FF
        e.stopPropagation();
    }
    var oEvent = e.srcElement;
    
    
    //单选按钮时
    if (getSecChild(document.getElementById("left@_" + i)).type == "radio" && !getSecChild(document.getElementById("left@_" + i)).disabled) {
        var HasSelValTxtone = document.getElementById("HasSelValTxt").value.split(',');
        //已传值或选值
        if (HasSelValTxtone.length > 1) {
            for (var k = 0; k < HasSelValTxtone.length; k++) {
                if (HasSelValTxtone[k] != "" && document.getElementById("left@_" + HasSelValTxtone[k])) {

                    getSecChild(document.getElementById("left@_" + HasSelValTxtone[k])).checked = false;
                    document.getElementById("right@_" + HasSelValTxtone[k]).style.display = "none";
                }
            }
            document.getElementById("HasSelValTxt").value = "";
            document.getElementById("HasSelNameTxt").value = "";

        }
        else {
            document.getElementById("left@_" + i).checked = true;
            document.getElementById("right@_" + i).style.display = "block";
        }

    }


    //触发左侧栏目 选中/取消
    if (document.getElementById("left@_" + i)) {
        //选中左侧
        if (getSecChild(document.getElementById("left@_" + i)).checked) {
            if (document.getElementById("right@_" + i)) {
                document.getElementById("right@_" + i).style.display = "block";
                HasSelValTxt.value += document.getElementById("right@_" + i).value + ",";
                HasSelNameTxt.value += document.getElementById("right@_" + i).innerText.Trim() + ",";
            }
        }
        //取消选中左侧
        else {
            if (document.getElementById("right@_" + i)) {
                document.getElementById("right@_" + i).style.display = "none";
                HasSelValTxt.value = HasSelValTxt.value.replace(document.getElementById("right@_" + i).value + ",", "");
                HasSelNameTxt.value = HasSelNameTxt.value.replace(document.getElementById("right@_" + i).innerText.Trim() + ",", "");
            }
        }
    }



    //	        var OpObj; //获取操作对象
    //	        var e=(event) ? event : window.event;
    //	        if (window.event) {//IE
    //	            e.cancelBubble=true;
    //	        } else { //FF
    //	            e.stopPropagation();
    //	        }
    //	        var oEvent = e.srcElement;
    //	        
    //	        
    //	        //点击单选/多选按钮时；
    //	        if (oEvent.nodeName == "INPUT") {
    //	            OpObj = oEvent.parentElement;

    //	        }
    //	        else if (oEvent.nodeName == "IMG") {
    //	        return;
    //	        }
    //	        //点击文字时；
    //	        else {
    //	            OpObj = oEvent;
    //	            //单选多选按钮必须非disabled时，才能改写；
    //	            if (!getSecChild(OpObj).disabled) {
    //	                if (getSecChild(OpObj).checked) {
    //	                    getSecChild(OpObj).checked = false;
    //	                }
    //	                else {
    //	                    getSecChild(OpObj).checked = true;
    //	                }
    //	            }
    //	        }



    //子项目联动(此处应该加入是否联动的配置，让用户选择或后台配置)
    if (document.getElementById("IfsonElmOp") && document.getElementById("IfsonElmOp").checked) {
        var OpObj = document.getElementById("left@_" + i);
        sonElmOp(OpObj);

    }


    return;


    //	        //写右侧已选
    //	        writeSelected(OpObj);


    //	        //判定数据源是否是点击右侧已选区域项目
    //	        if (OpObj.id.indexOf("right@_") > -1) {
    //	            getSecChild(document.getElementById(OpObj.id.replace("right@_", "left@_"))).checked = false;
    //	        }


}
//清除右侧已选 new
function ClearRight() {
    var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
    document.getElementById("IfsonElmOp").checked = false;
    for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
        if (getSecChild(document.getElementById("leftLay").childNodes[i]).checked) {
            getSecChild(document.getElementById("leftLay").childNodes[i]).click();
        }
    }
    document.getElementById("HasSelValTxt").value = "";
    document.getElementById("HasSelNameTxt").value = "";

    document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
}

//全选左侧 new
function SelectLeftAll() {
    var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
    document.getElementById("IfsonElmOp").checked = false;
    for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
        if (!getSecChild(document.getElementById("leftLay").childNodes[i]).checked) {
            getSecChild(document.getElementById("leftLay").childNodes[i]).click();
        }
    }
    document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
}




//点击右侧已选项 new
function removeRight(i) {
    if (document.getElementById("left@_" + i)) {
        getSecChild(document.getElementById("left@_" + i)).checked = false;
        if (document.getElementById("right@_" + i)) {
            document.getElementById("right@_" + i).style.display = "none";
            HasSelValTxt.value = HasSelValTxt.value.replace(document.getElementById("right@_" + i).value + ",", "");
            HasSelNameTxt.value = HasSelNameTxt.value.replace(document.getElementById("right@_" + i).innerText.Trim() + ",", "");

        }
    }
}

//显示/隐藏子项
function ShowHiddenSon() {
    var OpObj; //获取操作对象
    var e = (event) ? event : window.event;
    if (window.event) {//IE
        e.cancelBubble = true;
    } else { //FF
        e.stopPropagation();
    }
    var oEvent = e.srcElement;
    //alert(oEvent.outerHTML);

    if (oEvent.src.indexOf("up.gif") > -1) {
        for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
            var sonElm = document.getElementById("leftLay").childNodes[i];
            //直属父节点ID
            var sonElmLastParentID = sonElm.parent.split('|')[sonElm.parent.split('|').length - 2];
            //alert(sonElmLastParentID+"--"+getNextObjByID(oEvent).value);
            if (sonElmLastParentID == getNextObj(oEvent).value) {
                sonElm.style.display = "block";
            }
        }
        oEvent.src = "../images/Tree/down.gif";
    }
    else if (oEvent.src.indexOf("down.gif") > -1) {

        for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
            var sonElm = document.getElementById("leftLay").childNodes[i];
            //alert(getNextObjByID(oEvent).outerHTML);
            if (sonElm.parent.indexOf(getNextObj(oEvent).value) > -1) {
                sonElm.style.display = "none";
                if (getFirstChild(sonElm).src.indexOf("down.gif") > -1) {
                    getFirstChild(sonElm).src = "../images/Tree/up.gif";
                }
            }
        }
        oEvent.src = "../images/Tree/up.gif";


    }
    else { } //none.gif时
}

//	    //隐藏子项
//	    function HidenSon() {
//	        var OpObj; //获取操作对象
//	        var e = (event) ? event : window.event;
//	        if (window.event) {//IE
//	            e.cancelBubble = true;
//	        } else { //FF
//	            e.stopPropagation();
//	        }
//	        var oEvent = e.srcElement;


//	        for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
//	            var sonElm = document.getElementById("leftLay").childNodes[i];

//	            
//	        }
//	        
//	        oEvent.onclick = "ShowSon()";
//	    }


//打开所有下拉 new
function OpenAll() {
    for (var i = 0; i < document.getElementById("leftLay").childNodes.length; i++) {
        document.getElementById("leftLay").childNodes[i].style.display = "block";
        if (getFirstChild(document.getElementById("leftLay").childNodes[i]).src.indexOf("up.gif") > -1) {
            getFirstChild(document.getElementById("leftLay").childNodes[i]).src = "../images/Tree/down.gif";
        }

    }
}


//打开所有下拉 new
function CloseAll() {
    var AllImg = document.getElementById("DownUpImg").value;
    if (AllImg != "") {
        for (var i = 0; i < AllImg.split('|').length - 1; i++) {
            var oneImg = document.getElementById(AllImg.split('|')[i]);
            if (oneImg.src.indexOf("down.gif") > -1) {
                oneImg.click();
            }
        }
    }
}




//获取右侧选中 new
function getAllSel() {

    var names = document.getElementById("HasSelNameTxt").value;
    var values = document.getElementById("HasSelValTxt").value;
    if (names.indexOf(",") > -1) {
        names = names.substring(0, parseInt(names.length) - 1);
        values = values.substring(0, parseInt(values.length) - 1);
    }

    window.returnValue = names + "|" + values;
    window.close();

}
//清空前台选项
function ClearFrontInput() {
    window.returnValue = "|";
    window.close();
}


//加载传入选中项目
function LoadHasSel() {
    var FrontValueID = getUrlvalue("HasSel");
    if (window.dialogArguments&&window.dialogArguments.document.getElementById(FrontValueID)) {
        //document.getElementById("HasSelValTxt").value = window.dialogArguments.document.getElementById(FrontValueID).value;
        var HasSelVal = window.dialogArguments.document.getElementById(FrontValueID).value;
        //alert(HasSelVal);
        var IfsonElmOpchecked = document.getElementById("IfsonElmOp").checked;
        document.getElementById("IfsonElmOp").checked = false;
        if (HasSelVal != "") {
            for (var i = 0; i < HasSelVal.split(',').length; i++) {
                var oneSelVal = HasSelVal.split(',')[i];
                if (oneSelVal != "" && document.getElementById("left@_" + oneSelVal)) {
                    getSecChild(document.getElementById("left@_" + oneSelVal)).click();
                }
            }
        }
        document.getElementById("IfsonElmOp").checked = IfsonElmOpchecked;
    }
}




//查找关键字
var NS4 = (document.layers);
var IE4 = (document.all);
var win = this;
var n = 0;

function searchKeywords() {
    var charCode = (window.event || e).keyCode;
    if (charCode == 13) {
        document.getElementById("searchBtn").click();
        //不能通过获取焦点重置按钮，因为此时焦点在搜索中的关键字上
    }
}


function findInPage(str) {
    if (str == "") {
        return;
    }


    //搜索前首先全部展开
    OpenAll();

    var txt, i, found;
    if (str == "" || str == "")
        return false;
    try {
        if (NS4) {
            if (!win.find(str))
                while (win.find(str, false, true))
                n++;
            else
                n++;
            if (n == 0) alert("没有选项和[ " + str + " ]" + "匹配，请输入其他关键字!");
        }
        if (IE4) {
            txt = win.document.body.createTextRange();
            //alert(win.document.body.outerHTML);
            //txt = document.getElementById("leftLay").createTextRange();
            for (i = 0; i <= n && (found = txt.findText(str)) != false; i++) {
                txt.moveStart("character", 1);
                txt.moveEnd("textedit");
            }
            if (found) {
                txt.moveStart("character", -1);
                txt.findText(str);
                txt.select();
                txt.scrollIntoView();
                n++;
            }
            else {
                if (n > 0) {
                    n = 0;
                    findInPage(str);
                }
                else
                    alert("没有选项和[ " + str + " ]" + "匹配，请输入其他关键字!");
            }
        }
        return false;
    }
    catch (Error) {
        alert("查找完毕！");
        n = 0;
        return false;
    }
}