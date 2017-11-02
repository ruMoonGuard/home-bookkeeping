using System.Runtime.Serialization;

namespace HomeBookkeeping.Models.Enums
{
  public enum EventType
  {
        [EnumMember(Value = "income")]
        Income = 1,
        [EnumMember(Value = "outcome")]
        Outcome = 2
  }
}
