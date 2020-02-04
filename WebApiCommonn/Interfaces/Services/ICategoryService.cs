using System.Collections.Generic;
using WebApiCommon.DataModel;

namespace WebApiCommon.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void AddCategory(Category category);
        void UpdateCategoryName(int id, Category category);
        void DeleteCategory(int id);
    }
}
