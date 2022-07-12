

jQuery(document).ready(function() {
	jQuery("body").append("<div class='demo-settings'></div>");
	jQuery(".demo-settings").append("<a href='#show-settings' class='demo-button'><i class='fa fa-gear'></i>Settings</a>");
	jQuery(".demo-settings").append("<div class='demo-options'>"+
										"<div class='title'>Demo Settings</div>"+
										"<a href='#demo' rel='font-options' class='option'><img src='images/demo/image-1.png' class='demo-icon' alt='' /><span>Font Settings</span><font>Change Fonts</font></a>"+
										"<div class='option-box' rel='font-options'>"+
											"<div alt='font-options'>"+
												"<b class='sub-title'>Panel Titles & Menu Font</b>"+
												"<a href='#' class='font-bulb active' style='font-family:\"PT Sans Narrow\", sans-serif;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Exo\", sans-serif;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Kaushan Script\", cursive;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Iceberg\", cursive;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Text Me One\", sans-serif;'>Aa</a>"+
											"</div>"+
										"</div>"+
										"<div class='option-box sequal' rel='font-options'>"+
											"<div alt='article-options'>"+
												"<b class='sub-title'>Article Titles Font</b>"+
												"<a href='#' class='font-bulb active' style='font-family:\"Roboto Slab\", serif;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Exo\", sans-serif;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Kaushan Script\", cursive;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Iceberg\", cursive;'>Aa</a>"+
												"<a href='#' class='font-bulb' style='font-family:\"Text Me One\", sans-serif;'>Aa</a>"+
											"</div>"+
										"</div>"+
										"<a href='#demo' rel='color-options' class='option'><img src='images/demo/image-2.png' class='demo-icon' alt='' /><span>Color Options</span><font>Color schemes</font></a>"+
										"<div class='option-box' rel='color-options'>"+
											"<div alt='color-options'>"+
												"<b class='sub-title'>Main Color Scheme</b>"+
												"<a href='#' class='color-bulb active' style='background: #292a32;'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: #232323;'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: #7F3108;'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: #0B5264;'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: #63477E;'>&nbsp;</a>"+
											"</div>"+
										"</div>"+
										// "<div class='option-box sequal' rel='color-options'>"+
										// 	"<div alt='link-colors'>"+
										// 		"<b class='sub-title'>Link Colors</b>"+
										// 		"<a href='#' class='color-bulb active' style='background: #2582bd;'>&nbsp;</a>"+
										// 		"<a href='#' class='color-bulb' style='background: #75c025;'>&nbsp;</a>"+
										// 		"<a href='#' class='color-bulb' style='background: #c03425;'>&nbsp;</a>"+
										// 		"<a href='#' class='color-bulb' style='background: #8725c0;'>&nbsp;</a>"+
										// 		"<a href='#' class='color-bulb' style='background: #d86c0d;'>&nbsp;</a>"+
										// 	"</div>"+
										// "</div>"+
										"<a href='#demo' rel='background' class='option'><img src='images/demo/image-3.png' class='demo-icon' alt='' /><span>Background</span><font>Backgorund textures</font></a>"+
										"<div class='option-box' rel='background'>"+
											"<div alt='background'>"+
												"<b class='sub-title'>Background Texture</b>"+
												"<a href='#' class='color-bulb active' style='background: url(images/background-texture-3.jpg);'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: url(images/background-texture-1.jpg);'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: url(images/background-texture-2.jpg);'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: url(images/background-texture-4.jpg);'>&nbsp;</a>"+
												"<a href='#' class='color-bulb' style='background: url(images/background-texture-5.jpg);'>&nbsp;</a>"+
											"</div>"+
										"</div>"+
										"<a href='#demo' rel='page-width' class='option'><img src='images/demo/image-4.png' class='demo-icon' alt='' /><span>Change Width</span><font>Boxed or Full-Width</font></a>"+
										"<div class='option-box' rel='page-width'>"+
											"<div alt='option-box'>"+
												"<b class='sub-title'>Switch Page Width</b>"+
												"<a href='#' class='option-bulb active' rel='full'>Full-Width</a>"+
												"<a href='#' class='option-bulb' rel='boxed'>Boxed-Width</a>"+
											"</div>"+
										"</div>"+
									"</div>");
	
	jQuery(".demo-settings a[href=#demo]").click(function(){
		var thiselem = jQuery(this);
		thiselem.parent().find("div[rel="+thiselem.attr("rel")+"]").toggle();
		return false;
	});
	
	jQuery(".option-box div .color-bulb").click(function(){
		var thiselem = jQuery(this);
		var newcolor = thiselem.css("background-color");
		thiselem.siblings().removeClass("active");
		thiselem.addClass("active");

		if(thiselem.parent().attr("alt") == "color-options"){
			jQuery(".header-topmenu li, .slider-navigation li.active a, .main-menu, .header-topmenu, #sidebar .widget > .w-title h3, .content .panel > .p-title h2").css("background-color", newcolor);
			jQuery(".main-menu").css("color", newcolor);
			jQuery(".tab-a").css("box-shadow", "inset 0 -3px 0 "+newcolor);
		}else

		return false;
	});
	
	jQuery(".option-box div .color-bulb").click(function(){
		var thiselem = jQuery(this);
		var newcolor = thiselem.css("background");
		thiselem.siblings().removeClass("active");
		thiselem.addClass("active");

		if(thiselem.parent().attr("alt") == "background"){
			jQuery("body").css("background", newcolor);
		}

		return false;
	});
	
	jQuery(".option-box div .font-bulb").click(function(){
		var thiselem = jQuery(this);
		var newfont = thiselem.css("font-family");
		thiselem.siblings().removeClass("active");
		thiselem.addClass("active");

		if(thiselem.parent().attr("alt") == "font-options"){
			jQuery("body .toggle-menu, .main-menu ul.menu > li ul.sub-menu > li > a, .main-menu ul.menu > li > a, .header-topmenu, .breaking-news h3, .breaking-news .breaking-block h4, .panel-title, .content .panel > .p-title h2, #sidebar .widget > .w-title h3, .rating-total .master-rate, .footer .footer-widgets > .widget > h3, .footer .footer-widgets > .widget > .w-title h3").css("font-family", newfont);
		}else
		if(thiselem.parent().attr("alt") == "article-options"){
			jQuery("h1, h2, h3, h4, h5, h6, .comments-list .item .item-content span, .article-list .item .item-content span, .menu-block .featured-post .article-icons, .slider .slider-image a .slider-overlay strong, .comments-list .item .item-content span, .article-list .item .item-content span, .menu-block .featured-post .article-icons, .slider .slider-image a .slider-overlay strong").css("font-family", newfont);
		}

		return false;
	});
	
	jQuery(".option-box div .option-bulb").click(function(){
		var thiselem = jQuery(this);
		var newsize = thiselem.attr("rel");
		thiselem.siblings().removeClass("active");
		thiselem.addClass("active");

		if(thiselem.parent().attr("alt") == "option-box"){
			if(newsize == "boxed"){
				jQuery(".boxed").addClass("active");
			}else
			if(newsize == "full"){
				jQuery(".boxed").removeClass("active");
			}
		}

		return false;
	});
	
	jQuery(".demo-settings").mouseleave(function(){
		var thiselem = jQuery(this);
		thiselem.removeClass("active");
		return false;
	});
	
	jQuery(".demo-settings .demo-button").click(function(){
		jQuery(".demo-settings").addClass("active");
		return false;
	});
});

