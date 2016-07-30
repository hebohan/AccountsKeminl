
var timer;
var len
function getRandNum()

{

var str=document.all("hary").value;
var mobile=str.split(",");
len=mobile.length;
    document.getElementById("result").value = mobile[GetRnd(len)];
}

function GetRnd(length)
{
   var  randnum = parseInt(Math.random()*length);
    return randnum;
}
function changebutton()
{ 


    if(document.getElementById("start").value=="开始")
    {
        
        timer = setInterval("getRandNum();",10);
        document.getElementById("start").value="确定";
    }
    else
    {
        clearInterval(timer);
        document.getElementById("start").disabled = true;
        document.getElementById("daili").value = document.getElementById("result").value ;
        alert(mobile[1]);
    }
}



