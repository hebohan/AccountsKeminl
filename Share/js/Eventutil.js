//�������ͨ���¼�����	(������BrowserCheck.js)  
//�÷���portal��קҳ��
//youqing 2011-3-29 23:14:33


var EventUtil = new Object;
// �˷����������ض���������¼���oTarget��ָ������,sEventType���¼����ͣ���click��keydown�ȣ�    fnHandler���¼��ص�����
EventUtil.addEventHandler = function (oTarget, sEventType, fnHandler) {
		//alert(fnHandler);
    //firefox�����
    if (oTarget.addEventListener) {
        oTarget.addEventListener(sEventType, fnHandler, false);
    }
    //IE��
    else if (oTarget.attachEvent) {
        oTarget.attachEvent("on" + sEventType,fnHandler);
    }
    else {
        oTarget["on" + sEventType] = fnHandler;
    }
};


//�˷��������Ƴ��ض�������ض��¼���oTarget��ָ������,sEventType���¼����ͣ���click��keydown�ȣ�fnHandler���¼��ص�����
EventUtil.removeEventHandler = function (oTarget, sEventType, fnHandler) {
    if (oTarget.removeEventListener) {
        oTarget.removeEventListener(sEventType, fnHandler, false);
    } 
	else if (oTarget.detachEvent) {
        oTarget.detachEvent("on" + sEventType, fnHandler);
    } else {
        oTarget["on" + sEventType] = null;
    }
};

// ��ʽ���¼�����ΪIE������������»�ȡ�¼��ķ�ʽ��ͬ�����¼�������Ҳ������ͬ��ͨ���˷����ṩһ��һ�µ��¼�

EventUtil.formatEvent = function (oEvent) {
    //isIE��isWin���õ�һ��js�ļ����ж�������Ͳ���ϵͳ����
    if (isIE && isWin) {
        oEvent.charCode = (oEvent.type == "keypress") ? oEvent.keyCode : 0;
        //IEֻ֧��ð�ݣ���֧�ֲ���
        oEvent.eventPhase = 2;
        oEvent.isChar = (oEvent.charCode > 0);
        oEvent.pageX = oEvent.clientX + document.body.scrollLeft;
        oEvent.pageY = oEvent.clientY + document.body.scrollTop;
        //��ֹ�¼���Ĭ����Ϊ
        oEvent.preventDefault = function () {
            this.returnValue = false;
        };

         //��toElement,fromElementת��Ϊ��׼��relatedTarget
        if (oEvent.type == "mouseout") {
            oEvent.relatedTarget = oEvent.toElement;
        } else if (oEvent.type == "mouseover") {
            oEvent.relatedTarget = oEvent.fromElement;
        }
        //ȡ��ð��     
        oEvent.stopPropagation = function () {
            this.cancelBubble = true;
        };

        oEvent.target = oEvent.srcElement;
        //����¼�����ʱ�����ԣ�IEû��
        oEvent.time = (new Date).getTime();
  }
    return oEvent;
};

EventUtil.getEvent = function() {
    if (window.event) {
        //��ʽ��IE���¼�
        return this.formatEvent(window.event);
    } else {
        return EventUtil.getEvent.caller.arguments[0];
    }
};