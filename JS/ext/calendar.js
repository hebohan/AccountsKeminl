Ext.BLANK_IMAGE_URL='js/ext/resources/images/default/s.gif'; 
Ext.onReady(function(){
	Ext.QuickTips.init();
	var text="";
	var dateMenu = new Ext.menu.DateMenu({
		handler : function(dp, date)
		{
			document.all(document.all('time0').from).value=date.format('Y-m-d');
		}
	});
	for(var i =0;i<10;i++)
	{
		if(document.all('time'+i))
		{
			var tb = new Ext.Toolbar({cls:'toolbar'});
			tb.render('time'+i);
			tb.add({
				iconCls: 'calendar',
				menu:dateMenu
			});
		}
	}
});