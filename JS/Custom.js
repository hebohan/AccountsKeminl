var _oldColor;

function SetNewColor(source) {

    _oldColor = source.style.backgroundColor;

    source.style.backgroundColor = '#666666';



}

function SetOldColor(source) {

    source.style.backgroundColor = _oldColor;

}
//================== AJAX��̬������ AddBy Lizhaoch start===============================================
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
    var drag = document.getElementById(dragId); //�϶���Ŀ�꣬����갴��ʱ������    
    var target = document.getElementById(moveId); //�ƶ���Ŀ�꣬���϶�ʱ�ƶ�������    
    target.style.position = "absolute"; //position��������Ϊabsulute�ſ������ƶ���    
    var canMove = false; //�Ƿ��ڿ����϶���״̬��    
    var mouseDownX = 0, mouseDownY = 0; //��¼��갴��ʱλ�á�    
    var mouseDownLeft = 0, mouseDownY = 0; //��¼��갴��ʱ�ƶ�Ŀ���λ�á�    

    var DragConfig = {}; //����һ���������������¼���    
    DragConfig.startMove = function() {
        drag.setCapture(); //ʹ��drag���Բ���ȫ��Ļ�ڵ������¼���    
        /*��¼��갴��ʱ��״̬��*/
        mouseDownX = event.screenX;
        mouseDownY = event.screenY;
        mouseDownLeft = target.offsetLeft;
        mouseDownTop = target.offsetTop;
        canMove = true; //�Ƿ�����ƶ���ʶΪtrue��ʹdoMove������Ч��    
    }
    DragConfig.doMove = function() {
        if (canMove) {/*��������ƶ�����ı�target��λ�á�*/
            target.style.left = mouseDownLeft + (event.screenX - mouseDownX);
            target.style.top = mouseDownTop + (event.screenY - mouseDownY);
        }
    }
    DragConfig.endMove = function() {
        drag.releaseCapture(); //�ͷ�drag���¼�����    
        canMove = false; //���������ƶ���ʶΪfalse,doMove������Ч��    
    }

    drag.onmousedown = DragConfig.startMove; //��startMove�����󶨵�onmousedown    
    drag.onmousemove = DragConfig.doMove; //��doMove�����󶨵�onmousemove    
    drag.onmouseup = DragConfig.endMove; //��endMove�����󶨵�onmouseup    
}
//================== AJAX��̬������ end===============================================
//================== ��̬����  AddBy Lizhaoch start===============================================

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

//================== ��̬���� end===============================================
//================== �������� start===============================================
//�������ʾһ���㣬�ò���ڿ�Ϊdiv2������
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
    div3.style.display = "block"; //div3��ʼ״̬�ǲ��ɼ��ģ����ÿ�Ϊ�ɼ� 
    div3.style.left = event.clientX + 15; //���Ŀǰ��X���ϵ�λ�ã���10��Ϊ�����ұ��ƶ�10��px���㿴������ 
    div3.style.top = event.clientY - 100;
    div3.style.position = "absolute"; //����ָ��������ԣ�����div3���޷�������궯 
}
//�رղ�div3����ʾ
function closeTip(depth) {
    var pres = "";
    for (var i = 0; i < depth; i++) {
        pres += "../";
    }
    var div3 = document.getElementById('div3');
    div3.style.display = "none";
    var useris = "����,���ڲ���,�����ʼ�,�ƶ��绰,֤������,�칫�绰,ְ��," + pres + "images/oauser/nobody.jpg".split(',');
    document.getElementById("tbName").innerText = useris[0];
    document.getElementById("tbDept").innerText = useris[1];
    document.getElementById("tbEmail").innerText = useris[2];
    document.getElementById("tbMobil").innerText = useris[3];
    document.getElementById("tbShortNum").innerText = useris[4];
    document.getElementById("tbTEL").innerText = useris[5];
    document.getElementById("tbZhiCheng").innerText = useris[6];
    document.getElementById("imgPerson").src = useris[7];
}
//�ύ����
function PostMeg(DataToSend, Url) {
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.Open("POST", Url, false);
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlhttp.send(DataToSend);
    return xmlhttp.responseText;
}
//================== �������� end===============================================
//================== ���Ž�פ������ʾ====================
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
    if (e == "��Ϣ��������") {
        tag[3].style.display = "none";
        tag[4].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "Э�����촦") {
        tag[2].style.display = "none";
        tag[4].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "�ۺϴ�") {
        tag[2].style.display = "none";
        tag[3].style.display = "none";
        tag[5].style.display = "none";
    }
    if (e == "�������") {
    }

}

//�ж��ж��iframe�ı���
function IsFilled(array,iframe) {
    for (var i = 0; i < array.length; i++) {
        if (window.frames[iframe].document.getElementById(array[i]).value =="") {
            alert("��������*��ǵı�����Ϣ��");
            return false;
        }
    }
    return true;
}
//�жϱ���
function filled(array) {
    for (var i = 0; i < array.length; i++) {
        if (document.getElementById(array[i]).value == "") {
            alert("��������*��ǵı�����Ϣ��");
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
            //����̷� 
            //alert(value.indexOf(':')); 
            //����ļ��Ƿ�����չ�� 
            //alert(value.length-value.lastIndexOf('.')); 
            //ȡ�ļ���չ�� 
            //alert(value.substr(value.length-3,3)); 
            //����ļ���չ���Ƿ�Ϸ� 
            //alert(CheckExt(value.substr(value.length-3,3))); 
             
                if(value.length>5&&value.indexOf(':')==1&&(value.length-value.lastIndexOf('.'))==4&&CheckExt(value.substr(value.length-3,3))) 
                { 
                        img.src=value; 
                        img.alt="����ͼƬԤ��"; 
                        img.style.visibility="visible"; 
                } 
                else 
                { 
      img.style.visibility="hidden"; 
    } 
        } 
        //�����չ���Ƿ�Ϸ�,�Ϸ�����True 
        function CheckExt(ext) 
        { 
          //����������������չ�� 
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
