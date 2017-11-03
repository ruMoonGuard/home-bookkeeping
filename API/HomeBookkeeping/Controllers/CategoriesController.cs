using System.Collections.Generic;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HomeBookkeeping.Controllers
{
    [Route("api/categories")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoryService;

        public CategoriesController(ICategoriesService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryService.GetCategories();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryService.GetCategory(id);
        }

        [HttpPost]
        public bool Post([FromBody] Category category)
        {
            var result = _categoryService.AddCategory(category);

            return result != null;
        }
    }
}
