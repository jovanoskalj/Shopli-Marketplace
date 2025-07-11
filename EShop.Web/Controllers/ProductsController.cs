﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain_Models;
using EShop.Service.Interface;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EShop.Domain.DTO;

namespace EShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,ProductDescription,ProductImage,ProductPrice,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Insert(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid pid)
        {
            var product = _productService.GetById(pid);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ProductName,ProductDescription,ProductImage,ProductPrice,Rating")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            _productService.Update(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid id)
        {

            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _productService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult AddProductToCard(Guid id)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }

        //    _productService.AddProductToSoppingCart(id, Guid.Parse(userId));
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult AddProductToCard(Guid id)
        {
            AddToCartDTO model = _productService.GetSelectedShoppingCartProduct(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductToCard(AddToCartDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _productService.AddProductToSoppingCart(model.SelectedProductId, Guid.Parse(userId), model.Quantity);
            return RedirectToAction(nameof(Index));
        }

        // Diagnostic action to check product categories
        [HttpGet]
        public IActionResult CheckCategories()
        {
            var products = _productService.GetAll();
            var result = products.Select(p => new { 
                Name = p.ProductName, 
                CategoryId = p.CategoryId,
                HasCategory = p.CategoryId != null
            }).ToList();
            
            return Json(new { 
                TotalProducts = result.Count,
                ProductsWithCategories = result.Count(r => r.HasCategory),
                ProductsWithoutCategories = result.Count(r => !r.HasCategory),
                Products = result
            });
        }
    }
}
