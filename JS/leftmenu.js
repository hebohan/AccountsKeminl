

function leftmenuitem(name){
this.name=name;
this.plat=null;
this.titleplat=null;
this.subitemplat=null;
this.moveplatup=null;
this.moveplatdown=null;
this.subitems=new Array(0);
this.subitemsid=new Array(0);
this.subitemclass="";
this.state=0;
this.normalclass="";
this.closeclass="";
this.openclass="";
this.mainplat=null;
this.titleimg="";
this.tooltext="";
this.text="";
this.href="#";
this.brother=new Array(0);
this.baseitems=5;
this.moveitemstep=45;

this.titleclick=titleclick;
function titleclick(flag){
	for(var i=0;i<this.brother.length;i++){
		if(this.brother[i].subitems.length>0){
			this.brother[i].subitemplat.style.display="none";
			if(flag==0){
				this.brother[i].moveplatup.style.display="none";
				this.brother[i].moveplatdown.style.display="none";
			}
			if(this.brother[i].state==1&&this!=this.brother[i]){
				this.brother[i].state=0;
			}
			this.brother[i].mainplat.className=this.brother[i].closeclass;
		}
	}
	if(this.state==0){
		this.subitemplat.style.display="block";
		if(flag==0){
			this.moveplatup.style.display="block";
			this.moveplatdown.style.display="block";
		}
		this.mainplat.className=this.openclass;
	}
	this.state=(this.state+1)%2;
	if(flag==0){
		this.subitemplat.style.display="block";
		this.moveplatup.style.display="block";
		this.moveplatdown.style.display="block";
		this.mainplat.className=this.openclass;
	}

}
	

}
function leftmenu(name){

	this.name=name;
	this.plat=null;
	this.subitems=new Array(0);
	this.subitemsid=new Array(0);
	this.mover;

	this.titlemouseclick=titlemouseclick;
	function titlemouseclick(thisid){
		var path=thisid.split(".");
		var makeobj=this;
		for(var j=0;j<path.length;j++){
			for(var k=0;k<this.subitemsid.length;k++){
				if(makeobj.subitemsid[k]==path[j]){
					if(j==path.length-1){
						makeobj.subitems[k].titleclick(j);
						break;
					}
					else{
						makeobj=makeobj.subitems[k];
						break;
					}
				}
			}
		}
	}
	this.submoveup=submoveup;
	function submoveup(moveobj){
		//moveobj.parentElement.doScroll("up");
		this.mover=setInterval(this.name+".moveobj("+moveobj.id+",'up')",80);
	}
	this.endmover=endmover;

	function endmover(){
		clearInterval(this.mover);
	}

	this.submovedown=submovedown;
	function submovedown(moveobj){
		//moveobj.parentElement.doScroll("down");
		this.mover=setInterval(this.name+".moveobj("+moveobj.id+",'down')",80);
	}
	this.moveobj=moveobj;
	function moveobj(obj,updown){
		obj.parentElement.doScroll(updown);
	}


	this.loadinfo=loadinfo;
	function loadinfo(strxml){
		var xmlDoc=new ActiveXObject("Msxml.DOMDocument");
		xmlDoc.loadXML(strxml);
		
		var menunodes=xmlDoc.documentElement;
		this.subitems=new Array(menunodes.childNodes.length);
		
		this.subitemsid=new Array(menunodes.childNodes.length);
		for(var i=0;i<menunodes.childNodes.length;i++){
			var mnuitem=new leftmenuitem(this.name+"leftmenu"+i);
			this.subitems[i]=mnuitem;
			this.subitemsid[i]=this.name+"leftmenu"+i;

		}
		for(var i=0;i<menunodes.childNodes.length;i++){
			this.subitems[i].closeclass=menunodes.childNodes[i].getAttribute("closeclass");
			this.subitems[i].openclass=menunodes.childNodes[i].getAttribute("openclass");
			this.subitems[i].text=menunodes.childNodes[i].getAttribute("text");
			this.subitems[i].tooltext=menunodes.childNodes[i].getAttribute("tooltext");
			this.subitems[i].subitemclass=menunodes.childNodes[i].getAttribute("subitemclass");
			this.subitems[i].brother=this.subitems;

			//---------------------------------title
			var trplat=document.createElement("TR");
			trplat.id="trbaseplat"+this.subitems[i].name;
			trplat.name="trbaseplat"+this.subitems[i].name;

			var tdplat=document.createElement("TD");
			tdplat.width="100%";
			tdplat.height="1%";
			tdplat.name="tdtitleplat"+this.subitems[i].name;
			tdplat.id="tdtitleplat"+this.subitems[i].name;
			tdplat.className=menunodes.childNodes[i].getAttribute("closeclass");
			tdplat.align="center";
			tdplat.innerHTML="<a href='#' title='"+menunodes.childNodes[i].getAttribute("tooltext")+"'>"+menunodes.childNodes[i].getAttribute("text")+"</a>";
			tdplat.attachEvent("onclick",new Function(this.name+".titlemouseclick('"+this.subitems[i].name+"')"));

			this.subitems[i].mainplat=tdplat;
			
			trplat.appendChild(tdplat);
			this.subitems[i].titleplat=trplat;
			//-------------------------------------------moveup
			var trmoveup=document.createElement("TR");
			trmoveup.id="trmoveup"+this.subitems[i].name;
			trmoveup.name="trmoveup"+this.subitems[i].name;
			trmoveup.style.display="none";

			var tdmoveup=document.createElement("TD");
			tdmoveup.widht="100%";
			tdmoveup.height="1%";
			tdmoveup.id="tdmove"+this.subitems[i].name;
			tdmoveup.name="tdmove"+this.subitems[i].name;
			tdmoveup.align="right";

			var upimg=document.createElement("IMG");
			upimg.src=menunodes.childNodes[i].getAttribute("upimg");
			upimg.border=0;
			upimg.style.cursor="hand";
			upimg.attachEvent("onmousedown",new Function(this.name+".submoveup("+this.subitems[i].name+"divplatinner)"));
			upimg.attachEvent("onmouseup",new Function(this.name+".endmover()"));
			upimg.attachEvent("onmouseout",new Function(this.name+".endmover()"));
			tdmoveup.appendChild(upimg);

			
			trmoveup.appendChild(tdmoveup);

			this.subitems[i].moveplatup=trmoveup;

			//------------------------------------------subs

			var trsubplat=document.createElement("TR");
			trsubplat.style.display="none";
			var tdsubplat=document.createElement("TD");
			tdsubplat.valign="top";
			tdsubplat.width="100%";
			tdsubplat.style.height="99%";

			var divplatouter=document.createElement("DIV");
			divplatouter.width="100%";
			divplatouter.style.height="100%";
			divplatouter.style.overflow="hidden";

			var divplatinner=document.createElement("DIV");
			divplatinner.id=this.subitems[i].name+"divplatinner";
			divplatinner.name=this.subitems[i].name+"divplatinner";
			divplatinner.style.position="relative";
			divplatinner.style.top=0;
			divplatinner.style.left=0;
			divplatinner.width="100%";
			divplatinner.valign="top";

			divplatouter.appendChild(divplatinner);
			tdsubplat.appendChild(divplatouter);
			trsubplat.appendChild(tdsubplat);

			this.subitems[i].subitemplat=trsubplat;

			//-------------------------------------------movedown
			var trmovedown=document.createElement("TR");
			trmovedown.id="trmovedown"+this.subitems[i].name;
			trmovedown.name="trmovedown"+this.subitems[i].name;
			trmovedown.style.display="none";

			var tdmovedown=document.createElement("TD");
			tdmovedown.widht="100%";
			tdmovedown.height="1%";
			tdmovedown.id="tdmove"+this.subitems[i].name;
			tdmovedown.name="tdmove"+this.subitems[i].name;
			tdmovedown.align="right";

			var downimg=document.createElement("IMG");
			downimg.src=menunodes.childNodes[i].getAttribute("downimg");
			downimg.border=0;
			downimg.style.cursor="hand";
			downimg.attachEvent("onmousedown",new Function(this.name+".submovedown("+this.subitems[i].name+"divplatinner)"));
			downimg.attachEvent("onmouseup",new Function(this.name+".endmover()"));
			downimg.attachEvent("onmouseout",new Function(this.name+".endmover()"));

			tdmovedown.appendChild(downimg);
			
			trmovedown.appendChild(tdmovedown);

			this.subitems[i].moveplatdown=trmovedown;

			this.loadsubsinfo(this.subitems[i].name,menunodes.childNodes[i],divplatinner,this.subitems[i]);



		}

	}

	this.loadsubsinfo=loadsubsinfo;
	function loadsubsinfo(parentid,menunodes,parentplat,parentobj){
		parentobj.subitems=new Array(menunodes.childNodes.length);
		parentobj.subitemsid=new Array(menunodes.childNodes.length);
		for(var i=0;i<menunodes.childNodes.length;i++){
			var mnuitem=new leftmenuitem(parentid+".lmn"+i);
			parentobj.subitems[i]=mnuitem;
			parentobj.subitemsid[i]="lmn"+i;

		}
		var tablesub=document.createElement("TABLE");
		tablesub.border=0;
		tablesub.cellSpacing=0;
		tablesub.cellPadding=0;
		tablesub.valign="top";
		tablesub.style.width="95%";
		tablesub.style.height="1%";
		var tbodysub=document.createElement("TBODY");
		tablesub.appendChild(tbodysub);

		for(var i=0;i<menunodes.childNodes.length;i++){
			if(menunodes.childNodes[i].childNodes.length==0){
				parentobj.subitems[i].tooltext=menunodes.childNodes[i].getAttribute("tooltext");
				parentobj.subitems[i].text=menunodes.childNodes[i].getAttribute("text");
				parentobj.subitems[i].href=menunodes.childNodes[i].getAttribute("href");
				parentobj.subitems[i].titleimg=menunodes.childNodes[i].getAttribute("titleimg");
				parentobj.subitems[i].plat=parentplat;
				parentobj.subitems[i].normalclass=menunodes.childNodes[i].getAttribute("normalclass");
				var trsub=document.createElement("TR");
				var tdleftsub=document.createElement("TD");
				tdleftsub.width="50%";
				tdleftsub.align="center";
				tdleftsub.className=menunodes.childNodes[i].getAttribute("normalclass");
				var titleimg=document.createElement("IMG");
				titleimg.alt=menunodes.childNodes[i].getAttribute("tooltext");
				titleimg.border=0;
				titleimg.src=menunodes.childNodes[i].getAttribute("titleimg");
				tdleftsub.appendChild(titleimg);
				var tdrightsub=document.createElement("TD");
				tdrightsub.width="50%";
				tdrightsub.align="left";
				tdrightsub.className=menunodes.childNodes[i].getAttribute("normalclass");
				var titleA=document.createElement("A");
				titleA.href=menunodes.childNodes[i].getAttribute("href");
				titleA.title=menunodes.childNodes[i].getAttribute("tooltext");
				titleA.innerText=menunodes.childNodes[i].getAttribute("text");
				tdrightsub.appendChild(titleA);
				trsub.appendChild(tdleftsub);
				trsub.appendChild(tdrightsub);
				tablesub.children(0).appendChild(trsub);

			}
			else{
				parentobj.subitems[i].closeclass=menunodes.childNodes[i].getAttribute("closeclass");
				parentobj.subitems[i].openclass=menunodes.childNodes[i].getAttribute("openclass");
				parentobj.subitems[i].text=menunodes.childNodes[i].getAttribute("text");
				parentobj.subitems[i].tooltext=menunodes.childNodes[i].getAttribute("tooltext");
				parentobj.subitems[i].subitemclass=menunodes.childNodes[i].getAttribute("subitemclass");
				parentobj.subitems[i].brother=parentobj.subitems;

				//---------------------------------title
				var trplat=document.createElement("TR");
				trplat.id="trbaseplat"+this.subitems[i].name;
				trplat.name="trbaseplat"+this.subitems[i].name;

				var tdplat=document.createElement("TD");
				tdplat.width="99%";
				//tdplat.height="1%";
				tdplat.name="tdtitleplat"+this.subitems[i].name;
				tdplat.id="tdtitleplat"+this.subitems[i].name;
				tdplat.className=menunodes.childNodes[i].getAttribute("closeclass");
				tdplat.align="center";
				tdplat.colSpan=2;
				tdplat.innerHTML="<a href='#' title='"+menunodes.childNodes[i].getAttribute("tooltext")+"'>"+menunodes.childNodes[i].getAttribute("text")+"</a>";
				tdplat.attachEvent("onclick",new Function(this.name+".titlemouseclick('"+parentobj.subitems[i].name+"')"));

				parentobj.subitems[i].mainplat=tdplat;
			
				trplat.appendChild(tdplat);
				parentobj.subitems[i].titleplat=trplat;
				//------------------------------------------subs

				var trsubplat=document.createElement("TR");
				trsubplat.style.display="none";
				var tdsubplat=document.createElement("TD");
				tdsubplat.valign="top";
				tdsubplat.width="99%";
				tdsubplat.style.height="99%";
				tdsubplat.colSpan=2;

				var divplatinner=document.createElement("DIV");
				divplatinner.id=parentobj.subitems[i].name+"divplatinner";
				divplatinner.name=parentobj.subitems[i].name+"divplatinner";
				divplatinner.style.position="relative";
				divplatinner.style.top=0;
				divplatinner.style.left=0;
				divplatinner.width="100%";
				divplatinner.valign="top";

				tdsubplat.appendChild(divplatinner);
				trsubplat.appendChild(tdsubplat);

				parentobj.subitems[i].subitemplat=trsubplat;

				this.loadsubsinfo(parentobj.subitems[i].name,menunodes.childNodes[i],divplatinner,parentobj.subitems[i]);

				tablesub.children(0).appendChild(trplat);
				tablesub.children(0).appendChild(trsubplat);
			}


		}
		parentplat.appendChild(tablesub);

	}

	this.show=show;
	function show(){
		var tableplat=document.createElement("TABLE");
		tableplat.width="100%";
		var tbodyplat=document.createElement("TBODY");
		tableplat.appendChild(tbodyplat);
		tableplat.border=0;
		tableplat.cellSpacing=0;
		tableplat.cellPadding=0;
		tableplat.height="100%";
		tableplat.valign="top";
		for(var i=0;i<this.subitems.length;i++){
			tableplat.children(0).appendChild(this.subitems[i].titleplat);
			tableplat.children(0).appendChild(this.subitems[i].moveplatup);
			tableplat.children(0).appendChild(this.subitems[i].subitemplat);
			tableplat.children(0).appendChild(this.subitems[i].moveplatdown);
			if(i==0){
				this.subitems[i].subitemplat.style.display="block";
				this.subitems[i].moveplatup.style.display="block";
				this.subitems[i].moveplatdown.style.display="block";
			}
		}
		this.plat.appendChild(tableplat);
	}


}