var numStr="0123456789";
var strStr="abcdefghijklmnopqrstuvwsyzABCDEFGHIJKLMNOPQRSTUVWSYZ";
var moneyStr = numStr + ".";
var intStr = numStr+"-";
var floatStr = moneyStr+"-";
var phoneStr = "()-#" + numStr;
var flag = 0;

/*********************************************
*�ַ�������String����չ������ʼ
*********************************************/
/*
        function:����ַ������ֽ���
        return:�ֽ���
        example:"test����".getByteֵΪ8
*/
String.prototype.getByte = function()
{
        var intCount = 0;
        for(var i = 0;i < this.length;i ++)
        {
            // Ascii�����255��˫�ֽڵ��ַ�
            var ascii = this.charCodeAt(i).toString(16);
            var byteNum = ascii.length / 2.0;
            intCount += (byteNum + (ascii.length % 2) / 2);
        }
        return intCount;
}

/*
        function: ָ���ַ�������ַ�ȫ��ת��Ϊ��Ӧ��ȫ���ַ�
        parameter:
                dbcStr: Ҫת���İ���ַ�����
        return: ת������ַ���
*/
String.prototype.dbcToSbc = function(dbcStr)
{
        var resultStr = this;

        for(var i = 0;i < this.length;i ++)
        {
                switch(dbcStr.charAt(i))
                {
                        case ",":
                                resultStr = resultStr.replace(/\,/g,"��"); 
                                break;
                        case "!":
                                resultStr = resultStr.replace(/\!/g,"��"); 
                                break;
                        case "#":
                                resultStr = resultStr.replace(/\#/g,"��"); 
                                break;
                        case "|":
                                resultStr = resultStr.replace(/\|/g,"|"); 
                                break;
                        case ".":
                                resultStr = resultStr.replace(/\./g,"��"); 
                                break;
                        case "?":
                                resultStr = resultStr.replace(/\?/g,"��"); 
                                break;
                        case ";":
                                resultStr = resultStr.replace(/\;/g,"��"); 
                                break;
                }
        }
        return resultStr;
}

//��ȡ�ַ������ֽ���ָ�����ִ�
String.prototype.bytesubstr = function(index1,index2)
{
        var resultStr = "";
        var byteCount = 0;
        for(var i = index1;i < index2;i ++)
        {
                if(i > this.length)break;
                if(this.charCodeAt(i) > 255)byteCount += 2;
                else byteCount += 1;
                if(byteCount > (index2 - index1))break;
                resultStr += this.charAt(i);
        }
        return resultStr;
}

//�ж��ַ����Ƿ��������ַ����������򷵻�true�����򷵻�false
String.prototype.isNumber = function() {
	return (this.isInt() || this.isFloat());
}
//�ж��ַ����Ƿ��Ǹ������ַ����������򷵻�true�����򷵻�false
String.prototype.isFloat = function() {
	return /^(?:-?|\+?)\d*\.\d+$/g.test(this);
}
//�ж��ַ����Ƿ��������ַ����������򷵻�true�����򷵻�false
String.prototype.isInt = function() {
	return /^(?:-?|\+?)\d+$/g.test(this);
}
//�ж��ַ����Ƿ��������ַ��������������򷵻�true�����򷵻�false
String.prototype.isPlus = function() {
	return this.isPlusInt() || this.isPlusFloat();
}
//�ж��ַ����Ƿ������������ַ��������������򷵻�true�����򷵻�false
String.prototype.isPlusFloat = function() {
	return /^\+?\d*\.\d+$/g.test(this);
}
//�ж��ַ����Ƿ����������ַ��������������򷵻�true�����򷵻�false
String.prototype.isPlusInt = function() {
	return /^\+?\d+$/g.test(this);
}
//�ж��ַ����Ƿ��Ǹ����ַ��������������򷵻�true�����򷵻�false
String.prototype.isMinus = function() {
	return this.isMinusInt() || this.isMinusFloat();
}
//�ж��ַ����Ƿ��Ǹ��������ַ��������������򷵻�true�����򷵻�false
String.prototype.isMinusFloat = function() {
	return /^-\d*\.\d+$/g.test(this);
}
//�ж��ַ����Ƿ��Ǹ������ַ��������������򷵻�true�����򷵻�false
String.prototype.isMinusInt = function() {
	return /^-\d+$/g.test(this);
}

//�ж��ַ����Ƿ�ֻ���������ַ��������򷵻�true�����򷵻�false
String.prototype.isLeastCharSet = function() {
	return !(/[^A-Za-z0-9_]/g.test(this));
}
//�ж��ַ����Ƿ���Email�ַ����������򷵻�true�����򷵻�false
String.prototype.isEmail = function() {
	return /^\w+@.+\.\w+$/g.test(this);
}
//�ж��ַ����Ƿ������������ַ����������򷵻�true�����򷵻�false
String.prototype.isZip = function() {
	return /^\d{6}$/g.test(this);
}
//�ж��ַ����Ƿ��ǹ̶��绰�����ַ����������򷵻�true�����򷵻�false
String.prototype.isFixedTelephone = function() {
	return /^(\d{2,4}-)?((\(\d{3,5}\))|(\d{3,5}-))?\d{3,18}(-\d+)?$/g.test(this);
}
//�ж��ַ����Ƿ����ֻ��绰�����ַ����������򷵻�true�����򷵻�false
String.prototype.isMobileTelephone = function() {
	return /^13\d{9}$/g.test(this);
}
//�ж��ַ����Ƿ��ǵ绰�����ַ����������򷵻�true�����򷵻�false
String.prototype.isTelephone = function() {
	return this.isMobileTelephone() || this.isFixedTelephone();
}
//�ж��ַ����Ƿ��������ַ����������򷵻�true�����򷵻�false
String.prototype.isDate = function() {
	return /^\d{1,4}-(?:(?:(?:0?[1,3,5,7,8]|1[0,2])-(?:0?[1-9]|(?:1|2)[0-9]|3[0-1]))|(?:(?:0?[4,6,9]|11)-(?:0?[1-9]|(?:1|2)[0-9]|30))|(?:(?:0?2)-(?:0?[1-9]|(?:1|2)[0-9])))$/g.test(this);
}
//�ж��ַ����Ƿ���ʱ���ַ����������򷵻�true�����򷵻�false
String.prototype.isTime = function() {
	return /^(?:(?:0?|1)[0-9]|2[0-3]):(?:(?:0?|[1-5])[0-9]):(?:(?:0?|[1-5])[0-9]).(?:\d{1,3})$/g.test(this);
}
//�ж��ַ����Ƿ�������ʱ���ַ����������򷵻�true�����򷵻�false
String.prototype.isDateTime = function() {
	return /^\d{1,4}-(?:(?:(?:0?[1,3,5,7,8]|1[0,2])-(?:0?[1-9]|(?:1|2)[0-9]|3[0-1]))|(?:(?:0?[4,6,9]|11)-(?:0?[1-9]|(?:1|2)[0-9]|30))|(?:(?:0?2)-(?:0?[1-9]|(?:1|2)[0-9]))) +(?:(?:0?|1)[0-9]|2[0-3]):(?:(?:0?|[1-5])[0-9]):(?:(?:0?|[1-5])[0-9])$/g.test(this);
}
//�Ƚ������ַ�����������򷵻�0�����򷵻ص�ǰ�����ַ�����Ŀ���ַ���֮�����ĺ�������������һ���ַ������������ڻ�����ʱ���ʽ���򷵻�null
String.prototype.compareDate = function(target) {
	var thisDate = this.toDate();
	var targetDate = target.toDate();
	if (thisDate == null || targetDate == null) {
		return null;
	}
	else {
		return thisDate.getTime() - targetDate.getTime();
	}
}
//�ж������ַ���ָ����ʱ���Ƿ��ǵ�ǰ���ڣ������򷵻�true�����򷵻�false
String.prototype.isToday = function() {
	return this.trim().split(' ')[0].compareDate(getSysDate()) == 0;
}
//�ж������ַ���ָ����ʱ���Ƿ��ǵ�ǰ����֮ǰ�������򷵻�true�����򷵻�false
String.prototype.isBeforeDate = function(baseDate) {
	if (baseDate == null) {
		baseDate = getSysDate();
	}
	return this.trim().split(' ')[0].compareDate(baseDate.trim().split(' ')[0]) < 0;
}
//�ж������ַ���ָ����ʱ���Ƿ��ǵ�ǰ����֮�������򷵻�true�����򷵻�false
String.prototype.isAfterDate = function(baseDate) {
	if (baseDate == null) {
		baseDate = getSysDate();
	}
	return this.trim().split(' ')[0].compareDate(baseDate.trim().split(' ')[0]) > 0;
}

//�ж�����ʱ���ַ���ָ����ʱ���Ƿ���ָ������ʱ��֮ǰ�������򷵻�true�����򷵻�false
String.prototype.isBeforeDateTime = function(baseDateTime) {
	if (baseDateTime == null) {
		baseDateTime = getSysDateTime();
	}
	return this.trim().compareDate(baseDateTime.trim()) < 0;
}
//�ж�����ʱ���ַ���ָ����ʱ���Ƿ���ָ������ʱ��֮�������򷵻�true�����򷵻�false
String.prototype.isAfterDateTime = function(baseDateTime) {
	if (baseDateTime == null) {
		baseDateTime = getSysDateTime();
	}
	return this.trim().compareDate(baseDateTime.trim()) > 0;
}



//�ж��ַ������Ƿ��������ַ��������򷵻�true�����򷵻�false
String.prototype.hasSpecialChar = function() {
	specialChars.test('');
	return specialChars.test(this);
}
//ɾ���ַ����еĿո�
String.prototype.deleteSpace = function() {
	return this.replace(/( +)|(��+)/g, '');
}
//ɾ���ַ�����ָ�����ַ���
String.prototype.remove = function(str) {
	if (str == null || str == '') {
		return this;
	}
	return this.replace(str.toRegExp('g'), '');
}
//���ַ����а�����find�ַ����滻��target�ַ����������滻��Ľ���ַ���
String.prototype.replaceByString = function(find, target) {
	return this.replace(find.toRegExp('g'), target);
}
//���ַ���ת������Ӧ��������ʽ
String.prototype.toRegExp = function(regType) {
	if (regType == null || regType.trim() == '') {
		regType = 'g';
	}
	var find = ['\\\\', '\\$', '\\(', '\\)', '\\*', '\\+', '\\.', '\\[', '\\]', '\\?', '\\^', '\\{', '\\}', '\\|', '\\/'];
	var str = this;
	for (var i = 0; i < find.length; i++) {
		str = str.replace(new RegExp(find[i], 'g'), find[i]);
	}
	return new RegExp(str, regType);
}
//���ַ���ת����Date����Ҫ���ַ�������������ڻ�����ʱ���ʽ�����򷵻�null
String.prototype.toDate = function() {
	if (this.isDate()) {
		var data = this.split('-');
		return new Date(parseInt(data[0].replace(/^0+/g, '')), parseInt(data[1].replace(/^0+/g, '')) - 1, parseInt(data[2].replace(/^0+/g, '')));
	}
	else if (this.isDateTime()) {
		var data = this.split(' ');
		var date = data[0].split('-');
		var time = data[1].split(".")[0].split(':');
		return new Date(parseInt(date[0].replace(/^0+/g, '')), parseInt(date[1].replace(/^0+/g, '')) - 1, parseInt(date[2].replace(/^0+/g, '')), 
			parseInt(time[0].replace(/^0+/g, '')), parseInt(time[1].replace(/^0+/g, '')), parseInt(time[2].replace(/^0+/g, '')));
	}
	else {
		return null;
	}
}
//���ַ�����HTML��Ҫ�ı��뷽ʽ����
String.prototype.encodeHtml = function() {
	var strToCode = this.replace(/</g,"&lt;");
	strToCode = strToCode.replace(/>/g,"&gt;");
	return strToCode;
}
//���ַ�����HTML��Ҫ�ı��뷽ʽ����
String.prototype.decodeHtml = function() {
	var strToCode = this.replace(/&lt;/g,"<");
	strToCode = strToCode.replace(/&gt;/g,">");
	return strToCode;
}
/*********************************************
*�ַ�������String����չ��������
*********************************************/
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
	      obj.focus();
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
	      obj.focus();
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
	 
	 if(obj.style.display=='none'){	
        
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
		  
		   return false;
		}
	    
	  }

	  if (!isLengthOf(obj.value,obj.v_minlength,obj.v_maxlength)){
	    flag = 1;
		alert("'"+obj.v_name+"'�ĳ��Ȳ���ȷ����󳤶�Ϊ" + obj.v_maxlength + ",ע��һ�����ֵĳ���Ϊ2��");
		 
		return false;
	  
	  }
  
	  return true;
      
}

