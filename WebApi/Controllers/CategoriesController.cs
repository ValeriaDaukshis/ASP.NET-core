using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiCommon.DataModel;
using WebApiCommon.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        [HttpGet("{Id}")]
        public Category GetScreen(int id)
        {
            return _categoryService.GetCategory(id);
        }

       
        [HttpPost("{Id}")]
        public IActionResult CreateScreen([FromBody]Category category)
        {
            _categoryService.AddCategory(category);
            return Ok();
        }

        
        [HttpPut("{Id}")]
        public IActionResult UpdateScreen(int id, [FromBody]Category category)
        {
            _categoryService.UpdateCategoryName(id, category);
            return Ok();
        }

        
        [HttpDelete("{Id}")]
        public IActionResult DeleteScreen(int id)
        {
            _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
