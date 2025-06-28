using EShop.Domain.Domain_Models;

namespace EShop.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Category>> GetActiveCategoriesAsync();
        Category? GetCategoryById(Guid id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(Guid id);
        Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId);
    }
}
