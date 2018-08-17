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

            /** VEGAS BACKGROUND  IMAGE  **/
            $(function () {
                $.vegas('slideshow', {
                    backgrounds: [
                      { src: 'assets/img/4.jpg' }
                    
                    ]
                })('overlay', {
                    /** SLIDESHOW OVERLAY IMAGE **/
                    src: 'assets/plugins/vegas/overlays/01.png' // THERE ARE TOTAL 01 TO 15 .png IMAGES AT THE PATH GIVEN, WHICH YOU CAN USE HERE
                });


            });

            /*
				Der er mulighed for scripts her
            
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


