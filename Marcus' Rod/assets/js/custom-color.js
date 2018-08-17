/*=============================================================
    Authour : Goliath
    License: Commons Attribution 3.0

    

    100% To use For Personal And Commercial Use.
    IN EXCHANGE JUST GIVE US CREDITS AND TELL YOUR FRIENDS ABOUT US
	you can may lose your domain!
   
    ========================================================  */

(function ($) {
    "use strict";
    var mainApp = {


        plusMain: function () {

            $(function () {
                $('nav a').bind('click', function (event) {
                    var $anchor = $(this);

                    $('html, body').stop().animate({
                        scrollTop: $($anchor.attr('href')).offset().top
                    }, 1000, 'easeInOutExpo');
                    /*
                    if you don't want to use the easing effects:
                    $('html, body').stop().animate({
                        scrollTop: $($anchor.attr('href')).offset().top
                    }, 1000);
                    */
                    event.preventDefault();
                });
            });

           


            $('.Icon-trigger span').click(function () {
                if (
            $('.left-panel').css('left') == '-160px') {
                    $('.left-panel').animate({ left: '0px' });
                }
                else
                    $('.left-panel').animate({ left: '-160px' });
            });

            $('body').addClass('body-back'); 
            
           
            /*
				Mulighed for scripts
            */



        },

        initialization: function () {
            mainApp.plusMain();

        }


    }

    $(document).ready(function () {
        mainApp.plusMain();
    });


}(jQuery));


