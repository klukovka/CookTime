var delay_popup = 2000;
setTimeout("document.getElementById('overlay').style.display='block'", delay_popup);

function change() {
    window.location = "index.html?max=" + max.value + "&speed=" + speed.value;
}

jQuery(document).ready(function ($) {
    $(window).load(function () {
        setTimeout(function () {
            $('.preloader').fadeOut('slow', function () { });
        }, 1000);

    });
});