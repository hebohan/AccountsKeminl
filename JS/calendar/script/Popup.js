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
        	endtarget = "../masterref/popup_meetingPresidialDeptName.jsp";
	}else if(dialogType == "POPUPLIST_MEETINGADDRESS"){
        	endtarget = "../masterref/popup_meetingAddress.jsp";
	}else if(dialogType == "POPUPLIST_MEETINGTITLE"){
        	endtarget = "../masterref/popup_meetingTitle.jsp";
	}else if(dialogType == "POPUPLIST_REMARK"){
		endtarget = "../masterref/popup_docr.jsp";
	}else if(dialogType == "POPUPLIST_POSITION"){
		endtarget = "../masterref/popup_position.jsp";
	}else if(dialogType == "POPUPLIST_UNIVERSITY"){
		endtarget = "../masterref/popup_university.jsp";
	}else if(dialogType == "POPUPLIST_MAJOR"){
		endtarget = "../masterref/popup_major.jsp";
	}else if(dialogType == "POPUPLIST_ORGAN"){
		endtarget = "../masterref/popup_organ.jsp";
	}else if(dialogType == "POPUPLIST_PROBLEMTYPE"){
		endtarget = "../masterref/popup_problemtype.jsp";
	}else if(dialogType == "POPUPLIST_CERTIFICATION"){
		endtarget = "../masterref/popup_certification.jsp";
	}else if(dialogType == "POPUPLIST_SCHEDULETYPE"){
		endtarget = "../masterref/popup_scheduletype.jsp";
	}else if(dialogType == "POPUPLIST_PLACE"){
		endtarget = "../masterref/popup_place.jsp";
	}else if(dialogType == "POPUPLIST_CONTRACTSTOP"){
		endtarget = "../masterref/popup_contractstop.jsp";
	}else if(dialogType == "POPUPLIST_WHITHER"){
		endtarget = "../masterref/popup_whither.jsp";
	}else if(dialogType == "POPUPLIST_MESSAGETITLE"){
		endtarget = "../masterref/popup_messagetitle.jsp";
	}else if(dialogType == "POPUPLIST_BOOKPURPOSE"){
		endtarget = "../masterref/popup_bookpurpose.jsp";
	} else if(dialogType == "POPUPLIST_CONTRACTSTOP"){
		endtarget = "../masterref/popup_contractstop.jsp";
	}else if(dialogType == "POPUPLIST_APPLICATIONTITLE"){
		endtarget = "../masterref/popup_applicationtitle.jsp";
	}else if(dialogType == "POPUPLIST_DDNAPPLICATIONTITLE"){
		endtarget = "../ddn/popup_ddnApplicationtitle.jsp";
	}else if(dialogType == "POPUPLIST_EMPCURPOSITION"){
		endtarget = "../masterref/popup_empcurposition.jsp";
	}else if(dialogType == "POPUPLIST_SHOWMESSAGETITLE"){
		endtarget = "../masterref/popup_showmessagetitle.jsp";
	}else if(dialogType == "POPUPLIST_COMPANYNAME"){
		endtarget = "../masterref/popup_companyname.jsp";
	}else if(dialogType == "POPUPLIST_BOOKPUBLISHER"){
		endtarget = "../masterref/popup_bookpublisher.jsp";
	}else if(dialogType == "POPUPLIST_BOOKLANGUAGE"){
		endtarget = "../masterref/popup_booklanguage.jsp";
	}else if(dialogType == "POPUPLIST_BOOKCLASSIFY"){
		endtarget = "../masterref/popup_bookclassify.jsp";
	}else if(dialogType == "POPUPLIST_PROVINCE"){
		endtarget = "../masterref/popup_province.jsp";
	}else if(dialogType == "POPUPLIST_RELATIONSHIP"){
		endtarget = "../masterref/popup_relationship.jsp";
	}else if(dialogType == "POPUPLIST_INDUSTRYTYPE"){
		endtarget = "../masterref/popup_industrytype.jsp";
	}else if(dialogType == "POPUPLIST_ENTERPRISETYPE"){
		endtarget = "../masterref/popup_enterprisetype.jsp";
	}else if(dialogType == "POPUPLIST_OPPRSTAGE"){
		endtarget = "../masterref/popup_opprstage.jsp";
	}else if(dialogType == "POPUPLIST_OPPRROLE"){
		endtarget = "../masterref/popup_opprrole.jsp";
	}else if(dialogType == "POPUPLIST_OPPRDOCTYPE"){
		endtarget = "../masterref/popup_opprdoctype.jsp";
	}else if(dialogType == "POPUPLIST_MEASUREUNIT"){
		endtarget = "../masterref/popup_measureunit.jsp";
	}else if(dialogType == "POPUPLIST_GERINFOTYPE"){
		endtarget = "../masterref/popup_gerinfotype.jsp";
	}else if(dialogType == "POPUPLIST_COLORPALATE"){
		endtarget = "../vote/palate.htm";
	}else if(dialogType == "POPUPLIST_SKILLNAME"){
		endtarget = "../masterref/popup_skilllist.jsp";
	}else if(dialogType == "POPUPLIST_SKILLLEVEL"){
		endtarget = "../masterref/popup_skilllevel.jsp";
	}else {
		endtarget = "../masterref/popup_posincharge.htm";
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

function fPopUpCalendarDlg_New(ctrlobj,dirlevel,rowno)
{
	if(N){
	    showx = 220 ; // + deltaX;
	    showy = 220; // + deltaY;
	}else{
	    showx = event.screenX - event.offsetX - 4 - 210 ; // + deltaX;
	    showy = event.screenY - event.offsetY + 18; // + deltaY;
        }
	newWINwidth = 210 + 4 + 18;
	
	var dirStr="";
	for(var i=1;i<=dirlevel;i++)
	{
	   dirStr += "../";
	}
	if(N){
	    window.top.captureEvents (Event.CLICK|Event.FOCUS);
    	    window.top.onclick=IgnoreEvents;
            window.top.onfocus=HandleFocus;
            winPopupWindow.args = ctrlobj;
            winPopupWindow.returnedValue = new Array(); 
            // winPopupWindow.returnFunc = PopupRetFunc;
            winPopupWindow.args = ctrlobj;
	        newWINwidth = 202;
            winPopupWindow.win = window.open(dirStr+"js/calendar/masterref/CalendarDlg.htm","CalendarDialog","dependent=yes,left="+showx + ",top=" + showy + ",width="+newWINwidth + ",height=182px" )
            winPopupWindow.win.focus()
            return winPopupWindow;
	}else{
	    retval = window.showModalDialog(dirStr+"js/calendar/masterref/CalendarDlg.htm", "", "dialogWidth:10px; dialogHeight:180px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
    }
        
	if( retval != null ){
//		if(rowno=="rowIndex"||rowno==null){
//          if(document.all.item(ctrlobj).getAttribute("CanWrite")!="0")
//		   document.all.item(ctrlobj).value=retval;
//		}else{
//		   var row = parseInt(rowno); 
//		   if(document.all.item(ctrlobj)(row-1).getAttribute("CanWrite")!="0")
//		     document.all.item(ctrlobj)(row-1).value=retval;
//		}  

		ctrlobj.value = retval;
	}else{
		//alert("canceled");
	}
}

function fPopUpCalendarDlg_Path(url,ctrlobj)
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
            winPopupWindow.win = window.open(url,"CalendarDialog","dependent=yes,left="+showx + ",top=" + showy + ",width="+newWINwidth + ",height=182px" )
            winPopupWindow.win.focus()
            return winPopupWindow;
	}else{
	    retval = window.showModalDialog(url, "", "dialogWidth:197px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
        }
        
	if( retval != null ){
		if(rowno=="rowIndex"||rowno==null){
          if(document.all.item(ctrlobj).getAttribute("CanWrite")!="0")
		   document.all.item(ctrlobj).value=retval;
		}else{
		   var row = parseInt(rowno); 
		   if(document.all.item(ctrlobj)(row-1).getAttribute("CanWrite")!="0")
		     document.all.item(ctrlobj)(row-1).value=retval;
		}   
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
            winPopupWindow.win = window.open("../vote/palette.htm","CalendarDialog","dependent=yes,width=242px,height=333px" )
            winPopupWindow.win.focus()
            return winPopupWindow;
    }else{	 
	showx = event.screenX - event.offsetX - 4 - 210 ; // + deltaX;
	showy = event.screenY - event.offsetY + 18; // + deltaY;
	newWINwidth = 380 + 4 + 18;

	retval = window.showModalDialog("../vote/palette.htm", "", "dialogWidth:242px; dialogHeight:333px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
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
		endtarget = "../vote/vote-chart.do?action=Init&serialId=" + para1;
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
		endtarget = "../vote/vote-chart.do?action=Init&serialId=" + para1;
	}
	var retval = window.showModalDialog(endtarget, " ", features );
    }
}

function gwp_GetSelected(aCell)
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

//function getDate(elName,rowno){
//	//fPopUpCalendarDlg(document.getElementsByName(elName)[0]);
//	fPopUpCalendarDlg_New(elName,rowno);
//}
function getDate(elName,Dirlevel) {
    if(Dirlevel == undefined)
    {
        Dirlevel = 2;
    }
    var ctrlObj = document.all(elName);  
    if(ctrlObj)
	  fPopUpCalendarDlg_New(ctrlObj,Dirlevel);
}

function getDateValue(url, elName)
{
	fPopUpCalendarDlg_Path(url,elName);
}

//选择考勤人员查询
function selPeople(f1,f2){
  dept=window.showModalDialog("/GWPWEB/Attendance/PeopleList.jsp","","dialogWidth:380px;dialogHeight:360px;center:1;status:0");
  if(dept!=null){
    if(dept[0]!=null&&dept[0]!="")f2.value=dept[0];
    if(dept[1]!=null&&dept[1]!="")f1.value=dept[1];
  }
}