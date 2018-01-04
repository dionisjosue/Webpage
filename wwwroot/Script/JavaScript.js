$(document).ready(function () {

    var $form = $(".form");
    var buton = $("#buybutton");
    buton.on("click", function () {
        $form.toggle();
    });
   
    var listitems = $(".buy-thumbnail li");
    listitems.on("click", function () {

        console.log("click the items " + $(this).text());
    });

    var login = $("#loginToggle");
    var formlogin = $(".Login")
    login.on("click", function () {
        formlogin.fadeToggle(1000);
    })

});