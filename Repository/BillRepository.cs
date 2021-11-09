using System.Linq;
using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        private readonly ApplicationDbContext _db;
        
        public BillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public bool UpdateBill(Bill bill)
        {
            var existingBill = dbSet.Where(a => a.Id == bill.Id).AsNoTracking();

            if (existingBill == null) return false;
            
            _db.Update(bill);
            return true;
        }
    }
}