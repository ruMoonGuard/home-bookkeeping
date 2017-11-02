using System;
using HomeBookkeeping.Models.Enums;

namespace HomeBookkeeping.Models
{
  public class Bill
  {
    public int Id { get; set; }
    public decimal Value { get; set; }
    public Currency Currency { get; set; }
    public string UserId { get; set; }

    public User User { get; set; }
  }
}
