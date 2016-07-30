function showSwf(strpath,intwidth,intheight,transpart,inputurl)
{
	if(transpart=="1")
		{
			document.writeln("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0' width='"+intwidth+"' height='"+intheight+"' />");
			document.writeln("<param name='movie' value='"+strpath+"' />");
			document.writeln("<param name='FlashVars' value='inputurl="+inputurl+"' />");
			document.writeln("<param name='quality' value='high' />");
			document.writeln("<param name='wmode' value='transparent' /> ")
			document.writeln("<embed src='"+strpath+"' FlashVars='inputurl="+inputurl+"' wmode='transparent'  quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' width='"+intwidth+"' height='"+intheight+"'></embed>");
			document.writeln("</object>");
		}
	else
		{
			document.writeln("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0' width='"+intwidth+"' height='"+intheight+"' />");
			document.writeln("<param name='movie' value='"+strpath+"' />");
			document.writeln("<param name='FlashVars' value='inputurl="+inputurl+"' />");
			document.writeln("<param name='quality' value='high' />");
			document.writeln("<embed src='"+strpath+"' FlashVars='inputurl="+inputurl+"'  quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' width='"+intwidth+"' height='"+intheight+"'></embed>");
			document.writeln("</object>");
		}
}