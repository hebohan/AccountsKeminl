//js数据业务操作（公用）
//youqing 2011-6-24 22:26:17


/**********取多选机构***************/
function OnSetScope(sign) {
    var item;
    if (sign == 1) {
        item = document.all("ObjDeptId").value;
    }
    var rtn;
    //rtn = OpenTree2('../components/generaltree/', 'OrgTreeView', 'selecteditem=' + item);
    rtn = OpenTree2('../../share/tree/', 'OrgTreeView', 'selecteditem=' + item);
    if (rtn!=undefined) {
        document.all.item("ObjDeptId").value = rtn.split('|')[0];
        document.all.item("ObjDeptName").value = rtn.split('|')[1];
        if (document.all.item("OrgTxT")) {
            document.all.item("OrgTxT").value = document.all.item("ObjDeptName").value;
        }
        if (document.all.item("OrgTxT2")) {
            document.all.item("OrgTxT2").value = document.all.item("ObjDeptName").value;
        }
    }

}
