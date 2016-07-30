//解析xml字符串
function loadXML(xmlStr){ 
    if(!xmlStr)return null; //空串返回null
    if (window.ActiveXObject){
        var xmlDom=new ActiveXObject("Microsoft.XMLDOM");
    }else{
        if (document.implementation&&document.implementation.createDocument){var xmlDom=document.implementation.createDocument("","doc",null)} 
    }           
    xmlDom.async = false;
    try{
        xmlDom.loadXML(xmlStr);
    }catch(e){  //非IE浏览器
        var oParser=new DOMParser();
        xmlDom=oParser.parseFromString(xmlStr,"text/xml");
    }
    return xmlDom;
}
//下面两个函数用于解析xml为树结构输出
Ext.BLANK_IMAGE_URL='js/ext/resources/images/default/s.gif'; 
Ext.onReady(function(){
    var tree=new Ext.tree.TreePanel({el:'tree-div',animate:true,border:false,autoHeight:true,cls:'treebg'});
try{
//alert(xmlstr);
    var xmlDom=loadXML(xmlstr); 
}catch(e1){alert(e1.description+"111");}
    try{    //作为xml串解析
        tree.setRootNode(treeNodeFromXml(xmlDom.documentElement||xmlDom));
        tree.render();
    	tree.getRootNode().expand();
    }catch(e){alert(e.description);
    }
    return tree;
});
function treeNodeFromXml(XmlEl) {
    var t=((XmlEl.nodeType==3)?XmlEl.nodeValue:XmlEl.tagName);

    if(t.replace(/\s/g,'').length==0){return null}
    var result = {text : t};
    if(XmlEl.nodeType==1){

        Ext.each(XmlEl.attributes,function(a){
            if(a.nodeName=="href"){
				result[a.nodeName]=a.nodeValue.replace(/\$/g,"&");
            }else{
				result[a.nodeName]=a.nodeValue
            };
        });
        result = new Ext.tree.TreeNode(result); //根据属性设置构建树
        Ext.each(XmlEl.childNodes,function(el){
            if((el.nodeType==1)||(el.nodeType ==3)){
                var c=treeNodeFromXml(el);
                if(c){result.appendChild(c);}
            }
        });
    }
    return result;
}