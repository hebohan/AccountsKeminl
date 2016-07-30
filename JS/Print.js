var gAutoPrint = true; // Flag for whether or not to automatically call the print function
function printSpecial(title,content) {

    if (document.getElementById != null) {
        var html = '<HTML>\n<HEAD>\n';

        if (document.getElementsByTagName != null) {
            var headTags = document.getElementsByTagName("head");
            if (headTags.length > 0)
                html += headTags[0].innerHTML;
            html = html.substring(0, html.length - 76);
        }

        html += '\n</HE' + 'AD>\n<BODY>\n';
        var printReadyElem = document.getElementById(content);
        if (printReadyElem != null) {
            //html +=document.getElementById("PrintTitle").innerHTML;
            html += "<p align=\"center\"><b><font size=\"6\">" + document.all(title).innerHTML.replace('~', '</font><br>') + "</b></p><table>" + printReadyElem.innerHTML;
            
        }
        else {
            //alert("Could not find the printReady section in the HTML");
            alert("没有您要的记录，请勿打印.:)");
            return;
        }

        html += '\n</table>\n</BO' + 'DY>\n</HT' + 'ML>';

        var printWin = window.open("PrintContainer.aspx", "Print");

        printWin.document.open();
        printWin.document.write(html);

        printWin.document.close();
        if (gAutoPrint)
            printWin.print();
    }
    else {
        //alert("Sorry, the print ready feature is only available in modern browsers.");
        alert("对不起，您的浏览器不支持该打印特性，请升级浏览器。");
    }
}