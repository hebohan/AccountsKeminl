
window.onload = getMsg;
window.onresize = resizeDiv;
window.onerror = function(){}
//短信提示使用(asilas添加)
var divTop,divLeft,divWidth,divHeight,docHeight,docWidth,objTimer,i = 0;
function getMsg()
{
try{
// 监察
divTop = parseInt(document.getElementById("eMeng").style.top,10)
divLeft = parseInt(document.getElementById("eMeng").style.left,10)
divHeight = parseInt(document.getElementById("eMeng").offsetHeight,10)
divWidth = parseInt(document.getElementById("eMeng").offsetWidth,10)
docWidth = document.body.clientWidth;
docHeight = document.body.clientHeight;
document.getElementById("eMeng").style.top = parseInt(document.body.scrollTop,10) + docHeight + 10;// divHeight
document.getElementById("eMeng").style.left = parseInt(document.body.scrollLeft,10) + docWidth - divWidth
document.getElementById("eMeng").style.visibility="visible"
objTimer = window.setInterval("moveDiv()",10)
getMsgTongZhi()
// 消息
divTop1 = parseInt(document.getElementById("Inform").style.top,10)
divLeft1 = parseInt(document.getElementById("Inform").style.left,10)
divHeight1 = parseInt(document.getElementById("Inform").offsetHeight,10)
divWidth1 = parseInt(document.getElementById("Inform").offsetWidth,10)
docWidth1 = document.body.clientWidth;
docHeight1 = document.body.clientHeight;
document.getElementById("Inform").style.top = parseInt(document.body.scrollTop,10) + docHeight1 + 10;// divHeight1
document.getElementById("Inform").style.left = parseInt(document.body.scrollLeft,10) + docWidth1 - divWidth1 -200
document.getElementById("Inform").style.visibility="visible"
}
catch(e){}
} 
　

function resizeDiv()
{
i+=1
if(i>500) closeDiv()
try{
divHeight = parseInt(document.getElementById("eMeng").offsetHeight,10)
divWidth = parseInt(document.getElementById("eMeng").offsetWidth,10)
docWidth = document.body.clientWidth;
docHeight = document.body.clientHeight;
document.getElementById("eMeng").style.top = docHeight - divHeight + parseInt(document.body.scrollTop,10)
document.getElementById("eMeng").style.left = docWidth - divWidth + parseInt(document.body.scrollLeft,10)

TdivHeight = parseInt(document.getElementById("tongzhi").offsetHeight,10)
TdivWidth = parseInt(document.getElementById("tongzhi").offsetWidth,10)
TdocWidth = document.body.clientWidth;
TdocHeight = document.body.clientHeight;
document.getElementById("tongzhi").style.top = TdocHeight - TdivHeight + parseInt(document.body.scrollTop,10)
document.getElementById("tongzhi").style.left = TdocWidth - TdivWidth + parseInt(document.body.scrollLeft,10)-200

}
catch(e){}
}

function moveDiv()
{
try
{
if(parseInt(document.getElementById("eMeng").style.top,10) <= (docHeight - divHeight + parseInt(document.body.scrollTop,10)))
{
window.clearInterval(objTimer)
//objTimer = window.setInterval("resizeDiv()",1)
}
divTop = parseInt(document.getElementById("eMeng").style.top,10)
document.getElementById("eMeng").style.top = divTop - 1
moveDiv1()
}
catch(e){}
}
function closeDiv()
{
document.getElementById('eMeng').style.visibility='hidden';
if(objTimer) window.clearInterval(objTimer)
}
//window.onload = getMsg1;
//window.onresize = resizeDiv1;
//短信提示使用(asilas添加)
var divTop1,divLeft1,divWidth1,divHeight1,docHeight1,docWidth1,objTimer1,i1 = 0;
function getMsg1()
{
try{
divTop1 = parseInt(document.getElementById("Inform").style.top,10)
divLeft1 = parseInt(document.getElementById("Inform").style.left,10)
divHeight1 = parseInt(document.getElementById("Inform").offsetHeight,10)
divWidth1 = parseInt(document.getElementById("Inform").offsetWidth,10)
docWidth1 = document.body.clientWidth;
docHeight1 = document.body.clientHeight;
document.getElementById("Inform").style.top = parseInt(document.body.scrollTop,10) + docHeight1 + 10;// divHeight1
document.getElementById("Inform").style.left = parseInt(document.body.scrollLeft,10) + docWidth1 - divWidth1
document.getElementById("Inform").style.visibility="visible"
objTimer1 = window.setInterval("moveDiv1()",10)
}
catch(e){}
} 
　

