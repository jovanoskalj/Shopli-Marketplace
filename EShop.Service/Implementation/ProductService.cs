using EShop.Domain.Domain_Models;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly HttpClient _httpClient;

        public ProductService(IRepository<Product> productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IRepository<Category> categoryRepository, IShoppingCartService shoppingCartService, HttpClient httpClient)
        {
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _categoryRepository = categoryRepository;
            _shoppingCartService= shoppingCartService;
            _httpClient = httpClient;
        }

        public void AddProductToSoppingCart(Guid id, Guid userId, int quantity)
        {
            var shoppingCart = _shoppingCartService.GetByUserId(userId);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found");
            }

            var product = GetById(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            UpdateCartItem(product, shoppingCart, quantity);

        }


        private ProductInShoppingCart? GetProductInShoppingCart(Guid productId, Guid cartId)
        {
            return _productInShoppingCartRepository.Get(selector: x => x,
                predicate: x => x.ShoppingCartId.ToString() == cartId.ToString()
                                                && x.ProductId.ToString() == productId.ToString());
        }

        private void UpdateCartItem(Product product, ShoppingCart shoppingCart, int quantity)
        {
            var existingProduct = GetProductInShoppingCart(product.Id, shoppingCart.Id);

            if(existingProduct == null)
            {
                var productInShoppingCart = new ProductInShoppingCart
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ShoppingCartId = shoppingCart.Id,
                    Product = product,
                    ShoppingCart = shoppingCart,
                    Quantity = quantity
                };

                _productInShoppingCartRepository.Insert(productInShoppingCart);
            }
            else
            {
                existingProduct.Quantity+=quantity;
                _productInShoppingCartRepository.Update(existingProduct);
            }
        }

        public Product DeleteById(Guid id)
        {
            var product = GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _productRepository.Delete(product);
            return product;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll(selector: x => x).ToList();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Product Insert(Product product)
        {
            product.Id = Guid.NewGuid();
            return _productRepository.Insert(product);
        }

        public Product Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public AddToCartDTO GetSelectedShoppingCartProduct(Guid id)
        {
            var selectedProduct = GetById(id);

            if (selectedProduct == null)
            {
                throw new Exception("Product not found");
            }

            var addProductToCartModel = new AddToCartDTO
            {
                SelectedProductId = selectedProduct.Id,
                SelectedProductName = selectedProduct.ProductName,
                Quantity = 1
            };

            return addProductToCartModel;
        }

        public async Task<IEnumerable<Product>> GetCoursesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://fakestoreapi.com/products");
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0");

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var fakeProducts = JsonConvert.DeserializeObject<List<FakeProductDto>>(json);

                if (fakeProducts == null)
                {
                    return new List<Product>();
                }

                var products = fakeProducts.Select(fp => new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = fp.Title,
                    ProductDescription = fp.Description,
                    ProductImage = fp.Image,
                    ProductPrice = fp.Price,
                    Rating = fp.Rating?.Rate ?? 0
                });

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ГРЕШКА ВО GetCoursesAsync: " + ex.ToString());
                return new List<Product>();
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAll()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetAll()
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> InsertAsync(Product product)
        {
            return await _productRepository.InsertAsync(product);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteAsync(product);
        }

        public async Task<List<Product>> GetProductsWithDetailsAsync()
        {
            return await _productRepository.GetAll()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }

        public async Task<Product?> GetProductWithDetailsAsync(Guid id)
        {
            return await _productRepository.GetAll()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> IsProductInStockAsync(Guid id, int quantity = 1)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null && product.StockQuantity >= quantity;
        }

        public async Task UpdateStockAsync(Guid id, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.StockQuantity += quantity; // Can be negative for sales
                await _productRepository.UpdateAsync(product);
            }
        }

    public async Task AssignCategoriesToProductsAsync()
    {
        // Get all products without categories
        var products = _productRepository.GetAll<Product>(
            selector: p => p,
            predicate: p => p.CategoryId == null
        ).ToList();
        
        // Get all categories
        var electronics = _categoryRepository.Get<Category>(
            selector: c => c,
            predicate: c => c.Name == "Electronics"
        );
        var books = _categoryRepository.Get<Category>(
            selector: c => c,
            predicate: c => c.Name == "Books"
        );
        var clothing = _categoryRepository.Get<Category>(
            selector: c => c,
            predicate: c => c.Name == "Clothing"
        );
        var sports = _categoryRepository.Get<Category>(
            selector: c => c,
            predicate: c => c.Name == "Sports"
        );
        var homeGarden = _categoryRepository.Get<Category>(
            selector: c => c,
            predicate: c => c.Name == "Home & Garden"
        );

        foreach (var product in products)
        {
            var productName = product.ProductName?.ToLower() ?? "";
            var productDesc = product.ProductDescription?.ToLower() ?? "";

            // Assign based on product name and description
            if (productName.Contains("laptop") || productName.Contains("computer") || 
                productName.Contains("phone") || productName.Contains("tablet") ||
                productName.Contains("gaming") || productName.Contains("tech") ||
                productDesc.Contains("electronic") || productDesc.Contains("digital"))
            {
                product.CategoryId = electronics?.Id;
            }
            else if (productName.Contains("book") || productName.Contains("novel") ||
                     productName.Contains("guide") || productDesc.Contains("read"))
            {
                product.CategoryId = books?.Id;
            }
            else if (productName.Contains("shirt") || productName.Contains("dress") ||
                     productName.Contains("pants") || productName.Contains("shoes") ||
                     productDesc.Contains("clothing") || productDesc.Contains("fashion"))
            {
                product.CategoryId = clothing?.Id;
            }
            else if (productName.Contains("sport") || productName.Contains("ball") ||
                     productName.Contains("fitness") || productDesc.Contains("exercise"))
            {
                product.CategoryId = sports?.Id;
            }
            else if (productName.Contains("home") || productName.Contains("garden") ||
                     productName.Contains("kitchen") || productDesc.Contains("house"))
            {
                product.CategoryId = homeGarden?.Id;
            }
            else
            {
                // Default to electronics
                product.CategoryId = electronics?.Id;
            }

            await _productRepository.UpdateAsync(product);
        }
    }
    }
}
