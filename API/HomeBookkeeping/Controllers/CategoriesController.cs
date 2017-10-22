using System.Collections.Generic;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeeping.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return new CategoriesService().GetCategories();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return new CategoriesService().GetCategory(id);
        }
    }
}