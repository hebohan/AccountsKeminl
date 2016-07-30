var numStr="0123456789";
var strStr="abcdefghijklmnopqrstuvwsyzABCDEFGHIJKLMNOPQRSTUVWSYZ";
var moneyStr = numStr + ".";
var intStr = numStr+"-";
var floatStr = moneyStr+"-";
var phoneStr = "()-#" + numStr;
var flag = 0;


function saveCheck(form){
	var obj = null;
    var t = null;
	var i;
    for (i=0;i<form.length;i++)
    {    
		 obj = form.elements[i];		 	 		 
		 if(packUp(obj)){
		     if(obj.v_type == "string") forSaveString(obj);	     
	         else if(obj.v_type == "zip") forZip(obj);
	         else if(obj.v_type == "money") forMoney(obj);
	         else if(obj.v_type == "0_9") for0_9(obj);
			 else if(obj.v_type == "int") forInt(obj);
			 else if(obj.v_type == "float") forFloat(obj);
			 else if(obj.v_type == "email") forEmail(obj);
			 else if(obj.v_type == "date") forDate(obj);
			 else if(obj.v_type == "pwd") forPwd(obj);
			 else if(obj.v_type == "phone") forPhone(obj);
			 else if(obj.v_type == "idcard") forIdCard(obj);
			 else if(obj.v_type == "char") forChar(obj);
			 else if(obj.v_type == "chinese") forChinese(obj);
         }  
                
		if(flag == 1) {
	      flag = 0;
		  return false;
		}

     }
	 
     return true;
}



function check(form){
	var obj = null;
    var t = null;
	var i;
    for (i=0;i<form.length;i++)
    {    
		 obj = form.elements[i]; 		 
		 if(packUp(obj)){
		     if(obj.v_type == "string") forString(obj);	     
	         else if(obj.v_type == "zip") forZip(obj);
	         else if(obj.v_type == "money") forMoney(obj);
	         else if(obj.v_type == "0_9") for0_9(obj);
			 else if(obj.v_type == "int") forInt(obj);
			 else if(obj.v_type == "float") forFloat(obj);
			 else if(obj.v_type == "email") forEmail(obj);
			 else if(obj.v_type == "date") forDate(obj);
			 else if(obj.v_type == "pwd") forPwd(obj);
			 else if(obj.v_type == "phone") forPhone(obj);
			 else if(obj.v_type == "idcard") forIdCard(obj);
			 else if(obj.v_type == "char") forChar(obj);
			 else if(obj.v_type == "chinese") forChinese(obj);
         }  
                
		if(flag == 1) {
	      flag = 0;
		  return false;
		}

     }
	 
     return true;
}

function  packUp(obj)
{   
	if (obj == null){
	   alert("�Ƿ�����");
   		return false;
 	}
 
	if (obj.v_must == null || obj.v_must == ""){
	   return false;
	}			 
	 
	if (obj.value != null){
		obj.value = trimSubStr(trimSubStr(obj.value," ",1)," ",2);
	}
	 
	 
	//�������Ϊ�գ��򴴽�Ĭ��ֵ
	
	if (obj.v_name == null){
		obj.v_name = new String(obj.name);
	} 

   	if (obj.v_type == null){	 
	 	obj.v_type = new String("string");  
   	}			  
	
	if (obj.v_minlength == null){	 
	 	obj.v_minlength = new String("0");
    }			 

   	if (obj.v_maxlength == null){	 
	 	obj.v_maxlength = obj.getAttribute("maxlength");
    }
 
   	if (obj.v_maxvalue == null){	 
	 	obj.v_maxvalue = new String("");
    }			 

   	if (obj.v_minvalue == null){	 
	 	obj.v_minvalue = new String("");
    }
    		 
	return true;	
}




function isMadeOf(val,str)
{

	var jj;
	var chr;
	for (jj=0;jj<val.length;++jj){
		chr=val.charAt(jj);
		if (str.indexOf(chr,0)==-1)
			return false;			
	}
	
	return true;
}


