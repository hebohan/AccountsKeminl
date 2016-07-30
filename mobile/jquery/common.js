//自写 公共JS

//打开页面加 载特效
$(window).load(function() {
	//$("#status").fadeOut();
	//$("#preloader").delay(350).fadeOut("slow");
})

//
function changeCode(){  
     $("#yzm_img").attr('src','../verify/verify_image?r='+Math.random());  
}