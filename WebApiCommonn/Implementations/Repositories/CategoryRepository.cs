using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiCommon.DataModel;
using WebApiCommon.Interfaces.Repositories;

namespace WebApiCommon.Implementations.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ProductDbContext db;
        private DbSet<Category> _dbSet;
        public CategoryRepository(ProductDbContext context)
        {
            _dbSet = context.Categories;
            db = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _dbSet;
        }

        public Category GetCategory(int id)
        {
            return _dbSet.Find(id);
        }

        public void AddCategory(Category category)
        {
            _dbSet.Add(category);
            db.SaveChanges();
        }

        public void UpdateCategoryName(int id, Category category)
        {
            _dbSet.Update(category);
            db.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var categoryToDelete = GetCategory(id);
            _dbSet.Remove(categoryToDelete);
            db.SaveChanges();
        }
    }
}
