var numStr="0123456789";
var moneyStr = numStr + ".";
var intStr = numStr+"-";
var floatStr = moneyStr+"-";
var phoneStr = "()-#" + numStr;
var flag = 0;


function check(formName){
	form = document.forms(formName);
	var obj = null;
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
	   alert("非法对象");
   		return false;
 	}
 
	if (obj.v_must == null || obj.v_must == ""){
	   return false;
	}			 
	 
	if (obj.value != null){
		obj.value = trimSubStr(trimSubStr(obj.value," ",1)," ",2);
	}
	 
	 
	//如果属性为空，则创建默认值
	
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
	  //if (val.getByte() > maxlen){
	    return false;
	  }
	}
	if (!isNaN(minlen)){
	 if (val.length < minlen){
	 // if (val.getByte() < minlen){
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
      case 0://所有
        do{
          tempLength = tempString.length;
          tempString = tempString.replace(ASubStr,"");
        } while(tempLength != tempString.length);
        break; 
      case 1://左
        while (true){
          if (tempString.length < tSbLength) break;
          for (i = 0;i < tSbLength;i++)
            if (ASubStr.charAt(i) != tempString.charAt(i)) 
              return tempString;
          tempString = tempString.replace(ASubStr,"");  
        };  
      case 2://右
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
		   alert("'" + obj.v_name + "'为必填项！");
		   flag = 1;
		   obj.focus();
		   return false;
		}
	    
	  }

	  if (!isLengthOf(obj.value,obj.v_minlength,obj.v_maxlength)){
	    flag = 1;
		alert("'"+obj.v_name+"'的长度不正确！最大长度为" + obj.v_maxlength + ",注意一个汉字的长度为2！");
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
        alert("'" + obj.v_name + "'的值不正确！请输入数字！");
	    obj.focus();
	    return false;
      }
	      
    if (!isRight_length(obj.value,"6")){
      flag = 1;
      alert("'"+obj.v_name+"'的值不正确！长度有错误！(6位数字)");
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
      alert("'" + obj.v_name + "'的值不正确！请输入数字！");
	  obj.focus();
	  return false;
    }
     
	if (!isValid_dot(obj.value)){
	    flag = 1;
	    alert("'" + obj.v_name + "'的值不正确！小数点有错误！");
	    obj.focus();
	    return false;  
	}
	
	var temp = obj.value;
	if (temp.indexOf(".") != -1) {
		var str = temp.substring(temp.indexOf(".")+1, temp.length);
		if(str.length > 2){
			flag = 1;
			alert("'" + obj.v_name + "'的值不正确！小数点有错误！");
			obj.focus();
			return false;
		}
	}
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'的值不正确！超出规定范围！");
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
      alert("'" + obj.v_name + "'的值不正确！请输入数字！");
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
      alert("'" + obj.v_name + "'的值不正确！请输入数字！");
	  obj.focus();
	  return false;
    }
	
	if (!isValid_negative(obj.value)){
      flag = 1;
      alert("'" + obj.v_name + "'的值不正确！'-'符号有错误！");
	  obj.focus();
	  return false;
    }
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'的值不正确！超出规定范围！");
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
      alert("'" + obj.v_name + "'的值不正确！请输入数字！");
	  obj.focus();
	  return false;
    }
	
	if (!isValid_dot(obj.value)){
	    flag = 1;
	    alert("'" + obj.v_name + "'的值不正确！小数点有错误！");
	    obj.focus();
	    return false;  
	}	
	
	if (!isValid_negative(obj.value)){
      flag = 1;
      alert("'" + obj.v_name + "'的值不正确！'-'符号有错误！");
	  obj.focus();
	  return false;
    }
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'的值不正确！超出规定范围！");
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
	  alert("'" + obj.v_name + "'的值不正确！请填写数字,可以包含(,),－,#符号"); 
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
	alert("'" + obj.v_name + "'的值不正确！请确信填写正确的电子邮箱！");
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
	  alert("'" + obj.v_name + "'的值不正确！身份证必须填写数字！");
	  obj.focus();
	  return false;
	}
	*/
	
	if (!isRight_length(obj.value,"15") && !isRight_length(obj.value,"18")){
	  flag = 1;
	  alert("'" + obj.v_name + "'的值不正确(只能为15或18位)！请输入正确的身份证号！"); //身份证长度不正确(只能为15或18位)！");
	  obj.focus();
	  return false;
	}
	
	return true;

}

function forDate(obj)
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
  	
  	var Date = obj.value;
  	if(Date != null && Date != ""){
  		var msg = "日期格式不合法，请使用类似“20040101”或“2004-01-01”的格式！";
  		if(!Date.isDate()){
  			if(Date.getByte() != 8){
  				flag = 1;
  				alert("'" + obj.v_name + "'的值不正确！" + msg);
		    	obj.focus();
		    	return false;
  			}
  			Date = Date.substring(0, 4) + "-" + Date.substring(4, 6) + "-" + Date.substring(6, 8);
  			if(!Date.isDate()){
  				flag = 1;
  				alert("'" + obj.v_name + "'的值不正确！" + msg);
		    	obj.focus();
		    	return false;
		    }
		}
	}
    return true;
}
