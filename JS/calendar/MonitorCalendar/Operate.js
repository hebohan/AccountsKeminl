function SetDateValueByTxt(objHidden, objTxt, url, btn) {
    if (url == "" || url == undefined) {
        url = "../js/calendar/MonitorCalendar/SetDateValue.aspx";
    }

    var vsRe = window.showModalDialog(url, "", "dialogWidth:420px;dialogHeight:300px");

    if (vsRe != null && vsRe != undefined) {
        document.all.item(objHidden).value = vsRe;

        if (vsRe == "") {
            document.all.item(objTxt).value = "(不限)";
        }
        else {
            if (vsRe.length > 10) {
                vsRe = vsRe.replace("~", " 至 ");
            }
            else if (vsRe.length == 7) {
                vsRe = vsRe.replace("-", "年");
                vsRe = vsRe + "月";
            }
            else if (vsRe.length == 4) {
                vsRe = vsRe + "年";
            }

            document.all.item(objTxt).value = vsRe;
        }

        if (btn != "" && btn != undefined) {
            document.all.item(btn).click();
        }
    }
}

function InitDateByTxt(date, dateTxt) {
    var objDate = document.all.item(date);
    var objDateTxt = document.all.item(dateTxt);
    if (objDate != null && objDateTxt != null) {
        if (objDate.value == "") {
            objDateTxt.value = "(不限)";
        }
        else {
            var dateValue = objDate.value;
            if (dateValue.length > 10) {
                dateValue = dateValue.replace("~", " 至 ");
            }
            else if (dateValue.length == 7) {
                dateValue = dateValue.replace("-", "年");
                dateValue = dateValue + "月";
            }
            else if (dateValue.length == 4) {
                dateValue = dateValue + "年";
            }

            objDateTxt.value = dateValue;
        }
    }
}