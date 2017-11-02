using System.Collections.Generic;
using System.Linq;
using HomeBookkeeping.Database;
using HomeBookkeeping.Models;

namespace HomeBookkeeping.Services
{
  public interface ICategoriesService
  {
    IList<Category> GetCategories();
    Category GetCategory(int id);
    Category AddCategory(Category category);
  }

  public class CategoriesService : ICategoriesService
  {
    private readonly DatabaseContext _context;

    public CategoriesService(DatabaseContext context)
    {
      _context = context;
    }

    public IList<Category> GetCategories()
    {
      return _context.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
      return _context.Categories.FirstOrDefault(c => c.Id == id);
    }

    public Category AddCategory(Category category)
    {
      return _context.Categories.Add(category).Entity;
    }
  }
}
