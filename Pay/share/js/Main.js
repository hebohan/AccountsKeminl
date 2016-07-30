//主页面（内框架） By youqing   2012-3-10 13:36:09

//跨页页签切换
function tabchange_TabAllpage(total, n) {
    tabchange(n, 'tabAll', total, 'tabAll_selected', 'tabAll_Noselect', 'tabAll_Noselect_noBg', 'TabPage');
}

//竖页签切换
function tabchange_TabVer(total, n) {
    tabchange(n, 'TabVer', total, 'TabVer_selected', 'TabVer_Noselect', 'TabVer_Noselect_noBg', 'TabPage');
}



//切换标签：元素（多传this，id前面前几位共有字符，一共个数，选中的class，没选中的class，选中个前一个是否替换class为。。。,内容页id前字符）
function tabchange(n, tabAll, total, tabAll_selected, tabAll_Noselect, tabAll_Noselect_noBg, TabPage) {
    for (var i = 1; i < total + 1; i++) {
        var tabName = tabAll + i;
        if (document.getElementById(tabName)) {
            document.getElementById(tabName).className = tabAll_Noselect;
        }

        var TabPageName = TabPage + i;
        if (document.getElementById(TabPageName)) {
            document.getElementById(TabPageName).style.display = "none";
        }
    }
    if (document.getElementById(tabAll + n)) {
        document.getElementById(tabAll + n).className = tabAll_selected;
    }
    // alert(tabAll + (n - 1));
    if (document.getElementById(tabAll + (n - 1)) && tabAll_Noselect_noBg != "") {
        document.getElementById(tabAll + (n - 1)).className = tabAll_Noselect_noBg;
    }

    if (document.getElementById(TabPage + n)) {
        document.getElementById(TabPage + n).style.display = "block";
    }
}


//内容页面表格变色
function ContChangeBg(id) {
    TableChangeBg(id, "#FAFEFF", "#EBF6FA", "#FFFFA4", "#FC6", "#333", "#333", 1, "tr");
}