function resizeDiv1()
{
i1+=1
if(i1>500) closeDiv1()
try{
divHeight1 = parseInt(document.getElementById("Inform").offsetHeight,10)
divWidth1 = parseInt(document.getElementById("Inform").offsetWidth,10)
docWidth1 = document.body.clientWidth;
docHeight1 = document.body.clientHeight;
document.getElementById("Inform").style.top = docHeight1 - divHeight1 + parseInt(document.body.scrollTop,10)
document.getElementById("Inform").style.left = docWidth1 - divWidth1 + parseInt(document.body.scrollLeft,10)
}
catch(e){}
}

function moveDiv1()
{
try
{
if(parseInt(document.getElementById("Inform").style.top,10) <= (docHeight1 - divHeight1 + parseInt(document.body.scrollTop,10)))
{
window.clearInterval(objTimer1)
//objTimer1 = window.setInterval("resizeDiv()",1)
}
divTop1 = parseInt(document.getElementById("Inform").style.top,10)
document.getElementById("Inform").style.top = divTop1 - 1
}
catch(e){}
}
function closeDiv1()
{
document.getElementById('Inform').style.visibility='hidden';
if(objTimer1) window.clearInterval(objTimer1)
}

function RediretNotify()
{
   //alert(document.parentWindow.frameElment)
   //alert(window.parent.document.frames("contents").obj.)
   window.location = "inform/flashtab.aspx?infotype=notify";
   MenuRedirect("消息中心");
  // window.parent.document.frames("contents").obj.titlemouseclick("objleftmenu3");
}
function MenuRedirect(menuName)
{ 
   var menuObj = window.parent.document.frames("contents").obj;
   for(var i=0;i<menuObj.subitems.length;i++)
   { 
     if(menuObj.subitems[i].text==menuName)
     { 
       menuObj.subitems[i].titleclick(0);
       break;
     }
   }
}

//紧急通知
var TdivTop,TdivLeft,TdivWidth,TdivHeight,TdocHeight,TdocWidth,TobjTimer,Ti = 0;
function getMsgTongZhi()
{
try{

TdivTop = parseInt(document.getElementById("tongzhi").style.top,10)
TdivLeft = parseInt(document.getElementById("tongzhi").style.left,10)
TdivHeight = parseInt(document.getElementById("tongzhi").offsetHeight,10)
TdivWidth = parseInt(document.getElementById("tongzhi").offsetWidth,10)
TdocWidth = document.body.clientWidth;
TdocHeight = document.body.clientHeight;
document.getElementById("tongzhi").style.top = parseInt(document.body.scrollTop,10) + TdocHeight + 10;// divHeight
document.getElementById("tongzhi").style.left = parseInt(document.body.scrollLeft,10) + TdocWidth - TdivWidth-200
document.getElementById("tongzhi").style.visibility="visible"
TobjTimer = window.setInterval("moveDivTongzhi()",10)


}
catch(e){}
} 
function resizeDivTongzhi()
{
Ti+=1
if(Ti>500) closeDivTongZhi()
try{
TdivHeight = parseInt(document.getElementById("tongzhi").offsetHeight,10)
TdivWidth = parseInt(document.getElementById("tongzhi").offsetWidth,10)
TdocWidth = document.body.clientWidth;
TdocHeight = document.body.clientHeight;
document.getElementById("tongzhi").style.top = TdocHeight - TdivHeight + parseInt(document.body.scrollTop,10)
document.getElementById("tongzhi").style.left = TdocWidth - TdivWidth + parseInt(document.body.scrollLeft,10)
}
catch(e){}
}
function moveDivTongzhi()
{
try
{
if(parseInt(document.getElementById("tongzhi").style.top,10) <= (TdocHeight - TdivHeight + parseInt(document.body.scrollTop,10)))
{
window.clearInterval(TobjTimer)
//objTimer = window.setInterval("resizeDiv()",1)
}
TdivTop = parseInt(document.getElementById("tongzhi").style.top,10)
document.getElementById("tongzhi").style.top = TdivTop - 1

}
catch(e){}
}

function closeDivTongZhi()
{
document.getElementById('tongzhi').style.visibility='hidden';
if(TobjTimer) window.clearInterval(TobjTimer)
}