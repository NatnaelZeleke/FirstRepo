/**
 * Created by user on 4/5/2017.
 */
$(document).ready(function () {
    $('.ui.dropdown').dropdown();
     $('.wdrop').dropdown();

    
 
    $('.rating')
        .rating({
            initialRating: 0,
            maxRating: 5,
            interactive:false
        })
    ;

     $('.rating_active')
        .rating({
            initialRating: 0,
            maxRating: 5,
            interactive:true
        })
    ;    
    $('.ui.radio.checkbox')
        .checkbox()
    ;
    $('.tabular.menu .item').tab();
  
    $('.ui.accordion')
        .accordion()
    ;
    
     $('.menu .item')
                    .tab()
            ;
});
