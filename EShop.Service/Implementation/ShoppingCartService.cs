﻿using EShop.Domain.Domain_Models;
using EShop.Domain.DTO;
using EShop.Domain.Email;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
            _userRepository = userRepository;
            _emailService = emailService;   
        }


        public void DeleteProductFromShoppingCart(Guid productInShoppingCartId)
        {
            
            var prodictInShoppingCart = _productInShoppingCartRepository.Get(selector: x => x,
                                                                             predicate: x => x.Id.Equals(productInShoppingCartId));

            if (prodictInShoppingCart == null)
            {
                throw new Exception("Product in shopping cart not found");
            }

            _productInShoppingCartRepository.Delete(prodictInShoppingCart);
        }

        public ShoppingCart? GetByUserId(Guid userId)
        {
            var userCart = _shoppingCartRepository.Get(selector: x => x,
                                                       predicate: x => x.OwnerId != null && x.OwnerId.Equals(userId.ToString()));

            // If no cart exists for the user, create one
            if (userCart == null)
            {
                userCart = new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId.ToString(),
                    AllProducts = new List<ProductInShoppingCart>()
                };
                _shoppingCartRepository.Insert(userCart);
            }

            return userCart;
        }

        public ShoppingCartDTO GetByUserIdWithIncludedPrducts(Guid userId)
        {
            var userCart = _shoppingCartRepository.Get(selector: x => x,
                                               predicate: x => x.OwnerId != null && x.OwnerId.Equals(userId.ToString()),
                                               include: x => x.Include(z => z.AllProducts).ThenInclude(m => m.Product));

            // If no cart exists for the user, create one
            if (userCart == null)
            {
                userCart = new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId.ToString(),
                    AllProducts = new List<ProductInShoppingCart>()
                };
                _shoppingCartRepository.Insert(userCart);
            }

            var allProducts = userCart.AllProducts?.ToList() ?? new List<ProductInShoppingCart>();

            var allProductPrices = allProducts.Select(z => new
            {
                ProductPrice = z.Product?.ProductPrice ?? 0,
                Quantity = z.Quantity
            }).ToList();

            double totalPrice = 0.0;

            foreach (var item in allProductPrices)
            {
                totalPrice += item.Quantity * item.ProductPrice;
            }

            ShoppingCartDTO model = new ShoppingCartDTO
            {
                Products = allProducts,
                TotalPrice = totalPrice
            };

            return model;
        }

        public bool OrderProducts(Guid userId)
        {

            var userCart = _shoppingCartRepository.Get(selector: x => x,
                                              predicate: x => x.OwnerId != null && x.OwnerId.Equals(userId.ToString()),
                                              include: x => x.Include(z => z.AllProducts).ThenInclude(m => m.Product));

            // Check if user has a cart and products in it
            if (userCart == null || userCart.AllProducts == null || !userCart.AllProducts.Any())
            {
                return false; // No cart or no products to order
            }

            var loggedInUser = _userRepository.GetUserById(userId.ToString());

            var emailMessage = new EmailMessage();

            emailMessage.MailTo = loggedInUser.Email;
            emailMessage.Subject = "Successfull order";

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OwnerId = loggedInUser.Id,
                Owner = loggedInUser
            };

            _orderRepository.Insert(order);

            var productInOrder = userCart.AllProducts.Select(x => new ProductInOrder
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Order = order,
                ProductId = x.ProductId,
                OrderedProduct = x.Product,
                Quantity = x.Quantity
            }).ToList();

            StringBuilder sb = new StringBuilder();

            var totalPrice = 0.0;

            sb.AppendLine("Your order is completed. The order conatins: ");

            for (int i = 1; i <= productInOrder.Count(); i++)
            {
                var currentItem = productInOrder[i - 1];
                if (currentItem.OrderedProduct != null)
                {
                    totalPrice += currentItem.Quantity * currentItem.OrderedProduct.ProductPrice;
                    sb.AppendLine(i.ToString() + ". " + currentItem.OrderedProduct.ProductName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.OrderedProduct.ProductPrice);
                }
            }

            sb.AppendLine("Total price for your order: " + totalPrice.ToString());
            emailMessage.Content = sb.ToString();



            //_productInOrderRepository.InsertMany(productInOrder);
            foreach (var product in productInOrder)
            {
                _productInOrderRepository.Insert(product);
            }

            userCart.AllProducts.Clear();

            _shoppingCartRepository.Update(userCart);

    

            return true;
        }
        public void IncreaseQuantity(Guid productInShoppingCartId)
        {
            var productInCart = _productInShoppingCartRepository.Get(
                selector: x => x,
                predicate: x => x.Id == productInShoppingCartId
            );

            if (productInCart == null)
            {
                throw new Exception("Product in shopping cart not found");
            }

            productInCart.Quantity += 1;

            _productInShoppingCartRepository.Update(productInCart);
        }

        public void DecreaseQuantity(Guid productInShoppingCartId)
        {
            var productInCart = _productInShoppingCartRepository.Get(
                selector: x => x,
                predicate: x => x.Id == productInShoppingCartId
            );

            if (productInCart == null)
            {
                throw new Exception("Product in shopping cart not found");
            }

            if (productInCart.Quantity > 1)
            {
                productInCart.Quantity -= 1;
                _productInShoppingCartRepository.Update(productInCart);
            }
            // Ако quantity е 1, немој да намалуваш под 1 (можеш и да избришеш, ако сакаш)
        }

    }
}
