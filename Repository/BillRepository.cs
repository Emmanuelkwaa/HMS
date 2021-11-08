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
        
        public async Task<bool> UpdateBill(Bill bill)
        {
            var existingBill = await _db.Bills.FirstOrDefaultAsync(a => a.Id == bill.Id);

            if (existingBill == null) return false;
            
            _db.Update(bill);
            return true;
        }
    }
}