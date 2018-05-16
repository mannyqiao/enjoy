
jQuery(document).ready(function() {
    /*
        Fullscreen background
    */
    $.backstretch("/themes/enjoytheme/content/images/backgrounds/1.jpg");    

    $("#btn_switch_to_signup").click(function(e){
       
        $("#signin").css("display","none"); 
        $("#signup").css("display","inline"); 
        console.log("changeed");
        e.preventDefault();
    });

    $("#btn_switch_to_signin").click(function(e){
        
        console.log("changeed");
          $("#signup").css("display","none"); 
          $("#signin").css("display","inline"); 
          console.log("changeed");
        e.preventDefault();
    });
});
