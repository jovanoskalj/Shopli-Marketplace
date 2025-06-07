using EShop.Repository;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ProductImportService : IProductImportService
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;

        public ProductImportService(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        public async Task ImportProductsFromApiAsync()
        {
            var products = await _productService.GetCoursesAsync();

            foreach (var product in products)
            {
                bool exists = await _context.Products.AnyAsync(p => p.ProductName == product.ProductName);

                if (!exists)
                    await _context.Products.AddAsync(product);
            }

            await _context.SaveChangesAsync();
        }
    }
}
