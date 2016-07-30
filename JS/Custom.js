var _oldColor;

function SetNewColor(source) {

    _oldColor = source.style.backgroundColor;

    source.style.backgroundColor = '#666666';



}

function SetOldColor(source) {

    source.style.backgroundColor = _oldColor;

}
//================== AJAX动态浮动框 AddBy Lizhaoch start===============================================
function showdiv() {
    var sid = event.srcElement.id;

    var ob = eval(document.getElementById("MovableDiv"));
    var ob2 = eval(document.getElementById("divDisable"));
    ob.style.top = (document.body.offsetHeight / 2 - 200) / 2;
    ob.style.left = (document.body.offsetWidth / 2 - 200) / 2;
    ob.style.display = '';
    ob2.style.display = '';
    $("#divDisable").fadeIn(800);
    $("#MovableDiv").slideDown(1200);
}
function showdivs(toplen, leftlen) {
//    var sid = event.srcElement.id;
    var ob = eval(document.getElementById("MovableDiv"));
    var ob2 = eval(document.getElementById("divDisable"));
    ob.style.top = (document.body.offsetHeight / 2 - toplen) / 2;
    ob.style.left = (document.body.offsetWidth / 2 - leftlen) / 2;
    // ob.style.display = '';
    //ob2.style.display = '';
    $("#divDisable").fadeIn(800);
    $("#MovableDiv").slideDown(1200);

}
function hidediv() {
    //    document.getElementById("MovableDiv").style.display = 'none';
    //   document.getElementById("divDisable").style.display = 'none';
    //   $("#MovableDiv").slideDown(2000);
    //   $("#divDisable").slideup(2000);
    $("#divDisable").fadeOut("1200");
    $("#MovableDiv").fadeOut("800");

}

function RegDrag(dragId, moveId) {
    if (moveId == "false")
        return;
    var drag = document.getElementById(dragId); //拖动的目标，即鼠标按下时的区域。    
    var target = document.getElementById(moveId); //移动的目标，即拖动时移动的区域。    
    target.style.position = "absolute"; //position属性设置为absulute才可正常移动。    
    var canMove = false; //是否处于可以拖动的状态。    
    var mouseDownX = 0, mouseDownY = 0; //记录鼠标按下时位置。    
    var mouseDownLeft = 0, mouseDownY = 0; //记录鼠标按下时移动目标的位置。    

    var DragConfig = {}; //创建一个对象用来管理事件。    
    DragConfig.startMove = function() {
        drag.setCapture(); //使得drag可以捕获全屏幕内的所有事件。    
        /*记录鼠标按下时的状态。*/
        mouseDownX = event.screenX;
        mouseDownY = event.screenY;
        mouseDownLeft = target.offsetLeft;
        mouseDownTop = target.offsetTop;
        canMove = true; //是否可以移动标识为true，使doMove方法有效。    
    }
    DragConfig.doMove = function() {
        if (canMove) {/*如果可以移动，则改变target的位置。*/
            target.style.left = mouseDownLeft + (event.screenX - mouseDownX);
            target.style.top = mouseDownTop + (event.screenY - mouseDownY);
        }
    }
    DragConfig.endMove = function() {
        drag.releaseCapture(); //释放drag的事件捕获。    
        canMove = false; //是束可以移动标识为false,doMove方法无效。    
    }

    drag.onmousedown = DragConfig.startMove; //将startMove方法绑定到onmousedown    
    drag.onmousemove = DragConfig.doMove; //将doMove方法绑定到onmousemove    
    drag.onmouseup = DragConfig.endMove; //将endMove方法绑定到onmouseup    
}
//================== AJAX动态浮动框 end===============================================
//================== 动态表格  AddBy Lizhaoch start===============================================

nereidFadeObjects = new Object();
nereidFadeTimers = new Object();
function nereidFade(object, destOp, rate, delta) {
    if (!document.all)
        return
    if (object != "[object]") {  //do this so I can take a string too
        setTimeout("nereidFade(" + object + "," + destOp + "," + rate + "," + delta + ")", 0);
        return;
    }
    clearTimeout(nereidFadeTimers[object.sourceIndex]);
    diff = destOp - object.filters.alpha.opacity;
    direction = 1;
    if (object.filters.alpha.opacity > destOp) {
        direction = -1;
    }
    delta = Math.min(direction * diff, delta);
    object.filters.alpha.opacity += direction * delta;
    if (object.filters.alpha.opacity != destOp) {
        nereidFadeObjects[object.sourceIndex] = object;
        nereidFadeTimers[object.sourceIndex] = setTimeout("nereidFade(nereidFadeObjects[" + object.sourceIndex + "]," + destOp + "," + rate + "," + delta + ")", rate);
    }
}

