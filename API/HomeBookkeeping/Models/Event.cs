using System;
using HomeBookkeeping.Models.Enums;

namespace HomeBookkeeping.Models
{
  public class Event
  {
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public EventType Type { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }
  }
}