function  forSaveString(obj)
{     
      

	  if (!isLengthOf(obj.value,obj.v_minlength,obj.v_maxlength)){
	    flag = 1;
		alert("'"+obj.v_name+"'�ĳ��Ȳ���ȷ����󳤶�Ϊ" + obj.v_maxlength + ",ע��һ�����ֵĳ���Ϊ2��");
		 
		return false;
	  
	  }
  
	  return true;
      
}

function forZip(obj)
{
    if (!forString(obj)){
	  flag = 1;
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
  
    if (!isMadeOf(obj.value,numStr)){
        flag = 1;
        alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	   
	    return false;
      }
	      
    if (!isRight_length(obj.value,"6")){
      flag = 1;
      alert("'"+obj.v_name+"'��ֵ����ȷ�������д���(6λ����)");
	 
	  return false;
    } 

    return true;
}

function forMoney(obj)
{   
    if (!forString(obj)){
	  flag = 1;
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
		
    if (!isMadeOf(obj.value,moneyStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	 
	  return false;
    }
     
	if (!isValid_dot(obj.value)){
	    flag = 1;
	    alert("'" + obj.v_name + "'��ֵ����ȷ��С�����д���");
	   
	    return false;  
	}
	
	var temp = obj.value;
	if (temp.indexOf(".") != -1) {
		var str = temp.substring(temp.indexOf(".")+1, temp.length);
		if(str.length > 2){
			flag = 1;
			alert("'" + obj.v_name + "'��ֵ����ȷ��С�����д���");
			 
			return false;
		}
	}
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�������涨��Χ��");
	 
	  return false;
	} 
	
	return true;
	
}


function for0_9(obj)
{    
    if (!forString(obj)){
	  flag = 1;
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
    
	if (!isMadeOf(obj.value,numStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	 
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
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
    
	if (!isMadeOf(obj.value,strStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ����������ĸ��");
	 
	  return false;
    }
	
	return true;
	
}

function forInt(obj)
{  
    if (!forString(obj)){
	  flag = 1;
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
    
    if(obj.value < 0)
    {
		flag = 1;
		alert("'" + obj.v_name + "'��ֵ������ڵ���1�����������룡");
		obj.focus();
		
		return false;
    }
	
	if (!isValid_negative(obj.value)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ��'-'�����д���");
	 
	  return false;
    }
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�������涨��Χ��");
	 
	  return false;
	}
    
   return true;
   
}


function forFloat(obj)
{
    if (!forString(obj)){
	  flag = 1;
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
   
    if (!isMadeOf(obj.value,floatStr)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ�����������֣�");
	 
	  return false;
    }
	
	if (!isValid_dot(obj.value)){
	    flag = 1;
	    alert("'" + obj.v_name + "'��ֵ����ȷ��С�����д���");
	   
	    return false;  
	}	
	
	if (!isValid_negative(obj.value)){
      flag = 1;
      alert("'" + obj.v_name + "'��ֵ����ȷ��'-'�����д���");
	 
	  return false;
    }
	
	if (!isSizeOf(obj.value,obj.v_minvalue,obj.v_maxvalue)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ�������涨��Χ��");
	 
	  return false;
	}

   return true;

}

function forPhone(obj)
{
    if (!forString(obj)){
	  flag = 1;
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
	
	if (!isMadeOf(obj.value,phoneStr)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ������д����,���԰���(,),��,#����"); 
	 
	  return false; 
	}

}

function forEmail(obj)
{
    if (!forString(obj)){
	  flag = 1;
	 
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
	 
    return false; 

}

function forIdCard(obj)
{
    if (!forString(obj)){
	  flag = 1;
	 
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
	 
	  return false;
	}
	*/
	
	if (!isRight_length(obj.value,"15") && !isRight_length(obj.value,"18")){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ����������ȷ�����֤�ţ�"); //���֤���Ȳ���ȷ(ֻ��Ϊ15��18λ)��");
	 
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
	 
	  return false;
	}else{
	  if (obj.value.length == 0){
	    return true;
	  }
	}
  	
  	var Date = obj.value;
  	if(Date != null && Date != ""){
  		var msg = "���ڸ�ʽ���Ϸ�����ʹ�����ơ�2004-01-01 00:00:00����2004-01-01���ĸ�ʽ��";
  		if(!Date.isDate()&&!Date.isDateTime()){
  			//if(Date.getByte() != 8){
  			//	flag = 1;
  			//	alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
		    	// 
		   // 	return false;
  			//}
  			//myyear = Date.substring(0, 4);
			//mymonth = Date.substring(4, 6);
			//myday = Date.substring(6, 8);
  			//Date = myyear + "-" + mymonth + "-" + myday;
  			//if(!Date.isDate()){
  				flag = 1;
  				alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
		    	// 
		    	return false;
		   // }
		}else{
			//myyear = Date.substring(0, 4);
			//mymonth = Date.substring(5, 7);
			//myday = Date.substring(8, 10);
			myyear = Date.substring(0, Date.indexOf("-"));
			mymonth = Date.substring(Date.indexOf("-") + 1, Date.lastIndexOf("-"));
			myday = Date.substring(Date.lastIndexOf("-") + 1);
			if(mymonth.length == 1)
			{
				mymonth = "0" + mymonth;
			}
			if(myday.length > 2)
			{
				myday = myday.substring(0, myday.indexOf(" "));
			}
			if(myday.length == 1)
			{
				myday = "0" + myday;
			}
		}
	}
	if (!isMadeOf(myyear,numStr)){
		flag = 1;
		alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
    	 
	    return false;
    }
    if (!isMadeOf(mymonth,numStr)){
    	flag = 1;
    	alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
    	 
	    return false;
    }
    if (!isMadeOf(myday,numStr)){
    	flag = 1;
    	alert("'" + obj.v_name + "'��ֵ����ȷ��" + msg);
    	 
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
function forChinese(obj)
{
	
	if (!checkIsChinese(obj.value)){
	  flag = 1;
	  alert("'" + obj.v_name + "'��ֵ����ȷ������д����"); 
	 
	  return false; 
	}

}
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
        return false;
    }
}

function getNumber(value)
{ if(value!=null&&value!="undefined") {
  if(value=="" ||!isMadeOf(value,floatStr))
   return value;
  else
   return parseFloat(value);
} else 
  return value;
}


//----------------------------------------------------------
/***********************************************
���������û���
LiChun
2006-03-23
***********************************************/

//�����̬���ؼ�������Ҫ���ã�
document.write("<input type=\"hidden\" id=\"treeparameter\" orgroot=\"\" treetype=\"\" checktype=\"\" haschecked=\"\" hascheckedtext=\"\">");

function DynamicTree(DataToSend)
{
	var vsRe1 = window.showModalDialog("../../DynamicTree/GetTreeData.aspx?" + DataToSend + "",window,"dialogwidth:220px; dialogheight:100px");
	
	if(vsRe1 == undefined)
	{
		alert("���ݶ�ȡ�������û�ȡ����");
		return;
	}
	
	if(vsRe1.substring(0,5) == "error")
	{
		alert("����" + vsRe1.substring(6,vsRe1.length));
		return;
	}
	
	var parameter = "";
	
	//Ŀ¼����Ĭ��Ϊ�գ�
	parameter += "&orgroot=" + document.all.item("treeparameter").orgroot;
	
	//Ŀ¼���ͣ�all,org,user��Ĭ��Ϊall��
	parameter += "&treetype=" + document.all.item("treeparameter").treetype;
	
	//ѡ������ͣ�checkbox,radio��Ĭ��Ϊcheckbox��
	parameter += "&checktype=" + document.all.item("treeparameter").checktype;
	var vsRe2 = window.showModalDialog("../../DynamicTree/Tree.aspx?" + parameter + "",window,"dialogwidth:400px; dialogheight:450px");
	
	return vsRe2;
}

//----------------------------------------------------------
function DynamicDicTree(DataToSend)
{
	//var parameter = "";
	
	//Ŀ¼����Ĭ��Ϊ�գ�
	//parameter += "&id=" + document.all.item("treeparameter").orgroot;
	
	//Ŀ¼���ͣ�all,org,user��Ĭ��Ϊall��
	//parameter += "&treetype=" + document.all.item("treeparameter").treetype;
	
	//ѡ������ͣ�checkbox,radio��Ĭ��Ϊcheckbox��
	//parameter += "&checktype=" + document.all.item("treeparameter").checktype;
	
	var vsRe2 = window.showModalDialog("../../DynamicTree/DicTree.aspx?" + DataToSend + "",window,"dialogwidth:400px; dialogheight:450px");
	
	return vsRe2;
}

/**
*ֵ�Ƚ�
*����ֵ��true or false
*�����ˣ�����
*/
function compareValue(comvalue1,operator,comvalue2,datatype)
{ 
   	
   if(datatype == "number"){
     	
    var  value1 = parseFloat(comvalue1)
    var  value2 = parseFloat(comvalue2)
   }
   else if(datatype == "date"){
   
     if(comvalue1.isDate()&&comvalue2.isDate()){
     	var value1 = comvalue1
        var value2 = comvalue2  		
     
     }else
       return false; 
   }
   else{
     	
     var value1 = comvalue1
     var value2 = comvalue2 
   }
  	
   if(operator=="=")
     if(value1==value2)
       return true;
     else
       return false;
   if(operator=="<>")
     if(value1!=value2)
       return true;
     else
       return false;
   if(operator=="<")
     if(value1<value2)
       return true;
     else
       return false;  
   if(operator=="<=")
     if(value1<=value2)
       return true;
     else
       return false; 
   if(operator==">")
     if(value1>value2)
       return true;
     else
       return false; 
   if(operator==">=")
     if(value1>=value2)
       return true;
     else
       return false; 
   if(operator=="isnull")
     if(value1==null||value1=="")
       return true;
     else
       return false; 
   if(operator=="notnull")
     if(value1==null||value1=="")
       return false;
     else
       return true; 
   if(operator=="in")
     if(value2.indexOf(value1)!="-1")
       return true;
     else
       return false; 
   if(operator=="notin")
     if(value2.indexOf(value1)=="-1")
       return true;
     else
       return false; 
   if(operator=="beginwith")
     if(value1.indexOf(value2)=="0")
       return true;
     else
       return false; 
   if(operator=="notin")
     if(value1.indexOf(value2)!="0")
       return true;
     else
       return false;                                        
}
