//��ʼ��
var startYear=1940;
//������
var endYear=2051;
var gdCtrl = new Object();
var goSelectTag = new Array();
var gcGray = "#808080";
var gcToggle = "#ffff00";
var gcBG = "#99CCFF";
var previousObject = null;

var gdCurDate = new Date();
var giYear = gdCurDate.getFullYear();
var giMonth = gdCurDate.getMonth()+1;
var giDay = gdCurDate.getDate();

var gCalMode = "";
var gCalDefDate = "";

var CAL_MODE_NOBLANK = "2";

N = (document.all) ? 0 : 1;
function fSetDate(iYear, iMonth, iDay){
  //VicPopCal.style.visibility = "hidden";
  if ((iYear == 0) && (iMonth == 0) && (iDay == 0)){
  	gdCtrl.value = "";
  }else{
  	iMonth = iMonth + 100 + "";
  	iMonth = iMonth.substring(1);
  	iDay   = iDay + 100 + "";
  	iDay   = iDay.substring(1);
  	gdCtrl.value = iYear+"-"+iMonth+"-"+iDay;
  }
  
  for (i in goSelectTag)
  	goSelectTag[i].style.visibility = "visible";
  goSelectTag.length = 0;
  
  if(N){
      // if(typeof(opener.winPopupWindow.args) == "") 
      // alert(opener.winPopupWindow.args.value);
      if(opener.winPopupWindow.args.value ){
      	  opener.winPopupWindow.returnedValue = gdCtrl.value;
      	  opener.winPopupWindow.args.value = gdCtrl.value;
      }else if(opener.winPopupWindow.args.value == null ||
               opener.winPopupWindow.args.value == "" ){
      	  opener.winPopupWindow.returnedValue = gdCtrl.value;
      	  opener.winPopupWindow.args.value = gdCtrl.value;
      }else{
      	  opener.winPopupWindow.returnedValue = gdCtrl.value;
      }
      if(opener.winPopupWindow.returnFunc)  opener.winPopupWindow.returnFunc();
      window.close();
  }else{
      window.returnValue=gdCtrl.value;
      window.close();
  }
}
function fSetTime(){
	gdCtrl.value = tbSelHour.value+":"+tbSelMin.value;

	window.returnValue=gdCtrl.value;
	window.close();
}
function fSetDateTime(iYear, iMonth, iDay){
  //VicPopCal.style.visibility = "hidden";
  if ((iYear == 0) && (iMonth == 0) && (iDay == 0)){
  	gdCtrl.value = "";
  }else{
  	iMonth = iMonth + 100 + "";
  	iMonth = iMonth.substring(1);
  	iDay   = iDay + 100 + "";
  	iDay   = iDay.substring(1);
  	gdCtrl.value = iYear+"-"+iMonth+"-"+iDay+"  "+tbSelHour.value+":"+tbSelMin.value+":00";
  }
  
  for (i in goSelectTag)
  	goSelectTag[i].style.visibility = "visible";
  goSelectTag.length = 0;
  
  if(N){
      // if(typeof(opener.winPopupWindow.args) == "") 
      // alert(opener.winPopupWindow.args.value);
      if(opener.winPopupWindow.args.value ){
      	  opener.winPopupWindow.returnedValue = gdCtrl.value;
      	  opener.winPopupWindow.args.value = gdCtrl.value;
      }else if(opener.winPopupWindow.args.value == null ||
               opener.winPopupWindow.args.value == "" ){
      	  opener.winPopupWindow.returnedValue = gdCtrl.value;
      	  opener.winPopupWindow.args.value = gdCtrl.value;
      }else{
      	  opener.winPopupWindow.returnedValue = gdCtrl.value;
      }
      if(opener.winPopupWindow.returnFunc)  opener.winPopupWindow.returnFunc();
      window.close();
  }else{
      window.returnValue=gdCtrl.value;
      window.close();
  }
}
function HiddenDiv()
{
	var i;
  VicPopCal.style.visibility = "hidden";
  for (i in goSelectTag)
  	goSelectTag[i].style.visibility = "visible";
  goSelectTag.length = 0;

}
function fSetSelected(aCell){
  var iOffset = 0;
  if(N){
      tbSelYear  = document.getElementById("tbSelYear");
      tbSelMonth = document.getElementById("tbSelMonth"); 
      var iYear = parseInt(tbSelYear.value);
      var iMonth = parseInt(tbSelMonth.value);
      aCell.bgColor = gcBG;
      fontObj = aCell.getElementsByTagName("font");
      var iDay = parseInt(fontObj[0].childNodes[0].nodeValue);
      // alert(fontObj[0].color);
      /*** below temp patch by maxiang ***/
      if( fontObj[0].color == gcGray ){
          iOffset = (iDay < 15 )?1:-1;
      }
      /*** above temp patch by maxiang ***/

      iMonth += iOffset;
      if (iMonth<1) {
	  iYear--;
	  iMonth = 12;
      }else if (iMonth>12){
	  iYear++;
	  iMonth = 1;
      }
  }else{
      var iYear = parseInt(tbSelYear.value);
      var iMonth = parseInt(tbSelMonth.value);
      aCell.bgColor = gcBG;
      with (aCell.children["cellText"]){
  	  var iDay = parseInt(innerText);
  	  if (color==gcGray)
		iOffset = (Victor<10)?-1:1;

	  /*** below temp patch by maxiang ***/
	  if( color == gcGray ){
		iOffset = (iDay < 15 )?1:-1;
	  }
	  /*** above temp patch by maxiang ***/

	  iMonth += iOffset;
	  if (iMonth<1) {
		iYear--;
		iMonth = 12;
	  }else if (iMonth>12){
		iYear++;
		iMonth = 1;
	  }
      }
  }
  
  fSetDateTime(iYear, iMonth, iDay);
}

