window.onload = getMsg;
window.onresize = resizeDiv;
window.onerror = function(){};


var divTop,divLeft,divWidth,divHeight,docHeight,docWidth,objTimer,i = 0;

function getMsg()
{
	try
	{
		// ���
		//Message�ĸ߶�
		divTop = parseInt(document.getElementById("Message").style.top,10)
		divLeft = parseInt(document.getElementById("Message").style.left,10)
		docWidth = document.body.clientWidth;
		divHeight = parseInt(document.getElementById("Message").offsetHeight,10);
		//Message�ĸ߶�
		divWidth = parseInt(document.getElementById("Message").offsetWidth,10);
		//alert(divHeight+"|"+divWidth);
		
		//body�Ŀ��
		docWidth = document.body.clientWidth;
		//body�ĸ߶�
		docHeight = document.body.clientHeight;
		//alert(docWidth+"|"+docHeight);
		
		//��Message��top��bodyҳ�����׶�
		document.getElementById("Message").style.top = parseInt(document.body.scrollTop,10) + docHeight;// divHeight
		//alert(document.getElementById("Message").style.top);
		
		//��Message��left��bodyҳ���ȼ�ȥMessage�Ŀ��
		document.getElementById("Message").style.left = parseInt(document.body.scrollLeft,10) + docWidth - divWidth
		//alert(document.getElementById("Message").style.left);
		//����Message��Ϊ�ɼ�״̬
		document.getElementById("Message").style.visibility="visible"
		
		objTimer = window.setInterval("moveDiv()",10)
	}
	catch(e){}
} 
��

function resizeDiv()
{
	try
	{
	i+=1
	if(i>500) closeDiv()
	divHeight = parseInt(document.getElementById("Message").offsetHeight,10)
	divWidth = parseInt(document.getElementById("Message").offsetWidth,10)
	docWidth = document.body.clientWidth;
	docHeight = document.body.clientHeight;
	document.getElementById("Message").style.top = docHeight - divHeight + parseInt(document.body.scrollTop,10)
	document.getElementById("Message").style.left = docWidth - divWidth + parseInt(document.body.scrollLeft,10)
	}catch(e){}
}


function moveDiv()
{
	try
	{
	//alert(document.getElementById("Message").style.top);
	if(parseInt(document.getElementById("Message").style.top,10) <= (docHeight - divHeight + parseInt(document.body.scrollTop,10)))
	{
		//alert(document.getElementById("Message").style.top+"������ͽ���");
		window.clearInterval(objTimer)
		objTimer = window.setInterval("closeDiv()",3600)
	}
	divTop = parseInt(document.getElementById("Message").style.top,10)
	//alert(divTop);
	document.getElementById("Message").style.top = divTop - 1
	
	}catch(e){}	
}

function closeDiv()
{
	try
	{
	document.getElementById('Message').style.visibility='hidden';
	if(objTimer) window.clearInterval(objTimer)
	}catch(e){}
}


function ShiftContent(ContentTag,url) 
{ 
divroll = document.all.tags("SPAN"); 
  for(i=0; i<divroll.length; i++) 
  { 
whichEl = divroll(i); 
idtag = "Content"+ContentTag 
    if (whichEl.id == idtag) 
    { 
     if (whichEl.style.display == "none") 
     { 

	eval('document.all.Icon'+ContentTag+'.src=url+"Images/Share/icominus.gif"') 
     whichEl.style.display = "block"; 
     } 
    else 
     { 
	eval('document.all.Icon'+ContentTag+'.src=url+"Images/Share/icoplus.gif"')
     whichEl.style.display = "none"; 
     } 
    } 
  } 
} 
function ShiftContentAll(Action) 
{ 
	
	divroll = document.all.tags("span"); 
	if (Action == 'Open')
	{		
		  for(j=0; j<divroll.length-1; j++) 
		  { 
				whichEl = divroll(j); 
				idtag = "Content"+(j+1) 
				
				eval('document.all.Icon'+(j+1)+'.src="/images/minus.gif"') 
				whichEl.style.display = "block"; 
			} 
	  }
	  else
	  {
			for(j=0; j<divroll.length-1; j++) 
		  { 
				whichEl = divroll(j); 
				idtag = "Content"+(j+1) 
				eval('document.all.Icon'+(j+1)+'.src="/images/plus.gif"') 
				whichEl.style.display = "none"; 
			}  
		}
} 