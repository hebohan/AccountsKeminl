var gcToggle = "#ffff00";
var gcBG = "#CCCCFF";

var ctlObj = new Object();
var winPopupWindow = new Object(); 
N = (document.all) ? 0 : 1;
 
function IgnoreEvents(e)
{
  return false
}
function HandleFocus()
{
  if (winPopupWindow)
  {
    if (!winPopupWindow.win.closed)
    {
      winPopupWindow.win.focus()
    }
    else
    {
      window.top.releaseEvents (Event.CLICK|Event.FOCUS)
    }
  }
  return false
}

function fPopUpDlg(dialogType,ctl,WINname,WINwidth,WINheight){
N = (document.all) ? 0 : 1;

	var endtarget;
	if(dialogType == "POPUPLIST_MEETINGPRESIDIALDEPTNAME"){
        	endtarget = "/GWPWEB/resource/calendar/masterref/popup_meetingPresidialDeptName.jsp";
	}else if(dialogType == "POPUPLIST_MEETINGADDRESS"){
        	endtarget = "/GWPWEB/resource/calendar/masterref/popup_meetingAddress.jsp";
	}else if(dialogType == "POPUPLIST_MEETINGTITLE"){
        	endtarget = "/GWPWEB/resource/calendar/masterref/popup_meetingTitle.jsp";
	}else if(dialogType == "POPUPLIST_REMARK"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_docr.jsp";
	}else if(dialogType == "POPUPLIST_POSITION"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_position.jsp";
	}else if(dialogType == "POPUPLIST_UNIVERSITY"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_university.jsp";
	}else if(dialogType == "POPUPLIST_MAJOR"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_major.jsp";
	}else if(dialogType == "POPUPLIST_ORGAN"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_organ.jsp";
	}else if(dialogType == "POPUPLIST_PROBLEMTYPE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_problemtype.jsp";
	}else if(dialogType == "POPUPLIST_CERTIFICATION"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_certification.jsp";
	}else if(dialogType == "POPUPLIST_SCHEDULETYPE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_scheduletype.jsp";
	}else if(dialogType == "POPUPLIST_PLACE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_place.jsp";
	}else if(dialogType == "POPUPLIST_CONTRACTSTOP"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_contractstop.jsp";
	}else if(dialogType == "POPUPLIST_WHITHER"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_whither.jsp";
	}else if(dialogType == "POPUPLIST_MESSAGETITLE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_messagetitle.jsp";
	}else if(dialogType == "POPUPLIST_BOOKPURPOSE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_bookpurpose.jsp";
	} else if(dialogType == "POPUPLIST_CONTRACTSTOP"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_contractstop.jsp";
	}else if(dialogType == "POPUPLIST_APPLICATIONTITLE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_applicationtitle.jsp";
	}else if(dialogType == "POPUPLIST_DDNAPPLICATIONTITLE"){
		endtarget = "/GWPWEB/resource/calendar/ddn/popup_ddnApplicationtitle.jsp";
	}else if(dialogType == "POPUPLIST_EMPCURPOSITION"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_empcurposition.jsp";
	}else if(dialogType == "POPUPLIST_SHOWMESSAGETITLE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_showmessagetitle.jsp";
	}else if(dialogType == "POPUPLIST_COMPANYNAME"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_companyname.jsp";
	}else if(dialogType == "POPUPLIST_BOOKPUBLISHER"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_bookpublisher.jsp";
	}else if(dialogType == "POPUPLIST_BOOKLANGUAGE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_booklanguage.jsp";
	}else if(dialogType == "POPUPLIST_BOOKCLASSIFY"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_bookclassify.jsp";
	}else if(dialogType == "POPUPLIST_PROVINCE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_province.jsp";
	}else if(dialogType == "POPUPLIST_RELATIONSHIP"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_relationship.jsp";
	}else if(dialogType == "POPUPLIST_INDUSTRYTYPE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_industrytype.jsp";
	}else if(dialogType == "POPUPLIST_ENTERPRISETYPE"){
		endtarget = "/GWPWEB/resource/calendar/masterref/popup_enterprisetype.jsp";
	}else if(dialogType == "POPUPLIST_OPPRSTAGE"){
		endtarget = "/GWPWEB/calendar/masterref/popup_opprstage.jsp";
	}else if(dialogType == "POPUPLIST_OPPRROLE"){
		endtarget = "/GWPWEB/calendar/masterref/popup_opprrole.jsp";
	}else if(dialogType == "POPUPLIST_OPPRDOCTYPE"){
		endtarget = "/GWPWEB/calendar/masterref/popup_opprdoctype.jsp";
	}else if(dialogType == "POPUPLIST_MEASUREUNIT"){
		endtarget = "/GWPWEB/calendar/masterref/popup_measureunit.jsp";
	}else if(dialogType == "POPUPLIST_GERINFOTYPE"){
		endtarget = "/GWPWEB/calendar/masterref/popup_gerinfotype.jsp";
	}else if(dialogType == "POPUPLIST_COLORPALATE"){
		endtarget = "/GWPWEB/calendar/vote/palate.htm";
	}else if(dialogType == "POPUPLIST_SKILLNAME"){
		endtarget = "/GWPWEB/calendar/masterref/popup_skilllist.jsp";
	}else if(dialogType == "POPUPLIST_SKILLLEVEL"){
		endtarget = "/GWPWEB/calendar/masterref/popup_skilllevel.jsp";
	}else {
		endtarget = "/GWPWEB/calendar/masterref/popup_posincharge.htm";
	}
	if(N){
	    	showx = window.screen.width /2;
	    	showy = window.screen.height /2;
	}else{
	    showx = event.screenX - event.offsetX - 4 - WINwidth ; // + deltaX;
	    showy = event.screenY - event.offsetY + 18; // + deltaY;
	}

	if (dialogType == "POPUPLIST_CONTRACTSTOP" ){
	    if(N){
	    	showx = window.screen.width /2;
	    	showy = window.screen.height /2;
	    }else{
	        showx = event.screenX - event.offsetX - WINwidth + 150; 
  	        showy = event.screenY - event.offsetY + 20; 
	    }
	} 

	newWINwidth = WINwidth + 4 + 18;
	var retval;
	if(N){
	    window.top.captureEvents (Event.CLICK|Event.FOCUS);
    	    window.top.onclick=IgnoreEvents;
            window.top.onfocus=HandleFocus;
            winPopupWindow.returnedValue = new Array(); 
   	    if (dialogType == "POPUPLIST_CONTRACTSTOP" ){
	        if(N){
            	    winPopupWindow.returnFunc = HRMContractN6SubmitDelete;
	        }
	    }
            winPopupWindow.args = ctl;
            winPopupWindow.win = window.open(endtarget,"PopupDialog","dependent=yes,left="+showx + ",top=" + showy + ",width="+newWINwidth + ",height=" + WINheight )
            winPopupWindow.win.focus()
            winPopupWindow.win.screen.top = showy;
            winPopupWindow.win.screen.left = showx;
            return winPopupWindow;
	}else{
		var features =
		'dialogWidth:'  + newWINwidth  + 'px;' +
		'dialogHeight:' + WINheight + 'px;' +
		'dialogLeft:'   + showx     + 'px;' +
		'dialogTop:'    + showy     + 'px;' +
		'directories:no; localtion:no; menubar:no; status=no; toolbar=no;scrollbars:no;Resizeable=no';
	    retval = window.showModalDialog(endtarget, " ", features );
        }

	if( retval != null ){
		retval = trim(retval);
		ctl.value = retval;
	}else{
		//alert("canceled");
	}
}