//================== 动态表格 end===============================================
//================== 人物属性 start===============================================
//在鼠标显示一个层，该层的内空为div2的内容
function showTip(userid, depth) {
    var usid = ""; var pres = "";
    if (userid == usid) return; else usid = userid;
    for (var i = 0; i < depth; i++) {
        pres += "../";
    }
    var userinfo = PostMeg("userid=" + userid + "&pres=" + pres, pres + "sys/organize/FrameUserInfoHandle.aspx");
    if (userinfo != null && userinfo != "") {
        var useris = userinfo.split(',');
        document.getElementById("tbName").innerText = useris[0];
        document.getElementById("tbDept").innerText = useris[1];
        document.getElementById("tbEmail").innerText = useris[2];
        document.getElementById("tbMobil").innerText = useris[3];
        document.getElementById("tbShortNum").innerText = useris[4];
        document.getElementById("tbTEL").innerText = useris[5];
        document.getElementById("tbZhiCheng").innerText = useris[6];
        document.getElementById("imgPerson").src = useris[7];
    }
    var div3 = document.getElementById('div3');
    div3.style.display = "block"; //div3初始状态是不可见的，设置可为可见 
    div3.style.left = event.clientX + 15; //鼠标目前在X轴上的位置，加10是为了向右边移动10个px方便看到内容 
    div3.style.top = event.clientY - 100;
    div3.style.position = "absolute"; //必须指定这个属性，否则div3层无法跟着鼠标动 
}
//关闭层div3的显示
function closeTip(depth) {
    var pres = "";
    for (var i = 0; i < depth; i++) {
        pres += "../";
    }
    var div3 = document.getElementById('div3');
    div3.style.display = "none";
    var useris = "姓名,所在部门,电子邮件,移动电话,证件号码,办公电话,职务," + pres + "images/oauser/nobody.jpg".split(',');
    document.getElementById("tbName").innerText = useris[0];
    document.getElementById("tbDept").innerText = useris[1];
    document.getElementById("tbEmail").innerText = useris[2];
    document.getElementById("tbMobil").innerText = useris[3];
    document.getElementById("tbShortNum").innerText = useris[4];
    document.getElementById("tbTEL").innerText = useris[5];
    document.getElementById("tbZhiCheng").innerText = useris[6];
    document.getElementById("imgPerson").src = useris[7];
}
//提交数据
function PostMeg(DataToSend, Url) {
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", Url, false);
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlhttp.send(DataToSend);
    return xmlhttp.responseText;
}
//================== 人物属性 end===============================================
//================== 部门进驻控制显示====================
function MyFun(n,txt) {
    var s = document.getElementById("tags");
    var t = s.childNodes.length;
    var li = document.createElement("li");
    li.innerHTML = txt;
    for (var i = 0; i < t; i++) {
        if (n == -1) {
            s.appendChild(li);
        }
        else if (i == n - 1) {
            s.insertBefore(li, s.childNodes[i]);
        }
    }
}
function ShowTags(e) {
    var tag = document.getElementById("tags").getElementsByTagName("li");
    var taglength = tag.length;
    if (e == "0") {
        tag[2].style.display = "none";
        tag[3].style.display = "none";
        tag[4].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "信息技术中心") {
        tag[3].style.display = "none";
        tag[4].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "协调督办处") {
        tag[2].style.display = "none";
        tag[4].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "综合处") {
        tag[2].style.display = "none";
        tag[3].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "政务服务处") {
    }

}

//判断有多个iframe的必填
function IsFilled(array,iframe) {
    for (var i = 0; i < array.length; i++) {
        if (window.frames[iframe].document.getElementById(array[i]).value =="") {
            alert("请完善以*标记的必填信息！");
            return false;
        }
    }
    return true;
}
//判断必填
function filled(array) {
    for (var i = 0; i < array.length; i++) {
        if (document.getElementById(array[i]).value == "") {
            alert("请完善以*标记的必填信息！");
            return false;
        }
    }
    return true;
}

function SetNotesReadOnly(obj) {
    if (!obj) return;
    for (var i = 0; i < obj.childNodes.length; i++) {
        if (obj.childNodes(i).tagName == "INPUT" || obj.childNodes(i).tagName == "TEXTAREA" || obj.childNodes(i).tagName == "IMG" || obj.childNodes(i).tagName == "SELECT") {

            obj.childNodes(i).readOnly = true;
//            obj.childNodes(i).setAttribute('readonly', 'readonly');

        }
        SetNotesReadOnly(obj.childNodes(i));
    }

}
function ShowImage(value,img) 
        { 
            //alert(value); 
            //检测盘符 
            //alert(value.indexOf(':')); 
            //检测文件是否有扩展名 
            //alert(value.length-value.lastIndexOf('.')); 
            //取文件扩展名 
            //alert(value.substr(value.length-3,3)); 
            //检测文件扩展名是否合法 
            //alert(CheckExt(value.substr(value.length-3,3))); 
             
                if(value.length>5&&value.indexOf(':')==1&&(value.length-value.lastIndexOf('.'))==4&&CheckExt(value.substr(value.length-3,3))) 
                { 
                        img.src=value; 
                        img.alt="本地图片预览"; 
                        img.style.visibility="visible"; 
                } 
                else 
                { 
      img.style.visibility="hidden"; 
    } 
        } 
        //检查扩展名是否合法,合法返回True 
        function CheckExt(ext) 
        { 
          //这里设置允许的扩展名 
          var AllowExt="jpg|gif|jpeg|png|bmp"; 
          var ExtOK=false; 
      var ArrayExt; 
      if(AllowExt.indexOf('|')!=-1) 
      { 
        ArrayExt=AllowExt.split('|'); 
        for(i=0;i<ArrayExt.length;i++) 
        { 
          if(ext.toLowerCase()==ArrayExt[i]) 
          { 
            ExtOK=true; 
            break; 
          } 
        } 
      } 
      else 
      { 
        ArrayExt=AllowExt; 
        if(ext.toLowerCase()==ArrayExt) 
        { 
          ExtOK=true; 
        } 
      } 
      return ExtOK; 
        } 

