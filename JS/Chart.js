// ==================
// Output Chart Flash
function Chart_2_3(swf,width,height,sessionSN)
{
	if(screen.width > 1000)
	{
		width = width * 1024 / 800;
		height = height * 1024 / 800;
	}
	swf = "../../includes/" + swf;
	document.write(' \n');
	document.write('<OBJECT id="FC2Column" codeBase="../../swflash.cab#version=6,0,0,0" ');
	document.write(' width="' + width + '" height="' + height + '" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" VIEWASTEXT> \n');
	document.write('<PARAM NAME="_cx" VALUE="14552"> \n');
	document.write('<PARAM NAME="_cy" VALUE="7938"> \n');
	document.write('<PARAM NAME="FlashVars" VALUE=""> \n');

	// parameter:swf,width,height
	document.write('<PARAM NAME="Movie" VALUE="' + swf + '?dataURL=../../includes/GetChartData.aspx?sessionSN=' + sessionSN + '&chartWidth=' + width + '&ChartHeight=' + (-(-12-height)) + '"> \n');
	document.write('<PARAM NAME="Src" VALUE="' + swf + '?dataURL=../../includes/GetChartData.aspx?sessionSN=' + sessionSN + '&chartWidth=' + width + '&ChartHeight=' + (-(-12-height)) + '"> \n');
	
	document.write('<PARAM NAME="WMode" VALUE="Transparent"> \n');
	document.write('<PARAM NAME="Play" VALUE="0"> \n');
	document.write('<PARAM NAME="Loop" VALUE="-1"> \n');
	document.write('<PARAM NAME="Quality" VALUE="High"> \n');
	document.write('<PARAM NAME="SAlign" VALUE="LT"> \n');
	document.write('<PARAM NAME="Menu" VALUE="-1"> \n');
	document.write('<PARAM NAME="Base" VALUE=""> \n');
	document.write('<PARAM NAME="AllowScriptAccess" VALUE=""> \n');
	document.write('<PARAM NAME="Scale" VALUE="NoScale"> \n');
	document.write('<PARAM NAME="DeviceFont" VALUE="0"> \n');
	document.write('<PARAM NAME="EmbedMovie" VALUE="0"> \n');
	document.write('<PARAM NAME="SWRemote" VALUE=""> \n');
	document.write('<PARAM NAME="MovieData" VALUE=""> \n');
	document.write('<PARAM NAME="SeamlessTabbing" VALUE="1"> \n');
	document.write('<PARAM NAME="Profile" VALUE="0"> \n');
	document.write('<PARAM NAME="ProfileAddress" VALUE=""> \n');
	document.write('<PARAM NAME="ProfilePort" VALUE="0"> \n');
	
	// parameter
	document.write('<EMBED src="' + swf + '?dataURL=../../includes/GetChartData.aspx?sessionSN=' + sessionSN + '&chartWidth=' + width + '&ChartHeight=' + (-(-12-height)) + '" ');
	document.write(' quality="high" WIDTH="' + width + '" HEIGHT="' + height + '" NAME="FC2Column" ALIGN="" ');
	document.write(' TYPE="application/x-shockwave-flash" PLUGINSPAGE="http://www.macromedia.com/go/getflashplayer"> \n');
	document.write('</EMBED> \n');
	document.write('</OBJECT> \n');
}