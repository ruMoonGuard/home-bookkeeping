using System.Linq;
using HomeBookkeeping.Database;
using HomeBookkeeping.Models;

namespace HomeBookkeeping.Services
{
    public interface IBillService
    {
        Bill GetBill();
    }
    
    public class BillService : IBillService
    {
        private readonly DatabaseContext _context;

        public BillService(DatabaseContext context) => _context = context;

        public Bill GetBill() => _context.Bills.First(); //упрощенная форма, что бы не трахать мозг, позже переделать
    }
}