using System.Collections.Generic;
using System.Linq;
using HomeBookkeeping.Database;
using HomeBookkeeping.Models;

namespace HomeBookkeeping.Services
{
    public interface IEventService
    {
        IList<Event> GetEvents();
        Event GetEvent(int id);
        Event AddEvent(Event @event);
    }
    
    public class EventService : IEventService
    {
        private readonly DatabaseContext _context;

        public EventService(DatabaseContext context)
        {
            _context = context;
        }
        
        public IList<Event> GetEvents()
        {
            return _context.Events.ToList();
        }

        public Event GetEvent(int id)
        {
            return _context.Events.FirstOrDefault(e => e.Id == id);
        }

        public Event AddEvent(Event @event)
        {
            return _context.Events.Add(@event).Entity;
        }
    }
}