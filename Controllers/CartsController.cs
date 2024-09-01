﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecom.Models;
using Ecom.Services;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;

        public CartsController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var cart = _cartService.GetCart(user.Id);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            _cartService.AddToCart(user.Id, productId, quantity);
            var cart = _cartService.GetCart(user.Id);
            var cartItemCount = cart.CartItems.Sum(ci => ci.Quantity);


            return Json(new { success = true, cartItemCount });
        }

        [HttpPost]
        public async Task<IActionResult> BuyNow(Guid productId, int quantity = 1)
        {
            await AddToCart(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid cartItemId)
        {
            var user = await _userManager.GetUserAsync(User);

            _cartService.RemoveFromCart(user.Id, cartItemId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var user = await _userManager.GetUserAsync(User);

            _cartService.ClearCart(user.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetCartItemCount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (User == null)
            {
                return Json(new { cartItemCount = 0 });
            }
            var cart = _cartService.GetCart(user.Id);
            var cartItemCount = cart?.CartItems?.Sum(q => q.Quantity) ?? 0;
            return Json(new { cartItemCount });
        }
    }
}