function Point(iX, iY){
	this.x = iX;
	this.y = iY;
}

function fBuildCal(iYear, iMonth) {
  var aMonth=new Array();
  for(i=1;i<7;i++)
  	aMonth[i]=new Array(i);
  
  var dCalDate=new Date(iYear, iMonth-1, 1);
  var iDayOfFirst=dCalDate.getDay();
  var iDaysInMonth=new Date(iYear, iMonth, 0).getDate();
  var iOffsetLast=new Date(iYear, iMonth-1, 0).getDate()-iDayOfFirst+1;
  var iDate = 1;
  var iNext = 1;

  for (d = 0; d < 7; d++)
	aMonth[1][d] = (d<iDayOfFirst)?-(iOffsetLast+d):iDate++;
  for (w = 2; w < 7; w++)
  	for (d = 0; d < 7; d++)
		aMonth[w][d] = (iDate<=iDaysInMonth)?iDate++:-(iNext++);
  return aMonth;
}

function fDrawCal(iYear, iMonth, iCellHeight, sDateTextSize) {
  var WeekDay = new Array("��","һ","��","��","��","��","��");
  var styleTD = " bgcolor='"+gcBG+"' bordercolor='"+gcBG+"' valign='middle' align='center' height='"+iCellHeight+"' style='font-size:12px; ";

  with (document) {
	write("<tr>");
	for(i=0; i<7; i++)
		write("<td "+styleTD+" color:#990099' >" + WeekDay[i] + "</td>");
	write("</tr>");

  	for (w = 1; w < 7; w++) {
		write("<tr>");
		for (d = 0; d < 7; d++) {
			write("<td id=calCell "+styleTD+"cursor:hand;' onMouseOver='this.bgColor=gcToggle' onMouseOut='this.bgColor=gcBG' onclick='fSetSelected(this)'>");
			write("<font id=cellText name='cellText'><b> </b></font>");
			write("</td>")
		}
		write("</tr>");
	}
  }
}

function fUpdateCal(iYear, iMonth) {
  if(N){
      cellText = document.getElementsByName("cellText");
      // alert(cellText);
      myMonth = fBuildCal(iYear, iMonth);
      var i = 0;
      for (w = 0; w < 6; w++){
          for (d = 0; d < 7; d++){
              var ind = (7*w) + d;
	      Victor = i++;
    	      if (myMonth[w+1][d]<0) {
	         cellText[ind].color = gcGray;
		 cellText[ind].innerHTML = -myMonth[w+1][d];
	      }else{
		 if( d == 0 ){
		     cellText[ind].color = "red";
		 }else if( d == 6 ){
		     cellText[ind].color = "blue";
		 }else{
		     cellText[ind].color = "black";
		 }
	         // End of above maxiang
		 cellText[ind].innerHTML = myMonth[w+1][d];
	      }
	  }
      }
  }else{
      myMonth = fBuildCal(iYear, iMonth);
      var i = 0;
      for (w = 0; w < 6; w++)
	  for (d = 0; d < 7; d++)
	      with (cellText[(7*w)+d]) {
			Victor = i++;
			if (myMonth[w+1][d]<0) {
				color = gcGray;
				innerText = -myMonth[w+1][d];
			}else{
				// Modified by maxiang for we need 
				// Saturday displayed in blue font color.
				//color = ((d==0)||(d==6))?"red":"black";
				if( d == 0 ){
					color = "red";
				}else if( d == 6 ){
					color = "blue";
				}else{
					color = "black";
				}
				// End of above maxiang
				innerText = myMonth[w+1][d];
			}
		}
   }
}

