using EShop.Domain.Domain_Models;
using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IProductService
    {
        List<Product> GetAll();
        Task<List<Product>> GetAllAsync();
        Product? GetById(Guid id);
        Task<Product?> GetByIdAsync(Guid id);
        Product Insert(Product product);
        Task<Product> InsertAsync(Product product);
        Product Update(Product product);
        Task<Product> UpdateAsync(Product product);
        Product DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        AddToCartDTO GetSelectedShoppingCartProduct(Guid id);
        void AddProductToSoppingCart(Guid id, Guid userId, int quantity);
        Task<IEnumerable<Product>> GetCoursesAsync();
        Task<List<Product>> GetProductsWithDetailsAsync();
        Task<Product?> GetProductWithDetailsAsync(Guid id);
        Task<bool> IsProductInStockAsync(Guid id, int quantity = 1);
        Task UpdateStockAsync(Guid id, int quantity);
        Task AssignCategoriesToProductsAsync();
    }
}
