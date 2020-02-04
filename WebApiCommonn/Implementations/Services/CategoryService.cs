using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApiCommon.DataModel;
using WebApiCommon.Interfaces;
using WebApiCommon.Interfaces.Repositories;

namespace WebApiCommon.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private const string AllCategories = "categories";
        private const string SingleCategory = "category_";

        private readonly ILogger<CategoryService> _logger;
        private readonly ICaching<Category> _cache;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger, ICaching<Category> cache)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _cache = cache;
        }

        public IEnumerable<Category> GetCategories()
        {
            if (!_cache.CheckInCache(AllCategories))
            {
                Func<IEnumerable<Category>> scr = () => _categoryRepository.GetCategories(); ;
                _cache.SetInCache(AllCategories, scr);
                _logger.LogInformation("Set hives in cache");
            }

            return _cache.ReturnValueByKey(AllCategories);
        }

        public Category GetCategory(int id)
        {
            string key = SingleCategory + id;
            if (!_cache.CheckInCache(key))
            {
                Func<int, Category> scr = d => _categoryRepository.GetCategory(d);
                _cache.SetInCache(key, scr, id);
                _logger.LogInformation($"Set category {id} in cache");
            }
            return _cache.ReturnSingleValueByKey(key);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
            _cache.RemoveValueFromCache(AllCategories);
            _logger.LogInformation($"Remove all categories from cache");
        }

        public void UpdateCategoryName(int id, Category category)
        {
            _categoryRepository.UpdateCategoryName(id, category);
            _cache.RemoveValueFromCache(AllCategories);
            _cache.RemoveValueFromCache(SingleCategory + id);
            _logger.LogInformation($"Remove all categories and category {id} from cache");
        }

        public void DeleteCategory(int id)
        {
           _categoryRepository.DeleteCategory(id);
           _cache.RemoveValueFromCache(AllCategories);
           _cache.RemoveValueFromCache(SingleCategory + id);
           _logger.LogInformation($"Remove all categories and category {id} from cache");
        }
    }
}
