using System;
using System.Collections.Generic;
using System.Text;
using WebApiCommon.DataModel;

namespace WebApiCommon.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void AddCategory(Category category);
        void UpdateCategoryName(int id, Category category);
        void DeleteCategory(int id);
    }
}