function fPopUpCalendarDlg(ctrlobj)
{
	if(N){
	    showx = 220 ; // + deltaX;
	    showy = 220; // + deltaY;
	}else{
	    showx = event.screenX - event.offsetX - 4 - 210 ; // + deltaX;
	    showy = event.screenY - event.offsetY + 18; // + deltaY;
        }
	newWINwidth = 210 + 4 + 18;
	if(N){
	    window.top.captureEvents (Event.CLICK|Event.FOCUS);
    	    window.top.onclick=IgnoreEvents;
            window.top.onfocus=HandleFocus;
            winPopupWindow.args = ctrlobj;
            winPopupWindow.returnedValue = new Array(); 
            // winPopupWindow.returnFunc = PopupRetFunc;
            winPopupWindow.args = ctrlobj;
	    newWINwidth = 202;
            winPopupWindow.win = window.open("../masterref/CalendarDlg.htm","CalendarDialog","dependent=yes,left="+showx + ",top=" + showy + ",width="+newWINwidth + ",height=182px" )
            winPopupWindow.win.focus()
            return winPopupWindow;
	}else{
	    retval = window.showModalDialog("../masterref/CalendarDlg.htm", "", "dialogWidth:197px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
        }
        
	if( retval != null ){
		ctrlobj.value = retval;
	}else{
		//alert("canceled");
	}
}

function fPopUpCalendarDlg_New(ctrlobj)
{  
	if(N){
	    showx = 220 ; // + deltaX;
	    showy = 220; // + deltaY;
	}else{
	    showx = event.screenX - event.offsetX - 4 - 210 ; // + deltaX;
	    showy = event.screenY - event.offsetY + 18; // + deltaY;
        }
	newWINwidth = 210 + 4 + 18;
	if(N){
	    window.top.captureEvents (Event.CLICK|Event.FOCUS);
    	    window.top.onclick=IgnoreEvents;
            window.top.onfocus=HandleFocus;
            winPopupWindow.args = ctrlobj;
            winPopupWindow.returnedValue = new Array(); 
            // winPopupWindow.returnFunc = PopupRetFunc;
            winPopupWindow.args = ctrlobj;
	    newWINwidth = 202;
            winPopupWindow.win = window.open("../script/js/calendar/masterref/CalendarDlg.htm","CalendarDialog","dependent=yes,left="+showx + ",top=" + showy + ",width="+newWINwidth + ",height=182px" )
            winPopupWindow.win.focus()
            return winPopupWindow;
	}else{

	    retval = window.showModalDialog("../script/js/calendar/masterref/CalendarDlg.htm", "", "dialogWidth:197px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
        }
        
	if( retval != null ){
		//alert(ctrlobj.name);
		document.all.item(ctrlobj).value=retval;
		//ctrlobj.value = retval;
	}else{
		//alert("canceled");
	}
}

function fPopUpColorDlg(ctrlobj)
{
    if(N){
	    window.top.captureEvents (Event.CLICK|Event.FOCUS);
    	    window.top.onclick=IgnoreEvents;
            window.top.onfocus=HandleFocus;
            winPopupWindow.args = ctrlobj;
            winPopupWindow.returnedValue = new Array(); 
            winPopupWindow.win = window.open("/GWPWEB/resource/calendar/vote/palette.htm","CalendarDialog","dependent=yes,width=242px,height=333px" )
            winPopupWindow.win.focus()
            return winPopupWindow;
    }else{	 
	showx = event.screenX - event.offsetX - 4 - 210 ; // + deltaX;
	showy = event.screenY - event.offsetY + 18; // + deltaY;
	newWINwidth = 380 + 4 + 18;

	retval = window.showModalDialog("/GWPWEB/resource/calendar/vote/palette.htm", "", "dialogWidth:242px; dialogHeight:333px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
	if( retval != null ){
		ctrlobj.value = retval;
	}else{
	}
    }
}

function fPopUpChart(dialogType,WINwidth,WINheight, para1,para2)
{
    if(N){
	    window.top.captureEvents (Event.CLICK|Event.FOCUS);
    	    window.top.onclick=IgnoreEvents;
            window.top.onfocus=HandleFocus;
	    if(dialogType == "POPUP_VOTECHART"){
		endtarget = "/GWPWEB/resource/calendar/vote/vote-chart.do?action=Init&serialId=" + para1;
	    }
    	    showx = 30;
	    showy = 30;
            winPopupWindow.win = window.open(endtarget,"CalendarDialog","dependent=yes,width="+WINwidth + ",height="+WINheight + ",left=" + showx + ",top=" + showy  )
            winPopupWindow.win.focus()
            return winPopupWindow;
    }else{
	showx = event.screenX / 3 ; // + deltaX;
	showy = event.screenY / 3 ; // + deltaY;

	var features =
		'dialogWidth:'  + WINwidth  + 'px;' +
		'dialogHeight:' + WINheight + 'px;' +
		'dialogLeft:'   + showx     + 'px;' +
		'dialogTop:'    + showy     + 'px;' +
		'directories:no; localtion:no; menubar:no; status=no; toolbar=no;scrollbars:no;Resizeable=no';

	if(dialogType == "POPUP_VOTECHART"){
		endtarget = "/GWPWEB/resource/calendar/vote/vote-chart.do?action=Init&serialId=" + para1;
	}
	var retval = window.showModalDialog(endtarget, " ", features );
    }
}

function IOFFICE_GetSelected(aCell)
{
	if(document.all){
  		window.returnValue = aCell.innerText;
		window.close();
	}else{
		// alert(opener.winPopupWindow.returnFunc);
		// alert(aCell.innerHTML);
		// alert(aCell.childNodes[0].nodeValue);
		// alert(opener.winPopupWindow.args);
		// alert(opener.winPopupWindow.args.value);
	    opener.winPopupWindow.returnedValue = aCell.childNodes[0].nodeValue;
		opener.winPopupWindow.args.value = trim(aCell.childNodes[0].nodeValue);
		if(opener.winPopupWindow.returnFunc) opener.winPopupWindow.returnFunc();
 		window.close();
	}
  
}

//用于信息检索的函数
function selectType(type){
	document.forms[0].SearchType.value = type.value;
	//alert(document.forms[0].SearchType.value);
	if (type.value == "date"){
		//selectDate(document.getElementsByName("SelDate")[0]);
		document.getElementById("date").style.removeAttribute("display");
		document.getElementById("people").style.display = "none";
	}
	else if (type.value == "people"){
		document.getElementById("date").style.display = "none";
		document.getElementById("people").style.removeAttribute("display");
	}
	else document.getElementById("date").style.display = "none";
}

function selectDate(date){
	var da = new Date();
	if (date.value != "other"){
		document.forms[0].Info_Search_EndDate.value = da.getFullYear() + "/" + (new Number((da.getMonth() + 1)).toString().length == 1 ? ("0" + (da.getMonth() + 1)) : (da.getMonth() + 1)) + "/" + (new Number(da.getDate()).toString().length == 1 ? ("0" + da.getDate()) : da.getDate());
		da.setDate(da.getDate() - parseInt(date.value));
		document.forms[0].Info_Search_StartDate.value = da.getFullYear() + "/" + (new Number((da.getMonth() + 1)).toString().length == 1 ? ("0" + (da.getMonth() + 1)) : (da.getMonth() + 1)) + "/" + (new Number(da.getDate()).toString().length == 1 ? ("0" + da.getDate()) : da.getDate());
	}
}

function getDate(elName){
	//fPopUpCalendarDlg(document.getElementsByName(elName)[0]);
	fPopUpCalendarDlg_New(elName);
}

//选择考勤人员查询
function selPeople(f1,f2){
  dept=window.showModalDialog("/GWPWEB/Attendance/PeopleList.jsp","","dialogWidth:380px;dialogHeight:360px;center:1;status:0");
  if(dept!=null){
    if(dept[0]!=null&&dept[0]!="")f2.value=dept[0];
    if(dept[1]!=null&&dept[1]!="")f1.value=dept[1];
  }
}