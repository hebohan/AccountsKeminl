function openDivPro() {
    this.w = new Array(); //宽度
    this.h = new Array(); //高度
    this.count = 0;
    this.pro = new Array(); //过程
    //this.oId = new Array();
}
openDivPro.prototype = {

    creatParent: function(id, openId, closeId, minH, minW, type) {//创建父容器	
        var _this = this;
        var parent = document.createElement("div");
        _this.count++
        parent.value = _this.count;
        _this.$(id).value = _this.count;
        _this.$(closeId).value = _this.count;

        _this.$(openId).value = _this.count;
        parent.style.height = _this.$(id).offsetHeight + "px";
        parent.style.width = _this.$(id).offsetWidth + "px";
        parent.style.overflow = "hidden";
        _this.w[_this.count] = _this.$(id).offsetWidth;
        _this.h[_this.count] = _this.$(id).offsetHeight;
        _this.$(id).parentNode.appendChild(parent);
        parent.appendChild(_this.$(id));
        //_this.$(closeId).onclick = function(){var __this = this;clearInterval(_this.pro[__this.value]);_this.pro[__this.value] = setInterval(function(){_this.closeDiv(parent,minH,minW,__this.value)},10)}	

        //_this.$(openId).onclick = function(){var __this = this;clearInterval(_this.pro[__this.value]);_this.pro[__this.value] = setInterval(function(){_this.openDiv(parent,_this.h[__this.value],_this.w[__this.value],__this.value)},10);}
        if (type == "wh") {
            _this.$(closeId).onclick = function() { var __this = this; clearInterval(_this.pro[__this.value]); _this.pro[__this.value] = setInterval(function() { _this.closeDiv(parent, minH, minW, __this.value) }, 10) }

            _this.$(openId).onclick = function() { var __this = this; clearInterval(_this.pro[__this.value]); _this.pro[__this.value] = setInterval(function() { _this.openDiv(parent, _this.h[__this.value], _this.w[__this.value], __this.value) }, 10); }
        }
        if (type == "h") {
            _this.$(closeId).onclick = function() { $("#"+closeId).hide(); $("#"+openId).show(); var __this = this; clearInterval(_this.pro[__this.value]); _this.pro[__this.value] = setInterval(function() { _this.closeDivH(parent, minH, __this.value) }, 10) }

            _this.$(openId).onclick = function() {  $("#" + openId).hide(); $("#" + closeId).show(); var __this = this; clearInterval(_this.pro[__this.value]); _this.pro[__this.value] = setInterval(function() { _this.openDiv(parent, _this.h[__this.value], _this.w[__this.value], __this.value) }, 10); }
        }
        if (type == "w") {
            _this.$(closeId).onclick = function() { var __this = this; clearInterval(_this.pro[__this.value]); _this.pro[__this.value] = setInterval(function() { _this.closeDivW(parent, minW, __this.value) }, 10) }

            _this.$(openId).onclick = function() { var __this = this; clearInterval(_this.pro[__this.value]); _this.pro[__this.value] = setInterval(function() { _this.openDivW(parent, _this.w[__this.value], __this.value) }, 10); }
        }
    },
    openDivH: function(o, maxH, i) {
        if (parseInt(o.style.height) < maxH) {
            o.style.height = (parseInt(o.style.height) + 1) + "px";
            o.style.height = (parseInt(o.style.height) + Math.ceil((maxH - parseInt(o.style.height)) / 10)) + "px";
        }
        if (parseInt(o.style.height) >= maxH) {
            clearInterval(this.pro[i]);
        }
    },
    closeDivH: function(o, minH, i) {
        if (parseInt(o.style.height) > minH) {
            o.style.height = (parseInt(o.style.height) - Math.ceil((parseInt(o.style.height) - minH) / 10)) + "px";
        }
        if (parseInt(o.style.height) <= minH) {
            clearInterval(this.pro[i]);
        }
    },
    openDivW: function(o, maxW, i) {
        if (parseInt(o.style.width) < maxW) {
            o.style.width = (parseInt(o.style.width) + 1) + "px";
            o.style.width = (parseInt(o.style.width) + Math.ceil((maxW - parseInt(o.style.width)) / 10)) + "px";
        }
        if (parseInt(o.style.width) >= maxW) {
            clearInterval(this.pro[i]);
        }
    },
    closeDivW: function(o, minW, i) {

        if (parseInt(o.style.width) > minW) {
            o.style.width = (parseInt(o.style.width) - Math.ceil((parseInt(o.style.width) - minW) / 10)) + "px";
        }
        if (parseInt(o.style.width) <= minW) {
            clearInterval(this.pro[i]);
        }
    },
    openDiv: function(o, maxH, maxW, i) {
        if (parseInt(o.style.height) < maxH) {
            o.style.height = (parseInt(o.style.height) + 1) + "px";
            o.style.height = (parseInt(o.style.height) + Math.ceil((maxH - parseInt(o.style.height)) / 10)) + "px";
        }
        if (parseInt(o.style.width) < maxW) {
            o.style.width = (parseInt(o.style.width) + 1) + "px";
            o.style.width = (parseInt(o.style.width) + Math.ceil((maxW - parseInt(o.style.width)) / 10)) + "px";
        }
        if (parseInt(o.style.height) >= maxH && parseInt(o.style.width) >= maxW) {
            clearInterval(this.pro[i]);
        }
    },
    closeDiv: function(o, minH, minW, i) {
        var _this = this;
        if (parseInt(o.style.height) > minH) {
            o.style.height = (parseInt(o.style.height) - Math.ceil((parseInt(o.style.height) - minH) / 10)) + "px";
        }
        if (parseInt(o.style.width) > minW) {
            o.style.width = (parseInt(o.style.width) - Math.ceil((parseInt(o.style.width) - minW) / 10)) + "px";
        }
        if (parseInt(o.style.height) <= minH && parseInt(o.style.width) <= minW) {
            clearInterval(this.pro[i]);
        }
    },
    $: function(o) {//获取对象
        if (typeof (o) == "string") {
            if (document.getElementById(o)) {
                return document.getElementById(o);
            }
            else {
                alert("errId \"" + o + "\"!");
                return false;
            }
        }
        else {
            return o;
        }
    }
}
