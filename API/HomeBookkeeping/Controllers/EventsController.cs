using System.Collections.Generic;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HomeBookkeeping.Controllers
{
    [Route("api/events")]
    [Authorize(AuthenticationSchemes = "JwtBearer")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _eventService.GetEvents();
        }

        [HttpGet("{id}")]
        public Event Get(int id)
        {
            return _eventService.GetEvent(id);
        }

        [HttpPost]
        public Event Post(Event @event)
        {
            return _eventService.AddEvent(@event);
        }
    }
}