function isNotNull(val)
{
	if (val =="")
		return (false);
	if (val == null)
		return (false);
	
		return (true);
}

function isSizeOf(val,min,max)
{
    var maxval = parseFloat(max);
	var minval = parseFloat(min);
	var selval = parseFloat(val);
	
	if (isNaN(selval)){
	  return false;
	}
	
	if (!isNaN(maxval)){
	  if (selval > maxval){
	    return false;
	  }
	}
	if (!isNaN(minval)){
	  if (selval < minval){
	    return false;
	  }
	}
	
    /*
	if (val < min || val > max){
	  return false;	
	}
	*/
		
	return (true);
}


function isLengthOf(val,min,max)
{   
    	
	var minlen = parseInt(min);
	var maxlen = parseInt(max);

	if (!isNaN(maxlen)){
	  if (val.length > maxlen){
	 // if (val.getByte() > maxlen){
	    return false;
	  }
	}
	if (!isNaN(minlen)){
	  if (val.length < minlen){
	  //if (val.getByte() < minlen){
	    return false;
	  }
	}
	
	/*
	if (val.length < minlen || val.lengh > maxlen ){
	  return false;
	}
	*/
	return true;
}


function isValid_dot(val)
{   
    
    var subvalue;
    
    if (val.indexOf(".",0) != -1){
	
	   subvalue = val.substring(val.indexOf(".",0)+1);
	   
	  if (subvalue.indexOf(".",0) != -1){
	    return false;  
	  }
	  
	}
    
	return true;
	
}


function isValid_negative(val)
{
    
    var subvalue;
    
    if (val.indexOf("-",0) != -1){
	
	   if (val.indexOf("-",0) > 0){
	     return false;
	   }
	   
	   subvalue = val.substring(val.indexOf("-",0)+1);
	  
	  if (subvalue.indexOf("-",0) != -1){
	    return false;  		
	  }
	  
	}
    
	return true;
	

}


function isRight_length(val,num)
{
     var len = parseInt(num);
	 
	 if (isNaN(len)){ return true;}
	 
	 if (val.length != len){
	   return false;
	 }

     return true;

}


  function trimSubStr(ATrimStr,ASubStr,AWhere){  
    var tTrLength,tSbLength,tempLength;
    var tempString;
    var i; 

    tTrLength = ATrimStr.length;
    tSbLength = ASubStr.length;

    if (tSbLength == 0){return ATrimStr;}
    if (tSbLength > tTrLength){return ATrimStr;}
  
    tempString = ATrimStr;
    switch(AWhere){
      case 0://����
        do{
          tempLength = tempString.length;
          tempString = tempString.replace(ASubStr,"");
        } while(tempLength != tempString.length);
        break; 
      case 1://��
        while (true){
          if (tempString.length < tSbLength) break;
          for (i = 0;i < tSbLength;i++)
            if (ASubStr.charAt(i) != tempString.charAt(i)) 
              return tempString;
          tempString = tempString.replace(ASubStr,"");  
        };  
      case 2://��
        while(true){
          tempLength = tempString.length;
          if (tempLength < tSbLength){return tempString;}
          for (i = 0;i < tSbLength;i ++){
            if (ASubStr.charAt(i) != tempString.charAt(tempLength - tSbLength+i)){
              return tempString;
            }  
          }    
          tempString = tempString.substr(0,tempLength-tSbLength); 
        };
      default:
        return tempString;
    }
    return tempString; 
  }


