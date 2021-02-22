$(function(){
    var $listitems = $(".card#product");

    $listitems.on('mouseover', function(){
        $(this).addClass('focused');
        $(this).children('.card-body').children('#sizes').css('display','block');
        
    });

    $listitems.on('mouseout', function(){
        $(this).removeClass('focused');
        $(this).children('.card-body').children('#sizes').css('display','none');        
    });

});