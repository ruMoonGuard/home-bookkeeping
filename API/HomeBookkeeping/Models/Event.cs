using System;
using HomeBookkeeping.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomeBookkeeping.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType Type { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