function forDate(useryear,usermonth,userday)
{
	var myyear;
	var mymonth;
	var myday;
	//myyear=parseInt(useryear);
	//mymonth=parseInt(usermonth);
	//myday=parseInt(userday);
	myyear=useryear;
	mymonth=usermonth;
	myday=userday;
	if (myyear < 1950 || myyear > 2050 ||mymonth < 1 ||mymonth > 12 || myday < 1 || myday > 31)
		return (false);
        if(mymonth==4 || mymonth==6 || mymonth==9 || mymonth==11)
        { 
        	if(myday>30)
            		return (false);
        }
	if(myyear%4==0)
	{
	 	if((myyear%100==0 && myyear%400==0) || myyear%100!=0)
          	{
            		if(mymonth==2 && myday>29)
             			return (false);
            		else
	     			return (true);
          	}
        }
	else
	{
          	if(mymonth==2 && myday>28)
             		return (false);
             	else
             		return (true);
	}
}



function  forString(obj)
{     
      
	  if (obj.v_must!="0"){
	    
	    if (obj.value.length == 0){
		   alert("'" + obj.v_name + "'Ϊ������������д");
		   flag = 1;
		   obj.focus();
		   return false;
		}
	    
	  }

	  if (!isLengthOf(obj.value,obj.v_minlength,obj.v_maxlength)){
	    flag = 1;
		alert("'"+obj.v_name+"'�ĳ��Ȳ���ȷ����󳤶�Ϊ" + obj.v_maxlength + ",ע��һ�����ֵĳ���Ϊ2��");
		obj.focus();
		return false;
	  
	  }
  
	  return true;
      
}

function  forSaveString(obj)
{     
      

	  if (!isLengthOf(obj.value,obj.v_minlength,obj.v_maxlength)){
	    flag = 1;
		alert("'"+obj.v_name+"'�ĳ��Ȳ���ȷ����󳤶�Ϊ" + obj.v_maxlength + ",ע��һ�����ֵĳ���Ϊ2��");
		obj.focus();
		return false;
	  
	  }
  
	  return true;
      
}

function forZip(obj)
{
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
  
    if (!isMadeOf(obj.value,numStr)){
        flag = 1;
        alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	    obj.focus();
	    return false;
      }
	      
    if (!isRight_length(obj.value,"6")){
      flag = 1;
      alert("'"+obj.v_name+"'��ֵ����ȷ�������д���(6λ����)");
	  obj.focus();
	  return false;
    } 

    return true;
}

function forMoney(obj)
{   
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
		
    if (!isMadeOf(obj.value,moneyStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	  obj.focus();
	  return false;
    }
     
	if (!isValid_dot(obj.value)){
	    flag = 1;
	    alert("'" + obj.v_name + "'��ֵ����ȷ��С�����д���");
	    obj.focus();
	    return false;  
	}
	
	var temp = obj.value;
	if (temp.indexOf(".") != -1) {
		var str = temp.substring(temp.indexOf(".")+1, temp.length);
		if(str.length > 2){
			flag = 1;
			alert("'" + obj.v_name + "'��ֵ����ȷ��С�����д���");
			obj.focus();
			return false;
		}
	}
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�������涨��Χ��");
	  obj.focus();
	  return false;
	} 
	
	return true;
	
}


