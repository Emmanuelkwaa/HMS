using System.Linq;
using HMS.Models;
using HMS.Repository.IRepository;
using HSM.Repository;
using System.Threading.Tasks;
using HMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HMS.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _db;
        
        public AddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public bool UpdateAddress(Address address)
        {
            var existingAddress = dbSet.Where(a => a.Id == address.Id).AsNoTracking();
        
            if (existingAddress == null) return false;
            
            dbSet.Update(address);
            return true;
        }
    }
}