function fSetYearMon(iYear, iMon){
  if (N) {
     var objSelMonth;
     objSelMonth = document.getElementById("tbSelMonth");
     objSelYear  = document.getElementById("tbSelYear");
     objSelMonth.options[iMon-1].selected = true;
     for (i = 0; i < objSelYear.length; i++)
	if (objSelYear.options[i].value == iYear)
		objSelYear.options[i].selected = true;
  }else{
     tbSelMonth.options[iMon-1].selected = true;
     for (i = 0; i < tbSelYear.length; i++)
	if (tbSelYear.options[i].value == iYear)
		tbSelYear.options[i].selected = true;
  }
  fUpdateCal(iYear, iMon);
}

function fPrevMonth(){
    if(N){
        objSelMonth = document.getElementById("tbSelMonth");
        objSelYear  = document.getElementById("tbSelYear");
        var iMon  = objSelMonth.value;
        var iYear = objSelYear.value;
    }else{
        var iMon = tbSelMonth.value;
        var iYear = tbSelYear.value;
    }
  
  if (--iMon<1) {
	  iMon = 12;
	  iYear--;
  }
  
  fSetYearMon(iYear, iMon);
}

function fNextMonth(){
    if(N){
        objSelMonth = document.getElementById("tbSelMonth");
        objSelYear  = document.getElementById("tbSelYear");
        var iMon  = objSelMonth.value;
        var iYear = objSelYear.value;
    }else{
        var iMon = tbSelMonth.value;
        var iYear = tbSelYear.value;
    }
  
  if (++iMon>12) {
	  iMon = 1;
	  iYear++;
  }
  
  fSetYearMon(iYear, iMon);
}

function fToggleTags(){
  with (document.all.tags("SELECT")){
 	for (i=0; i<length; i++)
 		if ((item(i).Victor!="Won")&&fTagInBound(item(i))){
 			item(i).style.visibility = "hidden";
 			goSelectTag[goSelectTag.length] = item(i);
 		}
  }
}

function fTagInBound(aTag){
  with (VicPopCal.style){
  	var l = parseInt(left);
  	var t = parseInt(top);
  	var r = l+parseInt(width);
  	var b = t+parseInt(height);
	var ptLT = fGetXY(aTag);
	return !((ptLT.x>r)||(ptLT.x+aTag.offsetWidth<l)||(ptLT.y>b)||(ptLT.y+aTag.offsetHeight<t));
  }
}

function fGetXY(aTag){
  var oTmp = aTag;
  var pt = new Point(0,0);
  do {
  	pt.x += oTmp.offsetLeft;
  	pt.y += oTmp.offsetTop;
  	oTmp = oTmp.offsetParent;
  } while(oTmp.tagName!="BODY");
  return pt;
}

// Main: popCtrl is the widget beyond which you want this calendar to appear;
//       dateCtrl is the widget into which you want to put the selected date.
// i.e.: <input type="text" name="dc" style="text-align:center" readonly><INPUT type="button" value="V" onclick="fPopCalendar(dc,dc);return false">
function fPopCalendar(popCtrl, dateCtrl, mode, defDate){
	gCalMode = mode;
	gCalDefDate = defDate;
	
  if (popCtrl == previousObject){
	  	if (VicPopCal.style.visibility == "visible"){
  		//HiddenDiv();
  		return true;
  	}
  	
  }
  previousObject = popCtrl;
  gdCtrl = dateCtrl;
  fSetYearMon(giYear, giMonth); 
  if(N){
        VicPopCal = document.getElementById("VicPopCal");
  	VicPopCal.style.visibility = 'visible';
  	return;
  }  
  var point = fGetXY(popCtrl);

	if( gCalMode == CAL_MODE_NOBLANK ){
		document.all.CAL_B_BLANK.style.visibility = "hidden";	
	}else{
		document.all.CAL_B_BLANK.style.visibility = "visible";
	}	

  with (VicPopCal.style) {
  	left = point.x;
	top  = point.y+popCtrl.offsetHeight;
	width = VicPopCal.offsetWidth;
	height = VicPopCal.offsetHeight;
	fToggleTags(point); 	
	visibility = 'visible';
  }
}

