﻿using System.Collections.Generic;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeeping.Controllers
{
  [Route("api/categories")]
  public class CategoriesController : Controller
  {
    private readonly ICategoriesService _categoryService;

    public CategoriesController(ICategoriesService categoryService)
    {
      _categoryService = categoryService;
    }

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

    public bool Post([FromBody] Category category)
    {
      var result = _categoryService.AddCategory(category);

      return result != null;
    }
  }
}
