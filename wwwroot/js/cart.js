$(document).ready(function () {
    $(".add-to-cart-btn").click(function (e) {
        e.preventDefault();

        var productId = $(this).data('product-id');
        var quantity = 1;

        $.ajax({
            url: '/Carts/AddToCart',
            type: 'POST',
            data: {
                productId: productId,
                quantity: quantity
            },
            success: function (response) {
                if (response.success) {
                    $("#cartItemCount").text(response.cartItemCount);
                } else {
                    alert(response.message || "Failed to add product to cart.");
                }
            },
            error: function () {
                console.log("An error occurred while adding the product to cart.");
            }
        });
    });
});
$(document).ready(function () {
    // Handle decrement button click
    $(".decrement-btn").click(function (e) {
        e.preventDefault();

        var productId = $(this).data("product-id");
        var quantityChange = -1; // Decrement

        updateCartQuantity(productId, quantityChange, $(this));
    });

    // Handle increment button click
    $(".increment-btn").click(function (e) {
        e.preventDefault();

        var productId = $(this).data("product-id");
        var quantityChange = 1; // Increment

        updateCartQuantity(productId, quantityChange, $(this));
    });

    // Function to update cart quantity via AJAX
    function updateCartQuantity(productId, quantityChange, element) {
        $.ajax({
            url: '/Carts/UpdateQuantity',
            type: 'POST',
            data: {
                CartItemId: productId,
                quantity: quantityChange
            },
            success: function (response) {
                if (response.success) {
                    // Update the total item count in the cart
                    $("#cartItemCount").text(response.cartItemCount);

                    // Update the grand total in the cart
                    $('#grandTotal').text('Rs ' + response.grandTotal);

                    // Update the specific item's quantity and total price
                    var row = element.closest('tr');
                    row.find('#quantity_' + productId).val(response.quantity); // Update quantity
                    row.find('#totalPrice_' + productId).text('Rs ' + response.totalPrice); // Update total price
                } else {
                    alert(response.message || "Failed to update product quantity in the cart.");
                }
            },
            error: function () {
                console.log("An error occurred while updating the cart.");
            }
        });
    }
});


$(document).ready(function () {
    $.ajax({
        url: '/Carts/GetCartItemCount/',
        type: 'GET',
        cache: false,
        success: function (response) {
            $("#cartItemCount").text(response.cartItemCount);
        },
        error: function () {
            console.log("Error retrieving cart item count.");
        }
    });
});