function for0_9(obj)
{    
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
    
	if (!isMadeOf(obj.value,numStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	  obj.focus();
	  return false;
    }
	
	return true;
	
}

/*
**У���ַ����Ƿ�ΪӢ����ĸ
*�����ˣ�����ƽ
*Date  ��2004-10-27
*/
function forChar(obj)
{    
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
    
	if (!isMadeOf(obj.value,strStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ����������ĸ��");
	  obj.focus();
	  return false;
    }
	
	return true;
	
}

function forInt(obj)
{
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
   
    if (!isMadeOf(obj.value,intStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	  obj.focus();
	  return false;
    }
	
	if (!isValid_negative(obj.value)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ��'-'�����д���");
	  obj.focus();
	  return false;
    }
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�������涨��Χ��");
	  obj.focus();
	  return false;
	}
    
   return true;
   
}


function forFloat(obj)
{
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
   
    if (!isMadeOf(obj.value,floatStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	  obj.focus();
	  return false;
    }
	
	if (!isValid_dot(obj.value)){
	    flag = 1;
	    alert("'" + obj.v_name + "'��ֵ����ȷ��С�����д���");
	    obj.focus();
	    return false;  
	}	
	
	if (!isValid_negative(obj.value)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ��'-'�����д���");
	  obj.focus();
	  return false;
    }
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�������涨��Χ��");
	  obj.focus();
	  return false;
	}

   return true;

}

function forPhone(obj)
{
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
	
	if (!isMadeOf(obj.value,phoneStr)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ������д����,���԰���(,),��,#����"); 
	  obj.focus();
	  return false; 
	}

}

function forEmail(obj)
{
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
	
    var myReg = /^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$/; 
    if(myReg.test(obj.value)){
	  return true; 
	}
	alert("'" + obj.v_name + "'��ֵ����ȷ����ȷ����д��ȷ�ĵ������䣡");
	flag = 1;
	obj.focus();
    return false; 

}

function forIdCard(obj)
{
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	} 
	
	/*
	if (!isMadeOf(obj.value,numStr)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�����֤������д���֣�");
	  obj.focus();
	  return false;
	}
	*/
	
	if (!isRight_length(obj.value,"15") && !isRight_length(obj.value,"18")){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ����������ȷ�����֤�ţ�"); //���֤���Ȳ���ȷ(ֻ��Ϊ15��18λ)��");
	  obj.focus();
	  return false;
	}
	
	return true;

}

function forDate(obj)
{
	var myyear;
	var mymonth;
	var myday;
    if (!forString(obj)){
	  flag = 1;
	  obj.focus();
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
  	
  	var Date = obj.value;
  	if(Date != null && Date != ""){
  		var msg = "���ڸ�ʽ���Ϸ�����ʹ�����ơ�20040101����2004-01-01���ĸ�ʽ��";
  		if(!Date.isDate()){
  			if(Date.getByte() != 8){
  				flag = 1;
  				alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
		    	obj.focus();
		    	return false;
  			}
  			myyear = Date.substring(0, 4);
			mymonth = Date.substring(4, 6);
			myday = Date.substring(6, 8);
  			Date = myyear + "-" + mymonth + "-" + myday;
  			if(!Date.isDate()){
  				flag = 1;
  				alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
		    	obj.focus();
		    	return false;
		    }
		}else{
			myyear = Date.substring(0, 4);
			mymonth = Date.substring(5, 7);
			myday = Date.substring(8, 10);
		}
	}
	if (!isMadeOf(myyear,numStr)){
		flag = 1;
		alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
    	obj.focus();
	    return false;
    }
    if (!isMadeOf(mymonth,numStr)){
    	flag = 1;
    	alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
    	obj.focus();
	    return false;
    }
    if (!isMadeOf(myday,numStr)){
    	flag = 1;
    	alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
    	obj.focus();
	    return false;
    }
    return true;
}

/********************************** chinese ***************************************/
/**
*У���ַ����Ƿ�Ϊ����
*����ֵ��
*���Ϊ�գ�����У��ͨ����           ����true
*����ִ�Ϊ���ģ�У��ͨ����         ����true
*����ִ�Ϊ�����ģ�                ����false    �ο���ʾ��Ϣ������Ϊ���ģ�
*�����ˣ�����ƽ
*Date  ��2004-10-27
*/
function checkIsChinese(str)
{
    //���ֵΪ�գ�ͨ��У��
    if (str == "")
        return true;
    var pattern = /^([\u4E00-\u9FA5]|[\uFE30-\uFFA0])*$/gi;
    if (pattern.test(str))
        return true;
    else
    {
        alert('��������������!');
        return false;
    }
}

function getNumber(value)
{ if(value!=null&&value!="undefined") {
  if(value=="" ||!isMadeOf(value,floatStr))
   return 0;
  else
   return parseInt(value);
} else 
  return 0;
}