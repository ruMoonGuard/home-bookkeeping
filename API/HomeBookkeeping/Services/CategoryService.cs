using System.Collections.Generic;
using System.Linq;
using HomeBookkeeping.Models;

namespace HomeBookkeeping.Services
{
    public interface ICategoriesService
    {
        List<Category> GetCategories();
    }
    
    public class CategoriesService : ICategoriesService
    {
        private static readonly List<Category> data = new List<Category>
        {
            new Category {Id = 1, Name = "Дом", Capacity = 15000},
            new Category {Id = 2, Name = "Еда", Capacity = 10000},
            new Category {Id = 3, Name = "Машина", Capacity = 7000}
        };

        public List<Category> GetCategories()
        {
            return data;
        }

        public Category GetCategory(int id)
        {
            return data.FirstOrDefault(c => c.Id == id);
        }

        public Category AddCategory(Category category)
        {
            return category;
        }
    }
}