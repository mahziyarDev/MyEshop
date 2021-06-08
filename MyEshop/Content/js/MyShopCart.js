$(document).ready(function() {
    countShopCart();
});


function countShopCart() {
    $.get("/api/shop",
        function (res) {
            $("#CountShopCart").html(res);
        });
}
function AddToCart(id) {
    $.get("/api/shop/"+id,
        function(res) {
            $("#CountShopCart").html(res);
            updateShopCart();
        });
}
function updateShopCart() {
    $("#ShowCart").load("/ShopCart/ShowCart").fadeOut(100).fadeIn(800);
}