var gMonths = new Array("1��","2��","3��","4��","5��","6��","7��","8��","9��","10��","11��","12��");

with (document) {
write("<Div id='VicPopCal' style='OVERFLOW:hidden;POSITION:absolute;VISIBILITY:hidden;border:0px ridge;width:100%;height:100%;top:0;left:0;z-index:100;overflow:hidden'>");
write("<table border='0' bgcolor='#3366CC'>");
write("<TR>");
write("<td valign='middle' align='center'><input type='button' name='PrevMonth' value='<' style='height:20;width:20;FONT:bold' onClick='fPrevMonth()'>");
write("&nbsp;<SELECT name='tbSelYear' id=tbSelYear onChange='fUpdateCal(document.getElementById(\"tbSelYear\").value, document.getElementById(\"tbSelMonth\").value)' Victor='Won'>");
for(i=startYear;i<endYear;i++)
	write("<OPTION value='"+i+"'>"+i+"��</OPTION>");
write("</SELECT>");
write("&nbsp;<select name='tbSelMonth' id='tbSelMonth' onChange='fUpdateCal(document.getElementById(\"tbSelYear\").value, document.getElementById(\"tbSelMonth\").value)' Victor='Won'>");
for (i=0; i<12; i++)
	write("<option value='"+(i+1)+"'>"+gMonths[i]+"</option>");
write("</SELECT>");
write("&nbsp;<input type='button' name='PrevMonth' value='>' style='height:20;width:20;FONT:bold' onclick='fNextMonth()'>");
write("</td>");
write("</TR><TR>");
write("<td align='center'>");
write("<DIV style='background-color:#000066'><table width='100%' border='0'>");
fDrawCal(giYear, giMonth, 8, '12');
write("</table></DIV>");
write("</td>");
write("</tr><tr><TD align='center'>");
write("<font size='2'><b><font color='#99CCFF'>ʱ�䣺");
write("<select name='tbSelHour' Victor='Won'>");
write("<OPTION value='00'>00</OPTION>");
write("<OPTION value='01'>01</OPTION>");
write("<OPTION value='02'>02</OPTION>");
write("<OPTION value='03'>03</OPTION>");
write("<OPTION value='04'>04</OPTION>");
write("<OPTION value='05'>05</OPTION>");
write("<OPTION value='06'>06</OPTION>");
write("<OPTION value='07'>07</OPTION>");
write("<OPTION value='08'selected>08</OPTION>");
write("<OPTION value='09'>09</OPTION>");
for(i=10;i<24;i++)
	write("<OPTION value='"+i+"'>"+i+"</OPTION>");
write("</select>");
write(" �㣺");
write("<select name='tbSelMin' Victor='Won'>");
write("<OPTION value='00'>00</OPTION>");
write("<OPTION value='05'>05</OPTION>");
for(i=10;i<60;i+=5)
	write("<OPTION value='"+i+"'>"+i+"</OPTION>");
write("</select>");
write(" ��");
write("</TR><TR><TD align='center'>");
write("<TABLE width='100%'><TR><TD align='center'>");
write("<B ID=\"CAL_B_BLANK\" style='color:"+gcBG+"; visibility:visible; cursor:hand; font-size:12px' onclick='fSetDate(0,0,0)' onMouseOver='this.style.color=gcToggle' onMouseOut='this.style.color=gcBG'>���</B>");
write("</td><td algin='center'>");
write("<B style='color:"+gcBG+";cursor:hand; font-size:12px' onclick='fSetDateTime(giYear,giMonth,giDay)' onMouseOver='this.style.color=gcToggle' onMouseOut='this.style.color=gcBG'>ѡ��: "+giYear+"/"+giMonth+"/"+giDay+"</B>");
write("</td></tr></table>");
write("</TD></TR>");
write("</TABLE></Div>");

}