﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain_Models;
using EShop.Repository;
using EShop.Service.Interface;
using System.Security.Claims;
using EShop.Service.Implementation;
using Stripe;
using EShop.Domain;
using Microsoft.Extensions.Options;

namespace EShop.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService,IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userShoppingCart = _shoppingCartService.GetByUserIdWithIncludedPrducts(Guid.Parse(userId));
            return View(userShoppingCart);
        }

        public IActionResult Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _shoppingCartService.DeleteProductFromShoppingCart(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult OrderNow()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
            {
                throw new Exception("Log in");
            }

            _shoppingCartService.OrderProducts(Guid.Parse(userId));

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult IncreaseQuantity(Guid id)
        {
            _shoppingCartService.IncreaseQuantity(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecreaseQuantity(Guid id)
        {
            _shoppingCartService.DecreaseQuantity(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {

            StripeConfiguration.ApiKey = "sk_test_51RX3e3P3AXEZO8VnEwu8XFTOOJS8Cfu38Lb99yqXJ8w4tpHFShp1IJ0XHrxiHsnEQb1Qr68Ky1BOFd9hFS4AkUaQ0093DUsx4k";
            var customerService = new Stripe.CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = _shoppingCartService.GetByUserIdWithIncludedPrducts(Guid.Parse(userId));

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "EShop Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.OrderNow();
                return RedirectToAction("SuccessPayment");
            }
            else
                return RedirectToAction("NotsuccessPayment");

        }

        public IActionResult SuccessPayment()
        {
            return View();
        }


    }
}
