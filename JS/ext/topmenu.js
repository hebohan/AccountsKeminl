/*
 * Ext JS Library 2.0.1
 * Copyright(c) 2006-2008, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://extjs.com/license
 */
Ext.BLANK_IMAGE_URL='js/ext/resources/images/default/s.gif'; 
Ext.onReady(function(){
    Ext.QuickTips.init();

    // Menus can be prebuilt and passed by reference
    var dateMenu = new Ext.menu.DateMenu({
        handler : function(dp, date){
            Ext.example.msg('Date Selected', 'You chose {0}.', date.format('M j, Y'));
        }
    });

    var colorMenu = new Ext.menu.ColorMenu({
        handler : function(cm, color){
            Ext.example.msg('Color Selected', 'You chose {0}.', color);
        }
    });
	//配置管理平台
    //toolbar
    //alert(menuArray);
    var menuStr = document.all('hidmenu').value.split('$');
    var typeStr = document.all('hidtype').value.split('$');
    var tb = new Ext.Toolbar();
    tb.render('toolbar');
    for(var i=0;i<menuStr.length;i++)
    {
		if(typeStr[i] == "1")
		{
			tb.add(
				{
					text:menuStr[i],
					//menu:menuArray[i],
					enableToggle: true,
					toggleHandler: functionArray[i],
					toggleGroup : 'one',
					pressed: false
				},'-'
			);
		}else{
			tb.add(
				{
					text:menuStr[i],
					menu:menuArray[i]
				},'-'
			);
		}
    }
    tb.add(
		'->', {
		    text: '返回首页',
		    enableToggle: true,
		    toggleHandler: returnmain
		},
		{
		    text: '内部门户',
		    enableToggle: true,
		    toggleHandler: returnout
		},
		{
		    text: '桌面配置',
		    enableToggle: true,
		    toggleHandler: startDeskConfig
		},
        {
            text: '退出登录',
            enableToggle: true,
            toggleHandler: returnlogin
        }
    );
    function returnmain(item, pressed) {
        window.location = 'index.aspx';
    }
    function returnlogin(item, pressed) {
        window.location = 'sys/login/login.aspx';
    }
    function returnout(item, pressed) {
        window.location = '/Portal/sys/Frame/frm.aspx';
    }
    function startDeskConfig(item, pressed) {
        //document.frames['main'].location = '/Portal/StartDesk/config.aspx';
        window.open('StartDesk/config.aspx', 'main');
    }
    Ext.get(toolbar1).on("click",hideallmenu);
	//Ext.get(toolbar1).on("click",function(){menu1.hide();menu2.hide();menu3.hide();menu4.hide();menu5.hide();menu6.hide();});
});