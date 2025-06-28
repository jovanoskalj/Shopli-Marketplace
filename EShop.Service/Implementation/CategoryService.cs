using EShop.Domain.Domain_Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;

        public CategoryService(IRepository<Category> categoryRepository, IRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAll()
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            return await _categoryRepository.GetAll()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public Category? GetCategoryById(Guid id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category CreateCategory(Category category)
        {
            return _categoryRepository.Insert(category);
        }

        public Category UpdateCategory(Category category)
        {
            return _categoryRepository.Update(category);
        }

        public void DeleteCategory(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            // Use the repository's GetAll method with proper selector and predicate
            var products = _productRepository.GetAll<Product>(
                selector: p => p,
                predicate: p => p.CategoryId == categoryId,
                orderBy: products => products.OrderBy(p => p.ProductName)
            );
            
            return await Task.FromResult(products.ToList());
        }
    }
}
