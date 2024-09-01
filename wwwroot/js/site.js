document.getElementById('imgInput').addEventListener('change', function (event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('imgPreview');
        output.src = reader.result;
        output.style.display = 'block'; 
    };
    if (event.target.files[0]) {
        reader.readAsDataURL(event.target.files[0]); 
    } else {
        document.getElementById('imgPreview').style.display = 'none'; 
    }
});

$(document).ready(function () {
    $("#add-to-cart-btn").click(function (e) {
        e.preventDefault();

        var productId = $(this).data("procuctId");
        var quantity = 1;
        $.ajax({
            url: @Url.Action("AddToCart", "Carts"),
            type: Post,
            data: {
                productId: productId,
                quantity: quantity
            },
            success: function (response) {
                if (response.success) {
                    $("#cartItemCount").text(response.cartItemCount);
                }
                else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("error while adding item to cart");
            }
        });
    });
});

$(document).ready(function () {
    $.ajax({
        url: '@Url.Action("GetCartItemCount", "Cart")',
        type: 'GET',
        success: function (response) {
            $("#cartItemCount").text(response.cartItemCount);
        }
    });
});
