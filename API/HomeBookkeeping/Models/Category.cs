using System.Collections.Generic;

namespace HomeBookkeeping.Models
{
    public class Category
    {
      public Category()
      {
        Events = new List<Event>();
      }

      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Capacity { get; set; }

      public IList<Event> Events { get; set; }
    }
}
