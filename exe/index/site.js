$(document).ready(function (){

	$("#botonerafile div").click(function (){
		$("#botonerafile div").toggleClass("selected",false);
		$("#botonerafile div").toggleClass("notselected",true);
		$(this).toggleClass("selected",true);
		$(this).toggleClass("notselected",false);
		var nom=$(this).attr("id");
		//alert(nom);
		$("#filecontent div").css("display","none");
		$('#filecontent div[content="'+nom+'"]').css("display","block");
	});

});