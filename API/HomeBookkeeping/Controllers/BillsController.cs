using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeeping.Controllers
{
    [Route("api/bills")]
    [Authorize(AuthenticationSchemes = "JwtBearer")]
    public class BillsController : Controller
    {
        private readonly IBillService _billService;

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public Bill Get()
        {
            return _billService.GetBill();
        }
    }
}